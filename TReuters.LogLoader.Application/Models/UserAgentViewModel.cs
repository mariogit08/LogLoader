using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Application.Models
{
    public class UserAgentViewModel
    {
        public UserAgentViewModel(long id, string product, string productVersion, string systemInformation)
        {
            Id = id;
            Product = product;
            ProductVersion = productVersion;
            SystemInformation = systemInformation;
        }

        public long Id { get; set; }
        public string Product { get; set; }
        public string ProductVersion { get; set; }
        public string SystemInformation { get; set; }
    }
}
