using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ContactPositionDB
    { 
        public static List<ContactPosition> GetContactPosition()
        {
            List<ContactPosition> contactpositionList = new List<ContactPosition>();
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
                    contactpositionList.Add(conPos);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return contactpositionList;
        }
        public static ContactPosition GetContactPositionByRow(int contactpositionID)
        {
            ContactPosition conpos = new ContactPosition();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, PositionID FROM dbo.ContactPosition WHERE ContactID = @ContactID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ContactID", contactpositionID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cNOrd = reader.GetOrdinal("PositionID");
                while (reader.Read())
                {

                    conpos.ContactID = reader.GetInt32(cIDOrd);
                    conpos.PositionID = reader.GetInt32(cNOrd);

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return conpos;
        }

        public static bool DeleteContactPosition(int contactID)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM ContactPosition WHERE ContactID = @ContactID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ContactID", contactID);
            try
            {
                connection.Open();
                int count = DeleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int AddContactPosition(ContactPosition contactposition)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT ContactPosition " +
                  "(ContactID, PositionID) " +
                "VALUES (@ContactID, @PositionID);";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@ContactID", contactposition.ContactID);
            insertCommand.Parameters.AddWithValue("@PositionID", contactposition.PositionID);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('ContactPosition') FROM ContactPosition";
                SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                int vendorID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return vendorID;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateContactPosition(ContactPosition oldContactPosition, ContactPosition newContactPosition)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE ContactPosition SET " +
                  "ContactID = @NewContactID, " +
                  "PositionID = @NewPositionID " +
                "WHERE ContactID = @OldContactID " +
                  "AND PositionID = @OldPositionID";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            
                updateCommand.Parameters.AddWithValue("@NewContactID", newContactPosition.ContactID);
            
                updateCommand.Parameters.AddWithValue("@NewPositionID", newContactPosition.PositionID);

                updateCommand.Parameters.AddWithValue("@OldContactID", oldContactPosition.ContactID);
            
                updateCommand.Parameters.AddWithValue("@OldPositionID", oldContactPosition.PositionID);

            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}