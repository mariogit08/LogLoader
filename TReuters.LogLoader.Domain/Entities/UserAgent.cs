using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Domain.Entities
{
    public class UserAgent
    {
        public UserAgent(string product, string productVersion, string systemInformation)
        {
            Product = product;
            ProductVersion = productVersion;
            SystemInformation = systemInformation;
        }

        public UserAgent(string product, string productVersion)
        {
            Product = product;
            ProductVersion = productVersion;            
        }

        public int Id { get; set; }
        public string Product { get; set; }
        public string ProductVersion { get; set; }
        public string SystemInformation { get; set; }
    }
}
