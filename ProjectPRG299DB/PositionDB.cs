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
            Position position = new Position();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT PositionID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Positions WHERE PositionID = @PositionID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@PositionID", positionID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("PositionID"),
                    cNOrd = reader.GetOrdinal("Name"),
                    cAOrd = reader.GetOrdinal("Address"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Phone"),
                    cEOrd = reader.GetOrdinal("Email");
                while (reader.Read())
                {
                    position.PositionID = reader.GetInt32(cIDOrd);
                    position.Name = reader.GetString(cNOrd);
                    position.Address = reader.GetString(cAOrd);
                    position.City = reader.GetString(cCOrd);
                    position.State = reader.GetString(cSOrd);
                    position.ZipCode = reader.GetString(cZCOrd);
                    position.Phone = reader.GetString(cPOrd);
                    position.Email = reader.GetString(cEOrd);
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
            return position;
        }

        public static bool DeletePosition()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Positions WHERE PositionID = @PositionID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@PositionID", cust.PositionID);
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

        public static int AddPosition()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Positions " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", position.Name);
            insertCommand.Parameters.AddWithValue("@Address", position.Address);
            insertCommand.Parameters.AddWithValue("@City", position.City);
            insertCommand.Parameters.AddWithValue("@State", position.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", position.ZipCode);
            if (position.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", position.Phone);
            if (position.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    position.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Positions') FROM Positions";
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

        public static bool UpdatePosition()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Positions SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE PositionID = @OldPositionID " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newPosition.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newPosition.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newPosition.City);
            updateCommand.Parameters.AddWithValue("@NewState", newPosition.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newPosition.ZipCode);
            if (newPosition.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newPosition.Phone);
            if (newPosition.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newPosition.Email);

            updateCommand.Parameters.AddWithValue("@OldPositionID", oldPosition.PositionID);
            updateCommand.Parameters.AddWithValue("@OldName", oldPosition.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldPosition.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldPosition.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldPosition.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldPosition.ZipCode);
            if (oldPosition.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldPosition.Phone);
            if (oldPosition.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldPosition.Email);

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