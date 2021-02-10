using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Domain.Interfaces.Repositories;

namespace TReuters.LogLoader.Domain.Interfaces.DBContext
{
    public interface IDBContext
    {
        ILogRepository Log { get; }
        IUserAgentRepository UserAgent { get; }

        void Commit();
        void Rollback();
    }
}
