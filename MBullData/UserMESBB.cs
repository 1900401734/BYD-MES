using MBullData.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBullData
{
    public  class UserMESBB
    {
        [Display(Name = "DisplayNameUserId", ResourceType = typeof(Resources))]
        public string UserId { get; set; }
    }
}
