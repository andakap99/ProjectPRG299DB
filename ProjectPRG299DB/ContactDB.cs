﻿using System;
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
                  "Name = @NewName, " +
                  "Address = @NewAddress, " +
                  "City = @NewCity, " +
                  "State = @NewState, " +
                  "ZipCode = @NewZipCode, " +
                  "Phone = @NewPhone, " +
                  "Email = @NewEmail " +
                "WHERE ContactID = @OldContactID " +
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
            updateCommand.Parameters.AddWithValue("@NewName", newContact.Name);
            updateCommand.Parameters.AddWithValue("@NewAddress", newContact.Address);
            updateCommand.Parameters.AddWithValue("@NewCity", newContact.City);
            updateCommand.Parameters.AddWithValue("@NewState", newContact.State);
            updateCommand.Parameters.AddWithValue("@NewZipCode", newContact.ZipCode);
            if (newContact.Phone == "")
                updateCommand.Parameters.AddWithValue("@NewPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewPhone", newContact.Phone);
            if (newContact.Email == "")
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@NewEmail",
                    newContact.Email);

            updateCommand.Parameters.AddWithValue("@OldContactID", oldContact.ContactID);
            updateCommand.Parameters.AddWithValue("@OldName", oldContact.Name);
            updateCommand.Parameters.AddWithValue("@OldAddress", oldContact.Address);
            updateCommand.Parameters.AddWithValue("@OldCity", oldContact.City);
            updateCommand.Parameters.AddWithValue("@OldState", oldContact.State);
            updateCommand.Parameters.AddWithValue("@OldZipCode", oldContact.ZipCode);
            if (oldContact.Phone == "")
                updateCommand.Parameters.AddWithValue("@OldPhone", DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldPhone", oldContact.Phone);
            if (oldContact.Email == "")
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    DBNull.Value);
            else
                updateCommand.Parameters.AddWithValue("@OldEmail",
                    oldContact.Email);

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