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
            insertCommand.Parameters.AddWithValue("@SchoolName", school.SchoolName);
            insertCommand.Parameters.AddWithValue("@StreetName", school.StreetName);
            insertCommand.Parameters.AddWithValue("@City", school.City);
            insertCommand.Parameters.AddWithValue("@State", school.State);
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
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateSchool(School oldSchool, School newSchool)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Schools SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE SchoolID = @OldSchoolID " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newSchool.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newSchool.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newSchool.City);
            updateCommand.Parameters.AddWithValue("@NewState", newSchool.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newSchool.ZipCode);
            if (newSchool.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newSchool.Phone);
            if (newSchool.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newSchool.Email);

            updateCommand.Parameters.AddWithValue("@OldSchoolID", oldSchool.SchoolID);
            updateCommand.Parameters.AddWithValue("@OldName", oldSchool.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldSchool.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldSchool.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldSchool.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldSchool.ZipCode);
            if (oldSchool.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldSchool.Phone);
            if (oldSchool.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldSchool.Email);

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