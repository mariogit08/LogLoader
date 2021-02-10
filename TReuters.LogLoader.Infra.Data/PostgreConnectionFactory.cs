using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TReuters.LogLoader.Infra.Data
{
    public class PostgreConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration configuration;

        public PostgreConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection(string connectionStringName)
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=LogLoader;";
            var connection2 = configuration.GetSection($"ConnectionStrings:{connectionStringName}").Value;
            var connection = new NpgsqlConnection(configuration.GetSection($"ConnectionStrings:{connectionStringName}").Value);
            
            connection.Open();
            return connection;
        }
    }
}
