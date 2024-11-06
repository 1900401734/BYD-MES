using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesDatas
{
    public static class UIUpdateUtils
    {

        // 委托定义用于包装各种UI更新操作
        public delegate void UpdateUIDelegate();

        /// <summary>
        /// 安全地在UI线程上执行控件更新
        /// </summary>
        /// <param name="control">要更新的控件</param>
        /// <param name="action">更新操作</param>
        public static void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// UI状态更新器类，用于批量管理UI更新
        /// </summary>
        public class UIStateUpdater
        {
            private readonly Control _parentControl;
            private readonly Queue<Action> _updateActions;

            public UIStateUpdater(Control parentControl)
            {
                _parentControl = parentControl;
                _updateActions = new Queue<Action>();
            }

            public UIStateUpdater SetLabel(Label label, string text, Color? color = null)
            {
                _updateActions.Enqueue(() =>
                {
                    label.Text = text;
                    if (color.HasValue)
                        label.ForeColor = color.Value;
                });
                return this;
            }

            public UIStateUpdater SetRichTextBox(RichTextBox rtb, string text, bool clearFirst = false)
            {
                _updateActions.Enqueue(() =>
                {
                    if (clearFirst)
                        rtb.Clear();
                    rtb.AppendText(text);
                });
                return this;
            }

            public void Update()
            {
                if (_updateActions.Count == 0) return;

                void UpdateAll()
                {
                    while (_updateActions.Count > 0)
                    {
                        var action = _updateActions.Dequeue();
                        action();
                    }
                }

                SafeInvoke(_parentControl, UpdateAll);
            }
        }
    }
}
