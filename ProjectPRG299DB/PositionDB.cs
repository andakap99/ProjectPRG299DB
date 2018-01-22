using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class PositionDB
    {
        public static List<Position> GetPosition()
        {
            List<Position> positionList = new List<Position>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT PositionID, PositionName, Description, " +
                "CompanyID, AdditionalNotes, ResumeID FROM dbo.Position";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("PositionID"),
                    cNOrd = reader.GetOrdinal("PositionName"),
                    cAOrd = reader.GetOrdinal("Description"),
                    cCOrd = reader.GetOrdinal("CompanyID"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes"),
                    cZCOrd = reader.GetOrdinal("ResumeID");
                while (reader.Read())
                {
                    Position posi = new Position();
                    posi.PositionID = reader.GetInt32(cIDOrd);
                    posi.PositionName = reader.GetString(cNOrd);
                    posi.Description = reader.GetString(cAOrd);
                    posi.CompanyID = reader.GetInt32(cCOrd);
                    posi.AdditionalNotes = reader.GetString(cSOrd);
                    posi.ResumeID = reader.GetInt32(cZCOrd);
                    positionList.Add(posi);
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
            return positionList;
        }
        public static List<Position> GetPositionByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeletePosition()
        {
            throw new System.NotImplementedException();
        }

        public static int AddPosition()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdatePosition()
        {
            throw new System.NotImplementedException();
        }
    }
}