using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class PositionDB
    {
        public static List<Position> GetPosition() // USED TO POPULATE THE DATA GRID TABLE ALSO USED TO REFRESH THE DATA GRID
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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        posi.PositionName = "";
                    else
                        posi.PositionName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        posi.Description = "";
                    else
                        posi.Description = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        posi.CompanyID = -1;
                    else
                        posi.CompanyID = reader.GetInt32(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        posi.AdditionalNotes = "";
                    else
                        posi.AdditionalNotes = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        posi.ResumeID = -1;
                    else
                        posi.ResumeID = reader.GetInt32(cZCOrd);
                    positionList.Add(posi);
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
            return positionList;
        }
        public static Position GetPositionByRow(int positionID)// GETS ONE ROW AT A TIME FROM THE DATABASE
        {
            Position position = new Position();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT PositionID, PositionName, Description, " +
                "CompanyID, AdditionalNotes, ResumeID FROM dbo.Position = @PositionID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@PositionID", positionID);
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
                    position.PositionID = reader.GetInt32(cIDOrd);
                    if (reader[cNOrd].Equals(DBNull.Value))
                        position.PositionName = "";
                    else
                        position.PositionName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        position.Description = "";
                    else
                        position.Description = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        position.CompanyID = -1;
                    else
                        position.CompanyID = reader.GetInt32(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        position.AdditionalNotes = "";
                    else
                        position.AdditionalNotes = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        position.ResumeID = -1;
                    else
                        position.ResumeID = reader.GetInt32(cZCOrd);
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
            return position;
        }

        public static bool DeletePosition(int positionID)// DELETES A ROW FROM THE DATABASE
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Position WHERE PositionID = @PositionID";
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

        public static int AddPosition(Position position) // ADDS A NEW ROW TO THE DATABASE
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Position " +
                "(PositionName, Description, " +
                "CompanyID, AdditionalNotes, ResumeID) " +
                "VALUES (@PositionName, @Description, " +
                "@CompanyID, @AdditionalNotes, @ResumeID);";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            if(position.PositionName == null)
                insertCommand.Parameters.AddWithValue("@PositionName", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@PositionName", position.PositionName);
            if(position.Description == null)
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Description", position.Description);
            if(position.CompanyID.ToString() == null)
                insertCommand.Parameters.AddWithValue("@CompanyID", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@CompanyID", position.CompanyID);
            if(position.AdditionalNotes == null)
                insertCommand.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@AdditionalNotes", position.AdditionalNotes);
            if(position.ResumeID.ToString() == null)
                insertCommand.Parameters.AddWithValue("@ResumeID", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@ResumeID", position.ResumeID);

            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Position') FROM Position";
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

        public static bool UpdatePosition(Position oldPosition, Position newPosition) // MODIFIES THE DATABASE A ROW AT A TIME
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Position SET " +
                  "PositionName = @NewPositionName, " +
                  "Description = @NewDescription, " +
                  "CompanyID = @NewCompanyID, " +
                  "AdditionalNotes = @NewAdditionalNotes, " +
                  "ResumeID = @NewResumeID " +
                "WHERE PositionID = @OldPositionID " +
                  "PositionName = @OldPositionName, " +
                      "OR PositionName IS NULL AND @OldPositionName IS NULL) " +
                  "Description = @OldDescription, " +
                      "OR Description IS NULL AND @OldDescription IS NULL) " +
                  "CompanyID = @OldCompanyID, " +
                      "OR CompanyID IS NULL AND @OldCompanyID IS NULL) " +
                  "AdditionalNotes = @OldAdditionalNotes, " +
                      "OR AdditionalNotes IS NULL AND @OldAdditionalNotes IS NULL) " +
                  "ResumeID = @OldResumeID " +
                      "OR ResumeID IS NULL AND @OldResumeID IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            if (newPosition.PositionName == "")
                updateCommand.Parameters.AddWithValue("@NewPositionName", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPositionName", newPosition.PositionName);
            if (newPosition.Description == "")
                updateCommand.Parameters.AddWithValue("@NewDescription", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewDescription", newPosition.Description);
            if (newPosition.CompanyID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@NewCompanyID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewCompanyID", newPosition.CompanyID);
            if (newPosition.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes", newPosition.AdditionalNotes);
            if (newPosition.ResumeID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@NewResumeID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewResumeID", newPosition.ResumeID);

            updateCommand.Parameters.AddWithValue("@OldPositionID", oldPosition.PositionID);
            if (oldPosition.PositionName == "")
                updateCommand.Parameters.AddWithValue("@OldPositionName", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPositionName", oldPosition.PositionName);
            if (oldPosition.Description == "")
                updateCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldDescription", oldPosition.Description);
            if (oldPosition.CompanyID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@OldCompanyID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldCompanyID", oldPosition.CompanyID);
            if (oldPosition.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes", oldPosition.AdditionalNotes);
            if (oldPosition.ResumeID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@OldResumeID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldResumeID", oldPosition.ResumeID);

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
        public static List<Position> GetPositionSorted(string columnName) // SORTS THE DATA IN THE DATABASE
        {
            List<Position> positionList = new List<Position>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT PositionID, PositionName, Description, " +
                "CompanyID, AdditionalNotes, ResumeID FROM dbo.Position " +
                "ORDER BY CASE WHEN @ColumnName = 'PositionID' THEN PositionID END ASC, " +
                "CASE WHEN @ColumnName = 'PositionName' THEN PositionName END ASC, " +
                "CASE WHEN @ColumnName = 'Description' THEN Description END ASC, " +
                "CASE WHEN @ColumnName = 'CompanyID' THEN CompanyID END ASC, " +
                "CASE WHEN @ColumnName = 'AdditionalNotes' THEN AdditionalNotes END ASC, " +
                "CASE WHEN @ColumnName = 'ResumeID' THEN ResumeID END ASC;";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);

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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        posi.PositionName = "";
                    else
                        posi.PositionName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        posi.Description = "";
                    else
                        posi.Description = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        posi.CompanyID = -1;
                    else
                        posi.CompanyID = reader.GetInt32(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        posi.AdditionalNotes = "";
                    else
                        posi.AdditionalNotes = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        posi.ResumeID = -1;
                    else
                        posi.ResumeID = reader.GetInt32(cZCOrd);
                    positionList.Add(posi);
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
            return positionList;
        }
        public static List<Position> GetPositionFiltered(string columnName, string columnfilter)// PUTS A FILTER TO THE DATABASE
        {
            int filtered = 0;

            List<Position> positionList = new List<Position>();
            SqlConnection connection = PRG299DB.GetConnection();
            /* 
                When you pass any column name and filter which matches to any records and per column name, it will return those records.             
                When column name matches and no record matches as per column name it fallback to last ELSE part so it won't return any records as expected.
                In one special case when you don't mention any column name i.e. @ColumnName = '' then all rows will be returned as you didn't want to filter.
             */
            string selectStatement = "SELECT PositionID, PositionName, Description, " +
                "CompanyID, AdditionalNotes, ResumeID FROM dbo.Position " +
                "WHERE CASE WHEN @ColumnName = 'PositionID' AND PositionID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'PositionName' AND PositionName LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'Description' AND Description LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'CompanyID' AND CompanyID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'AdditionalNotes' AND AdditionalNotes LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'CompanyID' AND CompanyID = @Filter THEN 1 WHEN @ColumnName = '' THEN 1 ELSE 0 END = 1";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            if (columnName == "ClientID" || columnName == "ResumeID" || columnName == "CompanyID")
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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        posi.PositionName = "";
                    else
                        posi.PositionName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        posi.Description = "";
                    else
                        posi.Description = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        posi.CompanyID = -1;
                    else
                        posi.CompanyID = reader.GetInt32(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        posi.AdditionalNotes = "";
                    else
                        posi.AdditionalNotes = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        posi.ResumeID = -1;
                    else
                        posi.ResumeID = reader.GetInt32(cZCOrd);
                    positionList.Add(posi);
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
            return positionList;
        }

    }
}