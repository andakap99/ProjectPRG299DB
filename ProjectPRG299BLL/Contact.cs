using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class Contact
    {
        private int contactID;
        private string firstName;
        private string lastName;
        private string emailAddress;
        private string phoneNumber;
        private string cellPhone;
        private string additionalNotes;

        public Contact()
        {
            
        }

        public int ContactID
        {
            get
            {
                return contactID;
            }
            set { contactID = value; }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                emailAddress = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                phoneNumber = value;
            }
        }

        public string CellPhone
        {
            get
            {
                return cellPhone;
            }

            set
            {
                cellPhone = value;
            }
        }

        public string AdditionalNotes
        {
            get
            {
                return additionalNotes;
            }

            set
            {
                additionalNotes = value;
            }
        }
    }
}
