using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader.Application.Adapters
{
    public static class LogAdapter
    {
        public static LogViewModel ToViewModel(this Log log)
        {
            if (log == null)
                return null;

            var userAgentsViewModel = log?.UserAgents?.Select(a => a?.ToViewModel())?.ToList();
            return new LogViewModel(log.LogId.Value,
                                    log.IP,
                                    log.UserIdentifier,
                                    log.RequestDate,
                                    log.Timezone,
                                    log.Method,
                                    log.RequestURL,
                                    log.Protocol,
                                    log.ProtocolVersion,
                                    log.StatusCodeResponse,
                                    log.Port,
                                    log.OriginUrl,
                                    userAgentsViewModel);
        }

        public static Log ToDomainModel(this LogViewModel log)
        {
            var userAgents = log.UserAgents.Select(a => a.ToDomainModel()).ToList();
            return new Log(log.LogId,
                           log.IP,
                           log.UserIdentifier,
                           log.RequestDate,
                           log.Timezone,
                           log.Method,
                           log.RequestURL,
                           log.Protocol,
                           log.ProtocolVersion,
                           log.StatusCodeResponse,
                           log.Port,
                           log.OriginUrl,
                           userAgents);
        }
    }
}
