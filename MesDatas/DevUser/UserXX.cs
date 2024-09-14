using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesDatas.DevUser
{
    public class UserXX
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class UserXXBLL
    {
        public static string userpath = AppDomain.CurrentDomain.BaseDirectory + "DevUser\\UserXX.json";
        public static UserXX GetUserXX()
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserXX>(System.IO.File.ReadAllText(userpath));
            }catch (Exception ex)
            {
                return new UserXX() { UserName=string.Empty,Password=string.Empty};
            }
        }
    }
}
