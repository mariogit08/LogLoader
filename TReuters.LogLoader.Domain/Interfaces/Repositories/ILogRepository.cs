using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        
        Task<IEnumerable<Log>> GetAll();
        Task<IEnumerable<Log>> GetByFilter(string ip, string userAgentProduct, int? fromHour, int? fromMinute, int? toHour, int? toMinute);
        Task<Log> GetById(int logId);
        Task<int> Insert(Log log);
        Task<bool> Update(Log log);
    }

}
