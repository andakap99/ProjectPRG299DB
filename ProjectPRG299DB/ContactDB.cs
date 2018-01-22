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
            finally
            {
                connection.Close();
            }
            return contactList;
        }
        public static List<Contact> GetContactByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeleteContact()
        {
            throw new System.NotImplementedException();
        }

        public static int AddContact()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdateContact()
        {
            throw new System.NotImplementedException();
        }
    }
}