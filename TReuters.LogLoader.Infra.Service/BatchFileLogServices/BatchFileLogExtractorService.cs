using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Infra.Crosscutting;
using TReuters.LogLoader.Infra.Crosscutting.Helpers;
using TReuters.LogLoader.Infra.Service.BatchFileLogServices;
using TReuters.LogLoader.Infra.Service.Interfaces;

namespace TReuters.LogLoader.Infra.Service
{
    public class BatchFileLogExtractorService : IBatchFileLogExtractorService
    {
        const string generalRegexPattern = @"(?<ip>^.*?)[\s|-](?<user>.*?[\s|-])?\[(?<requestDate>.*?)\s(?<timezone>[-|+].*?)\]\s""(?<method>.*?)\s(?<requestUrl>.*?)\s(?<protocol>.*?\/)(?<protocolVersion>.*?)""\s(?<statusCode>\d*?)\s(?<portOriginUrlUserAgent>.*?)$";
        const string portRegexPattern = @"^\s*(?<port>.*?)\s";
        const string originUrlRegexPattern = @"(?<originUrl>http.*?)""\s*""";

        Regex generalRegex = new Regex(generalRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex portRegex = new Regex(portRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        Regex originUrlRegex = new Regex(originUrlRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public async Task<Result<List<Log>>> ExtractLogsFromFile(IFormFile file)
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
            return Result.Ok(logs);
        }
        private Log ExtractRequiredFirstGroupLogInformation(Match generalMatch)
        {
            var ip = generalMatch.Groups["ip"].Value;
            var user = generalMatch.Groups["user"].Value;
            var requestDate = generalMatch.ExtractAndConvertRequestDate();
            var timezone = generalMatch.Groups["timezone"].Value;
            var method = generalMatch.Groups["method"].Value;
            var requestUrl = generalMatch.Groups["requestUrl"].Value;
            var protocol = generalMatch.Groups["protocol"]?.Value.Replace("/", "");
            var protocolVersion = generalMatch.Groups["protocolVersion"].Value;
            int.TryParse(generalMatch.Groups["statusCode"].Value, out int statusCodeResponse);

            return new Log(ip, user, requestDate, timezone, method, requestUrl, protocol, protocolVersion, statusCodeResponse);
        }

        private Log ExtractOptionalSecondGroupLogInformation(Match generalMatch, Log log)
        {
            var portOriginUrlUserAgentMatch = generalMatch.Groups["portOriginUrlUserAgent"];

            if (portOriginUrlUserAgentMatch.Success)
            {
                var port = portRegex.Match(portOriginUrlUserAgentMatch.Value)?.Groups["port"]?.Value;
                var originUrl = originUrlRegex.Match(portOriginUrlUserAgentMatch.Value)?.Groups["originUrl"]?.Value;

                var userAgents = portOriginUrlUserAgentMatch.Value.ExtractUserAgents();

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
    }
}
