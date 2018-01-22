using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace ProjectPRG299DB
{
    public static class ClientDB
    {
        public static List<Client> GetClient()
        {
            List<Client> clientList = new List<Client>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ClientID, FirstName, LastName, BirthDate, StreetName, " +
                "City, State, ZipCode, CellPhone FROM dbo.Client";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ClientID"),
                    cFOrd = reader.GetOrdinal("FirstName"),
                    cLOrd = reader.GetOrdinal("LastName"),
                    cBOrd = reader.GetOrdinal("BirthDate"),
                    cSTROrd = reader.GetOrdinal("StreetName"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("CellPhone");
                while (reader.Read())
                {
                    Client clientA = new Client();
                    clientA.ClientID = reader.GetInt32(cIDOrd);
                    clientA.FirstName = reader.GetString(cFOrd);
                    clientA.LastName = reader.GetString(cLOrd);
                    clientA.BirthDate = reader.GetDateTime(cBOrd);
                    clientA.StreetName = reader.GetString(cSTROrd);
                    clientA.City = reader.GetString(cCOrd);
                    clientA.State = reader.GetString(cSOrd);
                    clientA.ZipCode = reader.GetString(cZCOrd);
                    clientA.CellPhone = reader.GetString(cPOrd);
                    clientList.Add(clientA);
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
            return clientList;
        }

        public static List<Client> GetClientByRow(int clientID)
        {
            Client client = new Client();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ClientID, FirstName, LastName, BirthDate, StreetName, " +
                "City, State, ZipCode, CellPhone FROM dbo.Client WHERE ClientID = @ClientID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ClientID", clientID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ClientID"),
                    cFNOrd = reader.GetOrdinal("FirstName"),
                    cLNOrd = reader.GetOrdinal("LastName"),
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

        public static bool DeleteClient()
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

        public static int AddClient()
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

        public static bool UpdateClient()
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