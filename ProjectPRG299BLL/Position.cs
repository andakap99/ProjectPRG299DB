using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class Position
    {
        private int positionID;
        private string positionName;
        private string description;
        private int companyID;
        private string additionalNotes;
        private int resumeID;

        public Position()
        {
 
        }

        public int PositionID
        {
            get
            {
                return positionID;
            }
            set { positionID = value; }   
        }

        public string PositionName
        {
            get
            {
                return positionName;
            }

            set
            {
                positionName = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
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

        public int ResumeID
        {
            get
            {
                return resumeID;
            }

            set
            {
                resumeID = value;
            }
        }
    }
}
