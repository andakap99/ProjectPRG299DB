using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class Interview
    {
        private int interviewID;
        private int positionID;
        private int companyID;
        private int contactID;
        private DateTime dateTimeInterview;
        private string additionalNotes;

        public Interview()
        {
            
        }

        public int InterviewID
        {
            get
            {
                return interviewID;
            }
            set { interviewID = value; }           
        }

        public int PositionID
        {
            get
            {
                return positionID;
            }

            set
            {
                positionID = value;
            }
        }

        public int CompanyID
        {
            get
            {
                return companyID;
            }

            set
            {
                companyID = value;
            }
        }

        public int ContactID
        {
            get
            {
                return contactID;
            }

            set
            {
                contactID = value;
            }
        }

        public DateTime DateTimeInterview
        {
            get
            {
                return dateTimeInterview;
            }

            set
            {
                dateTimeInterview = value;
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
