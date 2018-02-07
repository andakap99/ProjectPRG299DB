using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProjectPRG299BLL;

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
                    if (reader["CellPhone"].Equals(DBNull.Value))
                    {
                        clientA.CellPhone = "";
                    }
                    else
                    clientA.CellPhone = reader.GetString(cPOrd);
                    clientList.Add(clientA);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
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
                    client.ClientID = reader.GetInt32(cIDOrd);
                    client.FirstName = reader.GetString(cFOrd);
                    client.LastName = reader.GetString(cLOrd);
                    client.BirthDate = reader.GetDateTime(cBOrd);
                    client.StreetName = reader.GetString(cSTROrd);
                    client.City = reader.GetString(cCOrd);
                    client.State = reader.GetString(cSOrd);
                    client.ZipCode = reader.GetString(cZCOrd);
                    if (reader["CellPhone"].Equals(DBNull.Value))
                    {
                        client.CellPhone = "";
                    }
                    else
                        client.CellPhone = reader.GetString(cPOrd);
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
            return client;
        }

        public static bool DeleteClient(Client client)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Client WHERE ClientID = @ClientID";
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
            catch (Exception ex)
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
                "INSERT INTO Client " +
                  "(FirstName, LastName, BirthDate, StreetName, " +
                "City, State, ZipCode, CellPhone) " +
                "VALUES (@FirstName, @LastName, @BirthDate, @StreetName, " +
                "@City, @State, @ZipCode, @CellPhone)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@FirstName", client.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", client.LastName);
            insertCommand.Parameters.AddWithValue("@BirthDate", client.BirthDate);
            insertCommand.Parameters["@BirthDate"].SqlDbType = SqlDbType.DateTime;
            insertCommand.Parameters.AddWithValue("@StreetName", client.StreetName);
            insertCommand.Parameters.AddWithValue("@City", client.City );
            insertCommand.Parameters.AddWithValue("@State", client.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", client.ZipCode);
            if (client.CellPhone == null)
                insertCommand.Parameters.AddWithValue("@CellPhone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@CellPhone", client.CellPhone);

            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Client') FROM Client";
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

        public static bool UpdateClient(Client oldClient, Client newClient)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Client SET " +
                  "FirstName = @NewFirstName, " +
                  "LastName = @NewLastName, " +
                  "BirthDate = @NewBirthDate, " +
                  "StreetName = @NewStreetName, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "CellPhone = @NewCellPhone " +
                "WHERE ClientID = @OldClientID " +
                  "AND FirstName = @OldFirstName " +
                  "AND LastName = @OldLastName " +
                  "AND Birthdate = @OldBirthDate" +
                  "AND StreetName = @OldStreetName " +
                  "AND City = @OldCity " +
                  "AND State = @OldState " +
                  "AND ZipCode = @OldZipCode " +
                  "AND (CellPhone = @OldCellPhone " +
                      "OR CellPhone IS NULL AND @OldCellPhone IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@NewFirstName", newClient.FirstName);
            updateCommand.Parameters.AddWithValue("@NewLastName", newClient.LastName);
            updateCommand.Parameters.AddWithValue("@NewBirthDate", newClient.BirthDate);
            updateCommand.Parameters.AddWithValue("@NewStreetName", newClient.StreetName);
            updateCommand.Parameters.AddWithValue("@NewCity", newClient.City);
            updateCommand.Parameters.AddWithValue("@NewState", newClient.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newClient.ZipCode);
            if (newClient.CellPhone == null)
                updateCommand.Parameters.AddWithValue("@NewCellPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewCellPhone", newClient.CellPhone);

            updateCommand.Parameters.AddWithValue("@OldClientID", oldClient.ClientID);
            updateCommand.Parameters.AddWithValue("@OldFirstName", oldClient.FirstName);
            updateCommand.Parameters.AddWithValue("@OldLastName", oldClient.LastName);
            updateCommand.Parameters.AddWithValue("@OldBirthDate", oldClient.BirthDate);
            updateCommand.Parameters.AddWithValue("@OldStreetName", oldClient.StreetName);
            updateCommand.Parameters.AddWithValue("@OldCity", oldClient.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldClient.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldClient.ZipCode);
            if (oldClient.CellPhone == null)
                updateCommand.Parameters.AddWithValue("@OldCellPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldCellPhone", oldClient.CellPhone);

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