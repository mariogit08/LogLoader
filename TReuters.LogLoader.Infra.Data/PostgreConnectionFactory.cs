using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TReuters.LogLoader.Infra.Data
{
    public class PostgreConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection(string connectionString)
        {
            var cs = @"Host=""postgres://wkogadwc:8gZPLt0eSdse71rykxUvPr0td1a94h1i@motty.db.elephantsql.com:5432/wkogadwc"";Username = mylogin; Password = mypass; Database = mydatabase";



            var con = new NpgsqlConnection();
            con.Host = 
            con.Open();
            return con;            
        }
    }
}
