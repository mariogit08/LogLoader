using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TReuters.LogLoader.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IDbTransaction Transaction { get; }

        void Commit();
        void Rollback();
    }
}
