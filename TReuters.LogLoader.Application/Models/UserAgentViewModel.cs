using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Application.Models
{
    public class UserAgentViewModel
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string ProductVersion { get; set; }
        public string SystemInformation { get; set; }
    }
}
