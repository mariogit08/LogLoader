using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Application.Models
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string UserIdentifier { get; set; }
        public DateTime RequestDate { get; set; }
        public string Timezone { get; set; }
        public string Method { get; set; }
        public string RequestURL { get; set; }
        public string Protocol { get; set; }
        public string ProtocolVersion { get; set; }
        public int StatusCodeResponse { get; set; }
        public int Port { get; set; }
        public string OriginUrl { get; set; }
        public IEnumerable<UserAgentViewModel> UserAgents { get; set; }
    }

    
}
