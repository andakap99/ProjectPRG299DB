using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ContactPositionDB
    { 
        public static List<ContactPosition> GetContactPosition()// USED TO POPULATE THE DATA GRID TABLE ALSO USED TO REFRESH THE DATA GRID
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
        public static ContactPosition GetContactPositionByRow(int contactpositionID)// GETS ONE ROW AT A TIME FROM THE DATABASE
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

        public static bool DeleteContactPosition(int contactID) // DELETES A ROW FROM THE DATABASE 
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
        public static bool DeleteContactPosition2(int positionID) // DELETES A ROW FROM THE DATABASE 
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM ContactPosition WHERE PositionID = @PositionID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@PositionID", positionID);
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

        public static int AddContactPosition(ContactPosition contactposition)// ADDS A NEW ROW TO THE DATABASE
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
                    "SELECT IDENT_CURRENT('Contact') FROM ContactPosition";
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

        public static bool UpdateContactPosition(ContactPosition oldContactPosition, ContactPosition newContactPosition) // MODIFIES THE DATABASE A ROW AT A TIME
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
        public static List<ContactPosition> GetContactPositionSorted(string columnName) // SORTS THE DATA IN THE DATABASE  
        {
            List<ContactPosition> contactpositionList = new List<ContactPosition>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, PositionID FROM dbo.ContactPosition " +
               "ORDER BY CASE WHEN @ColumnName = 'ContactID' THEN ContactID END ASC, " +
               "CASE WHEN @ColumnName = 'PositionID' THEN PositionID END ASC;";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);

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
        public static List<ContactPosition> GetContactPositionFiltered(string columnName, string columnfilter)// PUTS A FILTER TO THE DATABASE 
        {
            int filtered = 0;

            List<ContactPosition> contactpositionList = new List<ContactPosition>();
            SqlConnection connection = PRG299DB.GetConnection();
            /* 
                When you pass any column name and filter which matches to any records and per column name, it will return those records.             
                When column name matches and no record matches as per column name it fallback to last ELSE part so it won't return any records as expected.
                In one special case when you don't mention any column name i.e. @ColumnName = '' then all rows will be returned as you didn't want to filter.
             */
            string selectStatement = "SELECT ContactID, PositionID FROM dbo.ContactPosition "+
                "WHERE CASE WHEN @ColumnName = 'ContactID' AND ContactID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'PositionID' AND PositionID = @Filter THEN 1 WHEN @ColumnName = '' THEN 1 ELSE 0 END = 1";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            if (columnName == "ContactID" || columnName == "PositionID")
            {
                int.TryParse(columnfilter, out filtered);
                selectCommand.Parameters.AddWithValue("@Filter", filtered);
                selectCommand.Parameters["@Filter"].SqlDbType = SqlDbType.Int;
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Filter", columnfilter);
                selectCommand.Parameters["@Filter"].SqlDbType = SqlDbType.VarChar;
            }

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

    }
}