using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TReuters.LogLoader.Domain.Entities
{

    public class Log
    {
        public int Id { get; set; }
        private string iP;
        private DateTime requestDate;
        private string timezone;
        private string method;
        private string requestURL;
        private string protocol;
        private string protocolVersion;
        private int statusCodeResponse;       

        public Log(string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgent> userAgents)
        {
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

        public Log(string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse)
        {
            IP = iP;
            UserIdentifier = userIdentifier;
            RequestDate = requestDate;
            Timezone = timezone;
            Method = method;
            RequestURL = requestURL;
            Protocol = protocol;
            ProtocolVersion = protocolVersion;
            StatusCodeResponse = statusCodeResponse;
        }     

        public string IP
        {
            get => iP;
            set
            {
                var validIP = Regex.IsMatch(value, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                if (validIP)
                    iP = value;
                else
                    throw new ArgumentException("Invalid IP");
            }
        }
        public string UserIdentifier { get; set; }
        public DateTime RequestDate
        {
            get => requestDate;
            set
            {
                if (value.CompareTo(DateTime.MinValue) <= 0)
                    throw new ArgumentException("set a valid date greater than 1970-01-01 08:00:00");
                else
                    requestDate = value;
            }
        }

        public string Timezone
        {
            get => timezone;
            set
            {
                if (value == null)
                    throw new ArgumentException("Timezone cannot be null");
                /*if()*///TimeZone validantio two formats - Conversor from GMT+11:00 to +01100 format and virse versa

                timezone = value;
            }
        }
        public string Method
        {
            get => method; set
            {
                if (value == null)
                    throw new ArgumentException("Method cannot be null");

                method = value;
            }
        }

        public string RequestURL
        {
            get => requestURL;
            set
            {
                if (value == null)
                    throw new ArgumentException("RequestURL cannot be null");

                requestURL = value;
            }
        }
        public string Protocol
        {
            get => protocol;
            set
            {
                if (value == null)
                    throw new ArgumentException("Protocol cannot be null");

                protocol = value;
            }
        }

        public string ProtocolVersion
        {
            get => protocolVersion;
            set
            {
                if (value == null)
                    throw new ArgumentException("ProtocolVersion cannot be null");

                protocolVersion = value;
            }
        }
        public int StatusCodeResponse
        {
            get => statusCodeResponse;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("StatusCode cannot be negative a number");

                statusCodeResponse = value;
            }
        }
        public int Port { get; set; }
        public string OriginUrl { get; set; }
        public List<UserAgent> UserAgents { get; set; } = new List<UserAgent>();

        public override bool Equals(object obj)
        {
            return obj is Log log &&
                   Id == log.Id &&
                   IP == log.IP &&
                   UserIdentifier == log.UserIdentifier &&
                   RequestDate == log.RequestDate &&
                   Timezone == log.Timezone &&
                   Method == log.Method &&
                   RequestURL == log.RequestURL &&
                   Protocol == log.Protocol &&
                   ProtocolVersion == log.ProtocolVersion &&
                   StatusCodeResponse == log.StatusCodeResponse &&
                   Port == log.Port &&
                   OriginUrl == log.OriginUrl &&
                   EqualityComparer<IEnumerable<UserAgent>>.Default.Equals(UserAgents, log.UserAgents);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(iP);
            hash.Add(requestDate);
            hash.Add(timezone);
            hash.Add(method);
            hash.Add(requestURL);
            hash.Add(protocol);
            hash.Add(protocolVersion);
            hash.Add(statusCodeResponse);
            hash.Add(IP);
            hash.Add(UserIdentifier);
            hash.Add(RequestDate);
            hash.Add(Timezone);
            hash.Add(Method);
            hash.Add(RequestURL);
            hash.Add(Protocol);
            hash.Add(ProtocolVersion);
            hash.Add(StatusCodeResponse);
            hash.Add(Port);
            hash.Add(OriginUrl);
            hash.Add(UserAgents);
            return hash.ToHashCode();
        }
    }
}
