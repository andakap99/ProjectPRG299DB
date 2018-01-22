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
            string connectionString = "Data Source=localhost\\SQLExpress2014;Initial Catalog=PRG299DB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}