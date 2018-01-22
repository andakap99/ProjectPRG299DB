using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class InterviewDB
    {
        public static List<Interview> GetInterview()
        {
            List<Interview> interviewList = new List<Interview>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CustomerID, Name, Address, " +
                "City, State, ZipCode, Phone, Email FROM dbo.Interview";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("InterviewID"),
                    cNOrd = reader.GetOrdinal("PositionID"),
                    cAOrd = reader.GetOrdinal("CompanyID"),
                    cCOrd = reader.GetOrdinal("DateTime"),
                    cSOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Interview interv = new Interview();
                    interv.InterviewID = reader.GetInt32(cIDOrd);
                    interv.PositionID = reader.GetInt32(cNOrd);
                    interv.CompanyID = reader.GetInt32(cAOrd);
                    interv.DateTimeInterview = reader.GetDateTime(cCOrd);
                    interv.AdditionalNotes = reader.GetString(cSOrd);
                    interviewList.Add(interv);
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
            return interviewList;
        }
        public static List<Interview> GetInterviewByRow()
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

        public static bool DeleteInterview()
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

        public static int AddInterview()
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

        public static bool UpdateInterview()
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