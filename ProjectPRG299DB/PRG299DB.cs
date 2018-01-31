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
            connectionString.DataSource = "(LocalDB)\\MSSQLLocalDB";
            connectionString.AttachDBFilename = "|DataDirectory|\\PRG299.mdf";
            connectionString.IntegratedSecurity = true;
            string connectString = connectionString.ConnectionString;
                        
            SqlConnection connection = new SqlConnection(connectString);
            return connection;
        }
    }
}