using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class ContactDB
    {
        public static List<Contact> GetContact()
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
                    cont.EmailAddress = reader.GetString(cCOrd);
                    cont.PhoneNumber = reader.GetString(cSOrd);
                    cont.CellPhone = reader.GetString(cZCOrd);
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
        public static Contact GetContactByRow(int contactID)
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
                    contact.EmailAddress = reader.GetString(cCOrd);
                    contact.PhoneNumber = reader.GetString(cSOrd);
                    contact.CellPhone = reader.GetString(cZCOrd);
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

        public static bool DeleteContact(Contact contact)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string deleteStatement = "DELETE FROM Contact WHERE ContactID = @ContactID";
            SqlCommand DeleteCommand = new SqlCommand(deleteStatement, connection);
            DeleteCommand.Parameters.AddWithValue("@ContactID", contact.ContactID);
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

        public static int AddContact(Contact contact)
        {
            SqlConnection connection = PRG299DB.GetConnection();
            string insertStatement =
                "INSERT Contact " +
                  "(FirstName, LastName, " +
                "EmailAddress, PhoneNumber, CellPhone, AdditionalNotes) " +
                "VALUES (@FirstName, @LastName, " +
                "@EmailAddress, @PhoneNumber, @CellPhone, @AdditionalNotes)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", contact.LastName);
            if(contact.EmailAddress == "")
                insertCommand.Parameters.AddWithValue("@EmailAddress", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@EmailAddress", contact.EmailAddress);
            if(contact.PhoneNumber == "")
                insertCommand.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
            if(contact.CellPhone == "")
                insertCommand.Parameters.AddWithValue("@CellPhone", DBNull.Value);
            else
                insertCommand.Parameters.AddWithValue("@CellPhone", contact.CellPhone);
            if(contact.AdditionalNotes == "")
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

        public static bool UpdateContact(Contact oldContact, Contact newContact)
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
                  "AND FirstName = @OldName " +
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
    }
}