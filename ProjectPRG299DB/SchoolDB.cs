using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class SchoolDB
    {
        public static List<School> GetSchool()
        {
            List<School> SchoolList = new List<School>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT SchoolID, SchoolName, StreetName, " +
                "City, State, ZipCode, NumberOfYearsAttended, Graduated FROM dbo.School";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("SchoolID"),
                    cNOrd = reader.GetOrdinal("SchoolName"),
                    cAOrd = reader.GetOrdinal("StreetName"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("NumberOfYearsAttended"),
                    cEOrd = reader.GetOrdinal("Graduated");
                while (reader.Read())
                {
                    School scho = new School();
                    scho.SchoolID = reader.GetInt32(cIDOrd);
                    scho.SchoolName = reader.GetString(cNOrd);
                    scho.StreetName = reader.GetString(cAOrd);
                    scho.City = reader.GetString(cCOrd);
                    scho.State = reader.GetString(cSOrd);
                    scho.ZipCode = reader.GetString(cZCOrd);
                    scho.NumberOfYearsAttended = reader.GetString(cPOrd);
                    scho.Graduated = reader.GetString(cEOrd);
                    SchoolList.Add(scho);
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
            return SchoolList;
        }
        public static List<School> GetSchoolByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeleteSchool()
        {
            throw new System.NotImplementedException();
        }

        public static int AddSchool()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdateSchool()
        {
            throw new System.NotImplementedException();
        }
    }
}