using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class InterviewDB
    {
        public static List<Interview> GetInterview() // USED TO POPULATE THE DATA GRID TABLE ALSO USED TO REFRESH THE DATA GRID
        {
            List<Interview> interviewList = new List<Interview>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT InterviewID, PositionID, CompanyID, ContactID, " +
                "DateTime, AdditionalNotes FROM dbo.Interview";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cford = reader.GetOrdinal("ContactID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Interview interv = new Interview();
                    interv.InterviewID = reader.GetInt32(cIDOrd);
                    interv.PositionID = reader.GetInt32(cNOrd);
                    interv.CompanyID = reader.GetInt32(cAOrd);
                    interv.ContactID = reader.GetInt32(cford);
                    interv.DateTimeInterview = reader.GetDateTime(cCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        interv.AdditionalNotes = "";
                    else
                        interv.AdditionalNotes = reader.GetString(cSOrd);
                    interviewList.Add(interv);
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
            return interviewList;
        }
        public static Interview GetInterviewByRow(int interviewID) // GETS ONE ROW AT A TIME FROM THE DATABASE
        {
            Interview interview = new Interview();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT InterviewID, PositionID, CompanyID, ContactID, " +
                "DateTime, AdditionalNotes FROM dbo.Interview WHERE InterviewID = @InterviewID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@InterviewID", interviewID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cford = reader.GetOrdinal("ContactID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    interview.InterviewID = reader.GetInt32(cIDOrd);
                    interview.PositionID = reader.GetInt32(cNOrd);
                    interview.CompanyID = reader.GetInt32(cAOrd);
                    interview.ContactID = reader.GetInt32(cford);
                    interview.DateTimeInterview = reader.GetDateTime(cCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        interview.AdditionalNotes = "";
                    else
                        interview.AdditionalNotes = reader.GetString(cSOrd);
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
            return interview;
        }

        public static bool DeleteInterview(int interviewID) // DELETES A ROW FROM THE DATABASE
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Interview WHERE InterviewID = @InterviewID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@InterviewID", interviewID);
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

        public static int AddInterview(Interview interview) // ADDS A NEW ROW TO THE DATABASE
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Interview " +
                "(PositionID, CompanyID, ContactID, " +
                "DateTime, AdditionalNotes) " +
                "VALUES (@PositionID, @CompanyID, @ContactID, " +
                "@DateTime, @AdditionalNotes);";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@PositionID", interview.PositionID);
            insertCommand.Parameters.AddWithValue("@CompanyID", interview.CompanyID);
            insertCommand.Parameters.AddWithValue("@ContactID", interview.ContactID);
            insertCommand.Parameters.AddWithValue("@DateTime", interview.DateTimeInterview);
            insertCommand.Parameters["@DateTime"].SqlDbType = SqlDbType.DateTime;
            if (interview.AdditionalNotes == null)
                insertCommand.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@AdditionalNotes", interview.AdditionalNotes);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Interview') FROM Interview";
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

        public static bool UpdateInterview(Interview oldInterview, Interview newInterview) // MODIFIES THE DATABASE A ROW AT A TIME
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Interview SET " +
                  "PositionID = @NewPositionID, " +
                  "CompanyID = @NewCompanyID, " +
                  "ContactID = @NewContactID, " +
                  "DateTime = @NewDateTime, " +
                  "AdditionalNotes = @NewAdditionalNotes " +
                "WHERE InterviewID = @OldInterviewID " +
                  "AND PositionID = @OldPositionID " +
                  "AND CompanyID = @OldCompanyID " +
                  "AND ContactID = @OldContactID " +
                  "AND DateTime = @OldDateTime " +
                  "AND (AdditionalNotes = @OldAdditionalNotes " +
                      "OR AdditionalNotes IS NULL AND @OldAdditionalNotes IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@NewPositionID", newInterview.PositionID);
            updateCommand.Parameters.AddWithValue("@NewCompanyID", newInterview.CompanyID);
            updateCommand.Parameters.AddWithValue("@NewContactID", newInterview.ContactID);
            updateCommand.Parameters.AddWithValue("@NewDateTime", newInterview.DateTimeInterview);
            if (newInterview.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes", newInterview.AdditionalNotes);

            updateCommand.Parameters.AddWithValue("@OldInterviewID", oldInterview.InterviewID);
            updateCommand.Parameters.AddWithValue("@OldPositionID", oldInterview.PositionID);
            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldInterview.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldContactID", oldInterview.ContactID);
            updateCommand.Parameters.AddWithValue("@OldDateTime", oldInterview.DateTimeInterview);
            if (oldInterview.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes", oldInterview.AdditionalNotes);

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
        public static List<Interview> GetInterviewSorted(string columnName) // SORTS THE DATA IN THE DATABASE
        {
            List<Interview> interviewList = new List<Interview>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT InterviewID, PositionID, CompanyID, ContactID, " +
                "DateTime, AdditionalNotes FROM dbo.Interview " +
                "ORDER BY CASE WHEN @ColumnName = 'InterviewID' THEN InterviewID END ASC, " +
               "CASE WHEN @ColumnName = 'PositionID' THEN PositionID END ASC, " +
               "CASE WHEN @ColumnName = 'CompanyID' THEN CompanyID END ASC, " +
               "CASE WHEN @ColumnName = 'ContactID' THEN ContactID END ASC, " +
               "CASE WHEN @ColumnName = 'DateTime' THEN DateTime END ASC, " +
               "CASE WHEN @ColumnName = 'AdditionalNotes' THEN AdditionalNotes END ASC;";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);

            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cford = reader.GetOrdinal("ContactID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Interview interv = new Interview();
                    interv.InterviewID = reader.GetInt32(cIDOrd);
                    interv.PositionID = reader.GetInt32(cNOrd);
                    interv.CompanyID = reader.GetInt32(cAOrd);
                    interv.ContactID = reader.GetInt32(cford);
                    interv.DateTimeInterview = reader.GetDateTime(cCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        interv.AdditionalNotes = "";
                    else
                        interv.AdditionalNotes = reader.GetString(cSOrd);
                    interviewList.Add(interv);
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
            return interviewList;
        }
        public static List<Interview> GetInterviewFiltered(string columnName, string columnfilter) // PUTS A FILTER TO THE DATABASE
        {
            int filtered = 0;
            DateTime dateFilter;
            List<Interview> interviewList = new List<Interview>();
            SqlConnection connection = PRG299DB.GetConnection();
            /* 
                When you pass any column name and filter which matches to any records and per column name, it will return those records.             
                When column name matches and no record matches as per column name it fallback to last ELSE part so it won't return any records as expected.
                In one special case when you don't mention any column name i.e. @ColumnName = '' then all rows will be returned as you didn't want to filter.
             */

            string selectStatement = "SELECT InterviewID, PositionID, CompanyID, ContactID," +
                "DateTime, AdditionalNotes FROM dbo.Interview " +
                "WHERE CASE WHEN @ColumnName = 'InterviewID' AND InterviewID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'PositionID' AND PositionID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'CompanyID' AND CompanyID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'ContactID' AND ContactID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'DateTime' AND CASE WHEN ISDATE(@Filter) = 1 THEN CONVERT(DATETIME, @Filter, 101) ELSE NULL END = DateTime THEN 1 " +
                "WHEN @ColumnName = 'AdditionalNotes' AND AdditionalNotes LIKE '%' + @Filter + '%' THEN 1 WHEN @ColumnName = '' THEN 1 ELSE 0 END = 1";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            if (columnName == "InterviewID" || columnName == "PositionID" || columnName == "CompanyID" || columnName == "ContactID")
            {
                int.TryParse(columnfilter, out filtered);
                selectCommand.Parameters.AddWithValue("@Filter", filtered);
                selectCommand.Parameters["@Filter"].SqlDbType = SqlDbType.Int;
            }
            else if (columnName == "DateTimeInterview")
            {
                if (DateTime.TryParse(columnfilter, out dateFilter))
                {
                    selectCommand.Parameters.AddWithValue("@Filter", dateFilter);
                    selectCommand.Parameters["@Filter"].SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                }
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
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cford = reader.GetOrdinal("ContactID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Interview interv = new Interview();
                    interv.InterviewID = reader.GetInt32(cIDOrd);
                    interv.PositionID = reader.GetInt32(cNOrd);
                    interv.CompanyID = reader.GetInt32(cAOrd);
                    interv.ContactID = reader.GetInt32(cford);
                    interv.DateTimeInterview = reader.GetDateTime(cCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        interv.AdditionalNotes = "";
                    else
                        interv.AdditionalNotes = reader.GetString(cSOrd);
                    interviewList.Add(interv);
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
            return interviewList;
        }

    }
}