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
        public static List<Company> GetCompanyByRow()
        {
            Customer customer = new Customer();
            SqlConnection connection = TechSupportDB.GetConnection();
            string selectStatement = "SELECT CustomerID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Customers WHERE CustomerID = @CustomerID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@CustomerID", customerID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("CustomerID"),
                    cNOrd = reader.GetOrdinal("Name"),
                    cAOrd = reader.GetOrdinal("Address"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Phone"),
                    cEOrd = reader.GetOrdinal("Email");
                while (reader.Read())
                {
                    customer.CustomerID = reader.GetInt32(cIDOrd);
                    customer.Name = reader.GetString(cNOrd);
                    customer.Address = reader.GetString(cAOrd);
                    customer.City = reader.GetString(cCOrd);
                    customer.State = reader.GetString(cSOrd);
                    customer.ZipCode = reader.GetString(cZCOrd);
                    customer.Phone = reader.GetString(cPOrd);
                    customer.Email = reader.GetString(cEOrd);
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
            return customer;
        }

        public static bool DeleteCompany()
        {
            SqlConnection connection = TechSupportDB.GetConnection();
            string deleteStatement = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@CustomerID", cust.CustomerID);
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

        public static int AddCompany()
        {
            SqlConnection connection = TechSupportDB.GetConnection();
            string insertStatement =
                "INSERT Customers " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", customer.Name);
            insertCommand.Parameters.AddWithValue("@Address", customer.Address);
            insertCommand.Parameters.AddWithValue("@City", customer.City);
            insertCommand.Parameters.AddWithValue("@State", customer.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
            if (customer.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", customer.Phone);
            if (customer.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    customer.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Customers') FROM Customers";
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

        public static bool UpdateCompany()
        {
            SqlConnection connection = TechSupportDB.GetConnection();
            string updateStatement =
                "UPDATE Customers SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE CustomerID = @OldCustomerID " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newCustomer.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newCustomer.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newCustomer.City);
            updateCommand.Parameters.AddWithValue("@NewState", newCustomer.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newCustomer.ZipCode);
            if (newCustomer.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newCustomer.Phone);
            if (newCustomer.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newCustomer.Email);

            updateCommand.Parameters.AddWithValue("@OldCustomerID", oldCustomer.CustomerID);
            updateCommand.Parameters.AddWithValue("@OldName", oldCustomer.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldCustomer.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldCustomer.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldCustomer.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldCustomer.ZipCode);
            if (oldCustomer.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldCustomer.Phone);
            if (oldCustomer.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldCustomer.Email);

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