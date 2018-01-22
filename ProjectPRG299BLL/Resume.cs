using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class Resume
    {
        private int resumeID;
        private string rSCDirectoryPath;
        private int schoolID;
        private int clientID;

        public Resume()
        {
            
        }

        public int ResumeID
        {
            get
            {
                return resumeID;
            }
            set { resumeID = value; }                
        }

        public string RSCDirectoryPath
        {
            get
            {
                return rSCDirectoryPath;
            }

            set
            {
                rSCDirectoryPath = value;
            }
        }

        public int SchoolID
        {
            get
            {
                return schoolID;
            }

            set
            {
                schoolID = value;
            }
        }

        public int ClientID
        {
            get
            {
                return clientID;
            }

            set
            {
                clientID = value;
            }
        }
    }
}
