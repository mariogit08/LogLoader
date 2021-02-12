using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TReuters.LogLoader.WebAPI.Models
{
    public class LogFilterParameters
    {
        public string ip { get; set; }
        public string userAgentProduct { get; set; }
        public int? fromHour { get; set; }
        public int? fromMinute { get; set; }
        public int? toHour { get; set; }
        public int? toMinute { get; set; }

    }
}
