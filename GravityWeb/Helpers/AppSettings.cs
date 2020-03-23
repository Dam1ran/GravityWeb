using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityWeb.Helpers
{
    public class AppSettings
    {
        public string Site { get; set; }
        public string Audience { get; set; }
        public int ExpireTimeDays { get; set; }
        public string Secret { get; set; }
        public string FBAccessToken { get; set; }
    }
}
