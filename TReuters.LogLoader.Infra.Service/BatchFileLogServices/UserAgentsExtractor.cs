using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader.Infra.Service.BatchFileLogServices
{
    internal static class UserAgentsExtractor
    {
        const string userAgentWithSystemInfoRegexPattern = @"(?<product>[A-Z][a-zA-Z]{3,15})\/(?<productVersion>.*?)[\s""](\((?<systemInfo>.*?)\))?";
        const string userAgentsGeneralRegexPattern = @"""\s""(?<useAgents>.*?)$";

        static Regex userAgentsGeneralRegex = new Regex(userAgentsGeneralRegexPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        static Regex userAgentWithSystemInfoRegex = new Regex(userAgentWithSystemInfoRegexPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
        
        internal static List<UserAgent> ExtractUserAgents(this string text)
        {
            var userAgentGroup = userAgentsGeneralRegex.Match(text)?.Groups["useAgents"].Value;
            var userAgents = ExtractUserAgentsByGroups(userAgentGroup);
            return userAgents;
        }

        private static List<UserAgent> ExtractUserAgentsByGroups(string text)
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
    }
}
