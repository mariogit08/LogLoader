using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TReuters.LogLoader.Application.Interfaces;
using TReuters.LogLoader.Application.ViewModel;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Infra.Crosscutting;
using TReuters.LogLoader.Infra.Crosscutting.Helpers;

namespace TReuters.LogLoader.Application
{


    public class LogAppService : ILogAppService
    {
        const string generalRegexPattern = @"(?<ip>^.*?)[\s|-](?<user>.*?[\s|-])?\[(?<requestDate>.*?)\s(?<timezone>[-|+].*?)\]\s""(?<method>.*?)\s(?<requestUrl>.*?)\s(?<protocol>.*?\/)(?<protocolVersion>.*?)""\s(?<statusCode>\d*?)\s(?<portOriginUrlUserAgent>.*?)$";
        const string portRegexPattern = @"^\s*(?<port>.*?)\s";
        const string originUrlRegexPattern = @"(?<originUrl>http.*?)""\s*""";
        const string userAgentsGeneralRegexPattern = @"""\s""(?<useAgents>.*?)$";
        const string userAgentBasicRegexPattern = @"^(?<product>[A-Z].+?)\/(?<productVersion>.*?)$";
        const string userAgentWithSystemInfoRegexPattern = @"(?<product>[A-Z][a-zA-Z]{3,15})\/(?<productVersion>.*?)[\s""](\((?<systemInfo>.*?)\))?";

        Regex generalRegex = new Regex(generalRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex portRegex = new Regex(portRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex originUrlRegex = new Regex(originUrlRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex userAgentsGeneralRegex = new Regex(userAgentsGeneralRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex userAgentBasicRegex = new Regex(userAgentBasicRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex userAgentWithSystemInfoRegex = new Regex(userAgentWithSystemInfoRegexPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public async Task<Result> InsertLogBatchFileAsync(IFormFile file)
        {
            string logLines = await file.ReadAsStringAsync();
            var logs = new List<Log>();
            foreach (var logLine in logLines.Split(Environment.NewLine))
            {
                var generalMatch = generalRegex.Match(logLine);
                if (generalMatch.Success)
                {
                    Log log = ExtractRequiredFirstGroupLogInformation(generalMatch);
                    log = ExtractOptionalSecondGroupLogInformation(generalMatch, log);
                    logs.Add(log);
                }
            }

            return Result.Ok();
        }

        private Log ExtractOptionalSecondGroupLogInformation(Match generalMatch, Log log)
        {
            var portOriginUrlUserAgentMatch = generalMatch.Groups["portOriginUrlUserAgent"];

            if (portOriginUrlUserAgentMatch.Success)
            {
                var port = portRegex.Match(portOriginUrlUserAgentMatch.Value)?.Groups["port"]?.Value;
                var originUrl = originUrlRegex.Match(portOriginUrlUserAgentMatch.Value)?.Groups["originUrl"]?.Value;

                var userAgents = ExtractUserAgents(portOriginUrlUserAgentMatch.Value);

                int.TryParse(port, out int portInt);
                var completedLog = new Log(log.IP,
                                           log.UserIdentifier,
                                           log.RequestDate,
                                           log.Timezone,
                                           log.Method,
                                           log.RequestURL,
                                           log.Protocol,
                                           log.ProtocolVersion,
                                           log.StatusCodeResponse,
                                           portInt,
                                           originUrl,
                                           userAgents);
                return completedLog;
            }
            return log;
        }

        private Log ExtractRequiredFirstGroupLogInformation(Match generalMatch)
        {
            var ip = generalMatch.Groups["ip"].Value;
            var user = generalMatch.Groups["user"].Value;
            DateTime requestDate = ExtractAndConvertRequestDate(generalMatch);
            var timezone = generalMatch.Groups["timezone"].Value;
            var method = generalMatch.Groups["method"].Value;
            var requestUrl = generalMatch.Groups["requestUrl"].Value;
            var protocol = generalMatch.Groups["protocol"]?.Value.Replace("/", "");
            var protocolVersion = generalMatch.Groups["protocolVersion"].Value;
            int.TryParse(generalMatch.Groups["statusCode"].Value, out int statusCodeResponse);

            return new Log(ip, user, requestDate, timezone, method, requestUrl, protocol, protocolVersion, statusCodeResponse);
        }

        private static DateTime ExtractAndConvertRequestDate(Match generalMatch)
        {
            DateTime.TryParseExact(generalMatch.Groups["requestDate"].Value, "dd/MMM/yyyy:HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime requestDate);
            return requestDate;
        }




        private List<UserAgent> ExtractUserAgentsComplete(string text)
        {
            var basicUserAgents = userAgentWithSystemInfoRegex.Matches(text);
            var userAgents = new List<UserAgent>();
            foreach (var basicUserAgent in basicUserAgents)
            {
                var match = (basicUserAgent as Match);
                var product = match.Groups["product"].Value;
                var productVersion = match.Groups["productVersion"].Value;
                var systemInformation = match.Groups["systemInfo"].Value;
                userAgents.Add(new UserAgent(product, productVersion, systemInformation));
            }
            return userAgents;
        }


        private List<UserAgent> ExtractUserAgents(string text)
        {
            var userAgentGroup = userAgentsGeneralRegex.Match(text)?.Groups["useAgents"].Value;
            var userAgents = ExtractUserAgentsComplete(userAgentGroup);
            return userAgents;
        }

        private List<UserAgent> ExtractBasicUserAgents(string userAgentGroup)
        {
            var basicUserAgents = userAgentBasicRegex.Matches(userAgentGroup);
            var userAgents = new List<UserAgent>();
            foreach (var basicUserAgent in basicUserAgents)
            {
                var match = (basicUserAgent as Match);
                var product = match.Groups["product"].Value;
                var productVersion = match.Groups["productVersion"].Value;
                userAgents.Add(new UserAgent(product, productVersion));
            }
            return userAgents;
        }


        private List<UserAgent> ExtractUserAgentsWithSystemInformation(string userAgentGroup)
        {
            var basicUserAgents = userAgentWithSystemInfoRegex.Matches(userAgentGroup);
            var userAgents = new List<UserAgent>();
            foreach (var basicUserAgent in basicUserAgents)
            {
                var match = (basicUserAgent as Match);
                var product = match.Groups["product"].Value;
                var productVersion = match.Groups["productVersion"].Value;
                var systemInformation = match.Groups["systemInformation"].Value;
                userAgents.Add(new UserAgent(product, productVersion, systemInformation));
            }
            return userAgents;
        }
    }
}
