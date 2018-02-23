﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
                "@DateTime, @AdditionalNotes);";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@PositionID", interview.PositionID);
            insertCommand.Parameters.AddWithValue("@CompanyID", interview.CompanyID);
            insertCommand.Parameters.AddWithValue("@ContactID", interview.ContactID);
            insertCommand.Parameters.AddWithValue("@DateTime", SqlDbType.DateTime);
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
    }
}