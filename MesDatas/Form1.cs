using ADOX;
using HFrfid;
using HslCommunication;
using HslCommunication.Core;
using HslCommunication.Core.Net;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Keyence;
using HslCommunication.Profinet.Melsec;
using HslCommunication.Profinet.Omron;
using HslCommunication.Profinet.Yamatake;
using INIFile;
using MathNet.Numerics.Distributions;
using MesDatas.DatasDataGridView;
using MesDatas.DatasModel;
using MesDatas.DatasServer;
using MesDatas.Entity;
using MesDatas.MESModel;
using MesDatas.Services;
using MesDatas.SqlConverter;
using MesDatas.Utiey;
using MesDatas.Utility.IniLaguagePath;
using MesDatas.Utility.PrintersFileChecker;
using MesDatas.Utility.ResourcesLaguage;
using MesDatas.Utility.SugarDB;
using MesDatasCore;
using Microsoft.VisualBasic;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using NLog;
using NPOI.OpenXmlFormats.Vml;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Org.BouncyCastle.Utilities.Net;
using Seagull.BarTender.Print;
using SqlSugar;
using Sunisoft.IrisSkin;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Interop;
using System.Xml;
using System.Xml.Linq;
using UltimateFileSoftwareUpdate.UpdateModel;
using 工艺部信息化组;
using static MesDatas.Fwelcome;

namespace MesDatas
{
    public partial class Form1 : Form
    {
        #region ------------ 登录状态相关的属性与方法 ------------

        private int MES { get; set; }
        public void SetMES(int strText)
        {
            MES = strText;
        }

        /// <summary>
        /// 登录模式 (联机或单机) 0=联机  1=单机
        /// </summary>
        public string LoginMode { get; private set; }
        /// <summary>
        /// 0=联机  1=单机
        /// </summary>
        private int isOffLine { get; set; }
        public void SetoffLineType(int strText)
        {
            isOffLine = strText;
            LoginMode = isOffLine == 0 ? "联机" : "单机";
        }

        /// <summary>
        /// 登录方式(刷卡或密码)
        /// </summary>
        public string LoginMethod { get; private set; }
        private int CheckCard { get; set; }
        public void Setcheckcard(int strText)
        {
            CheckCard = strText;
            LoginMethod = CheckCard == 0 ? "密码登录" : "刷卡登录";
        }

        private int Access_take { get; set; }
        public void Setaccess_take(int strText)
        {
            Access_take = strText;
        }

        /// <summary>
        /// 用户权限
        /// </summary>
        private int Access { get; set; }
        public void Setaccess(int strText)
        {
            Access = strText;
            GetAccessName(Access);
        }

        /// <summary>
        /// 权限名
        /// </summary>
        public string AccessName { get; set; }
        private void GetAccessName(int access)
        {
            switch (access)
            {
                case 1:
                    AccessName = "OP";
                    break;
                case 2:
                    AccessName = "PE";
                    break;
                case 3:
                    AccessName = "ADM";
                    break;
                case 4:
                    AccessName = "DEV";
                    break;
                case 5:
                    AccessName = "QE";
                    break;
                case 6:
                    AccessName = "ME";
                    break;
                default:
                    AccessName = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 登录用户（工号）
        /// </summary>
        private string LoginUser { get; set; }
        public void SetloginUser(string strText)
        {
            LoginUser = strText;
        }

        /// <summary>
        /// 登录名称（姓名）
        /// </summary>
        private string LoginName { get; set; }
        public void SetloginName(string strText)
        {
            if (strText == null)
            {
                LoginName = "开发者";
            }
            else
            {
                LoginName = strText;
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        private string LoginPwd { get; set; }
        public void SetloginPwd(string strText)
        {
            LoginPwd = strText;
        }

        #endregion

        #region ------------ KPI产线关键性能指标字段 ------------

        // 基本生产数量指标
        /// <summary>
        /// 工单数量(从MES获取)
        /// </summary>
        private string orderQuantity;
        /// <summary>
        /// 工单完成数量(MES获取)
        /// </summary>
        private string completedQuantity;
        /// <summary>
        /// 合格数量
        /// </summary>
        private string passQuantity;
        /// <summary>
        /// NG数量
        /// </summary>
        private string NGQuantity;
        /// <summary>
        /// 生产总数
        /// </summary>
        private string totalQuantity;
        // 生产效率指标
        /// <summary>
        /// 完成率(MES获取)
        /// </summary>
        private string completeRate;
        /// <summary>
        /// 合格率
        /// </summary>
        private string passRate;
        /// <summary>
        /// 不良率
        /// </summary>
        private string failRate;
        /// <summary>
        /// 直通率 FPY：First-pass yield = TPY：ThroughPut Yield
        /// </summary>
        private string FPY;
        // 节拍指标(生产节拍,整体节拍,机械节拍,人工节拍)
        /// <summary>
        /// 生产节拍
        /// </summary>
        private string productionCycleTime;
        /// <summary>
        /// 整体节拍
        /// </summary>
        private string overallCycleTime;
        /// <summary>
        /// 机械节拍
        /// </summary>
        private string machineCycleTime;
        /// <summary>
        /// 人工节拍
        /// </summary>
        private string manualCycleTime;
        // 设备维护与时间指标
        /// <summary>
        /// 保养计数
        /// </summary>
        private string maintenanceCount;
        /// <summary>
        /// 工序时间
        /// </summary>
        private string processTime;
        /// <summary>
        /// 利用时间
        /// </summary>
        private string usingTime;
        /// <summary>
        /// 负荷时间
        /// </summary>
        private string loadTime;

        #endregion

        #region ------------ 工装验证相关字段和属性 ------------

        /// <summary>
        /// 工装验证管理器
        /// </summary>
        private FixtureValidationManager fixtureValidationManager;

        /// <summary>
        /// 工装条码读取点位（从PLC配置中获取）
        /// </summary>
        private string fixtureBarcodePLCAddress = "1800"; // 可配置

        /// <summary>
        /// 工装条码长度
        /// </summary>
        private ushort fixtureBarcodeLength = 20; // 可配置

        private string LastFixtureBarcode = string.Empty;

        private int FixtureCounter = 1;

        #endregion

        #region ------------ 工装验证初始化 ------------

        /// <summary>
        /// 初始化工装验证管理器
        /// </summary>
        private void InitializeFixtureValidation()
        {
            fixtureValidationManager = new FixtureValidationManager();

            // 订阅事件
            fixtureValidationManager.OnValidationStateChanged += OnFixtureValidationStateChanged;
            fixtureValidationManager.OnValidationMessage += OnFixtureValidationMessage;

            DisplayMessage("工装验证管理器初始化完成");
        }

        /// <summary>
        /// 工装验证状态变更事件处理
        /// </summary>
        /// <param name="oldState">旧状态</param>
        /// <param name="newState">新状态</param>
        private void OnFixtureValidationStateChanged(FixtureValidationState oldState, FixtureValidationState newState)
        {
            this.InvokeAsync(() =>
            {
                UpdateFixtureValidationUI(newState);
                DisplayMessage($"工装验证状态变更：{oldState} -> {newState}");
            });
        }

        /// <summary>
        /// 工装验证消息事件处理
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="isError">是否为错误消息</param>
        private void OnFixtureValidationMessage(string message, bool isError = false)
        {
            this.InvokeAsync(() =>
            {
                if (isError)
                {
                    DisplayMessage($"[工装验证错误] {message}");
                }
                else
                {
                    DisplayMessage($"[工装验证] {message}");
                }
            });
        }

        #endregion

        #region ------------ 工装验证UI更新 ------------

        /// <summary>
        /// 更新工装验证相关的UI显示
        /// </summary>
        /// <param name="state">当前验证状态</param>
        private void UpdateFixtureValidationUI(FixtureValidationState state)
        {
            switch (state)
            {
                case FixtureValidationState.NotRequired:
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("RSFixture_NotRequire");    // 无需工装验证
                    lblOperatePrompt.ForeColor = Color.Black;
                    lblOperatePrompt.Text = resources.GetString("OT_WaitingScan");          // 等待扫描条码
                    txtFixtureBinding.BackColor = Color.White;
                    break;

                case FixtureValidationState.Required:
                    lblRunningStatus.ForeColor = Color.Orange;
                    lblRunningStatus.Text = resources.GetString("RSFixture_RequireValidate");   // 需要工装验证
                    lblOperatePrompt.ForeColor = Color.Orange;
                    lblOperatePrompt.Text = resources.GetString("OT_ScanFixture");              // 等待扫工装
                    txtFixtureBinding.BackColor = Color.LightYellow;

                    // 显示需要的工装列表
                    //var requiredFixtures = fixtureValidationManager.RequiredFixtures;
                    //if (requiredFixtures.Count > 0)
                    //{
                    //    lblOperatePrompt.Text += $" (需要: {string.Join(", ", requiredFixtures)})";
                    //    MessageBox.Show($" (需要: {string.Join(", ", requiredFixtures)})");
                    //}
                    break;

                case FixtureValidationState.InProgress:
                    lblRunningStatus.ForeColor = Color.Blue;
                    lblRunningStatus.Text = resources.GetString("RSFixture_Validating");    // 工装验证中
                    lblOperatePrompt.ForeColor = Color.Blue;
                    lblOperatePrompt.Text = resources.GetString("OT_ScanFixtureContinue");
                    txtFixtureBinding.BackColor = Color.LightBlue;
                    break;

                case FixtureValidationState.Completed:
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("RSFixture_OK");    // 工装验证通过
                    lblOperatePrompt.ForeColor = Color.Black;
                    lblOperatePrompt.Text = resources.GetString("OT_WaitingScan"); // 等待扫描条码
                    txtFixtureBinding.BackColor = Color.LightGreen;

                    // 更新工装绑定显示
                    var validatedFixtures = fixtureValidationManager.ValidatedFixtures;
                    txtFixtureBinding.Text = string.Join("+", validatedFixtures);
                    break;

                case FixtureValidationState.Failed:
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSFixture_NG");        // 工装编号验证失败
                    lblOperatePrompt.ForeColor = Color.Red;
                    lblOperatePrompt.Text = resources.GetString("OT_ScanFixture");      // 等待扫工装
                    txtFixtureBinding.BackColor = Color.LightPink;
                    break;
            }

            // 更新状态描述
            string stateDescription = fixtureValidationManager.GetStateDescription();
            // 可以在某个Label或ToolTip中显示详细状态
        }

        #endregion

        #region ------------ 配方变更处理 ------------

        /// <summary>
        /// 配方变更UI提醒
        /// </summary>
        /// <param name="newRecipeId">新配方ID</param>
        private async Task HandleRecipeChange(string newRecipeId)
        {
            if (chkBypassFixtureValidation.Checked)
            {
                await readWriteNet.WriteAsync(ssd.FixtureOK, Convert.ToInt16(1)); // 工装验证通过
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(newRecipeId))
                {
                    DisplayMessage("配方ID为空，跳过工装验证");
                    return;
                }

                DisplayMessage($"检测到配方变更：{newRecipeId}");

                // 通知工装验证管理器配方变更
                bool needsValidation = await fixtureValidationManager.OnRecipeChanged(newRecipeId);

                if (needsValidation)
                {
                    DisplayMessage("配方变更完成，需要进行工装验证");
                    // 开始监听工装条码
                    StartFixtureBarcodeMonitoring();
                }
                else
                {
                    DisplayMessage("配方变更完成，无需工装验证");
                    // 保存工装绑定信息到数据库
                    await SaveFixtureBindingToDatabase();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"配方变更处理异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新配方相关的UI
        /// </summary>
        /// <param name="recipeId">配方ID</param>
        private void UpdateRecipeRelatedUI(string recipeId)
        {
            try
            {
                //更新配方选择控件
                if (cboBarcodeRule.SelectedValue?.ToString() != recipeId)
                {
                    cboBarcodeRule.SelectedValue = recipeId;
                }

                lblRecipeId.Text = recipeId;

                // 更新产品信息
                if (recipeInfoBindingList.Count > 0)
                {
                    var findCodes = recipeInfoBindingList.FirstOrDefault(find => find.RecipeID == recipeId);
                    if (findCodes != null)
                    {
                        txtProductCode.Text = findCodes.ProductCode ?? "";
                        productModel = txtProductModel.Text = findCodes.ProductName ?? "";
                        // 注意：不要在这里更新txtFixtureBinding.Text，让工装验证管理器来管理
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"更新配方UI异常：{ex.Message}");
            }
        }

        #endregion

        #region ------------ 工装条码监听和处理 ------------

        /// <summary>
        /// 开始监听工装条码
        /// </summary>
        private async Task StartFixtureBarcodeMonitoring()
        {
            await readWriteNet.WriteAsync(ssd.FixtureValidate, Convert.ToInt16(1));
            DisplayMessage("开始监听工装条码");
        }

        /// <summary>
        /// 从PLC读取工装条码并处理
        /// 这个方法应该在现有的PLC数据读取循环中调用
        /// </summary>
        private async Task ProcessFixtureBarcodeFromPLC()
        {
            if (chkBypassFixtureValidation.Checked)
            {
                await readWriteNet.WriteAsync(ssd.FixtureOK, Convert.ToInt16(1)); // 工装验证通过
                return;
            }

            // 只有在需要工装验证时才读取工装条码
            if (!fixtureValidationManager.CanValidateProductBarcode &&
                (fixtureValidationManager.CurrentState == FixtureValidationState.Required ||
                 fixtureValidationManager.CurrentState == FixtureValidationState.InProgress))
            {
                try
                {
                    // 从PLC读取工装条码
                    if (!ushort.TryParse(txtFixtureLength.Text, out var lenght))
                    {
                        lenght = 20;
                    }
                    var result = await readWriteNet.ReadStringAsync(ssd.FixtureNumber, lenght);

                    if (result.IsSuccess)
                    {
                        string rawBarcode = result.Content;
                        string cleanBarcode = CodeNum.CleanString(rawBarcode);

                        if (!string.IsNullOrWhiteSpace(cleanBarcode))
                        {
                            // 核心逻辑：比较当前条码和上一个条码
                            if (cleanBarcode == LastFixtureBarcode)
                            {
                                // 条码与上一次相同，增加计数器
                                FixtureCounter++;
                            }
                            else
                            {
                                // 条码是新的，重置计数器并更新上一个条码记录
                                FixtureCounter = 1; // 这是新条码的第一次出现
                                LastFixtureBarcode = cleanBarcode;
                                DisplayMessage($"检测到新的工装条码，开始验证：{cleanBarcode}");
                            }


                            // 如果同一个条码连续出现的次数超过3次，则退出当前验证流程
                            // 直到下一次循环读到一个不同的条码
                            if (FixtureCounter > 3)
                            {
                                // 可以选择性地在这里显示一条提示信息，说明正在等待新条码
                                if (DateTime.Now.Second % 60 == 0) // 每60秒提示一次，避免刷屏
                                {
                                    DisplayMessage($"条码 {cleanBarcode} 已连续处理多次，等待新条码...");
                                }
                                return; // 退出，不执行下面的验证
                            }

                            // 如果是前3次（或更少）读取到这个条码，则执行验证
                            DisplayMessage($"第 {FixtureCounter} 次读取到工装条码：{cleanBarcode}");

                            // 处理工装条码验证
                            await ProcessFixtureValidation(cleanBarcode);
                        }
                    }
                    else
                    {
                        // PLC读取失败时的处理
                        if (DateTime.Now.Second % 10 == 0) // 每10秒记录一次，避免日志过多
                        {
                            DisplayMessage($"读取工装条码失败：{result.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage($"读取工装条码异常：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 处理工装验证
        /// </summary>
        /// <param name="fixtureBarcode">工装条码</param>
        private async Task ProcessFixtureValidation(string fixtureBarcode)
        {
            try
            {
                // 使用工装验证管理器进行验证
                var validationResult = fixtureValidationManager.ValidateFixtureBarcode(fixtureBarcode);

                if (validationResult.Success)
                {
                    DisplayMessage($"工装验证成功：{fixtureBarcode}");

                    // 如果还有未验证的工装，继续等待
                    if (validationResult.RemainingFixtures.Count > 0)
                    {
                        DisplayMessage($"还需验证工装：{string.Join(", ", validationResult.RemainingFixtures)}");
                    }
                    else
                    {
                        // 所有工装验证完成
                        DisplayMessage("所有工装验证完成，可以开始产品条码验证");
                        await OnAllFixturesValidated();
                    }
                }
                else
                {
                    DisplayMessage($"工装验证失败：{validationResult.Message}");

                    // 反馈给PLC（工装验证失败）
                    try
                    {
                        await readWriteNet.WriteAsync(ssd.FixtureOK, Convert.ToInt16(0)); // 工装验证失败信号
                        DisplayMessage("已向PLC反馈工装验证失败");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"向PLC反馈工装验证失败时异常：{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"工装验证处理异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 所有工装验证完成后的处理
        /// </summary>
        private async Task OnAllFixturesValidated()
        {
            try
            {
                // 保存工装绑定信息到数据库
                await SaveFixtureBindingToDatabase();

                // 反馈给PLC（工装验证成功）
                if (isPlcConnected)
                {
                    try
                    {
                        await readWriteNet.WriteAsync(ssd.FixtureOK, Convert.ToInt16(1)); // 工装验证成功信号
                        DisplayMessage("已向PLC反馈工装验证成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"向PLC反馈工装验证成功时异常：{ex.Message}");
                    }
                }

                DisplayMessage("工装验证流程完成");
            }
            catch (Exception ex)
            {
                DisplayMessage($"工装验证完成处理异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 保存工装绑定信息到数据库
        /// </summary>
        private async Task SaveFixtureBindingToDatabase()
        {
            try
            {
                await Task.Run(() =>
                {
                    dbHelper = new MDBHelper(path4);
                    string sql = "update [SytemSet] set [BoardBeat]='" + txtFixtureBinding.Text + "' where [ID] = '1'";
                    var result = dbHelper.Change(sql);
                    dbHelper.CloseConnection();

                    if (result)
                    {
                        DisplayMessage("工装绑定信息已保存到数据库");
                    }
                    else
                    {
                        DisplayMessage("工装绑定信息保存失败");
                    }
                });
            }
            catch (Exception ex)
            {
                DisplayMessage($"保存工装绑定信息异常：{ex.Message}");
            }
        }

        #endregion

        #region ------------ 产品条码验证集成 ------------

        /// <summary>
        /// 工装验证前置检查
        /// </summary>
        /// <param name="productBarcode">产品条码</param>
        /// <returns>是否允许继续验证</returns>
        private async Task<bool> CheckFixtureValidation(BarcodeVerification bv)
        {
            if (chkBypassFixtureValidation.Checked)
            {
                await readWriteNet.WriteAsync(ssd.FixtureOK, Convert.ToInt16(1)); // 工装验证完成
                return true;
            }

            // 检查工装验证状态
            if (!fixtureValidationManager.CanValidateProductBarcode)
            {
                string stateMessage = fixtureValidationManager.GetStateDescription();

                this.InvokeAsync(() =>
                {
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSFixture_NG");
                    lblOperatePrompt.ForeColor = Color.Red;
                    lblOperatePrompt.Text = resources.GetString("OT_ScanFixture");
                });

                DisplayMessage($"产品条码验证被阻止：工装验证未完成 - {stateMessage}");

                try
                {

                    await readWriteNet.WriteAsync(ssd.FixtureOK, Convert.ToInt16(0)); // 工装验证未完成
                    DisplayMessage($"已向PLC反馈工装验证未完成：{ssd.FixtureOK} = 0");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"向PLC反馈工装验证状态异常：{ex.Message}");
                }

                return false;
            }

            return true;
        }

        #endregion

        /// <summary>
        /// <para>
        /// MES_Result[2002] = "1" 条码验证通过，
        /// MES_Result[2004] = "1" 条码验证失败；
        /// </para>
        /// MES_Result[2006] = "1" 数据上传成功，
        /// MES_Result[2008] = "1" 数据上传失败；
        /// <para>
        /// MES_Result[3688] = "3" 产品总结果OK，否则NG；
        /// </para>
        /// </summary>
        public string[] MES_Result = new string[10000];
        /// <summary>
        /// 最终产品结果
        /// </summary>
        string ProductResult;
        /// <summary>
        ///产品型号 = 产品名称 = 产品编号
        /// </summary>
        private string productModel;
        /// <summary>
        /// 配方号
        /// </summary>
        private string recipeId;
        /// <summary>
        /// 工单号
        /// </summary>
        private string orderNumber;
        /// <summary>
        /// 设备状态
        /// </summary>
        private string deviceState;

        // 存储测试项目对应的PLC点位数据
        string[] targetStationNum = new string[] { };       // 目标工位序号
        string[] testItemsName_Chinese = new string[] { };  // 测试项目名称
        string[] TestItemsName_English = new string[] { };  // 测试项目英文名称
        string[] TestItemsName_Upload = new string[] { };   // 用于数据上传的Pascal名称
        string[] TestItemsName_Thai = new string[] { };     // 测试项目泰文名称
        string[] actualValuePoint = new string[] { };       // 实际值点位
        string[] maxValuePoint = new string[] { };          // 上限点位
        string[] minValuePoint = new string[] { };          // 下限点位
        string[] beatPoint = new string[] { };              // 节拍点位
        string[] resultPoint = new string[] { };            // 结果点位
        string[] unitName = new string[] { };               // 单位
        string[] standardValuePoint = new string[] { };     // 标准值点位

        // 存储ReadData对应的测试数据
        List<string> testList;// 实际值
        List<string> beatList;       // 节拍
        List<string> maxList;        // 上限
        List<string> minList;        // 下限
        List<string> resultList;     // 结果
        List<string> nameList;       // 工位名称

        string[] stationNameSets = new string[] { };        // 工位名称集合
        string[] stationNameSets_English = new string[] { };// 工位名称集合
        string[] stationNameSets_Thai = new string[] { };   // 工位名称集合
        string[] kpisPointSets = new string[] { };  // 生产指标点位集合
        string[] kpisNameSets = new string[] { };   // 生产指标名称集合（Chinese）
        string[] kpisEnglishSets = new string[] { };// 生产指标名称集合（English）
        string[] kpisThaiSets = new string[] { };   // 生产指标名称集合（Thailand）

        MDBHelper dbHelper;
        Assembly assembly = Assembly.GetExecutingAssembly();
        ResourceManager resources;
        int LanguageId = 0;

        public static int iOperCount = 0;
        public static System.Timers.Timer timer;    // 用于ADM计时退出
        private System.Windows.Forms.Timer timer1;  // 用于实时更新时间

        // 字典用于存储每个CheckBox的初始状态
        private Dictionary<System.Windows.Forms.CheckBox, bool> checkBoxStates = new Dictionary<System.Windows.Forms.CheckBox, bool>();
        Dictionary<string, string> faultsMap = new Dictionary<string, string>();    // 故障映射

        Logger loggerConfig = LogManager.GetLogger("ArgumentConfigLog");
        Logger loggerAccount = LogManager.GetLogger("AccountManageLog");
        Logger logProduction = LogManager.GetLogger("ProductionLog");

        private CancellationTokenSource _cts1;
        public Form1()
        {
            string language = Properties.Settings.Default.DefaultLanguage;
            if (language == "zh-CN")
            {
                LanguageId = 0;
                resources = new ResourceManager("MesDatas.Language_Resources.language_Chinese", assembly);
            }
            else if (language == "en-US")
            {
                LanguageId = 1;
                resources = new ResourceManager("MesDatas.Language_Resources.language_English", assembly);
            }
            else if (language == "th-TH")
            {
                LanguageId = 2;
                resources = new ResourceManager("MesDatas.Language_Resources.language_Thai", assembly);
            }

            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScaleDimensions = new SizeF(96F, 96F);

            InitializeComponent();

            // 延迟设置窗体状态，确保初始化完成
            this.Load += (s, e) => this.WindowState = FormWindowState.Maximized;

            lblLoginMode.Click -= new EventHandler(SwitchLoginMode); // Add it once
            lblLoginMode.Click += new EventHandler(SwitchLoginMode);

            logUpdateTimer = new System.Windows.Forms.Timer { Interval = 500 };
            logUpdateTimer.Tick += LogUpdateTimer_Tick;
            logUpdateTimer.Start();

            InitializeTimer();      // 实时更新当前时间

            // 开启监听键盘和鼠标操作
            Application.AddMessageFilter(new MyIMessageFilter());

            //bvList = BarcodeVerificationServer.GetBarcodeVerificationList(LanguageId);
            bvList = BarcodeVerificationServer.GetAllBarcodeVerifications();

            _dataManager = new StationDataManager();
            InitializeAsync();
            StartCleanupTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            WhiteNightShift.GetShift(DateTime.Now);

            InitializeFixtureValidation();  // 初始化工装验证

            SearchPort();               // 初始化刷卡器端口

            LoadDashboardConfig();      // 读取看板参数配置

            LoadParameter_MES();        // 读取联机参数设置

            SYS_BOARD();                // 读取PLC点位集合    

            LoadSystemConfig();         // 读取系统设置

            InitializeDirectoryTree();  // 初始化加载本地历史数据

            PLCBarQRCode();             // 条码验证表格

            switch (LanguageId)
            {
                case 0:
                    lblDeviceName.Text = txtDeviceName.Text;
                    break;
                case 1:
                    lblDeviceName.Text = txtDeviceName_English.Text;
                    break;
                case 2:
                    lblDeviceName.Text = txtDeviceName_Thai.Text;
                    break;
                default:
                    lblDeviceName.Text = txtDeviceName.Text;
                    break;
            }

            // 机台名称
            FontAutoResizer.ChangeLabelFont(lblDeviceName, 42F, FontStyle.Bold);

            // 添加工具提示
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(lblLoginMode, "点击切换在线/离线模式");

            if (txtStationNameSets.Text.Length > 0)
            {
                if (chkDoubleStation.Checked || chkLeftRight.Checked)
                {
                    stationNameSets = string.IsNullOrEmpty(txtStationNameSets.Text)
                              ? new string[] { "Left", "Right" }
                               : txtStationNameSets.Text.Split('|');
                }
                else
                {
                    stationNameSets = string.IsNullOrEmpty(txtStationNameSets.Text)
                              ? new string[] { "station name" }
                               : txtStationNameSets.Text.Split('|');
                }
                stationNameSets_English = txtStationNameSets_English.Text.Split('|');
                stationNameSets_Thai = txtStationNameSets_Thai.Text.Split('|');

                kpisPointSets = txtPointSets.Text.ToString().Split('|');            // 生产指标点位集合
                kpisNameSets = txtNameSets.Text.ToString().Split('|');              // 生产指标名称集合（Chinese）
                kpisEnglishSets = txtEnglishNameSets.Text.ToString().Split('|');    // 生产指标名称集合（English）
                kpisThaiSets = txtThaiNameSets.Text.ToString().Split('|');          // 生产指标名称集合（Thai)
                TestItemsName_Upload = PascalCaseConverter.ConvertToPascalCase(TestItemsName_English);
            }

            btnRefreshUser_Click(null, null);  // 用户管理刷新按钮

            GetPrinterName();           // 从系统获取打印机名称

            LoadPrinterConfig();        // 加载打印机配置

            InitializeSerialNumber();   // 初始化流水号

            UpLoginInfo();              // 修改最后登录时间和次数

            try
            {
                ConiferFile coniferFile = ConiferFile.GetJson();
                lblVersion.Text = coniferFile.Version;
            }
            catch { }   // 版本号

            InitMaxMinDataTable();      // 初始化最大最小数据列

            InitializeDatabaseOnStartup();  // 初始化生产数据库
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                AssignUI(); // 根据用户权限分配主界面菜单
                splitContainer_LR.SetPercent(0.73); // 设置splitContainer1为70%-30%分割

                InitializeCheckBoxStates(this.Controls);

                LogManager.Configuration.Variables["LoginName"] = LoginName;

                string loginInfo = $"【用户登录】\n工号：{LoginUser} | 姓名：{LoginName} | 权限：{AccessName} | 登录模式：{LoginMode} | 登录方式：{LoginMethod}";
                loggerAccount.Trace(loginInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // 存储和加载故障信息
            try
            {
                string jsonStr = File.ReadAllText(pathText);
                string jsonStr1 = File.ReadAllText(pathText1);
                string jsonStr2 = File.ReadAllText(pathText2);

                if (!string.IsNullOrWhiteSpace(jsonStr))
                {
                    faultsMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
                }

                if (faultsMap == null)
                {
                    faultsMap = new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                // 用户登录验证
                if (isOffLine == 0)
                {
                    await VarifyUserLogin_MES();
                }
                else
                {
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("RSUser_OfflineOK");    // 单机用户验证成功
                    lblOperatePrompt.ForeColor = Color.Black;
                    lblOperatePrompt.Text = resources.GetString("OT_WaitingScan");     // 等待扫描条码
                }

                ConnectDashboard();                         // 连接看板
                //ConnectPLC();                             // 连接PLC
                Task.Factory.StartNew(() => ManagePlcConnectionAsync(), TaskCreationOptions.LongRunning); // 管理PLC连接状态
                System.Windows.Forms.Timer plcStatusTimer = new System.Windows.Forms.Timer();
                plcStatusTimer.Interval = 500;
                plcStatusTimer.Tick += UiUpdateTimer_Tick;
                plcStatusTimer.Start();

                InitializeModelReadAsync();                 // 读取生产指标
                Process_Offline();                          // 根据状态写模式
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                this.InvokeAsync(() =>
                {
                    // 初始状态为待机
                    if (chkDoubleStation.Checked)
                    {
                        lbl_Left.ForeColor = Color.Black;
                        lbl_Left.BackColor = Color.White;
                        lbl_Left.Text = resources.GetString("Standby");
                        FontAutoResizer.ChangeLabelFont(lbl_Left, 22F, FontStyle.Bold);

                        lbl_Right.ForeColor = Color.Black;
                        lbl_Right.BackColor = Color.White;
                        lbl_Right.Text = resources.GetString("Standby");
                        FontAutoResizer.ChangeLabelFont(lbl_Right, 22F, FontStyle.Bold);
                    }
                    else
                    {
                        lblProductResult.ForeColor = Color.Black;
                        lblProductResult.BackColor = Color.White;
                        lblProductResult.Text = resources.GetString("Standby");
                    }
                });

                if (!int.TryParse(txtStationCount.Text, out int stationCount))
                {
                    stationCount = 1;
                }

                if (stationNameSets.Length == stationCount)
                {
                    GetInLaguageArray();

                    this.InvokeAsync(() =>
                    {
                        if (chkDoubleStation.Checked)
                        {
                            tabControl_UploadData.SizeMode = TabSizeMode.Normal;
                            tabControl_UploadData.ItemSize = new Size(100, 30);

                            tabPage3.Parent = null;
                            tabPage1.Text = StationNameArray[0];
                            tabPage2.Text = StationNameArray[1];
                            splitContainer3.Panel1Collapsed = false;

                            CreateHeaderText(dgvResult1, "1", true);
                            CreateHeaderText(dgvResult2, "2", true);
                        }
                        else if (chkLeftRight.Checked)
                        {
                            tabControl_UploadData.SizeMode = TabSizeMode.Normal;
                            tabControl_UploadData.ItemSize = new Size(100, 30);

                            tabPage1.Text = StationNameArray[0] + "&" + StationNameArray[1];
                            tabPage2.Text = StationNameArray[0];
                            tabPage3.Text = StationNameArray[1];
                            splitContainer3.Panel1Collapsed = true;

                            CreateHeaderText(dgvResult1);
                            CreateHeaderText(dgvResult2, "1", true);
                            CreateHeaderText(dgvResult3, "2", true);
                        }
                        else
                        {
                            tabControl_UploadData.SizeMode = TabSizeMode.Fixed;
                            tabControl_UploadData.ItemSize = new Size(0, 1);
                            tabPage2.Parent = null;
                            tabPage3.Parent = null;
                            splitContainer3.Panel1Collapsed = true;

                            CreateHeaderText(dgvResult1);
                        }
                    });
                }

                rtbProductLog.Clear();
                UTYPE.SelectedIndex = 0;

                nameList = new List<string>();
                if (targetStationNum.Length > 0)
                {
                    nameList = CodeNum.GetStationNameListByID(targetStationNum, stationNameSets);
                }

                // 为 ADM 权限增加定时器
                if (Access == 3 && chkAutoExit.Checked)
                {
                    timer = new System.Timers.Timer();
                    timer.Elapsed += Timer_Elapsed;
                    timer.Enabled = true;
                    timer.Interval = 1800000;
                    timer.Start();
                }

                // 联机用户验证失败,直接返回
                if (isOffLine == 0 && isMesLoginSuccessful == false)
                {
                    return;
                }
                if (isOffLine == 1)
                {
                    txtWorkOrder.Text = "111111111111";
                }
                else
                {
                    txtWorkOrder.Text = Interaction.InputBox(resources.GetString("InputBox"), resources.GetString("InputBoxName"), "", 100, 100);

                    // 超过3次自动退出
                    for (int i = 1; i <= 5; i++)
                    {
                        if (string.IsNullOrWhiteSpace(txtWorkOrder.Text))
                        {
                            if (i == 4)
                            {
                                Form1_FormClosed(null, null);
                            }
                            txtWorkOrder.Text = Interaction.InputBox(resources.GetString("InputBox") + i + resources.GetString("InputBox1"), resources.GetString("InputBoxName"), "", 100, 100);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            barcodeData = "0";
            barcodeData1 = "0";
            barcodeData2 = "0";
            barcodeInfo = "1";
            barcodeInfo1 = "1";
            barcodeInfo2 = "1";
            isAllowSwitchWorkOrder = true;

            try
            {
                _cts1 = new CancellationTokenSource();
                Task.Factory.StartNew(() => ProcessPlc_ReadBarcodeAsync(_cts1.Token), TaskCreationOptions.LongRunning);
                Task.Factory.StartNew(() => ProcessPlc_ReadData(), TaskCreationOptions.LongRunning);
                Task.Factory.StartNew(() => Get_MaxMinValue(), TaskCreationOptions.LongRunning);
                Task.Factory.StartNew(() => UpdateTestResultRealtime(), TaskCreationOptions.LongRunning);
                Task.Factory.StartNew(() => ProcessPlc_ShowBarcode());

                Task.Factory.StartNew(() => Process_MES(), TaskCreationOptions.LongRunning);        // 向PLC反馈看板连接状态
                //Task.Factory.StartNew(() => UpdatePlcStatus(), TaskCreationOptions.LongRunning);    // 更新PLC连接状态指示灯
                //Task.Factory.StartNew(() => DetectPlcHeartbeat(), TaskCreationOptions.LongRunning); // 检测PLC心跳

                ConnectReader();                                // 连接读卡器

                if (chkPlcControlPrint.Checked)
                {
                    Task.Run(() => PlcControlPrint()); // PLC控制打印
                }

                if (recipeInfoBindingList.Count > 0 && cboBarcodeRule.Text.Trim() != "" && cboBarcodeRule.Text.Trim() != "System.Data.DataRowView")
                {
                    RecipeEntity findCodes = recipeInfoBindingList.FirstOrDefault(find => cboBarcodeRule.Text == find.BarcodeRule);
                    txtProductCode.Text = findCodes.ProductCode;
                    productModel = txtProductModel.Text = findCodes.ProductName;
                    txtFixtureBinding.Text = findCodes.FixtureNumber;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UiUpdateTimer_Tick(object sender, EventArgs e)
        {
            // 更新 PLC 状态
            if (isPlcConnected)
                lblPlcStatus.ForeColor = Color.Green;
            else
                lblPlcStatus.ForeColor = Color.Red;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnApplicationExit();

            _cts?.Cancel();
            _cts?.Dispose();

            _cts1?.Cancel();
            _cts1?.Dispose();

            CloseSocketSafely();    // 窗口关闭时关闭Socket连接
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭标签应用，并且不保存
            if (btApp != null)
            {
                btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
            }
            try
            {
                Form1_FormClosing(sender, null);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                // 记录异常信息
                MessageBox.Show("An error occurred while closing the application: " + ex.Message);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 保持比例不变
            splitContainer_LR.SetPercent(0.73);
        }

        #region ------------ 杂项 ------------

        private void InitializeTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer_Tick;
            timer1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 达到时间间隔发生的方法
        /// </summary>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            iOperCount++;
            if (iOperCount >= 1)
            {
                Console.WriteLine("30分钟未动作程序退出！");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 根据用户权限分配界面
        /// </summary>
        private void AssignUI()
        {
            if (Access_take == 1)
            {
                if (Access == 1)//操作员
                {
                    this.tabPage生产日志.Parent = this.tabControl1;
                    this.tabPage用户管理.Parent = this.tabControl1;
                    this.tabPage打印设置.Parent = null;
                    this.tabPageMES参数.Parent = null;
                    this.tabPage看板设置.Parent = null;
                    this.tabPage系统设置.Parent = null;
                    this.tabPage历史数据.Parent = this.tabControl1;
                    this.tabPage配方设置.Parent = null;
                }
                else if (Access == 2)//工艺工程师
                {
                    this.tabPage生产日志.Parent = this.tabControl1;
                    this.tabPage用户管理.Parent = this.tabControl1;
                    this.tabPage打印设置.Parent = this.tabControl1;
                    this.tabPageMES参数.Parent = this.tabControl1;
                    this.tabPage看板设置.Parent = this.tabControl1;
                    this.tabPage系统设置.Parent = null;
                    this.tabPage历史数据.Parent = this.tabControl1;
                    this.tabPage配方设置.Parent = this.tabControl1;
                }
                else if (Access == 3)//超级用户
                {
                    this.tabPage生产日志.Parent = this.tabControl1;    // 生产日志
                    this.tabPage用户管理.Parent = this.tabControl1;    // 用户管理
                    this.tabPage打印设置.Parent = this.tabControl1;    // 打印设置
                    this.tabPageMES参数.Parent = this.tabControl1;    // MES参数
                    this.tabPage看板设置.Parent = this.tabControl1;    // 看板设置
                    this.tabPage系统设置.Parent = this.tabControl1;    // 系统设置
                    this.tabPage历史数据.Parent = this.tabControl1;    // 历史数据
                    this.tabPage配方设置.Parent = this.tabControl1;    // 配方设置
                }
                else if (Access == 4)//开发者
                {
                    this.tabPage生产日志.Parent = this.tabControl1;
                    this.tabPage用户管理.Parent = this.tabControl1;
                    this.tabPage打印设置.Parent = this.tabControl1;
                    this.tabPageMES参数.Parent = this.tabControl1;
                    this.tabPage看板设置.Parent = this.tabControl1;
                    this.tabPage系统设置.Parent = this.tabControl1;
                    this.tabPage历史数据.Parent = this.tabControl1;
                    this.tabPage配方设置.Parent = this.tabControl1;
                }
                else if (Access == 5)//品质
                {
                    this.tabPage生产日志.Parent = this.tabControl1;
                    this.tabPage用户管理.Parent = this.tabControl1;
                    this.tabPage打印设置.Parent = null;
                    this.tabPageMES参数.Parent = null;
                    this.tabPage看板设置.Parent = null;
                    this.tabPage系统设置.Parent = null;
                    this.tabPage历史数据.Parent = this.tabControl1;
                    this.tabPage配方设置.Parent = null;
                }
                else if (Access == 6)//设备
                {
                    this.tabPage生产日志.Parent = this.tabControl1;
                    this.tabPage用户管理.Parent = this.tabControl1;
                    this.tabPage打印设置.Parent = this.tabControl1;
                    this.tabPageMES参数.Parent = null;
                    this.tabPage看板设置.Parent = null;
                    this.tabPage系统设置.Parent = this.tabControl1;
                    this.tabPage历史数据.Parent = this.tabControl1;
                    this.tabPage配方设置.Parent = null;
                }
                Access_take = 0;
            }
        }

        /// <summary>
        /// 递归地初始化所有 CheckBox 的状态，包括嵌套在其他容器中的 CheckBox
        /// </summary>
        /// <param name="controls"></param>
        private void InitializeCheckBoxStates(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // 如果是CheckBox，记录其状态并绑定CheckedChanged事件
                if (control is System.Windows.Forms.CheckBox cbx)
                {
                    if (!checkBoxStates.ContainsKey(cbx))
                    {
                        checkBoxStates[cbx] = cbx.Checked;
                        chkBindOrderNumber.CheckedChanged += CheckBox_CheckedChanged;           // 勾选绑定工单
                        chkReadPName.CheckedChanged += CheckBox_CheckedChanged;                 // 读取PLC
                        chkEnableDashboard.CheckedChanged += CheckBox_CheckedChanged;           // 启用看板
                        chkPlcControlPrint.CheckedChanged += CheckBox_CheckedChanged;           // PLC控制打印
                        chkBanRuleValidation.CheckedChanged += CheckBox_CheckedChanged;         // 屏蔽本地条码验证
                        chkReadBarcodeSecondly.CheckedChanged += CheckBox_CheckedChanged;       // 二次读条码
                        chkBypassFixtureValidation.CheckedChanged += CheckBox_CheckedChanged;   // 屏蔽本地扫工装验证
                        chkBanQRcodeValidation.CheckedChanged += CheckBox_CheckedChanged;       // 屏蔽本地二维码验证
                        chkBanNGDataVerify.CheckedChanged += CheckBox_CheckedChanged;           // 屏蔽本地NG历史数据
                        chkBanLocalHistoricalData.CheckedChanged += CheckBox_CheckedChanged;    // 屏蔽本地历史数据
                    }
                }
                // 如果是容器控件，递归调用此方法
                else if (control is ContainerControl || control is Panel || control is System.Windows.Forms.GroupBox || control is TabPage || control is TabControl)
                {
                    InitializeCheckBoxStates(control.Controls);
                }
            }
        }

        /// <summary>
        /// CheckedChanged 事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.CheckBox cbx)
            {
                // 检查字典中是否存在对应的键
                if (checkBoxStates.TryGetValue(cbx, out bool initialState))
                {
                    // 当状态发生变化且与初始状态不同
                    if (cbx.Checked != initialState)
                    {
                        // 记录日志
                        string state = cbx.Checked ? "启用" : "已关闭";
                        loggerConfig.Trace($"【{cbx.Text}】{state}");

                        // 更新字典中的状态
                        checkBoxStates[cbx] = cbx.Checked;
                    }
                }
                else
                {
                    loggerConfig.Warn($"未能在字典中找到 CheckBox '{cbx.Name}' 的初始状态。");
                }
            }
        }

        /// <summary>
        /// 向 PLC 反馈看板连接状态
        /// </summary>
        private async Task Process_MES()
        {
            while (true)
            {
                try
                {
                    // 向 PLC 反馈看板连接状态，这部分暂时未使用。
                    if (string.IsNullOrEmpty(deviceInfo.DashboardStatusPoint))
                        return;

                    if (isDashboardConnected)
                    {
                        await readWriteNet.WriteAsync(deviceInfo.DashboardStatusPoint, 1);
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(deviceInfo.DashboardStatusPoint, 0);
                    }
                }
                catch (Exception)
                {
                    DisplayMessage($"反馈{deviceInfo.DashboardStatusPoint}看板状态失败");
                }

                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// 更新登录模式，用户登录信息
        /// </summary>
        private void Process_Offline()
        {
            if (isOffLine == 1)
            {

                lblLoginMode.Text = $"{resources.GetString("loginMode1")}";  // 离线
                lblCurrentUser.Text = $"{LoginUser} ({LoginName})";

            }
            else if (isOffLine == 0)
            {
                lblLoginMode.Text = $"{resources.GetString("loginMode")}";   // 在线
                lblCurrentUser.Text = $"{LoginUser} ({LoginName})";
            }
        }

        private async Task DisplayMessage(string msg)
        {
            this.InvokeAsync(() =>
            {
                if (rtbProductLog.TextLength > 50000)
                {
                    rtbProductLog.Clear();
                }

                rtbProductLog.AppendText($"{DateTime.Now:HH:mm:ss_fff}：{msg}{Environment.NewLine}");
                rtbProductLog.ScrollToCaret();
            });

            logProduction.Trace(msg);
        }

        #endregion

        Task taskReadBar = null;
        Task taskReadMaxMin = null;
        Task taskBindValue = null;
        Task taskReadData = null;
        Task taskUpdateStatus = null;
        Task taskProcess_ZPL = null;

        private bool isReadBarcode_PLC = true;  // 开始读条码
        private bool isReadData_PLC = true;     // 开始读取生产数据
        private bool isReadMaxMin_PLC = true;   // 读取、绑定上下限
        private bool isPrinted_PLC = true;      // PLC控制打印
        private List<BarcodeVerification> bvList;

        /// <summary>
        /// 用于防止多个工位同时执行数据上传逻辑（ProcessProductionData）
        /// </summary>
        private static readonly SemaphoreSlim _uploadSemaphore = new SemaphoreSlim(1, 1);
        /// <summary>
        /// 数据保存完成标志
        /// </summary>
        private bool isSaveDataSuccessfully = true;
        /// <summary>
        /// // 条码验证完成标志，自动生成条码时使用
        /// </summary>
        private bool IsVerifyOK = false;
        /// <summary>
        /// 实时读取或自动生成的条码，用于本地验证、MES验证
        /// </summary>
        private string barcodeData = string.Empty;
        private string barcodeData1 = string.Empty;
        private string barcodeData2 = string.Empty;
        /// <summary>
        /// 存储二次读取过后的条码，同时用于测试结果上传、上传看板、保存本地、测试结果UI显示
        /// </summary>
        private string barcodeInfo = null;
        private string barcodeInfo1 = null;
        private string barcodeInfo2 = null;

        public static string path4 = System.AppDomain.CurrentDomain.BaseDirectory + "SystemDateBase.mdb";
        public static string pathText = System.AppDomain.CurrentDomain.BaseDirectory + "logfault.txt";
        public static string pathText1 = System.AppDomain.CurrentDomain.BaseDirectory + "barcode1.txt";
        public static string pathText2 = System.AppDomain.CurrentDomain.BaseDirectory + "barcode2.txt";
        public static string userFilePath = "D:\\BYD_Users\\Users_Data.MDB";

        #region ------------ 条码读取与验证 ------------

        private async Task ProcessPlc_ReadBarcodeAsync(CancellationToken cancellationToken)
        {
            while (isReadBarcode_PLC)
            {
                try
                {
                    await SelectStationMode();
                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                    DisplayMessage($"条码验证发生错误: {ex.Message}");
                }
            }
        }

        private async Task SelectStationMode()
        {
            if (!isPlcConnected) return;

            txtBarcodeNumber.Enabled = true;
            txtSN.Enabled = true;

            BarcodeVerification bv = null;
            for (int i = 0; i < bvList.Count; i++)
            {
                if (!bvList[i].IsEnableBarcodeVerify) continue;

                // 双工位：
                if (chkDoubleStation.Checked)
                {
                    OperateResult<short> result = await readWriteNet.ReadInt16Async(bvList[i].BarcodeStartPLC);
                    if (result.IsSuccess && result.Content == 1)
                    {
                        bv = bvList[i];
                        break;
                    }
                }
                // 单工位 & 左右款
                else
                {
                    OperateResult<int> result = await readWriteNet.ReadInt32Async(bvList[i].BarcodeStartPLC);
                    if (!result.IsSuccess) return;

                    int triggerValue = result.Content;
                    if (triggerValue == 1)
                    {
                        bv = bvList[i];
                        break;
                    }
                }
            }

            if (bv == null) return;

            await ReadBarcodeAsync(bv);
        }

        /// <summary>
        /// 读条码/生成条码 -> 条码验证
        /// </summary>
        /// <remarks>
        /// <para>
        /// 条码验证：条码有效性验 -> 本地条码验证 -> 二维码验证 -> MES条码验证
        /// </para>
        /// <para>
        /// 本地条码验证：工装验证 -> 条码重复性验证 -> 条码规则验证
        /// </para>
        /// <para>
        /// 条码规则验证：条码规则有效性验证 -> 条码规范性验证 -> 条码规则匹配
        /// </para>
        /// </remarks>
        private async Task ReadBarcodeAsync(BarcodeVerification bv)
        {
            // 工装前置验证
            if (!await CheckFixtureValidation(bv)) return;

            #region 进行条码验证和二维码验证

            // 初始化状态指示灯
            await this.InvokeAsync(() =>
            {
                lblScanBarcodeStatus.ForeColor = Color.Black; // 扫码状态指示灯
                lblValidationStatus.ForeColor = Color.Black;  // 验证状态指示灯
                lblUploadStatus.ForeColor = Color.Black;      // 上传状态指示灯

                if (chkDoubleStation.Checked)
                {
                    lbl_Left.ForeColor = Color.Black;
                    lbl_Left.BackColor = Color.White;
                    lbl_Left.Text = resources.GetString("Standby");
                    FontAutoResizer.ChangeLabelFont(lbl_Left, 22F, FontStyle.Bold);

                    lbl_Right.ForeColor = Color.Black;
                    lbl_Right.BackColor = Color.White;
                    lbl_Right.Text = resources.GetString("Standby");
                    FontAutoResizer.ChangeLabelFont(lbl_Right, 22F, FontStyle.Bold);
                }
                else
                {
                    lblProductResult.ForeColor = Color.Black;
                    lblProductResult.BackColor = Color.White;
                    lblProductResult.Text = resources.GetString("Standby");
                }
            });

            // 自动生成条码的情况下, 确保数据保存成功才能进入条码验证流程
            if (chkGenerateBarcode.Checked && !isSaveDataSuccessfully)
            {
                DisplayMessage("数据保存尚未完成，退出条码验证流程");
                await readWriteNet.WriteAsync(bv.PassBarcodeEndPLC, 1); // 反馈(条码验证结束PLC) OK
                return;
            }

            // 是否自动生成条码
            DoesGenerateBarcodeAutomatically(bv);

            string barcodeRule = string.Empty;
            await this.InvokeAsync(() => { barcodeRule = cboBarcodeRule.Text; });

            if (bv.IsEnableBarcodeVerify)
            {
                // 条码有效性验证:判断是否为空引用、空字符或仅空格
                if (string.IsNullOrWhiteSpace(barcodeData))
                {
                    await this.InvokeAsync(() =>
                    {
                        lblScanBarcodeStatus.ForeColor = Color.Red;
                        txtShowBarcode.ForeColor = Color.Red;
                        txtShowBarcode.Text = bv.BarcodeIsNullOrWhiteSpace; // 未读到条码
                    });

                    try
                    {
                        if (chkDoubleStation.Checked)
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                        }
                        else
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                        }
                        DisplayMessage($"条码有效性验证：条码为空引用、空字符或仅空白，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"条码有效性验证：条码为空引用、空字符或仅空白，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }

                    return;
                }

                // 本地条码验证:重复性验证 -> 条码规则验证
                if (!chkBanLocalVerification.Checked)
                {
                    // 条码重复性验证和工位依赖检查
                    if (!await CheckBarcodeRepeatability(bv, barcodeData))
                    {
                        return;
                    }

                    // 条码规则验证
                    if (!chkBanRuleValidation.Checked)
                    {
                        // 条码规则验证：若实际获取的条码前缀与设定的条码验证规则的长度和字符都相同 => 通过验证
                        bool isVerifySuccessfully = await VerifyBarcodeRules(bv, barcodeRule);
                        if (!isVerifySuccessfully) return;
                    }
                }
            }

            // 二维码验证：当前未启用，IsEnableQRcodeVerify = false;
            if (bv.IsEnableQRcodeVerify)
            {
                // 验证二维码前缀
                if (!chkBanQRcodeValidation.Checked)
                {
                    string verificationRule = CodeNum.GetQRCodeVerification(barcodeRule, RecipeTable);

                    if (!await VerifyBarcodeRules(bv, verificationRule))
                    {
                        return;
                    }
                }
            }

            // MES条码验证
            if (isOffLine == 1)
            {
                this.InvokeAsync(() =>
                {
                    lblScanBarcodeStatus.ForeColor = Color.Green;
                    lblValidationStatus.ForeColor = Color.Green;
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("RSBarcode_OK");      // 条码验证通过
                    lblOperatePrompt.ForeColor = Color.Black;
                    lblOperatePrompt.Text = resources.GetString("OT_StartProduce");     // 请开始生产
                });

                try
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.PassBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.PassBarcodeEndPLC, 1);
                    }

                    DisplayMessage($"MES条码验证：单机模式条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"MES条码验证：单机模式条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)失败：{ex}");
                }
            }
            else
            {
                // 绑定工单
                if (chkBindOrderNumber.Checked)
                {
                    orderNumber = txtWorkOrder.Text;
                    var result = await MesIntegrationService.BindWorkOrderAsync(orderNumber);
                    var orderInfo = await MesIntegrationService.FetchOrderNumberInfo(orderNumber);
                    orderQuantity = orderInfo.orderQuantity;
                    completedQuantity = orderInfo.completedQuantity;
                    completeRate = orderInfo.completeRate;
                }

                // 上传MES进行条码验证
                await VerifyBarcode_MES(barcodeData);

                // 更新验证结果并反馈给PLC
                if (MES_Result[2002] == "1")
                {
                    this.InvokeAsync(() =>
                    {
                        lblScanBarcodeStatus.ForeColor = Color.Green;
                        lblValidationStatus.ForeColor = Color.Green;
                        lblRunningStatus.ForeColor = Color.Green;
                        lblRunningStatus.Text = resources.GetString("RSBarcode_OK");      // 条码验证通过
                        lblOperatePrompt.ForeColor = Color.Black;
                        lblOperatePrompt.Text = resources.GetString("OT_StartProduce");     // 请开始生产
                    });

                    try
                    {
                        // 双工位
                        if (chkDoubleStation.Checked)
                        {
                            await readWriteNet.WriteAsync(bv.PassBarcodeEndPLC, Convert.ToInt16(1));
                        }
                        // 左右款 & 单工位
                        else
                        {
                            await readWriteNet.WriteAsync(bv.PassBarcodeEndPLC, 1);
                        }
                        DisplayMessage($"MES条码验证：MES条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"MES条码验证：MES条码验证通过，反馈(条码验证{bv.PassBarcodeEndPLC} = 1)失败：{ex}");
                    }

                }
                else if (MES_Result[2004] == "1")
                {
                    this.InvokeAsync(() =>
                    {
                        lblScanBarcodeStatus.ForeColor = Color.Green;
                        lblValidationStatus.ForeColor = Color.Red;
                        lblRunningStatus.ForeColor = Color.Red;
                        lblRunningStatus.Text = resources.GetString("RSBarcode_NG");
                        lblOperatePrompt.ForeColor = Color.Black;
                        lblOperatePrompt.Text = resources.GetString("OT_Scaned");   // 扫码完成
                        if (chkGenerateBarcode.Checked)
                        {
                            isSaveDataSuccessfully = true; //重新生成条码
                        }
                    });

                    try
                    {
                        if (chkDoubleStation.Checked)
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));

                        }
                        else
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                        }
                        DisplayMessage($"MES条码验证：MES条码验证失败，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"MES条码验证：MES条码验证失败，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }
                }
            }

            DisplayMessage($"条码验证完成{Environment.NewLine}");
            IsVerifyOK = true;  // 确保条码验证完成后才能进行数据上传

            this.InvokeAsync(() =>
            {
                if (chkBanLocalVerification.Checked)
                {
                    chkBanLocalVerification.Checked = false;
                }
                if (chkBanRuleValidation.Checked)
                {
                    chkBanRuleValidation.Checked = false;
                }
            });

            #endregion

            #region 物料验证，PLC置3

            //var Read_data = KeyenceMcNet.ReadInt32(bvList[0].BarcodeStartPLC).Content;
            //if (Read_data == 3)
            //{
            //    Invoke(new Action(() =>
            //    {
            //        bv = bvList[0];
            //        ushort barcodeLength = bv.GetBarcodeLength();
            //        string rawBarcode = KeyenceMcNet.ReadString(bv.BarcodePositionPLC, barcodeLength).Content;
            //        barcodeData = CodeNum.CleanString(rawBarcode);
            //        txtShowBarcode.Text = barcodeData;
            //        //DisplayMessage($"条码【D1100】 = {barcodeData}");

            //        if (string.IsNullOrEmpty(this.barcodeData))
            //        {
            //            lblScanBarcodeStatus.ForeColor = Color.Red;
            //            txtShowBarcode.Text = resources.GetString("barCode_State");
            //            //DisplayMessage("未获取到条码，请重新扫描！");
            //            try
            //            {
            //                //KeyenceMcNet.Write("D1005", 1);
            //                KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 1);
            //                //DisplayMessage("反馈条码验证【D1005】 = 1");
            //            }
            //            catch (Exception ex)
            //            {
            //                //DisplayMessage(ex.ToString());
            //            }
            //            return;
            //        }
            //        string[] frockA = txtProductCode.Text.ToString().Split('+');
            //        if (frockA.Contains(this.barcodeData))
            //        {
            //            lblScanBarcodeStatus.ForeColor = Color.Green;
            //            lblRunningStatus.ForeColor = Color.Green;
            //            lblRunningStatus.Text = resources.GetString("material_OK");
            //            try
            //            {

            //                //KeyenceMcNet.Write("D1003", 3);
            //                KeyenceMcNet.Write(bv.PassBarcodeEndPLC, 3);
            //            }
            //            catch (Exception ex)
            //            {
            //                //DisplayMessage(ex.ToString());
            //            }
            //            //DisplayMessage("反馈条码验证【D1003】 =3");
            //            return;
            //        }
            //        else
            //        {
            //            lblRunningStatus.ForeColor = Color.Red;
            //            lblRunningStatus.Text = resources.GetString("material_NG");
            //            lblValidationStatus.ForeColor = Color.Red;
            //            //DisplayMessage("物料验证失败，请重新扫描！");
            //            try
            //            {
            //                //KeyenceMcNet.Write("D1005", 3);
            //                KeyenceMcNet.Write(bv.ErrorBarcodeEndPLC, 3);
            //                //DisplayMessage("反馈条码验证【D1005】 =3");
            //            }
            //            catch (Exception ex)
            //            {
            //                //DisplayMessage(ex.ToString());
            //            }
            //            return;
            //        }
            //    }));
            //}

            #endregion
        }

        /// <summary>
        /// 判断是否需要自动生成条码，否则实时读取条码
        /// </summary>
        /// <param name="bv"></param>
        private async Task DoesGenerateBarcodeAutomatically(BarcodeVerification bv)
        {
            if (chkGenerateBarcode.Checked)
            {
                // 生成条码
                barcodeData = AutoGenerateBarcode(txtBarcodeNumber.Text);
                DisplayMessage($"【条码验证流程开始】");
                DisplayMessage($" 生成的条码为：{barcodeData} ");

                // 更新UI及相关控件可用性
                this.InvokeAsync(() =>
                {
                    txtShowBarcode.ForeColor = Color.Black;
                    txtShowBarcode.Text = lblBarcodeContent.Text = barcodeData;
                    txtBarcodeNumber.Enabled = false;
                    txtSN.Enabled = false;
                });

                // 更新字段
                barcodeInfo = barcodeData;
                IsVerifyOK = false;
                isSaveDataSuccessfully = false;
            }
            else
            {
                // 读取条码 (D1100)
                ushort barcodeLength = bv.GetBarcodeLength();
                var result = readWriteNet.ReadString(bv.BarcodePositionPLC, barcodeLength);
                if (!result.IsSuccess)
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                    }
                    DisplayMessage($"读取条码失败：{result.Message}，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    return;
                }

                string rawBarcode = result.Content;
                barcodeData = CodeNum.CleanString(rawBarcode);

                if (chkDoubleStation.Checked)
                {
                    if (bv.ID == 1)
                    {
                        barcodeData1 = barcodeData;
                    }
                    else if (bv.ID == 2)
                    {
                        barcodeData2 = barcodeData;
                    }
                }

                DisplayMessage($"【条码验证流程开始】");
                DisplayMessage($" 读取的条码为：{bv.BarcodePositionPLC} = {barcodeData} ");

                // 更新UI
                this.InvokeAsync(() =>
                {
                    txtShowBarcode.ForeColor = Color.Black;
                    txtShowBarcode.Text = barcodeData;
                });

                // 更新字段
                barcodeInfo = barcodeData;
                IsVerifyOK = false;
            }
        }

        /// <summary>
        /// 自动生成条码内容
        /// </summary>
        /// <param name="codeNumber">10位字符串码号</param>
        /// <returns>18位的条码字符串</returns>
        private string AutoGenerateBarcode(string codeNumber)
        {
            if (ShouldResetSerialNumber())
            {
                ResetSerialNumber();
                SaveLastSavedDate();
            }
            else
            {
                SaveLastSavedDate();
            }

            // 获取当前流水号
            if (!int.TryParse(txtSN.Text, out int serialNumber))
            {
                serialNumber = 0;
            }
            // 计算新的流水号
            int newSerialNumber = serialNumber + 1;

            // 更新流水号并保存
            string formattedSerialNumber = newSerialNumber.ToString("D5");
            ssd.SerialNumber = formattedSerialNumber;
            txtSN.Text = formattedSerialNumber;
            ssd.Save();

            string month = DateTime.Now.Month.ToString("D2");
            string day = DateTime.Now.Day.ToString("D2");

            /*if (month == "10" || month == "11" || month == "12")
            {
                switch (month)
                {
                    case "10": month = "0"; break;
                    case "11": month = "A"; break;
                    case "12": month = "B"; break;
                }
            }*/

            codeNumber = codeNumber += month + day;
            string barcodeInfo = codeNumber + txtSN.Text;
            return barcodeInfo;
        }

        /// <summary>
        /// 条码规则验证（条码规则有效性验证 -> 条码规范性验证 -> 条码规则匹配）
        /// </summary>
        /// <param name="bv"></param>
        /// <param name="barcodeRule">条码验证规则</param>
        /// <returns>验证成功返回True，否则为False</returns>
        private async Task<bool> VerifyBarcodeRules(BarcodeVerification bv, string barcodeRule)
        {
            // 条码规则有效性验证：判断是否为空引用、空字符或空白
            if (barcodeRule == null)
            {
                await this.InvokeAsync(() =>
                {
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSBarcode_NoRule");  // 无条码验证规则
                });

                try
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                    }
                    DisplayMessage($"条码规则有效性验证：未选择条码验证规则，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"条码规则有效性验证：未选择条码验证规则，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                }

                return false;
            }

            // 从实际读取到的条码中提取条码前缀
            int ruleLength = barcodeRule.Length;
            string actualBarcodePrefix = barcodeData.Substring(0, ruleLength);
            string[] validationRule = barcodeRule.Split('|');

            // 条码规范性验证：判断条码内容长度是否有误
            if (barcodeData.Length <= 12 || barcodeData.Length <= ruleLength)
            {
                await this.InvokeAsync(() =>
                {
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSBarcode_Fault");  // 条码内容不规范
                });

                try
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                    }
                    DisplayMessage($"条码规范性验证：条码内容不符合规范，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"条码规范性验证：条码内容不符合规范，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                }

                return false;
            }

            // 条码规则验证：判断实际条码与设定的条码规则是否匹配
            if (validationRule.Contains(actualBarcodePrefix))
            {
                return true;
            }
            else
            {
                await this.InvokeAsync(() =>
                {
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSBarcode_Dismach");    // 条码规则不匹配
                });

                try
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                    }
                    DisplayMessage($"条码规则验证：实际条码与设定的条码规则不匹配，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"条码规则验证：实际条码与设定的条码规则不匹配，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                }

                return false;
            }
        }

        /// <summary>
        /// 工位依赖检查：确保工位1测试通过后才能进入工位2
        /// </summary>
        /// <param name="bv">条码验证对象</param>
        /// <param name="currentBarcode">当前条码</param>
        /// <returns>检查是否通过</returns>
        private async Task<bool> CheckStationDependency(BarcodeVerification bv, string currentBarcode)
        {
            // 只有双工位模式且当前为工位2时才需要检查
            if (!chkDoubleStation.Checked || bv.ID != 2)
            {
                return true;
            }

            bool station1HasDataInDatabase = false;
            bool station1HasDataInMemory = false;
            bool station1ResultOK = false;

            // 方法1：检查本地数据库中工位1的测试结果
            string dataPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
            string selectSql = $"SELECT * FROM [Sheet1] WHERE 条码 = '{currentBarcode}'";
            try
            {
                dbHelper = new MDBHelper(dataPath);
                DataTable station1Data = dbHelper.Find(selectSql);

                if (station1Data.Rows.Count > 0)
                {
                    station1HasDataInDatabase = true;
                    // 检查测试结果是否为OK
                    string testResult = station1Data.Rows[0]["测试结果"]?.ToString();
                    station1ResultOK = testResult == "OK";

                    DisplayMessage($"工位依赖检查：在数据库中找到工位1数据，测试结果：{testResult}");
                }
                else
                {
                    DisplayMessage($"工位依赖检查：在数据库中未找到工位1数据");
                }

                dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                DisplayMessage($"工位依赖检查：查询数据库失败：{ex.Message}");
            }

            // 方法2：检查内存中的stationResultList1数据（作为补充）
            if (!station1HasDataInMemory)
            {
                var station1MemoryData = stationResultList1.FirstOrDefault(x => x.Barcode == currentBarcode);
                if (station1MemoryData != null)
                {
                    station1HasDataInMemory = true;
                    station1ResultOK = station1MemoryData.Result == "OK";
                    DisplayMessage($"工位依赖检查：在内存中找到工位1数据，测试结果：{station1MemoryData.Result}");
                }
                else
                {
                    DisplayMessage($"工位依赖检查：内存列表为空");
                }
            }

            // 检查结果处理
            if (!station1HasDataInDatabase && !station1HasDataInMemory)
            {
                await this.InvokeAsync(() =>
                {
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSStation_NoData");    // 工位1无此条码数据
                    lblOperatePrompt.ForeColor = Color.Red;
                    lblOperatePrompt.Text = resources.GetString("RSStation_TobeCompleted"); // 请先在工位1完成测试
                });

                try
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                    }
                    DisplayMessage($"工位依赖检查：工位1无此条码数据，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"工位依赖检查：工位1无此条码数据，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                }

                return false;
            }

            if (!station1ResultOK)
            {
                await this.InvokeAsync(() =>
                {
                    lblValidationStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSStation_Station1NG");    // 工位1测试结果为NG
                });

                try
                {
                    if (chkDoubleStation.Checked)
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                    }
                    else
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                    }
                    DisplayMessage($"工位依赖检查：工位1测试结果为NG，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                }
                catch (Exception ex)
                {
                    DisplayMessage($"工位依赖检查：工位1测试结果为NG，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                }

                return false;
            }

            // 检查通过
            DisplayMessage($"工位依赖检查：工位1测试结果为OK，允许进入工位2");
            return true;
        }

        /// <summary>
        /// 条码重复性验证
        /// 包含双工位顺序验证
        /// </summary>
        private async Task<bool> CheckBarcodeRepeatability_Old(BarcodeVerification bv, string currentBarcode)
        {
            if (chkBanLocalHistoricalData.Checked)
            {
                return true; // 如果屏蔽本地历史数据验证，直接通过
            }

            string dataPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
            string selectSql = $"SELECT * FROM [Sheet1] WHERE 条码 = '{currentBarcode}'";

            // 屏蔽本地NG数据验证：仅保留OK数据参与验证（即NG产品可以通过测试，OK产品会被判定为重复条码）
            if (chkBanNGDataVerify.Checked)
            {
                selectSql += " AND 测试结果 = 'OK'";
            }

            dbHelper = new MDBHelper(dataPath);
            bool isDataExist = dbHelper.DoesDataExist(selectSql);

            // 双工位模式下的特殊处理
            if (chkDoubleStation.Checked)
            {
                if (bv.ID == 2)
                {
                    // 工位2：检查工位依赖

                    if (!await CheckStationDependency(bv, currentBarcode))
                    {
                        return false;
                    }

                    // 检查工位2是否已有数据（避免重复测试）
                    string station2SelectSql = selectSql;
                    // 可以添加额外的条件来区分工位2的数据，比如通过特定字段或表结构

                    bool station2HasData = dbHelper.DoesDataExist(station2SelectSql);
                    if (station2HasData)
                    {
                        await this.InvokeAsync(() =>
                        {
                            lblValidationStatus.ForeColor = Color.Red;
                            lblRunningStatus.ForeColor = Color.Red;
                            lblRunningStatus.Text = resources.GetString("RSBarcode_Repeat");
                        });

                        try
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                            DisplayMessage($"条码重复性验证：工位2条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage($"条码重复性验证：工位2条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                        }

                        return false;
                    }
                }
                else if (bv.ID == 1)
                {
                    // 工位1：正常的重复性检查
                    if (isDataExist)
                    {
                        await this.InvokeAsync(() =>
                        {
                            lblValidationStatus.ForeColor = Color.Red;
                            lblRunningStatus.ForeColor = Color.Red;
                            lblRunningStatus.Text = resources.GetString("RSBarcode_Repeat");
                        });

                        try
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                            DisplayMessage($"条码重复性验证：工位1条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage($"条码重复性验证：工位1条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                        }

                        return false;
                    }
                }
            }
            else
            {
                // 左右款 & 单工位模式：正常的重复性检查
                if (isDataExist)
                {
                    await this.InvokeAsync(() =>
                    {
                        lblValidationStatus.ForeColor = Color.Red;
                        lblRunningStatus.ForeColor = Color.Red;
                        lblRunningStatus.Text = resources.GetString("RSBarcode_Repeat");
                    });

                    try
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, 1);
                        DisplayMessage($"条码重复性验证：条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"条码重复性验证：条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 条码重复性验证
        /// 包含双工位模式下的依赖性检查
        /// </summary>
        private async Task<bool> CheckBarcodeRepeatability(BarcodeVerification bv, string currentBarcode)
        {
            // 1. 双工位模式下的特殊处理（工位依赖检查）
            // 无论是否屏蔽历史数据，工位2必须先检查工位1是否完成
            if (chkDoubleStation.Checked && bv.ID == 2)
            {
                // 工位2：首先检查工位依赖
                if (!await CheckStationDependency(bv, currentBarcode))
                {
                    // 依赖检查失败（例如工位1未测试），直接返回false
                    return false;
                }
                // 依赖检查通过，继续执行后续的“重复性”检查
            }

            // 2. 检查是否屏蔽本地历史数据（重复性）验证
            // (此时工位2的依赖性已验证通过)
            if (chkBanLocalHistoricalData.Checked)
            {
                // 如果屏蔽本地历史数据验证，跳过数据库查询，直接通过
                return true;
            }

            // --- 以下是未屏蔽历史数据时的 数据库重复性检查 ---

            // 3. 数据库查询设置
            string dataPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
            string selectSql = $"SELECT * FROM [Sheet1] WHERE 条码 = '{currentBarcode}'";

            // 屏蔽本地NG数据验证：仅保留OK数据参与验证
            if (chkBanNGDataVerify.Checked)
            {
                selectSql += " AND 测试结果 = 'OK'";
            }

            dbHelper = new MDBHelper(dataPath);
            bool isDataExist = dbHelper.DoesDataExist(selectSql);

            // 4. 根据查询结果处理重复性
            if (chkDoubleStation.Checked)
            {
                // (工位1 和 工位2 的重复检查逻辑)
                if (bv.ID == 2)
                {
                    // 工位2：检查工位2是否已有数据（避免重复测试）
                    if (isDataExist)
                    {
                        await this.InvokeAsync(() =>
                        {
                            lblValidationStatus.ForeColor = Color.Red;
                            lblRunningStatus.ForeColor = Color.Red;
                            lblRunningStatus.Text = resources.GetString("RSBarcode_Repeat");
                        });

                        try
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                            DisplayMessage($"条码重复性验证：工位2条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage($"条码重复性验证：工位2条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                        }

                        return false;
                    }
                }
                else if (bv.ID == 1)
                {
                    // 工位1：正常的重复性检查
                    if (isDataExist)
                    {
                        await this.InvokeAsync(() =>
                        {
                            lblValidationStatus.ForeColor = Color.Red;
                            lblRunningStatus.ForeColor = Color.Red;
                            lblRunningStatus.Text = resources.GetString("RSBarcode_Repeat");
                        });

                        try
                        {
                            await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                            DisplayMessage($"条码重复性验证：工位1条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage($"条码重复性验证：工位1条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                        }

                        return false;
                    }
                }
            }
            else
            {
                // 左右款 & 单工位模式：正常的重复性检查
                if (isDataExist)
                {
                    await this.InvokeAsync(() =>
                    {
                        lblValidationStatus.ForeColor = Color.Red;
                        lblRunningStatus.ForeColor = Color.Red;
                        lblRunningStatus.Text = resources.GetString("RSBarcode_Repeat");
                    });

                    try
                    {
                        await readWriteNet.WriteAsync(bv.ErrorBarcodeEndPLC, Convert.ToInt16(1));
                        DisplayMessage($"条码重复性验证：条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)成功");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage($"条码重复性验证：条码重复，反馈(条码验证{bv.ErrorBarcodeEndPLC} = 1)失败：{ex}");
                    }

                    return false;
                }
            }

            // 5. 所有检查通过（未屏蔽且数据库无重复）
            return true;
        }

        /// <summary>
        /// 获取工位1的测试结果
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns>测试结果信息</returns>
        private (bool hasData, string result) GetStation1TestResult(string barcode)
        {
            // 方法1：从数据库查询
            string dataPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
            string selectSql = $"SELECT 测试结果 FROM [Sheet1] WHERE 条码 = '{barcode}'";

            try
            {
                dbHelper = new MDBHelper(dataPath);
                DataTable resultData = dbHelper.Find(selectSql);

                if (resultData.Rows.Count > 0)
                {
                    string testResult = resultData.Rows[0]["测试结果"]?.ToString();
                    dbHelper.CloseConnection();
                    return (true, testResult);
                }

                dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                DisplayMessage($"查询工位1测试结果失败：{ex.Message}");
            }

            // 方法2：从内存中查询
            var station1Data = stationResultList1.FirstOrDefault(x => x.Barcode == barcode);
            if (station1Data != null)
            {
                return (true, station1Data.Result);
            }

            return (false, null);
        }

        /// <summary>
        /// 增强的错误提示显示
        /// </summary>
        /// <param name="errorType">错误类型</param>
        /// <param name="barcode">条码</param>
        /// <param name="additionalInfo">附加信息</param>
        private void ShowStationDependencyError(string errorType, string barcode, string additionalInfo = "")
        {
            string errorMessage;

            switch (errorType)
            {
                case "NoStation1Data":
                    errorMessage = $"条码 {barcode} 在工位1无测试数据";
                    break;
                case "Station1NG":
                    errorMessage = $"条码 {barcode} 在工位1测试结果为NG";
                    break;
                case "Station2Repeat":
                    errorMessage = $"条码 {barcode} 在工位2已测试过";
                    break;
                default:
                    errorMessage = $"工位依赖检查错误：{errorType}";
                    break;
            }

            if (!string.IsNullOrEmpty(additionalInfo))
            {
                errorMessage += $"，{additionalInfo}";
            }

            DisplayMessage(errorMessage);

            // 可以添加声音提示或其他用户友好的提示方式
            // SystemSounds.Beep.Play();
        }

        /// <summary>
        /// 更新StationResult的测试结果状态
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <param name="stationId">工位ID</param>
        /// <param name="testResult">测试结果</param>
        /// <param name="isCompleted">是否完成测试</param>
        private void UpdateStationResultStatus(string barcode, string stationId, string testResult, bool isCompleted = true)
        {
            List<StationResult> targetList = stationId == "1" ? stationResultList1 : stationResultList2;

            var stationResult = targetList.FirstOrDefault(x => x.Barcode == barcode);
            if (stationResult != null)
            {
                stationResult.Result = testResult;
                stationResult.TestTime = DateTime.Now;

                // 如果测试完成，可以添加完成标记
                if (isCompleted)
                {
                    // 可以添加额外的完成状态字段
                    DisplayMessage($"更新工位{stationId}条码{barcode}的测试结果为：{testResult}");
                }
            }
            else
            {
                DisplayMessage($"警告：在工位{stationId}的结果列表中未找到条码{barcode}的记录");
            }
        }

        /// <summary>
        /// 在测试完成后调用此方法更新状态
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <param name="stationId">工位ID</param>
        /// <param name="finalResult">最终测试结果</param>
        public async Task OnTestCompleted(string barcode, string stationId, string finalResult)
        {
            // 更新内存中的数据
            UpdateStationResultStatus(barcode, stationId, finalResult, true);

            // 保存到数据管理器
            if (_dataManager != null)
            {
                await _dataManager.SaveDataAsync(stationResultList1, stationResultList2);
            }

            // 如果是工位1测试完成且结果为NG，给出额外提示
            if (stationId == "1" && finalResult != "OK")
            {
                DisplayMessage($"工位1测试结果为{finalResult}，此条码将无法进入工位2");

                // 可以发送通知或记录日志
                await this.InvokeAsync(() =>
                {
                    // 更新UI提示
                    lblOperatePrompt.ForeColor = Color.Orange;
                    lblOperatePrompt.Text = resources.GetString("OT_Station1NG");
                });
            }

            // 如果是工位2测试完成，可以清理对应的数据
            if (stationId == "2" && _dataManager != null)
            {
                _dataManager.CleanCompletedData(stationResultList1, stationResultList2, barcode);
                await _dataManager.SaveDataAsync(stationResultList1, stationResultList2);
            }
        }

        /// <summary>
        /// 检查特定条码在工位1的状态
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns>工位1状态信息</returns>
        public (bool exists, string result, DateTime testTime) CheckStation1Status(string barcode)
        {
            var station1Data = stationResultList1.FirstOrDefault(x => x.Barcode == barcode);
            if (station1Data != null)
            {
                return (true, station1Data.Result, station1Data.TestTime);
            }

            // 如果内存中没有，尝试从数据库查询
            try
            {
                string dataPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
                string selectSql = $"SELECT 测试结果, 测试时间 FROM [Sheet1] WHERE 条码 = '{barcode}'";

                dbHelper = new MDBHelper(dataPath);
                DataTable resultData = dbHelper.Find(selectSql);

                if (resultData.Rows.Count > 0)
                {
                    string testResult = resultData.Rows[0]["测试结果"]?.ToString();
                    DateTime testTime = DateTime.TryParse(resultData.Rows[0]["测试时间"]?.ToString(), out DateTime parsedTime)
                        ? parsedTime : DateTime.MinValue;

                    dbHelper.CloseConnection();
                    return (true, testResult, testTime);
                }

                dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                DisplayMessage($"查询工位1状态失败：{ex.Message}");
            }

            return (false, null, DateTime.MinValue);
        }

        /// <summary>
        /// 获取工位依赖检查的详细报告
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns>依赖检查报告</returns>
        public string GetStationDependencyReport(string barcode)
        {
            var (exists, result, testTime) = CheckStation1Status(barcode);

            if (!exists)
            {
                return $"条码 {barcode} 在工位1无测试记录";
            }

            string timeInfo = testTime != DateTime.MinValue ? testTime.ToString("yyyy-MM-dd HH:mm:ss") : "未知";

            return $"条码 {barcode} 工位1状态：{result}，测试时间：{timeInfo}";
        }

        /// <summary>
        /// 清理过期的工位依赖数据
        /// </summary>
        /// <param name="expireHours">过期小时数</param>
        public void CleanExpiredStationData(int expireHours = 24)
        {
            var expireTime = DateTime.Now.AddHours(-expireHours);

            int removed1 = stationResultList1.RemoveAll(x => x.TestTime < expireTime);
            int removed2 = stationResultList2.RemoveAll(x => x.TestTime < expireTime);

            if (removed1 > 0 || removed2 > 0)
            {
                DisplayMessage($"清理过期数据：工位1清理{removed1}条，工位2清理{removed2}条");
            }
        }

        #endregion

        #region ------------ 处理生产数据 ------------

        private StationDataManager _dataManager;
        private List<StationResult> stationResultList1;
        private List<StationResult> stationResultList2;
        private System.Timers.Timer _cleanupTimer;

        /// <summary>
        /// 读取并上传生产数据
        /// </summary>
        private async Task ProcessPlc_ReadData()
        {
            while (isReadData_PLC)
            {
                await ReadDataByStation();
                await Task.Delay(50);
            }
        }

        private async Task ReadDataByStation()
        {
            if (!isPlcConnected) return;

            // 模式一：双工位模式
            if (chkDoubleStation.Checked)
            {
                this.InvokeAsync(() => chkReadBarcodeSecondly.Checked = true);

                // 检查工位1的触发信号
                var trigger1 = await readWriteNet.ReadInt16Async(ssd.StartProductPoint1);
                if (trigger1.IsSuccess && trigger1.Content == 1)
                {
                    //await ProcessProductionData("1");
                    //return; // 处理完一个就返回，避免冲突

                    // --- Bug fixed：使用 Semaphore 互斥锁 ---
                    if (await _uploadSemaphore.WaitAsync(0)) // 尝试获取锁 (0ms超时)
                    {
                        try
                        {
                            await ProcessProductionData("1");
                        }
                        finally
                        {
                            _uploadSemaphore.Release(); // 确保释放锁
                        }
                    }
                    // else: 正在处理中，本次跳过，等待下次PLC触发
                    return; // 处理完一个就返回
                }

                // 检查工位2的触发信号 (例如，来自原双工位版本的 D1092)
                var trigger2 = await readWriteNet.ReadInt16Async(ssd.StartProductPoint2);
                if (trigger2.IsSuccess && trigger2.Content == 1)
                {
                    //await ProcessProductionData("2");
                    //return;

                    // --- Bug fixed：使用 Semaphore 互斥锁 ---
                    if (await _uploadSemaphore.WaitAsync(0)) // 尝试获取锁
                    {
                        try
                        {
                            await ProcessProductionData("2");
                        }
                        finally
                        {
                            _uploadSemaphore.Release(); // 确保释放锁
                        }
                    }
                    return;
                }
            }
            // 模式二：左右款模式
            else if (chkLeftRight.Checked)
            {
                string triggerL_addr = txt_LR1.Text;
                string triggerR_addr = txt_LR2.Text;

                var triggerL = await readWriteNet.ReadInt16Async(triggerL_addr);
                var triggerR = await readWriteNet.ReadInt16Async(triggerR_addr);

                if (triggerL.IsSuccess && triggerR.IsSuccess)
                {
                    if (triggerL.Content == 1 && triggerR.Content == 0)
                    {
                        this.InvokeAsync(() => tabControl_UploadData.SelectedTab = tabPage2);

                        var operateResult = await readWriteNet.ReadInt32Async(ssd.StartProductPoint);   // D1200
                        if (!operateResult.IsSuccess) return;

                        var uploadSignal = operateResult.Content;
                        if (uploadSignal != 1) return;

                        if (!IsVerifyOK && !chkAllowUploadContinuously.Checked)
                        {
                            DisplayMessage("条码验证尚未完成，退出数据上传流程");
                            await readWriteNet.WriteAsync(ssd.EndProductPoint, 1); // 反馈(结束产品点D1201)
                            DisplayMessage($"【数据上传流程结束】 {ssd.EndProductPoint} =1");
                            return;
                        }

                        //await ProcessProductionData("1"); // "Left" 

                        // --- 修正点：使用 Semaphore 互斥锁 ---
                        if (await _uploadSemaphore.WaitAsync(0))
                        {
                            try { await ProcessProductionData("1"); } // "Left" 
                            finally { _uploadSemaphore.Release(); }
                        }
                    }
                    else if (triggerL.Content == 0 && triggerR.Content == 1)
                    {
                        this.InvokeAsync(() => tabControl_UploadData.SelectedTab = tabPage3);


                        var operateResult = await readWriteNet.ReadInt32Async(ssd.StartProductPoint);   // D1200
                        if (!operateResult.IsSuccess) return;

                        var uploadSignal = operateResult.Content;
                        if (uploadSignal != 1) return;

                        if (!IsVerifyOK && !chkAllowUploadContinuously.Checked)
                        {
                            DisplayMessage("条码验证尚未完成，退出数据上传流程");
                            await readWriteNet.WriteAsync(ssd.EndProductPoint, 1); // 反馈(结束产品点D1201)
                            DisplayMessage($"【数据上传流程结束】 {ssd.EndProductPoint} =1");
                            return;
                        }

                        //await ProcessProductionData("2"); // "Right"

                        // --- 修正点：使用 Semaphore 互斥锁 ---
                        if (await _uploadSemaphore.WaitAsync(0))
                        {
                            try { await ProcessProductionData("2"); } // "Right"
                            finally { _uploadSemaphore.Release(); }
                        }
                    }
                    else if (triggerL.Content == 1 && triggerR.Content == 1)
                    {
                        this.InvokeAsync(() => tabControl_UploadData.SelectedTab = tabPage1);

                        var operateResult = await readWriteNet.ReadInt32Async(ssd.StartProductPoint);   // D1200
                        if (!operateResult.IsSuccess) return;

                        var uploadSignal = operateResult.Content;
                        if (uploadSignal != 1) return;

                        if (!IsVerifyOK && !chkAllowUploadContinuously.Checked)
                        {
                            DisplayMessage("条码验证尚未完成，退出数据上传流程");
                            await readWriteNet.WriteAsync(ssd.EndProductPoint, 1); // 反馈(结束产品点D1201)
                            DisplayMessage($"【数据上传流程结束】 {ssd.EndProductPoint} =1");
                            return;
                        }

                        //await ProcessProductionData("3"); // "Both"

                        // --- 修正点：使用 Semaphore 互斥锁 ---
                        if (await _uploadSemaphore.WaitAsync(0))
                        {
                            try { await ProcessProductionData("3"); } // "Both"
                            finally { _uploadSemaphore.Release(); }
                        }
                    }
                }
            }
            // 模式三：单工位模式
            else
            {
                var operateResult = await readWriteNet.ReadInt32Async(ssd.StartProductPoint);   // D1200
                if (!operateResult.IsSuccess) return;

                var uploadSignal = operateResult.Content;
                if (uploadSignal != 1) return;

                if (!IsVerifyOK && !chkAllowUploadContinuously.Checked)
                {
                    DisplayMessage("条码验证尚未完成，退出数据上传流程");
                    await readWriteNet.WriteAsync(ssd.EndProductPoint, 1); // 反馈(结束产品点D1201)
                    DisplayMessage($"【数据上传流程结束】 {ssd.EndProductPoint} =1");
                    return;
                }

                //await ProcessProductionData();
                // --- 修正点：使用 Semaphore 互斥锁 ---
                if (await _uploadSemaphore.WaitAsync(0))
                {
                    try
                    {
                        await ProcessProductionData();
                    }
                    finally
                    {
                        _uploadSemaphore.Release();
                    }
                }
            }
        }

        /// <summary>
        /// 二次读取条码？ -> 读测试结果 -> 上传MES -> 上传看板 -> 数据显示 -> 本地保存
        /// </summary>
        /// <returns></returns>
        private async Task ProcessProductionData(string stationToken = null)
        {
            this.InvokeAsync(() => lblRunningStatus.Text = resources.GetString("RSData_StartReading"));
            DisplayMessage("【数据上传流程开始】");

            // 二次读条码
            if (chkReadBarcodeSecondly.Checked)
            {
                await ReadBarcodeSecondly(stationToken);
            }

            // 读测试结果
            if (chkLeftRight.Checked || chkDoubleStation.Checked)
            {
                await ReadTestResult(stationToken);
                //}
                //else if (chkDoubleStation.Checked)
                //{
                //    await ReadTestResult(stationToken);
            }
            else
            {
                await ReadTestResult();
            }

            // 更新产品结果UI
            await UpdateProductResultUI(stationToken);

            // 双工位数据缓存
            await DataChace(stationToken);

            // 上传条码及相应测试结果到MES
            await SendToMESAsync(stationToken);

            // 反馈信号给PLC
            await FinalizeProductionCycle(stationToken);

            // 上传看板
            await GenerateAndSendProductionDataAsync(stationToken);

            // 数据显示
            await ShowResultByStation(stationToken);

            // 数据保存
            await SaveProductionDataConditionally(stationToken);

            this.InvokeAsync(() =>
            {
                lblOperatePrompt.ForeColor = Color.Black;
                lblOperatePrompt.Text = resources.GetString("OT_WaitingScan"); // 等待扫描条码
            });
        }

        private async Task ReadBarcodeSecondly(string stationToken = null)
        {
            if (chkDoubleStation.Checked)
            {
                if (stationToken == "1")
                {
                    ushort.TryParse(ssd.SecondLength1, out ushort sencondLength);

                    var result = await readWriteNet.ReadStringAsync(ssd.SecondPoint1, sencondLength);
                    if (result.IsSuccess)
                    {
                        string rawBarcode = result.Content;
                        // Bug fixed: 读取到的条码信息未赋值给 barcodeInfo1，原本是直接赋值给 barcodeInfo
                        barcodeInfo1 = CodeNum.CleanString(rawBarcode);
                        DisplayMessage($"工位{stationToken}二次读条码成功，条码为：{barcodeInfo}");
                    }
                }
                else if (stationToken == "2")
                {
                    ushort.TryParse(ssd.SecondLength2, out ushort sencondLength);

                    var result = await readWriteNet.ReadStringAsync(ssd.SecondPoint2, sencondLength);
                    if (result.IsSuccess)
                    {
                        string rawBarcode = result.Content;
                        // Bug fixed: 读取到的条码信息未赋值给 barcodeInfo2，原本是直接赋值给 barcodeInfo
                        barcodeInfo2 = CodeNum.CleanString(rawBarcode);
                        DisplayMessage($"工位{stationToken}二次读条码成功，条码为：{barcodeInfo}");
                    }
                }
            }
            else
            {
                ushort.TryParse(ssd.SecondProductLength, out ushort secondProductLength);

                var operateResult = await readWriteNet.ReadStringAsync(ssd.SecondProductPoint, secondProductLength);
                if (!operateResult.IsSuccess)
                {
                    DisplayMessage("二次读条码失败");
                    return;
                }
                string rawBarcode = operateResult.Content;

                barcodeInfo = CodeNum.CleanString(rawBarcode);
                if (string.IsNullOrWhiteSpace(barcodeInfo))
                {
                    DisplayMessage("二次读取条码为空");
                    return;
                }

                DisplayMessage($"二次读条码成功，条码为：{barcodeInfo}");
            }
        }

        /// <summary>
        /// 读测试结果（各测试项结果 -> 产品总结果）
        /// </summary>
        private async Task ReadTestResult(string stationToken = null)
        {
            nameList = new List<string>();
            testList = new List<string>();
            maxList = new List<string>();
            minList = new List<string>();
            resultList = new List<string>();

            if (PLCPointInfoTable.Rows.Count > 0)
            {
                // 双工位或左右款
                if (chkDoubleStation.Checked || chkLeftRight.Checked)
                {
                    //NameList = new List<string>();

                    for (int i = 0; i < testItemsName_Chinese.Length; i++)
                    {
                        if (targetStationNum[i] == stationToken || stationToken == "3")
                        {
                            nameList.Add(testItemsName_Chinese[i]);
                            testList.Add(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["BoardCode"].ToString()));
                            maxList.Add(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString()));
                            minList.Add(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString()));
                            resultList.Add(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString()));
                        }
                    }
                }
                // 单工位
                else
                {
                    var tasks = PLCPointInfoTable.AsEnumerable().Select(async row =>
                    {

                        var listItemTask = ProcessPointData_PLC(row["BoardCode"].ToString());           // 实际值
                        var maxItemTask = ProcessPointData_PLC(row["MaxBoardCode"].ToString());        // 上限
                        var minItemTask = ProcessPointData_PLC(row["MinBoardCode"].ToString());        // 下限
                        var resultItemTask = ProcessPointData_PLC(row["ResultBoardCode"].ToString());   // 测试结果

                        await Task.WhenAll(listItemTask, maxItemTask, minItemTask, resultItemTask);
                        return new
                        {
                            ListItem = await listItemTask,
                            MaxItem = await maxItemTask,
                            MinItem = await minItemTask,
                            ResultItem = await resultItemTask
                        };

                    }).ToList();

                    var results = await Task.WhenAll(tasks);    // 等待所有行的数据都读取完毕
                    foreach (var r in results)
                    {
                        testList.Add(r.ListItem);
                        maxList.Add(r.MaxItem);
                        minList.Add(r.MinItem);
                        resultList.Add(r.ResultItem);
                    }
                }
            }

            // 读取产品总测试结果
            // 双工位
            if (chkDoubleStation.Checked)
            {
                if (stationToken == "1")
                {
                    var result = await readWriteNet.ReadInt16Async(ssd.TotalProductPoint1);
                    if (result.IsSuccess)
                    {
                        MES_Result[3688] = result.Content.ToString();
                    }
                }

                if (stationToken == "2")
                {
                    var result = await readWriteNet.ReadInt16Async(ssd.TotalProductPoint2);
                    if (result.IsSuccess)
                    {
                        MES_Result[3688] = result.Content.ToString();
                    }
                }
            }
            // 单工位 & 左右款
            else
            {
                OperateResult<int> result = await readWriteNet.ReadInt32Async(ssd.TotalProductPoint);
                if (result.IsSuccess)
                {
                    MES_Result[3688] = result.Content.ToString();
                }

                DisplayMessage("读取产品测试数据成功");
            }
        }

        /// <summary>
        /// 更新产品结果UI显示
        /// </summary>
        /// <returns></returns>
        private async Task UpdateProductResultUI(string stationToken = null)
        {
            // 双工位
            if (chkDoubleStation.Checked)
            {
                if (stationToken == "1")
                {
                    if (MES_Result[3688] == "3")
                    {
                        this.InvokeAsync(() =>
                        {
                            lbl_Left.ForeColor = Color.White;
                            lbl_Left.BackColor = Color.Green;
                            lbl_Left.Text = "OK";

                            lbl_Right.ForeColor = Color.Black;
                            lbl_Right.BackColor = Color.White;
                            lbl_Right.Text = resources.GetString("Standby");
                            FontAutoResizer.ChangeLabelFont(lbl_Right, 22F, FontStyle.Bold);

                        });

                        ProductResult = "OK";
                    }
                    else
                    {
                        this.InvokeAsync(async () =>
                        {
                            lbl_Left.ForeColor = Color.White;
                            lbl_Left.BackColor = Color.Red;
                            lbl_Left.Text = "NG";

                            lbl_Right.ForeColor = Color.Black;
                            lbl_Right.BackColor = Color.White;
                            lbl_Right.Text = resources.GetString("Standby");
                            FontAutoResizer.ChangeLabelFont(lbl_Right, 22F, FontStyle.Bold);
                        });

                        ProductResult = "NG";
                    }

                }
                else if (stationToken == "2")
                {
                    if (MES_Result[3688] == "3")
                    {
                        this.InvokeAsync(() =>
                        {
                            lbl_Right.ForeColor = Color.White;
                            lbl_Right.BackColor = Color.Green;
                            lbl_Right.Text = "OK";

                            lbl_Left.ForeColor = Color.Black;
                            lbl_Left.BackColor = Color.White;
                            lbl_Left.Text = resources.GetString("Standby");
                            FontAutoResizer.ChangeLabelFont(lbl_Left, 22F, FontStyle.Bold);
                        });

                        ProductResult = "OK";
                    }
                    else
                    {
                        this.InvokeAsync(async () =>
                        {
                            lbl_Right.ForeColor = Color.White;
                            lbl_Right.BackColor = Color.Red;
                            lbl_Right.Text = "NG";

                            lbl_Left.ForeColor = Color.Black;
                            lbl_Left.BackColor = Color.White;
                            lbl_Left.Text = resources.GetString("Standby");
                            FontAutoResizer.ChangeLabelFont(lbl_Left, 22F, FontStyle.Bold);
                        });

                        ProductResult = "NG";
                    }
                }
            }
            // 单工位 & 左右款
            else
            {
                if (MES_Result[3688] == "3")
                {
                    this.InvokeAsync(() =>
                    {
                        lblProductResult.ForeColor = Color.White;
                        lblProductResult.BackColor = Color.Green;
                        lblProductResult.Text = "OK";
                    });

                    ProductResult = "OK";
                }
                else
                {
                    this.InvokeAsync(async () =>
                    {
                        lblProductResult.ForeColor = Color.White;
                        lblProductResult.BackColor = Color.Red;
                        lblProductResult.Text = "NG";
                    });

                    ProductResult = "NG";
                }
            }
        }

        private async Task DataChace(string stationToken = null)
        {
            if (!chkDoubleStation.Checked) return;

            // --- 修正点：根据 stationToken 决定使用哪个条码 ---
            string currentBarcode;
            if (stationToken == "1")
            {
                currentBarcode = barcodeInfo1;
                if (string.IsNullOrWhiteSpace(currentBarcode))
                {
                    DisplayMessage($"DataChace 错误: 工位1未能从 barcodeInfo1 获取条码。");
                    return; // 无法缓存
                }
            }
            else // stationToken == "2"
            {
                currentBarcode = barcodeInfo2;
                if (string.IsNullOrWhiteSpace(currentBarcode))
                {
                    DisplayMessage($"DataChace 错误: 工位2未能从 barcodeInfo2 获取条码。");
                    return; // 无法缓存
                }
            }
            // --- 修正点结束 ---


            var targetList = stationToken == "1" ? stationResultList1 : stationResultList2;

            // 查找是否已存在相同条码的记录
            //var existingResult = targetList.FirstOrDefault(x => x.Barcode.Contains(barcodeInfo));
            // --- 修正点：使用 currentBarcode 查找 ---
            var existingResult = targetList.FirstOrDefault(x => x.Barcode == currentBarcode);

            if (existingResult != null)
            {
                // 更新现有记录
                existingResult.stationId = stationToken;
                existingResult.Result = ProductResult;
                existingResult.actualValueList = new List<string>(this.testList);
                existingResult.maxList = new List<string>(this.maxList);
                existingResult.minList = new List<string>(this.minList);
                existingResult.resultList = new List<string>(this.resultList);
                existingResult.nameList = new List<string>(this.nameList);
                existingResult.TestTime = DateTime.Now; // 更新时间戳
            }
            else
            {
                // 添加新记录
                var stationResult = new StationResult
                {
                    stationId = stationToken,
                    //Barcode = barcodeInfo,
                    Barcode = currentBarcode, // <-- 修正点
                    Result = ProductResult,
                    actualValueList = new List<string>(this.testList),
                    maxList = new List<string>(this.maxList),
                    minList = new List<string>(this.minList),
                    resultList = new List<string>(this.resultList),
                    nameList = new List<string>(this.nameList),
                    TestTime = DateTime.Now
                };
                targetList.Add(stationResult);
            }

            // 限制缓存大小
            _dataManager.LimitCacheSize(stationResultList1, stationResultList2);

            // 异步保存数据
            await _dataManager.SaveDataAsync(stationResultList1, stationResultList2);
        }

        /// <summary>
        /// 数据上传MES
        /// </summary>
        /// <param name="stationToken"></param>
        /// <returns></returns>
        private async Task SendToMESAsync(string stationToken = null)
        {
            // 判断是否需要上传数据
            if (!ShouldUploadToMES(stationToken))
            {
                if (chkDoubleStation.Checked)
                {
                    DisplayMessage($"当前不需要上传MES数据 - 工位: {stationToken}, 结果: {ProductResult}");
                }
                return;
            }

            await this.InvokeAsync(() =>
            {
                lblUploadStatus.ForeColor = Color.Orange;
                lblRunningStatus.ForeColor = Color.Black;
                lblRunningStatus.Text = resources.GetString("RSData_Uploading");  // 联机数据上传中
                lblOperatePrompt.ForeColor = Color.Black;
                lblOperatePrompt.Text = resources.GetString("OT_Wait");        // 请等待
            });

            try
            {
                // 联机上传条码测试数据
                bool isProductOK = ProductResult == "OK";

                // Bug fixed: 双工位上传条码最新条码覆盖的问题
                if (!chkDoubleStation.Checked)
                {
                    await UploadResult_MES(barcodeInfo, isProductOK);
                }
                else
                {
                    await UploadResult_MES(barcodeInfo2, isProductOK);  // 双工位
                }

                await UpdateUploadStatus();
            }
            catch (Exception ex)
            {
                DisplayMessage($"MES数据上传异常: {ex.Message}");

                // 设置上传失败状态
                MES_Result[2008] = "1";
                await this.InvokeAsync(() =>
                {
                    lblUploadStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSData_UploadNG");
                });
            }
        }

        private async Task UpdateUploadStatus()
        {
            if (MES_Result[2006] == "1")
            {
                await this.InvokeAsync(() =>
                {
                    lblUploadStatus.ForeColor = Color.Green;
                    lblRunningStatus.ForeColor = Color.Green;
                    lblRunningStatus.Text = resources.GetString("RSData_UploadOK");
                });
            }
            else if (MES_Result[2008] == "1")
            {
                await this.InvokeAsync(() =>
                {
                    lblUploadStatus.ForeColor = Color.Red;
                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSData_UploadNG");
                    lblOperatePrompt.Text = resources.GetString("OT_Reupload");
                });
            }
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="stationToken"></param>
        /// <returns></returns>
        private async Task ShowResultByStation(string stationToken = null)
        {
            if (chkDoubleStation.Checked)
            {
                if (stationToken == "1")
                {
                    Num1++;
                    await ShowResult1(dgvResult1, stationToken);
                }
                else if (stationToken == "2")
                {
                    Num2++;
                    await ShowResult2(dgvResult2, stationToken);
                }
            }
            else if (chkLeftRight.Checked)
            {
                if (stationToken == "1")
                {
                    Num1++;
                    await ShowResult1(dgvResult2, stationToken);
                }
                else if (stationToken == "2")
                {
                    Num1++;
                    await ShowResult1(dgvResult3, stationToken);
                }
                else
                {
                    Num1++;
                    await ShowResult1(dgvResult1, stationToken);
                }
            }
            else
            {
                Num1++;
                await ShowResult1(dgvResult1);
            }
        }

        /// <summary>
        /// 数据反馈，结束周期
        /// </summary>
        /// <param name="stationToken"></param>
        /// <returns></returns>
        private async Task FinalizeProductionCycle(string stationToken = null)
        {
            try
            {
                // 双工位
                if (chkDoubleStation.Checked)
                {
                    if (stationToken == "1")
                    {
                        await readWriteNet.WriteAsync(txtEndPoint1.Text, Convert.ToInt16(1));
                    }
                    else if (stationToken == "2")
                    {
                        await readWriteNet.WriteAsync(txtEndPoint2.Text, Convert.ToInt16(1));
                    }
                }
                // 单工位 & 左右款
                else
                {
                    await readWriteNet.WriteAsync(ssd.EndProductPoint, 1);
                    DisplayMessage($"数据联机上传成功，反馈PLC {ssd.EndProductPoint} = 1\r\n");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"数据联机上传成功，反馈PLC {ssd.EndProductPoint} = 1失败\r\n");
            }
            finally
            {
                isSaveDataSuccessfully = true;
                IsVerifyOK = false; // 重置条码验证状态，避免同一条码被重复上传
            }
        }

        /// <summary>
        /// 判断是否应该上传到MES
        /// </summary>
        /// <param name="stationToken">当前工位</param>
        /// <returns>是否应该上传</returns>
        private bool ShouldUploadToMES(string stationToken)
        {
            // 离线模式不上传
            if (isOffLine == 1)
            {
                return false;
            }

            // 双工位模式
            if (chkDoubleStation.Checked)
            {
                // 工位2完成 OR 当前结果为NG 才上传
                //return stationToken == "2" || ProductResult == "NG";

                // 只有工位2完成才上传
                return stationToken == "2";
            }

            // 单工位和左右款模式：都需要上传
            return true;
        }

        // <summary>
        /// 根据当前模式和条码获取要上传到MES的测试数据
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns>要上传的测试数据</returns>
        private async Task<UploadDataModel> GetTestDataForMESUpload(string barcode)
        {
            // 双工位模式：需要根据不同情况获取不同范围的数据
            if (chkDoubleStation.Checked)
            {
                var station1Data = stationResultList1.FirstOrDefault(x => x.Barcode == barcode);
                var station2Data = stationResultList2.FirstOrDefault(x => x.Barcode == barcode);

                // 情况1：工位2完成测试，需要上传工位1+工位2的完整数据
                if (station2Data != null)
                {
                    DisplayMessage($"双工位完成，准备上传工位1+工位2完整数据：{barcode}");
                    return await GetCombinedStationData(barcode);
                }
                // 情况2：工位1测试结果为NG，只上传工位1数据
                else if (station1Data != null && station1Data.Result == "NG")
                {
                    DisplayMessage($"工位1测试NG，准备上传工位1数据：{barcode}");
                    return GetStationData(station1Data);
                }
                // 情况3：工位1测试结果为OK，但工位2还未完成 (理论上不应该到这里)
                else if (station1Data != null && station1Data.Result == "OK")
                {
                    DisplayMessage($"警告：工位1测试OK但工位2未完成，这种情况不应该触发上传：{barcode}");
                    return GetStationData(station1Data);
                }
            }

            // 单工位或左右款模式：使用当前测试数据
            DisplayMessage($"单工位/左右款模式，使用当前测试数据：{barcode}");
            return new UploadDataModel
            {
                ResultList = new List<string>(resultList ?? new List<string>()),
                ActualValueList = new List<string>(testList ?? new List<string>()),
                MaxList = new List<string>(maxList ?? new List<string>()),
                MinList = new List<string>(minList ?? new List<string>()),
                TestItemsNames = TestItemsName_Upload ?? new string[0],
                MaxValuePoint = maxValuePoint ?? new string[0],
                MinValuePoint = minValuePoint ?? new string[0],
                FinalResult = ProductResult
            };
        }

        // <summary>
        /// 获取合并后的工位1+工位2数据
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns>合并后的测试数据</returns>
        private async Task<UploadDataModel> GetCombinedStationData(string barcode)
        {
            var station1Data = stationResultList1.FirstOrDefault(x => x.Barcode == barcode);
            var station2Data = stationResultList2.FirstOrDefault(x => x.Barcode == barcode);

            var uploadData = new UploadDataModel();

            // 构建完整的测试项名称数组（按工位顺序：工位1项目 + 工位2项目）
            var completeTestItemsList = new List<string>();
            var completeMaxValuePointList = new List<string>();
            var completeMinValuePointList = new List<string>();

            // 首先添加工位1的测试项
            for (int i = 0; i < testItemsName_Chinese.Length; i++)
            {
                if (targetStationNum[i] == "1")
                {
                    completeTestItemsList.Add(TestItemsName_Upload[i]);
                    completeMaxValuePointList.Add(maxValuePoint[i]);
                    completeMinValuePointList.Add(minValuePoint[i]);
                }
            }

            // 然后添加工位2的测试项
            for (int i = 0; i < testItemsName_Chinese.Length; i++)
            {
                if (targetStationNum[i] == "2")
                {
                    completeTestItemsList.Add(TestItemsName_Upload[i]);
                    completeMaxValuePointList.Add(maxValuePoint[i]);
                    completeMinValuePointList.Add(minValuePoint[i]);
                }
            }

            uploadData.TestItemsNames = completeTestItemsList.ToArray();
            uploadData.MaxValuePoint = completeMaxValuePointList.ToArray();
            uploadData.MinValuePoint = completeMinValuePointList.ToArray();

            // 合并测试数据：工位1数据 + 工位2数据
            if (station1Data != null)
            {
                uploadData.ResultList.AddRange(station1Data.resultList ?? new List<string>());
                uploadData.ActualValueList.AddRange(station1Data.actualValueList ?? new List<string>());
                uploadData.MaxList.AddRange(station1Data.maxList ?? new List<string>());
                uploadData.MinList.AddRange(station1Data.minList ?? new List<string>());
            }

            if (station2Data != null)
            {
                uploadData.ResultList.AddRange(station2Data.resultList ?? new List<string>());
                uploadData.ActualValueList.AddRange(station2Data.actualValueList ?? new List<string>());
                uploadData.MaxList.AddRange(station2Data.maxList ?? new List<string>());
                uploadData.MinList.AddRange(station2Data.minList ?? new List<string>());

                // 最终结果以工位2为准
                uploadData.FinalResult = station2Data.Result;
            }

            DisplayMessage($"合并数据完成 - 工位1项目数: {station1Data?.resultList?.Count ?? 0}, " +
                          $"工位2项目数: {station2Data?.resultList?.Count ?? 0}, " +
                          $"总项目数: {uploadData.ResultList.Count}, 最终结果: {uploadData.FinalResult}");

            return uploadData;
        }

        // <summary>
        /// 获取单个工位的数据（用于工位1 NG的情况）
        /// </summary>
        /// <param name="stationData">工位数据</param>
        /// <returns>单工位测试数据</returns>
        private UploadDataModel GetStationData(StationResult stationData)
        {
            if (stationData == null)
            {
                return new UploadDataModel();
            }

            // 构建该工位的测试项名称数组
            var stationTestItemsList = new List<string>();
            var stationMaxValuePointList = new List<string>();
            var stationMinValuePointList = new List<string>();

            for (int i = 0; i < testItemsName_Chinese.Length; i++)
            {
                if (targetStationNum[i] == stationData.stationId)
                {
                    stationTestItemsList.Add(TestItemsName_Upload[i]);
                    stationMaxValuePointList.Add(maxValuePoint[i]);
                    stationMinValuePointList.Add(minValuePoint[i]);
                }
            }

            return new UploadDataModel
            {
                ResultList = new List<string>(stationData.resultList ?? new List<string>()),
                ActualValueList = new List<string>(stationData.actualValueList ?? new List<string>()),
                MaxList = new List<string>(stationData.maxList ?? new List<string>()),
                MinList = new List<string>(stationData.minList ?? new List<string>()),
                TestItemsNames = stationTestItemsList.ToArray(),
                MaxValuePoint = stationMaxValuePointList.ToArray(),
                MinValuePoint = stationMinValuePointList.ToArray(),
                FinalResult = stationData.Result
            };
        }

        #endregion

        #region ------------ 条码显示 ------------

        DataTable BaleTable = null;
        string barcode1;
        string barcode2;

        /// <summary>
        /// 读取条码并显示
        /// </summary>
        private async Task ProcessPlc_ShowBarcode()
        {
            if (!chkDoubleStation.Checked) return;

            while (isReadData_PLC)
            {
                await LangBar();
                await Task.Delay(1000);
            }
        }

        string[] barcodeArray;
        public async Task LangBar()
        {
            if (!isPlcConnected) return;

            string station = resources.GetString("d1StationName");
            string barcode = resources.GetString("d1Barcode");

            var bvList = BarcodeVerificationServer.GetAllBarcodeVerifications();
            if (!int.TryParse(txtStationCount.Text, out int StationQTY))
            {
                StationQTY = 2;
            }

            barcodeArray = new string[bvList.Count];

            for (int i = 0; i < bvList.Count; i++)
            {
                BarcodeVerification bv = bvList[i];
                ushort barcodeLength = bv.GetBarcodeLength();
                barcodeArray[i] = CodeNum.CleanString(readWriteNet.ReadString(bv.BarcodePositionPLC, barcodeLength).Content);
            }

            if (BaleTable == null)
            {
                BaleTable = new DataTable();

                BaleTable.Columns.Add(station, typeof(string));
                BaleTable.Columns.Add(barcode, typeof(string));

                if (stationNameSets.Length == StationQTY)
                {
                    DataRow dr1 = BaleTable.NewRow();
                    dr1[station] = stationNameSets[0];
                    dr1[barcode] = barcodeArray[0];
                    BaleTable.Rows.Add(dr1);

                    DataRow dr2 = BaleTable.NewRow();
                    dr2[station] = stationNameSets[1];
                    dr2[barcode] = barcodeArray[1];
                    BaleTable.Rows.Add(dr2);
                }

                this.InvokeAsync(() => dgvShowBarcode.DataSource = BaleTable);
            }
            else
            {
                if (stationNameSets.Length == StationQTY)
                {

                    dgvShowBarcode.Rows[0].Cells[0].Value = StationNameArray[0];
                    dgvShowBarcode.Rows[0].Cells[1].Value = barcodeArray[0];

                    dgvShowBarcode.Rows[1].Cells[0].Value = StationNameArray[1];
                    dgvShowBarcode.Rows[1].Cells[1].Value = barcodeArray[1];
                }
            }
        }

        #endregion

        #region ------------ MES上传 ------------

        NLog.Logger loggerMESBarCoode = NLog.LogManager.GetLogger("MESBarCoodeLog");    // 记录上传条码后的MES反馈
        NLog.Logger loggerMESData = NLog.LogManager.GetLogger("MESDataLog");            // 记录上传测试结果的后MES反馈
        private bool isMesLoginSuccessful = false;  // 用户联机验证结果

        private string appVersion;
        private string fileVersion;

        #region ----- 从文本框获取MES配置参数 -----

        private string ip => txtMES_IP.Text;
        private string port => txtMES_Port.Text;
        private string timeout => txtMES_Timeout.Text;
        private string nccode => txt_nccode.Text;
        private string operation => txtOperation.Text;
        private string user => txt_user.Text;
        private string password => txt_password.Text;
        private string url => txtMES_url.Text;
        private string site => txtMES_site.Text;
        private string resource => txtResource.Text;
        private string _header;

        public string Header
        {
            get
            {
                switch (resources.BaseName)
                {
                    case "MesDatas.Language_Resources.language_English":
                        _header = "en-US";
                        break;
                    case "MesDatas.Language_Resources.language_Thai":
                        _header = "th";
                        break;
                    case "MesDatas.Language_Resources.language_Chinese":
                        _header = "zh-CN";
                        break;
                    default:
                        _header = "";
                        break;
                }
                return _header;
            }
            set { _header = value; }
        }


        #endregion

        /// <summary>
        /// MES参数 > 保存
        /// </summary>
        private void SaveMesConfig_Click(object sender, EventArgs e)
        {
            SaveParameter_MES();
            LoadParameter_MES();
            VarifyUserLogin_MES();
        }

        /// <summary>
        /// 加载MES联机参数到系统
        /// </summary>
        public void LoadParameter_MES()
        {
            dbHelper = new MDBHelper(path4);
            DataTable table = dbHelper.Find("select * from SytemInfo where ID = '1'");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    txtMES_IP.Text = table.Rows[i]["IP"].ToString();
                    txtMES_Port.Text = table.Rows[i]["Port"].ToString();
                    txtMES_Timeout.Text = table.Rows[i]["Timeout"].ToString();
                    txt_nccode.Text = table.Rows[i]["NcCode"].ToString();
                    txtOperation.Text = table.Rows[i]["Opration"].ToString();
                    txt_password.Text = table.Rows[i]["Password"].ToString();
                    txtResource.Text = table.Rows[i]["Resource"].ToString();
                    txtMES_site.Text = table.Rows[i]["Site"].ToString();
                    txtMES_url.Text = table.Rows[i]["Url"].ToString();
                    txt_user.Text = table.Rows[i]["User"].ToString();
                }
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 保存MES联机参数到数据库
        /// </summary>
        public void SaveParameter_MES()
        {
            SytemInfoEntity infoEntity = new SytemInfoEntity();
            infoEntity.IP = txtMES_IP.Text;
            infoEntity.Port = txtMES_Port.Text;
            infoEntity.Timeout = txtMES_Timeout.Text;
            infoEntity.NcCode = txt_nccode.Text;
            infoEntity.Opration = txtOperation.Text;
            infoEntity.Password = txt_password.Text;
            infoEntity.Resource = txtResource.Text;
            infoEntity.Site = txtMES_site.Text;
            infoEntity.Url = txtMES_url.Text;
            infoEntity.User = txt_user.Text;

            SaveSystemInfo(path4, infoEntity);
        }

        private void SaveSystemInfo(string conn, SytemInfoEntity systemInfo)
        {
            try
            {
                dbHelper = new MDBHelper(conn);
                DataTable table1 = dbHelper.Find("select * from SytemInfo where ID = '1'");

                if (table1.Rows.Count > 0)
                {

                    DataRow row = table1.Rows[0];
                    List<string> changeDetails = new List<string>();

                    // 检查每个字段的变化
                    if (row["IP"].ToString() != systemInfo.IP)
                    {
                        changeDetails.Add($"IP：{row["IP"]} -> {systemInfo.IP}");
                    }
                    if (row["Port"].ToString() != systemInfo.Port)
                    {
                        changeDetails.Add($"端口：{row["Port"]} -> {systemInfo.Port}");
                    }
                    if (row["Timeout"].ToString() != systemInfo.Timeout)
                    {
                        changeDetails.Add($"连接超时：{row["Timeout"]} -> {systemInfo.Timeout}");
                    }
                    if (row["Url"].ToString() != systemInfo.Url)
                    {
                        changeDetails.Add($"URL：{row["Url"]} -> {systemInfo.Url}");
                    }
                    if (row["Site"].ToString() != systemInfo.Site)
                    {
                        changeDetails.Add($"站点：{row["Site"]} -> {systemInfo.Site}");
                    }
                    if (row["Resource"].ToString() != systemInfo.Resource)
                    {
                        changeDetails.Add($"工位：{row["Resource"]} -> {systemInfo.Resource}");
                    }
                    if (row["Opration"].ToString() != systemInfo.Opration)
                    {
                        changeDetails.Add($"工序：{row["Opration"]} -> {systemInfo.Opration}");
                    }
                    if (row["NcCode"].ToString() != systemInfo.NcCode)
                    {
                        changeDetails.Add($"不合格代码：{row["NcCode"]} -> {systemInfo.NcCode}");
                    }
                    if (row["Password"].ToString() != systemInfo.Password)
                    {
                        changeDetails.Add($"密码：{row["Password"]} -> {systemInfo.Password}");
                    }
                    if (row["User"].ToString() != systemInfo.User)
                    {
                        changeDetails.Add($"用户：{row["User"]} -> {systemInfo.User}");
                    }

                    if (changeDetails.Count >= 0)
                    {
                        string sql = $"update [SytemInfo] set [IP] = '{systemInfo.IP}', [Port] = '{systemInfo.Port}', [Timeout] = '{systemInfo.Timeout}', " +
                            $"[NcCode] = '{systemInfo.NcCode}', [Opration] = '{systemInfo.Opration}', [Resource] = '{systemInfo.Resource}', " +
                            $"[Site] = '{systemInfo.Site}', [Url] = '{systemInfo.Url}', [User] = '{systemInfo.User}', [Password] = '{systemInfo.Password}' where [ID] = '1'";

                        var result = dbHelper.Change(sql);
                        if (result)
                        {
                            string changeLog = string.Join("\n", changeDetails);
                            loggerConfig.Trace($"【MES参数修改成功】\n修改详情：\n{changeLog}");
                            MessageBox.Show(resources.GetString("PassBtnSave"));
                        }
                    }
                }
                else
                {
                    MDBHelper.CreateAccessDatabase(conn);
                    MDBHelper.TryCreateAccessTable(conn, "SytemInfo", new System.Collections.ArrayList(new object[]
                    { "ID", "IP", "Port", "Timeout", "NcCode", "Opration", "Password", "Resource", "Site", "Url",
                    "User", "FileVersion", "SoftwareVersion", "UserCheckCmd", "UserCheckPass", "UserCheckFail",
                    "CodeCheckCmd", "CodeCheckPass", "CodeSendCmd" }));

                    DataTable dt = new DataTable("SytemInfo");
                    dt.Columns.Add("ID", typeof(string));
                    dt.Columns.Add("IP", typeof(string));
                    dt.Columns.Add("Port", typeof(string));
                    dt.Columns.Add("Timeout", typeof(string));
                    dt.Columns.Add("NcCode", typeof(string));
                    dt.Columns.Add("Opration", typeof(string));
                    dt.Columns.Add("Password", typeof(string));
                    dt.Columns.Add("Resource", typeof(string));
                    dt.Columns.Add("Site", typeof(string));
                    dt.Columns.Add("Url", typeof(string));
                    dt.Columns.Add("User", typeof(string));
                    dt.Columns.Add("FileVersion", typeof(string));
                    dt.Columns.Add("SoftwareVersion", typeof(string));
                    dt.Columns.Add("UserCheckCmd", typeof(string));
                    dt.Columns.Add("UserCheckFail", typeof(string));
                    dt.Columns.Add("UserCheckPass", typeof(string));
                    dt.Columns.Add("CodeCheckCmd", typeof(string));
                    dt.Columns.Add("CodeCheckPass", typeof(string));
                    dt.Columns.Add("CodeSendCmd", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["ID"] = "1";
                    dr["IP"] = systemInfo.IP ?? "";
                    dr["Port"] = systemInfo.Port ?? "";
                    dr["Timeout"] = systemInfo.Timeout ?? "";
                    dr["NcCode"] = systemInfo.NcCode ?? "";
                    dr["Opration"] = systemInfo.Opration ?? "";
                    dr["Password"] = systemInfo.Password ?? "";
                    dr["Resource"] = systemInfo.Resource ?? "";
                    dr["Site"] = systemInfo.Site ?? "";
                    dr["Url"] = systemInfo.Url ?? "";
                    dr["User"] = systemInfo.User ?? "";
                    dr["FileVersion"] = "";
                    dr["SoftwareVersion"] = "";
                    dr["UserCheckCmd"] = "";
                    dr["UserCheckPass"] = "";
                    dr["UserCheckFail"] = "";
                    dr["CodeCheckCmd"] = "";
                    dr["CodeCheckPass"] = "";
                    dr["CodeSendCmd"] = "";
                    dt.Rows.Add(dr);

                    dbHelper.DatatableToMdb("SytemInfo", dt);
                    MessageBox.Show("系统信息成功创建并保存");
                }

                dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                loggerConfig.Error($"保存MES参数时发生错误: {ex.Message}\n堆栈跟踪: {ex.StackTrace}");
                MessageBox.Show($"{resources.GetString("ErrorBtnSave")}: {ex.Message}");
            }
            finally
            {
                if (dbHelper != null)
                {
                    dbHelper.CloseConnection();
                }
            }

        }

        /// <summary>
        /// MES配置
        /// </summary>
        /// <param name="ip"></param>
        public void Config_Mes(string ip, string port, string timeout, string url, string site,
            string user, string password, string resource, string operation, string ncCode, string lang)
        {
            工艺部信息化组.MesConfig.IP = ip;
            工艺部信息化组.MesConfig.PORT = port;
            工艺部信息化组.MesConfig.TimeOut = int.Parse(timeout);
            工艺部信息化组.MesConfig.URL = url;
            工艺部信息化组.MesConfig.Site = site;

            if (chkUserBinding.Checked)
            {
                工艺部信息化组.MesConfig.UserName = user;
                工艺部信息化组.MesConfig.Password = password;
            }
            else
            {
                工艺部信息化组.MesConfig.UserName = LoginUser;
                工艺部信息化组.MesConfig.Password = LoginPwd;
            }

            工艺部信息化组.MesConfig.Resource = resource;
            工艺部信息化组.MesConfig.Operation = operation;
            工艺部信息化组.MesConfig.NcCode = ncCode;
            工艺部信息化组.MesConfig.Language = lang;
        }

        /// <summary>
        /// MES交互1：用户验证
        /// </summary>
        private async Task VarifyUserLogin_MES()
        {
            this.InvokeAsync(() =>
            {
                lblRunningStatus.Text = resources.GetString("RSUser_Verifing");     // 用户验证中
                lblOperatePrompt.Text = resources.GetString("OT_Wait");         // 请等待
            });

            Config_Mes(ip, port, timeout, url, site, user, password, resource, operation, nccode, Header);

            var result = await MesIntegrationService.VarifyUserLoginAsync();
            string MESFeedback = await ExtractMESInfo(result.MESFeedback);

            if (result.isUserVerifySuccessfully)
            {
                if (MESFeedback != null)
                {
                    this.InvokeAsync(() =>
                    {
                        rtbMesLog.Clear();
                        rtbMesLog.AppendText(MESFeedback);

                        lblRunningStatus.ForeColor = Color.Green;
                        lblRunningStatus.Text = resources.GetString("RSUser_OnlineOK");       // 联机用户验证成功
                        lblOperatePrompt.ForeColor = Color.Black;
                        lblOperatePrompt.Text = resources.GetString("OT_WaitingScan"); // 等待扫描条码
                    });

                    isMesLoginSuccessful = true;
                }
            }
            else
            {
                this.InvokeAsync(() =>
                {
                    rtbMesLog.Clear();
                    rtbMesLog.AppendText(MESFeedback);

                    lblRunningStatus.ForeColor = Color.Red;
                    lblRunningStatus.Text = resources.GetString("RSUser_OnlineNG");
                    lblOperatePrompt.Text = resources.GetString("OT_CheckParam");
                });
            }
        }

        /// <summary>
        /// MES交互2：条码验证
        /// </summary>
        private async Task VerifyBarcode_MES(string barcodeData)
        {
            MES_Result[2002] = "0";
            MES_Result[2004] = "0";

            try
            {
                var result = await MesIntegrationService.VarifyBarcodeAsync(barcodeData);
                string MESFeedback = await ExtractMESInfo(result.MESFeedback);

                // 详细日志记录
                loggerMESBarCoode.Trace($"条码{barcodeData}");
                loggerMESBarCoode.Trace($"MES反馈{MESFeedback}");
                loggerMESBarCoode.Trace($"验证结果: {result.isVerifySuccessfully}");

                this.InvokeAsync(() =>
                {
                    rtbMesLog.Clear();
                    rtbMesLog.AppendText(MESFeedback);
                    rtbMesLog.SelectionStart = rtbMesLog.Text.Length;
                    rtbMesLog.ScrollToCaret();
                });

                if (result.isVerifySuccessfully)
                {
                    MES_Result[2002] = "1";
                }
                else
                {
                    MES_Result[2004] = "1";
                }
            }
            catch (Exception ex)
            {
                loggerMESBarCoode.Trace($"MES验证异常: {ex.Message}");
                MES_Result[2004] = "1"; // 异常时默认设置为失败
            }
        }

        /// <summary>
        /// MES交互3：数据上传
        /// </summary>
        /// <remarks>
        /// <para>验证成功，MES_Result[2006] = "1";
        /// </para>
        /// <para>验证失败，MES_Result[2008] = "1";
        /// </para>
        /// </remarks>
        private async Task UploadResult_MES(string barcode, bool productResult)
        {
            MES_Result[2006] = "0";
            MES_Result[2008] = "0";
            DisplayMessage("数据联机上传开始");

            StringBuilder sb = new StringBuilder();

            // 工装信息
            string[] fixtureInfo = txtFixtureBinding.Text.Split('+');
            if (fixtureInfo.Length > 0)
            {
                for (int i = 0; i < fixtureInfo.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(fixtureInfo[i]))
                    {
                        sb.Append($"!FixtureID{i + 1},FixtureID{i + 1},{fixtureInfo[i]}");
                    }
                }
            }

            string barcodeRule = string.Empty;
            this.InvokeAsync(() => barcodeRule = cboBarcodeRule.Text);

            // 产品编码
            string[] productCode = CodeNum.GetProductCodes(barcodeRule, RecipeTable);
            if (productCode.Length > 0)
            {
                for (int i = 0; i < productCode.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(productCode[i]))
                    {
                        sb.Append($"!MaterialsID{i + 1},MaterialsID{i + 1},{productCode[i]}");
                    }
                }
            }

            // 根据当前模式和工位情况获取要上传的测试数据
            var uploadData = await GetTestDataForMESUpload(barcode);

            // 测试项目数据处理
            if (uploadData.ResultList != null && uploadData.ResultList.Count > 0 && uploadData.TestItemsNames != null)
            {
                // 用于记录已添加的测试项目，避免重复
                HashSet<string> addedTestItems = new HashSet<string>();

                for (int i = 0; i < uploadData.TestItemsNames.Length && i < uploadData.ResultList.Count; i++)
                {
                    if (uploadData.ResultList[i] != "null" && !string.IsNullOrWhiteSpace(uploadData.TestItemsNames[i]))
                    {
                        string englishTestItem = uploadData.TestItemsNames[i]; // 英文测试项名称

                        // 检查是否有数值限制（上限和下限）
                        bool hasNumericLimits = (i < uploadData.MaxValuePoint.Length && i < uploadData.MinValuePoint.Length &&
                                               uploadData.MaxValuePoint[i] != "NO" && uploadData.MinValuePoint[i] != "NO" &&
                                               i < uploadData.MaxList.Count && i < uploadData.MinList.Count &&
                                               !string.IsNullOrWhiteSpace(uploadData.MaxList[i]) && !string.IsNullOrWhiteSpace(uploadData.MinList[i]));

                        if (hasNumericLimits)
                        {
                            // 包含数值的测试项：添加Value, UpperLimit, LowerLimit, Result

                            // 1. 测试值 (Value)
                            string testItemValue = $"{englishTestItem}Value";
                            string testParamValue = $"{englishTestItem}ValueData";
                            if (!addedTestItems.Contains(testItemValue))
                            {
                                sb.Append($"!{testItemValue},{testParamValue},{uploadData.ActualValueList[i]}");
                                addedTestItems.Add(testItemValue);
                            }

                            // 2. 上限值 (UpperLimit)
                            string testItemUpper = $"{englishTestItem}UpperLimit";
                            string testParamUpper = $"{englishTestItem}UpperLimitData";
                            if (!addedTestItems.Contains(testItemUpper))
                            {
                                sb.Append($"!{testItemUpper},{testParamUpper},{uploadData.MaxList[i]}");
                                addedTestItems.Add(testItemUpper);
                            }

                            // 3. 下限值 (LowerLimit)
                            string testItemLower = $"{englishTestItem}LowerLimit";
                            string testParamLower = $"{englishTestItem}LowerLimitData";
                            if (!addedTestItems.Contains(testItemLower))
                            {
                                sb.Append($"!{testItemLower},{testParamLower},{uploadData.MinList[i]}");
                                addedTestItems.Add(testItemLower);
                            }

                            // 4. 测试结果 (Result)
                            string testItemResult = $"{englishTestItem}Result";
                            string testParamResult = $"{englishTestItem}ResultData";
                            if (!addedTestItems.Contains(testItemResult))
                            {
                                sb.Append($"!{testItemResult},{testParamResult},{uploadData.ResultList[i]}");
                                addedTestItems.Add(testItemResult);
                            }
                        }
                        else
                        {
                            // 只有一个结果的测试项：添加Result后缀
                            string testItemResult = $"{englishTestItem}Result";
                            string testParamResult = $"{englishTestItem}ResultData";

                            if (!addedTestItems.Contains(testItemResult))
                            {
                                sb.Append($"!{testItemResult},{testParamResult},{uploadData.ResultList[i]}");
                                addedTestItems.Add(testItemResult);
                            }
                        }
                    }
                }
            }

            // 固定的系统信息（保持英文格式）
            string testItems = $"!UserID,{LoginUser},{LoginName}" +
                $"!BarcodeData,BarcodeData,{barcode}" +
                $"!ProductModel,ProductModel,{txtProductModel.Text}" +
                sb.ToString() +
                $"!TestTotalResult,TestTotalResultData,{uploadData.FinalResult}";

            var result = await MesIntegrationService.UploadBarcodeAsync(productResult, barcode, fileVersion, appVersion, testItems);

            string MES反馈 = await ExtractMESInfo(result.MESFeedback);
            string requestUrl = MesIntegrationService.ParamOUT;

            this.InvokeAsync(() =>
            {
                rtbMesLog.Clear();
                rtbMesLog.AppendText($"{MES反馈}{Environment.NewLine}{requestUrl}");
                loggerMESData.Trace(MES反馈);
            });

            if (result.isUploadSuccessfully)
            {
                MES_Result[2006] = "1";
            }
            else
            {
                MES_Result[2008] = "1";
            }
        }

        public async Task<string> ExtractMESInfo(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "未获取到MES反馈";
            if (str == "The operation has timed out")
            {
                return "连接超时";
            }

            try
            {
                string[] sArray = Regex.Split(str, "<b>");

                // 检查数组长度
                if (sArray.Length <= 1)
                {
                    // 如果数组不足，说明格式不符合预期
                    return str;
                }

                string[] state_Array = Regex.Split(sArray[1], "</td>");
                string state_Str = state_Array[0].Replace("</b>", "");

                string[] info_Array = Regex.Split(sArray[2], "</td>");
                string info_Str = info_Array[0].Replace("</b>", "");
                string info_Str1 = info_Str.Replace("&lt;", "<");
                string info_Str2 = info_Str1.Replace("&gt;", ">");

                return state_Str + Environment.NewLine + info_Str2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "解析MES反馈时发生错误";
            }
        }

        #endregion

        #region ------------ PLC连接 ------------

        dynamic PlcConectObject = null;  // 当前plc连接对象
        //private static HslCommunication.Core.Net.NetworkDeviceBase readWriteNet;
        private static HslCommunication.Core.IReadWriteNet readWriteNet;
        private bool isPlcConnected = false;

        /// <summary>
        /// 管理PLC连接与心跳
        /// <para>1. 如果未连接，循环尝试连接。</para>
        /// <para>2. 如果已连接，执行心跳检查。</para>
        /// 3. 任何读/写失败、异常或“卡住”都会认为断线，并返回步骤1。
        /// </summary>
        private async Task ManagePlcConnectionAsync()
        {
            while (true)
            {
                try
                {
                    // --- 步骤 1: 如果未连接 ---
                    if (!isPlcConnected)
                    {
                        if (await TryConnectPlcAsync())
                        {
                            isPlcConnected = true;
                        }
                        else
                        {
                            // 2秒后重试连接
                            await Task.Delay(2000);
                            continue;
                        }
                    }

                    // --- 步骤 2: 如果已连接，执行心跳检查 ---

                    // 2b. 读取PLC心跳
                    var result = await TryReadInt16Async("1000");
                    if (!result.isReadOk)
                    {
                        // 读取失败，断线
                        isPlcConnected = false;
                        continue;
                    }

                    await Task.Delay(1000);
                }
                catch
                {
                    isPlcConnected = false;
                    await Task.Delay(1000);
                }
            }
        }

        /// <summary>
        /// Try to create PLC connection asynchronously.
        /// </summary>
        /// <returns>True if connect successfully, else return False.</returns>
        private async Task<bool> TryConnectPlcAsync()
        {
            // Bug fixed:只需要断开连接，不需要将相关对象置null
            PlcConectObject?.ConnectClose();
            PlcConectObject = null;
            //readWriteNet = null;

            string ipAddress = ControlExtensions.GetControlPropertyValueSafely(txt_IP, c => c.Text);
            int port = ControlExtensions.GetControlPropertyValueSafely(txt_port, c => int.Parse(c.Text));
            string connectionMethod = ControlExtensions.GetControlPropertyValueSafely(cboConnectType, c => c.Text);

            NetworkDeviceBase networkDeviceBase = null;

            try
            {
                switch (connectionMethod)
                {
                    case "TCP":
                        var omronFinsNet = new OmronFinsNet(ipAddress, port);
                        omronFinsNet.ConnectTimeOut = 1000;
                        omronFinsNet.DA2 = 0;
                        omronFinsNet.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)2;
                        networkDeviceBase = omronFinsNet;
                        break;
                    case "UDP":
                        var omronFinsUdp = new OmronFinsUdp(ipAddress, port);
                        omronFinsUdp.SA1 = 192;
                        omronFinsUdp.ReceiveTimeout = 1000;
                        omronFinsUdp.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)2;
                        readWriteNet = omronFinsUdp;
                        PlcConectObject = omronFinsUdp;
                        break;
                    case "MelsecMcNet":
                        networkDeviceBase = new MelsecMcNet(ipAddress, port);
                        break;
                    case "KeyenceMcNet":
                        networkDeviceBase = new KeyenceMcNet(ipAddress, port);
                        break;
                    case "ModbusTCP":
                        networkDeviceBase = new ModbusTcpNet(ipAddress, port);
                        break;
                    default:
                        networkDeviceBase = new KeyenceMcNet(ipAddress, port);
                        break;
                }

                networkDeviceBase.ReceiveTimeOut = 3000;
                networkDeviceBase.ConnectTimeOut = 3000;

                var connectTask = networkDeviceBase.ConnectServerAsync();
                var completedTask = await Task.WhenAny(connectTask, Task.Delay(5000));

                if (completedTask != connectTask)
                {
                    // 连接超时
                    return false;
                }

                var connect = await connectTask;

                if (connect.IsSuccess && connect.ErrorCode >= 0)
                {
                    readWriteNet = networkDeviceBase;
                    PlcConectObject = networkDeviceBase;
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                //isPlcConnected = false;
                return false;
            }
        }

        /// <summary>
        /// 尝试异步读取 Int16 值，带 500ms 超时保护和最多3次重试。
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <returns>
        /// 一个表示异步操作的任务。
        /// 任务结果是一个元组 (bool isReadOk, short value):
        /// <list type="bullet">
        /// <item><description><c>isReadOk</c>: <c>true</c> 表示读取成功, <c>false</c> 表示3次尝试后依旧失败（包括超时）。</description></item>
        /// <item><description><c>value</c>: 读取成功时的值。如果 <c>isReadOk</c> 为 <c>false</c>，则返回 -1。</description></item>
        /// </list>
        /// </returns>
        private async Task<(bool isReadOk, short value)> TryReadInt16Async(string address)
        {
            for (int i = 0; i < 3; i++)
            {
                Task<OperateResult<short>> readTask = readWriteNet.ReadInt16Async(address);
                Task delayTask = Task.Delay(500);
                Task completedTask = await Task.WhenAny(readTask, delayTask);

                if (delayTask == completedTask)
                {
                    // 超时，进行下一次重试
                    await Task.Delay(50);
                    continue;
                }
                else
                {
                    OperateResult<short> result = await readTask;

                    if (!result.IsSuccess || result.ErrorCode < 0)
                    {
                        // 读取失败或通信失败，进行下一次重试
                        await Task.Delay(50);
                        continue;
                    }
                    else
                    {
                        return (true, result.Content);
                    }
                }
            }

            // 循环3次后仍然失败
            return (false, Convert.ToInt16(-1));
        }

        private async Task ConnectPLC()
        {
            BtnConnectPlc_Click(null, null);
        }

        private async void BtnConnectPlc_Click(object sender, EventArgs e)
        {
            /*try
            {
                switch (cboConnectType.Text)
                {
                    case "KeyenceMcNet":    // 基恩士
                        readWriteNet = new KeyenceMcNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    case "ModbusTCP":       // ModbusTCP
                        readWriteNet = new ModbusTcpNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    case "MelsecMcNet":     // 三菱
                        readWriteNet = new MelsecMcNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    case "OmronFinsNet":    // 欧姆龙
                        readWriteNet = new OmronFinsNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                    default:
                        readWriteNet = new KeyenceMcNet(txt_IP.Text, Convert.ToInt16(txt_port.Text));
                        break;
                }

                readWriteNet.ConnectTimeOut = 3000;
                readWriteNet.ReceiveTimeOut = 3000;
                var result = await readWriteNet.ConnectServerAsync();
                if (!result.IsSuccess || result.ErrorCode < 0)
                {
                    isPlcConnected = false;
                    await ShowMessageBoxAsync(resources.GetString("plcConn"));  // PLC连接失败，请重启软件或重启机台！
                }
                else
                {
                    isPlcConnected = true;
                }
            }
            catch (Exception ex)
            {
                await ShowMessageBoxAsync(ex.Message);
            }*/

            string ipAddress = ControlExtensions.GetControlPropertyValueSafely(txt_IP, c => c.Text);
            int port = ControlExtensions.GetControlPropertyValueSafely(txt_port, c => int.Parse(c.Text));
            string connectionMethod = ControlExtensions.GetControlPropertyValueSafely(cboConnectType, c => c.Text);

            NetworkDeviceBase networkDeviceBase = null;

            try
            {
                switch (connectionMethod)
                {
                    case "TCP":
                        var omronFinsNet = new OmronFinsNet(ipAddress, port);
                        omronFinsNet.ConnectTimeOut = 1000;
                        omronFinsNet.DA2 = 0;
                        omronFinsNet.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)2;
                        networkDeviceBase = omronFinsNet;
                        break;
                    case "UDP":
                        var omronFinsUdp = new OmronFinsUdp(ipAddress, port);
                        omronFinsUdp.SA1 = 192;
                        omronFinsUdp.ReceiveTimeout = 1000;
                        omronFinsUdp.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)2;
                        readWriteNet = omronFinsUdp;
                        PlcConectObject = omronFinsUdp;
                        break;
                    case "MelsecMcNet":
                        networkDeviceBase = new MelsecMcNet(ipAddress, port);
                        break;
                    case "KeyenceMcNet":
                        networkDeviceBase = new KeyenceMcNet(ipAddress, port);
                        break;
                    case "ModbusTCP":
                        networkDeviceBase = new ModbusTcpNet(ipAddress, port);
                        break;
                    default:
                        networkDeviceBase = new KeyenceMcNet(ipAddress, port);
                        break;
                }

                networkDeviceBase.ReceiveTimeOut = 3000;
                networkDeviceBase.ConnectTimeOut = 3000;

                var connectTask = networkDeviceBase.ConnectServerAsync();
                var completedTask = await Task.WhenAny(connectTask, Task.Delay(5000));

                if (completedTask != connectTask)
                {
                    // 连接超时
                    return;
                }

                var connect = await connectTask;

                if (connect.IsSuccess && connect.ErrorCode >= 0)
                {
                    readWriteNet = networkDeviceBase;
                    PlcConectObject = networkDeviceBase;
                    return;
                }

            }
            catch (Exception)
            {
                //isPlcConnected = false;
                return;
            }
        }

        private Task ShowMessageBoxAsync(string message)
        {
            return Task.Run(() =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => MessageBox.Show(message)));
                }
                else
                {
                    MessageBox.Show(message);
                }
            });
        }

        #endregion

        #region ------------ 保存 & 加载系统参数设置 ------------

        DatasModel.SytemSetDerived ssd = new DatasModel.SytemSetDerived();
        DatasModel.DeviceInformation deviceInfo = new DatasModel.DeviceInformation();
        Repository<SytemSetAll> _systemSetAll => new Repository<SytemSetAll>();

        /// <summary>
        /// 保存系统设置参数，保存的内容范围：
        /// 系统设置 -> 初始化设置；
        /// 通用点位设置 -> 读取生产数据点位
        /// </summary>
        private void SaveSystemConfig()
        {
            // PLC的IP，端口号以及设备名称 -> 必填项
            if (txt_IP.Text == String.Empty || txt_port.Text == String.Empty || txtDeviceName.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }

            // 通用点位设置 > 读取生产数据点位
            ssd.StartProductPoint = txtStartPoint.Text;                         // 读取生产数据起始点位
            ssd.SecondProductPoint = txtSecondPoint.Text;                       // 二次读取起始点位
            ssd.TotalProductPoint = txtResultPoint.Text;                        // 总结果点位
            ssd.SecondProductLength = txtSecondLength.Text;              // 二次读取条码的长度
            ssd.EndProductPoint = txtEndPoint.Text;                             // 结束点位

            ssd.StartProductPoint1 = txtStartPoint1.Text;                       // 工位1开始点位
            ssd.EndProductPoint1 = txtEndPoint1.Text;                           // 工位1结束点位
            ssd.TotalProductPoint1 = txtResultPoint1.Text;                      // 工位1结果点位
            ssd.SecondPoint1 = txtSecondPoint1.Text;                            // 二次条码1
            ssd.SecondLength1 = txtSecondLength1.Text;                          // 二次长度1

            ssd.StartProductPoint2 = txtStartPoint2.Text;                       // 工位2开始点位
            ssd.EndProductPoint2 = txtEndPoint2.Text;                           // 工位2结束点位
            ssd.TotalProductPoint2 = txtResultPoint2.Text;                      // 工位2结果点位
            ssd.SecondPoint2 = txtSecondPoint2.Text;                            // 二次条码2
            ssd.SecondLength2 = txtSecondLength2.Text;                          // 二次长度2

            ssd.LRTrigger1 = txt_LR1.Text;                                      // 左右款触发1
            ssd.LRTrigger2 = txt_LR2.Text;                                      // 左右款触发2

            ssd.FixtureValidate = txtFixtureValidata.Text;                      // 触发工装验证
            ssd.FixtureOK = txtFixtureOK.Text;                                  // 工装验证通过
            ssd.FixtureNumber = txtFixtutreNumber.Text;                         // 工装条码
            ssd.FixtureLength = txtFixtureLength.Text;                          // 工装条码长度

            // 功能模块
            ssd.IsSkipBarcodeVerifyLocally = chkBanLocalVerification.Checked;     // 屏蔽本地条码验证 
            ssd.SytemNorifytooling = chkBypassFixtureValidation.Checked;          // 屏蔽本地扫工装
            ssd.SytemHistorCodes = chkBanLocalHistoricalData.Checked;             // 本地历史数据
            ssd.SytemNGCodesData = chkBanNGDataVerify.Checked;                    // 屏蔽本地NG历史记录
            ssd.SytemNoerifbarcodes = chkBanRuleValidation.Checked;               // 屏蔽条码规则验证
            ssd.SytemQRcodNorif = chkBanQRcodeValidation.Checked;                 // 屏蔽本地二维码验证

            ssd.ISAutoGenerateBarcode = chkGenerateBarcode.Checked;               // 自动生成条码
            ssd.AllowUploadContinuously = chkAllowUploadContinuously.Checked;     // 允许重复上传数据
            ssd.IsOrderNumberBinding = chkBindOrderNumber.Checked;                // 绑定工单
            ssd.IsUserBinding = chkUserBinding.Checked;                           // 用户绑定
            ssd.IsAutoExit = chkAutoExit.Checked;                                 // 自动退出
            ssd.IsAutoLaunch = chkAutoLaunch.Checked;                             // 开机自启动
            MesDatas.Services.AutoLaunch.AutoStart(chkAutoLaunch.Checked);

            ssd.SerialNumber = txtSN.Text;                                        // 流水号
            ssd.BarcodeNumber = txtBarcodeNumber.Text;                            // 码号
            ssd.SytemSetnullCoden = txtDefaultStyle.Text;                         // 默认数据显示样式
            ssd.CurrentPLCType = cboConnectType.Text;                             // PLC连接类型

            ssd.IsDoubleStation = chkDoubleStation.Checked; // 双工位
            ssd.IsLeftRight = chkLeftRight.Checked;         // 左右款
            ssd.StationCount = txtStationCount.Text;        // 工位数量

            deviceInfo.DeviceName_English = txtDeviceName_English.Text;                         // 机台英文名
            deviceInfo.DeviceName_Thai = txtDeviceName_Thai.Text;                               // 机台泰文名

            ssd.Save();
            deviceInfo.Save();

            // PLC参数：IP、端口、
            // 本地数据存放路径：DataPath
            // 其他设置：DeviceName、RFIDPort、读卡器设备号、显示宽度
            // 二次读条码、点位集合、名称集合、
            var systemsetList = _systemSetAll.GetList();
            // 从界面控件获取值并设置到实体类属性
            systemsetList[0].IP = txt_IP.Text;          // PLC IP地址
            systemsetList[0].Port = txt_port.Text;      // PLC端口号
            systemsetList[0].DataUrl = lblDataPath.Text; // 数据库路径
            systemsetList[0].DeviceName = txtDeviceName.Text; // 设备名称
            systemsetList[0].wordNo = txtWorkOrder.Text; // 工单号
            systemsetList[0].Workstname = chkReadRecipeId_PLC.Checked ? "True" : "False"; // 实时读取配方号
            systemsetList[0].BoardBeat = txtFixtureBinding.Text; // 工装绑定
            systemsetList[0].faults = cboBarcodeRule.SelectedValue?.ToString() ?? ""; // 条码验证规则
            systemsetList[0].ResultCode = txtDisplayWidth.Text; // 运行界面显示宽度

            systemsetList[0].rfidProt = cmbShowPort.SelectedItem?.ToString() ?? ""; // 读卡器端口
            systemsetList[0].rfidCode = tbxReaderDeviceID.Text; // 读卡器设备号
            systemsetList[0].readBarCode = chkReadBarcodeSecondly.Checked.ToString(); // 二次读取条码
            systemsetList[0].StatisticsCode = txtPointSets.Text; // 点位集合
            systemsetList[0].StatisticsName = txtNameSets.Text; // 名称集合
            systemsetList[0].StatisticsThaiName = txtThaiNameSets.Text; // 泰语名称集合
            systemsetList[0].StatisticsEnglishName = txtEnglishNameSets.Text; // 英语名称集合

            var result = _systemSetAll.Update(systemsetList[0]);
            if (result)
            {
                MessageBox.Show(resources.GetString("PassBtnSave"));
            }
            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("SELECT * FROM [SytemSet] WHERE ID = '1'");

            if (table1.Rows.Count > 0)
            {
            }
            else
            {
                MDBHelper.CreateAccessDatabase(path4);
                MDBHelper.TryCreateAccessTable(path4, "SytemSet", new System.Collections.ArrayList(new object[] { "ID", "IP", "Port", "DataUrl",
                    "DeviceName", "wordNo", "Workstname", "BoardBeat", "faults", "ResultCode", "rfidProt", "rfidCode", "readBarCode", "StatisticsCode", "StatisticsName", "StatisticsThaiName", "StatisticsEnglishName" }));

                DataTable dt = new DataTable("SytemSet");
                DataColumn ID = new DataColumn("ID", typeof(string));
                dt.Columns.Add(ID);
                DataColumn IP = new DataColumn("IP", typeof(string));
                dt.Columns.Add(IP);
                DataColumn Port = new DataColumn("Port", typeof(string));
                dt.Columns.Add(Port);
                DataColumn DataUrl = new DataColumn("DataUrl", typeof(string));
                dt.Columns.Add(DataUrl);
                DataColumn deviceName = new DataColumn("DeviceName", typeof(string));
                dt.Columns.Add(deviceName);
                DataColumn wordNo = new DataColumn("wordNo", typeof(string));
                dt.Columns.Add(wordNo);
                DataColumn readRecipeID = new DataColumn("Workstname", typeof(string));
                dt.Columns.Add(readRecipeID);
                DataColumn fixtureID = new DataColumn("BoardBeat", typeof(string));
                dt.Columns.Add(fixtureID);
                DataColumn defaultsRecipeID = new DataColumn("faults", typeof(string));
                dt.Columns.Add(defaultsRecipeID);
                DataColumn displayWidth = new DataColumn("ResultCode", typeof(string));
                dt.Columns.Add(displayWidth);
                DataColumn rfidProt = new DataColumn("rfidProt", typeof(string));
                dt.Columns.Add(rfidProt);
                DataColumn rfidCode = new DataColumn("rfidCode", typeof(string));
                dt.Columns.Add(rfidCode);
                DataColumn readBarCode = new DataColumn("readBarCode", typeof(string));
                dt.Columns.Add(readBarCode);
                DataColumn StatisticsCode = new DataColumn("StatisticsCode", typeof(string));
                dt.Columns.Add(StatisticsCode);
                DataColumn StatisticsName = new DataColumn("StatisticsName", typeof(string));
                dt.Columns.Add(StatisticsName);
                DataColumn StatisticsThaiName = new DataColumn("StatisticsThaiName", typeof(string));
                dt.Columns.Add(StatisticsThaiName);
                DataColumn StatisticsEnglishName = new DataColumn("StatisticsEnglishName", typeof(string));
                dt.Columns.Add(StatisticsEnglishName);
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                dr[0] = "1";
                dr[1] = txt_IP.Text;
                dr[2] = txt_port.Text;
                dr[3] = lblDataPath.Text;
                dr[4] = txtDeviceName.Text;
                dr[5] = txtWorkOrder.Text; // 工单号
                dr[6] = chkReadRecipeId_PLC.Checked.ToString(); // 实时读取配方号
                dr[7] = txtFixtureBinding.Text; // 工装绑定
                dr[8] = cboBarcodeRule.SelectedIndex.ToString(); // 默认配方号
                dr[9] = txtDisplayWidth.Text; // 显示宽度
                dr[10] = cmbShowPort.Text; // RFID端口
                dr[11] = tbxReaderDeviceID.Text; // 读卡器设备号
                dr[12] = chkReadBarcodeSecondly.Checked.ToString(); // 二次读取条码
                dr[13] = txtPointSets.Text; // 点位集合
                dr[14] = txtNameSets.Text; // 名称集合
                dr[15] = txtThaiNameSets.Text; // 泰语名称集合
                dr[16] = txtEnglishNameSets.Text; // 英语名称集合
                dbHelper.DatatableToMdb("SytemSet", dt);
            }

            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 加载系统配置参数
        /// </summary>
        private void LoadSystemConfig()
        {
            ssd = DatasServer.SytemSetDerivedServer.GetSytemSetDerived(1);
            deviceInfo = DatasServer.DeviceInformationServer.GetDeviceInformation(1);

            // 屏蔽
            chkBanLocalVerification.Checked = ssd.IsSkipBarcodeVerifyLocally; // 屏蔽本地条码验证
            chkBypassFixtureValidation.Checked = ssd.SytemNorifytooling;      // 屏蔽本地扫工装
            chkBanLocalHistoricalData.Checked = ssd.SytemHistorCodes;         // 屏蔽本地历史数据
            chkBanNGDataVerify.Checked = ssd.SytemNGCodesData;                // 屏蔽本地NG历史数据
            chkBanRuleValidation.Checked = ssd.SytemNoerifbarcodes;     // 屏蔽条码规则验证
            chkBanQRcodeValidation.Checked = ssd.SytemQRcodNorif;             // 屏蔽本地二维码验证

            chkGenerateBarcode.Checked = ssd.ISAutoGenerateBarcode;           // 自动生成条码
            chkAllowUploadContinuously.Checked = ssd.AllowUploadContinuously; // 允许重复上传数据
            chkBindOrderNumber.Checked = ssd.IsOrderNumberBinding;            // 绑定工单
            chkUserBinding.Checked = ssd.IsUserBinding;                       // 用户绑定
            chkAutoLaunch.Checked = ssd.IsAutoLaunch;                         // 开机自启动
            chkAutoExit.Checked = ssd.IsAutoExit;                             // 自动退出

            txtSN.Text = ssd.SerialNumber;                                    // 流水号
            txtBarcodeNumber.Text = ssd.BarcodeNumber;                        // 码号

            cboConnectType.Text = ssd.CurrentPLCType;                         // 当前PLC连接类型
            txtDefaultStyle.Text = ssd.SytemSetnullCoden;                     // 默认数据显示样式

            // 读取生产数据点位
            txtStartPoint.Text = ssd.StartProductPoint;                       // 开始读取点位：D1200
            txtSecondPoint.Text = ssd.SecondProductPoint;                     // 二次读取点位：D1050
            txtSecondLength.Text = ssd.SecondProductLength;                   // 二次读取长度：10
            txtEndPoint.Text = ssd.EndProductPoint;                           // 结束生产点位：D1202
            txtResultPoint.Text = ssd.TotalProductPoint;                      // 产品结果点位：D1078

            txtStartPoint1.Text = ssd.StartProductPoint1;                     // 工位1开始点位
            txtEndPoint1.Text = ssd.EndProductPoint1;                         // 工位1结束点位
            txtResultPoint1.Text = ssd.TotalProductPoint1;                    // 工位1结果点位
            txtSecondPoint1.Text = ssd.SecondPoint1;                          // 二次条码1
            txtSecondLength1.Text = ssd.SecondLength1;                        // 二次长度1

            txtStartPoint2.Text = ssd.StartProductPoint2;                     // 工位2开始点位
            txtEndPoint2.Text = ssd.EndProductPoint2;                         // 工位2结束点位
            txtResultPoint2.Text = ssd.TotalProductPoint2;                    // 工位2结果点位
            txtSecondPoint2.Text = ssd.SecondPoint2;                          // 二次条码2
            txtSecondLength2.Text = ssd.SecondLength2;                        // 二次长度1

            txt_LR1.Text = ssd.LRTrigger1;                                    // 左右款触发1
            txt_LR2.Text = ssd.LRTrigger2;                                    // 左右款触发2

            txtFixtureValidata.Text = ssd.FixtureValidate;                    // 触发工装验证
            txtFixtureOK.Text = ssd.FixtureOK;                                // 工装验证通过
            txtFixtutreNumber.Text = ssd.FixtureNumber;                       // 工装条码
            txtFixtureLength.Text = ssd.FixtureLength;                        // 工装条码长度

            // 设备产品点位
            txtDeviceStatePoint.Text = deviceInfo.DeviceStatusPoint;    // 设备状态点位：D1007
            txtProductModelPoint.Text = deviceInfo.ProductModelPoint;   // 产品型号点位：D1120
            txtPMLength.Text = deviceInfo.ProductModelLength;           // 产品型号长度：10
            txtRecipeIdPoint.Text = deviceInfo.RecipeIdPoint;           // 配方号点位：D1208
            textBox43.Text = deviceInfo.ModifyRecipePoint;              // 配方修改：D1204
            textBox47.Text = deviceInfo.ModifyRecipeIDPoint;            // 配方号修改：D1206
            textBox35.Text = deviceInfo.EndNFCPoint;                    // 结束NFC
            txtViewStatus.Text = deviceInfo.DashboardStatusPoint;       // 看板状态

            txtDeviceName_English.Text = deviceInfo.DeviceName_English; // 机台英文名
            txtDeviceName_Thai.Text = deviceInfo.DeviceName_Thai;       // 机台泰文名
            txtStationNameSets_English.Text = deviceInfo.StationNameSets_English;   // 工位名称_英文
            txtStationNameSets_Thai.Text = deviceInfo.StationNameSets_Thai;         // 工位名称_泰文

            chkDoubleStation.Checked = ssd.IsDoubleStation;             // 双工位
            chkLeftRight.Checked = ssd.IsLeftRight;                     // 左右款
            txtStationCount.Text = ssd.StationCount;                    // 工位数量

            var systemsetList = _systemSetAll.GetList();
            txt_IP.Text = systemsetList[0].IP;                      // PLC IP地址
            txt_port.Text = systemsetList[0].Port;                  // PLC端口号
            lblDataPath.Text = systemsetList[0].DataUrl;            // 数据库路径
            txtDeviceName.Text = systemsetList[0].DeviceName;       // 设备名称
            txtWorkOrder.Text = systemsetList[0].wordNo;            // 工单号
            string isReadPlc = systemsetList[0].Workstname;         // 实时读取配方号
            if (isReadPlc == "True")
            {
                chkReadRecipeId_PLC.Checked = true; // 如果是实时读取配方号，则勾选
            }
            txtFixtureBinding.Text = systemsetList[0].BoardBeat;                        // 工装绑定
            cboBarcodeRule.SelectedValue = systemsetList[0].faults;                     // 当前配方号
            lblRecipeId.Text = recipeId = systemsetList[0].faults;
            txtDisplayWidth.Text = systemsetList[0].ResultCode;                         // 运行界面显示宽度

            cmbShowPort.SelectedItem = systemsetList[0].rfidProt;                       // 读卡器端口
            tbxReaderDeviceID.Text = systemsetList[0].rfidCode;                         // 读卡器设备号
            chkReadBarcodeSecondly.Checked = bool.Parse(systemsetList[0].readBarCode);  // 二次读取条码
            txtPointSets.Text = systemsetList[0].StatisticsCode;                        // 点位集合
            txtNameSets.Text = systemsetList[0].StatisticsName;                         // 名称集合
            txtThaiNameSets.Text = systemsetList[0].StatisticsThaiName;                 // 泰语名称集合
            txtEnglishNameSets.Text = systemsetList[0].StatisticsEnglishName;           // 英语名称集合
        }

        /// <summary>
        /// 系统设置页面 > 通用点位设置页 > 保存按钮
        /// </summary>
        private void PLCPointSaveBtn_Click(object sender, EventArgs e)
        {
            SaveSystemConfig();

            deviceInfo.DeviceStatusPoint = txtDeviceStatePoint.Text;
            deviceInfo.ProductModelPoint = txtProductModelPoint.Text;
            deviceInfo.ProductModelLength = txtPMLength.Text;
            deviceInfo.RecipeIdPoint = txtRecipeIdPoint.Text;
            deviceInfo.ModifyRecipePoint = textBox43.Text;
            deviceInfo.ModifyRecipeIDPoint = textBox47.Text;
            deviceInfo.EndNFCPoint = textBox35.Text;
            deviceInfo.DashboardStatusPoint = txtViewStatus.Text;
            deviceInfo.Save();
            loggerConfig.Trace($"通用点位设置保存成功");
        }

        /// <summary>
        /// 系统设置页面的保存按钮
        /// </summary>
        private void BtnSaveAtSystemSetting_Click(object sender, EventArgs e)
        {
            SaveSystemConfig();
            loggerConfig.Trace($"系统设置参数保存成功");
        }

        /// <summary>
        /// 配方设置界面的保存按钮
        /// </summary>
        private void btnSaveRecipeConfig_Click(object sender, EventArgs e)
        {
            SaveSystemConfig();
            loggerConfig.Trace($"名称集合保存成功！\n点位集合保存成功！");
        }

        #endregion

        #region ------------ 测试数据的读取、绑定 ------------

        DataTable maxminTable = null;
        public List<TestItemData> ValueList = null;

        /// <summary>
        /// 实时读取上限值
        /// </summary>
        public async Task Get_MaxMinValue()
        {
            while (isReadMaxMin_PLC)
            {
                await Task.Run(() => GetPLCMaxMin());
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// 读取测试数据
        /// </summary>
        private async Task GetPLCMaxMin()
        {
            if (!isPlcConnected) return;

            ValueList = new List<TestItemData>();
            for (int i = 0; i < PLCPointInfoTable.Rows.Count; i++)
            {
                TestItemData value = new TestItemData();

                value.ItemName = PLCPointInfoTable.Rows[i]["BoardName"].ToString();
                value.StandardValue = NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["StandardCode"].ToString()));
                value.MaxValue = NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString()));
                value.MinValue = NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString()));
                value.ActualValue = NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["BoardCode"].ToString()));
                value.Result = NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString()));
                ValueList.Add(value);
            }
        }

        /// <summary>
        /// 实时更新上下限数据
        /// </summary>
        public async Task UpdateTestResultRealtime()
        {
            while (isReadMaxMin_PLC)
            {
                await Task.Run(() => BindTestResult());
                await Task.Delay(1000);
            }
        }

        private void InitMaxMinDataTable()
        {
            for (int i = 0; i < 7; i++)
            {
                dgvTest.Columns.Add(new DataGridViewTextBoxColumn());
            }

            dgvTest.Columns[0].HeaderText = resources.GetString("d4NO");    // 序号
            dgvTest.Columns[1].HeaderText = resources.GetString("d4CSXM");  // 测试项目
            dgvTest.Columns[2].HeaderText = resources.GetString("d4BZZ");   // 标准值
            dgvTest.Columns[3].HeaderText = resources.GetString("d4SXZ");   // 上限值
            dgvTest.Columns[4].HeaderText = resources.GetString("d4XXZ");   // 下限值
            dgvTest.Columns[5].HeaderText = resources.GetString("d4SJZ");   // 测试值
            dgvTest.Columns[6].HeaderText = resources.GetString("d4CSJG");  // 测试结果

            //  自适应列宽
            dgvTest.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvTest.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            for (int i = 0; i < PLCPointInfoTable.Rows.Count; i++)
            {
                string testItemName = PLCPointInfoTable.Rows[i]["BoardName"].ToString();
                string standardValue = PLCPointInfoTable.Rows[i]["StandardCode"].ToString();
                string maxValue = PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString();
                string minValue = PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString();
                string realValue = PLCPointInfoTable.Rows[i]["BoardCode"].ToString();
                string testResult = PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString();

                // 添加新行
                int rowIndex = dgvTest.Rows.Add();

                // 设置行数据
                dgvTest.Rows[rowIndex].Cells[0].Value = (i + 1).ToString(); // 序号
                dgvTest.Rows[rowIndex].Cells[1].Value = testItemName;
                dgvTest.Rows[rowIndex].Cells[2].Value = standardValue;
                dgvTest.Rows[rowIndex].Cells[3].Value = maxValue;
                dgvTest.Rows[rowIndex].Cells[4].Value = minValue;
                dgvTest.Rows[rowIndex].Cells[5].Value = realValue;
                dgvTest.Rows[rowIndex].Cells[6].Value = testResult;
            }

            // 禁用列排序
            for (int i = 0; i < dgvTest.Columns.Count; i++)
            {
                dgvTest.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BindTestResult()
        {
            BeginInvoke(new Action(() =>
            {
                if (!isPlcConnected) return;
                List<TestItemData> maxMinList = this.ValueList;
                if (PLCPointInfoTable.Rows.Count > 0 && dgvTest.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvTest.Rows.Count; i++)
                    {
                        string testItemName = PLCPointInfoTable.Rows[i]["BoardName"].ToString();

                        if (maxMinList != null && maxMinList.Count == dgvTest.Rows.Count && maxMinList.Count > 0)
                        {
                            if (maxMinList[i].ItemName == testItemName)
                            {
                                dgvTest.Rows[i].Cells[0].Value = i + 1;
                                dgvTest.Rows[i].Cells[1].Value = GetTestItemName(GetIndexByName(testItemName));
                                dgvTest.Rows[i].Cells[2].Value = maxMinList[i].StandardValue.ToString();
                                dgvTest.Rows[i].Cells[3].Value = maxMinList[i].MaxValue.ToString();
                                dgvTest.Rows[i].Cells[4].Value = maxMinList[i].MinValue.ToString();
                                dgvTest.Rows[i].Cells[5].Value = maxMinList[i].ActualValue.ToString();

                                string actualValue = maxMinList[i].ActualValue.ToString();
                                if (actualValue == "NG" || actualValue == "OK")
                                {
                                    // 如果实际值为OK/NG，则同步结果至测试结果所在列
                                    dgvTest.Rows[i].Cells[6].Value = actualValue;
                                }
                                else
                                {
                                    dgvTest.Rows[i].Cells[6].Value = maxMinList[i].Result.ToString();
                                }

                                string testResult = dgvTest.Rows[i].Cells[6].Value.ToString();
                                if (testResult.Equals("OK"))
                                {
                                    dgvTest.Rows[i].Cells[6].Style.BackColor = Color.Green;
                                }
                                else if (testResult.Equals("NG"))
                                {
                                    dgvTest.Rows[i].Cells[6].Style.BackColor = Color.Red;
                                }
                                else
                                {
                                    dgvTest.Rows[i].Cells[6].Style.BackColor = Color.White;
                                }
                            }
                        }
                    }
                }
            }));
        }

        public int GetIndexByName(string name)
        {
            if (testItemsName_Chinese == null || string.IsNullOrEmpty(name))
                return -1; // 数组为空或名称为空时返回-1

            for (int i = 0; i < testItemsName_Chinese.Length; i++)
            {
                if (string.Equals(testItemsName_Chinese[i], name, StringComparison.OrdinalIgnoreCase))
                    return i; // 找到匹配项，返回索引
            }

            return -1; // 未找到匹配项
        }

        /// <summary>
        /// 读取 PLC 值并根据指定的格式返回结果。
        /// </summary>
        /// <param name="plcPointInfo"></param>
        /// <returns></returns>
        private static string ProcessPointData_PLC1(string plcPointInfo)
        {
            string outputStr = "";

            if (plcPointInfo.Equals("NO"))
            {
                outputStr = "null";
            }

            else if (plcPointInfo.Contains(":"))
            {
                // 提取索引标志，点位信息填写模板：D1090:H-3 D1080:I-5
                int index = plcPointInfo.IndexOf(":");
                int index1 = plcPointInfo.IndexOf("-");

                // 提取类型部分，包括：H-short，I-Int32，F-float，J-Int32，N-Int32，O-Int32，S-string
                string dataType = plcPointInfo.Substring(index + 1, 1);

                // 提取数值部分
                ushort number = 0;
                ushort.TryParse(plcPointInfo.Substring(index1 + 1), out number);

                // 提取PLC地址
                string plcAddress = plcPointInfo.Substring(0, index);

                if (dataType == "H")
                {
                    string aa = "";
                    OperateResult<short> result = readWriteNet.ReadInt16(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.NumericOperate(result.Content.ToString(), number.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }        // short

                else if (dataType == "I")
                {
                    string aa = "";
                    OperateResult<int> result = readWriteNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.NumericOperate(result.Content.ToString(), number.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "F")
                {
                    string aa = "";
                    OperateResult<float> result = readWriteNet.ReadFloat(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = TypeRead.NumericOperate(result.Content.ToString(), number.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // float

                else if (dataType == "J")
                {
                    string aa = "";
                    OperateResult<int> result = readWriteNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {

                        aa = CodeNum.DivBy100Rounded2(result.Content.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "N")
                {
                    string aa = "";
                    OperateResult<int> result = readWriteNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = CodeNum.ConvertToOkNg(result.Content.ToString());
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "O")
                {
                    string aa = "";
                    OperateResult<int> result = readWriteNet.ReadInt32(plcAddress);

                    if (result.IsSuccess)
                    {
                        aa = result.Content.ToString();
                    }
                    else
                    {
                        aa = "null";
                    }
                    outputStr = aa;

                }   // Int32

                else if (dataType == "S")
                {
                    string ss = "";
                    OperateResult<string> result = readWriteNet.ReadString(plcAddress, number);

                    if (result.IsSuccess)
                    {
                        ss = CodeNum.CleanString(result.Content.ToString());
                    }
                    else
                    {
                        ss = "null";
                    }
                    outputStr = ss;

                }   // string

                else
                {
                    var ss = readWriteNet.ReadString(plcAddress, number).Content;
                    outputStr = ss;
                }                        // string
            }

            else
            {
                string aa = "";
                OperateResult<int> readss = readWriteNet.ReadInt32(plcPointInfo);
                if (readss.IsSuccess)
                {

                    aa = CodeNum.DivBy100(readss.Content.ToString());
                }
                else
                {
                    aa = "null";
                }
                outputStr = aa;
            }

            return outputStr;
        }

        private static async Task<string> ProcessPointData_PLC(string plcPointInfo)
        {
            if (plcPointInfo.Equals("NO"))
            {
                return "null";
            }

            if (plcPointInfo.Contains(":"))
            {
                // ... 现有解析逻辑不变 ...
                int index = plcPointInfo.IndexOf(":");
                int index1 = plcPointInfo.IndexOf("-");
                string dataType = plcPointInfo.Substring(index + 1, 1);
                ushort.TryParse(plcPointInfo.Substring(index1 + 1), out ushort number);
                string plcAddress = plcPointInfo.Substring(0, index);

                try
                {
                    switch (dataType)
                    {
                        case "H": // short
                            var resultH = await readWriteNet.ReadInt16Async(plcAddress);
                            return resultH.IsSuccess ? TypeRead.NumericOperate(resultH.Content.ToString(), number.ToString()) : "null";
                        case "I": // Int32
                            var resultI = await readWriteNet.ReadInt32Async(plcAddress);
                            return resultI.IsSuccess ? TypeRead.NumericOperate(resultI.Content.ToString(), number.ToString()) : "null";
                        case "F": // float
                            var resultF = await readWriteNet.ReadFloatAsync(plcAddress);
                            return resultF.IsSuccess ? TypeRead.NumericOperate(resultF.Content.ToString(), number.ToString()) : "null";
                        case "J": // Int32
                            var resultJ = await readWriteNet.ReadInt32Async(plcAddress);
                            return resultJ.IsSuccess ? CodeNum.DivBy100Rounded2(resultJ.Content.ToString()) : "null";
                        case "N": // Int32
                            var resultN = await readWriteNet.ReadInt32Async(plcAddress);
                            return resultN.IsSuccess ? CodeNum.ConvertToOkNg(resultN.Content.ToString()) : "null";
                        case "O": // Int32
                            var resultO = await readWriteNet.ReadInt32Async(plcAddress);
                            return resultO.IsSuccess ? resultO.Content.ToString() : "null";
                        case "S": // string
                            var resultS = await readWriteNet.ReadStringAsync(plcAddress, number);
                            return resultS.IsSuccess ? CodeNum.CleanString(resultS.Content.ToString()) : "null";
                        default:
                            var resultDefault = await readWriteNet.ReadStringAsync(plcAddress, number);
                            return resultDefault.IsSuccess ? resultDefault.Content : "null";
                    }
                }
                catch
                {
                    return "null"; // 异常时返回 "null"
                }
            }
            else
            {
                var readss = await readWriteNet.ReadInt32Async(plcPointInfo);
                return readss.IsSuccess ? CodeNum.DivBy100(readss.Content.ToString()) : "null";
            }
        }

        public string NullModify(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                str = ssd.SytemSetnullCoden;
            }
            if (str.Equals("null"))
            {
                str = ssd.SytemSetnullCoden;
            }
            return str;
        }

        #endregion

        #region  ------------ 设备运行状态、配方更改、生产指标读取 ------------

        List<string> kpiList = null;
        /// <summary>
        /// KPIS：Key Performance Indicators 关键性能指标，生产指标
        /// </summary>
        DataTable KPIsTable = null;

        private CancellationTokenSource _cts;
        private readonly object _lockObject = new object();

        private async Task InitializeModelReadAsync()
        {
            // 获取用户输入
            deviceInfo.DeviceStatusPoint = txtDeviceStatePoint.Text;
            deviceInfo.ProductModelPoint = txtProductModelPoint.Text;
            deviceInfo.ProductModelLength = txtPMLength.Text;
            deviceInfo.Save();

            _cts = new CancellationTokenSource();

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    await ReadModelAsync_PLC(_cts.Token);
                    await Task.Delay(250);
                }
            }
            catch (OperationCanceledException)
            {
                // 任务被取消，正常退出
            }
            catch (Exception ex)
            {
                await ShowErrorMessageAsync($"读取 PLC 数据时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 读取设备运行状态、产品型号、配方号、生产指标、更新UI
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task ReadModelAsync_PLC(CancellationToken token)
        {
            if (!isPlcConnected) return;

            try
            {
                // 读取设备运行状态
                deviceState = await ReadPlcValueAsync(deviceInfo.DeviceStatusPoint, token);

                // 从PLC读取配方号、产品型号
                if (chkReadRecipeId_PLC.Checked)
                {
                    // 读取配方号
                    string currentId = await ReadPlcValueAsync(deviceInfo.RecipeIdPoint, CancellationToken.None);

                    // 检查配方是否变更
                    if (recipeId != currentId)
                    {
                        // 清除工装绑定
                        txtFixtureBinding.Text = string.Empty;

                        // 配方变更，触发对应提示
                        await HandleRecipeChange(currentId);

                        // 原有的UI更新逻辑
                        cboBarcodeRule.SelectedValue = currentId;
                        recipeId = lblRecipeId.Text = currentId;

                        // 保存最新配方号到数据库
                        try
                        {
                            dbHelper = new MDBHelper(path4);
                            string sql = $"update [SytemSet] set [faults]='{recipeId}' where [ID] = '1'";
                            if (dbHelper.Change(sql))
                            {
                                DisplayMessage("配方号更新，并成功保存到数据库");
                            }
                            else
                            {
                                DisplayMessage("配方号更新，保存到数据库失败");
                            }
                        }
                        finally
                        {
                            dbHelper.CloseConnection();
                        }
                    }

                    // 更新配方相关的UI
                    UpdateRecipeRelatedUI(currentId);
                }

                // 处理工装条码读取（如果需要工装验证）
                await ProcessFixtureBarcodeFromPLC();

                // 读取 KPI 数据
                kpiList = await ReadKpiDataAsync(token);

                // 更新设备运行状态和生产指标
                await UpdateUIAsync(token);
            }
            catch (Exception ex)
            {
                await ShowErrorMessageAsync($"读取 PLC 数据时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 根据Kpi点位集合，读取相应点位的数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<List<string>> ReadKpiDataAsync(CancellationToken token)
        {
            var kpiList = new List<string>();
            orderNumber = txtWorkOrder.Text;

            // If work order binding is enabled, get data from MES
            if (chkBindOrderNumber.Checked && !string.IsNullOrEmpty(orderNumber) && LoginMode == "联机")
            {
                var orderInfo = await MesIntegrationService.FetchOrderNumberInfo(orderNumber);
                orderQuantity = orderInfo.orderQuantity;
                completedQuantity = orderInfo.completedQuantity;
                completeRate = orderInfo.completeRate;
            }

            if (kpisPointSets.Length > 0)
            {
                // 【安全修复】始终以点位数量为循环基准
                // 循环次数严格基于点位数量，防止越界
                for (int i = 0; i < kpisPointSets.Length; i++)
                {
                    if (token.IsCancellationRequested) break;

                    string pointSet = kpisPointSets[i];
                    //string kpiName = langArray[i];
                    // 【关键修复】这里千万不要用 langArray[i]！
                    // 1. 防止切换语言时数组瞬间变化导致越界
                    // 2. 确保 "工单数量" 等逻辑判断始终有效（基于中文名）
                    string kpiName = (i < kpisNameSets.Length) ? kpisNameSets[i] : $"Unknown_{i}";
                    string kpiValue = string.Empty;

                    // Check if this KPI should use MES data
                    if (chkBindOrderNumber.Checked && !string.IsNullOrEmpty(orderNumber))
                    {
                        // Map specific KPIs to MES data
                        if (kpiName.Contains("工单数量") && !string.IsNullOrEmpty(orderQuantity))
                        {
                            kpiValue = orderQuantity;
                            kpiList.Add(kpiValue);
                            continue;
                        }
                        else if (kpiName.Contains("完成数量") && !string.IsNullOrEmpty(completedQuantity))
                        {
                            kpiValue = completedQuantity;
                            kpiList.Add(kpiValue);
                            continue;
                        }
                        else if (kpiName.Contains("完成率") && !string.IsNullOrEmpty(completeRate))
                        {
                            kpiValue = completeRate;
                            kpiList.Add(kpiValue);
                            continue;
                        }
                    }

                    // If not replaced by MES data, read from PLC as usual
                    if (pointSet.Contains("-"))
                    {
                        kpiValue = await ProcessPointDataAsync(pointSet, token);
                    }
                    else
                    {
                        int index = pointSet.IndexOf(":");
                        string dataType = pointSet.Substring(index + 1, 1);
                        string plcAddress = pointSet.Substring(0, index);

                        // Read PLC point data
                        string rawData = await ReadPlcValueAsync(plcAddress, token);

                        // Process the data according to dataType
                        kpiValue = CodeNum.HandlePlcData(rawData, dataType);
                    }

                    kpiList.Add(kpiValue);
                }
            }

            // Map KPI list to class fields after all values have been collected
            MapKpiListToClassFields(kpiList);

            return kpiList;
        }

        private void MapKpiListToClassFields(List<string> kpiList)
        {
            // Only process if we have KPI data
            //if (kpiList == null || kpiList.Count == 0 || langArray.Length == 0)
            // 【安全修复】增加非空检查
            if (kpiList == null || kpiList.Count == 0 || kpisNameSets == null || kpisNameSets.Length == 0)
                return;

            // 【关键修复】循环次数取两者的最小值，确保绝对安全
            int loopCount = Math.Min(kpiList.Count, kpisNameSets.Length);

            // Map each KPI to its corresponding class field based on the name
            for (int i = 0; i < loopCount; i++)
            {
                //string kpiName = langArray[i];
                //string kpiValue = kpiList[i];
                // 始终使用中文名称进行关键词匹配
                string kpiName = kpisNameSets[i];
                string kpiValue = kpiList[i];

                // Skip if value is empty or null
                if (string.IsNullOrWhiteSpace(kpiValue))
                    continue;

                // Map to the appropriate class field based on the KPI name
                if (kpiName.Contains("工单数量"))
                {
                    // Only update if not already set by MES
                    if (chkBindOrderNumber.Checked && !string.IsNullOrEmpty(orderQuantity))
                        continue;
                    orderQuantity = kpiValue;
                }
                else if (kpiName.Contains("完成数量"))
                {
                    // Only update if not already set by MES
                    if (chkBindOrderNumber.Checked && !string.IsNullOrEmpty(completedQuantity))
                        continue;
                    completedQuantity = kpiValue;
                }
                else if (kpiName.Contains("合格数量"))
                {
                    passQuantity = kpiValue;
                }
                else if (kpiName.Contains("NG数量"))
                {
                    NGQuantity = kpiValue;
                }
                else if (kpiName.Contains("生产总数"))
                {
                    totalQuantity = kpiValue;
                }
                else if (kpiName.Contains("完成率"))
                {
                    // Only update if not already set by MES
                    if (chkBindOrderNumber.Checked && !string.IsNullOrEmpty(completeRate))
                        continue;
                    completeRate = kpiValue;
                }
                else if (kpiName.Contains("合格率"))
                {
                    passRate = kpiValue;
                }
                else if (kpiName.Contains("不良率"))
                {
                    failRate = kpiValue;
                }
                else if (kpiName.Contains("直通率"))
                {
                    FPY = kpiValue;
                }
                else if (kpiName.Contains("生产节拍"))
                {
                    productionCycleTime = kpiValue;
                }
                else if (kpiName.Contains("整体节拍"))
                {
                    overallCycleTime = kpiValue;
                }
                else if (kpiName.Contains("机械节拍"))
                {
                    machineCycleTime = kpiValue;
                }
                else if (kpiName.Contains("人工节拍"))
                {
                    manualCycleTime = kpiValue;
                }
                else if (kpiName.Contains("保养计数"))
                {
                    maintenanceCount = kpiValue;
                }
                else if (kpiName.Contains("工序时间"))
                {
                    processTime = kpiValue;
                }
                else if (kpiName.Contains("利用时间"))
                {
                    usingTime = kpiValue;
                }
                else if (kpiName.Contains("负荷时间"))
                {
                    loadTime = kpiValue;
                }
            }
        }

        /// <summary>
        /// ReadInt32
        /// </summary>
        private async Task<string> ReadPlcValueAsync(string address, CancellationToken token)
        {
            return await Task.Run(() => readWriteNet.ReadInt32(address).Content.ToString(), token);
        }

        private async Task<string> ReadPlcStringAsync(string address, ushort length, CancellationToken token)
        {
            return await Task.Run(() => readWriteNet.ReadString(address, length).Content, token);
        }

        private Task<string> ProcessPointDataAsync(string pointSet, CancellationToken token)
        {
            return Task.Run(() => ProcessPointData_PLC(pointSet), token);
        }

        private async Task UpdateUIAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested) return;

            await this.InvokeAsync(() =>
            {
                // 更新设备状态
                UpdateDeviceStatus();

                // 驱动打印 > 从PLC读取产品型号
                if (chkLoadModel.Checked)
                {
                    txtModel.Text = txtProductModel.Text;
                }

                // 更新 KPI 数据表格
                UpdateKpiTable();
            });
        }

        /// <summary>
        /// 更新设备运行状态
        /// </summary>
        private void UpdateDeviceStatus()
        {
            if (deviceState == null) return;

            switch (deviceState)
            {
                case "1":   // 运行
                    lblDeviceStatus.ForeColor = Color.Green;
                    EndFaults();
                    break;
                case "2":   // 故障不停机
                    lblDeviceStatus.ForeColor = Color.Red;
                    SendFaultInfo("触发故障");
                    break;
                case "3":   // 故障停机
                    lblDeviceStatus.ForeColor = Color.Red;
                    SendFaultInfo("故障停机");
                    break;
                case "4":   // 待机
                    lblDeviceStatus.ForeColor = Color.Orange;
                    EndFaults();
                    break;
                default:
                    lblDeviceStatus.ForeColor = Color.Black;
                    break;
            }
        }

        private void UpdateKpiTable()
        {
            if (kpisNameSets.Length == kpisPointSets.Length && kpisPointSets.Length > 0)
            {
                if (KPIsTable == null)
                {
                    InitializeKpiTable();
                }
                else
                {
                    UpdateExistingKpiTable();
                }
            }
        }

        private void InitializeKpiTable()
        {
            GetInLaguageArray();    // 获取当前语言数组 (langArray)

            // 安全检查
            if (langArray == null) return;

            KPIsTable = new DataTable();
            KPIsTable.Columns.Add(resources.GetString("d3Name"), typeof(string));
            KPIsTable.Columns.Add(resources.GetString("d3Value"), typeof(string));

            // 【关键修复】行数取 语言名称 与 数据列表 的最小值
            // 即使泰语配置了100行，但PLC数据只有5个，这里也只会循环5次，绝不越界
            /*for (int i = 0; i < langArray.Length; i++)
            {
                DataRow dr = KPIsTable.NewRow();
                dr[resources.GetString("d3Name")] = langArray[i];
                dr[resources.GetString("d3Value")] = kpiList[i];
                KPIsTable.Rows.Add(dr);
            }*/
            int rowCount = Math.Min(langArray.Length, kpiList?.Count ?? 0);
            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = KPIsTable.NewRow();
                dr[resources.GetString("d3Name")] = langArray[i];
                dr[resources.GetString("d3Value")] = kpiList[i]; // i 永远在 kpiList 范围内
                KPIsTable.Rows.Add(dr);
            }

            dgvProductionIndex.DataSource = KPIsTable;

            for (int i = 0; i < dgvProductionIndex.Columns.Count; i++)
            {
                dgvProductionIndex.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void UpdateExistingKpiTable()
        {
            // 安全检查
            if (langArray == null || kpiList == null) return;

            // 【关键修复】循环次数取两者的最小值
            int rowCount = Math.Min(langArray.Length, kpiList.Count);

            // 双重保险：不要超过表格现有的行数
            rowCount = Math.Min(rowCount, dgvProductionIndex.Rows.Count);

            for (int i = 0; i < rowCount; i++)
            {
                // 更新名称 (处理语言切换)
                dgvProductionIndex.Rows[i].Cells[0].Value = langArray[i];

                // 更新数值
                dgvProductionIndex.Rows[i].Cells[1].Value = kpiList[i]; // 绝对安全
            }

            /*for (int i = 0; i < langArray.Length; i++)
            {
                dgvProductionIndex.Rows[i].Cells[0].Value = langArray[i];
                dgvProductionIndex.Rows[i].Cells[1].Value = kpiList[i];
            }*/
        }

        private Task ShowErrorMessageAsync(string message)
        {
            return this.InvokeAsync(() => MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        private short[] faultTime = new short[] { };

        /// <summary>
        /// 触发发送设备故障信息
        /// </summary>
        /// <param name="status"></param>
        public void SendFaultInfo(string status)
        {
            Invoke(new Action(() =>
            {
                if (!isDashboardConnected) return;

                if (faultTime != null)
                {
                    for (int i = 0; i < faultTime.Length; i++)
                    {
                        if (faultsTable.Rows.Count > i)
                        {
                            ///编号
                            string faultID = faultsTable.Rows[i]["编号"].ToString();
                            if (faultTime[i] == 1)
                            {
                                if (!faultsMap.ContainsKey(faultID))
                                {
                                    string statssName = CodeNum.GetStationNameByID(faultsTable.Rows[i]["工位ID"].ToString(), stationNameSets); //
                                    string faultas = "1+" + statssName + "+" + txtDeviceName.Text + "+" + status + "+"
                                     + faultsTable.Rows[i]["故障描述"] + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    DisplayDashboardMessage(faultas);
                                    SendAsync(faultas);
                                    faultsMap.Add(faultID, faultas);
                                }
                            }
                            else if (faultTime[i] == 0)
                            {
                                string faulvor = "";
                                if (faultsMap.TryGetValue(faultID, out faulvor))
                                {
                                    if (!string.IsNullOrWhiteSpace(faulvor))
                                    {
                                        string shofault = faulvor + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        DisplayDashboardMessage(shofault);
                                        SendAsync(shofault);
                                        faultsMap.Remove(faultID);
                                    }
                                }
                            }
                            try
                            {
                                string jsonStr = JsonConvert.SerializeObject(faultsMap);
                                Exception exception = new Exception(jsonStr);
                                //devicelog.Trace(exception, status);
                                File.WriteAllText(pathText, jsonStr);
                            }
                            catch { }
                        }
                    }
                }
            }));
        }

        /// <summary>
        /// 结束故障结束时候触发
        /// </summary>
        public void EndFaults()
        {
            Invoke(new Action(() =>
            {
                if (isDashboardConnected)
                {
                    if (faultsMap.Count > 0 & faultsMap != null)
                    {
                        try
                        {
                            File.WriteAllText(pathText, "{}");
                            foreach (var item in faultsMap)
                            {
                                //输出
                                //1+故障所在工位+机台名称+设备状态+
                                //故障的描述+触发故障的结束时间 
                                string shofault = item.Value + "+" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                DisplayDashboardMessage(shofault);
                                SendAsync(shofault);
                                faultsMap.Remove(item.Key);
                                if (faultsMap.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }));
        }

        #endregion

        #region ------------ 显示列表 ------------

        private int Num1 = 0;
        private int Num2 = 0;
        private string uploadState;

        /// <summary>
        /// 创建列表表头（重构版本）
        /// </summary>
        /// <param name="gridView">DataGridView控件</param>
        /// <param name="stationToken">工位标识</param>
        /// <param name="isEnableMutiStation">是否启用多工位</param>
        private void CreateHeaderText(DataGridView gridView, string stationToken = null, bool isEnableMutiStation = false)
        {
            // 清除现有列
            if (gridView.Columns.Count > 0)
            {
                gridView.Columns.Clear();
            }

            // 获取列结构
            var columnStructure = GetColumnStructure(stationToken, isEnableMutiStation);

            // 创建列
            foreach (var columnInfo in columnStructure)
            {
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = GenerateColumnHeaderText(columnInfo);

                // 为动态列设置自动调整大小
                if (columnInfo.ColumnType != "Basic")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                gridView.Columns.Add(column);
            }

            // 存储列结构信息以供后续更新使用
            gridView.Tag = columnStructure;

            // 更新语言数组记录（用于后续的多语言更新）
            //languageArrayBefore = GetCurrentLanguageArray();
        }

        private async Task ShowResult1(DataGridView gridView, string stationToken = null)
        {
            this.InvokeAsync(() =>
            {
                if (gridView.RowCount > 2000)
                {
                    gridView.Rows.Clear();
                }

                uploadState = resources.GetString("UploadState_Failed");
                if (MES_Result[2006] == "1")
                {
                    uploadState = resources.GetString("UploadState_Success");
                }
                else if (isOffLine == 1)
                {
                    uploadState = resources.GetString("UploadState_Local");
                }

                // --- 修正点：决定使用哪个条码 ---
                string currentBarcode;
                if (chkDoubleStation.Checked)
                {
                    // 在双工位模式下, ShowResult1 只被工位1(stationToken=="1")调用
                    currentBarcode = barcodeInfo1;
                }
                else
                {
                    // 单工位或左右款模式
                    currentBarcode = barcodeInfo;
                }
                // --- 修正点结束 ---

                string productModel = txtProductModel.Text;
                DataGridViewRow dgvRow = new DataGridViewRow();
                dgvRow.CreateCells(gridView);
                dgvRow.Cells[0].Value = Num1;                  // 序号
                //dgvRow.Cells[1].Value = barcodeInfo;           // 条码
                dgvRow.Cells[1].Value = currentBarcode;        // <-- 修正点
                dgvRow.Cells[2].Value = ProductResult;         // 产品结果
                dgvRow.Cells[3].Value = productModel;          // 产品型号
                dgvRow.Cells[4].Value = LoginUser.ToString();  // 操作员
                if (chkDoubleStation.Checked)                        // 上传状态
                {
                    if (stationToken == txtStationCount.Text)
                    {
                        dgvRow.Cells[5].Value = uploadState;
                    }
                    else
                    {
                        dgvRow.Cells[5].Value = resources.GetString("UploadState_Local");
                    }
                }
                else
                {
                    dgvRow.Cells[5].Value = uploadState;
                }
                dgvRow.Cells[6].Value = DateTime.Now.ToString("MM-dd HH:mm:ss");

                int a = 7;
                if (resultList.Count > 0)
                {
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        if (testList.Count > i)
                        {
                            if (testList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = testList[i];
                                a = a + 1;
                            }
                        }

                        if (maxList.Count > i)
                        {
                            if (maxList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = maxList[i];
                                a = a + 1;
                            }
                        }

                        if (minList.Count > i)
                        {
                            if (minList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = minList[i];
                                a = a + 1;
                            }
                        }

                        if (resultList.Count > i)
                        {
                            if (resultList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = resultList[i];
                                a = a + 1;
                            }
                        }
                    }
                }

                if (ProductResult == "NG")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.Red;
                }

                gridView.Rows.Insert(0, dgvRow);
            });
        }

        private async Task ShowResult2(DataGridView gridView, string stationToken = null)
        {
            this.InvokeAsync(() =>
            {
                if (gridView.RowCount > 2000)
                {
                    gridView.Rows.Clear();
                }

                uploadState = resources.GetString("UploadState_Failed");
                if (MES_Result[2006] == "1")
                {
                    uploadState = resources.GetString("UploadState_Success");
                }
                else if (isOffLine == 1)
                {
                    uploadState = resources.GetString("UploadState_Local");
                }

                string productModel = txtProductModel.Text;
                DataGridViewRow dgvRow = new DataGridViewRow();
                dgvRow.CreateCells(gridView);
                dgvRow.Cells[0].Value = Num2;                   // 序号
                //dgvRow.Cells[1].Value = barcodeInfo;           // 条码
                dgvRow.Cells[1].Value = barcodeInfo2;         // <-- 修正点, ShowResult2 只被双工位的工位2(stationToken=="2")调用
                dgvRow.Cells[2].Value = ProductResult;         // 产品结果
                dgvRow.Cells[3].Value = productModel;          // 产品型号
                dgvRow.Cells[4].Value = LoginUser.ToString();  // 操作员

                if (chkDoubleStation.Checked)
                {
                    if (stationToken == txtStationCount.Text)
                    {
                        dgvRow.Cells[5].Value = uploadState;           // 上传状态
                    }
                    else
                    {
                        dgvRow.Cells[5].Value = resources.GetString("UploadState_Local");           // 上传状态
                    }
                }
                else
                {
                    dgvRow.Cells[5].Value = uploadState;           // 上传状态
                }

                dgvRow.Cells[6].Value = DateTime.Now.ToString("MM-dd HH:mm:ss");

                int a = 7;
                if (resultList.Count > 0)
                {
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        if (testList.Count > i)
                        {
                            if (testList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = testList[i];
                                a = a + 1;
                            }
                        }

                        if (maxList.Count > i)
                        {
                            if (maxList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = maxList[i];
                                a = a + 1;
                            }
                        }

                        if (minList.Count > i)
                        {
                            if (minList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = minList[i];
                                a = a + 1;
                            }
                        }

                        if (resultList.Count > i)
                        {
                            if (resultList[i] != "null")
                            {
                                dgvRow.Cells[a].Value = resultList[i];
                                a = a + 1;
                            }
                        }
                    }
                }

                if (ProductResult == "NG")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.Red;
                }

                gridView.Rows.Insert(0, dgvRow);
            });
        }

        /// <summary>
        /// 获取列结构信息（统一的列定义逻辑）
        /// </summary>
        /// <param name="stationToken">工位标识</param>
        /// <param name="isEnableMutiStation">是否启用多工位</param>
        /// <returns>列信息列表</returns>
        private List<ColumnInfo> GetColumnStructure(string stationToken = null, bool isEnableMutiStation = false)
        {
            var columns = new List<ColumnInfo>();

            // 基本信息列（固定7列）
            columns.Add(new ColumnInfo { HeaderKey = "d2NO", ColumnType = "Basic" });      // 序号
            columns.Add(new ColumnInfo { HeaderKey = "d2CPTM", ColumnType = "Basic" });    // 产品条码
            columns.Add(new ColumnInfo { HeaderKey = "d2CPJG", ColumnType = "Basic" });    // 产品结果
            columns.Add(new ColumnInfo { HeaderKey = "d2CPXH", ColumnType = "Basic" });    // 产品编号
            columns.Add(new ColumnInfo { HeaderKey = "d2CZY", ColumnType = "Basic" });     // 操作员
            columns.Add(new ColumnInfo { HeaderKey = "d2SCZT", ColumnType = "Basic" });    // 上传状态
            columns.Add(new ColumnInfo { HeaderKey = "d2CSSJ", ColumnType = "Basic" });    // 测试时间

            // 测试项相关列（动态列）
            if (resultPoint?.Length > 0)
            {
                for (int i = 0; i < testItemsName_Chinese.Length; i++)
                {
                    // 检查是否应该包含此测试项（多工位筛选）
                    bool shouldInclude = true;
                    if (isEnableMutiStation && !string.IsNullOrEmpty(stationToken))
                    {
                        shouldInclude = (targetStationNum[i] == stationToken);
                    }

                    if (!shouldInclude) continue;

                    string testItemName = GetTestItemName(i);
                    string unit = unitName?[i] ?? "";

                    // 实际值列
                    if (actualValuePoint[i] != "NO")
                    {
                        columns.Add(new ColumnInfo
                        {
                            TestItemName = testItemName,
                            ColumnType = "Value",
                            Unit = unit,
                            TestItemIndex = i
                        });
                    }

                    // 上限值列
                    if (maxValuePoint[i] != "NO")
                    {
                        columns.Add(new ColumnInfo
                        {
                            TestItemName = testItemName,
                            ColumnType = "UpperLimit",
                            Unit = unit,
                            TestItemIndex = i,
                            HeaderKey = "UpperLimit"
                        });
                    }

                    // 下限值列
                    if (minValuePoint[i] != "NO")
                    {
                        columns.Add(new ColumnInfo
                        {
                            TestItemName = testItemName,
                            ColumnType = "LowerLimit",
                            Unit = unit,
                            TestItemIndex = i,
                            HeaderKey = "LowerLimit"
                        });
                    }

                    // 测试结果列
                    if (resultPoint[i] != "NO")
                    {
                        columns.Add(new ColumnInfo
                        {
                            TestItemName = testItemName,
                            ColumnType = "Result",
                            Unit = "",
                            TestItemIndex = i,
                            HeaderKey = "TestResult"
                        });
                    }
                }
            }

            return columns;
        }

        /// <summary>
        /// 生成列标题文本
        /// </summary>
        /// <param name="columnInfo">列信息</param>
        /// <returns>列标题文本</returns>
        private string GenerateColumnHeaderText(ColumnInfo columnInfo)
        {
            if (columnInfo.ColumnType == "Basic")
            {
                return resources.GetString(columnInfo.HeaderKey) ?? columnInfo.HeaderKey;
            }

            string testItemName = columnInfo.TestItemName ?? "";
            string unit = columnInfo.Unit ?? "";

            switch (columnInfo.ColumnType)
            {
                case "Value":
                    return testItemName + unit;

                case "UpperLimit":
                    string upperLimit = resources.GetString("UpperLimit") ?? "上限";
                    return $"{testItemName} {upperLimit}{unit}";

                case "LowerLimit":
                    string lowerLimit = resources.GetString("LowerLimit") ?? "下限";
                    return $"{testItemName} {lowerLimit}{unit}";

                case "Result":
                    string testResult = resources.GetString("TestResult") ?? "结果";
                    return $"{testItemName} {testResult}";

                default:
                    return testItemName;
            }
        }

        #endregion

        #region ------------ 保存历史数据到本地 ------------

        private async Task InitializeAsync()
        {
            // 程序启动时加载数据
            (stationResultList1, stationResultList2) = await _dataManager.LoadDataAsync();
            Console.WriteLine($"加载数据完成: 工位1有 {stationResultList1.Count} 条记录, 工位2有 {stationResultList2.Count} 条记录");
        }

        private void StartCleanupTimer()
        {
            // 每30分钟清理一次过期数据
            _cleanupTimer = new System.Timers.Timer(TimeSpan.FromMinutes(30).TotalMilliseconds);
            _cleanupTimer.Elapsed += (sender, e) =>
            {
                _dataManager.CleanExpiredDataPeriodically(stationResultList1, stationResultList2);
                _dataManager.LimitCacheSize(stationResultList1, stationResultList2);
            };
            _cleanupTimer.Start();
        }

        // 程序退出时保存数据
        public async void OnApplicationExit()
        {
            try
            {
                _cleanupTimer?.Stop();
                await _dataManager.SaveDataAsync(stationResultList1, stationResultList2);
                Console.WriteLine("程序退出时数据已保存");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 根据工位模式条件性保存生产数据
        /// </summary>
        /// <param name="stationToken"></param>
        /// <returns></returns>
        private async Task SaveProductionDataConditionally(string stationToken = null)
        {
            // --- 修正点：决定使用哪个条码 ---
            string currentBarcode;
            if (chkDoubleStation.Checked)
            {
                currentBarcode = (stationToken == "1") ? barcodeInfo1 : barcodeInfo2;
            }
            else
            {
                currentBarcode = barcodeInfo;
            }
            // --- 修正点结束 ---

            // (安全回退)
            if (string.IsNullOrWhiteSpace(currentBarcode))
            {
                DisplayMessage("保存：条码为空，取消保存。");
                return;
            }

            if (chkDoubleStation.Checked)
            {
                // 双工位模式：只有工位2完成或出现NG时才保存
                if (stationToken == "1")
                {
                    if (ProductResult == "NG")
                    {
                        await SaveHistoryData(stationToken, currentBarcode);    // <-- 修正点：传入 currentBarcode
                        DisplayMessage($"工位1 NG数据已保存：{currentBarcode}");

                        await OnTestCompleted(currentBarcode, stationToken, ProductResult);
                    }
                    else
                    {
                        DisplayMessage($"工位1 OK数据已缓存：{currentBarcode}，等待工位2完成");
                    }
                }
                else if (stationToken == "2")
                {
                    await SaveHistoryData(stationToken);
                    DisplayMessage($"双工位完整数据已保存：{currentBarcode}");    // <-- 修正点：传递条码
                    await OnTestCompleted(currentBarcode, stationToken, ProductResult);
                }
            }
            else if (chkLeftRight.Checked)
            {
                await SaveHistoryData(stationToken, currentBarcode);    // <-- 修正点：传递条码
            }
            else
            {
                // 单工位模式：直接保存
                await SaveHistoryData(currentBarcode);  // <-- 修正点：传递条码
            }
        }

        private async Task SaveHistoryData(string stationToken = null, string currentBarcode = null)
        {
            this.InvokeAsync(() =>
            {
                lblRunningStatus.ForeColor = Color.Black;
                lblRunningStatus.Text = resources.GetString("RSData_Saving");   // 本地数据保存中
                lblOperatePrompt.ForeColor = Color.Black;
                lblOperatePrompt.Text = resources.GetString("OT_Wait");        // 请等待
            });

            try
            {
                await CreateTableAndInsertData($"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb", stationToken, currentBarcode);
            }
            catch (Exception)
            {
                DisplayMessage("数据保存异常");
            }

            this.InvokeAsync(() =>
            {
                lblRunningStatus.ForeColor = Color.Green;
                lblRunningStatus.Text = resources.GetString("RSData_Saved");        // 本地数据保存完成
                lblOperatePrompt.ForeColor = Color.Black;
                lblOperatePrompt.Text = resources.GetString("OT_Continue");         // 请取下产品继续生产
            });
        }

        // --- 修正点：传入需被保存的条码 ---
        private async Task CreateTableAndInsertData(string conn, string stationToken, string currentBarcode)
        {
            MDBHelper.CreateAccessDatabase(conn);
            var mdb = new MDBHelper(conn);

            #region 创建数据库表头
            ArrayList arrayList = new ArrayList();  // arrayList 用于构建数据库标题名
            object[] obj = new object[] { "产品", "当前工单号", "工装编号", "产品编码", "条码", "测试人", "测试时间", "测试结果" };
            arrayList.AddRange(obj);

            // TODO: 不用每次都重建
            // 构建数据库完整标题名
            for (int i = 0; i < (testItemsName_Chinese.Length); i++)
            {
                arrayList.Add(testItemsName_Chinese[i]);
                if (!maxValuePoint[i].Equals("NO") && !minValuePoint[i].Equals("NO") && !resultPoint[i].Equals("NO"))
                {
                    arrayList.Add(testItemsName_Chinese[i] + "上限");
                    arrayList.Add(testItemsName_Chinese[i] + "下限");
                    arrayList.Add(testItemsName_Chinese[i] + "结果");
                }
            }
            MDBHelper.TryCreateAccessTable(conn, "Sheet1", arrayList);
            #endregion

            // Basic information
            string productModel = string.IsNullOrEmpty(txtProductModel.Text) ? "无" : txtProductModel.Text;
            //string barcode = string.IsNullOrEmpty(barcodeInfo) ? " " : barcodeInfo;
            // --- 修正点：使用传入的 currentBarcode ---
            string barcode = string.IsNullOrEmpty(currentBarcode) ? " " : currentBarcode;
            // --- 结束修正点 ---
            DateTime now = DateTime.Now;

            var columnNames = new List<string>();
            var valueEntries = new List<string>();

            string barcodeRule = string.Empty;
            this.InvokeAsync(() => { barcodeRule = cboBarcodeRule.Text; });

            // 8项基本数据
            columnNames.AddRange(new[] { "产品", "当前工单号", "工装编号", "产品编码", "条码", "测试人", "测试时间", "测试结果" });
            valueEntries.AddRange(new[] {$"'{productModel}'",
            $"'{txtWorkOrder.Text}'",
            $"'{txtFixtureBinding.Text}'",
            $"'{CodeNum.GetProductCodeByRule(barcodeRule, RecipeTable)}'",
            $"'{barcode}'",
            $"'{LoginUser}'",
            $"'{now:yyyy年MM月dd日 HH:mm:ss}'",
            $"'{ProductResult}'" });

            // 动态添加测试项
            if (testItemsName_Chinese.Length > 0)
            {
                // 双工位
                if (chkDoubleStation.Checked)
                {
                    if (stationToken == "1")
                    {
                        // 工位1只添加自己的测试项
                        for (int i = 0; i < testItemsName_Chinese.Length; i++)
                        {
                            if (targetStationNum[i] == stationToken)
                            {
                                columnNames.Add(testItemsName_Chinese[i]);

                                // 如果有上下限和结果等附加数据
                                if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && resultPoint[i] != "NO")
                                {
                                    columnNames.Add(testItemsName_Chinese[i] + "上限");
                                    columnNames.Add(testItemsName_Chinese[i] + "下限");
                                    columnNames.Add(testItemsName_Chinese[i] + "结果");
                                }
                            }
                        }
                    }
                    else if (stationToken == "2")
                    {
                        // 工位2需要按顺序添加：先工位1的测试项，再工位2的测试项

                        // 第一步：添加工位1的测试项
                        for (int i = 0; i < testItemsName_Chinese.Length; i++)
                        {
                            if (targetStationNum[i] == "1")
                            {
                                columnNames.Add(testItemsName_Chinese[i]);

                                // 如果有上下限和结果等附加数据
                                if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && resultPoint[i] != "NO")
                                {
                                    columnNames.Add(testItemsName_Chinese[i] + "上限");
                                    columnNames.Add(testItemsName_Chinese[i] + "下限");
                                    columnNames.Add(testItemsName_Chinese[i] + "结果");
                                }
                            }
                        }

                        // 第二步：添加工位2的测试项
                        for (int i = 0; i < testItemsName_Chinese.Length; i++)
                        {
                            if (targetStationNum[i] == "2")
                            {
                                columnNames.Add(testItemsName_Chinese[i]);

                                // 如果有上下限和结果等附加数据
                                if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && resultPoint[i] != "NO")
                                {
                                    columnNames.Add(testItemsName_Chinese[i] + "上限");
                                    columnNames.Add(testItemsName_Chinese[i] + "下限");
                                    columnNames.Add(testItemsName_Chinese[i] + "结果");
                                }
                            }
                        }
                    }
                }
                // 左右款：检查当前工位是否需要处理此测试项
                else if (chkLeftRight.Checked)
                {
                    for (int i = 0; i < testItemsName_Chinese.Length; i++)
                    {
                        if (targetStationNum[i] == stationToken || stationToken == "3")
                        {
                            columnNames.Add(testItemsName_Chinese[i]);

                            // 如果有上下限和结果等附加数据
                            if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && resultPoint[i] != "NO")
                            {
                                columnNames.Add(testItemsName_Chinese[i] + "上限");
                                columnNames.Add(testItemsName_Chinese[i] + "下限");
                                columnNames.Add(testItemsName_Chinese[i] + "结果");
                            }
                        }
                    }
                }
                // 单工位
                else
                {
                    for (int i = 0; i < testItemsName_Chinese.Length; i++)
                    {
                        columnNames.Add(testItemsName_Chinese[i]);

                        // 如果有上下限和结果等附加数据
                        if (maxValuePoint[i] != "NO" && minValuePoint[i] != "NO" && resultPoint[i] != "NO")
                        {
                            columnNames.Add(testItemsName_Chinese[i] + "上限");
                            columnNames.Add(testItemsName_Chinese[i] + "下限");
                            columnNames.Add(testItemsName_Chinese[i] + "结果");
                        }
                    }
                }
            }

            // 双工位（数据填充）
            if (chkDoubleStation.Checked && resultList.Count > 0)
            {
                var stationResult = new StationResult();

                if (stationToken == "1")
                {
                    // StationToken为1时，只需要stationResultList1中对应条码的数据
                    for (int i = 0; i < stationResultList1.Count; i++)
                    {
                        if (stationResultList1[i].Barcode == currentBarcode)    // <-- 修正点
                        {
                            stationResult.Barcode = stationResultList1[i].Barcode;
                            stationResult.resultList = new List<string>(stationResultList1[i].resultList);
                            stationResult.maxList = new List<string>(stationResultList1[i].maxList);
                            stationResult.minList = new List<string>(stationResultList1[i].minList);
                            stationResult.actualValueList = new List<string>(stationResultList1[i].actualValueList);
                            break; // 找到匹配的条码后退出循环
                        }
                    }

                }
                else if (stationToken == "2")
                {
                    // StationToken为2时，需要将stationResultList1和stationResultList2中相同条码的数据汇总
                    StationResult station1Data = null;
                    StationResult station2Data = null;

                    // 从stationResultList1中查找匹配条码的数据
                    for (int i = 0; i < stationResultList1.Count; i++)
                    {
                        if (stationResultList1[i].Barcode == currentBarcode)    // <-- 修正点
                        {
                            station1Data = stationResultList1[i];
                            break;
                        }
                    }

                    // 从stationResultList2中查找匹配条码的数据
                    for (int i = 0; i < stationResultList2.Count; i++)
                    {
                        if (stationResultList2[i].Barcode == currentBarcode)    // <-- 修正点
                        {
                            station2Data = stationResultList2[i];
                            break;
                        }
                    }

                    // 合并数据到stationResult
                    //stationResult.Barcode = barcodeInfo;
                    stationResult.Barcode = currentBarcode;         // <-- 修正点
                    stationResult.resultList = new List<string>();
                    stationResult.maxList = new List<string>();
                    stationResult.minList = new List<string>();
                    stationResult.actualValueList = new List<string>();

                    // 添加工位1的数据（如果存在）
                    if (station1Data != null)
                    {
                        stationResult.resultList.AddRange(station1Data.resultList);
                        stationResult.maxList.AddRange(station1Data.maxList);
                        stationResult.minList.AddRange(station1Data.minList);
                        stationResult.actualValueList.AddRange(station1Data.actualValueList);
                    }

                    // 添加工位2的数据（如果存在）
                    if (station2Data != null)
                    {
                        stationResult.resultList.AddRange(station2Data.resultList);
                        stationResult.maxList.AddRange(station2Data.maxList);
                        stationResult.minList.AddRange(station2Data.minList);
                        stationResult.actualValueList.AddRange(station2Data.actualValueList);
                    }
                }

                for (int i = 0; i < stationResult.resultList.Count; i++)
                {
                    if (stationResult.maxList[i] != "null" && stationResult.minList[i] != "null" && stationResult.actualValueList[i] != "null")
                    {
                        valueEntries.Add($"'{stationResult.actualValueList[i]}'");
                    }
                    else
                    {
                        valueEntries.Add($"'{stationResult.resultList[i]}'");
                    }

                    if (stationResult.maxList[i] != "null" && stationResult.minList[i] != "null")
                    {
                        valueEntries.Add($"'{stationResult.maxList[i]}'");
                        valueEntries.Add($"'{stationResult.minList[i]}'");
                        valueEntries.Add($"'{stationResult.resultList[i]}'");
                    }
                }
            }
            // 左右款 & 单工位
            else if (resultList.Count > 0)
            {
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (maxList[i] != "null" && minList[i] != "null" && testList[i] != "null")
                    {
                        valueEntries.Add($"'{testList[i]}'");
                    }
                    else
                    {
                        valueEntries.Add($"'{resultList[i]}'");
                    }

                    if (maxList[i] != "null" && minList[i] != "null")
                    {
                        valueEntries.Add($"'{maxList[i]}'");
                        valueEntries.Add($"'{minList[i]}'");
                        valueEntries.Add($"'{resultList[i]}'");
                    }
                }
            }

            string strColumnName = string.Join(",", columnNames);
            string strValues = string.Join(",", valueEntries);

            // 确保列表不为空才执行插入
            if (columnNames.Count > 0)
            {
                string sql = $"INSERT INTO Sheet1 ({strColumnName}) VALUES ({strValues})";
                bool result = mdb.Add(sql);

                if (result)
                {
                    Console.WriteLine("数据插入成功");

                    // 如果是工位2完成测试，清理对应条码的数据
                    if (stationToken == "2")
                    {
                        _dataManager.CleanCompletedData(stationResultList1, stationResultList2, currentBarcode);    // <-- 修正点：传入 currentBarcode
                        await _dataManager.SaveDataAsync(stationResultList1, stationResultList2);
                    }
                }
                else
                {
                    Console.WriteLine("数据插入失败! 生成的SQL语句是: " + sql);
                }
            }
        }

        /// 确保数据库和表存在且可用
        /// </summary>
        private async Task EnsureDatabaseAndTableExist(string conn)
        {
            // 创建数据库文件
            MDBHelper.CreateAccessDatabase(conn);

            // 等待文件系统完成文件创建
            await Task.Delay(100);

            // 验证数据库文件是否真正创建成功
            string dbPath = conn.Replace("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", "").Replace(";", "");

            // 等待文件可访问
            int maxWait = 1000; // 最多等待1秒
            int waited = 0;
            while (waited < maxWait)
            {
                try
                {
                    if (File.Exists(dbPath))
                    {
                        // 尝试打开文件确认可访问
                        using (var fs = File.OpenRead(dbPath))
                        {
                            break; // 文件可访问，退出循环
                        }
                    }
                }
                catch
                {
                    // 文件还不可访问，继续等待
                }

                await Task.Delay(50);
                waited += 50;
            }

            // 构建表结构
            ArrayList arrayList = new ArrayList();
            object[] obj = new object[] { "产品", "当前工单号", "工装编号", "产品编码", "条码", "测试人", "测试时间", "测试结果" };
            arrayList.AddRange(obj);

            // 构建数据库完整标题名
            for (int i = 0; i < testItemsName_Chinese.Length; i++)
            {
                arrayList.Add(testItemsName_Chinese[i]);
                if (!maxValuePoint[i].Equals("NO") && !minValuePoint[i].Equals("NO") && !resultPoint[i].Equals("NO"))
                {
                    arrayList.Add(testItemsName_Chinese[i] + "上限");
                    arrayList.Add(testItemsName_Chinese[i] + "下限");
                    arrayList.Add(testItemsName_Chinese[i] + "结果");
                }
            }

            // 创建表（如果不存在）
            MDBHelper.TryCreateAccessTable(conn, "Sheet1", arrayList);

            // 再次等待确保表创建完成
            await Task.Delay(100);
        }

        private async Task InitializeDatabaseOnStartup()
        {
            try
            {
                // 在程序启动时就创建当月的数据库文件
                string dbPath = $"{lblDataPath.Text}\\{DateTime.Now:Y}生产数据.mdb";
                await EnsureDatabaseAndTableExist(dbPath);

                Console.WriteLine("数据库初始化完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库初始化失败: {ex.Message}");
            }
        }

        #endregion

        #region ------------ 历史数据页面 ------------

        #region ------------ 数据查询 ------------

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchHistoricalData();

            if (pager != null)
            {
                pager.fenye();  // 分页
                PageLoad();     // 显示分页数据
                                //dataset();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        public void SearchHistoricalData()
        {
            DateTime times_Start = dateTimePicker1.Value;
            DateTime times_End = dateTimePicker2.Value;
            //times_Start= times_Start.ToString("yyyy年MM月dd日 HH:mm:ss");
            // times_End= times_Start.ToString("yyyy年MM月dd日 HH:mm:ss");
            string times_Start_string = times_Start.ToString("yyyy年MM月dd日 HH:mm:ss");
            string times_End_string = times_End.ToString("yyyy年MM月dd日 HH:mm:ss");//dateTimePicker2.Value.ToString("yyyy/MM/dd").ToString();
            DateTime times_Start_Sub = DateTime.Parse(times_Start_string);
            DateTime times_End_Sub = DateTime.Parse(times_End_string);
            if (textBoxPath.Text.Length < 1)
            {
                MessageBox.Show("请先选择左侧数据源！");
                return;
            }
            string dataBaseUrl = textBoxPath.Text + ".mdb";
            dbHelper = new MDBHelper(dataBaseUrl);

            StringBuilder sql = new StringBuilder();

            sql.Append("select * from [Sheet1] where CDate(Format(测试时间,'yyyy/MM/dd HH:mm:ss')) between  #" + times_Start_Sub + "#" + " and #" + Convert.ToDateTime(times_End_Sub) + "# ");

            if (textBox_Code.Text != "")
            {
                sql.Append("and 条码 like '" + textBox_Code.Text + "%'");
            }
            if (textBox1.Text != "")
            {
                sql.Append("and 产品 = '" + textBox1.Text + "' ");
            }

            DataTable table = dbHelper.Find(sql.ToString());
            //tablelist_date(dataBaseUrl);
            //dataGridViewDynamic1.SetDataTable(table);
            // dataGridViewDynamic1.DataSource = table;
            pager = new Pager(table);//分页
            dbHelper.CloseConnection();
        }

        #endregion

        /// <summary>
        /// 导出生产历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportProductData(object sender, EventArgs e)
        {
            //打开文件对话框，导出文件
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "保存文件";
            saveFileDialog1.Filter = "Excel文件(*.xls,*.xlsx,*.xlsm)|*.xls,*.xlsx,*.xlsm";
            saveFileDialog1.FileName = "历史数据.xls"; //设置默认另存为的名字

            string dataBaseUrl = textBoxPath.Text + ".mdb";
            dbHelper = new MDBHelper(dataBaseUrl);
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string txtPath = this.saveFileDialog1.FileName;
                DateTime times_Start = dateTimePicker1.Value;
                DateTime times_End = dateTimePicker2.Value;
                string times_Start_string = times_Start.ToString();
                string times_End_string = times_End.ToString();//dateTimePicker2.Value.ToString("yyyy/MM/dd").ToString() + " 23:59:59";
                DateTime times_Start_Sub = DateTime.Parse(times_Start_string);
                DateTime times_End_Sub = DateTime.Parse(times_End_string);
                if (textBoxPath.Text.Length < 1)
                {
                    MessageBox.Show("请先选择左侧数据源！");
                    return;
                }

                StringBuilder sql = new StringBuilder();

                sql.Append("select * from [Sheet1] where CDate(Format(测试时间,'yyyy/MM/dd HH:mm:ss')) between  #" + times_Start_Sub + "#" + " and #" + Convert.ToDateTime(times_End_Sub) + "# ");

                if (textBox_Code.Text != "")
                {
                    sql.Append("and 条码 = '" + textBox_Code.Text + "'");
                }
                if (textBox1.Text != "")
                {
                    sql.Append("and 产品 = '" + textBox1.Text + "' ");
                }

                DataTable table = dbHelper.Find(sql.ToString());

                NPOIHelper.DataTableToExcel(table, txtPath);
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 变更产线数据存放路径
        /// </summary>
        private void ChangeStoragePath(object sender, EventArgs e)
        {
            string prePath = lblDataPath.Text;
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.lblDataPath.Text = path.SelectedPath;
            loggerConfig.Trace($"【变更存放路径】\n原先存放路径：{prePath}\n路径已变更为：{this.lblDataPath.Text}");
        }

        /// <summary>
        /// 初始化加载目录树
        /// </summary>
        private void InitializeDirectoryTree()
        {
            BtnRefreshDirectory_Click(null, null);
        }

        /// <summary>
        /// 刷新目录
        /// </summary>
        private void BtnRefreshDirectory_Click(object sender, EventArgs e)
        {
            directoryTreeView.Nodes.Clear();        // 每次确定时需要刷新内容
            string selectedPath = lblDataPath.Text; // 获得输入框的内容

            // 文件路径存在
            if (Directory.Exists(selectedPath))
            {
                TreeNode rootNode = new TreeNode(selectedPath); // 创建树节点
                directoryTreeView.Nodes.Add(rootNode);          // 加入视图
                FindDirectory(selectedPath, rootNode);          // 通过递归函数进行目录的遍历
            }
        }

        /// <summary>
        /// 遍历生产历史数据目录
        /// </summary>
        void FindDirectory(string nowDirectory, TreeNode parentNode)
        {
            try  // 当文件目录不可访问时，需要捕获异常
            {
                // 获取当前目录下的所有文件夹数组
                string[] directoryArray = Directory.GetFiles(nowDirectory, "*.mdb");
                if (directoryArray.Length > 0)
                {
                    foreach (string item in directoryArray)
                    {
                        // 遍历数组，将节点添加到父亲节点的
                        string str = Path.GetFileNameWithoutExtension(item);
                        TreeNode node = new TreeNode(str);
                        parentNode.Nodes.Add(node);
                    }
                }
            }
            catch (Exception)
            {
                parentNode.Nodes.Add("禁止访问");
            }
        }

        private void directoryTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (directoryTreeView.SelectedNode.FullPath == directoryTreeView.SelectedNode.Text)
            {
                string table = directoryTreeView.SelectedNode.Text;     // 数据表名

                directoryTreeView.SelectedNode.Expand();                // 展开选中的节点
            }
            else
            {
                int count = directoryTreeView.SelectedNode.Text.Length; // 获取选中节点字符长度
                string str = directoryTreeView.SelectedNode.FullPath;   // 获取选中节点从父节点到目标节点的路径

                textBoxPath.Text = str;
            }
        }

        private void dataGridViewDynamic1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                //示例：根据第4列的状态0,1,2 显示不同的颜色
                string status = dataGridViewDynamic1.Rows[e.RowIndex].Cells[7].Value.ToString();
                switch (status)
                {
                    case "NG":
                        dataGridViewDynamic1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case "OK":
                        //dataGridViewDynamic1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        break;
                }
            }
            catch
            {

            }
        }

        #endregion

        #region ------------ 用户管理 ------------

        DataTable userCollection;

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // OP = 1, PE = 2, ADM = 3, DEV = 4, QE = 5, ME = 6
            int[] OpAuth = { 1, 2, 5, 6 };                      // 只能注册操作员权限
            string[] restrictedUsers = { "PE", "QE", "ME" };    // 只能由 ADM 进行管理的权限
            int[] topLevel = { 3, 4 };                          // 最高权限用户
                                                                // 获取数据库中，用户工号的集合
            string[] userID = userCollection.AsEnumerable()
                                  .Select(row => row.Field<string>("工号"))
                                  .ToArray();

            // 确认当前用户是否为 OP、PE、QE、ME
            bool isOpAuth = OpAuth.Contains(Access);
            // 确认当前用户是否为 ADM、DEV
            bool isTopLevel = topLevel.Contains(Access);

            // 检查必填项是否为空
            if (UID.Text.Trim().Length < 1 || textBox22.Text.Trim().Length < 1 || UPWD.Text.Trim().Length < 1)
            {
                MessageBox.Show("工号、姓名、密码为必填项！", "提示");
                return;
            }

            // 确保 OP、PE、QE、ME 用户只能注册 OP 权限
            if (isOpAuth && UTYPE.Text != "OP")
            {
                MessageBox.Show("您只能注册操作员权限，请重新选择！", "提示");
                return;
            }

            // 确保 PE、QE、ME 用户只能由 ADM、DEV 等级用户进行管理
            if (restrictedUsers.Contains(UTYPE.Text) && !isTopLevel)
            {
                MessageBox.Show("您没有权限更改此用户等级，请重新选择！", "提示");
                return;
            }

            // 确保 OP 可以被注册
            if (UTYPE.Text == "OP" && !userID.Contains(UID.Text))
            {

            }
            // 防止除最高权限以外的权限将其他权限被篡改为 OP 权限
            else if (UTYPE.Text == "OP" && lblCurrentSelected.Text != "OP" && !isTopLevel)
            {
                MessageBox.Show("您没有权限更改此用户等级，请重新选择！", "提示");
                return;
            }

            string sql = "";
            dbHelper = new MDBHelper(userFilePath);
            DataTable table1 = dbHelper.Find("select * from Users where 工号 = '" + UID.Text + "'");
            if (table1.Rows.Count > 0)
            {
                //if (comboBox3.SelectedIndex == 1)
                //{
                if (tbxBrandID.Text.Length > 0)
                {
                    DataTable table = dbHelper.Find("select * from Users where [厂牌UID] = '" + tbxBrandID.Text + "' and [厂牌UID] <> '' and [工号] <> '" + UID.Text + "' ");
                    if (table.Rows.Count > 0)
                    {
                        if (tbxBrandID.Text == table.Rows[0]["厂牌UID"].ToString())
                        {
                            MessageBox.Show("不能重复注册、请检查厂牌UID是否重复！");
                            return;
                        }

                    }
                }
                sql = "update [Users] set [用户密码]='" + UPWD.Text + "',[用户权限]='" + UTYPE.Text + "',[用户名]='" + textBox22.Text + "',[厂牌UID]='" + tbxBrandID.Text + "'" +
                             " where [工号] = '" + UID.Text + "'";

                var result = dbHelper.Change(sql);
                if (result == true)
                {
                    MessageBox.Show("修改成功");
                }
            }
            else
            {
                if (tbxBrandID.Text.Length > 0)
                {
                    DataTable table = dbHelper.Find("select * from Users where [厂牌UID] = '" + tbxBrandID.Text + "' and [厂牌UID] <> '' ");
                    if (table.Rows.Count > 0)
                    {
                        MessageBox.Show("不能重复注册、请检查厂牌UID是否重复！");
                        return;
                    }
                }

                sql = "insert into Users ([工号],[用户密码],[用户权限],[用户名],[厂牌UID]) values ('" + UID.Text + "','" + UPWD.Text + "','" + UTYPE.Text + "','" + textBox22.Text + "','" + tbxBrandID.Text + "')";

                bool result = dbHelper.Add(sql.ToString());
                if (result == true)
                {
                    MessageBox.Show("新增成功");
                }

            }
            btnRefreshUser_Click(null, null);
            SaveSystemConfig();
            dbHelper.CloseConnection();

            string modifyInfo = SqlToJsonConverter.ConvertToJSON(sql);
            string final = SqlToJsonConverter.ConvertToCustomLog(modifyInfo);
            loggerAccount.Trace(final);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            // OP = 1, PE = 2, ADM = 3, DEV = 4, QE = 5, ME = 6
            int[] OPAuth = new int[] { 1, 2, 5, 6 };
            bool resultUser = OPAuth.Contains(Access);

            if (resultUser && UTYPE.Text != "OP")
            {
                MessageBox.Show("您无权进行更改！");
                return;
            }

            // 获取当前的行
            int rowindex = dataGridView1.CurrentRow.Index;

            // 将获取到的当前行转为id
            string id = (string)dataGridView1.Rows[rowindex].Cells[4].Value;

            // 连接到数据库
            dbHelper = new MDBHelper(userFilePath);

            // 删除前先执行查找
            string selectSql = $"SELECT [用户名], [用户权限] FROM [Users] WHERE [工号] = '{id}'";
            DataTable userInfo = dbHelper.Find(selectSql);

            string username = "";
            string permissions = "";
            if (userInfo.Rows.Count > 0)
            {
                username = userInfo.Rows[0]["用户名"].ToString();
                permissions = userInfo.Rows[0]["用户权限"].ToString();
            }

            string sql = $" DELETE FROM [Users] WHERE [工号] = '{id}' ";
            bool bl = dbHelper.Del(sql);
            if (bl == true)
            {
                MessageBox.Show("删除成功");
                // 记录日志
                string delInfo = $"【用户删除】\n工号：{id} | 姓名：{username} | 权限：{permissions}";
                loggerAccount.Trace(delInfo);
            }
            btnRefreshUser_Click(null, null);
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshUser_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(userFilePath);
            userCollection = dbHelper.Find("select 用户名,用户密码,用户权限,厂牌UID,工号 from Users where 用户权限 <> 'DEV'");
            userInfoEntities = DataConverter.ConvertDataTableToList(userCollection);
            dataGridView1.DataSource = userCollection;
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 修改登录次数和登录时间
        /// </summary>
        public void UpLoginInfo()
        {
            dbHelper = new MDBHelper(userFilePath);
            DataTable table = dbHelper.Find("select 登录次数 from Users where [工号] = '" + LoginUser + "'");
            if (table.Rows.Count > 0)
            {
                int i = 0;
                if (!string.IsNullOrWhiteSpace(table.Rows[0][0].ToString()))
                {
                    i = int.Parse(table.Rows[0][0].ToString());
                }
                string sql = "update [Users] set [最后登录时间]='" + DateTime.Now.ToString() + "',[登录次数]='" + (i + 1) + "' where [工号] = '" + LoginUser + "'";

                var result = dbHelper.Change(sql);
                //if (result == true)
                //{
                //    MessageBox.Show("修改成功");
                //}
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 选中行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.UID.Text = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                this.UPWD.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.textBox22.Text = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.tbxBrandID.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.UTYPE.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.lblCurrentSelected.Text = UTYPE.Text;
                //this.comboBox3.SelectedIndex = GetCodeByAuthority(this.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
        }

        #endregion

        #region ------------ 配方号的读取与发送 ------------

        /// <summary>
        /// 配方操作保存按钮
        /// </summary>
        private void btnSaveRecipeInfo_Click(object sender, EventArgs e)
        {
            ssd.IsOrderNumberBinding = chkBindOrderNumber.Checked;
            ssd.Save();

            dbHelper = new MDBHelper(path4);
            DataTable SytemSetTable = dbHelper.Find("SELECT * FROM [SytemSet] WHERE ID = '1'");
            if (SytemSetTable.Rows.Count > 0)
            {
                string currentRecipeID = "0";
                if (!string.IsNullOrWhiteSpace(cboBarcodeRule.Text))
                {
                    currentRecipeID = cboBarcodeRule.SelectedValue.ToString();
                }

                // 保存配方号，以及是否从PLC读取配方号
                string sql = $"UPDATE [SytemSet] SET [faults]='{currentRecipeID}', [Workstname] ='{chkReadRecipeId_PLC.Checked}'," +
                    $"[BoardBeat] = '{txtFixtureBinding.Text}' WHERE [ID] = '1'";
                var isUpdateSuccessfully = dbHelper.Change(sql);
                if (isUpdateSuccessfully)
                {
                    recipeId = lblRecipeId.Text = currentRecipeID;
                    MessageBox.Show(resources.GetString("PassBtnSave"));
                    // 记录操作日志
                    bool isChecked = chkReadRecipeId_PLC.Checked;
                    string readPlcStatus = isChecked ? "是" : "否";
                    string msgSave = $"【配方操作保存成功】\n是否读取PLC：{readPlcStatus}\n当前配方号：{lblRecipeId.Text}";
                    loggerConfig.Trace(msgSave);
                }
            }
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 发送配方号给PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendRecipeInfo_Click(object sender, EventArgs e)
        {
            if (chkReadRecipeId_PLC.Checked)
            {
                MessageBox.Show("读取PLC中，不能发送！");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(cboBarcodeRule.Text))
                {
                    MessageBox.Show("请选择条码验证规则！");
                    return;
                }
                try
                {
                    // D1204：允许修改标志
                    readWriteNet.Write(deviceInfo.ModifyRecipePoint, 1);
                    // D1206：修改配方号
                    readWriteNet.Write(deviceInfo.ModifyRecipeIDPoint, int.Parse(cboBarcodeRule.SelectedValue.ToString()));
                    MessageBox.Show("发送PLC成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("发送PLC失败！");
                }
            }
        }

        /// <summary>
        /// 变更工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChangeWorkOrder_Click(object sender, EventArgs e)
        {
            try
            {
                orderNumber = txtWorkOrder.Text;
                await MesIntegrationService.BindWorkOrderAsync(orderNumber);

                dbHelper = new MDBHelper(path4);
                DataTable table1 = dbHelper.Find("select * from SytemSet where ID = '1'");
                if (table1.Rows.Count > 0)
                {
                    string sql = $"UPDATE [SytemSet] SET [wordNo]='{txtWorkOrder.Text}' WHERE [ID] = '1'";
                    var result = dbHelper.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show(resources.GetString("PassBtnUpdate"));
                    }
                }
                dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(resources.GetString("ErrorBtnUpdate"));
            }
        }

        #endregion

        #region ------------ 分页 ------------

        Pager pager = null;

        /// <summary>
        /// 将分页面数据显示出来
        /// </summary>
        private void PageLoad()
        {
            dataGridViewDynamic1.DataSource = pager.LoadPage();//显示数据
            this.textBox13.Text = pager.pageSize.ToString();//每页的条
            this.label34.Text = pager.currentPage + "/" + pager.pageCount;//当前页0/0
            this.label36.Text = pager.recordCount.ToString();//  共条
        }

        /// <summary>
        /// 首页
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage = 1;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click_1(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage--;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click_1(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage++;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            if (pager != null)
            {
                pager.currentPage = pager.pageCount;
                PageLoad();//显示分页数据
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            // pager.currentPage = this.textBox16.Text;
            if (pager != null)
            {
                int i;
                if (int.TryParse(this.textBox16.Text, out i))
                {
                    pager.currentPage = i;
                    PageLoad();//显示分页数据
                }
            }

        }

        /// <summary>
        /// 重新分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (pager != null)
            {
                int i;
                if (int.TryParse(this.textBox13.Text, out i))
                {
                    if (i != 0) pager.pageSize = i;
                    pager.fenye();//分页
                    PageLoad();//显示分页数据
                }

            }
        }

        #endregion

        #region ------------ 看板数据库设置 ------------

        private Action<string> ShowMsgAction;
        private bool isDashboardConnected = false;  // 看板连接状态
        DataTable vulnerableTable;                  // 易损件信息表 vulnerable part
        DataTable faultsTable;                      // 故障信息表

        /// <summary>
        /// 加载看板设置页面相关参数
        /// </summary>
        private void LoadDashboardConfig()
        {
            BtnRefreshAtDashboardSetting(null, null);

            // 遍历SyemSocket表格，从数据库加载看板(Dashboard)相关的配置
            dbHelper = new MDBHelper(path4);
            DataTable table1 = dbHelper.Find("SELECT * FROM SytemSocket WHERE ID = 1");

            for (int i = 0; i < table1.Rows.Count; i++)
            {
                for (int j = 0; j < table1.Columns.Count; j++)
                {
                    try
                    {
                        chkEnableDashboard.Checked = bool.Parse(table1.Rows[i]["Devicestatu"].ToString());  // 是否启用看板
                        chkReadPName.Checked = bool.Parse(table1.Rows[i]["BoardPosition"].ToString());      // 读取PLC

                        txtDashboardIP.Text = table1.Rows[i]["IP"].ToString();                  // 看板IP
                        txtDashboardPort.Text = table1.Rows[i]["Port"].ToString();              // 看板端口
                        txtStationName.Text = table1.Rows[i]["BoardTheory"].ToString();         // 成品名称
                        txtStationNameSets.Text = table1.Rows[i]["BoardName"].ToString();       // 工位名称集合
                        txtFaultStartPoint.Text = table1.Rows[i]["FaultCode"].ToString();       // 故障起始点位
                        txtFaultLength.Text = table1.Rows[i]["FaultLeng"].ToString();           // 故障长度
                        txtLineName.Text = table1.Rows[i]["LineName"].ToString();               // 产线名称
                        txtUseTimePoint.Text = table1.Rows[i]["UseTimePoint"].ToString();       // 实际节拍点位
                    }
                    catch (ArgumentException ex)
                    {
                        try
                        {
                            string message = ex.Message;
                            int startIndex = message.IndexOf('“');
                            int endIndex = message.IndexOf('”');
                            string fieldName = ex.Message.Substring(startIndex + 1, endIndex - startIndex - 1);
                            dbHelper.Add($"ALTER TABLE [SytemSocket] ADD [{fieldName}] varchar(255)");
                            table1 = dbHelper.Find("SELECT * FROM SytemSocket WHERE ID = 1");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
            }
            dbHelper.CloseConnection();

            #region 初始化故障信息表
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = resources.GetString("operation");
            buttonColumn.Text = "保存";
            buttonColumn.Name = "btnCol";
            buttonColumn.DefaultCellStyle.NullValue = "保存";
            dgvWeakInfo.Columns.Add(buttonColumn);

            DataGridViewButtonColumn anotherButtonColumn = new DataGridViewButtonColumn();
            anotherButtonColumn.HeaderText = resources.GetString("operation");
            anotherButtonColumn.Name = "btnCol2";
            anotherButtonColumn.DefaultCellStyle.NullValue = "删除";
            dgvWeakInfo.Columns.Add(anotherButtonColumn);

            DataGridViewButtonColumn butnCo = new DataGridViewButtonColumn();
            butnCo.HeaderText = resources.GetString("operation");
            butnCo.Text = "保存";
            butnCo.Name = "btnCol";
            butnCo.DefaultCellStyle.NullValue = "保存";
            dgvFaultInfo.Columns.Add(butnCo);

            DataGridViewButtonColumn anotrButCo = new DataGridViewButtonColumn();
            anotrButCo.HeaderText = resources.GetString("operation");
            anotrButCo.Name = "btnCol2";
            anotrButCo.DefaultCellStyle.NullValue = "删除";
            dgvFaultInfo.Columns.Add(anotrButCo);
            #endregion
        }

        /// <summary>
        /// 刷新易损件表和故障信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshAtDashboardSetting(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(path4);

            string selectSql = @" SELECT [ID] AS [编号],
                                     [WorkID] AS [工位ID],
                              [BoardPosition] AS [易损件所在的位置], 
                                  [BoardName] AS [易损件的名称],
                                [BoardTheory] AS [理论使用次数PLC点位],
                                  [BoardCode] AS [已经使用的PLC点位]
                                    FROM [VulnbleParts]";
            vulnerableTable = dbHelper.Find(selectSql);

            vulnerableTable.Columns["编号"].AutoIncrement = true;
            vulnerableTable.Columns["编号"].ReadOnly = true;
            vulnerableTable.Columns["编号"].AutoIncrementSeed = 0;

            int rowMax = 0;
            if (vulnerableTable.Rows.Count > 0)
            {
                rowMax = vulnerableTable.AsEnumerable().Max(row => row.Field<int>("编号"));
            }

            DataRow newRow = vulnerableTable.NewRow();
            newRow["编号"] = rowMax;

            // 刷新易损件数据
            dgvWeakInfo.DataSource = vulnerableTable;

            // 刷新故障信息
            faultsTable = dbHelper.Find("SELECT ID AS 编号, [WorkID] AS 工位ID, [CodeID] AS 故障点位, Faults AS 故障描述 from SytemFaults");
            dgvFaultInfo.DataSource = faultsTable;
            dbHelper.CloseConnection();
        }

        /// <summary>
        /// 易损件1.删除2.保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            // 需要记录到日志的字段
            string[] logFields = { "WorkID", "BoardPosition", "BoardName", "BoardTheory", "BoardCode" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "WorkID", "工位序号" },
                { "BoardPosition", "易损件所在的位置" },
                { "BoardName", "易损件的名称" },
                { "BoardTheory", "理论使用次数PLC点位" },
                { "BoardCode", "已经使用的PLC点位" },
            };

            // 删除
            DeleteRowFromDataGridView<int>(dgvWeakInfo, e, "VulnbleParts", "ID", logFields, 2, path4, "btnCol2", fieldAliases, "易损件数据");

            //保存
            if (dgvWeakInfo.Columns[e.ColumnIndex].Name == "btnCol")
            {
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dgvWeakInfo.Columns[e.ColumnIndex];
                string pid = this.dgvWeakInfo.Rows[e.RowIndex].Cells[2].Value.ToString();
                string workid = this.dgvWeakInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string bdPosition = this.dgvWeakInfo.Rows[e.RowIndex].Cells[4].Value.ToString();
                string bdName = this.dgvWeakInfo.Rows[e.RowIndex].Cells[5].Value.ToString();
                string bdTheory = this.dgvWeakInfo.Rows[e.RowIndex].Cells[6].Value.ToString();
                string bdCode = this.dgvWeakInfo.Rows[e.RowIndex].Cells[7].Value.ToString();

                dbHelper = new MDBHelper(path4);
                DataTable table1 = dbHelper.Find("select * from VulnbleParts where [ID] = " + pid);

                // 修改
                if (table1.Rows.Count > 0)
                {
                    DataRow row = table1.Rows[0];

                    // 修改前的详细数据
                    string logDetail = $"编号：{row["ID"]} | 工位序号：{row["WorkID"]} | " +
                        $"易损件所在的位置：{row["BoardPosition"]} | 易损件的名称：{row["BoardName"]} | " +
                        $"理论使用次数PLC点位：{row["BoardTheory"]} | 已经使用的PLC点位：{row["BoardCode"]}";

                    string sql = $"update [VulnbleParts] set [WorkID] = '{workid}', [BoardPosition] = '{bdPosition}', " +
                    $"[BoardName] = '{bdName}', [BoardTheory] = '{bdTheory}', [BoardCode] = '{bdCode}' where [ID] = {pid}";

                    var result = dbHelper.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                        string modifyInfo = $"【易损件数据修改成功】\n修改前的详细信息：\n{logDetail}\n修改后的详细信息：\n" +
                            $"编号：{pid} | 工位序号：{workid} | 易损件所在的位置：{bdPosition} | 易损件的名称：{bdName} | " +
                            $"理论使用次数PLC点位：{bdTheory} | 已经使用的PLC点位：{bdCode}";
                        loggerConfig.Trace(modifyInfo);
                    }
                }

                // 新增
                else
                {
                    string sql = $"insert into VulnbleParts ([ID], [WorkID], [BoardPosition], [BoardName], [BoardTheory], [BoardCode]) " +
                                 $"values ({pid}, '{workid}', '{bdPosition}', '{bdName}', '{bdTheory}', '{bdCode}')";
                    bool result = dbHelper.Add(sql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                        string insertInfo = $"【易损件数据新增成功】\n新增详情：\n" +
                            $"编号：{pid} | 工位序号：{workid} | 易损件所在的位置：{bdPosition} | 易损件的名称：{bdName} | " +
                            $"理论使用次数PLC点位：{bdTheory} | 已经使用的PLC点位：{bdCode}";
                        loggerConfig.Trace(insertInfo);
                    }
                }

                dbHelper.CloseConnection();
                //button18_Click(null, null);
            }

        }

        /// <summary>
        /// 故障信息 1.删除2.保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            // 需要记录日志的字段
            string[] logField = { "WorkID", "CodeID", "Faults" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "WorkID", "工位序号" },
                { "CodeID", "故障点位" },
                { "Faults", "故障描述" },
            };

            // 删除
            DeleteRowFromDataGridView<string>(dgvFaultInfo, e, "SytemFaults", "ID", logField, 2, path4, "btnCol2", fieldAliases, "故障信息");

            // 保存
            if (dgvFaultInfo.Columns[e.ColumnIndex].Name == "btnCol")
            {
                DataGridViewColumn column = dgvFaultInfo.Columns[e.ColumnIndex];
                string pid = this.dgvFaultInfo.Rows[e.RowIndex].Cells[2].Value.ToString();
                string workID = this.dgvFaultInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string codeID = this.dgvFaultInfo.Rows[e.RowIndex].Cells[4].Value.ToString();
                string funmae = this.dgvFaultInfo.Rows[e.RowIndex].Cells[5].Value.ToString();

                if (string.IsNullOrWhiteSpace(pid))
                {
                    MessageBox.Show("故障信息编号不能为空！");
                    return;
                }

                dbHelper = new MDBHelper(path4);
                DataTable table1 = dbHelper.Find("select * from SytemFaults where [ID] = '" + pid + "'");

                // 修改
                if (table1.Rows.Count > 0)
                {
                    DataRow row = table1.Rows[0];

                    // 修改前的详细数据
                    string logDetail = $"修改前的详细信息：\n" +
                        $"编号：{row["ID"]} | 工位序号：{row["WorkID"]} | " +
                        $"故障点位：{row["CodeID"]} | 故障描述：{row["Faults"]}";

                    string sql = $"update [SytemFaults] set [WorkID] = '{workID}', [CodeID] = '{codeID}', " +
                        $"[Faults] = '{funmae}' where [ID] = '{pid}'";

                    var result = dbHelper.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                        string updateInfo = $"修改后的详细信息：\n" +
                            $"编号：{pid} | 工位序号：{workID} | " +
                            $"故障点位：{codeID} | 故障描述：{funmae}";
                        loggerConfig.Trace($"【故障信息修改成功】\n{logDetail}\n{updateInfo}");
                    }
                }

                // 新增
                else
                {
                    string sql = $"insert into SytemFaults ([ID],[WorkID],[CodeID],[Faults]) " +
                        $"values ('{pid}', '{workID}', '{codeID}', '{funmae}')";

                    bool result = dbHelper.Add(sql.ToString());
                    if (result == true)
                    {
                        MessageBox.Show("新增成功");
                        string insertInfo = $"【故障信息新增成功】\n新增详情：\n" +
                            $"编号：{pid} | 工位序号：{workID} | " +
                            $"故障点位：{codeID} | 故障描述：{funmae}";
                        loggerConfig.Trace(insertInfo);
                    }
                }

                dbHelper.CloseConnection();
                //button18_Click(null, null);
            }

        }

        /// <summary>
        /// 看板设置 > 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveDashboardConfig_Click(object sender, EventArgs e)
        {
            if (txtDashboardIP.Text == String.Empty || txtDashboardPort.Text == String.Empty)
            {
                MessageBox.Show("当前界面内容均为必填项、请先填写完善");
                return;
            }

            try
            {
                dbHelper = new MDBHelper(path4);
                DataTable table1 = dbHelper.Find("select * from SytemSocket where ID = 1");
                if (table1.Rows.Count > 0)
                {
                    string sql = $"UPDATE [SytemSocket] SET " +
                        $"[IP]='{txtDashboardIP.Text}'," +
                        $"[Port]='{txtDashboardPort.Text}'," +
                        $"[Devicestatu]='{chkEnableDashboard.Checked}'," +
                        $"[BoardName]='{txtStationNameSets.Text}'," +
                        $"[BoardTheory]='{txtStationName.Text}'," +
                        $"[BoardPosition]='{chkReadPName.Checked}'," +
                        $"[FaultCode]='{txtFaultStartPoint.Text}'," +
                        $"[FaultLeng]='{txtFaultLength.Text}'," +
                        $"[LineName]='{txtLineName.Text}'," +
                        $"[UseTimePoint]='{txtUseTimePoint.Text}'" +
                        $"WHERE [ID] = 1";

                    deviceInfo.StationNameSets_English = txtStationNameSets_English.Text;               // 机台名称_英文
                    deviceInfo.StationNameSets_Thai = txtStationNameSets_Thai.Text;                     // 机台名称_泰文
                    deviceInfo.Save();

                    var result = dbHelper.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show(resources.GetString("PassBtnSave"));
                    }
                }
                dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ------------ 看板Socket连接服务器 ------------

        private System.Net.Sockets.Socket socket;
        private static readonly object objSync = new object();
        private CancellationTokenSource dashboardCts;
        private readonly SemaphoreSlim sendSemaphore = new SemaphoreSlim(1, 1);
        private readonly StringBuilder logBuffer = new StringBuilder(capacity: 10000);
        private readonly System.Windows.Forms.Timer logUpdateTimer;

        private void LogUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (logBuffer.Length > 0)
            {
                string logs;
                lock (logBuffer)
                {
                    logs = logBuffer.ToString();
                    logBuffer.Clear();
                }

                this.InvokeAsync(() =>
                {
                    if (rtbDashboardLog.TextLength > 50000)
                    {
                        rtbDashboardLog.Clear();
                    }

                    rtbDashboardLog.AppendText(logs);
                    rtbDashboardLog.ScrollToCaret();
                });
            }
        }

        /// <summary>
        /// 连接看板
        /// </summary>
        private void ConnectDashboard()
        {
            ConnectDashboard_Click(null, null);
        }

        /// <summary>
        /// 连接看板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectDashboard_Click(object sender, EventArgs e)
        {
            if (!chkEnableDashboard.Checked)
            {
                isDashboardConnected = false;
                lblDashboardStatus.ForeColor = Color.Black;

                // Cancel any ongoing connection attempts
                dashboardCts?.Cancel();
                dashboardCts?.Dispose();
                dashboardCts = null;

                // Dispose the socket properly
                if (socket != null && socket.Connected)
                {
                    try
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                    catch { }
                    socket = null;
                }

                return;
            }

            // Cancel any existing connections first
            dashboardCts?.Cancel();
            dashboardCts?.Dispose();
            dashboardCts = new CancellationTokenSource();

            ConnectToServerAsync(dashboardCts.Token);
        }

        /// <summary>
        /// Socket连接
        /// </summary>
        public async Task ConnectToServerAsync(CancellationToken cancellationToken)
        {
            const int RECONNECT_DELAY_MS = 5000; // More reasonable reconnect delay (5 seconds)

            try
            {
                // Set UI status at start of connection attempt
                await this.InvokeAsync(() => lblDashboardStatus.ForeColor = Color.Orange);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Close existing socket before creating a new one
                        if (socket != null)
                        {
                            try
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();
                            }
                            catch { }
                            socket = null;
                        }

                        // Internet 协议、字节流、IPv4连接
                        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                        // Set receive timeout to detect connection issues faster
                        socket.ReceiveTimeout = 120000; // 2 minutes
                        socket.SendTimeout = 5000; // 5 seconds

                        var serverIP = System.Net.IPAddress.Parse(await this.InvokeAsync(() => txtDashboardIP.Text));
                        var port = await this.InvokeAsync(() => Convert.ToInt32(txtDashboardPort.Text));
                        var serverEndPoint = new IPEndPoint(serverIP, port);

                        // Use async connect with timeout
                        var connectTask = socket.ConnectAsync(serverEndPoint);
                        await Task.WhenAny(connectTask, Task.Delay(5000, cancellationToken));

                        // Check if connection was successful
                        if (!socket.Connected)
                        {
                            throw new TimeoutException("连接超时");
                        }

                        isDashboardConnected = true;

                        // Update UI when connected successfully
                        await this.InvokeAsync(() => lblDashboardStatus.ForeColor = Color.Green);

                        // Start a dedicated task for receiving messages
                        _ = Task.Run(() => ReceiveMessagesAsync(cancellationToken), cancellationToken);

                        // Start heartbeat in separate task
                        _ = SendHeartbeatAsync(cancellationToken);

                        // Send device and station names
                        await SendDeviceAndStationNameAsync(cancellationToken);

                        // Break from reconnection loop once connected
                        break;
                    }
                    catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
                    {
                        isDashboardConnected = false;
                        socket = null;

                        // Update UI to show disconnected state
                        await this.InvokeAsync(() => lblDashboardStatus.ForeColor = Color.Red);

                        DisplayDashboardMessage($"连接失败: {ex.Message}");

                        // Wait before trying to reconnect
                        await Task.Delay(RECONNECT_DELAY_MS, cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation - no need to log
                isDashboardConnected = false;
            }
            catch (Exception ex)
            {
                isDashboardConnected = false;
                DisplayDashboardMessage($"连接异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 接收消息的异步方法
        /// </summary>
        private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[8192]; // Smaller buffer size, adequate for normal messages

            try
            {
                while (isDashboardConnected && !cancellationToken.IsCancellationRequested)
                {
                    // Begin an async receive operation
                    var receiveTask = socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                    // Add timeout to detect network issues
                    var timeoutTask = Task.Delay(60000, cancellationToken); // 60 second timeout

                    // Wait for either completion or timeout
                    var completedTask = await Task.WhenAny(receiveTask, timeoutTask);

                    if (completedTask == timeoutTask)
                    {
                        throw new TimeoutException("接收消息超时");
                    }

                    int bytesRead = await receiveTask;

                    if (bytesRead == 0)
                    {
                        // Connection closed by server
                        throw new Exception("服务器关闭了连接");
                    }

                    string receivedMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    ProcessReceivedData(receivedMsg);

                    DisplayDashboardMessage($"{socket.RemoteEndPoint}:{receivedMsg}");
                }
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation - just exit
            }
            catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
            {
                DisplayDashboardMessage($"接收消息异常: {ex.Message}");
                isDashboardConnected = false;

                // Attempt to reconnect
                _ = ConnectToServerAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 定时发送心跳包
        /// </summary>
        private async Task SendHeartbeatAsync(CancellationToken cancellationToken)
        {
            const int HEARTBEAT_INTERVAL_MS = 10000; // 10 seconds is more reasonable
            byte[] heartbeatData = Encoding.UTF8.GetBytes("heartbeat");

            try
            {
                while (isDashboardConnected && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Send heartbeat only if connected
                        if (socket != null && socket.Connected)
                        {
                            await SendDataAsync(heartbeatData, cancellationToken);
                        }

                        // Wait for next heartbeat interval
                        await Task.Delay(HEARTBEAT_INTERVAL_MS, cancellationToken);
                    }
                    catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
                    {
                        DisplayDashboardMessage($"心跳包发送异常: {ex.Message}");
                        isDashboardConnected = false;

                        // Attempt to reconnect
                        _ = ConnectToServerAsync(cancellationToken);
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation - just exit
            }
        }

        /// <summary>
        /// 发送机台名称和工位名称：一个机台可能有若干个工位
        /// </summary>
        public async Task SendDeviceAndStationNameAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Send station names first
                foreach (var name in stationNameSets)
                {
                    await SendAsync("0+" + name, cancellationToken);
                    await Task.Delay(50, cancellationToken);
                }

                // Send device name after a short delay
                await Task.Delay(200, cancellationToken);

                string deviceName = await this.InvokeAsync(() => txtDeviceName.Text);
                await SendAsync("5+" + deviceName, cancellationToken);
            }
            catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
            {
                DisplayDashboardMessage($"发送机台和工位信息异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 发送单机台数据
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> SendSingleStationDataAsync(string currentBarcode, CancellationToken cancellationToken = default)
        {
            // 8+工位名称+条码+产线名称(产品型号+产线编号)+当前工单号+工装编号+产品编码+当前配方名称+操作人工号+测试时间+测试总结果+实际节拍+测试项目
            string productInfoStr = "8+";

            // 工位名称
            productInfoStr += txtStationName.Text.Trim();

            // 条码
            productInfoStr += "+" + (string.IsNullOrWhiteSpace(currentBarcode) ? barcodeInfo : currentBarcode);

            //产线名称(产品型号+产线编号)
            if (productModel.Trim() == "")
            {
                productInfoStr += "+null" + txtLineName.Text;
            }
            else
            {
                productInfoStr += "+" + txtProductModel.Text + " " + txtLineName.Text;
            }

            // 工单号
            if (orderNumber.Trim() == "")
            {
                productInfoStr += "+null";
            }
            else
            {
                productInfoStr += "+" + orderNumber;
            }

            // 工装编号
            if (txtFixtureBinding.Text.Trim() == "")
            {
                productInfoStr += "+null";
            }
            else
            {
                productInfoStr += "+" + txtFixtureBinding.Text;
            }

            // 产品编码
            if (txtProductCode.Text.Trim() == "")
            {
                productInfoStr += "+null";
            }
            else
            {
                productInfoStr += "+" + txtProductCode.Text;
            }

            // 当前配方名称
            RecipeEntity findCodes = recipeInfoBindingList.FirstOrDefault(find => cboBarcodeRule.Text == find.BarcodeRule);
            try
            {
                if (findCodes == null)
                {
                    productInfoStr += "+null";
                }
                else
                {
                    productInfoStr += "+" + findCodes.ProductName;
                }

            }
            catch (Exception ex)
            {
                productInfoStr += "+null";
            }
            // 操作人工号
            productInfoStr += "+" + LoginUser;
            // 测试时间
            productInfoStr += "+" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
            //测试总结果
            if (ProductResult == "OK")
            {
                productInfoStr += "+OK";
            }
            else
            {
                productInfoStr += "+NG";
            }
            // 实际节拍
            if (productionCycleTime != "null")
            {
                productInfoStr += "+" + productionCycleTime;
            }
            else
            {
                productInfoStr += "+null";
            }

            //添加对应工位的测试项目
            for (int i = 0; i < PLCPointInfoTable.Rows.Count; i++)
            {

                if (PLCPointInfoTable.Rows[i]["BoardName"].ToString().Contains("检测"))
                {
                    productInfoStr += "+" + ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString());
                }
                else
                {
                    //上限
                    if (PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString() != "NO")
                    {

                        productInfoStr += "+" + NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString()));
                    }

                    //下限
                    if (PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString() != "NO")
                    {
                        productInfoStr += "+" + NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString()));
                    }

                    //数值
                    if (PLCPointInfoTable.Rows[i]["BoardCode"].ToString() != "NO")
                    {
                        productInfoStr += "+" + NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["BoardCode"].ToString()));
                    }

                    //结果
                    if (PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString() != "NO")
                    {
                        productInfoStr += "+" + NullModify(await ProcessPointData_PLC(PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString()));
                    }

                }
            }

            return productInfoStr;
        }

        /// <summary>
        /// 判断是否应该上传到MES
        /// </summary>
        /// <param name="stationToken">当前工位</param>
        /// <returns>是否应该上传</returns>
        private bool ShouldSendToDashboard(string stationToken)
        {
            // 看板未连接或Socket未连接时不上传  
            if (!isDashboardConnected || socket == null || !socket.Connected) return false;

            // 双工位模式：工位2上传看板
            if (chkDoubleStation.Checked)
            {
                if (stationToken == "1")
                {
                    DisplayMessage($"当前工位 - {stationToken}, 工位2完成后才发送看板"); return false;
                }
                else if (stationToken == "2")
                {
                    return true;
                }
                else
                {
                    DisplayMessage("未知工位，无法上传数据"); return false;
                }
            }

            // 单工位和左右款模式：都需要上传
            return true;
        }

        /// <summary>
        /// 生成产线信息并发送.
        /// <remark>
        /// 产线信息 = 统计信息、易损件信息、生产信息
        /// </remark>
        /// </summary>
        public async Task GenerateAndSendProductionDataAsync(string stationToken = null, CancellationToken cancellationToken = default)
        {
            if (!ShouldSendToDashboard(stationToken))
            {
                return; // 不上传数据
            }

            // --- 修正点：决定使用哪个条码 ---
            string currentBarcode;
            if (chkDoubleStation.Checked)
            {
                // 双工位模式下，看板只在工位2发送 (ShouldSendToDashboard 保证了这一点)
                // 所以我们总是使用 barcodeInfo2
                currentBarcode = barcodeInfo2;
            }
            else
            {
                // 单工位或左右款，使用全局 barcodeInfo
                currentBarcode = barcodeInfo;
            }

            if (string.IsNullOrWhiteSpace(currentBarcode))
            {
                DisplayMessage("看板：条码为空，取消发送。");
                return;
            }
            // --- 修正点结束 ---

            try
            {
                var deviceName = await this.InvokeAsync(() => txtDeviceName.Text);
                var fixture = await this.InvokeAsync(() => txtFixtureBinding.Text);
                var barcodeRule = await this.InvokeAsync(() => cboBarcodeRule.Text);
                var productModel = await this.InvokeAsync(() => txtProductModel.Text);

                // Use StringBuilder for efficient string concatenation
                var dataBuilder = new StringBuilder(capacity: 10000);

                // 生成统计信息(13项)
                // 机台名称 + 工单号 + 工单数量 + 完成数量 + 完成率 + 合格率 + 生产节拍 + 生产总数 + 工序时间 + 利用时间 + 负荷时间 + 直通率 + 产品型号
                dataBuilder.Append(
                    $"3+{deviceName}" +
                    $"+{orderNumber}" +
                    $"+{orderQuantity}" +
                    $"+{completedQuantity}" +
                    $"+{completeRate}" +
                    $"+{passRate}" +
                    $"+{productionCycleTime}" +
                    $"+{totalQuantity}" +
                    $"+{processTime}" +
                    $"+{usingTime}" +
                    $"+{loadTime}" +
                    $"+{FPY}" +
                    $"+{productModel}");

                // 生成易损件信息
                // 4 + 易损件所在工位 + 机台名称 + 易损件所在位置 + 易损件名称 + 易损件理论使用次数 + 易损件已使用次数；
                if (vulnerableTable != null)
                {
                    // Process in parallel for better performance with larger tables
                    var vulnerableInfos = new List<string>();

                    foreach (DataRow row in vulnerableTable.Rows)
                    {
                        string theoreticalUsageAddress = row["理论使用次数PLC点位"].ToString();
                        string actualUsageAddress = row["已经使用的PLC点位"].ToString();

                        var theoreticalUsageTask = Task.Run(() => readWriteNet.ReadInt32(theoreticalUsageAddress).Content);
                        var actualUsageTask = Task.Run(() => readWriteNet.ReadInt32(actualUsageAddress));

                        // Wait for both tasks to complete
                        await Task.WhenAll(theoreticalUsageTask, actualUsageTask);

                        var actualUsageResult = await actualUsageTask;

                        // [VulnbleParts]
                        // [ID] AS[编号],
                        // [WorkID] AS[工位ID],
                        // [BoardPosition] AS[易损件所在的位置], 
                        // [BoardName] AS[易损件的名称],
                        // [BoardTheory] AS[理论使用次数PLC点位],
                        // [BoardCode] AS[已经使用的PLC点位]
                        if (actualUsageResult.IsSuccess)
                        {
                            var theoreticalUsage = await theoreticalUsageTask;
                            string actualUsage = actualUsageResult.Content.ToString();
                            //string stationName = CodeNum.GetStationNameByID(row["工位ID"].ToString(), stationNameSets);
                            string stationName = txtStationName.Text;

                            string vulnerableInfo =
                                $"4+{stationName}" +
                                $"+{deviceName}" +
                                $"+{row["易损件所在的位置"]}" +
                                $"+{row["易损件的名称"]}" +
                                $"+{theoreticalUsage.ToString()}" +
                                $"+{actualUsage}";

                            vulnerableInfos.Add(vulnerableInfo);
                        }
                    }

                    // Add all vulnerable items
                    foreach (var info in vulnerableInfos)
                    {
                        dataBuilder.Append('|').Append(info);
                    }
                }

                // 生成生产信息
                foreach (var stationName in stationNameSets)
                {
                    // 产品型号信息(11项)
                    // 测试项所在工位名称 + 工单号 + 条码 + 用户工号 + 测试时间 + 产品结果 + 生产节拍 + 产品名称 + empty + empty + 产品型号 
                    //dataBuilder.Append('|').Append(
                    //    $"2+{stationName}+{orderNumber}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                    //    $"+{ProductResult}+{productionCycleTime}+产品名称+ + +{productModel}");

                    // --- 修正点：将 barcodeInfo 替换为 currentBarcode ---
                    dataBuilder.Append('|').Append(
                        $"2+{stationName}+{orderNumber}+{currentBarcode}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                        $"+{ProductResult}+{productionCycleTime}+产品名称+ + +{productModel}");

                    // 工装信息(11项)
                    // 测试项所在工位名称 + 工单号 + 条码 + 用户工号 + 测试时间 + 产品结果 + 生产节拍 + 工装编号[i+1] + empty + empty + 工装信息[i]
                    string[] fixturesInfo = fixture.Split('+');
                    for (int i = 0; i < fixturesInfo.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(fixturesInfo[i]))
                        {
                            //dataBuilder.Append('|').Append(
                            //    $"2+{stationName}+{orderNumber}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                            //    $"+{ProductResult}+{productionCycleTime}+工装编号{i + 1}+ + +{fixturesInfo[i]}");

                            // --- 修正点：将 barcodeInfo 替换为 currentBarcode ---
                            dataBuilder.Append('|').Append(
                                $"2+{stationName}+{orderNumber}+{currentBarcode}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                $"+{ProductResult}+{productionCycleTime}+工装编号{i + 1}+ + +{fixturesInfo[i]}");
                        }
                    }

                    // 物料编码信息(11项)
                    // 测试项所在工位名称 + 工单号 + 条码 + 用户工号 + 测试时间 + 产品结果 + 生产节拍 + 产品物料号[i+1] + empty + empty + 产品编码[i+1]
                    string[] productCodeInfo = CodeNum.GetProductCodes(barcodeRule, RecipeTable);
                    for (int i = 0; i < productCodeInfo.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(productCodeInfo[i]))
                        {
                            //dataBuilder.Append('|').Append(
                            //    $"2+{stationName}+{orderNumber}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                            //    $"+{ProductResult}+{productionCycleTime}+产品物料号{i + 1}+ + +{productCodeInfo[i]}");

                            // --- 修正点：将 barcodeInfo 替换为 currentBarcode ---
                            dataBuilder.Append('|').Append(
                                $"2+{stationName}+{orderNumber}+{currentBarcode}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                $"+{ProductResult}+{productionCycleTime}+产品物料号{i + 1}+ + +{productCodeInfo[i]}");
                        }
                    }
                }
                // 测试项信息
                if (beatList.Count == testList.Count && maxList.Count == minList.Count &&
                     maxList.Count == testList.Count && nameList.Count == testList.Count &&
                     resultList.Count == testList.Count && testList.Count > 0 && testItemsName_Chinese.Length > 0)
                {
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        if (resultList[i] != "null")
                        {
                            // 只有测试结果的
                            // 测试项所在的工位名称 + 工单号 + 条码 + 用户工号 + 测试时间 + 产品总结果 + 生产节拍 + 测试项名称 +  +  + 测试结果
                            if (maxValuePoint[i] == "NO" && minValuePoint[i] == "NO" && resultPoint[i] != "NO")
                            {
                                //dataBuilder.Append('|').Append(
                                //    $"2+{nameList[i]}+{orderNumber}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                //    $"+{ProductResult}+{productionCycleTime}+{testItemsName_Chinese[i]}+ + +{resultList[i]}");

                                // --- 修正点：将 barcodeInfo 替换为 currentBarcode ---
                                dataBuilder.Append('|').Append(
                                    $"2+{nameList[i]}+{orderNumber}+{currentBarcode}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                    $"+{ProductResult}+{productionCycleTime}+{testItemsName_Chinese[i]}+ + +{resultList[i]}");
                            }
                            else
                            {
                                // 带有测试数据的
                                // 测试项所在的工位名称 + 工单号 + 条码 + 用户工号 + 测试时间 + 产品总结果 + 生产节拍 + 测试项名称 + 上限值 + 下限值 + 实际值
                                //dataBuilder.Append('|').Append(
                                //    $"2+{nameList[i]}+{orderNumber}+{barcodeInfo}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                //    $"+{ProductResult}+{productionCycleTime}+{testItemsName_Chinese[i]}+{maxList[i]}+{minList[i]}+{testList[i]}");

                                // --- 修正点：将 barcodeInfo 替换为 currentBarcode ---
                                dataBuilder.Append('|').Append(
                                    $"2+{nameList[i]}+{orderNumber}+{currentBarcode}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                                    $"+{ProductResult}+{productionCycleTime}+{testItemsName_Chinese[i]}+{maxList[i]}+{minList[i]}+{testList[i]}");
                            }
                        }
                    }
                }
                else
                {
                    // 没有测试项的
                    // 总结果信息
                    foreach (var stationName in stationNameSets)
                    {
                        // --- 修正点：将 barcodeInfo 替换为 currentBarcode ---
                        dataBuilder.Append('|').Append(
                            $"2+{stationName}+{orderNumber}+{currentBarcode}+{LoginUser}+{DateTime.Now:yyyy-MM-dd HH:mm:ss}" +
                            $"+{ProductResult}+{productionCycleTime}+测试总结果+ + +{ProductResult}");
                    }
                }

                string productionData = dataBuilder.ToString();
                string singleStationData = await SendSingleStationDataAsync(currentBarcode, cancellationToken);
                productionData += "|" + singleStationData;
                DisplayDashboardMessage(productionData);

                // Send data in a single operation
                await SendAsync(productionData, cancellationToken);

                DisplayMessage("数据上传看板成功");
            }
            catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
            {
                DisplayDashboardMessage($"生成产线信息异常: {ex.Message}");
                DisplayMessage("数据上传看板失败");
            }
        }

        /// <summary>
        /// 使用信号量控制发送并支持取消操作
        /// </summary>
        private async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(message) || !isDashboardConnected || socket == null)
            {
                DisplayMessage("消息为空,退出发送");
                return;
            }

            // Split message into parts
            string[] parts = message.Split('|');

            // Acquire semaphore to ensure only one sender at a time
            await sendSemaphore.WaitAsync(cancellationToken);

            try
            {
                foreach (var part in parts)
                {
                    if (string.IsNullOrWhiteSpace(part))
                    {
                        DisplayMessage("消息组为空,继续发送剩余内容!");
                        continue;
                    }

                    // Format the message with proper delimiters
                    string formattedMessage = $"|{part}|";
                    byte[] buffer = Encoding.UTF8.GetBytes(formattedMessage);

                    await SendDataAsync(buffer, cancellationToken);

                    // Small delay between parts
                    await Task.Delay(10, cancellationToken);
                }
            }
            finally
            {
                // Always release the semaphore
                sendSemaphore.Release();
            }
        }

        /// <summary>
        /// 实际的发送字节数据方法
        /// </summary>
        private async Task SendDataAsync(byte[] data, CancellationToken cancellationToken)
        {
            // Safety check
            if (socket == null || !socket.Connected)
            {
                isDashboardConnected = false;
                throw new SocketException((int)SocketError.NotConnected);
            }

            try
            {
                // Use async send with cancellation
                //await socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None).WaitAsync(cancellationToken);

                // 使用 Task.WhenAny 来实现超时
                var sendTask = socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
                var completedTask = await Task.WhenAny(sendTask, Task.Delay(5000, cancellationToken));

                if (completedTask != sendTask)
                {
                    throw new TimeoutException("发送数据超时");
                }

                await sendTask; // 等待发送完成并传播任何异常
            }
            catch (Exception)
            {
                // If any exception occurs during send, mark as disconnected
                isDashboardConnected = false;
                throw;
            }
        }

        /// <summary>
        /// 日志记录，使用缓冲区批量更新
        /// </summary>
        private void DisplayDashboardMessage(string message)
        {
            string formattedMessage = $"{DateTime.Now:G}: {message}{Environment.NewLine}{Environment.NewLine}";

            // Add to buffer with lock
            lock (logBuffer)
            {
                logBuffer.Append(formattedMessage);
            }
        }

        bool isAllowSwitchWorkOrder = false;

        public void ProcessReceivedData(string receivedMsg)
        {
            string[] dateArray = new string[] { };
            dateArray = receivedMsg.Split(new char[] { '+' });

            // dateArray[0]
            // "0"：切换配方号； 0+配方号
            // "1"：切换工单号； 0+工单号

            switch (dateArray[0])
            {
                case "0":
                    try
                    {
                        // 向PLC发送配方切换信号，并发送对应配方号
                        readWriteNet.Write(deviceInfo.ModifyRecipePoint, 1);
                        readWriteNet.Write(deviceInfo.ModifyRecipeIDPoint, int.Parse(dateArray[1].ToString()));

                        // 更新系统配方号
                        dbHelper = new MDBHelper(path4);
                        DataTable SytemSetTable = dbHelper.Find(" SELECT * FROM [SytemSet] WHERE ID = '1' ");
                        if (SytemSetTable.Rows.Count > 0)
                        {
                            string sql = $" UPDATE [SytemSet] SET [faults]= '{dateArray[1]}' WHERE [ID] = '1' ";
                            var result = dbHelper.Change(sql);
                        }

                        // 反馈配方切换状态
                        Invoke(new Action(() =>
                        {
                            cboBarcodeRule.SelectedValue = dateArray[1];
                            recipeId = lblRecipeId.Text = dateArray[1];
                            SendAsync($"6+{lblDeviceName.Text}配方切换成功");
                        }));
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.ToString());
                        SendAsync($"6+{lblDeviceName.Text}配方切换失败");
                    }
                    break;
                case "1":
                    try
                    {
                        // 联机登录成功时接收工单
                        if (isOffLine == 0 && isMesLoginSuccessful == true)
                        {
                            // 切换工单号
                            if (isAllowSwitchWorkOrder)
                            {
                                Invoke(new Action(() =>
                                {
                                    txtWorkOrder.Text = dateArray[1];
                                }));

                                Invoke(new Action(() =>
                                {
                                    if (txtWorkOrder.Text.Equals(dateArray[1]))
                                    {
                                        SendAsync($"6+{lblDeviceName.Text}生产工单接收成功");
                                    }
                                }));
                            }
                            else
                            {
                                char[] prowrd = dateArray[1].ToCharArray();
                                foreach (var chstr in prowrd)
                                {
                                    SendKeys.SendWait("{" + chstr + "}");
                                }
                                SendKeys.SendWait("{Enter}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SendAsync($"6+{lblDeviceName.Text}生产工单接收失败");
                    }
                    break;
            }
        }

        #endregion

        #region ------------ 打印设置 ------------

        DatasModel.Printer printerConfig = new DatasModel.Printer();    // 打印机基本配置
        private static BarTender.Application btApp = null;
        private static BarTender.Format btFormat;
        private Socket clientSocket = null;                             // 连接打印机
        private bool isPrintSuccessfully = false;                       // 标记打印是否成功
        private bool isSocketConnected = false;                         // 标记打印机连接状态

        #region ------ 打印机基本配置 ------

        /// <summary>
        /// 加载打印机配置信息
        /// </summary>
        private void LoadPrinterConfig()
        {
            try
            {
                printerConfig = DatasServer.PrinterServer.GetPrinterSetting(1);

                // 基本配置
                txtStartPoint_print.Text = printerConfig.StepOfPrint;            // 开始点位
                txtEndPoint_Print.Text = printerConfig.ResultOfPrint;            // 结束点位
                chkPlcControlPrint.Checked = printerConfig.IsControlledByPLC;    // PLC控制打印

                // 打印模式配置
                cboPrintMode.Text = printerConfig.PrintMode;                    // 打印模式
                cboPrinterType.Text = printerConfig.PrinterName;                // 打印机名称               
                txtPrinter_IP.Text = printerConfig.IP;                          // IP
                txtPrinter_Port.Text = printerConfig.Port;                      // 端口

                // 文本配置
                txtBefore.Text = printerConfig.BeforeBarcode;                   // 条码前端
                txtAfter.Text = printerConfig.AfterBarcode;                     // 条码后端
                txtModel.Text = printerConfig.ProductModel;                     // 产品型号
                chkUseFont.Checked = printerConfig.IsUseFont;                   // 使用文本
                chkLoadModel.Checked = printerConfig.IsLoadModel_PLC;           // 读取PLC型号

                // 动态码配置
                txtBarcodeNumber_Printer.Text = printerConfig.BarcodeNumber;    // 码号
                txtSN_Printer.Text = printerConfig.SerialNumber;                // 流水号
                txtSerialSpan.Text = printerConfig.SerialSpan;                  // 流水间隔
                txtPrintCount.Text = printerConfig.PrintCount;                  // 打印份数
                chkAutoAddDate.Checked = printerConfig.IsAutoAddDate;           // 自动添加日期
                chkEnableSN.Checked = printerConfig.IsEnableSN;

                // 文件配置
                lblFileName.Text = printerConfig.FilePath;                      // 文件路径
                cboFileFormat.Text = printerConfig.CurrentFileFormat;           // 文件格式
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 保存打印基本配置
        /// </summary>
        private void SavePrinterConfig_Click(object sender, EventArgs e)
        {
            // 基本配置
            printerConfig.StepOfPrint = txtStartPoint_print.Text;            // 开始点位
            printerConfig.ResultOfPrint = txtEndPoint_Print.Text;            // 结束点位
            printerConfig.IsControlledByPLC = chkPlcControlPrint.Checked;   // 是否启用PLC控制打印

            // 打印模式配置
            printerConfig.PrintMode = cboPrintMode.Text;                    // 打印模式
            printerConfig.PrinterName = cboPrinterType.Text;                // 打印机名称               
            printerConfig.IP = txtPrinter_IP.Text;                          // IP
            printerConfig.Port = txtPrinter_Port.Text;                      // 端口

            var result = printerConfig.Save();
            MessageBox.Show(result);
        }

        #endregion

        #region ------ 辅助方法 ------

        /// <summary>
        /// 获取可用的打印机
        /// </summary>
        private void GetPrinterName()
        {
            foreach (string pkInstalledPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboPrinterType.Items.Add(pkInstalledPrinters);
            }
        }

        /// <summary>
        /// 快速定位至文件所在位置
        /// </summary>
        /// <param name="filePath"></param>
        private static void ShowFileInExplorer(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
                        processStartInfo.FileName = "explorer.exe";
                        processStartInfo.Arguments = "/select," + filePath;  // 使用/select参数定位到文件本身
                        System.Diagnostics.Process.Start(processStartInfo);
                        Console.WriteLine("文件已在'此电脑'中定位并选中。");
                    }
                    catch (Exception ex)
                    {
                        // 处理异常  
                        Console.WriteLine("定位文件时发生错误：");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("文件不存在。");
                }
            }
        }

        /// <summary>
        /// 安全关闭Socket连接
        /// </summary>
        private void CloseSocketSafely()
        {
            if (clientSocket != null)
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                    }
                    clientSocket.Close();
                }
                catch (Exception ex)
                {
                    DisplayMessage($"关闭Socket连接时出现异常: {ex.Message}");
                }
                finally
                {
                    clientSocket = null;
                    isSocketConnected = false;
                }
            }
        }

        /// <summary>
        /// 连接打印机服务器
        /// </summary>
        /// <returns>连接是否成功</returns>
        public async Task<Boolean> ConnectPrinterServerAsync()
        {
            string IP = txtPrinter_IP.Text.ToString();
            if (!int.TryParse(txtPrinter_Port.Text, out int port))
            {
                port = 9100;    // 默认端口
            }

            // 验证IP地址
            if (!System.Net.IPAddress.TryParse(IP, out System.Net.IPAddress ipAddress))
            {
                await this.InvokeAsync(() =>
                {
                    MessageBox.Show("IP地址无效");
                    lblConnectStatus.Text = "IP地址无效";
                    lblConnectStatus.ForeColor = Color.Red;
                });

                return false;
            }

            // 更新UI显示连接状态
            await this.InvokeAsync(() =>
            {
                lblConnectStatus.Text = "连接中...";
                lblConnectStatus.ForeColor = Color.DarkGreen;
            });

            try
            {
                // 关闭现有连接
                CloseSocketSafely();

                // 创建新的Socket连接
                Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 设置超时
                newSocket.ReceiveTimeout = 5000;
                newSocket.SendTimeout = 5000;

                // 连接到打印机
                var connectResult = newSocket.BeginConnect(
                    new IPEndPoint(ipAddress, port), null, null);

                // 等待连接完成，设置超时5秒
                bool success = connectResult.AsyncWaitHandle.WaitOne(5000);

                if (!success)
                {
                    throw new SocketException((int)SocketError.TimedOut);
                }

                // 完成连接
                newSocket.EndConnect(connectResult);

                // 更新连接状态
                clientSocket = newSocket;
                isSocketConnected = true;

                // 更新UI
                await this.InvokeAsync(() =>
                {
                    lblConnectStatus.Text = "打印机连接成功";
                    lblConnectStatus.ForeColor = Color.Green;
                });

                return true;
            }
            catch (Exception ex)
            {
                // 连接失败
                isSocketConnected = false;

                await this.InvokeAsync(() =>
                {
                    lblConnectStatus.Text = $"打印机连接失败: {ex.Message}";
                    lblConnectStatus.ForeColor = Color.Red;
                    DisplayMessage($"打印机连接异常: {ex.Message}");
                });

                return false;
            }
        }

        public static void Init_Bartender()
        {
            try
            {
                if (btApp == null)
                {
                    btApp = new BarTender.Application();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return;
            }
        }

        #endregion

        #region ------ 核心逻辑 ------

        /// <summary>
        /// PCL控制打印
        /// </summary>
        private async Task PlcControlPrint()
        {
            if (!isPlcConnected) return;

            while (isPrinted_PLC)
            {
                // 重置打印状态
                isPrintSuccessfully = false;

                // 更新连接状态
                isSocketConnected = await ConnectPrinterServerAsync();
                if (!isSocketConnected)
                {
                    await this.InvokeAsync(() =>
                    {
                        lblPrintResultTips.Text = "NG";
                        lblPrintResultTips.ForeColor = Color.Red;
                        lblConnectStatus.Text = "连接失败！";
                        lblConnectStatus.ForeColor = Color.Red;
                    });
                }

                // step == 1 && result == 0 , Print new;        step == 2 , Print Again;
                // result == 1 ，Print Successful;              result == 2 , Print Failed;
                var step = readWriteNet.ReadInt32(printerConfig.StepOfPrint).Content;
                var result = readWriteNet.ReadInt16(printerConfig.ResultOfPrint).Content;

                if (step == 1 && result == 0 && isSocketConnected)
                {
                    await this.InvokeAsync(() =>
                    {
                        try
                        {
                            btnPrint_Click(null, null);     // 执行打印

                            readWriteNet.Write(printerConfig.ResultOfPrint, 1);
                        }
                        catch (Exception ex)
                        {
                            isPrintSuccessfully = false;
                            readWriteNet.Write(printerConfig.ResultOfPrint, 2);
                            DisplayMessage($"打印过程发生异常: {ex.Message}，已向PLC反馈失败状态");
                        }
                    });
                }
                else if (step == 2 && isSocketConnected)
                {
                    await this.InvokeAsync(() =>
                    {
                        try
                        {
                            GoToPrint();    // PLC触发条码打印
                            readWriteNet.Write(printerConfig.ResultOfPrint, 1);
                        }
                        catch (Exception ex)
                        {
                            isPrintSuccessfully = false;
                            readWriteNet.Write(printerConfig.ResultOfPrint, 2);
                            DisplayMessage($"重打过程发生异常: {ex.Message}，已向PLC反馈失败状态");
                        }
                    });
                }

                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <returns>18为条码字符串</returns>
        private void GoToPrint()
        {
            string printAgo = txtBefore.Text;       // 条码前端
            string printAfter = txtAfter.Text;      // 条码后端
            string pModel = txtModel.Text;          // 产品型号
            string barcodeInfo = lblBarodeContent_Printer.Text;   // 条码内容

            // 默认为失败状态
            isPrintSuccessfully = false;

            // 获取打印数量
            if (!int.TryParse(txtPrintCount.Text, out int printCount))
            {
                // 默认数量为1
                printCount = 1;
            }

            // 判断打印格式
            switch (cboFileFormat.SelectedIndex)
            {
                case 0: // BTW 文件打印
                    PrintWithBtw(barcodeInfo, printAgo, printAfter, pModel, printCount);
                    break;
                case 1: // PRN 文件打印
                    PrintWithPrn(barcodeInfo, printAgo, printAfter, pModel, printCount);
                    break;
            }
        }

        /// <summary>
        /// 网络打印数据发送(检查连接状态并在必要时重连，然后发送数)
        /// </summary>
        /// <param name="msg">待发送信息的byte数组</param>
        /// <returns>发送是否成功</returns>
        public async Task<bool> SendDataToPrinter(byte[] msg)
        {
            if (msg == null || msg.Length == 0)
            {
                DisplayMessage("发送数据为空");
                return false;
            }

            // 尝试发送数据
            try
            {
                int bytesSent = clientSocket.Send(msg);

                return true;
            }
            catch (SocketException ex)
            {
                // 发送失败时，标记连接状态为断开,打印失败
                isSocketConnected = false;

                // 尝试再次重连并发送
                if (await ConnectPrinterServerAsync())
                {
                    try
                    {
                        int bytesSent = clientSocket.Send(msg);
                        //DisplayMessage("重连后发送数据成功");
                        return true;
                    }
                    catch (Exception retryEx)
                    {
                        //DisplayMessage($"重连后发送数据仍然失败: {retryEx.Message}");
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                //DisplayMessage($"打印机通信发生异常: {ex.Message}");
                isSocketConnected = false;
                return false;
            }
        }

        #endregion

        #region ------ 打印格式(BTW & PRN) ------

        /// <summary>
        /// .btw 文件格式打印
        /// </summary>
        /// <param name="barcodeInfo">条码内容</param>
        /// <param name="printAgo">条码前端内容</param>
        /// <param name="printAfter">条码后端内容</param>
        /// <param name="barcodeModel">条码对应的产品型号</param>
        /// <param name="printCount">打印份数</param>
        /// <exception cref="Exception"></exception>
        private async void PrintWithBtw(string barcodeInfo, string printAgo, string printAfter, string barcodeModel, int printCount)
        {
            try
            {
                // 初始化 Bartender 应用程序
                Init_Bartender();
                if (btApp == null)
                {
                    return;
                }

                // 检查是否为 .btw 文件
                if (PrinFileChecker.IsBtwFile(lblFileName.Text) == false)
                {
                    return;
                }

                // 打开 .btw 文件模板
                btFormat = btApp.Formats.Open(lblFileName.Text);

                // 设置打印份数
                btFormat.PrintSetup.NumberSerializedLabels = printCount;

                // 替换条码内容
                try
                {
                    btFormat.SetNamedSubStringValue("0", barcodeInfo);
                    Console.WriteLine("设置条码内容成功");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"设置条码内容失败: {ex.Message}");
                }

                // 替换文本 {0} = 条码，{1} = 前端，{2} = 后端，{3} = 型号；
                if (chkUseFont.Checked)
                {
                    try
                    {
                        btFormat.SetNamedSubStringValue("1", printAgo);
                        Console.WriteLine("设置前缀成功");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"设置前缀失败: {ex.Message}");
                    }

                    try
                    {
                        btFormat.SetNamedSubStringValue("2", printAfter);
                        Console.WriteLine("设置后缀成功");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"设置后缀失败: {ex.Message}");
                    }

                    try
                    {
                        btFormat.SetNamedSubStringValue("3", barcodeModel);
                        Console.WriteLine("设置产品型号成功");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"设置产品型号失败: {ex.Message}");
                    }
                }

                await Task.Run(() =>
                {
                    // 打印标签
                    btFormat.PrintOut(false, false);

                    // 不保存标签退出
                    btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 用于prn文件格式打印
        /// </summary>
        /// <param name="barcodeInfo">条码信息</param>
        /// <param name="printAgo">条码前缀</param>
        /// <param name="printAfter">条码后缀</param>
        /// <param name="pModel">产品型号</param>
        /// <param name="printCount">打印份数</param>
        /// <returns>打印是否全部成功</returns>
        private async void PrintWithPrn(string barcodeInfo, string printAgo, string printAfter, string pModel, int printCount)
        {
            // 替换文件内容
            string fileContent = File.ReadAllText(lblFileName.Text);
            string instruction = string.Format(fileContent, barcodeInfo);
            if (chkUseFont.Checked)
            {
                instruction = string.Format(fileContent, barcodeInfo, printAgo, printAfter, pModel);
            }

            try
            {
                for (int i = 0; i < printCount; i++)
                {
                    // 判断打印模式（网络 or 驱动）
                    if (cboPrintMode.Text == "驱动打印")
                    {
                        // 判断是否选中打印机
                        if (string.IsNullOrWhiteSpace(cboPrinterType.Text))
                        {
                            MessageBox.Show("未选择打印机类型");
                            return;
                        }

                        try
                        {
                            // 发送打印指令到打印机
                            RawPrinterHelper.SendStringToPrinter(cboPrinterType.Text, instruction);
                            isPrintSuccessfully = true;
                        }
                        catch (Exception ex)
                        {
                            isPrintSuccessfully = false;
                            DisplayMessage($"驱动打印异常 ({i + 1}/{printCount}): {ex.Message}");
                            break;
                        }
                    }
                    else
                    {
                        byte[] Data = System.Text.Encoding.UTF8.GetBytes(instruction);
                        isPrintSuccessfully = await SendDataToPrinter(Data);
                    }
                }

            }
            catch (Exception ex)
            {
                isPrintSuccessfully = false;
                DisplayMessage($"PRN打印过程出现严重错误: {ex.Message}");
            }

            // 更新打印结果
            if (isPrintSuccessfully)
            {
                lblPrintResultTips.Text = "OK";
                lblPrintResultTips.ForeColor = Color.Green;
            }
            else
            {
                lblPrintResultTips.Text = "NG";
                lblPrintResultTips.ForeColor = Color.Red;
            }
        }


        #endregion

        #region ------ 按钮事件处理器 ------

        /// <summary>
        /// 连接按钮
        /// <remark>
        /// 通过网络连接打印机
        /// </remark>
        /// </summary>
        private async void btnConnectPrinter_Click(object sender, EventArgs e)
        {
            // 禁用按钮，防止重复点击
            btnConnectPrinter.Enabled = false;

            try
            {
                // 调用连接方法
                isSocketConnected = await ConnectPrinterServerAsync();
            }
            catch (Exception ex)
            {
                DisplayMessage($"连接打印机时发生异常: {ex.Message}");
                isSocketConnected = false;
            }
            finally
            {
                // 连接完成后重新启用按钮
                btnConnectPrinter.Enabled = true;
            }
        }

        /// <summary>
        /// 变更存放路径
        /// </summary>
        private void btnChangePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (string.IsNullOrEmpty(cboFileFormat.Text))
            {
                MessageBox.Show("请选择打印文件格式");
                return;
            }
            openFileDialog.Filter = $"{cboFileFormat.Text}";
            openFileDialog.Title = "选择打印文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选中的PRN文件路径
                string prnFilePath = openFileDialog.FileName;
                lblFileName.Text = prnFilePath;
            }
        }

        /// <summary>
        /// 查看文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPath_Click(object sender, EventArgs e)
        {
            string filePath = lblFileName.Text;
            ShowFileInExplorer(filePath);
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 文本配置
            printerConfig.BeforeBarcode = txtBefore.Text;                   // 条码前端
            printerConfig.AfterBarcode = txtAfter.Text;                     // 条码后端
            printerConfig.ProductModel = txtModel.Text;                     // 产品型号
            printerConfig.IsUseFont = chkUseFont.Checked;                   // 使用文本
            printerConfig.IsLoadModel_PLC = chkLoadModel.Checked;           // 读取PLC型号

            // 动态码配置
            printerConfig.BarcodeNumber = txtBarcodeNumber_Printer.Text;    // 码号
            printerConfig.SerialNumber = txtSN_Printer.Text;                // 流水号
            printerConfig.SerialSpan = txtSerialSpan.Text;                  // 流水间隔
            printerConfig.PrintCount = txtPrintCount.Text;                  // 打印份数
            printerConfig.IsAutoAddDate = chkAutoAddDate.Checked;           // 自动添加日期
            printerConfig.IsEnableSN = chkEnableSN.Checked;                 // 启用流水号

            // 文件配置
            printerConfig.FilePath = lblFileName.Text;                       // 文件路径
            printerConfig.CurrentFileFormat = cboFileFormat.Text;            // 文件格式

            var result = printerConfig.Save();
            MessageBox.Show(result);
        }

        /// <summary>
        /// 打印按钮
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // 初始化状态
            isPrintSuccessfully = false;

            // 判断是否选中文件格式
            if (string.IsNullOrWhiteSpace(cboFileFormat.Text))
            {
                MessageBox.Show("未选择文件格式");
                return;
            }
            // 判断文件是否存在
            if (string.IsNullOrWhiteSpace(lblFileName.Text))
            {
                MessageBox.Show("文件不存在,请重新选择文件");
                return;
            }

            GenerateBarcode();  // 生成条码内容

            try
            {
                GoToPrint();    // 手动触发条码打印
            }
            catch (Exception ex)
            {
                // 出现异常，确保设置为失败状态
                isPrintSuccessfully = false;
                DisplayMessage($"打印过程发生异常: {ex.Message}");
            }

            // 显示打印结果
            if (isPrintSuccessfully)
            {
                lblPrintResultTips.Text = "OK";
                lblPrintResultTips.ForeColor = Color.Green;
            }
            else
            {
                lblPrintResultTips.Text = "NG";
                lblPrintResultTips.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 斑马打印测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintZebraTest_Click(object sender, EventArgs e)
        {
            if (cboPrintMode.Items.Equals("驱动打印"))
            {
                // 发送打印指令到打印机
                RawPrinterHelper.SendStringToPrinter(cboPrinterType.Text, this.richTextBox2.Text);
            }
            else
            {
                byte[] Data = System.Text.Encoding.UTF8.GetBytes(this.richTextBox2.Text);
                SendDataToPrinter(Data);
            }
        }

        #endregion

        #region ------ 条码 & 流水号 ------

        /// <summary>
        /// 生成条码内容
        /// </summary>
        /// <returns>18位条码字符串</returns>
        /// <remarks>
        /// <para>
        /// 条码 = 码号（10位）+ 生产日期（3位） + 流水号（5位）= 18位；
        /// </para>
        /// 码号 = 地区代码（5位）+ 总成代码（4位）+ 年份（1位）= 10位；
        /// </remarks>
        private string GenerateBarcode()
        {
            // 获取码号
            string codeNumber = txtBarcodeNumber_Printer.Text;
            // 获取流水号单次自增数量
            if (!int.TryParse(txtSerialSpan.Text, out int incrementCount))
            {
                incrementCount = 1;
            }

            // 检查日期并在需要时重置流水号
            if (ShouldResetSerialNumber())
            {
                ResetSerialNumber();
                SaveLastSavedDate();
            }
            else
            {
                // 无论是否重置，都更新最后保存日期
                SaveLastSavedDate();
            }

            SerialNumberIncrement(incrementCount);  // 设定流水号自增数量

            // 添加生产日期
            if (chkAutoAddDate.Checked)
            {
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString("D2");

                if (month == "10" || month == "11" || month == "12")
                {
                    switch (month)
                    {
                        case "10": month = "0"; break;
                        case "11": month = "A"; break;
                        case "12": month = "B"; break;
                    }
                }

                // 生成码号
                codeNumber = codeNumber += month + day;
            }

            // 生产日期的年份包含在码号里面，生产日期只对应月份和日期，其中月份(1位) + 日期(2位)
            // 月份使用数字：1 ~ 9，10(0), 11(A), 12(B); (年份字母禁用：I，O，Q，U，Z)
            // 日期使用数字：01 ~ 31

            string barcode = string.Empty;
            if (chkEnableSN.Checked)
            {
                barcode = codeNumber + txtSN_Printer.Text;

            }
            else
            {
                barcode = codeNumber;
            }

            lblBarodeContent_Printer.Text = barcode;
            return barcode;
        }

        /// <summary>
        /// 流水号自增
        /// </summary>
        /// <param name="incrementCount"></param>
        private async void SerialNumberIncrement(int incrementCount)
        {
            try
            {
                // 获取当前流水号
                int currentSerialNumber = GetCurrentSerialNumber();

                // 计算新的流水号
                int newSerialNumber = currentSerialNumber + incrementCount;

                // 更新流水号
                UpdateSerialNumber(newSerialNumber);

                // 异步保存
                await SaveSerialNumberAsync();
            }
            catch (Exception ex)
            {
                // 处理异常
                MessageBox.Show($"更新流水号时发生错误：{ex.Message}");
            }
        }

        private int GetCurrentSerialNumber()
        {
            // 初始流水号：00000 共5位，且需要每天清零
            if (!int.TryParse(txtSN_Printer.Text, out int serialNumber))
            {
                serialNumber = 0;
            }
            return serialNumber;
        }

        public void InitializeSerialNumber()
        {
            if (ShouldResetSerialNumber())
            {
                ResetSerialNumber();
                SaveLastSavedDate();
            }
        }

        private bool ShouldResetSerialNumber()
        {
            DateTime lastSavedDate = printerConfig.LastSavedDate;
            DateTime currentDate = DateTime.Now.Date;

            // 如果当前日期小于最后保存日期，说明时间被回调了，我们也应该重置
            return currentDate > lastSavedDate || currentDate < lastSavedDate;
        }

        private void ResetSerialNumber()
        {
            txtSN.Text = ssd.SerialNumber = 0.ToString("D5");

            UpdateSerialNumber(0);

            SaveLastSavedDate();
        }

        private void UpdateSerialNumber(int newSerialNumber)
        {
            string formattedSerialNumber = newSerialNumber.ToString("D5");

            txtSN_Printer.Text = printerConfig.SerialNumber = formattedSerialNumber;
        }

        public void SaveLastSavedDate()
        {
            DateTime currentDate = DateTime.Now.Date;

            printerConfig.LastSavedDate = currentDate;  // 只保存日期
            printerConfig.Save();
        }

        public async Task SaveSerialNumberAsync()
        {
            await Task.Run(() =>
            {
                printerConfig.Save();
            });
        }

        #endregion

        #endregion

        #region ------------ PLC点位 ------------

        /// <summary>
        /// 存储产品编号与条码验证型号
        /// </summary>
        DataTable RecipeTable;
        /// <summary>
        /// 测试数据点位信息
        /// </summary>
        DataTable PLCPointInfoTable;
        /// <summary>
        /// 存储配方设置相关的参数
        /// </summary>
        BindingList<RecipeEntity> recipeInfoBindingList = new BindingList<RecipeEntity>();
        BindingList<BarcodeVerification> bindingVefictn = new BindingList<BarcodeVerification>();   // 条码相关数据

        /// <summary>
        /// 从数据库加载 Board 表格保存到对应的数组中，并为 DataGridView4,5 设置属性
        /// </summary>
        private void SYS_BOARD()
        {
            RefreshTable();

            dbHelper = new MDBHelper(path4);
            PLCPointInfoTable = dbHelper.Find("SELECT * FROM Board");

            // 检查表格中是否存在数据
            if (PLCPointInfoTable.Rows.Count > 0)
            {
                // 从数据库表 Board 中的每一行提取特定列的数据，并存储在相应的数组中
                targetStationNum = PLCPointInfoTable.AsEnumerable().Select(row => row["WorkID"].ToString()).ToArray();
                testItemsName_Chinese = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardName"].ToString()).ToArray();
                TestItemsName_English = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardA2"].ToString()).ToArray();
                TestItemsName_Thai = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardA3"].ToString()).ToArray();
                actualValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardCode"].ToString()).ToArray();
                maxValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["MaxBoardCode"].ToString()).ToArray();
                minValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["MinBoardCode"].ToString()).ToArray();
                beatPoint = PLCPointInfoTable.AsEnumerable().Select(row => row["BeatBoardCode"].ToString()).ToArray();
                resultPoint = PLCPointInfoTable.AsEnumerable().Select(row => row["ResultBoardCode"].ToString()).ToArray();
                unitName = PLCPointInfoTable.AsEnumerable().Select(row => row["BoardA1"].ToString()).ToArray();
                standardValuePoint = PLCPointInfoTable.AsEnumerable().Select(row => row["StandardCode"].ToString()).ToArray();
            }
            dbHelper.CloseConnection();

            #region 初始化PLC点位信息表格
            DataGridViewButtonColumn btnNOSave = new DataGridViewButtonColumn();
            btnNOSave.HeaderText = resources.GetString("operation");
            btnNOSave.Text = "NO保存";
            btnNOSave.Name = "btnColNO";
            btnNOSave.DefaultCellStyle.NullValue = "NO保存";
            dgvPLCPointInfo.Columns.Add(btnNOSave);

            DataGridViewButtonColumn btnOneSave = new DataGridViewButtonColumn();
            btnOneSave.HeaderText = resources.GetString("operation");
            btnOneSave.Text = "ONE保存";
            btnOneSave.Name = "btnColONE";
            btnOneSave.DefaultCellStyle.NullValue = "ONE保存";
            dgvPLCPointInfo.Columns.Add(btnOneSave);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = resources.GetString("operation");
            btnDelete.Name = "btnCol2";
            btnDelete.DefaultCellStyle.NullValue = resources.GetString("del");
            dgvPLCPointInfo.Columns.Add(btnDelete);

            // DataGridView5 属性设置，保存
            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            btnSave.HeaderText = resources.GetString("operation");
            btnSave.Text = resources.GetString("save");
            btnSave.Name = "btnSave";
            btnSave.DefaultCellStyle.NullValue = resources.GetString("save");
            dgvRecipeManage.Columns.Add(btnSave);

            // DataGridView5 属性设置，删除
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.HeaderText = resources.GetString("operation");
            btnDel.Name = "btnDel";
            btnDel.DefaultCellStyle.NullValue = resources.GetString("del");
            dgvRecipeManage.Columns.Add(btnDel);
            #endregion
        }

        private void RefreshTable()
        {
            btnRefreshTable_Click(null, null);
        }

        /// <summary>
        /// 刷新PLC点位数据、产品型号相关数据
        /// </summary>
        private void btnRefreshTable_Click(object sender, EventArgs e)
        {
            dbHelper = new MDBHelper(path4);

            // 查询Board表格
            string selectSql = $@" SELECT [ID] AS 编号,
                                      [WorkID] AS 工位ID,
                                   [BoardName] AS 测试项目的中文名称,
                                   [BoardA2] AS 测试项目的英语名称,
                                   [BoardA3] AS 测试项目的泰文名称,
                                   [BoardCode] AS 测试项目的PLC点位,
                                [StandardCode] AS 测试项目标准值的PLC点位,
                                [MaxBoardCode] AS 测试项目的上限PLC点位,
                                [MinBoardCode] AS 测试项目的下限PLC点位,
                             [ResultBoardCode] AS 测试项目的结果PLC点位,
                               [BeatBoardCode] AS 测试项目的节拍PLC点位,
                                     [BoardA1] AS 单位 FROM [Board] ";
            DataTable boardTable = dbHelper.Find(selectSql);

            // 设置 “编号” 列的属性为自动增长、只读、起始值为0
            boardTable.Columns["编号"].AutoIncrement = true;
            boardTable.Columns["编号"].ReadOnly = true;
            boardTable.Columns["编号"].AutoIncrementSeed = 0;

            // 新增行自动填充编号
            int maxRowNum = 0;
            if (boardTable.Rows.Count > 0)
            {
                maxRowNum = boardTable.AsEnumerable().Max(row => row.Field<int>("编号"));
            }
            DataRow newRow = boardTable.NewRow();
            newRow["编号"] = maxRowNum;

            // 刷新PLC点位数据表格
            dgvPLCPointInfo.DataSource = boardTable;

            RecipeTable = dbHelper.Find("SELECT * FROM [RecipeEntity]");
            recipeInfoBindingList = RecipeManage.GetCodesBindingList();
            dgvRecipeManage.DataSource = recipeInfoBindingList;

            // 设置 dataGridView5 的列头文本
            RecipeDataGridView codesDataGridView = new RecipeDataGridView();
            codesDataGridView.GetCodesDataGridViewHeaderText(dgvRecipeManage);
            foreach (DataGridViewColumn column in dgvRecipeManage.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // 将 Codes 表的数据绑定到控件，并设置显示成员和值成员
            cboBarcodeRule.DataSource = RecipeTable;
            cboBarcodeRule.DisplayMember = "BarcodeRule";
            cboBarcodeRule.ValueMember = "RecipeID";

            dbHelper.CloseConnection();

            bindingVefictn = BarcodeVerificationServer.GetBarcodeVerificationBindingList();
            dataGridView6.DataSource = bindingVefictn;
        }

        /// <summary>
        /// PLC点位信息表格中，删除与保存按钮的事件处理器
        /// </summary>
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }
            //   [BoardA2] AS 测试项目的英语名称,
            // [BoardA3] AS 测试项目的泰文名称,
            // 需要记录日志的字段
            string[] logFields = { "WorkID", "BoardName", "BoardA2", "BoardA3", "BoardCode", "MaxBoardCode", "MinBoardCode", "BeatBoardCode", "ResultBoardCode", "BoardA1", "StandardCode" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "WorkID", "工位序号" },
                { "BoardName", "测试项" },
                { "BoardA2", "测试项英文" },
                { "BoardA3", "测试项泰文" },
                { "BoardCode", "实际值点位" },
                { "MaxBoardCode", "上限值点位" },
                { "MinBoardCode", "下限值点位" },
                { "BeatBoardCode", "节拍点位" },
                { "ResultBoardCode", "测试结果" },
                { "BoardA1", "单位" },
                { "StandardCode", "标准值点位" },
            };

            // 从DataGridView 删除选定行
            DeleteRowFromDataGridView<int>(dgvPLCPointInfo, e, "Board", "ID", logFields, 3, path4, btnName: "btnCol2", fieldAliases, "PLC点位数据");

            // NO保存
            if (dgvPLCPointInfo.Columns[e.ColumnIndex].Name == "btnColNO")
            {
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dgvPLCPointInfo.Columns[e.ColumnIndex];
                string pid = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string workID = CodeNum.GetOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[4].Value.ToString());
                string boardName = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[5].Value.ToString();
                string boardA2 = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[6].Value.ToString();
                string boardA3 = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[7].Value.ToString();
                string boardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[8].Value.ToString());
                string stanCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[9].Value.ToString());
                string maxBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[10].Value.ToString());
                string minBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[11].Value.ToString());
                string resultBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[12].Value.ToString());
                string beatBoardCode = CodeNum.GetNoIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[13].Value.ToString());
                string beatBoardA1 = CodeNum.GetNullUnit(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[14].Value.ToString());

                ModifyPlcAddress(pid, workID, stanCode, boardName, boardA2, boardA3, boardCode, maxBoardCode, minBoardCode, resultBoardCode, beatBoardCode, beatBoardA1);
                RefreshTable();
            }

            // ONE保存
            if (dgvPLCPointInfo.Columns[e.ColumnIndex].Name == "btnColONE")
            {
                //Board  
                //ID WorkID BoardName BoardCode MaxBoardCode MinBoardCode BeatBoardCode ResultBoardCode 
                //说明点击的列是DataGridViewButtonColumn列
                DataGridViewColumn column = dgvPLCPointInfo.Columns[e.ColumnIndex];
                string pid = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[3].Value.ToString();
                string workID = CodeNum.GetOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[4].Value.ToString());
                string boardName = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[5].Value.ToString();
                string boardA2 = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[6].Value.ToString();
                string boardA3 = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[7].Value.ToString();
                string boardCode = this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[8].Value.ToString();
                string stanCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[9].Value.ToString());
                string maxBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[10].Value.ToString());
                string minBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[11].Value.ToString());
                string resultBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[12].Value.ToString());
                string beatBoardCode = CodeNum.GetPlusOneIfEmpty(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[13].Value.ToString());
                string beatBoardA1 = CodeNum.GetNullUnit(this.dgvPLCPointInfo.Rows[e.RowIndex].Cells[14].Value.ToString());
                //int i = dateM.Rows.Count;
                ModifyPlcAddress(pid, workID, stanCode, boardName, boardA2, boardA3, boardCode, maxBoardCode, minBoardCode, resultBoardCode, beatBoardCode, beatBoardA1);
                RefreshTable();
            }
        }

        /// <summary>
        /// 删除某一行，并记录删除的数据
        /// </summary>
        private void DeleteRowFromDataGridView<T>(DataGridView dataGridView, DataGridViewCellEventArgs e,
            string tableName, string primaryKeyField, string[] logFields, int primaryKeyColumnIndex,
            string connectionString, string btnName, Dictionary<string, string> fieldAliases, string viewName)
        {
            // 检查选中的行是否为空
            if (dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumnIndex].Value == null ||
                string.IsNullOrWhiteSpace(dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumnIndex].Value.ToString()))
            {
                //MessageBox.Show("选中的行无效或编号为空，无法删除！");
                return;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == btnName)
            {
                // 获取选中行的主键值，并将其转换为泛型类型
                T primaryKeyValue = (T)dataGridView.Rows[e.RowIndex].Cells[primaryKeyColumnIndex].Value;

                // 根据主键类型创建 SQL 语句
                string primaryKeyCondition = primaryKeyValue is string ? $"'{primaryKeyValue}'" : primaryKeyValue.ToString();

                // 查询要删除的行数据
                dbHelper = new MDBHelper(connectionString);
                string selectSql = $"SELECT * FROM {tableName} WHERE [{primaryKeyField}] = {primaryKeyCondition}";
                DataTable rowTable = dbHelper.Find(selectSql);

                if (rowTable.Rows.Count > 0)
                {
                    DataRow row = rowTable.Rows[0];

                    // 生成日志详细数据
                    string logDetail = $"编号: {row[primaryKeyField]}";
                    foreach (var field in logFields)
                    {
                        //logDetail += $" | {field}: {row[field]}";
                        if (fieldAliases.TryGetValue(field, out string alias))
                        {
                            logDetail += $" | {alias}：{row[field]}";
                        }
                        else
                        {
                            // 如果映射中没有找到别名，就使用字段名
                            logDetail += $" | {field}：{row[field]}";
                        }
                    }

                    // 执行删除操作
                    string deleteSql = $"DELETE FROM {tableName} WHERE [{primaryKeyField}] = {primaryKeyCondition}";
                    bool bl = dbHelper.Del(deleteSql);

                    if (bl)
                    {
                        MessageBox.Show("删除成功");
                        loggerConfig.Trace($"【{viewName}删除】\n" +
                                           $"成功删除第{primaryKeyCondition}行, 该行的详细数据: \n{logDetail}");

                        RefreshTable();
                        BtnRefreshAtDashboardSetting(null, null);
                    }
                }

                dbHelper.CloseConnection();
            }
        }

        /// <summary>
        /// 更新或新增 DataGridView4 的数据
        /// </summary>
        private void ModifyPlcAddress(string pid, string workID, string stanCode, string boardName, string boardA2, string boardA3, string boardCode, string maxBoardCode, string minBoardCode, string resultBoardCode, string beatBoardCode, string beatBoardA1)
        {
            dbHelper = new MDBHelper(path4);
            string selectSql = $"select * from Board where [ID] = {pid}";
            DataTable table1 = dbHelper.Find(selectSql);

            // 修改
            if (table1.Rows.Count > 0)
            {
                DataRow row = table1.Rows[0];

                // 修改前的详细数据
                string logDetail = $"修改前的详细信息：\n" +
                                   $"编号：{row["ID"]} | 工位序号：{row["WorkID"]} | 测试项：中文 {row["BoardName"]}  英文 {row["boardA2"]} 泰文 {row["boardA3"]} | " +
                                   $"实际值点位：{row["BoardCode"]} | 上限值点位：{row["MaxBoardCode"]} | " +
                                   $"下限值点位：{row["MinBoardCode"]} | 节拍点位：{row["BeatBoardCode"]} | " +
                                   $"测试结果点位：{row["ResultBoardCode"]} | 单位：{row["BoardA1"]} | " +
                                   $"标准值点位：{row["StandardCode"]}";

                if (table1.Rows.Count > 0)
                {
                    string sql = $"update [Board] set [WorkID] = '{workID}', [BoardName] = '{boardName}', [boardA2]='{boardA2}', [boardA3]='{boardA3}' , " +
                        $"[BoardCode] = '{boardCode}', [MaxBoardCode] = '{maxBoardCode}', [MinBoardCode] = '{minBoardCode}', " +
                        $"[BeatBoardCode] = '{beatBoardCode}', [ResultBoardCode] = '{resultBoardCode}', [BoardA1] = '{beatBoardA1}', " +
                        $"[StandardCode] = '{stanCode}' where [ID] = {pid}";

                    var result = dbHelper.Change(sql);
                    if (result == true)
                    {
                        MessageBox.Show("修改成功");
                        string modifyInfo = $"【点位数据修改成功】\n{logDetail}\n修改后的详细信息：\n编号：{pid} | 工位序号：{workID} | 测试项：中文 {boardName} 英文{boardA2} 泰文 {boardA3}  | " +
                            $"实际值点位：{boardCode} | 上限值点位：{maxBoardCode} | 下限值点位：{minBoardCode} | 节拍点位：{beatBoardCode} | " +
                            $"测试结果点位：{resultBoardCode} | 单位：{beatBoardA1} | 标准值点位：{stanCode} ";
                        loggerConfig.Trace(modifyInfo);
                    }
                }

            }
            // 新增
            else
            {
                string sql = "insert into Board ([ID],[WorkID],[StandardCode],[BoardName],[boardA2],[boardA3],[BoardCode],[MaxBoardCode],[MinBoardCode],[BeatBoardCode],[ResultBoardCode],[BoardA1]) values ("
                    + pid + ",'" + workID + "','" + stanCode + "','" + boardName + "','" + boardA2 + "','" + boardA3 + "','" + boardCode + "','" + maxBoardCode + "','" + minBoardCode + "','" + beatBoardCode + "','" + resultBoardCode + "','" + beatBoardA1 + "')";

                bool result = dbHelper.Add(sql);
                if (result == true)
                {
                    MessageBox.Show("新增成功");
                    loggerConfig.Trace($"【点位数据新增成功】\n新增详情：\n" +
                        $"编号：{pid} | 工位序号：{workID} | 测试项：中文 {boardName} 英文{boardA2} 泰文 {boardA3} | " +
                        $"实际值点位：{boardCode} | 上限值点位：{maxBoardCode} | 下限值点位：{minBoardCode} | " +
                        $"节拍点位：{beatBoardCode} | 测试结果：{resultBoardCode} | 单位：{beatBoardA1} | 标准值点位：{stanCode} ");
                }
            }

            dbHelper.CloseConnection();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }

            // 需要记录日志的字段
            string[] logFields = { "ProductName", "BarcodeRule" };

            // 定义字段与别名的映射
            Dictionary<string, string> fieldAliases = new Dictionary<string, string>
            {
                { "ProductName", "产品型号" },
                { "BarcodeRule", "条码规则" },
            };

            // 删除
            DeleteRowFromDataGridView<string>(dgvRecipeManage, e, "RecipeEntity", "RecipeID", logFields, 2, path4, btnName: "btnDel", fieldAliases, "配方信息");

            // 保存
            if (dgvRecipeManage.Columns[e.ColumnIndex].Name == "btnSave")
            {
                RecipeEntity recipeEntity = recipeInfoBindingList[e.RowIndex];

                // 检查是否是修改操作
                bool isModification = !string.IsNullOrEmpty(recipeEntity.RecipeID) && recipeEntity.RecipeID != "0";
                if (isModification)
                {
                    // 修改前的 RecipeManage 信息
                    RecipeEntity original = RecipeManage.GetCodes(recipeEntity.RecipeID);
                    if (original != null)
                    {
                        string originalInfo = $"编号：{original.RecipeID} | 产品名称：{original.ProductName} | " +
                                              $"条码验证型号与工装编号：{original.BarcodeRule} | 产品编码：{original.ProductCode}";

                        // 保存新的信息
                        string saveResult = RecipeManage.GetCodesSave(recipeEntity);

                        if (saveResult == LanguageResour.PassBtnSave)
                        {
                            // 修改后的信息
                            string modifiedInfo = $"编号：{recipeEntity.RecipeID} | 产品名称：{recipeEntity.ProductName} | " +
                                                  $"条码规则：{recipeEntity.BarcodeRule} | 产品编码：{recipeEntity.ProductCode}";

                            // 记录修改日志
                            string modifyInfo = $"【产品信息修改成功】\n" +
                                                $"修改前的详细信息：\n{originalInfo}\n" +
                                                $"修改后的详细信息：\n{modifiedInfo}";
                            loggerConfig.Trace(modifyInfo);
                            MessageBox.Show(resources.GetString("PassBtnUpdate"));
                        }
                    }
                    else
                    {
                        string saveResult = RecipeManage.GetCodesSave(recipeEntity);
                        if (saveResult == LanguageResour.PassBtnSave)
                        {
                            string logDetail = $"【配方信息保存成功】\n" +
                                $"编号：{recipeEntity.RecipeID} | 产品名称：{recipeEntity.ProductName} | " +
                                $"条码规则：{recipeEntity.BarcodeRule} | 产品编码：{recipeEntity.ProductCode}";
                            loggerConfig.Trace(logDetail);
                            MessageBox.Show(resources.GetString("PassBtnSave"));
                        }
                    }
                }
            }
        }

        #endregion

        #region ------------ 条码验证 ------------

        public void PLCBarQRCode()
        {
            Utility.DGVButAdd.DataGridViewButton.AddDGVButton(dataGridView6);
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            BarcodeVerification bv = bindingVefictn[e.RowIndex];

            // 删除
            if (dataGridView6.Columns[e.ColumnIndex].Name == "BtnDel")
            {
                MessageBox.Show(bv.Delete());
                RefreshTable();
            }

            // 保存
            if (dataGridView6.Columns[e.ColumnIndex].Name == "BtnSave")
            {
                MessageBox.Show(bv.Save());
                RefreshTable();
            }
        }

        #endregion

        #region ------------ 读卡器 ------------

        RfidReader Reader = new RfidReader();
        List<UserInfoEntity> userInfoEntities;
        Task taskProcess_UserID = null;
        bool IsRunningplc_UserID = true;
        bool isReaderOpen = false;
        string PuserUID = string.Empty;

        public void SearchPort()
        {
            string[] ports = SerialPort.GetPortNames();

            cmbShowPort.Items.Clear();
            cmbShowPort.Text = null;

            foreach (string port in ports)
            {
                cmbShowPort.Items.Add(port);
            }

            if (ports.Length > 0)
            {
                cmbShowPort.Text = ports[0];
            }
            else
            {
                cmbShowPort.Text = "没有发现可用端口";
            }
        }

        /// <summary>
        /// 搜索读卡器端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchPort_Click(object sender, EventArgs e)
        {
            SearchPort();
        }

        private void ConnectReader()
        {
            OpenReader_Click(null, null);
        }

        /// <summary>
        /// 连接读卡器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenReader_Click(object sender, EventArgs e)
        {
            try
            {
                Reader.DisConnect();

                // 判断读卡器当前连接状态
                if (isReaderOpen)
                {
                    btnOpenReader.Enabled = false;
                    isReaderOpen = false;
                    btnOpenReader.Text = resources.GetString("readCard_Open");  // 打开读卡器
                    Task.WaitAll(taskProcess_UserID);
                    Thread.Sleep(1000);
                    btnOpenReader.Enabled = true;
                }
                else
                {
                    if (cmbShowPort.Text.Length < 1 || tbxReaderDeviceID.Text.Length < 1)
                    {
                        lblReaderState.Text = (resources.GetString("posType"));   // 读卡器端口、设备号获取失败、请先维护！
                        lblReaderState.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        // 连接读卡器
                        if (cmbShowPort.Text.Length < 1 || tbxReaderDeviceID.Text.Length < 1)
                        {
                            lblReaderState.Text = (resources.GetString("posType"));
                            lblReaderState.ForeColor = Color.Red;
                            return;
                        }
                        else
                        {
                            bool flg = Reader.Connect(cmbShowPort.Text, 9600);
                            if (flg == true)
                            {
                                lblReaderState.Text = "成功连接读卡器";
                                lblReaderState.ForeColor = Color.Green;
                                isReaderOpen = true;
                                btnOpenReader.Text = resources.GetString("readCard_Close");  // 关闭读卡器
                                taskProcess_UserID = new Task(Process_UserID);
                                taskProcess_UserID.Start();
                            }
                            else
                            {
                                lblReaderState.Text = (resources.GetString("portNG"));  // 状态显示：端口打开失败！
                                lblReaderState.ForeColor = Color.Red;
                                return;
                            }
                        }

                    }
                }
            }
            catch
            {
                lblReaderState.Text = (resources.GetString("portType"));    // 状态显示：端口被占用！
                lblReaderState.ForeColor = Color.Red;
                return;
            }
        }

        private void Process_UserID()
        {
            while (isReaderOpen)
            {
                this.Invoke(new Action(() =>
                {
                    timer1_Tick(null, null);
                }));

                Thread.Sleep(500);
                Application.DoEvents();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int status;
                byte[] type = new byte[2];
                byte[] id = new byte[4];

                Reader.Cmd = Cmd.M1_ReadId;//读卡号命令
                Reader.Addr = Convert.ToByte(tbxReaderDeviceID.Text, 16);//读写器地址,设备号
                Reader.Beep = Beep.On;

                status = Reader.M1_Operation();
                if (status == 0)//读卡成功
                {
                    for (int i = 0; i < 2; i++)//获取2字节卡类型
                    {
                        type[i] = Reader.RxBuffer[i];
                    }
                    for (int i = 0; i < 04; i++)//获取4字节卡号
                    {
                        id[i] = Reader.RxBuffer[i + 2];
                    }

                    string userid = byteToHexStr(id, 4);
                    if (userid.Length > 0)
                    {
                        tbxBrandID.Text = userid;
                        PuserUID = tbxBrandID.Text;

                        //查询用户
                        //根据userid 读取账号密码
                        List<UserInfoEntity> list = userInfoEntities.Where(x => x.cardID == PuserUID).ToList();
                        if (list.Count > 0)
                        {
                            if (list.Count > 1)
                            {
                                readWriteNet.Write(deviceInfo.EndNFCPoint, -1);
                            }
                            foreach (var v in list)
                            {
                                int Paccess = 0;
                                if (v.Uuser.Length > 0 && v.Upwd.Length > 0)
                                {
                                    if (v.Utype == "ADM")
                                    {
                                        Paccess = 5;
                                    }
                                    else if (v.Utype == "PE")
                                    {
                                        Paccess = 2;
                                    }
                                    else if (v.Utype == "QE")
                                    {
                                        Paccess = 4;
                                    }
                                    else if (v.Utype == "ME")
                                    {
                                        Paccess = 3;
                                    }
                                    else if (v.Utype == "OP")
                                    {
                                        Paccess = 1;
                                    }
                                }
                                lblPlcAccess.Text = Paccess.ToString();
                                readWriteNet.Write(deviceInfo.EndNFCPoint, Paccess);
                                loggerAccount.Trace($"【PLC触摸屏当前权限信息】\n 工号：{v.Uuser} | 姓名：{v.userName} | 权限：{v.Utype}");
                            }
                        }
                        else
                        {
                            readWriteNet.Write(deviceInfo.EndNFCPoint, -1);
                        }

                        PuserUID = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string byteToHexStr(byte[] bytes, int len)  //数组转十六进制字符
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < len; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        #endregion

        #region ------------ PLC测试 ------------

        /// <summary>
        /// 写入
        /// </summary>
        private async void btnWriteValue_Click(object sender, EventArgs e)
        {
            try
            {
                string type = cboWriteValue.Text;
                string address = txtPoint_Write.Text;
                string value = txtValue_Write.Text;

                switch (type)
                {
                    case "Int16":
                        var result = await readWriteNet.WriteAsync(address, Convert.ToInt16(value));
                        if (!result.IsSuccess)
                        {
                            MessageBox.Show(result.Message);
                        }
                        break;
                    case "Int32":
                        var result1 = await readWriteNet.WriteAsync(address, Convert.ToInt32(value));
                        if (!result1.IsSuccess)
                        {
                            MessageBox.Show(result1.Message);

                        }
                        break;
                    case "Float":
                        var resultFLoat = await readWriteNet.WriteAsync(address, Convert.ToSingle(value));
                        if (!resultFLoat.IsSuccess)
                        {
                            MessageBox.Show(resultFLoat.Message);

                        }
                        break;
                    case "Double":
                        var result2 = await readWriteNet.WriteAsync(address, Convert.ToDouble(value));
                        if (!result2.IsSuccess)
                        {
                            MessageBox.Show(result2.Message);

                        }
                        break;
                    case "String":
                        var result3 = await readWriteNet.WriteAsync(address, value);
                        if (!result3.IsSuccess)
                        {
                            MessageBox.Show(result3.Message);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        private async void btnReadValue_Click(object sender, EventArgs e)
        {
            try
            {
                string address = txtPoint_Read.Text;
                string type = cboReadValue.Text;

                switch (type)
                {
                    case "Int16":
                        var result = await readWriteNet.ReadInt16Async(address);
                        if (result.IsSuccess)
                        {
                            txtValue_Read.Text = result.Content.ToString();
                        }
                        else
                        {
                            MessageBox.Show(result.Message);
                        }
                        break;
                    case "Int32":
                        var result1 = await readWriteNet.ReadInt32Async(address);
                        if (result1.IsSuccess)
                        {
                            txtValue_Read.Text = result1.Content.ToString();
                        }
                        else
                        {
                            MessageBox.Show(result1.Message);
                        }
                        break;
                    case "Float":
                        var resultFLoat = await readWriteNet.ReadFloatAsync(address);
                        if (resultFLoat.IsSuccess)
                        {
                            txtValue_Read.Text = resultFLoat.Content.ToString();
                        }
                        else
                        {
                            MessageBox.Show(resultFLoat.Message);
                        }
                        break;
                    case "Double":
                        var result2 = await readWriteNet.ReadDoubleAsync(address);
                        if (result2.IsSuccess)
                        {
                            txtValue_Read.Text = result2.Content.ToString();
                        }
                        else
                        {
                            MessageBox.Show(result2.Message);
                        }
                        break;
                    case "String":
                        string length = Interaction.InputBox("请输入读取位数", "读取位数");
                        var result3 = await readWriteNet.ReadStringAsync(address, Convert.ToUInt16(length));
                        if (result3.IsSuccess)
                        {
                            txtValue_Read.Text = result3.Content.ToString();
                        }
                        else
                        {
                            MessageBox.Show(result3.Message);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ------------ Mes统计信息 ------------

        private void button37_Click(object sender, EventArgs e)
        {
            rtbMESOutput.Clear();
            string parameter = rtbMESInput.Text;
            string outMES = BydWorkCom.GetParamsAsy(parameter);
            rtbMESOutput.Text = outMES + "\r\n";
        }

        #endregion

        private async void cboBarcodeRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 从PLC读取配方时不能手动选择配方
                if (chkReadRecipeId_PLC.Checked) return;

                // 避免程序启动时抢先进入当前方法,确保recipeId正确初始化后才能进入
                if (string.IsNullOrWhiteSpace(recipeId)) return;

                // 防止配方号未正确初始化
                if (Convert.ToInt32(recipeId) < 0)
                {
                    recipeId = "0";
                }

                if (!chkBypassFixtureValidation.Checked)
                {
                    RecipeEntity findCodes = recipeInfoBindingList.FirstOrDefault(find => cboBarcodeRule.Text == find.BarcodeRule);
                    string currentId = findCodes.RecipeID;

                    // 检查配方是否变更
                    if (currentId != recipeId)
                    {
                        // 清除工装绑定
                        txtFixtureBinding.Text = string.Empty;

                        // 配方变更，触发对应提示
                        await HandleRecipeChange(currentId);

                        // 原有的UI更新逻辑
                        cboBarcodeRule.SelectedValue = currentId;
                        recipeId = lblRecipeId.Text = currentId;

                        // 保存最新配方号到数据库
                        try
                        {
                            dbHelper = new MDBHelper(path4);
                            string sql = $"update [SytemSet] set [faults]='{recipeId}' where [ID] = '1'";
                            if (dbHelper.Change(sql))
                            {
                                DisplayMessage("配方号更新，并成功保存到数据库");
                            }
                            else
                            {
                                DisplayMessage("配方号更新，保存到数据库失败");
                            }
                        }
                        finally
                        {
                            dbHelper.CloseConnection();
                        }
                    }

                    // 更新配方相关的UI
                    UpdateRecipeRelatedUI(currentId);
                }
                else
                {
                    if (recipeInfoBindingList.Count > 0 && cboBarcodeRule.Text.Trim() != "" && cboBarcodeRule.Text.Trim() != "System.Data.DataRowView")
                    {
                        RecipeEntity findCodes = recipeInfoBindingList.FirstOrDefault(find => cboBarcodeRule.Text == find.BarcodeRule);
                        txtProductCode.Text = findCodes.ProductCode;
                        txtProductModel.Text = findCodes.ProductName;
                        txtFixtureBinding.Text = findCodes.FixtureNumber;
                        recipeId = lblRecipeId.Text = findCodes.RecipeID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblRecipeId_TextChanged(object sender, EventArgs e)
        {
            if (recipeInfoBindingList.Count > 0 && cboBarcodeRule.Text.Trim() != "" && cboBarcodeRule.Text.Trim() != "System.Data.DataRowView")
            {
                RecipeEntity currentRecipe = recipeInfoBindingList.FirstOrDefault(find => cboBarcodeRule.Text == find.BarcodeRule);
                txtProductCode.Text = currentRecipe.ProductCode;
                txtProductModel.Text = currentRecipe.ProductName;
                txtFixtureBinding.Text = currentRecipe.FixtureNumber;
            }
        }

        private void BoardCreateDataBaseBtn_Click(object sender, EventArgs e)
        {
            SendInsertColumnsToBoard();
        }

        public void SendInsertColumnsToBoard()
        {
            if (!isDashboardConnected) return;

            Task.Run(() =>
            {
                Invoke(new Action(async () =>
                {
                    // 7+工位名称+条码+产线名称+当前工单号+工装编号+产品编码+当前配方名称+操作人工号+测试时间+测试总结果+实际节拍+测试项目
                    string insertColumnsStr = "7+";

                    // 工位名称
                    insertColumnsStr += txtStationName.Text.Trim();
                    insertColumnsStr += "+条码+产线名称+工单号+工装编号+产品编码+产品型号+用户工号+测试时间+测试总结果+实际节拍";

                    if (PLCPointInfoTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < PLCPointInfoTable.Rows.Count; i++)
                        {
                            string testItemName = PLCPointInfoTable.Rows[i]["BoardName"].ToString();
                            string maxValuePoint = PLCPointInfoTable.Rows[i]["MaxBoardCode"].ToString();
                            string minValuePoint = PLCPointInfoTable.Rows[i]["MinBoardCode"].ToString();
                            string realValuePoint = PLCPointInfoTable.Rows[i]["BoardCode"].ToString();
                            string testResultPoint = PLCPointInfoTable.Rows[i]["ResultBoardCode"].ToString();

                            if (maxValuePoint != "NO" && minValuePoint != "NO")
                            {
                                insertColumnsStr += $"+{testItemName}上限";
                                insertColumnsStr += $"+{testItemName}下限";
                                insertColumnsStr += $"+{testItemName}数值";
                                insertColumnsStr += $"+{testItemName}结果";
                            }
                            else
                            {
                                if (realValuePoint != "NO" || testResultPoint != "NO")
                                {
                                    insertColumnsStr += $"+{testItemName}结果";
                                }
                            }
                        }
                    }

                    DisplayDashboardMessage(insertColumnsStr);
                    await SendAsync(insertColumnsStr);

                    MessageBox.Show("配置数据发送成功", "看板操作", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });
        }

        private async void SwitchLoginMode(object sender, EventArgs e)
        {
            // 防止重复点击
            lblLoginMode.Enabled = false;

            try
            {
                // 询问用户是否确认切换模式
                string modeChangeMessage = isOffLine == 0 ?
                    "是否确认切换到离线模式？" :
                    "是否确认切换到在线模式？";

                DialogResult confirmResult = MessageBox.Show(
                    modeChangeMessage,
                    "模式切换确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return; // 用户取消了切换
                }

                // 创建并显示自定义登录表单
                using (FormChangeLoginMode loginForm = new FormChangeLoginMode(this, isOffLine == 1))
                {
                    if (loginForm.ShowDialog() == DialogResult.OK && loginForm.LoginSuccess)
                    {
                        // 从登录表单获取用户信息
                        string newLoginUser = loginForm.LoginUser;
                        string newLoginName = loginForm.LoginName;
                        string newLoginPwd = loginForm.LoginPassword;
                        int newAccess = loginForm.AccessLevel;

                        // 更新当前表单中的凭据
                        this.SetloginUser(newLoginUser);
                        this.SetloginName(newLoginName);
                        this.SetloginPwd(newLoginPwd);
                        this.Setaccess(newAccess);
                        this.Setaccess_take(1); // 设置访问权限标志

                        // 切换登录模式
                        if (isOffLine == 0) // 当前在线模式
                        {
                            // 切换到离线模式
                            isOffLine = 1;
                            this.SetoffLineType(isOffLine);
                            lblLoginMode.Text = resources.GetString("loginMode1"); // 离线

                            // 如果需要，设置默认工单号
                            if (string.IsNullOrWhiteSpace(txtWorkOrder.Text))
                            {
                                txtWorkOrder.Text = "111111111111";
                            }

                            // 更新状态指示器
                            this.InvokeAsync(() =>
                            {
                                lblRunningStatus.ForeColor = Color.Green;
                                lblRunningStatus.Text = resources.GetString("RSUser_OfflineOK"); // 离线用户验证成功
                                lblOperatePrompt.ForeColor = Color.Black;
                                lblOperatePrompt.Text = resources.GetString("OT_WaitingScan"); // 等待扫描条码
                            });

                            // 允许自由切换工单号
                            isAllowSwitchWorkOrder = true;
                        }
                        else // 当前离线模式
                        {
                            // 切换到在线模式
                            isOffLine = 0;
                            this.SetoffLineType(isOffLine);
                            lblLoginMode.Text = resources.GetString("loginMode"); // 在线

                            this.InvokeAsync(() =>
                            {
                                // MES用户验证
                                lblRunningStatus.Text = resources.GetString("RSUser_Verifing");     // 用户验证中
                                lblOperatePrompt.Text = resources.GetString("OT_Wait");         // 请等待
                            });

                            // 配置MES连接参数
                            Config_Mes(ip, port, timeout, url, site, user, password, resource, operation, nccode, Header);

                            // 执行MES用户验证
                            var verifyResult = await MesIntegrationService.VarifyUserLoginAsync();
                            string MESFeedback = await ExtractMESInfo(verifyResult.MESFeedback);

                            if (verifyResult.isUserVerifySuccessfully)
                            {
                                if (MESFeedback != null)
                                {
                                    this.InvokeAsync(() =>
                                    {
                                        rtbMesLog.Clear();
                                        rtbMesLog.AppendText(MESFeedback);

                                        lblRunningStatus.ForeColor = Color.Green;
                                        lblRunningStatus.Text = resources.GetString("RSUser_OnlineOK");       // 联机用户验证成功
                                        lblOperatePrompt.ForeColor = Color.Black;
                                        lblOperatePrompt.Text = resources.GetString("OT_WaitingScan"); // 等待扫描条码
                                    });

                                    isMesLoginSuccessful = true;

                                    // 处理工单号
                                    if (txtWorkOrder.Text == "111111111111")
                                    {
                                        string newWorkOrder = Interaction.InputBox(
                                            resources.GetString("InputBox"),
                                            resources.GetString("InputBoxName"),
                                            "", 100, 100);

                                        if (!string.IsNullOrWhiteSpace(newWorkOrder))
                                        {
                                            txtWorkOrder.Text = newWorkOrder;
                                            orderNumber = newWorkOrder;

                                            // 如果启用了工单绑定，则绑定工单
                                            if (chkBindOrderNumber.Checked)
                                            {
                                                await MesIntegrationService.BindWorkOrderAsync(orderNumber);

                                                var orderInfo = await MesIntegrationService.FetchOrderNumberInfo(orderNumber);
                                                orderQuantity = orderInfo.orderQuantity;
                                                completedQuantity = orderInfo.completedQuantity;
                                                completeRate = orderInfo.completeRate;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeAsync(() =>
                                    {
                                        rtbMesLog.Clear();
                                        rtbMesLog.AppendText(MESFeedback);

                                        lblRunningStatus.ForeColor = Color.Red;
                                        lblRunningStatus.Text = resources.GetString("RSUser_OnlineNG");   // 联机用户验证失败
                                        lblOperatePrompt.Text = resources.GetString("OT_CheckParam");     // 请检查联机参数
                                        lblLoginMode.Text = resources.GetString("loginMode1"); // 离线
                                        MessageBox.Show("联机验证失败，已切回离线模式", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    });

                                    // 验证失败，切回离线模式
                                    isOffLine = 1;
                                    this.SetoffLineType(isOffLine);
                                }
                            }
                            else
                            {
                                this.InvokeAsync(() =>
                                {
                                    rtbMesLog.Clear();
                                    rtbMesLog.AppendText(MESFeedback);

                                    lblRunningStatus.ForeColor = Color.Red;
                                    lblRunningStatus.Text = resources.GetString("RSUser_OnlineNG");
                                    lblOperatePrompt.Text = resources.GetString("OT_CheckParam");

                                    lblLoginMode.Text = resources.GetString("loginMode1"); // 离线
                                    MessageBox.Show("联机验证失败，已切回离线模式", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                });

                                // 验证失败，切回离线模式
                                isOffLine = 1;
                                this.SetoffLineType(isOffLine);
                            }
                        }

                        // 更新Process_Offline状态
                        Process_Offline();

                        // 记录模式更改
                        string modeChangeInfo = $"【登录模式切换】\n" +
                                              $"用户：{LoginUser} ({LoginName}) | " +
                                              $"切换为：{(isOffLine == 0 ? "在线模式" : "离线模式")}";
                        loggerAccount.Trace(modeChangeInfo);
                    }
                    else
                    {
                        // 用户取消登录，不更改模式
                        MessageBox.Show("模式切换已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                MessageBox.Show(
                    $"模式切换时发生错误\n{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 记录错误
                loggerConfig.Error($"模式切换错误: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // 重新启用标签点击
                lblLoginMode.Enabled = true;
            }
        }

        private void lblLoginMode_MouseEnter(object sender, EventArgs e)
        {
            // 鼠标悬停时改变样式，提示可点击
            lblLoginMode.Font = new Font(lblLoginMode.Font, FontStyle.Underline);
            lblLoginMode.ForeColor = Color.Blue;
            Cursor = Cursors.Hand;
        }

        private void lblLoginMode_MouseLeave(object sender, EventArgs e)
        {
            // 鼠标离开时恢复样式
            lblLoginMode.Font = new Font(lblLoginMode.Font, FontStyle.Regular);
            lblLoginMode.ForeColor = Color.Black;
            Cursor = Cursors.Default;
        }

        //当前选择的语言
        private Language CurrentSelectedLanguage = Language.ChineseSimplified;
        private string[] languageArrayBefore = null;

        private async void cboCurrentLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 检查是否选择了相同的语言，如果是则直接返回
            if (cboCurrentLanguage.SelectedIndex == LanguageId)
            {
                return;
            }

            cboCurrentLanguage.Enabled = false;

            try
            {
                // 保存当前窗体状态
                var currentWindowState = this.WindowState;
                var currentBounds = this.Bounds;

                // 获取当前程序集
                InitializeResourceMap();

                // 存储切换前的语言状态
                int previousLanguageId = LanguageId;

                switch (cboCurrentLanguage.SelectedIndex)
                {
                    case 0:
                        if (LanguageId == 0) return; // 双重检查
                        languageArrayBefore = testItemsName_Chinese;
                        break;
                    case 1:
                        if (LanguageId == 1) return;
                        languageArrayBefore = TestItemsName_English;
                        break;
                    case 2:
                        if (LanguageId == 2) return;
                        languageArrayBefore = TestItemsName_Thai;
                        break;
                    default:
                        languageArrayBefore = null;
                        break;
                }

                if (cboCurrentLanguage.SelectedIndex == 0)
                {
                    //修改默认语言
                    MultiLanguage.SetDefaultLanguage("zh-CN");
                    this.CurrentSelectedLanguage = Language.ChineseSimplified;
                    resources = new ResourceManager("MesDatas.Language_Resources.language_Chinese", assembly);
                    LanguageId = 0;
                    //对所有打开的窗口重新加载语言
                    UpdateResourceMap();
                }
                else if (cboCurrentLanguage.SelectedIndex == 1)
                {
                    //修改默认语言
                    MultiLanguage.SetDefaultLanguage("en-US");
                    this.CurrentSelectedLanguage = Language.English;
                    resources = new ResourceManager("MesDatas.Language_Resources.language_English", assembly);
                    LanguageId = 1;
                    //对所有打开的窗口重新加载语言
                    UpdateResourceMap();
                }
                else if (cboCurrentLanguage.SelectedIndex == 2)
                {
                    //修改默认语言
                    MultiLanguage.SetDefaultLanguage("th-TH");
                    this.CurrentSelectedLanguage = Language.Thai;
                    resources = new ResourceManager("MesDatas.Language_Resources.language_Thai", assembly);
                    LanguageId = 2;
                    //对所有打开的窗口重新加载语言
                    UpdateResourceMap();
                }

                lblCurrentUser.Text = $"{LoginUser} ({LoginName})";
                switch (LanguageId)
                {
                    case 0:
                        lblDeviceName.Text = txtDeviceName.Text;
                        break;
                    case 1:
                        lblDeviceName.Text = txtDeviceName_English.Text;
                        break;
                    case 2:
                        lblDeviceName.Text = txtDeviceName_Thai.Text;
                        break;
                    default:
                        lblDeviceName.Text = txtDeviceName.Text;
                        break;
                }

                this.WindowState = FormWindowState.Minimized;
                await Task.Delay(300);
                this.Invoke((MethodInvoker)delegate
                {
                    this.WindowState = FormWindowState.Maximized;
                    tabControl1.Dock = DockStyle.Fill;
                    this.Refresh();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"语言切换失败: {ex.Message}");
            }
            finally
            {
                cboCurrentLanguage.Enabled = true;
            }
        }

        // 初始化时生成反向映射
        private void InitializeResourceMap()
        {
            textToResourceKeyMap.Clear();

            // 获取当前语言的所有资源
            ResourceSet resourceSet = resources.GetResourceSet(
                Thread.CurrentThread.CurrentCulture, true, true);

            // 生成文本到键的映射
            foreach (DictionaryEntry entry in resourceSet)
            {
                string key = entry.Key.ToString();
                string value = entry.Value.ToString();

                // 避免重复值导致的冲突
                if (!textToResourceKeyMap.ContainsKey(value))
                {
                    textToResourceKeyMap.Add(value, key);
                }
            }
        }

        /// <summary>
        /// 更新多语言资源映射
        /// </summary>
        public void UpdateResourceMap()
        {
            this.InvokeAsync(() =>
            {
                lblRunningStatus.Text = UpdateDataLabelAndLabels(lblRunningStatus.Text);    // 运行状态
                lblOperatePrompt.Text = UpdateDataLabelAndLabels(lblOperatePrompt.Text);    // 操作提示
                lblProductResult.Text = UpdateDataLabelAndLabels(lblProductResult.Text);    // 产品结果
                lbl_Left.Text = UpdateDataLabelAndLabels(lbl_Left.Text);                    // 左结果
                lbl_Right.Text = UpdateDataLabelAndLabels(lbl_Right.Text);                  // 右结果

                // 更新TabPage文本
                UpdateTabPageTexts();

                // 更新dgvResult的列标题
                UpdateResultGridHeaders(dgvResult1);
                UpdateResultGridHeaders(dgvResult2);
                UpdateResultGridHeaders(dgvResult3);

                // 更新其他DataGridView
                UpdateDataGridViewColumns(dgvTest);
                UpdateDataGridViewColumns(dgvProductionIndex);
                UpdateDataGridViewColumns(dgvShowBarcode);

                RecipeDataGridView recipeDataGridView = new RecipeDataGridView();

                recipeDataGridView.GetCodesDataGridViewHeaderText(dgvRecipeManage);
            });

            bvList = BarcodeVerificationServer.GetAllBarcodeVerifications();
            MultiLanguage.LoadLanguage(this, typeof(Form1));
        }

        // 改进 UpdateResultGridHeaders，添加同步锁
        private readonly object _gridUpdateLock = new object();

        /// <summary>
        /// 更新dgv的列标题
        /// </summary>
        /// <param name="gridView">DataGridView控件</param>
        private void UpdateResultGridHeaders(DataGridView gridView)
        {
            if (gridView.InvokeRequired)
            {
                gridView.Invoke(new Action(() => UpdateResultGridHeaders(gridView)));
                return;
            }

            if (gridView.Columns.Count == 0)
            {
                // 如果没有列，则不进行更新
                return;
            }

            // 获取存储的列结构信息
            var columnStructure = gridView.Tag as List<ColumnInfo>;

            if (columnStructure == null)
            {
                // 如果没有存储的结构信息，尝试重新创建
                DisplayMessage("警告：未找到列结构信息，尝试重新创建表头");
                return;
            }

            // 确保列数匹配
            if (columnStructure.Count != gridView.Columns.Count)
            {
                DisplayMessage($"警告：列数不匹配，期望{columnStructure.Count}列，实际{gridView.Columns.Count}列");
                return;
            }

            lock (_gridUpdateLock)
            {
                // 更新列标题
                for (int i = 0; i < columnStructure.Count && i < gridView.Columns.Count; i++)
                {
                    var columnInfo = columnStructure[i];

                    // 对于测试项相关的列，需要更新测试项名称
                    if (columnInfo.ColumnType != "Basic")
                    {
                        columnInfo.TestItemName = GetTestItemName(columnInfo.TestItemIndex);
                    }

                    // 生成新的列标题
                    gridView.Columns[i].HeaderText = GenerateColumnHeaderText(columnInfo);
                }

                // 更新存储的列结构信息
                gridView.Tag = columnStructure;
            }
        }

        public string UpdateDataLabelAndLabels(string labelText)
        {
            var keyText = resources.GetString(FindBestMatchingKey(labelText) ?? "");
            return keyText ?? labelText;
        }

        private void UpdateDataGridViewColumns(DataGridView dgv)
        {
            if (dgv == null || dgv.Columns.Count == 0) return;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                // 假设资源键格式为 "dgv列名"，例如 "d2NO"
                // 查找匹配的前缀及其索引

                (int Index, string Value)? match = languageArrayBefore
                    .Where(prefix => !string.IsNullOrWhiteSpace(prefix)) // 过滤空白项
                    .Select((prefix, index) => (Index: index, Value: prefix))
                    .FirstOrDefault(item => item.Value != null && column.HeaderText.StartsWith(item.Value));
                if (string.IsNullOrWhiteSpace(match.Value.Value))
                {
                    string resourceKey = FindBestMatchingKey(column.HeaderText) ?? ""; // 或自定义前缀，如 "dgv_" + column.Name
                    string localizedText = resources.GetString(resourceKey);
                    if (!string.IsNullOrEmpty(localizedText))
                    {
                        column.HeaderText = localizedText;
                    }
                }
                else
                {
                    string prefix = match.Value.Value;
                    string remainingPart = column.HeaderText.Substring(prefix.Length);
                    string[] parts = remainingPart.Split('(');
                    string resourceKey = FindBestMatchingKey(parts[0]) ?? "";
                    string localizedText = resources.GetString(resourceKey);
                    if (string.IsNullOrEmpty(localizedText))
                    {
                        localizedText = parts[0];
                    }
                    var testItemName = GetTestItemName(match.Value.Index);
                    if (string.IsNullOrEmpty(testItemName))
                    {
                        testItemName = prefix;
                    }
                    string textUnit = "";
                    if (parts.Length > 1)
                    {
                        textUnit = "(" + parts[1];
                    }
                    column.HeaderText = testItemName + localizedText + textUnit;
                }
            }
        }

        private Dictionary<string, string> textToResourceKeyMap = new Dictionary<string, string>();

        private string FindBestMatchingKey(string text)
        {
            // 简单实现：直接查找完全匹配
            if (textToResourceKeyMap.TryGetValue(text, out string key))
            {
                return key;
            }

            // 高级实现：模糊匹配（可使用Levenshtein距离等算法）
            // 这里简化为查找包含部分文本的键
            //foreach (var pair in textToResourceKeyMap)
            //{
            //    if (pair.Key.Contains(text) || text.Contains(pair.Key))
            //    {
            //        return pair.Value;
            //    }
            //}

            return null;
        }

        string[] languageArray = null;
        string[] langArray = null;
        string[] StationNameArray = null;

        /// <summary>
        /// 根据静态LanguageId和指定索引获取测试项目名称
        /// </summary>
        /// <param name="index">数组位置索引</param>
        /// <returns>对应语言的测试项目名称，若索引或语言ID无效则返回null</returns>
        public string GetTestItemName(int index)
        {
            // 获取对应语言的数组
            GetInLaguageArray();

            // 检查语言ID和索引有效性
            if (languageArray == null || index < 0 || index >= languageArray.Length)
            {
                Console.WriteLine($"错误：无效的LanguageId={LanguageId} 或 索引={index}");
                return null;
            }

            return languageArray[index];
        }

        private void GetInLaguageArray()
        {
            switch (LanguageId)
            {
                case 0:
                    languageArray = testItemsName_Chinese;
                    langArray = kpisNameSets;
                    StationNameArray = stationNameSets;
                    break;
                case 1:
                    languageArray = TestItemsName_English;
                    langArray = kpisEnglishSets;
                    StationNameArray = stationNameSets_English;
                    break;
                case 2:
                    languageArray = TestItemsName_Thai;
                    langArray = kpisThaiSets;
                    StationNameArray = stationNameSets_Thai;
                    break;
                default:
                    languageArray = null;
                    langArray = null;
                    StationNameArray = null;
                    break;
            }
        }

        /// <summary>
        /// 更新TabPage的文本显示
        /// </summary>
        private void UpdateTabPageTexts()
        {
            GetInLaguageArray();

            // 确保stationNameSets数组有效
            if (stationNameSets == null || stationNameSets_English == null || stationNameSets_Thai == null) return;
            //if (stationNameSets.Length == 0 || stationNameSets_English.Length == 0 || stationNameSets_Thai.Length == 0) return;

            // 根据不同的工位模式更新TabPage文本
            if (chkDoubleStation.Checked)
            {
                // 双工位模式
                if (stationNameSets.Length >= 2)
                {
                    tabPage1.Text = StationNameArray[0];
                    tabPage2.Text = StationNameArray[1];

                    // 确保tabPage3被隐藏
                    if (tabPage3.Parent != null)
                    {
                        tabPage3.Parent = null;
                    }
                }
            }
            else if (chkLeftRight.Checked)
            {
                // 左右款模式
                if (stationNameSets.Length >= 2)
                {
                    tabPage1.Text = StationNameArray[0] + StationNameArray[1];
                    tabPage2.Text = StationNameArray[0];
                    tabPage3.Text = StationNameArray[1];

                    // 确保tabPage3可见
                    if (tabPage3.Parent == null)
                    {
                        tabPage3.Parent = tabControl_UploadData;
                    }
                }
            }
            else
            {
                tabControl_UploadData.SizeMode = TabSizeMode.Fixed;
                tabControl_UploadData.ItemSize = new Size(0, 1);
                tabPage2.Parent = null;
                tabPage3.Parent = null;
                splitContainer3.Panel1Collapsed = true;
            }
        }

        private async void chkBypassFixtureValidation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBypassFixtureValidation.Checked)
                {
                    this.InvokeAsync(() =>
                    {
                        lblRunningStatus.Text = resources.GetString("RSFixture_OK");
                        lblRunningStatus.ForeColor = Color.Green;
                        lblOperatePrompt.Text = resources.GetString("OT_WaitingScan");
                        lblOperatePrompt.ForeColor = Color.Black;
                        txtFixtureBinding.BackColor = Color.White;
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}