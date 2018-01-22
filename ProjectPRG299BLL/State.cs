using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPRG299BLL
{
    public class State
    {
        private string stateName;
        private string stateCode;
        private int firstZipCode;
        private int lastZipCode;

        public State()
        {

        }

        public string StateCode { get { return stateCode; } set { stateCode = value; } }
        public string StateName { get { return stateName; } set { stateName = value; } }
        public int FirstZipCode { get { return firstZipCode; } set { firstZipCode = value; } }
        public int LastZipCode { get { return lastZipCode; } set { lastZipCode = value; } }


    }
}
