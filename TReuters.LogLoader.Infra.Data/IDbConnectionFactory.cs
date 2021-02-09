using System;
using System.Data;

namespace TReuters.LogLoader.Infra.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(string connectionString);
    }
}
