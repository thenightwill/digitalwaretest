using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace digitalwaretest.API.Models
{
    public class updatePassRequest
    {
        public int userId { get; set; }
        public string password { get; set; }
        public string newPass { get; set; }
    }
}