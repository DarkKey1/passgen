using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace passgen
{
    public class PassGeneration
    {
        int length = 8;
        const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz"; //26
        const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //26
        const string numbers = "0123456789"; //10
        const string basicSpecialChars = "!@#$%^&*"; //8
        const string extendedSpecialChars = "~`_-+={}[]|\\:;<>,.?/";//20
        bool safe8 = false;
        bool safe16 = false;

        RandomGenerator rng = new RandomGenerator();

        public string Generate()
        {
            string passOut = "";

            for(int i=0; i < length;i++)
            {
                passOut = passOut + i;
            }






            return passOut;
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
    }
}
