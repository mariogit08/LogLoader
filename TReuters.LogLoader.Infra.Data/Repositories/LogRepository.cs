using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Domain.Interfaces;
using TReuters.LogLoader.Domain.Interfaces.Repositories;
using TReuters.LogLoader.Infra.Crosscutting;

namespace TReuters.LogLoader.Infra.Data.Repositories
{

    public class LogRepository : ILogRepository
    {
        protected readonly IDbConnection _con;
        protected readonly IDbTransaction _transaction;

        public LogRepository(IUnitOfWork unitOfWork)
        {
            _con = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task<int> Insert(Log log)
        {

            try
            {
                var sql = @"INSERT INTO log
                                    (ip, 
                                    requestdate, 
                                    timezone, 
                                    method, 
                                    requesturl, 
                                    protocol, 
                                    protocolversion, 
                                    statuscoderesponse, 
                                    originurl, 
                                    port, 
                                    useridentifier)
                                    values(@ip, 
                                    @requestdate,
                                    @timezone, 
                                    @method, 
                                    @requesturl, 
                                    @protocol, 
                                    @protocolversion, 
                                    @statuscoderesponse, 
                                    @originurl, 
                                    @port, 
                                    @useridentifier) RETURNING logid";

                var logId = await _con.ExecuteScalarAsync<int>(sql,
                    new
                    {
                        log.IP,
                        log.RequestDate,
                        log.Timezone,
                        log.Method,
                        log.RequestURL,
                        log.Protocol,
                        log.ProtocolVersion,
                        log.StatusCodeResponse,
                        log.OriginUrl,
                        log.Port,
                        log.UserIdentifier
                    }, _transaction);


                return logId;
            }
            catch (Exception e)
            {
                var asd = e.Message;
            }
            return -1;
        }

        public async Task<bool> Update(Log log)
        {
            var logId = await _con.UpdateAsync(log, _transaction);
            return logId;
        }

        public async Task<Log> GetById(int logId)
        {
            const string getQuery = @"select ip, requestdate, timezone, method, requesturl, protocol, protocolversion, statuscoderesponse, originurl, port, 
                                        useridentifier, logid
                                        from public.log where logid = @logid and available = true";

            return await _con.QuerySingleAsync<Log>(getQuery, new { logId });
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            var lookup = new Dictionary<long, Log>();
            var sql = @"
                SELECT  l.logid, ip, requestdate, timezone, method, requesturl, protocol, protocolversion, statuscoderesponse, originurl, port, useridentifier, available,
	                    useragentid, product, productversion, systeminformation, u.logid
	            FROM log l 
	            inner join useragent u on u.logid = l.logid limit 10";

            Func<Log, UserAgent, Log> joinMethod = (l, u) =>
            {
                Log log;
                if (!lookup.TryGetValue(l.LogId.Value, out log))
                {
                    lookup.Add(l.LogId.Value, log = l);
                }
                if (log.UserAgents == null)
                    log.UserAgents = new List<UserAgent>();
                log.UserAgents.Add(u);
                return log;
            };

            try
            {
                return await _con.QueryAsync(sql, joinMethod, splitOn: "logid,useragentid");
            }
            catch (Exception e)
            {

                var asd = e.Message;
            }
            return null;
        }

        public async Task<IEnumerable<Log>> GetByFilter(string ip, string userAgentProduct, int? fromHour, int? fromMinute, int? toHour, int? toMinute)
        {
            const string findByAllQuery = @"select *
	                                        from public.log as l
	                                        inner join public.useragent as u
                                            on u.logid = l.logid
	                                        where
		                                        (@ip is null or ip = @ip) and 
		                                        (@userAgentProduct is null or u.product = @userAgentProduct) and
		                                        (@fromHour is null or extract(hour from requestdate) >= '@fromHour') and
		                                        (@fromMinute is null or extract(minute from requestdate) >= '@fromMinute') and
		                                        (@toHour is null or extract(hour from requestdate) <= '@toHour') and
		                                        (@toMinute is null or extract(minute from requestdate) <= '@toMinute') 
                                                and available = true";

            var results = await _con.QueryAsync<Log>(findByAllQuery, new { ip, userAgentProduct, fromHour, fromMinute, toHour, toMinute }, _transaction);
            return results;
        }


    }
}
