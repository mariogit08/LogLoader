using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Application.Models
{
    public class LogViewModel
    {
        public LogViewModel(long logId, string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgentViewModel> userAgents)
        {
            LogId = logId;
            IP = iP;
            UserIdentifier = userIdentifier;
            RequestDate = requestDate;
            Timezone = timezone;
            Method = method;
            RequestURL = requestURL;
            Protocol = protocol;
            ProtocolVersion = protocolVersion;
            StatusCodeResponse = statusCodeResponse;
            Port = port;
            OriginUrl = originUrl;
            UserAgents = userAgents;
        }

        public long LogId { get; set; }
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
        public List<UserAgentViewModel> UserAgents { get; set; }
    }
}
