using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class Client
    {
        private int clientID;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private string streetName;
        private string city;
        private string state;
        private string zipCode;
        private string cellPhone;

        public Client()
        {
            
        }

        public int ClientID
        {
            get
            {
                return clientID;
            }
            set { clientID = value; }
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

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }

            set
            {
                birthDate = value;
            }
        }

        public string StreetName
        {
            get
            {
                return streetName;
            }

            set
            {
                streetName = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return zipCode;
            }

            set
            {
                zipCode = value;
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
    }
}
