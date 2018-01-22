using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ResumeDB
    {
        public static List<Resume> GetResume()
        {
            List<Resume> resumeList = new List<Resume>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ResumeID, RSCDirectoryPath, SchoolID, " +
                "ClientID FROM dbo.Resume";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ResumeID"),
                    cNOrd = reader.GetOrdinal("RSCDirectoryPath"),
                    cAOrd = reader.GetOrdinal("SchoolID"),
                    cCOrd = reader.GetOrdinal("ClientID");
                while (reader.Read())
                {
                    Resume resu = new Resume();
                    resu.ResumeID = reader.GetInt32(cIDOrd);
                    resu.RSCDirectoryPath = reader.GetString(cNOrd);
                    resu.SchoolID = reader.GetInt32(cAOrd);
                    resu.ClientID = reader.GetInt32(cCOrd);
                    resumeList.Add(resu);
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
            return resumeList;
        }
        public static List<Resume> GetResumeByRow()
        {
            Resume resume = new Resume();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ResumeID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Resumes WHERE ResumeID = @ResumeID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ResumeID", resumeID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ResumeID"),
                    cNOrd = reader.GetOrdinal("Name"),
                    cAOrd = reader.GetOrdinal("Address"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Phone"),
                    cEOrd = reader.GetOrdinal("Email");
                while (reader.Read())
                {
                    resume.ResumeID = reader.GetInt32(cIDOrd);
                    resume.Name = reader.GetString(cNOrd);
                    resume.Address = reader.GetString(cAOrd);
                    resume.City = reader.GetString(cCOrd);
                    resume.State = reader.GetString(cSOrd);
                    resume.ZipCode = reader.GetString(cZCOrd);
                    resume.Phone = reader.GetString(cPOrd);
                    resume.Email = reader.GetString(cEOrd);
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
            return resume;
        }

        public static bool DeleteResume()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Resumes WHERE ResumeID = @ResumeID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ResumeID", cust.ResumeID);
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

        public static int AddResume()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Resumes " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", resume.Name);
            insertCommand.Parameters.AddWithValue("@Address", resume.Address);
            insertCommand.Parameters.AddWithValue("@City", resume.City);
            insertCommand.Parameters.AddWithValue("@State", resume.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", resume.ZipCode);
            if (resume.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", resume.Phone);
            if (resume.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    resume.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Resumes') FROM Resumes";
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

        public static bool UpdateResume()
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Resumes SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE ResumeID = @OldResumeID " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newResume.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newResume.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newResume.City);
            updateCommand.Parameters.AddWithValue("@NewState", newResume.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newResume.ZipCode);
            if (newResume.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newResume.Phone);
            if (newResume.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newResume.Email);

            updateCommand.Parameters.AddWithValue("@OldResumeID", oldResume.ResumeID);
            updateCommand.Parameters.AddWithValue("@OldName", oldResume.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldResume.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldResume.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldResume.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldResume.ZipCode);
            if (oldResume.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldResume.Phone);
            if (oldResume.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldResume.Email);

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