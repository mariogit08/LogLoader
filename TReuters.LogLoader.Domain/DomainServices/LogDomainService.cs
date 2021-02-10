using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Domain.Interfaces.DBContext;
using TReuters.LogLoader.Domain.Interfaces.DomainServices;

namespace TReuters.LogLoader.Domain.DomainServices
{
    public class LogDomainService : ILogDomainService
    {
        private readonly IDBContext _dBContext;

        public LogDomainService(IDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<bool> Insert(List<Log> logs)
        {
            try
            {
                foreach (var log in logs)
                {
                    var logId = await _dBContext.Log.Insert(log);

                    if (logId > 0)
                    {
                        if (!await InsertUserAgents(log, logId))
                            _dBContext.Rollback();
                    }
                    else
                    {
                        _dBContext.Rollback();
                        return false;
                    }
                }
            }
            catch
            {
                _dBContext.Rollback();
                return false;
            }
            _dBContext.Commit();
            return true;
        }

        public async Task<bool> Insert(Log log)
        {
            try
            {
                var logId = await _dBContext.Log.Insert(log);

                if (logId > 0)
                {
                    if (!await InsertUserAgents(log, logId))
                        _dBContext.Rollback();
                }
                else
                {
                    _dBContext.Rollback();
                    return false;
                }
            }
            catch
            {
                _dBContext.Rollback();
                return false;
            }
            _dBContext.Commit();
            return true;
        }

        public async Task<bool> Update(Log log)
        {
            try
            {
                var resultUpdate = await _dBContext.Log.Update(log);
                if (resultUpdate)
                    return await ReplaceUserAgents(log);
            }
            catch
            {
                return false;
            }
            _dBContext.Commit();
            return true;
        }

        private async Task<bool> ReplaceUserAgents(Log log)
        {
            await _dBContext.UserAgent.DeleteByLogId(log.LogId.Value);
            foreach (var userAgent in log.UserAgents)
            {
                var resultId = await _dBContext.UserAgent.Insert(userAgent);
                if (resultId <= 0)
                    return false;

                userAgent.UserAgentId = resultId;
            }

            return true;
        }

        private async Task<bool> InsertUserAgents(Log log, int logId)
        {
            foreach (var userAgent in log.UserAgents)
            {
                userAgent.LogId = logId;
                var resultId = await _dBContext.UserAgent.Insert(userAgent);
                if (resultId <= 0)
                    return false;
                userAgent.UserAgentId = resultId;
            }
            return true;
        }
    }
}
