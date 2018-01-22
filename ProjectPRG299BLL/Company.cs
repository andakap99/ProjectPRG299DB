using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class Company
    {
        private int companyID;
        private string companyName;
        private string buildingName;
        private string buildingNumber;
        private string streetAddress;
        private string city;
        private string state;
        private string zipCode;
        private string website;
        private string additionalNotes;

        public Company()
        {
            
        }

        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set { companyID = value; }           
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }

            set
            {
                companyName = value;
            }
        }

        public string BuildingName
        {
            get
            {
                return buildingName;
            }

            set
            {
                buildingName = value;
            }
        }

        public string BuildingNumber
        {
            get
            {
                return buildingNumber;
            }

            set
            {
                buildingNumber = value;
            }
        }

        public string StreetAddress
        {
            get
            {
                return streetAddress;
            }

            set
            {
                streetAddress = value;
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

        public string Website
        {
            get
            {
                return website;
            }

            set
            {
                website = value;
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
