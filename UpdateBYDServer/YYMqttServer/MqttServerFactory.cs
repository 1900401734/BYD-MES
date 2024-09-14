using HslCommunication;
using HslCommunication.Core;
using HslCommunication.MQTT;
using HslCommunication.Reflection;
using YYMqttServer.UserModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYMqttServer;

namespace YYMqttClient
{
    public class MqttServerFactory
    {
        private static Logger loggerMqttServer = LogManager.GetLogger("MqttServerLog");
        private  MqttServer mqttServer;

        public void MqttServerStart(int port=1883)
        {
            try
            {
                mqttServer = new MqttServer();
                //登录验证
                //mqttServer.ClientVerification += MqttServer_ClientVerification;
                //客服端连接
               // mqttServer.OnClientConnected += MqttServer_OnClientConnected;
                // 客户端断开连接
              //  mqttServer.OnClientDisConnected += MqttServer_OnClientDisConnected;


                //处理发布订阅
               // mqttServer.OnClientApplicationMessageReceive += MqttServer_OnClientApplicationMessageReceive;

                // 启用文件管理服务  Enable file management service
              //  mqttServer.UseFileServer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileServer"));


                // 如果需要启用文件管理的权限，就编写 FileOperateVerification 事件
               // mqttServer.FileOperateVerification += MqttServer_FileOperateVerification;

                // 如果需要启用远程调用的权限，就编写 RpcApiVerification 事件
               // mqttServer.OnFileChangedEvent += MqttServer_OnFileChangedEvent;

                // 注册当前的服务，当下面所有的带特性HslMqttApi的方法都暴露出来，其中，设备温度仅支持admin账户设定
                // Register the current service, when all the following methods with characteristic HslMqttApi are exposed, 
                // the device temperature only supports the admin account setting
                // 实例化服务
                Services services = new Services();
                mqttServer.RegisterMqttRpcApi(services);
                mqttServer.ServerStart(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Start Failed: " + ex.Message);
            }
        }

        #region 用户验证管理
        private int MqttServer_ClientVerification(MqttSession session, string clientId, string userName, string passwrod)
        {

            // 这里是验证用户名和密码，如果验证通过，那么返回0，否则返回4
            UserInfo user = new UserInfo() { UserName = "admin", Password = "123456" };
            if (userName.Equals(user.UserName) && passwrod.Equals(user.Password))
            {
                loggerMqttServer.Trace($"客服端: {clientId} | IP: {session.EndPoint}  | 登录状态: OK");
                return 0;
            }
            else
            {
                loggerMqttServer.Trace($"客服端: {clientId} | IP: {session.EndPoint}  | 登录状态: NG");
                //return 0;
                return 4;
            }
        }
        #endregion

        #region 客服端管理
        /// <summary>
        /// 当客户端连接上来时，可以立即返回一些数据内容信息
        /// </summary>
        /// <param name="session"></param>
        private void MqttServer_OnClientConnected(MqttSession session)
        {
            // 当客户端连接上来时，可以立即返回一些数据内容信息
            mqttServer.PublishTopicPayload(session, "HslMqtt", Encoding.UTF8.GetBytes("This is a test message"));
        }

        private void MqttServer_OnClientDisConnected(MqttSession session)
        {
            loggerMqttServer.Trace($"客服端: {session.ClientId} | IP: {session.EndPoint}  | 断开连接");
        }
        #endregion

        #region 客服端消息处理
        private void MqttServer_OnClientApplicationMessageReceive(MqttSession session, MqttClientApplicationMessage message)
        {
            loggerMqttServer.Trace($"ClientId:[{message.ClientId}] Topic:[{message.Topic}] Payload: {Encoding.UTF8.GetString(message.Payload)}");
            // 这里是处理客户端发布消息的，如果消息是MQTT协议，那么会触发这个方法
            if (session.Protocol == "MQTT")
            {
                // 正常的MQTT协议的内容，进行发布订阅操作

                // 此处举例是打印，当然了，你也可以做一些其他的处理，当所有的客户端进行发布操作的时候，就会触发当前的方法
                // The example here is printing. Of course, you can also do some other processing. When all clients perform publishing operations, the current method will be triggered.
                //Console.WriteLine($"ClientId:[{message.ClientId}] Topic:[{message.Topic}] Payload: {Encoding.UTF8.GetString(message.Payload)}");
                loggerMqttServer.Trace($"MQTT OK");
                // 如果你想阻止当前的消息推送给客户端，那么可以按照下面的操作
                // If you want to prevent the current message from being pushed to the client, then you can follow
                // message.IsCancelPublish = true;
            }
            else
            {
                // 同步网络的情况，正常情况都是需要返回信息的，否则客户端就会引发接收超时的异常
                // In the case of synchronous network, the normal situation is to return information, otherwise the client will cause an exception of receiving timeout
                if (message.Topic == "GetA")
                {
                    mqttServer.PublishTopicPayload(session, "Success", Encoding.UTF8.GetBytes("这是你获取的数据A"));
                }
                else if (message.Topic == "GetB")
                {
                    mqttServer.PublishTopicPayload(session, "Success", Encoding.UTF8.GetBytes("这是你获取的数据B"));
                }
                else
                {
                    // 如果需要返回错误的信息，客户端直接IsSuccess为False，然后这个Message就是下面的字符串
                    // If you need to return wrong information, the client directly IsSuccess is False, and then this Message is the following string
                    mqttServer.ReportOperateResult(session, "当前的操作不支持");
                }
            }
        }
        #endregion

        #region 文件操作
        private OperateResult MqttServer_FileOperateVerification(MqttSession session, byte code, string[] groups, string[] fileNames)
        {
            // 此处举个例子，如果用户名为空，不允许删除操作，当然了，你可以自定义任何的规则
            if (string.IsNullOrEmpty(session.UserName))
            {
                if (code == MqttControlMessage.FileDelete) return new OperateResult("Null name not allowed delete!");
            }
            return OperateResult.CreateSuccessResult();
            //return OperateResult
        }
        private void MqttServer_OnFileChangedEvent(MqttSession session, MqttFileOperateInfo operateInfo)
        {
            // 当文件发生改变的时候触发的事件，可以用来记录日志等
            // The event triggered when the file changes, can be used to record logs, etc
            loggerMqttServer.Trace($"File changed: {operateInfo} from {session.ClientId} {session.UserName} {session.EndPoint}");
        }
        #endregion

        #region 发送消息

        public void SendPublishTopic(string topic,string payload,bool retain = true)
        {
            mqttServer.PublishTopicPayload(topic, Encoding.UTF8.GetBytes(payload), retain);
        }

        public async void SendPublishTopicAsync(string topic, string payload, bool retain = true)
        {
            await Task.Run(()=> SendPublishTopic(topic, payload, retain));
        }

        public void SendPublishTopic(string clientId, string topic, string payload, bool retain = true)
        {
            mqttServer.PublishTopicPayload(clientId, topic, Encoding.UTF8.GetBytes(payload), retain);
        }

        public async void SendPublishTopicAsync(string clientId, string topic, string payload, bool retain = true)
        {
            await Task.Run(() => SendPublishTopic(clientId, topic, payload, retain));
        }

        public void SendPublishAllClientTopic(string topic, string payload, bool retain = true)
        {
            mqttServer.PublishAllClientTopicPayload(topic, Encoding.UTF8.GetBytes(payload), retain);
        }
        public async void SendPublishAllClientTopicAsync(string topic, string payload, bool retain = true)
        {
            await Task.Run(() => SendPublishAllClientTopic(topic, payload, retain));
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // 此处示例是发布给指定的主题
            // The example here is posted to the specified topic
            mqttServer.PublishTopicPayload("Topic", Encoding.UTF8.GetBytes("Test Data"));

            // 此处示例是发布给指定的clientId
            // The example here is posted to the specified clientId
            mqttServer.PublishTopicPayload("ClientId1", "Topic", Encoding.UTF8.GetBytes("Test Data"));

            // 此处的示例是发布给所有的客户端，不管这个客户端有没有订阅相关的主题
            // The example here is published to all clients, regardless of whether the client is subscribed to related topics
            mqttServer.PublishAllClientTopicPayload("Topic", Encoding.UTF8.GetBytes("Test Data"));




            // 如果要消息驻留，意思就是当其他的客户端订阅了驻留的主题，会立即收到一条最后更新的数据内容，那么上面的代码可以修改成下面的形式
            // If you want the message to reside, it means that when other clients subscribe to the resident topic, they will immediately receive a last updated data content, then the above code can be modified into the following form
            mqttServer.PublishTopicPayload("Topic", Encoding.UTF8.GetBytes("Test Data"), true);
            mqttServer.PublishTopicPayload("ClientId1", "Topic", Encoding.UTF8.GetBytes("Test Data"), true);
            mqttServer.PublishAllClientTopicPayload("Topic", Encoding.UTF8.GetBytes("Test Data"), true);
        }
        #endregion


    }
}
