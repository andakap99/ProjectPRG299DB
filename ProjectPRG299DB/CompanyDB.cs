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
            string selectStatement = "SELECT CompanyID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Companys WHERE CompanyID = @CompanyID";
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
            finally
            {
                connection.Close();
            }
            return company;
        }

        public static bool DeleteCompany(Company company)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Companys WHERE CompanyID = @CompanyID";
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
            finally
            {
                connection.Close();
            }
        }

        public static int AddCompany(Company company)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Companys " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", company.Name);
            insertCommand.Parameters.AddWithValue("@Address", company.Address);
            insertCommand.Parameters.AddWithValue("@City", company.City);
            insertCommand.Parameters.AddWithValue("@State", company.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", company.ZipCode);
            if (company.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", company.Phone);
            if (company.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    company.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Companys') FROM Companys";
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

        public static bool UpdateCompany(Company oldCompany, Company newCompany)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Companys SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newCompany.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newCompany.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newCompany.City);
            updateCommand.Parameters.AddWithValue("@NewState", newCompany.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newCompany.ZipCode);
            if (newCompany.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newCompany.Phone);
            if (newCompany.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newCompany.Email);

            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldCompany.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldName", oldCompany.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldCompany.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldCompany.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldCompany.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldCompany.ZipCode);
            if (oldCompany.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldCompany.Phone);
            if (oldCompany.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldCompany.Email);

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