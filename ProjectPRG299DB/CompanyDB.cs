using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    comp.Website = reader.GetString(cPOrd);
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
                    company.Website = reader.GetString(cPOrd);
                    company.AdditionalNotes = reader.GetString(cEOrd);
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

        public static bool DeleteCompany(Company company)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Company WHERE CompanyID = @CompanyID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@CompanyID", company.CompanyID);
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
                "INSERT Company " +
                  "(CompanyID, CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes) " +
                "VALUES (@CompanyID, @CompanyName, @BuildingName, @BuildingNumber, @StreetAddress, " +
                "@City, @State, @ZipCode, @Website, @AdditionalNotes)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@CompanyID", company.CompanyID);
            insertCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            insertCommand.Parameters.AddWithValue("@BuildingName", company.BuildingName);
            insertCommand.Parameters.AddWithValue("@BuildingNumber", company.BuildingNumber);
            insertCommand.Parameters.AddWithValue("@StreetAddress", company.StreetAddress);
            insertCommand.Parameters.AddWithValue("@City", company.City);
            insertCommand.Parameters.AddWithValue("@State", company.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", company.ZipCode);
            if (company.Website == "")
                insertCommand.Parameters.AddWithValue("@Website", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Website", company.Website);
            if (company.AdditionalNotes == "")
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
    }
}