using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Domain.Interfaces;
using TReuters.LogLoader.Domain.Interfaces.Repositories;

namespace TReuters.LogLoader.Infra.Data.Repositories
{
    public class UserAgentRepository : IUserAgentRepository
    {
        protected readonly IDbConnection _con;
        protected readonly IDbTransaction _transaction;

        public UserAgentRepository(IUnitOfWork unitOfWork)
        {
            _con = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
            ConfigureTableNameDapper();
        }
        public async Task<int> Insert(UserAgent userAgent)
        {
            try
            {
                var sql = @"INSERT INTO public.useragent(
                                        product, productversion, systeminformation, logid)
	                               VALUES(@Product, @ProductVersion, @SystemInformation, @LogId) RETURNING useragentid";
                var id = await _con.ExecuteScalarAsync<int>(sql, userAgent, _transaction);
                return id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(long userAgentId)
        {
            var sql = @"DELETE FROM public.useragent
                       WHERE useragentid = @userAgentId";
            var affectedLines = await _con.ExecuteAsync(sql, new { userAgentId }, _transaction);
            return affectedLines > 0;
        }

        public async Task<bool> DeleteByLogId(long logId)
        {
            var sql = @"DELETE FROM public.useragent
                       WHERE logid = @logId";
            var affectedLines = await _con.ExecuteAsync(sql, new { logId }, _transaction);
            return affectedLines > 0;
        }

        public async Task<UserAgent> GetById(long userAgentId)
        {
            const string getQuery = @"select useragentid, 
                                             product, 
                                             productversion, 
                                             systeminformation, 
                                             logid
                                             from public.useragent 
                                             where useragentid = @userAgentId";

            return await _con.QuerySingleAsync<UserAgent>(getQuery, new { userAgentId });
        }

        public async Task<IEnumerable<UserAgent>> GetAll()
        {
            var userAgentId = await _con.GetAllAsync<UserAgent>(_transaction);
            return userAgentId;
        }

        private static void ConfigureTableNameDapper()
        {
            SqlMapperExtensions.TableNameMapper = entityType =>
            {
                if (entityType == typeof(UserAgent))
                {
                    return "useragent";
                }
                throw new Exception($"Not supported entity type {entityType}");
            };
            
        }

    }
}

