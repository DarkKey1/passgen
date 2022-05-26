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
        const string lowercaseChars = "";
        const string uppercaseChars = "";
        const string numbers = "0123456789";
        const string basicSpecialChars = "";
        const string extendedSpecialChars = "";
        bool safe8 = false;
        bool safe16 = false;

        public string Generate()
        {

            return "yyyggyg";
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
    }
}
