using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader.Domain.Interfaces.Repositories
{
    public interface IUserAgentRepository
    {
        Task<bool> Delete(long userAgentId);
        Task<IEnumerable<UserAgent>> GetAll();
        Task<UserAgent> GetById(long userAgentId);
        Task<int> Insert(UserAgent userAgent);
        Task<bool> DeleteByLogId(long logId);
    }
}
