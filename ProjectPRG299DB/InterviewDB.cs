using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class InterviewDB
    {
        public static List<Interview> GetInterview()
        {
            List<Interview> interviewList = new List<Interview>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CustomerID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Interview";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Interview interv = new Interview();
                    interv.InterviewID = reader.GetInt32(cIDOrd);
                    interv.PositionID = reader.GetInt32(cNOrd);
                    interv.CompanyID = reader.GetInt32(cAOrd);
                    interv.DateTimeInterview = reader.GetDateTime(cCOrd);
                    interv.AdditionalNotes = reader.GetString(cSOrd);
                    interviewList.Add(interv);
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
            return interviewList;
        }
        public static List<Interview> GetInterviewByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeleteInterview()
        {
            throw new System.NotImplementedException();
        }

        public static int AddInterview()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdateInterview()
        {
            throw new System.NotImplementedException();
        }
    }
}