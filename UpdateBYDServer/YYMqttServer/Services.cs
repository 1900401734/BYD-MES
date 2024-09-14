using HslCommunication.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYMqttServer
{
    public class Services
    {
        /// <summary>
        /// 获取客户端的版本号信息
        /// </summary>
        /// <returns>版本号</returns>
        [HslCommunication.Reflection.HslMqttApi()]
        public string GetClientApi()
        {
            return Version;
        }

        public string Version { get; set; } = "1.0.0";   // 服务器上当前最新版本的客户端的版本号，这个值在你更新客户端版本后，就需要手动更新
    }
}
