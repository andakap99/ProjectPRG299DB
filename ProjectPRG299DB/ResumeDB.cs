using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ResumeDB
    {
        public static List<Resume> GetResume() // USED TO POPULATE THE DATA GRID TABLE ALSO USED TO REFRESH THE DATA GRID
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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        resu.RSCDirectoryPath = "";
                    else
                        resu.RSCDirectoryPath = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        resu.SchoolID = -1;
                    else
                        resu.SchoolID = reader.GetInt32(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        resu.ClientID = -1;
                    else
                        resu.ClientID = reader.GetInt32(cCOrd);
                    resumeList.Add(resu);
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
            return resumeList;
        }
        public static Resume GetResumeByRow(int resumeID) // GETS ONE ROW AT A TIME FROM THE DATABASE
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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        resume.RSCDirectoryPath = "";
                    else
                        resume.RSCDirectoryPath = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        resume.SchoolID = -1;
                    else
                        resume.SchoolID = reader.GetInt32(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        resume.ClientID = -1;
                    else
                        resume.ClientID = reader.GetInt32(cCOrd);
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
            return resume;
        }

        public static bool DeleteResume(int resumeID) // DELETES A ROW FROM THE DATABASE 
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Resume WHERE ResumeID = @ResumeID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ResumeID", resumeID);
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

        public static int AddResume(Resume resume)// ADDS A NEW ROW TO THE DATABASE 
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Resume " +
                  "(RSCDirectoryPath, SchoolID, " +
                "ClientID) " +
                "VALUES (@RSCDirectoryPath, @SchoolID, " +
                "@ClientID);"; 
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            if (resume.RSCDirectoryPath == null|| resume.RSCDirectoryPath=="")
                insertCommand.Parameters.AddWithValue("@RSCDirectoryPath", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@RSCDirectoryPath", resume.RSCDirectoryPath);
            if (resume.SchoolID.ToString() == null)
                insertCommand.Parameters.AddWithValue("@SchoolID", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@SchoolID", resume.SchoolID);
            if (resume.ClientID.ToString() == null)
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateResume(Resume oldResume, Resume newResume)// MODIFIES THE DATABASE A ROW AT A TIME
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Resume SET " +
                  "RSCDirectoryPath = @NewRSCDirectoryPath, " +
                  "SchoolID = @NewSchoolID, " +
                  "ClientID = @NewClientID " +
                "WHERE ResumeID = @OldResumeID " +
                  "AND (RSCDirectoryPath = @OldRSCDirectoryPath " +
                      "OR RSCDirectoryPath IS NULL AND @OldRSCDirectoryPath IS NULL) " +
                  "AND (SchoolID = @OldSchoolID " +
                      "OR SchoolID IS NULL AND @OldSchoolID IS NULL) " +
                  "AND (ClientID = @OldClientID " +
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public static List<Resume> GetResumeSorted(string columnName) // SORTS THE DATA IN THE DATABASE
        {
            List<Resume> resumeList = new List<Resume>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ResumeID, RSCDirectoryPath, SchoolID, " +
                "ClientID FROM dbo.Resume " +
                "ORDER BY CASE WHEN @ColumnName = 'ResumeID' THEN ResumeID END ASC, " +
                "CASE WHEN @ColumnName = 'RSCDirectoryPath' THEN RSCDirectoryPath END ASC, " +
                "CASE WHEN @ColumnName = 'SchoolID' THEN SchoolID END ASC, " +
                "CASE WHEN @ColumnName = 'ClientID' THEN ClientID END ASC;";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);

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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        resu.RSCDirectoryPath = "";
                    else
                        resu.RSCDirectoryPath = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        resu.SchoolID = -1;
                    else
                        resu.SchoolID = reader.GetInt32(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        resu.ClientID = -1;
                    else
                        resu.ClientID = reader.GetInt32(cCOrd);
                    resumeList.Add(resu);
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
            return resumeList;
        }
        public static List<Resume> GetResumeFiltered(string columnName, string columnfilter)// PUTS A FILTER TO THE DATABASE
        {
            int filtered = 0;

            List<Resume> resumeList = new List<Resume>();
            SqlConnection connection = PRG299DB.GetConnection();
            /* 
                When you pass any column name and filter which matches to any records and per column name, it will return those records.             
                When column name matches and no record matches as per column name it fallback to last ELSE part so it won't return any records as expected.
                In one special case when you don't mention any column name i.e. @ColumnName = '' then all rows will be returned as you didn't want to filter.
             */
            string selectStatement = "SELECT ResumeID, RSCDirectoryPath, SchoolID, " +
                "ClientID FROM dbo.Resume "+
                "WHERE CASE WHEN @ColumnName = 'ResumeID' AND ResumeID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'RSCDirectoryPath' AND RSCDirectoryPath LIKE '%' + @Filter + '%' THEN 1 " +
                "WHERE CASE WHEN @ColumnName = 'SchoolID' AND SchoolID = @Filter THEN 1 " +
                "WHERE CASE WHEN @ColumnName = 'ClientID' AND ClientID = @Filter THEN 1 WHEN @ColumnName = '' THEN 1 ELSE 0 END = 1";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            if (columnName == "ResumeID" || columnName == "SchoolID" || columnName == "ClientID")
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
                int cIDOrd = reader.GetOrdinal("ResumeID"),
                    cNOrd = reader.GetOrdinal("RSCDirectoryPath"),
                    cAOrd = reader.GetOrdinal("SchoolID"),
                    cCOrd = reader.GetOrdinal("ClientID");
                while (reader.Read())
                {
                    Resume resu = new Resume();
                    resu.ResumeID = reader.GetInt32(cIDOrd);
                    if (reader[cNOrd].Equals(DBNull.Value))
                        resu.RSCDirectoryPath = "";
                    else
                        resu.RSCDirectoryPath = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        resu.SchoolID = -1;
                    else
                        resu.SchoolID = reader.GetInt32(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        resu.ClientID = -1;
                    else
                        resu.ClientID = reader.GetInt32(cCOrd);
                    resumeList.Add(resu);
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
            return resumeList;
        }

    }
}