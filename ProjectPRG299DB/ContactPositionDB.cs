using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ContactPositionDB
    { 
        public static List<ContactPosition> GetCustomer()
        {
            List<ContactPosition> customerList = new List<ContactPosition>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, PositionID FROM dbo.ContactPosition";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cNOrd = reader.GetOrdinal("PositionID");
                while (reader.Read())
                {
                    ContactPosition conPos = new ContactPosition();
                    conPos.ContactID = reader.GetInt32(cIDOrd);
                    conPos.PositionID = reader.GetInt32(cNOrd);
                    customerList.Add(conPos);
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
            return customerList;
        }
        public static List<ContactPosition> GetContactPositionByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeleteContactPosition()
        {
            throw new System.NotImplementedException();
        }

        public static int AddContactPosition()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdateContactPosition()
        {
            throw new System.NotImplementedException();
        }
    }
}