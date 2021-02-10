using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader.Domain.Interfaces.DomainServices
{
    public interface ILogDomainService
    {
        Task<bool> Insert(List<Log> logs);
        Task<bool> Insert(Log logs);
        Task<bool> Update(Log log);
    }
}
