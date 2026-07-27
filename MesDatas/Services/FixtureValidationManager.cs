using MesDatas.DatasModel;
using MesDatas.DatasServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.Services
{
    /// <summary>
    /// 工装验证状态枚举
    /// </summary>
    public enum FixtureValidationState
    {
        NotRequired,    // 不需要验证
        Required,       // 需要验证
        InProgress,     // 验证中
        Completed,      // 验证完成
        Failed          // 验证失败
    }

    /// <summary>
    /// 工装验证结果
    /// </summary>
    public class FixtureValidationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidatedFixtures { get; set; } = new List<string>();
        public List<string> RemainingFixtures { get; set; } = new List<string>();
    }

    /// <summary>
    /// 工装验证管理器
    /// </summary>
    public class FixtureValidationManager
    {
        private string currentRecipeId;
        private List<string> requiredFixtures;
        private List<string> validatedFixtures;
        private FixtureValidationState currentState;

        // 事件委托
        public delegate void ValidationStateChangedHandler(FixtureValidationState oldState, FixtureValidationState newState);
        public delegate void ValidationMessageHandler(string message, bool isError = false);

        // 事件
        public event ValidationStateChangedHandler OnValidationStateChanged;
        public event ValidationMessageHandler OnValidationMessage;

        public FixtureValidationManager()
        {
            currentState = FixtureValidationState.NotRequired;
            requiredFixtures = new List<string>();
            validatedFixtures = new List<string>();
        }

        #region 属性

        /// <summary>
        /// 当前验证状态
        /// </summary>
        public FixtureValidationState CurrentState => currentState;

        /// <summary>
        /// 当前配方ID
        /// </summary>
        public string CurrentRecipeId => currentRecipeId;

        /// <summary>
        /// 需要的工装列表
        /// </summary>
        public List<string> RequiredFixtures => new List<string>(requiredFixtures);

        /// <summary>
        /// 已验证的工装列表
        /// </summary>
        public List<string> ValidatedFixtures => new List<string>(validatedFixtures);

        /// <summary>
        /// 剩余需要验证的工装列表
        /// </summary>
        public List<string> RemainingFixtures => requiredFixtures.Except(validatedFixtures).ToList();

        /// <summary>
        /// 是否允许进行产品条码验证
        /// </summary>
        public bool CanValidateProductBarcode =>
            currentState == FixtureValidationState.NotRequired ||
            currentState == FixtureValidationState.Completed;

        #endregion

        #region 公共方法

        /// <summary>
        /// 当配方变更时调用此方法
        /// </summary>
        /// <param name="newRecipeId">新的配方ID</param>
        /// <returns>是否需要工装验证</returns>
        public async Task<bool> OnRecipeChanged(string newRecipeId)
        {
            try
            {
                // 如果配方没有变化，不需要重新验证
                if (currentRecipeId == newRecipeId && currentState == FixtureValidationState.Completed)
                {
                    OnValidationMessage?.Invoke($"配方未变化，无需重新验证工装");
                    return false;
                }

                if (newRecipeId == "0")
                {
                    OnValidationMessage?.Invoke("配方号无效，不需要工装验证");
                    SetState(FixtureValidationState.NotRequired);
                    return false;
                }

                OnValidationMessage?.Invoke($"配方变更：{currentRecipeId} -> {newRecipeId}");

                currentRecipeId = newRecipeId;

                // 获取新配方对应的工装要求
                var recipe = await GetRecipeInfo(newRecipeId);
                if (recipe == null)
                {
                    OnValidationMessage?.Invoke($"未找到配方信息：{newRecipeId}", true);
                    SetState(FixtureValidationState.Failed);
                    return false;
                }

                // 解析工装编号
                var newRequiredFixtures = ParseFixtureNumbers(recipe.FixtureNumber);

                if (newRequiredFixtures.Count == 0)
                {
                    // 不需要工装验证
                    OnValidationMessage?.Invoke("当前配方不需要工装验证");
                    SetState(FixtureValidationState.NotRequired);
                    return false;
                }

                // 更新必需工装列表
                requiredFixtures = newRequiredFixtures;
                validatedFixtures.Clear();

                OnValidationMessage?.Invoke($"需要验证工装：{string.Join(", ", requiredFixtures)}");

                // 开始工装验证流程
                SetState(FixtureValidationState.Required);
                return true;
            }
            catch (Exception ex)
            {
                OnValidationMessage?.Invoke($"配方变更处理异常：{ex.Message}", true);
                SetState(FixtureValidationState.Failed);
                return false;
            }
        }

        /// <summary>
        /// 验证工装条码
        /// </summary>
        /// <param name="fixtureBarcode">工装条码</param>
        /// <returns>验证结果</returns>
        public FixtureValidationResult ValidateFixtureBarcode(string fixtureBarcode)
        {
            var result = new FixtureValidationResult();

            try
            {
                if (string.IsNullOrWhiteSpace(fixtureBarcode))
                {
                    result.Success = false;
                    result.Message = "工装条码为空";
                    return result;
                }

                // 检查当前状态
                if (currentState != FixtureValidationState.Required && currentState != FixtureValidationState.InProgress)
                {
                    result.Success = false;
                    result.Message = $"当前状态不允许工装验证：{currentState}";
                    return result;
                }

                SetState(FixtureValidationState.InProgress);

                // 清理条码（去除空白字符等）
                string cleanBarcode = fixtureBarcode.Trim();

                // 检查是否为需要的工装
                if (!requiredFixtures.Contains(cleanBarcode))
                {
                    result.Success = false;
                    result.Message = $"工装编号不匹配，需要的工装：{string.Join(", ", requiredFixtures)}";
                    OnValidationMessage?.Invoke($"工装验证失败：{cleanBarcode} 不在需要的工装列表中", true);
                    SetState(FixtureValidationState.Required); // 回到需要验证状态
                    return result;
                }

                // 检查是否已经验证过
                if (validatedFixtures.Contains(cleanBarcode))
                {
                    result.Success = false;
                    result.Message = $"工装 {cleanBarcode} 已经验证过";
                    OnValidationMessage?.Invoke($"工装 {cleanBarcode} 重复验证");
                    return result;
                }

                // 添加到已验证列表
                validatedFixtures.Add(cleanBarcode);
                result.ValidatedFixtures = new List<string>(validatedFixtures);
                result.RemainingFixtures = RemainingFixtures;

                OnValidationMessage?.Invoke($"工装验证成功：{cleanBarcode}");

                // 检查是否所有工装都已验证
                if (RemainingFixtures.Count == 0)
                {
                    // 所有工装验证完成
                    SetState(FixtureValidationState.Completed);
                    result.Success = true;
                    result.Message = "所有工装验证完成";
                    OnValidationMessage?.Invoke("所有工装验证完成，允许进行产品条码验证");
                }
                else
                {
                    // 还有工装需要验证
                    result.Success = true;
                    result.Message = $"工装验证成功，还需验证：{string.Join(", ", RemainingFixtures)}";
                    OnValidationMessage?.Invoke($"还需验证工装：{string.Join(", ", RemainingFixtures)}");
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"工装验证异常：{ex.Message}";
                OnValidationMessage?.Invoke($"工装验证异常：{ex.Message}", true);
                SetState(FixtureValidationState.Failed);
                return result;
            }
        }

        /// <summary>
        /// 重置验证状态
        /// </summary>
        public void Reset()
        {
            currentRecipeId = null;
            requiredFixtures.Clear();
            validatedFixtures.Clear();
            SetState(FixtureValidationState.NotRequired);
            OnValidationMessage?.Invoke("工装验证状态已重置");
        }

        /// <summary>
        /// 强制完成验证（用于调试或特殊情况）
        /// </summary>
        public void ForceComplete()
        {
            SetState(FixtureValidationState.Completed);
            OnValidationMessage?.Invoke("工装验证已强制完成");
        }

        /// <summary>
        /// 获取验证状态描述
        /// </summary>
        /// <returns>状态描述</returns>
        public string GetStateDescription()
        {
            switch (currentState)
            {
                case FixtureValidationState.NotRequired:
                    return "不需要工装验证";
                case FixtureValidationState.Required:
                    return $"需要验证工装：{string.Join(", ", RemainingFixtures)}";
                case FixtureValidationState.InProgress:
                    return $"工装验证中，还需验证：{string.Join(", ", RemainingFixtures)}";
                case FixtureValidationState.Completed:
                    return $"工装验证完成：{string.Join(", ", validatedFixtures)}";
                case FixtureValidationState.Failed:
                    return "工装验证失败";
                default:
                    return "未知状态";
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置验证状态
        /// </summary>
        /// <param name="newState">新状态</param>
        private void SetState(FixtureValidationState newState)
        {
            var oldState = currentState;
            currentState = newState;
            OnValidationStateChanged?.Invoke(oldState, newState);
        }

        /// <summary>
        /// 获取配方信息
        /// </summary>
        /// <param name="recipeId">配方ID</param>
        /// <returns>配方信息</returns>
        private async Task<RecipeEntity> GetRecipeInfo(string recipeId)
        {
            return await Task.Run(() => RecipeManage.GetCodes(recipeId));
        }

        /// <summary>
        /// 解析工装编号字符串
        /// </summary>
        /// <param name="fixtureNumberString">工装编号字符串</param>
        /// <returns>工装编号列表</returns>
        private List<string> ParseFixtureNumbers(string fixtureNumberString)
        {
            var fixtures = new List<string>();

            if (string.IsNullOrWhiteSpace(fixtureNumberString))
            {
                return fixtures;
            }

            // 按 '+' 分割工装编号
            var parts = fixtureNumberString.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                var trimmed = part.Trim();
                if (!string.IsNullOrEmpty(trimmed) && !fixtures.Contains(trimmed))
                {
                    fixtures.Add(trimmed);
                }
            }

            return fixtures;
        }

        #endregion
    }

    /// <summary>
    /// 工装与产品关联验证器
    /// </summary>
    public static class FixtureProductValidator
    {
        /// <summary>
        /// 验证工装是否兼容指定产品
        /// </summary>
        /// <param name="fixtureNumber">工装编号</param>
        /// <param name="productName">产品型号</param>
        /// <returns>是否兼容</returns>
        public static async Task<bool> IsFixtureCompatibleWithProduct(string fixtureNumber, string productName)
        {
            try
            {
                // 获取所有配方
                var recipes = await Task.Run(() => RecipeManage.GetCodesList());

                if (recipes == null || recipes.Count == 0)
                {
                    return false;
                }

                // 查找包含指定工装和产品的配方
                var compatibleRecipe = recipes.FirstOrDefault(r =>
                    r.ProductName == productName &&
                    !string.IsNullOrEmpty(r.FixtureNumber) &&
                    r.FixtureNumber.Contains(fixtureNumber));

                return compatibleRecipe != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取工装兼容的产品列表
        /// </summary>
        /// <param name="fixtureNumber">工装编号</param>
        /// <returns>兼容的产品列表</returns>
        public static async Task<List<string>> GetCompatibleProducts(string fixtureNumber)
        {
            var products = new List<string>();

            try
            {
                var recipes = await Task.Run(() => RecipeManage.GetCodesList());

                if (recipes != null)
                {
                    var compatibleRecipes = recipes.Where(r =>
                        !string.IsNullOrEmpty(r.FixtureNumber) &&
                        r.FixtureNumber.Contains(fixtureNumber) &&
                        !string.IsNullOrEmpty(r.ProductName));

                    products = compatibleRecipes.Select(r => r.ProductName).Distinct().ToList();
                }
            }
            catch (Exception)
            {
                // 异常处理
            }

            return products;
        }

        /// <summary>
        /// 获取产品需要的所有工装
        /// </summary>
        /// <param name="productName">产品型号</param>
        /// <returns>工装列表</returns>
        public static async Task<List<string>> GetRequiredFixtures(string productName)
        {
            var fixtures = new List<string>();

            try
            {
                var recipes = await Task.Run(() => RecipeManage.GetCodesList());

                if (recipes != null)
                {
                    var productRecipes = recipes.Where(r => r.ProductName == productName);

                    foreach (var recipe in productRecipes)
                    {
                        if (!string.IsNullOrEmpty(recipe.FixtureNumber))
                        {
                            var fixtureNumbers = recipe.FixtureNumber.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var fixture in fixtureNumbers)
                            {
                                var trimmed = fixture.Trim();
                                if (!string.IsNullOrEmpty(trimmed) && !fixtures.Contains(trimmed))
                                {
                                    fixtures.Add(trimmed);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 异常处理
            }

            return fixtures;
        }
    }
}
