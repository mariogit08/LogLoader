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
            var connection = new NpgsqlConnection(configuration.GetSection($"ConnectionStrings:{connectionStringName}").Value);

            connection.Open();
            return connection;
        }
    }
}
