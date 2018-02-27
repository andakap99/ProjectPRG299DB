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

                    if (reader[cNOrd].Equals(DBNull.Value))
                        scho.SchoolName = "";
                    else
                        scho.SchoolName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        scho.StreetName = "";
                    else
                        scho.StreetName = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        scho.City = "";
                    else
                        scho.City = reader.GetString(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        scho.State = "";
                    else
                        scho.State = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        scho.ZipCode = "";
                    else
                        scho.ZipCode = reader.GetString(cZCOrd);
                    if (reader[cPOrd].Equals(DBNull.Value))
                        scho.NumberOfYearsAttended = -1;
                    else
                        scho.NumberOfYearsAttended = reader.GetInt32(cPOrd);
                    if (reader[cEOrd].Equals(DBNull.Value))
                        scho.Graduated = "";
                    else
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
                    if (reader[cNOrd].Equals(DBNull.Value))
                        school.SchoolName= "";
                    else
                        school.SchoolName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        school.StreetName = "";
                    else
                        school.StreetName = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        school.City = "";
                    else
                        school.City = reader.GetString(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        school.State = "";
                    else
                        school.State = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        school.ZipCode = "";
                    else
                        school.ZipCode = reader.GetString(cZCOrd);
                    if (reader[cPOrd].Equals(DBNull.Value))
                        school.NumberOfYearsAttended = -1;
                    else
                        school.NumberOfYearsAttended = reader.GetInt32(cPOrd);
                    if (reader[cEOrd].Equals(DBNull.Value))
                        school.Graduated = "";
                    else
                        school.Graduated = reader.GetString(cEOrd);
                    if (schoolID==school.SchoolID)
                    {
                        break;
                    }
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

        public static bool DeleteSchool(int schoolID)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM School WHERE SchoolID = @SchoolID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@SchoolID", schoolID);
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
                "@City, @State, @ZipCode, @NumberOfYearsAttended, @Graduated);";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            if (school.SchoolName == null)
                insertCommand.Parameters.AddWithValue("@SchoolName", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@SchoolName", school.SchoolName);
            if (school.StreetName == null)
                insertCommand.Parameters.AddWithValue("@StreetName", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@StreetName", school.StreetName);
            if (school.City == null)
                insertCommand.Parameters.AddWithValue("@City", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@City", school.City);
            if(school.State == null)
                insertCommand.Parameters.AddWithValue("@State", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@State", school.State);
            if (school.ZipCode == null)
                insertCommand.Parameters.AddWithValue("@ZipCode", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@ZipCode", school.ZipCode);
            if (school.NumberOfYearsAttended.ToString() == null)
                insertCommand.Parameters.AddWithValue("@NumberOfYearsAttended", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@NumberOfYearsAttended", school.NumberOfYearsAttended);
            if (school.Graduated == null)
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
                  "AND (SchoolName = @OldSchoolName " +
                      "OR SchoolName IS NULL AND @OldSchoolName IS NULL) " +
                  "AND (StreetName = @OldStreetName " +
                      "OR StreetName IS NULL AND @OldStreetName IS NULL) " +
                  "AND (City = @OldCity " +
                      "OR City IS NULL AND @OldCity IS NULL) " +
                  "AND (State = @OldState " +
                      "OR State IS NULL AND @OldState IS NULL) " +
                  "AND (ZipCode = @OldZipCode " +
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
            if (newSchool.NumberOfYearsAttended == -1)
                updateCommand.Parameters.AddWithValue("@NewNumberOfYearsAttended", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewNumberOfYearsAttended", newSchool.NumberOfYearsAttended);
            if (newSchool.Graduated == "")
                updateCommand.Parameters.AddWithValue("@NewGraduated", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewGraduated", newSchool.Graduated);
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
            if (oldSchool.NumberOfYearsAttended == -1)
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
        public static List<School> GetSchoolSorted(string columnName)
        {
            List<School> SchoolList = new List<School>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT SchoolID, SchoolName, StreetName, " +
                "City, State, ZipCode, NumberOfYearsAttended, Graduated FROM dbo.School " +
                "ORDER BY CASE WHEN @ColumnName = 'SchoolID' THEN SchoolID END ASC, " +
                "CASE WHEN @ColumnName = 'SchoolName' THEN SchoolName END ASC, " +
                "CASE WHEN @ColumnName = 'StreetName' THEN StreetName END ASC, " +
                "CASE WHEN @ColumnName = 'City' THEN City END ASC, " +
                "CASE WHEN @ColumnName = 'State' THEN State END ASC, " +
                "CASE WHEN @ColumnName = 'ZipCode' THEN ZipCode END ASC, " +
                "CASE WHEN @ColumnName = 'NumberOfYearsAttended' THEN NumberOfYearsAttended END ASC, " +
                "CASE WHEN @ColumnName = 'Graduated' THEN Graduated END ASC;";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);

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

                    if (reader[cNOrd].Equals(DBNull.Value))
                        scho.SchoolName = "";
                    else
                        scho.SchoolName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        scho.StreetName = "";
                    else
                        scho.StreetName = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        scho.City = "";
                    else
                        scho.City = reader.GetString(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        scho.State = "";
                    else
                        scho.State = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        scho.ZipCode = "";
                    else
                        scho.ZipCode = reader.GetString(cZCOrd);
                    if (reader[cPOrd].Equals(DBNull.Value))
                        scho.NumberOfYearsAttended = -1;
                    else
                        scho.NumberOfYearsAttended = reader.GetInt32(cPOrd);
                    if (reader[cEOrd].Equals(DBNull.Value))
                        scho.Graduated = "";
                    else
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
        public static List<School> GetSchoolFiltered()
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

                    if (reader[cNOrd].Equals(DBNull.Value))
                        scho.SchoolName = "";
                    else
                        scho.SchoolName = reader.GetString(cNOrd);
                    if (reader[cAOrd].Equals(DBNull.Value))
                        scho.StreetName = "";
                    else
                        scho.StreetName = reader.GetString(cAOrd);
                    if (reader[cCOrd].Equals(DBNull.Value))
                        scho.City = "";
                    else
                        scho.City = reader.GetString(cCOrd);
                    if (reader[cSOrd].Equals(DBNull.Value))
                        scho.State = "";
                    else
                        scho.State = reader.GetString(cSOrd);
                    if (reader[cZCOrd].Equals(DBNull.Value))
                        scho.ZipCode = "";
                    else
                        scho.ZipCode = reader.GetString(cZCOrd);
                    if (reader[cPOrd].Equals(DBNull.Value))
                        scho.NumberOfYearsAttended = -1;
                    else
                        scho.NumberOfYearsAttended = reader.GetInt32(cPOrd);
                    if (reader[cEOrd].Equals(DBNull.Value))
                        scho.Graduated = "";
                    else
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

    }
}