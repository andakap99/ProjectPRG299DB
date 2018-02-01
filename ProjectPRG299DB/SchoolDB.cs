using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class SchoolDB
    {
        public static List<School> GetSchool()
        {
            List<School> SchoolList = new List<School>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT SchoolID, SchoolName, StreetName, " +
                "City, State, ZipCode, NumberOfYearsAttended, Graduated FROM dbo.School";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("SchoolID"),
                    cNOrd = reader.GetOrdinal("SchoolName"),
                    cAOrd = reader.GetOrdinal("StreetName"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("NumberOfYearsAttended"),
                    cEOrd = reader.GetOrdinal("Graduated");
                while (reader.Read())
                {
                    School scho = new School();
                    scho.SchoolID = reader.GetInt32(cIDOrd);
                    scho.SchoolName = reader.GetString(cNOrd);
                    scho.StreetName = reader.GetString(cAOrd);
                    scho.City = reader.GetString(cCOrd);
                    scho.State = reader.GetString(cSOrd);
                    scho.ZipCode = reader.GetString(cZCOrd);
                    scho.NumberOfYearsAttended = reader.GetString(cPOrd);
                    scho.Graduated = reader.GetString(cEOrd);
                    SchoolList.Add(scho);
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
            return SchoolList;
        }
        public static School GetSchoolByRow(int schoolID)
        {
            School school = new School();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT SchoolID, SchoolName, StreetName, " +
                "City, State, ZipCode, NumberOfYearsAttended, Graduated FROM dbo.School WHERE SchoolID = @SchoolID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@SchoolID", schoolID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("SchoolID"),
                    cNOrd = reader.GetOrdinal("SchoolName"),
                    cAOrd = reader.GetOrdinal("StreetName"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("NumberOfYearsAttended"),
                    cEOrd = reader.GetOrdinal("Graduated");
                while (reader.Read())
                {
                    school.SchoolID = reader.GetInt32(cIDOrd);
                    school.SchoolName = reader.GetString(cNOrd);
                    school.StreetName = reader.GetString(cAOrd);
                    school.City = reader.GetString(cCOrd);
                    school.State = reader.GetString(cSOrd);
                    school.ZipCode = reader.GetString(cZCOrd);
                    school.NumberOfYearsAttended = reader.GetString(cPOrd);
                    school.Graduated = reader.GetString(cEOrd);
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
            return school;
        }

        public static bool DeleteSchool(School school)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM School WHERE SchoolID = @SchoolID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@SchoolID", school.SchoolID);
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

        public static int AddSchool(School school)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT School " +
                  "(SchoolName, StreetName, " +
                "City, State, ZipCode, NumberOfYearsAttended, Graduated) " +
                "VALUES (@SchoolName, @StreetName, " +
                "@City, @State, @ZipCode, @NumberOfYearsAttended, @Graduated)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            if (school.SchoolName == "")
                insertCommand.Parameters.AddWithValue("@SchoolName", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@SchoolName", school.SchoolName);
            if (school.StreetName == "")
                insertCommand.Parameters.AddWithValue("@StreetName", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@StreetName", school.StreetName);
            if (school.City == "")
                insertCommand.Parameters.AddWithValue("@City", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@City", school.City);
            if(school.State == "")
                insertCommand.Parameters.AddWithValue("@State", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@State", school.State);
            if (school.ZipCode == "")
                insertCommand.Parameters.AddWithValue("@ZipCode", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@ZipCode", school.ZipCode);
            if (school.NumberOfYearsAttended == "")
                insertCommand.Parameters.AddWithValue("@NumberOfYearsAttended", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@NumberOfYearsAttended", school.NumberOfYearsAttended);
            if (school.Graduated == "")
                insertCommand.Parameters.AddWithValue("@Graduated",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Graduated",
                    school.Graduated);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('School') FROM School";
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

        public static bool UpdateSchool(School oldSchool, School newSchool)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE School SET " +
                  "SchoolName = @NewSchoolName, " +
                  "StreetName = @NewStreetName, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "NumberofYearsAttended = @NewNumberofYearsAttended, " +
                  "Graduated = @NewGraduated " +
                "WHERE SchoolID = @OldSchoolID " +
                  "AND SchoolName = @OldSchoolName " +
                      "OR SchoolName IS NULL AND @OldSchoolName IS NULL) " +
                  "AND StreetName = @OldStreetName " +
                      "OR StreetName IS NULL AND @OldStreetName IS NULL) " +
                  "AND City = @OldCity " +
                      "OR City IS NULL AND @OldCity IS NULL) " +
                  "AND State = @OldState " +
                      "OR State IS NULL AND @OldState IS NULL) " +
                  "AND ZipCode = @OldZipCode " +
                      "OR ZipCode IS NULL AND @OldZipCode IS NULL) " +
                  "AND (NumberofYearsAttended = @OldNumberofYearsAttended " +
                      "OR NumberofYearsAttended IS NULL AND @OldNumberofYearsAttended IS NULL) " +
                  "AND (Graduated = @OldGraduated " +
                      "OR Graduated IS NULL AND @OldGraduated IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            if (newSchool.SchoolName == "")
                updateCommand.Parameters.AddWithValue("@NewSchoolName", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewSchoolName", newSchool.SchoolName);
            if (newSchool.StreetName == "")
                updateCommand.Parameters.AddWithValue("@NewStreetName", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewStreetName", newSchool.StreetName);
            if (newSchool.City == "")
                updateCommand.Parameters.AddWithValue("@NewCity", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewCity", newSchool.City);
            if (newSchool.State == "")
                updateCommand.Parameters.AddWithValue("@NewState", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewState", newSchool.State);
            if (newSchool.ZipCode == "")
                updateCommand.Parameters.AddWithValue("@NewZipCode", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewZipCode", newSchool.ZipCode);
            if (newSchool.NumberOfYearsAttended == "")
                updateCommand.Parameters.AddWithValue("@NewNumberOfYearsAttended", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewNumberOfYearsAttended", newSchool.NumberOfYearsAttended);
            if (newSchool.Graduated == "")
                updateCommand.Parameters.AddWithValue("@NewGraduated",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewGraduated",
                    newSchool.Graduated);
            updateCommand.Parameters.AddWithValue("@OldSchoolID", oldSchool.SchoolID);
            if (oldSchool.SchoolName == "")
                updateCommand.Parameters.AddWithValue("@OldSchoolName", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldSchoolName", oldSchool.SchoolName);
            if (oldSchool.StreetName == "")
                updateCommand.Parameters.AddWithValue("@OldStreetName", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldStreetName", oldSchool.StreetName);
            if (oldSchool.City == "")
                updateCommand.Parameters.AddWithValue("@OldCity", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldCity", oldSchool.City);
            if (oldSchool.State == "")
                updateCommand.Parameters.AddWithValue("@OldState", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldState", oldSchool.State);
            if (oldSchool.ZipCode == "")
                updateCommand.Parameters.AddWithValue("@OldZipCode", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldZipCode", oldSchool.ZipCode);
            if (oldSchool.NumberOfYearsAttended == "")
                updateCommand.Parameters.AddWithValue("@OldNumberOfYearsAttended", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldNumberOfYearsAttended", oldSchool.NumberOfYearsAttended);
            if (oldSchool.Graduated == "")
                updateCommand.Parameters.AddWithValue("@OldGraduated",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldGraduated",
                    oldSchool.Graduated);
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