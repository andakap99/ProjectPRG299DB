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
            string selectStatement = "SELECT InterviewID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Interview";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Interview interv = new Interview();
                    interv.InterviewID = reader.GetInt32(cIDOrd);
                    interv.PositionID = reader.GetInt32(cNOrd);
                    interv.CompanyID = reader.GetInt32(cAOrd);
                    interv.DateTimeInterview = reader.GetDateTime(cCOrd);
                    interv.AdditionalNotes = reader.GetString(cSOrd);
                    interviewList.Add(interv);
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
            return interviewList;
        }
        public static Interview GetInterviewByRow(int interviewID)
        {
            Interview interview = new Interview();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT InterviewID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Interviews WHERE InterviewID = @InterviewID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@InterviewID", interviewID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("Name"),
                    cAOrd = reader.GetOrdinal("Address"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Phone"),
                    cEOrd = reader.GetOrdinal("Email");
                while (reader.Read())
                {
                    interview.InterviewID = reader.GetInt32(cIDOrd);
                    interview.Name = reader.GetString(cNOrd);
                    interview.Address = reader.GetString(cAOrd);
                    interview.City = reader.GetString(cCOrd);
                    interview.State = reader.GetString(cSOrd);
                    interview.ZipCode = reader.GetString(cZCOrd);
                    interview.Phone = reader.GetString(cPOrd);
                    interview.Email = reader.GetString(cEOrd);
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
            return interview;
        }

        public static bool DeleteInterview(Interview interview)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Interviews WHERE InterviewID = @InterviewID";
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
            finally
            {
                connection.Close();
            }
        }

        public static int AddInterview(Interview interview)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Interviews " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", interview.Name);
            insertCommand.Parameters.AddWithValue("@Address", interview.Address);
            insertCommand.Parameters.AddWithValue("@City", interview.City);
            insertCommand.Parameters.AddWithValue("@State", interview.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", interview.ZipCode);
            if (interview.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", interview.Phone);
            if (interview.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    interview.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Interviews') FROM Interviews";
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

        public static bool UpdateInterview(Interview oldInterview, Interview newInterview)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Interviews SET " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newInterview.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newInterview.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newInterview.City);
            updateCommand.Parameters.AddWithValue("@NewState", newInterview.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newInterview.ZipCode);
            if (newInterview.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newInterview.Phone);
            if (newInterview.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newInterview.Email);

            updateCommand.Parameters.AddWithValue("@OldInterviewID", oldInterview.InterviewID);
            updateCommand.Parameters.AddWithValue("@OldName", oldInterview.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldInterview.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldInterview.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldInterview.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldInterview.ZipCode);
            if (oldInterview.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldInterview.Phone);
            if (oldInterview.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldInterview.Email);

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