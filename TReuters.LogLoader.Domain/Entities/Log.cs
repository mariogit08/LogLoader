using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TReuters.LogLoader.Domain.Entities
{
    public class Log
    {
        public long? LogId { get; private set; }
        public string IP { get; private set; }
        public string UserIdentifier { get; private set; }
        public DateTime RequestDate { get; private set; }
        public string Timezone { get; private set; }
        public string Method { get; private set; }
        public string RequestURL { get; private set; }
        public string Protocol { get; private set; }
        public string ProtocolVersion { get; private set; }
        public int StatusCodeResponse { get; private set; }
        public int Port { get; private set; }
        public string OriginUrl { get; private set; }
        public List<UserAgent> UserAgents { get; private set; } = new List<UserAgent>();
        public bool Available { get; private set; } = true;


        public Log()
        {

        }
        

        public Log(string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgent> userAgents)
        {
            SetIP(iP);
            SetUserIdentifier(userIdentifier);
            SetRequestDate(requestDate);
            SetTimezone(timezone);
            SetMethod(method);
            SetRequestURL(requestURL);
            SetProtocol(protocol);
            SetProtocolVersion(protocolVersion);
            SetStatusCodeResponse(statusCodeResponse);
            SetPort(port);
            SetOriginUrl(originUrl);
            SetUserAgents(userAgents);
        }

        public Log(long logId, string iP, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse, int port, string originUrl, List<UserAgent> userAgents)
        {
            SetLogId(logId);
            SetIP(iP);
            SetUserIdentifier(userIdentifier);
            SetRequestDate(requestDate);
            SetTimezone(timezone);
            SetMethod(method);
            SetRequestURL(requestURL);
            SetProtocol(protocol);
            SetProtocolVersion(protocolVersion);
            SetStatusCodeResponse(statusCodeResponse);
            SetPort(port);
            SetOriginUrl(originUrl);
            SetUserAgents(userAgents);
        }

        public Log(string ip, string userIdentifier, DateTime requestDate, string timezone, string method, string requestURL, string protocol, string protocolVersion, int statusCodeResponse)
        {
            SetIP(ip);
            SetUserIdentifier(userIdentifier);
            SetRequestDate(requestDate);
            SetTimezone(timezone);
            SetMethod(method);
            SetRequestURL(requestURL);
            SetProtocol(protocol);
            SetProtocolVersion(protocolVersion);
            SetStatusCodeResponse(statusCodeResponse);                                
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

        public Log SetLogId(long? logId)
        {
            return Clone(logId: logId);
        }

        public Log SetIP(string ip)
        {
            var validIP = Regex.IsMatch(ip, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            if (validIP)
                return Clone(iP: ip);
            else
                throw new ArgumentException("Invalid IP");

        }

        public Log SetUserIdentifier(string userIdentifier)
        {
            if (userIdentifier == null)
                throw new ArgumentException("Timezone cannot be null");

            return Clone(userIdentifier: userIdentifier);
        }

        public Log SetRequestDate(DateTime requestDate)
        {
            DateTime.TryParse("1970-01-01 08:00:00", out DateTime requestDateOut);
            if (requestDate.CompareTo(requestDateOut) <= 0)
                throw new ArgumentException("set a valid date greater than 1970-01-01 08:00:00");

            return Clone(requestDate: requestDate);
        }

        public Log SetTimezone(string timezone)
        {
            if (timezone == null)
                throw new ArgumentException("Timezone cannot be null");
            return Clone(timezone: timezone);
        }

        public Log SetMethod(string method)
        {
            return Clone(method: method);
        }


        public Log SetRequestURL(string requestURL)
        {
            if (requestURL == null)
                throw new ArgumentException("RequestURL cannot be null");

            return Clone(requestURL: requestURL);
        }

        public Log SetProtocol(string protocol)
        {
            if (protocol == null)
                throw new ArgumentException("Protocol cannot be null");

            return Clone(protocol: protocol);
        }

        public Log SetProtocolVersion(string protocolVersion)
        {
            if (protocolVersion == null)
                throw new ArgumentException("ProtocolVersion cannot be null");
            return Clone(protocolVersion: protocolVersion);
        }

        public Log SetStatusCodeResponse(int statusCodeResponse)
        {
            if (statusCodeResponse <= 0)
                throw new ArgumentException("StatusCode cannot be negative a number");

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
