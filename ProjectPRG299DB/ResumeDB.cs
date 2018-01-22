using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ResumeDB
    {
        public static List<Resume> GetResume()
        {
            List<Resume> resumeList = new List<Resume>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ResumeID, RSCDirectoryPath, SchoolID, " +
                "ClientID FROM dbo.Resume";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ResumeID"),
                    cNOrd = reader.GetOrdinal("RSCDirectoryPath"),
                    cAOrd = reader.GetOrdinal("SchoolID"),
                    cCOrd = reader.GetOrdinal("ClientID");
                while (reader.Read())
                {
                    Resume resu = new Resume();
                    resu.ResumeID = reader.GetInt32(cIDOrd);
                    resu.RSCDirectoryPath = reader.GetString(cNOrd);
                    resu.SchoolID = reader.GetInt32(cAOrd);
                    resu.ClientID = reader.GetInt32(cCOrd);
                    resumeList.Add(resu);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return resumeList;
        }
        public static List<Resume> GetResumeByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeleteResume()
        {
            throw new System.NotImplementedException();
        }

        public static int AddResume()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdateResume()
        {
            throw new System.NotImplementedException();
        }
    }
}