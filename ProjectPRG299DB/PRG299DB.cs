using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class PRG299DB
    {
        public static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = "(localdb)\\MSSQLLocalDB";
            connectionString.AttachDBFilename = "|DataDirectory|\\PRG299DB.mdf";
            connectionString.IntegratedSecurity = true;
            string connectString = connectionString.ConnectionString;
                        
            SqlConnection connection = new SqlConnection(connectString);
            return connection;
        }
    }
}