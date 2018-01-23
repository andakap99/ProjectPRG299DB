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

        public static Client GetClientByRow(int clientID)
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
                    client.ClientID = reader.GetInt32(cIDOrd);
                    client.Name = reader.GetString(cNOrd);
                    client.Address = reader.GetString(cAOrd);
                    client.City = reader.GetString(cCOrd);
                    client.State = reader.GetString(cSOrd);
                    client.ZipCode = reader.GetString(cZCOrd);
                    client.Phone = reader.GetString(cPOrd);
                    client.Email = reader.GetString(cEOrd);
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
            return client;
        }

        public static bool DeleteClient(Client client)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Clients WHERE ClientID = @ClientID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ClientID", client.ClientID);
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

        public static int AddClient(Client client)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Clients " +
                  "(Name, Address, " +
                "City, State, ZipCode, Phone, Email) " +
                "VALUES (@Name, @Address, " +
                "@City, @State, @ZipCode, @Phone, @Email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", client.Name);
            insertCommand.Parameters.AddWithValue("@Address", client.Address);
            insertCommand.Parameters.AddWithValue("@City", client.City);
            insertCommand.Parameters.AddWithValue("@State", client.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", client.ZipCode);
            if (client.Phone == "")
                insertCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Phone", client.Phone);
            if (client.Email == "")
                insertCommand.Parameters.AddWithValue("@Email",
                    DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@Email",
                    client.Email);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Clients') FROM Clients";
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

        public static bool UpdateClient(Client oldClient, Client newClient)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Clients SET " +
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE ClientID = @OldClientID " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newClient.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newClient.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newClient.City);
            updateCommand.Parameters.AddWithValue("@NewState", newClient.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newClient.ZipCode);
            if (newClient.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newClient.Phone);
            if (newClient.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newClient.Email);

            updateCommand.Parameters.AddWithValue("@OldClientID", oldClient.ClientID);
            updateCommand.Parameters.AddWithValue("@OldName", oldClient.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldClient.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldClient.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldClient.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldClient.ZipCode);
            if (oldClient.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldClient.Phone);
            if (oldClient.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldClient.Email);

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