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
            string selectStatement = "SELECT InterviewID, PositionID, CompanyID, ContactID," +
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
        public static Interview GetInterviewByRow(int interviewID)
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

        public static bool DeleteInterview(Interview interview)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Interview WHERE InterviewID = @InterviewID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@InterviewID", interview.InterviewID);
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

        public static int AddInterview(Interview interview)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Interview " +
                  "(PositionID, CompanyID, ContactID, " +
                "DateTime, AdditionalNotes) " +
                "VALUES (@PositionID, @CompanyID, @ContactID, " +
                "@DateTime, @AdditionalNotes)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@PositionID", interview.PositionID);
            insertCommand.Parameters.AddWithValue("@CompanyID", interview.CompanyID);
            insertCommand.Parameters.AddWithValue("@ContactID", interview.ContactID);
            insertCommand.Parameters.AddWithValue("@DateTime", interview.DateTimeInterview);
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

        public static bool UpdateInterview(Interview oldInterview, Interview newInterview)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Interview SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE InterviewID = @OldInterviewID " +
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
            updateCommand.Parameters.AddWithValue("@PositionID", newInterview.PositionID);
            updateCommand.Parameters.AddWithValue("@CompanyID", newInterview.CompanyID);
            updateCommand.Parameters.AddWithValue("@ContactID", newInterview.ContactID);
            updateCommand.Parameters.AddWithValue("@DateTime", newInterview.DateTimeInterview);
            if (newInterview.AdditionalNotes == null)
                updateCommand.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@AdditionalNotes", newInterview.AdditionalNotes);

            updateCommand.Parameters.AddWithValue("@InterviewID", oldInterview.InterviewID);
            updateCommand.Parameters.AddWithValue("@PositionID", oldInterview.PositionID);
            updateCommand.Parameters.AddWithValue("@CompanyID", oldInterview.CompanyID);
            updateCommand.Parameters.AddWithValue("@ContactID", oldInterview.ContactID);
            updateCommand.Parameters.AddWithValue("@DateTime", oldInterview.DateTimeInterview);
            if (oldInterview.AdditionalNotes == null)
                updateCommand.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@AdditionalNotes", oldInterview.AdditionalNotes);

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