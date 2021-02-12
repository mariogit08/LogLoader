using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TReuters.LogLoader.Domain.Entities
{
    public class Log
    {
        private long? logId;
        private string iP;
        private string userIdentifier;
        private DateTime requestDate;
        private string timezone;
        private string requestURL;
        private string protocol;
        private string protocolVersion;
        private int statusCodeResponse;
        private int port;
        private string originUrl;
        private List<UserAgent> userAgents = new List<UserAgent>();
        private bool available = true;
        

        public Log()
        {

        }

        public Log(string ip, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse)
        {
            IP = ip;
            UserIdentifier = userIdentifier;
            RequestDate = requestDate;
            Timezone = timezone;
            Method = method;
            RequestURL = requestURL;
            Protocol = protocol;
            ProtocolVersion = protocolVersion;
            StatusCodeResponse = statusCodeResponse;
        }

        private Log(long? logId, string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgent> userAgents, bool available)
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
            Available = available;
        }

        public Log(string ip, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgent> userAgents) :
                   this(ip, userIdentifier, requestDate, timezone, method, requestURL, protocol, protocolVersion, statusCodeResponse)
        {
            Port = port;
            OriginUrl = originUrl;
            UserAgents = userAgents;
        }

        public Log(long logId, string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgent> userAgents)
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
        public long? LogId { get => logId; set => logId = value; }

        public string IP
        {
            get => iP;
            private set
            {
                var validIP = Regex.IsMatch(value, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                if (validIP)
                    iP = value;
                else
                    throw new ArgumentException("Invalid IP");
            }
        }
        public string UserIdentifier
        {
            get => userIdentifier; private set
            {
                if (value == null)
                    throw new ArgumentException("Timezone cannot be null");

                userIdentifier = value;
            }
        }
        public DateTime RequestDate
        {
            get => requestDate; private set
            {
                DateTime.TryParse("1970-01-01 08:00:00", out DateTime requestDateOut);
                if (value.CompareTo(requestDateOut) <= 0)
                    throw new ArgumentException("set a valid date greater than 1970-01-01 08:00:00");

                requestDate = value;
            }
        }
        public string Timezone
        {
            get => timezone; private set
            {
                if (value == null)
                    throw new ArgumentException("Timezone cannot be null");
                timezone = value;
            }
        }
        public string Method { get; private set; }
        public string RequestURL
        {
            get => requestURL; private set
            {
                if (value == null)
                    throw new ArgumentException("RequestURL cannot be null");

                requestURL = value;
            }
        }
        public string Protocol
        {
            get => protocol; private set
            {
                if (value == null)
                    throw new ArgumentException("Protocol cannot be null");
                protocol = value;
            }
        }
        public string ProtocolVersion
        {
            get => protocolVersion; private set
            {
                if (value == null)
                    throw new ArgumentException("ProtocolVersion cannot be null");

                protocolVersion = value;
            }
        }
        public int StatusCodeResponse
        {
            get => statusCodeResponse; private set
            {
                if (value <= 0)
                    throw new ArgumentException("StatusCode cannot be negative a number");
                statusCodeResponse = value;
            }
        }
        public int Port { get => port; private set => port = value; }
        public string OriginUrl { get => originUrl; private set => originUrl = value; }
        public List<UserAgent> UserAgents { get => userAgents; private set => userAgents = value; }
        public bool Available { get => available; private set => available = value; }

        public Log SetLogId(long? logId)
        {
            return Clone(logId: logId);
        }

        public Log SetIP(string ip)
        {
            return Clone(iP: ip);
        }

        public Log SetUserIdentifier(string userIdentifier)
        {
            return Clone(userIdentifier: userIdentifier);
        }

        public Log SetRequestDate(DateTime requestDate)
        {
            return Clone(requestDate: requestDate);
        }

        public Log SetTimezone(string timezone)
        {
            return Clone(timezone: timezone);
        }

        public Log SetMethod(string method)
        {
            return Clone(method: method);
        }


        public Log SetRequestURL(string requestURL)
        {
            return Clone(requestURL: requestURL);
        }

        public Log SetProtocol(string protocol)
        {
            return Clone(protocol: protocol);
        }

        public Log SetProtocolVersion(string protocolVersion)
        {
            return Clone(protocolVersion: protocolVersion);
        }

        public Log SetStatusCodeResponse(int statusCodeResponse)
        {
            return Clone(statusCodeResponse: statusCodeResponse);
        }

        public Log SetPort(int port)
        {
            return Clone(port: port);
        }

        public Log SetOriginUrl(string originUrl)
        {
            return Clone(originUrl: originUrl);
        }

        public Log SetUserAgents(List<UserAgent> userAgents)
        {
            return Clone(userAgents: userAgents);
        }

        public Log SetAvailable(bool available)
        {
            return Clone(available: available);
        }
        public Log Clone(long? logId = null, string iP = null, string userIdentifier = null, DateTime? requestDate = null, string timezone = null, string method = null, string requestURL = null, string protocol = null, string protocolVersion = null, int? statusCodeResponse = null, int? port = null, string originUrl = null, List<UserAgent> userAgents = null, bool? available = null)
        {
            var logId_ = logId ?? LogId;
            var iP_ = iP ?? IP;
            var userIdentifier_ = userIdentifier ?? UserIdentifier;
            var requestDate_ = requestDate ?? RequestDate;
            var timezone_ = timezone ?? Timezone;
            var method_ = method ?? Method;
            var requestURL_ = requestURL ?? RequestURL;
            var protocol_ = protocol ?? Protocol;
            var protocolVersion_ = protocolVersion ?? ProtocolVersion;
            var statusCodeResponse_ = statusCodeResponse ?? StatusCodeResponse;
            var port_ = port ?? Port;
            var originUrl_ = originUrl ?? OriginUrl;
            var userAgents_ = userAgents ?? UserAgents;
            var available_ = available ?? Available;

            return new Log(logId_, iP_, userIdentifier_, requestDate_, timezone_, method_, requestURL_, protocol_, protocolVersion_, statusCodeResponse_, port_, originUrl_, userAgents_, available_);
        }
    }
}
