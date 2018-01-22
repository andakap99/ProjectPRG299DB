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
            finally
            {
                connection.Close();
            }
            return contactpositionList;
        }
        public static List<ContactPosition> GetContactPositionByRow()
        {
            ContactPosition contactposition = new ContactPosition();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactPositionID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.ContactPositions WHERE ContactPositionID = @ContactPositionID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ContactPositionID", contactpositionID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactPositionID"),
                    cNOrd = reader.GetOrdinal("Name"),
                    cAOrd = reader.GetOrdinal("Address"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Phone"),
                    cEOrd = reader.GetOrdinal("Email");
                while (reader.Read())
                {
                    contactposition.ContactPositionID = reader.GetInt32(cIDOrd);
                    contactposition.Name = reader.GetString(cNOrd);
                    contactposition.Address = reader.GetString(cAOrd);
                    contactposition.City = reader.GetString(cCOrd);
                    contactposition.State = reader.GetString(cSOrd);
                    contactposition.ZipCode = reader.GetString(cZCOrd);
                    contactposition.Phone = reader.GetString(cPOrd);
                    contactposition.Email = reader.GetString(cEOrd);
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
            return contactposition;
        }

        public static bool DeleteContactPosition()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM ContactPositions WHERE ContactPositionID = @ContactPositionID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ContactPositionID", cust.ContactPositionID);
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
            finally
            {
                connection.Close();
            }
        }

        public static int AddContactPosition()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT ContactPositions " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", contactposition.Name);
            insertCommand.Parameters.AddWithValue("@Address", contactposition.Address);
            insertCommand.Parameters.AddWithValue("@City", contactposition.City);
            insertCommand.Parameters.AddWithValue("@State", contactposition.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", contactposition.ZipCode);
            if (contactposition.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", contactposition.Phone);
            if (contactposition.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    contactposition.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('ContactPositions') FROM ContactPositions";
                SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                int vendorID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return vendorID;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateContactPosition()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE ContactPositions SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE ContactPositionID = @OldContactPositionID " +
                  "AND Name = @OldName " +
                  "AND Address = @OldAddress " +
                  "AND City = @OldCity " +
                  "AND State = @OldState " +
                  "AND ZipCode = @OldZipCode " +
                  "AND (Phone = @OldPhone " +
                      "OR Phone IS NULL AND @OldPhone IS NULL) " +
                  "AND (Email = @OldEmail " +
                      "OR Email IS NULL AND @OldEmail IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@NewName", newContactPosition.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newContactPosition.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newContactPosition.City);
            updateCommand.Parameters.AddWithValue("@NewState", newContactPosition.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newContactPosition.ZipCode);
            if (newContactPosition.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newContactPosition.Phone);
            if (newContactPosition.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newContactPosition.Email);

            updateCommand.Parameters.AddWithValue("@OldContactPositionID", oldContactPosition.ContactPositionID);
            updateCommand.Parameters.AddWithValue("@OldName", oldContactPosition.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldContactPosition.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldContactPosition.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldContactPosition.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldContactPosition.ZipCode);
            if (oldContactPosition.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldContactPosition.Phone);
            if (oldContactPosition.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldContactPosition.Email);

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
            finally
            {
                connection.Close();
            }
        }
    }
}