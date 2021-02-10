using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TReuters.LogLoader.Domain.Interfaces;

namespace TReuters.LogLoader.Infra.Data.UnitOfWork
{
    public class UnitOfWorkFactory<TConnection> : IUnitOfWorkFactory where TConnection : IDbConnection, new()
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private string connectionString;

        public IUnitOfWork Create(string connectionString)
        {

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString cannot be null");
            }
            this.connectionString = connectionString;
            return new UnitOfWork(CreateOpenConnection());
        }


        private IDbConnection CreateOpenConnection()
        {
            var conn = new TConnection();
            conn.ConnectionString = connectionString;

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("An error occured while connecting to the database. See innerException for details.", exception);
            }

            return conn;
        }
    }
}

