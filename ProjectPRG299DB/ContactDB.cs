using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ContactDB
    {
        public static List<Contact> GetContactCombobox() // USED TO POPULATE THE DATA GRID TABLE ALSO USED TO REFRESH THE DATA GRID
        {
            List<Contact> contactList = new List<Contact>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, FirstName + ' ' + LastName AS LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes FROM dbo.Contact";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cAOrd = reader.GetOrdinal("LastName"),
                    cCOrd = reader.GetOrdinal("EmailAddress"),
                    cSOrd = reader.GetOrdinal("PhoneNumber"),
                    cZCOrd = reader.GetOrdinal("CellPhone"),
                    cPOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Contact cont = new Contact();
                    cont.ContactID = reader.GetInt32(cIDOrd);
                    cont.LastName = reader.GetString(cAOrd);
                    if (reader["EmailAddress"].Equals(DBNull.Value))
                        cont.EmailAddress = "";
                    else
                        cont.EmailAddress = reader.GetString(cCOrd);
                    if (reader["PhoneNumber"].Equals(DBNull.Value))
                        cont.PhoneNumber = "";
                    else
                        cont.PhoneNumber = reader.GetString(cSOrd);
                    if (reader["CellPhone"].Equals(DBNull.Value))
                        cont.CellPhone = "";
                    else
                        cont.CellPhone = reader.GetString(cZCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        cont.AdditionalNotes = "";
                    else
                        cont.AdditionalNotes = reader.GetString(cPOrd);
                    contactList.Add(cont);
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
            return contactList;
        }
        public static List<Contact> GetContact() // USED TO POPULATE THE DATA GRID TABLE ALSO USED TO REFRESH THE DATA GRID
        {
            List<Contact> contactList = new List<Contact>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, FirstName, LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes FROM dbo.Contact";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cNOrd = reader.GetOrdinal("FirstName"),
                    cAOrd = reader.GetOrdinal("LastName"),
                    cCOrd = reader.GetOrdinal("EmailAddress"),
                    cSOrd = reader.GetOrdinal("PhoneNumber"),
                    cZCOrd = reader.GetOrdinal("CellPhone"),
                    cPOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Contact cont = new Contact();
                    cont.ContactID = reader.GetInt32(cIDOrd);
                    cont.FirstName = reader.GetString(cNOrd);
                    cont.LastName = reader.GetString(cAOrd);
                    if (reader["EmailAddress"].Equals(DBNull.Value))
                        cont.EmailAddress = "";
                    else
                        cont.EmailAddress = reader.GetString(cCOrd);
                    if (reader["PhoneNumber"].Equals(DBNull.Value))
                        cont.PhoneNumber = "";
                    else
                        cont.PhoneNumber = reader.GetString(cSOrd);
                    if (reader["CellPhone"].Equals(DBNull.Value))
                        cont.CellPhone = "";
                    else
                        cont.CellPhone = reader.GetString(cZCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        cont.AdditionalNotes = "";
                    else
                        cont.AdditionalNotes = reader.GetString(cPOrd);
                    contactList.Add(cont);
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
            return contactList;
        }
        public static Contact GetContactByRow(int contactID) // GETS ONE ROW AT A TIME FROM THE DATABASE
        {
            Contact contact = new Contact();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, FirstName, LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes FROM dbo.Contact WHERE ContactID = @ContactID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ContactID", contactID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cNOrd = reader.GetOrdinal("FirstName"),
                    cAOrd = reader.GetOrdinal("LastName"),
                    cCOrd = reader.GetOrdinal("EmailAddress"),
                    cSOrd = reader.GetOrdinal("PhoneNumber"),
                    cZCOrd = reader.GetOrdinal("CellPhone"),
                    cPOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    contact.ContactID = reader.GetInt32(cIDOrd);
                    contact.FirstName = reader.GetString(cNOrd);
                    contact.LastName = reader.GetString(cAOrd);
                    if (reader["EmailAddress"].Equals(DBNull.Value))
                        contact.EmailAddress = "";
                    else
                        contact.EmailAddress = reader.GetString(cCOrd);
                    if (reader["PhoneNumber"].Equals(DBNull.Value))
                        contact.PhoneNumber = "";
                    else 
                        contact.PhoneNumber = reader.GetString(cSOrd);
                    if (reader["CellPhone"].Equals(DBNull.Value))
                        contact.CellPhone = "";
                    else
                        contact.CellPhone = reader.GetString(cZCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        contact.AdditionalNotes = "";
                    else
                        contact.AdditionalNotes = reader.GetString(cPOrd);
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
            return contact;
        }

        public static bool DeleteContact(int contactID)// DELETES A ROW FROM THE DATABASE
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Contact WHERE ContactID = @ContactID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ContactID", contactID);
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

        public static int AddContact(Contact contact) // ADDS A NEW ROW TO THE DATABASE 
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
            //    "SET IDENTITY_INSERT Contact ON; " +
                "INSERT Contact " +
                  "(FirstName, LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes) " +
                "VALUES (@FirstName, @LastName, " +
                "@EmailAddress, @PhoneNumber, @CellPhone, @AdditionalNotes);";
              //  "SET IDENTITY_INSERT Contact OFF;";

            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", contact.LastName);
            if(contact.EmailAddress == null)
                insertCommand.Parameters.AddWithValue("@EmailAddress", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@EmailAddress", contact.EmailAddress);
            if(contact.PhoneNumber == null)
                insertCommand.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
            if(contact.CellPhone == null)
                insertCommand.Parameters.AddWithValue("@CellPhone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@CellPhone", contact.CellPhone);
            if(contact.AdditionalNotes == null)
                insertCommand.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@AdditionalNotes", contact.AdditionalNotes);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Contact') FROM Contact";
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

        public static bool UpdateContact(Contact oldContact, Contact newContact) // MODIFIES THE DATABASE A ROW AT A TIME
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string updateStatement =
                "UPDATE Contact SET " +
                  "FirstName = @NewFirstName, " +
                  "LastName = @NewLastName, " +
                  "EmailAddress = @NewEmailAddress, " +
                  "PhoneNumber = @NewPhoneNumber, " +
                  "CellPhone = @NewCellPhone, " +
                  "AdditionalNotes = @NewAdditionalNotes " +
                "WHERE ContactID = @OldContactID " +
                  "AND FirstName = @OldFirstName " +
                  "AND LastName = @OldLastName " +
                  "AND (EmailAddress = @OldEmailAddress " +
                      "OR EmailAddress IS NULL AND @OldEmailAddress IS NULL) " +
                  "AND (PhoneNumber = @OldPhoneNumber " +
                      "OR PhoneNumber IS NULL AND @OldPhoneNumber IS NULL)" +
                  "AND (CellPhone = @OldCellPhone " +
                      "OR CellPhone IS NULL AND @OldCellPhone IS NULL) " +
                  "AND (AdditionalNotes = @OldAdditionalNotes " +
                      "OR AdditionalNotes IS NULL AND @OldAdditionalNotes IS NULL)";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@NewFirstName", newContact.FirstName);
            updateCommand.Parameters.AddWithValue("@NewLastName", newContact.LastName);
            if (newContact.EmailAddress == "")
                updateCommand.Parameters.AddWithValue("@NewEmailAddress", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmailAddress", newContact.EmailAddress);
            if (newContact.PhoneNumber == "")
                updateCommand.Parameters.AddWithValue("@NewPhoneNumber", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhoneNumber", newContact.PhoneNumber);
            if (newContact.CellPhone == "")
                updateCommand.Parameters.AddWithValue("@NewCellPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewCellPhone", newContact.CellPhone);
            if (newContact.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewAdditionalNotes", newContact.AdditionalNotes);

            updateCommand.Parameters.AddWithValue("@OldContactID", oldContact.ContactID);
            updateCommand.Parameters.AddWithValue("@OldFirstName", oldContact.FirstName);
            updateCommand.Parameters.AddWithValue("@OldLastName", oldContact.LastName);
            if (oldContact.EmailAddress == "")
                updateCommand.Parameters.AddWithValue("@OldEmailAddress", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmailAddress", oldContact.EmailAddress);
            if (oldContact.PhoneNumber == "")
                updateCommand.Parameters.AddWithValue("@OldPhoneNumber", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhoneNumber", oldContact.PhoneNumber);
            if (oldContact.CellPhone == "")
                updateCommand.Parameters.AddWithValue("@OldCellPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldCellPhone", oldContact.CellPhone);
            if (oldContact.AdditionalNotes == "")
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldAdditionalNotes", oldContact.AdditionalNotes);

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
        public static List<Contact> GetContactSorted(string columnName) // SORTS THE DATA IN THE DATABASE
        {
            List<Contact> contactList = new List<Contact>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT ContactID, FirstName, LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes FROM dbo.Contact " +
                "ORDER BY CASE WHEN @ColumnName = 'ContactID' THEN ContactID END ASC, " +
                "CASE WHEN @ColumnName = 'FirstName' THEN FirstName END ASC, " +
                "CASE WHEN @ColumnName = 'LastName' THEN LastName END ASC, " +
                "CASE WHEN @ColumnName = 'EmailAddress' THEN EmailAddress END ASC, " +
                "CASE WHEN @ColumnName = 'PhoneNumber' THEN PhoneNumber END ASC, " +
                "CASE WHEN @ColumnName = 'CellPhone' THEN CellPhone END ASC, " +
                "CASE WHEN @ColumnName = 'AdditionalNotes' THEN AdditionalNotes END ASC;";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cNOrd = reader.GetOrdinal("FirstName"),
                    cAOrd = reader.GetOrdinal("LastName"),
                    cCOrd = reader.GetOrdinal("EmailAddress"),
                    cSOrd = reader.GetOrdinal("PhoneNumber"),
                    cZCOrd = reader.GetOrdinal("CellPhone"),
                    cPOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Contact cont = new Contact();
                    cont.ContactID = reader.GetInt32(cIDOrd);
                    cont.FirstName = reader.GetString(cNOrd);
                    cont.LastName = reader.GetString(cAOrd);
                    if (reader["EmailAddress"].Equals(DBNull.Value))
                        cont.EmailAddress = "";
                    else
                        cont.EmailAddress = reader.GetString(cCOrd);
                    if (reader["PhoneNumber"].Equals(DBNull.Value))
                        cont.PhoneNumber = "";
                    else
                        cont.PhoneNumber = reader.GetString(cSOrd);
                    if (reader["CellPhone"].Equals(DBNull.Value))
                        cont.CellPhone = "";
                    else
                        cont.CellPhone = reader.GetString(cZCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        cont.AdditionalNotes = "";
                    else
                        cont.AdditionalNotes = reader.GetString(cPOrd);
                    contactList.Add(cont);
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
            return contactList;
        }
        public static List<Contact> GetContactFiltered(string columnName, string columnfilter)// PUTS A FILTER TO THE DATABASE
        {
            int filtered = 0;

            List<Contact> contactList = new List<Contact>();
            SqlConnection connection = PRG299DB.GetConnection();
            /* 
                When you pass any column name and filter which matches to any records and per column name, it will return those records.             
                When column name matches and no record matches as per column name it fallback to last ELSE part so it won't return any records as expected.
                In one special case when you don't mention any column name i.e. @ColumnName = '' then all rows will be returned as you didn't want to filter.
             */
            string selectStatement = "SELECT ContactID, FirstName, LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes FROM dbo.Contact "+
                "WHERE CASE WHEN @ColumnName = 'ContactID' AND ContactID = @Filter THEN 1 " +
                "WHEN @ColumnName = 'FirstName' AND FirstName LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'LastName' AND LastName LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'EmailAddress' AND EmailAddress LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'PhoneNumber' AND PhoneNumber LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'CellPhone' AND CellPhone LIKE '%' + @Filter + '%' THEN 1 " +
                "WHEN @ColumnName = 'AdditionalNotes' AND AdditionalNotes LIKE '%' + @Filter + '%' THEN 1 WHEN @ColumnName = '' THEN 1 ELSE 0 END = 1";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ColumnName", columnName);
            if (columnName == "ContactID")
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
                int cIDOrd = reader.GetOrdinal("ContactID"),
                    cNOrd = reader.GetOrdinal("FirstName"),
                    cAOrd = reader.GetOrdinal("LastName"),
                    cCOrd = reader.GetOrdinal("EmailAddress"),
                    cSOrd = reader.GetOrdinal("PhoneNumber"),
                    cZCOrd = reader.GetOrdinal("CellPhone"),
                    cPOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Contact cont = new Contact();
                    cont.ContactID = reader.GetInt32(cIDOrd);
                    cont.FirstName = reader.GetString(cNOrd);
                    cont.LastName = reader.GetString(cAOrd);
                    if (reader["EmailAddress"].Equals(DBNull.Value))
                        cont.EmailAddress = "";
                    else
                        cont.EmailAddress = reader.GetString(cCOrd);
                    if (reader["PhoneNumber"].Equals(DBNull.Value))
                        cont.PhoneNumber = "";
                    else
                        cont.PhoneNumber = reader.GetString(cSOrd);
                    if (reader["CellPhone"].Equals(DBNull.Value))
                        cont.CellPhone = "";
                    else
                        cont.CellPhone = reader.GetString(cZCOrd);
                    if (reader["AdditionalNotes"].Equals(DBNull.Value))
                        cont.AdditionalNotes = "";
                    else
                        cont.AdditionalNotes = reader.GetString(cPOrd);
                    contactList.Add(cont);
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
            return contactList;
        }

    }
}