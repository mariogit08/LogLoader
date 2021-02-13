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
            catch
            {
                return -1;
            }
        }

        public async Task<bool> Update(Log log)
        {
            try
            {
                var sql = @"UPDATE public.log
                            SET ip = @ip, requestdate = @requestdate, timezone = @timezone, method = @method, requesturl = @requesturl, protocol = @protocol, protocolversion = @protocolversion, statuscoderesponse = @statuscoderesponse, originurl = @originurl, port = @port, useridentifier = @useridentifier, available = @available
                            WHERE logid = @logId;";
                var result = await _con.ExecuteAsync(sql, log, _transaction) > 0;
                return result;
            }
            catch
            {
                return false;
            }            
        }

        public async Task<Log> GetById(int logId)
        {
            var whereClause = "where l.logid = @logId and available = true";
            var logById = (await GetWithAggregations(whereClause, new { logId })).FirstOrDefault();
            return logById;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            return await GetWithAggregations();
        }

        private async Task<IEnumerable<Log>> GetWithAggregations(string whereClause = null, object parameterObject = null)
        {
            var lookup = new Dictionary<long, Log>();
            var sql = $@"
                SELECT  l.logid, ip, requestdate, timezone, method, requesturl, protocol, protocolversion, statuscoderesponse, originurl, port, useridentifier, available,
	                    useragentid, product, productversion, systeminformation, u.logid
	            FROM log l 
	            left join useragent u on u.logid = l.logid
                {whereClause}";

            var objetoCache = new Dictionary<long, Log>();

            await _con.QueryAsync(sql, new[] {
                   typeof(Log),
                   typeof(UserAgent),
                }, objects =>
                {
                    var c = 0;
                    var objetoAtual = objects[c++] as Log;
                    var userAgent = objects[c++] as UserAgent;

                    Log objetoAtualLista = null;

                    if (!objetoCache.TryGetValue(objetoAtual.LogId.Value, out objetoAtualLista))
                        objetoCache.Add(objetoAtual.LogId.Value, objetoAtualLista = objetoAtual);

                    if (userAgent != null && !objetoAtualLista.UserAgents.Any(x => x.UserAgentId == userAgent?.UserAgentId))
                        objetoAtualLista.UserAgents.AsList().Add(userAgent);


                    return objetoAtualLista;
                }, parameterObject, splitOn: "logid, useragentid");

            return objetoCache.Values.ToList();
        }


        public async Task<IEnumerable<Log>> GetByFilter(string ip, string userAgentProduct, int? fromHour, int? fromMinute, int? toHour, int? toMinute)
        {
            var whereClause = @"where
                                                (@ip is null or ip = @ip) and
                                                (@userAgentProduct is null or u.product = @userAgentProduct) and
                                                (@fromHour is null or extract(hour from requestdate) >= @fromHour) and
                                                (@fromMinute is null or extract(minute from requestdate) >= @fromMinute) and
                                                (@toHour is null or extract(hour from requestdate) <= @toHour) and
                                                (@toMinute is null or extract(minute from requestdate) <= @toMinute) and available = true
                                                ";
            var paramObject = new { ip, userAgentProduct, fromHour, fromMinute, toHour, toMinute };

            try
            {
                var results = await GetWithAggregations(whereClause, paramObject);
                return results;
            }
            catch
            {
                throw;

            }            
        }
    }
}
