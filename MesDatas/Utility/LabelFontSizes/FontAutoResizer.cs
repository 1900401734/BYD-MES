using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas.Utility.IniLaguagePath
{
    public static class FontAutoResizer
    {
        // <summary>
        /// 通用方法：控件内字体随着字数的增加而自动减小，控件大小不变
        /// 适用于Label、TextBox等文本控件
        /// </summary>
        /// <typeparam name="T">控件类型，必须继承自Control并具有Text和Font属性</typeparam>
        /// <param name="control">要调整的控件</param>
        /// <param name="size">初始字体大小</param>
        /// <param name="fontStyle">字体样式</param>
        /// <param name="minSize">最小字体大小（可选，默认为6）</param>
        /// <returns>调整后的控件</returns>
        public static T ChangeControlFont<T>(T control, float size, FontStyle fontStyle, float minSize = 6f)
            where T : Control
        {
            if (control == null || string.IsNullOrEmpty(control.Text))
                return control;

            // 获取控件的字体信息
            System.Drawing.FontFamily ff = new System.Drawing.FontFamily(control.Font.Name);
            string content = control.Text;

            // 初始化控件字体状态
            control.Font = new Font(ff, size, fontStyle, GraphicsUnit.Point);

            // 特殊处理TextBox的边距
            int padding = GetControlPadding(control);

            while (size >= minSize)
            {
                // 获取当前一行能放多少个字
                int controlWidth = control.Width - padding;

                // 获取当前字体宽度和高度
                using (Graphics gh = control.CreateGraphics())
                {
                    SizeF sf = gh.MeasureString("测", control.Font); // 使用中文字符测量更准确
                    float fontWidth = sf.Width;
                    float fontHeight = sf.Height;

                    // 计算一行能放多少个字符
                    int oneRowFontNum = Math.Max(1, (int)(controlWidth / fontWidth));

                    // 判断当前控件能放多少行
                    int controlHeight = control.Height - padding;
                    int maxRows = Math.Max(1, (int)(controlHeight / fontHeight));

                    // 获取当前字符串需要多少行
                    int needRows = (int)Math.Ceiling((double)content.Length / oneRowFontNum);

                    // 如果内容能完全显示，跳出循环
                    if (needRows <= maxRows)
                    {
                        break;
                    }
                }

                // 缩小字体
                size -= 0.25f;
                if (size >= minSize)
                {
                    control.Font = new Font(ff, size, fontStyle, GraphicsUnit.Point);
                }
            }

            return control;
        }

        /// <summary>
        /// 获取不同控件类型的内边距
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>内边距值</returns>
        private static int GetControlPadding(Control control)
        {
            switch (control)
            {
                case TextBox textBox:
                    // TextBox通常有边框和内边距
                    return textBox.BorderStyle == BorderStyle.None ? 4 : 8;
                case Label label:
                    // Label通常没有边框
                    return 2;
                default:
                    return 4;
            }
        }

        /// <summary>
        /// 专门针对Label的便捷方法（保持向后兼容）
        /// </summary>
        /// <param name="label">Label控件</param>
        /// <param name="size">初始字体大小</param>
        /// <param name="fontStyle">字体样式</param>
        /// <returns>调整后的Label</returns>
        public static Label ChangeLabelFont(Label label, float size, FontStyle fontStyle)
        {
            return ChangeControlFont(label, size, fontStyle);
        }

        /// <summary>
        /// 专门针对TextBox的便捷方法
        /// </summary>
        /// <param name="textBox">TextBox控件</param>
        /// <param name="size">初始字体大小</param>
        /// <param name="fontStyle">字体样式</param>
        /// <returns>调整后的TextBox</returns>
        public static TextBox ChangeTextBoxFont(TextBox textBox, float size, FontStyle fontStyle)
        {
            return ChangeControlFont(textBox, size, fontStyle);
        }
    }

    /// <summary>
    /// 高效的动态字体管理器
    /// </summary>
    public class SmartFontManager
    {
        // 字体缓存，避免重复创建Font对象
        private static Dictionary<string, Font> fontCache = new Dictionary<string, Font>();

        // 控件配置缓存
        private static Dictionary<Control, FontConfig> controlConfigs = new Dictionary<Control, FontConfig>();

        /// <summary>
        /// 字体配置信息
        /// </summary>
        private class FontConfig
        {
            public float InitialSize { get; set; }
            public FontStyle Style { get; set; }
            public float MinSize { get; set; }
            public string FontFamily { get; set; }
            public float LastCalculatedSize { get; set; } = -1; // 缓存上次计算的字体大小
            public string LastText { get; set; } = ""; // 缓存上次的文本内容
        }

        /// <summary>
        /// 注册需要动态调整字体的控件
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="initialSize">初始字体大小</param>
        /// <param name="fontStyle">字体样式</param>
        /// <param name="minSize">最小字体大小</param>
        public static void RegisterControl(Control control, float initialSize, FontStyle fontStyle, float minSize = 6f)
        {
            var config = new FontConfig
            {
                InitialSize = initialSize,
                Style = fontStyle,
                MinSize = minSize,
                FontFamily = control.Font.Name
            };

            controlConfigs[control] = config;

            // 初始化字体
            UpdateControlFont(control);

            // 监听控件大小变化事件
            control.SizeChanged += (sender, e) =>
            {
                var ctrl = sender as Control;
                if (ctrl != null && controlConfigs.ContainsKey(ctrl))
                {
                    // 重置缓存，强制重新计算
                    controlConfigs[ctrl].LastCalculatedSize = -1;
                    controlConfigs[ctrl].LastText = "";
                    UpdateControlFont(ctrl);
                }
            };
        }

        /// <summary>
        /// 智能更新控件字体（带缓存优化）
        /// </summary>
        /// <param name="control">控件</param>
        public static void UpdateControlFont(Control control)
        {
            if (!controlConfigs.ContainsKey(control) || string.IsNullOrEmpty(control.Text))
                return;

            var config = controlConfigs[control];

            // 如果文本没有变化且控件大小没有变化，直接使用缓存的字体大小
            if (config.LastText == control.Text && config.LastCalculatedSize > 0)
            {
                ApplyFont(control, config.LastCalculatedSize, config.Style, config.FontFamily);
                return;
            }

            float optimalSize = CalculateOptimalFontSize(control, config);

            // 缓存计算结果
            config.LastCalculatedSize = optimalSize;
            config.LastText = control.Text;

            ApplyFont(control, optimalSize, config.Style, config.FontFamily);
        }

        /// <summary>
        /// 计算最优字体大小
        /// </summary>
        private static float CalculateOptimalFontSize(Control control, FontConfig config)
        {
            float size = config.InitialSize;
            string content = control.Text;
            int padding = GetControlPadding(control);

            while (size >= config.MinSize)
            {
                string fontKey = $"{config.FontFamily}_{size}_{config.Style}";
                Font testFont = GetCachedFont(fontKey, config.FontFamily, size, config.Style);

                int controlWidth = control.Width - padding;
                int controlHeight = control.Height - padding;

                using (Graphics gh = control.CreateGraphics())
                {
                    SizeF sf = gh.MeasureString("测", testFont);
                    float fontWidth = sf.Width;
                    float fontHeight = sf.Height;

                    int oneRowFontNum = Math.Max(1, (int)(controlWidth / fontWidth));
                    int maxRows = Math.Max(1, (int)(controlHeight / fontHeight));
                    int needRows = (int)Math.Ceiling((double)content.Length / oneRowFontNum);

                    if (needRows <= maxRows)
                    {
                        return size;
                    }
                }

                size -= 0.25f;
            }

            return config.MinSize;
        }

        /// <summary>
        /// 获取缓存的字体对象
        /// </summary>
        private static Font GetCachedFont(string key, string familyName, float size, FontStyle style)
        {
            if (!fontCache.ContainsKey(key))
            {
                fontCache[key] = new Font(familyName, size, style, GraphicsUnit.Point);
            }
            return fontCache[key];
        }

        /// <summary>
        /// 应用字体到控件
        /// </summary>
        private static void ApplyFont(Control control, float size, FontStyle style, string familyName)
        {
            string fontKey = $"{familyName}_{size}_{style}";
            control.Font = GetCachedFont(fontKey, familyName, size, style);
        }

        /// <summary>
        /// 获取控件内边距
        /// </summary>
        private static int GetControlPadding(Control control)
        {
            switch (control)
            {
                case TextBox textBox:
                    return textBox.BorderStyle == BorderStyle.None ? 4 : 8;
                case Label label:
                    return 2;
                default:
                    return 4;
            }
        }

        /// <summary>
        /// 批量更新多个控件的文本和字体
        /// </summary>
        /// <param name="updates">控件和新文本的字典</param>
        public static void BatchUpdateControls(Dictionary<Control, string> updates)
        {
            foreach (var update in updates)
            {
                update.Key.Text = update.Value;
                UpdateControlFont(update.Key);
            }
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        public static void Cleanup()
        {
            foreach (var font in fontCache.Values)
            {
                font?.Dispose();
            }
            fontCache.Clear();
            controlConfigs.Clear();
        }

        /// <summary>
        /// 取消注册控件
        /// </summary>
        public static void UnregisterControl(Control control)
        {
            if (controlConfigs.ContainsKey(control))
            {
                controlConfigs.Remove(control);
            }
        }
    }

    /// <summary>
    /// 扩展方法，让控件使用更简单
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// 启用智能字体调整
        /// </summary>
        public static T EnableSmartFont<T>(this T control, float initialSize, FontStyle fontStyle = FontStyle.Regular, float minSize = 6f)
            where T : Control
        {
            SmartFontManager.RegisterControl(control, initialSize, fontStyle, minSize);
            return control;
        }

        /// <summary>
        /// 更新文本并自动调整字体
        /// </summary>
        public static void SetTextSmart(this Control control, string text)
        {
            control.Text = text;
            SmartFontManager.UpdateControlFont(control);
        }
    }
}
