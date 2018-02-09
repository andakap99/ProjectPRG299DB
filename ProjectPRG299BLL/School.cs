using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class School
    {
        private int schoolID;
        private string schoolName;
        private string streetName;
        private string city;
        private string state;
        private string zipCode;
        private int numberOfYearsAttended;
        /// <summary></summary>
        private string graduated;

        public School()
        {

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

        public string Graduated
        {
            get
            {
                return graduated;
            }

            set
            {
                graduated = value;
            }
        }

        public int NumberOfYearsAttended
        {
            get
            {
                return numberOfYearsAttended;
            }

            set
            {
                numberOfYearsAttended = value;
            }
        }

        public int SchoolID
        {
            get
            {
                return schoolID;
            }
            set { schoolID = value; }            
        }

        public string SchoolName
        {
            get
            {
                return schoolName;
            }

            set
            {
                schoolName = value;
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
    }
}
