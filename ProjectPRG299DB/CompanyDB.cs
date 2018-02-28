using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class CompanyDB
    {
        public static List<Company> GetCompany()
        {
            List<Company> companyList = new List<Company>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CompanyID, CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes FROM dbo.Company";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("CompanyID"),
                    cNOrd = reader.GetOrdinal("CompanyName"),
                    cBNaOrd = reader.GetOrdinal("BuildingName"),
                    cBNuOrd = reader.GetOrdinal("BuildingNumber"),
                    cAOrd = reader.GetOrdinal("StreetAddress"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Website"),
                    cEOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Company comp = new Company();
                    comp.CompanyID = reader.GetInt32(cIDOrd);
                    comp.CompanyName = reader.GetString(cNOrd);
                    comp.BuildingName = reader.GetString(cBNaOrd);
                    comp.BuildingNumber = reader.GetString(cBNuOrd);
                    comp.StreetAddress = reader.GetString(cAOrd);
                    comp.City = reader.GetString(cCOrd);
                    comp.State = reader.GetString(cSOrd);
                    comp.ZipCode = reader.GetString(cZCOrd);
                    if (reader["Website"].Equals(DBNull.Value))
                        comp.Website = "";
                    else
                        comp.Website = reader.GetString(cPOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        comp.AdditionalNotes = "";
                    else
                        comp.AdditionalNotes = reader.GetString(cEOrd);
                    companyList.Add(comp);
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
            return companyList;
        }
        public static Company GetCompanyByRow(int companyID)
        {
            Company company = new Company();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CompanyID, CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes FROM dbo.Company WHERE CompanyID = @CompanyID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@CompanyID", companyID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("CompanyID"),
                     cNOrd = reader.GetOrdinal("CompanyName"),
                     cBNaOrd = reader.GetOrdinal("BuildingName"),
                     cBNuOrd = reader.GetOrdinal("BuildingNumber"),
                     cAOrd = reader.GetOrdinal("StreetAddress"),
                     cCOrd = reader.GetOrdinal("City"),
                     cSOrd = reader.GetOrdinal("State"),
                     cZCOrd = reader.GetOrdinal("ZipCode"),
                     cPOrd = reader.GetOrdinal("Website"),
                     cEOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    company.CompanyID = reader.GetInt32(cIDOrd);
                    company.CompanyName = reader.GetString(cNOrd);
                    company.BuildingName = reader.GetString(cBNaOrd);
                    company.BuildingNumber = reader.GetString(cBNuOrd);
                    company.StreetAddress = reader.GetString(cAOrd);
                    company.City = reader.GetString(cCOrd);
                    company.State = reader.GetString(cSOrd);
                    company.ZipCode = reader.GetString(cZCOrd);
                    if (reader["Website"].Equals(DBNull.Value))
                        company.Website = "";
                    else
                        company.Website = reader.GetString(cPOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        company.AdditionalNotes = "";
                    else
                        company.AdditionalNotes = reader.GetString(cEOrd);
                    if (companyID == company.CompanyID) { break; }
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
            return company;
        }

        public static bool DeleteCompany(int companyID)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Company WHERE CompanyID = @CompanyID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@CompanyID", companyID);
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

        public static int AddCompany(Company company)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                //              "SET IDENTITY_INSERT Company ON; " +
                "INSERT Company " +
                  "(CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes) " +
                "VALUES (@CompanyName, @BuildingName, @BuildingNumber, @StreetAddress, " +
                "@City, @State, @ZipCode, @Website, @AdditionalNotes)"; 
    //            " SET IDENTITY_INSERT Company OFF;";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            insertCommand.Parameters.AddWithValue("@BuildingName", company.BuildingName);
            insertCommand.Parameters.AddWithValue("@BuildingNumber", company.BuildingNumber);
            insertCommand.Parameters.AddWithValue("@StreetAddress", company.StreetAddress);
            insertCommand.Parameters.AddWithValue("@City", company.City);
            insertCommand.Parameters.AddWithValue("@State", company.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", company.ZipCode);
            if (company.Website == null)
                insertCommand.Parameters.AddWithValue("@Website", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Website", company.Website);
            if (company.AdditionalNotes == null)
                insertCommand.Parameters.AddWithValue("@AdditionalNotes",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@AdditionalNotes",
                    company.AdditionalNotes);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Company') FROM Company";
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

        public static bool UpdateCompany(Company oldCompany, Company newCompany)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Company SET " +
                  "CompanyName = @NewCompanyName, " +
                  "BuildingName = @NewBuildingName, " +
                  "BuildingNumber = @NewBuildingNumber, " +
                  "StreetAddress = @NewStreetAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Website = @NewWebsite, " +
                  "AdditionalNotes = @NewAdditionalNotes " +
                "WHERE CompanyID = @OldCompanyID " +
                  "AND CompanyName = @OldCompanyName " +
                  "AND BuildingName = @OldBuildingName " +
                  "AND BuildingNumber = @OldBuildingNumber " +
                  "AND StreetAddress = @OldStreetAddress " +
                  "AND City = @OldCity " +
                  "AND State = @OldState " +
                  "AND ZipCode = @OldZipCode " +
                  "AND (Website = @OldWebsite " +
                      "OR Website IS NULL AND @OldWebsite IS NULL) " +
                  "AND (AdditionalNotes = @OldAdditionalNotes " +
                      "OR AdditionalNotes IS NULL AND @OldAdditionalNotes IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@NewCompanyName", newCompany.CompanyName);
            updateCommand.Parameters.AddWithValue("@NewBuildingName", newCompany.BuildingName);
            updateCommand.Parameters.AddWithValue("@NewBuildingNumber", newCompany.BuildingNumber);
            updateCommand.Parameters.AddWithValue("@NewStreetAddress", newCompany.StreetAddress);
            updateCommand.Parameters.AddWithValue("@NewCity", newCompany.City);
            updateCommand.Parameters.AddWithValue("@NewState", newCompany.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newCompany.ZipCode);
            if (newCompany.Website == "")
                updateCommand.Parameters.AddWithValue("@NewWebsite", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewWebsite", newCompany.Website);
            if (newCompany.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes",
                    newCompany.AdditionalNotes);

            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldCompany.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldCompanyName", oldCompany.CompanyName);
            updateCommand.Parameters.AddWithValue("@OldBuildingName", oldCompany.BuildingName);
            updateCommand.Parameters.AddWithValue("@OldBuildingNumber", oldCompany.BuildingNumber);
            updateCommand.Parameters.AddWithValue("@OldStreetAddress", oldCompany.StreetAddress);
            updateCommand.Parameters.AddWithValue("@OldCity", oldCompany.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldCompany.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldCompany.ZipCode);
            if (oldCompany.Website == "")
                updateCommand.Parameters.AddWithValue("@OldWebsite", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldWebsite", oldCompany.Website);
            if (oldCompany.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes",
                    oldCompany.AdditionalNotes);

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
        public static List<Company> GetCompanySorted(string columnName)
        {
            List<Company> companyList = new List<Company>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CompanyID, CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes FROM dbo.Company " + 
                "ORDER BY CASE WHEN @ColumnName = 'CompanyID' THEN CompanyID END ASC, " +
                "CASE WHEN @ColumnName = 'CompanyName' THEN CompanyName END ASC, " +
                "CASE WHEN @ColumnName = 'BuildingName' THEN BuildingName END ASC, " +
                "CASE WHEN @ColumnName = 'BuildingNumber' THEN BuildingName END ASC, " +
                "CASE WHEN @ColumnName = 'StreetAddress' THEN StreetAddress END ASC, " +
                "CASE WHEN @ColumnName = 'City' THEN City END ASC, " +
                "CASE WHEN @ColumnName = 'State' THEN State END ASC, " +
                "CASE WHEN @ColumnName = 'ZipCode' THEN ZipCode END ASC, " +
                "CASE WHEN @ColumnName = 'Website' THEN Website END ASC, " +
                "CASE WHEN @ColumnName = 'AdditionalNotes' THEN AdditionalNotes END ASC;";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("CompanyID"),
                    cNOrd = reader.GetOrdinal("CompanyName"),
                    cBNaOrd = reader.GetOrdinal("BuildingName"),
                    cBNuOrd = reader.GetOrdinal("BuildingNumber"),
                    cAOrd = reader.GetOrdinal("StreetAddress"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Website"),
                    cEOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Company comp = new Company();
                    comp.CompanyID = reader.GetInt32(cIDOrd);
                    comp.CompanyName = reader.GetString(cNOrd);
                    comp.BuildingName = reader.GetString(cBNaOrd);
                    comp.BuildingNumber = reader.GetString(cBNuOrd);
                    comp.StreetAddress = reader.GetString(cAOrd);
                    comp.City = reader.GetString(cCOrd);
                    comp.State = reader.GetString(cSOrd);
                    comp.ZipCode = reader.GetString(cZCOrd);
                    if (reader["Website"].Equals(DBNull.Value))
                        comp.Website = "";
                    else
                        comp.Website = reader.GetString(cPOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        comp.AdditionalNotes = "";
                    else
                        comp.AdditionalNotes = reader.GetString(cEOrd);
                    companyList.Add(comp);
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
            return companyList;
        }
        public static List<Company> GetCompanyFiltered(string columnName, string columnfilter)
        {
            int filtered = 0;
            List<Company> companyList = new List<Company>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CompanyID, CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes FROM dbo.Company "+
            "WHERE CASE WHEN @ColumnName = 'CompanyID' AND CompanyID = @Filter THEN 1 " +
            "WHEN @ColumnName = 'CompanyName' AND CompanyName LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'BuildingName' AND BuildingName LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'BuildingNumber' AND BuildingNumber LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'StreetAddress' AND StreetAddress LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'City' AND City LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'State' AND State LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'ZipCode' AND ZipCode LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'Website' AND Website LIKE '%' + @Filter + '%' THEN 1 " +
            "WHEN @ColumnName = 'AdditionalNotes' AND AdditionalNotes LIKE '%' + @Filter + '%' THEN 1 WHEN @ColumnName = '' THEN 1 ELSE 0 END = 1";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            if (columnName == "CompanyID")
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
                int cIDOrd = reader.GetOrdinal("CompanyID"),
                    cNOrd = reader.GetOrdinal("CompanyName"),
                    cBNaOrd = reader.GetOrdinal("BuildingName"),
                    cBNuOrd = reader.GetOrdinal("BuildingNumber"),
                    cAOrd = reader.GetOrdinal("StreetAddress"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Website"),
                    cEOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Company comp = new Company();
                    comp.CompanyID = reader.GetInt32(cIDOrd);
                    comp.CompanyName = reader.GetString(cNOrd);
                    comp.BuildingName = reader.GetString(cBNaOrd);
                    comp.BuildingNumber = reader.GetString(cBNuOrd);
                    comp.StreetAddress = reader.GetString(cAOrd);
                    comp.City = reader.GetString(cCOrd);
                    comp.State = reader.GetString(cSOrd);
                    comp.ZipCode = reader.GetString(cZCOrd);
                    if (reader["Website"].Equals(DBNull.Value))
                        comp.Website = "";
                    else
                        comp.Website = reader.GetString(cPOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        comp.AdditionalNotes = "";
                    else
                        comp.AdditionalNotes = reader.GetString(cEOrd);
                    companyList.Add(comp);
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
            return companyList;
        }

    }
}