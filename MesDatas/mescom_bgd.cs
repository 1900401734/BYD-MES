using MesDatas.MESModel;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 工艺部信息化组
{
    public struct MesConfig
    {
        public static string IP;
        public static string PORT;
        public static string URL;
        public static string Site;
        public static string UserName;
        public static string Password;
        public static string Resource;
        public static string Operation;
        public static string NcCode;
        public static int TimeOut;
        public static string Language;
    }

    public class MesIntegrationService
    {
        public static string ParamOUT;
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// MES交互1：用户验证
        /// </summary>
        /// <param name="userVerificationResult">用户验证结果</param>
        /// <param name="MESFeedback"></param>
        /// <param name="XMLOUT"></param>
        public static async Task<(bool isUserVerifySuccessfully, string MESFeedback, string XMLOUT)> VarifyUserLoginAsync()
        {
            string url = $"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}";
            string param =
                $"<PRODUCTION_REQUEST><USER>" +
                $"<SITE>{MesConfig.Site}</SITE>" +
                $"<NAME>{MesConfig.UserName}</NAME>" +
                $"<PWD>{MesConfig.Password}</PWD>" +
                $"</USER></PRODUCTION_REQUEST>";

            //string userVerifyResponse = await GetHtmlByPostAsync(url, param, MesConfig.TimeOut);
            string userVerifyResponse = await SendPoseRequestAsync(url, param, MesConfig.TimeOut);
            string XMLOUT = ParamOUT;

            bool isUserVerifySuccessfully = CutResult(userVerifyResponse);

            return (isUserVerifySuccessfully, userVerifyResponse, XMLOUT);
        }

        /// <summary>
        /// MES交互2：条码验证
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="isVerifySuccessfully"></param>
        /// <param name="barcodeverificationResponse"></param>
        /// <param name="XMLOUT"></param>
        public static async Task<(bool isVerifySuccessfully, string MESFeedback, string XMLOUT)> VarifyBarcodeAsync(string barcode)
        {
            string url = $"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}";
            string param =
                $"<PRODUCTION_REQUEST><START>" +
                $"<SFC_LIST>" +
                $"<SFC>" +
                $"<SITE>{MesConfig.Site}</SITE>" +
                $"<ACTIVITY>XML</ACTIVITY>" +
                $"<ID>{barcode}</ID>" +
                $"<RESOURCE>{MesConfig.Resource}</RESOURCE>" +
                $"<OPERATION>{MesConfig.Operation}</OPERATION>" +
                $"<USER>{MesConfig.UserName}</USER>" +
                $"<QTY></QTY>" +
                $"<DATE_TIME></DATE_TIME>" +
                $"<COMPLEX>N</COMPLEX>" +
                $"</SFC>" +
                $"</SFC_LIST>" +
                $"</START></PRODUCTION_REQUEST>!erpautogy03!1234567@byd";

            //string barcodeverificationResponse = await GetHtmlByPostAsync(url, param, MesConfig.TimeOut);
            string barcodeverificationResponse = await SendPoseRequestAsync(url, param, MesConfig.TimeOut);

            await Task.Delay(200);
            bool isVerifySuccessfully = CutResult(barcodeverificationResponse);
            string XMLOUT = ParamOUT;

            return (isVerifySuccessfully, barcodeverificationResponse, XMLOUT);
        }

        /// <summary>
        /// MES交互3：结果上传
        /// </summary>
        /// <param name="ProductStatus"></param>
        /// <param name="barcode"></param>
        /// <param name="fileVersion"></param>
        /// <param name="appVersion"></param>
        /// <param name="testItem"></param>
        /// <param name="验证结果"></param>
        /// <param name="resultFeedback"></param>
        /// <param name="XMLOUT"></param>
        public static async Task<(bool isUploadSuccessfully, string MESFeedback, string XMLOUT)> UploadBarcodeAsync(bool ProductStatus, string barcode, string fileVersion, string appVersion, string testItem)
        {
            string rawMESFeedback;

            if (ProductStatus == true)
            {
                rawMESFeedback = await PassValidate(barcode, fileVersion, appVersion, testItem);
            }
            else
            {
                rawMESFeedback = await ErrorValidate(barcode, fileVersion, appVersion, testItem);
            }

            bool uploadResult = CutResult(rawMESFeedback);
            string XMLOUT = ParamOUT;

            return (uploadResult, rawMESFeedback, XMLOUT);
        }

        /// <summary>
        /// MES交互4：绑定工单
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="isBindingSuccessfully">是否绑定成功</param>
        /// <param name="MESFeedback"></param>
        /// <param name="XMLOUT"></param>
        public static async Task<(bool isBindingSuccessfully, string MESFeedback, string XMLOUT)> BindWorkOrderAsync(string orderNumber)
        {
            string url = $"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}";
            string param =
                $"<PRODUCTION_REQUEST><RESOURCE_BANDING_SHOPORDER>" +
                $"<SITE>{MesConfig.Site}</SITE>" +
                $"<NAME>{MesConfig.UserName}</NAME>" +
                $"<PWD>{MesConfig.Password}</PWD>" +
                $"<RESOURCE>{MesConfig.Resource}</RESOURCE>" +
                $"<SHOPORDER>{orderNumber}</SHOPORDER>" +
                $"</RESOURCE_BANDING_SHOPORDER></PRODUCTION_REQUEST>";

            string MESFeedback = await SendPoseRequestAsync(url, param, MesConfig.TimeOut);
            bool isBindingSuccessfully = CutResult(MESFeedback);
            string XMLOUT = ParamOUT;

            return (isBindingSuccessfully, MESFeedback, XMLOUT);
        }

        /// <summary>
        /// MES交互5：工单信息查询
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="MESFeedback"></param>
        /// <param name="XMLOUT"></param>
        public static async Task<(string orderQuantity, string completedQuantity, string completeRate)> FetchOrderNumberInfo(string orderNumber)
        {
            string url = $"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}";
            string param =
                $"<PRODUCTION_REQUEST><SHOP_ORDER>" +
                $"<SITE>{MesConfig.Site}</SITE>" +
                $"<USER>{MesConfig.UserName}</USER>" +
                $"<ORDER>{orderNumber}</ORDER>" +
                $"<RESOURCE>{MesConfig.Resource}</RESOURCE>" +
                $"<SFC></SFC>" +
                $"</SHOP_ORDER></PRODUCTION_REQUEST>";

            string orderCheckResponse = await SendPoseRequestAsync(url, param, MesConfig.TimeOut);

            string orderQuantity = string.Empty;
            string completedQuantity = string.Empty;
            string completeRate = string.Empty;

            try
            {
                // 使用简单的字符串操作提取数据
                orderQuantity = ExtractBetweenTags(orderCheckResponse, "<ORDER_NUM>", "</ORDER_NUM>");
                completedQuantity = ExtractBetweenTags(orderCheckResponse, "<COMP_NUM>", "</COMP_NUM>");
                completeRate = ExtractBetweenTags(orderCheckResponse, "<COMP_RATE>", "</COMP_RATE>");

                Console.WriteLine($"查询工单 {orderNumber} 成功: 工单数量={orderQuantity}, 完成数量={completedQuantity}, 完成率={completeRate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"解析工单信息时发生错误: {ex.Message}");
            }

            // 返回结果元组
            return (orderQuantity, completedQuantity, completeRate);
        }

        private static async Task<string> PassValidate(string 产品条码, string 文件版本, string 软件版本, string 测试项)
        {
            try
            {
                string targetUrl = $"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}";
                string parameters =
                    $"PASS<PRODUCTION_REQUEST>" +
                    $"<COMPLETE>" +
                    $"<SFC_LIST>" +
                    $"<SFC>" +
                    $"<SITE>{MesConfig.Site}</SITE>" +
                    $"<ACTIVITY>XML</ACTIVITY>" +
                    $"<ID>{产品条码}</ID>" +
                    $"<RESOURCE>{MesConfig.Resource}</RESOURCE>" +
                    $"<OPERATION>{MesConfig.Operation}</OPERATION>" +
                    $"<USER>{MesConfig.UserName}</USER>" +
                    $"<QTY>1</QTY>" +
                    $"<DATE_TIME></DATE_TIME>" +
                    $"<DATE_STARTED></DATE_STARTED>" +
                    $"</SFC>" +
                    $"</SFC_LIST>" +
                    $"</COMPLETE>" +
                    $"</PRODUCTION_REQUEST>!erpautogy03!1234567@byd!PASS,{文件版本},{软件版本},{测试项}";

                return await SendPoseRequestAsync(targetUrl, parameters, MesConfig.TimeOut);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return $"Error: {ex.Message}";
            }
        }

        private static async Task<string> ErrorValidate(string 产品条码, string 文件版本, string 软件版本, string 测试项)
        {
            try
            {
                string url = $"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}";
                string param =
                    $"ERROR<PRODUCTION_REQUEST>" +
                    $"<NC_LOG_COMPLETE>" +
                    $"<SITE>{MesConfig.Site}</SITE>" +
                    $"<OWNER TYPE=\"USER\">{MesConfig.UserName}</OWNER>" +
                    $"<NC_CONTEXT>{产品条码}</NC_CONTEXT>" +
                    $"<QTY></QTY>" +
                    $"<IDENTIFIER></IDENTIFIER>" +
                    $"<FAILURE_ID></FAILURE_ID>" +
                    $"<DEFECT_COUNT>1</DEFECT_COUNT>" +
                    $"<COMMENTS></COMMENTS>" +
                    $"<DATE_TIME></DATE_TIME>" +
                    $"<RESOURCE>{MesConfig.Resource}</RESOURCE>" +
                    $"<OPERATION>{MesConfig.Operation}</OPERATION>" +
                    $"<ROOT_CAUSE_OPER></ROOT_CAUSE_OPER>" +
                    $"<NC_CODE>{MesConfig.NcCode}</NC_CODE>" +
                    $"<ACTIVITY>XML</ACTIVITY>" +
                    $"</NC_LOG_COMPLETE>" +
                    $"</PRODUCTION_REQUEST>!erpautogy03!1234567@byd!ERROR,{文件版本},{软件版本},{测试项}";

                return await SendPoseRequestAsync(url, param, MesConfig.TimeOut);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// 从MES响应报文中解析出是否成功
        /// </summary>
        /// <param name="html"></param>
        /// <returns>Y return true : N return false</returns>
        private static bool CutResult(string html)
        {
            return html.Contains("</b>Y</td>") ? true : false;
        }

        private static string ExtractBetweenTags(string source, string startTag, string endTag)
        {
            int startIndex = source.IndexOf(startTag);
            if (startIndex < 0) return string.Empty;

            startIndex += startTag.Length;
            int endIndex = source.IndexOf(endTag, startIndex);
            if (endIndex < 0) return string.Empty;

            return source.Substring(startIndex, endIndex - startIndex);
        }

        public static async Task<string> SendPoseRequestAsync(string URL, string Param, int TimeOut)
        {
            URL = $"{URL}&returnCharacter=utf8";
            Param = $"&message={Param}";
            ParamOUT = $"{Environment.NewLine}请求参数：{Param}";

            string responseContent;

            try
            {
                byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(Param);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Accept = "*/*";
                httpWebRequest.UserAgent = "Mozilla/4.0(compatible;MSIE 6.0;Windows NT 5.1;SV1;Maxthon;.NET CLR 1.1.4322)";
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.Timeout = TimeOut;
                httpWebRequest.ServicePoint.Expect100Continue = false;
                httpWebRequest.Headers.Add("language", MesConfig.Language);

                using (Stream requestStream = await httpWebRequest.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
                {
                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        responseContent = (await streamReader.ReadToEndAsync()).Replace("&lt;", "<").Replace("&gt;", ">");
                    }
                }

                httpWebRequest.Abort();
            }
            catch (Exception ex)
            {
                responseContent = ex.Message;
            }
            return responseContent;
        }
    }

    public class BydWorkCom
    {
        public static async Task<StatInforma> BydWorkStatisticsAsync(string 工单号, string barcode)
        {

            string Params = "&message=" + ("<PRODUCTION_REQUEST><SHOP_ORDER><SITE>" + MesConfig.Site + "</SITE><USER>" + MesConfig.UserName + "</USER><ORDER>" + 工单号 + "</ORDER><RESOURCE>" + MesConfig.Resource + "</RESOURCE><SFC>" + barcode + "</SFC></SHOP_ORDER></PRODUCTION_REQUEST>");
            string workInformation = PostMes("http://" + MesConfig.IP + ":" + MesConfig.PORT + MesConfig.URL, Params, MesConfig.TimeOut);
            bool workYN = workInformation.Replace("&lt;", "<").Replace("&gt;", ">").Contains("</b>Y</td>") ? true : false;
            StatInforma statInforma = new StatInforma();
            if (workYN == true)
            {

            }
            return statInforma;
        }

        public static string GetParamsAsy(string paraams)
        {
            string Params = "&message=" + (paraams);
            //string outMES = PostMes("http://" + MesConfig.IP + ":" + MesConfig.PORT + MesConfig.URL, Params, MesConfig.TimeOut);
            string outMES = PostMes($"http://{MesConfig.IP}:{MesConfig.PORT}{MesConfig.URL}", Params, MesConfig.TimeOut);
            return outMES;
        }

        /// <summary>
        /// Post MES接口调用
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Params"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string PostMes(string url, string Params, int timeout)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return "URL不能为空";
                }
                //TODO: 实现
                using (var handler = new HttpClientHandler())
                {
                    // 跳过证书验证
                    handler.ServerCertificateCustomValidationCallback = delegate { return true; };
                    using (var client = new HttpClient(handler))
                    {
                        // client.DefaultRequestHeaders.Authorization = CreateBasicHeader("admin", "api.admin");
                        // 设置请求超时时间
                        client.Timeout = TimeSpan.FromMilliseconds(timeout);
                        // 注册编码提供程序  
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        //设置编码格式
                        Encoding encoding = Encoding.GetEncoding("GB2312");
                        // 创建请求内容
                        HttpContent content = new StringContent(Params, encoding, "application/x-www-form-urlencoded");
                        // 发送POST请求
                        var response = client.PostAsync(url, content).Result;
                        // 确保请求成功
                        response.EnsureSuccessStatusCode();
                        // 读取响应内容
                        string result = response.Content.ReadAsStringAsync().Result;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;//TODO: 返回错误信息
            }
        }
    }
}

