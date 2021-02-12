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

        public UserAgent(long id, string product, string productVersion, string systemInformation)
        {
            UserAgentId = id;
            Product = product;
            ProductVersion = productVersion;
            SystemInformation = systemInformation;
        }

        public UserAgent(long useragentid, string product, string productversion, string systeminformation, long logid)
        {
            UserAgentId = useragentid;
            Product = product;
            ProductVersion = productversion;
            SystemInformation = systeminformation;
            LogId = logid;
        }

        public long UserAgentId { get; set; }
        public string Product { get; set; }
        public string ProductVersion { get; set; }
        public string SystemInformation { get; set; }

        public long LogId { get; set; }
    }
}
