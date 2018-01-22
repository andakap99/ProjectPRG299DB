using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299DB
{
    public class ContactPosition
    {
        private int contactID;
        private int positionID;

        public ContactPosition()
        {
            
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
    }
}
