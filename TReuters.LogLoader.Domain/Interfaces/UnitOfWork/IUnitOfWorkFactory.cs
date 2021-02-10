using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Domain.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(string connectionString);
    }
}
