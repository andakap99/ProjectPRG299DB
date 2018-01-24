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
        public static Resume GetResumeByRow(int resumeID)
        {
            Resume resume = new Resume();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ResumeID, RSCDirectoryPath, SchoolID, " +
                "ClientID FROM dbo.Resume WHERE ResumeID = @ResumeID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ResumeID", resumeID);
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
                    resume.ResumeID = reader.GetInt32(cIDOrd);
                    resume.RSCDirectoryPath = reader.GetString(cNOrd);
                    resume.SchoolID = reader.GetInt32(cAOrd);
                    resume.ClientID = reader.GetInt32(cCOrd);
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

        public static bool DeleteResume(Resume resume)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Resume WHERE ResumeID = @ResumeID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ResumeID", resume.ResumeID);
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

        public static int AddResume(Resume resume)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Resume " +
                  "(RSCDirectoryPath, SchoolID, " +
                "ClientID) " +
                "VALUES (@RSCDirectoryPath, @SchoolID, " +
                "@ClientID)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            if (resume.RSCDirectoryPath == "")
                insertCommand.Parameters.AddWithValue("@RSCDirectoryPath", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@RSCDirectoryPath", resume.RSCDirectoryPath);
            if (resume.SchoolID.ToString() == "")
                insertCommand.Parameters.AddWithValue("@SchoolID", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@SchoolID", resume.SchoolID);
            if (resume.ClientID.ToString() == "")
                insertCommand.Parameters.AddWithValue("@ClientID", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@ClientID", resume.ClientID);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Resume') FROM Resume";
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

        public static bool UpdateResume(Resume oldResume, Resume newResume)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Resume SET " +
                  "RSCDirectoryPath = @NewRSCDirectoryPath, " +
                  "SchoolID = @NewSchoolID, " +
                  "ClientID = @NewClientID " +
                "WHERE ResumeID = @OldResumeID " +
                  "AND RSCDirectoryPath = @OldRSCDirectoryPath " +
                      "OR RSCDirectoryPath IS NULL AND @OldRSCDirectoryPath IS NULL) " +
                  "AND SchoolID = @OldSchoolID " +
                      "OR SchoolID IS NULL AND @OldSchoolID IS NULL) " +
                  "AND ClientID = @OldClientID " +
                      "OR ClientID IS NULL AND @OldClientID IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            if (newResume.RSCDirectoryPath == "")
                updateCommand.Parameters.AddWithValue("@NewRSCDirectoryPath", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewRSCDirectoryPath", newResume.RSCDirectoryPath);
            if (newResume.SchoolID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@NewSchoolID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewSchoolID", newResume.SchoolID);
            if (newResume.ClientID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@NewClientID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewClientID", newResume.ClientID);

            updateCommand.Parameters.AddWithValue("@OldResumeID", oldResume.ResumeID);
            if (oldResume.RSCDirectoryPath == "")
                updateCommand.Parameters.AddWithValue("@OldRSCDirectoryPath", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldRSCDirectoryPath", oldResume.RSCDirectoryPath);
            if (oldResume.SchoolID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@OldSchoolID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldSchoolID", oldResume.SchoolID);
            if (oldResume.ClientID.ToString() == "")
                updateCommand.Parameters.AddWithValue("@OldClientID", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldClientID", oldResume.ClientID);

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