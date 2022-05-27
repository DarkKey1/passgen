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

        bool[] flags = new bool[16];
        /* 
            XXXXFFFFFFFFFFFF      F-flags X-reserved
            15  11         0

            0 - Lowercase letters
            1 - Uppercase letters
            2 - Numbers
            3 - Basic special chars
            4 - Extended special chars
            5 - Basic Safety of password
            6 - Extended Safety/Strong password
            7 - Password
            8 - Numbers
            9 - Hex
            10 - Bin
            11 - Less special chars
         
         */

        RandomGenerator rng = new RandomGenerator();

        public string Generate()
        {
            string passwordOutputString = "";
            if (!flags[0]&& !flags[1] && !flags[2] && !flags[3] && !flags[4]) //Checking if any type of symbols is checked
            {
                return passwordOutputString;
            }

            if (flags[7])
            {
                int lessSpecialCharsCounter = 0;
                for (int i = 0; i < length; i++)
                {
                    switch(rng.Next(0, 5))
                    {
                        case 0:
                            if(flags[0])
                            {
                                passwordOutputString = passwordOutputString + lowercaseChars[rng.Next(0,26)];
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                        break;
                        case 1:
                            if (flags[1])
                            {
                                passwordOutputString = passwordOutputString + uppercaseChars[rng.Next(0, 26)];
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                            break;
                        case 2:
                            if (flags[2])
                            {
                                passwordOutputString = passwordOutputString + numbers[rng.Next(0, 10)];
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                            break;
                        case 3:
                            if (flags[3])
                            {
                                if (lessSpecialCharsCounter == 2 || !flags[11])
                                {
                                    passwordOutputString = passwordOutputString + basicSpecialChars[rng.Next(0, 8)];
                                    lessSpecialCharsCounter = 0;
                                }
                                else
                                {
                                    lessSpecialCharsCounter++;
                                    i--;
                                    continue;
                                }
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                            break;
                        case 4:
                            if (flags[4])
                            {
                                if (lessSpecialCharsCounter == 2 || !flags[11])
                                {
                                    passwordOutputString = passwordOutputString + extendedSpecialChars[rng.Next(0, 20)];
                                    lessSpecialCharsCounter = 0;
                                }
                                else
                                {
                                    lessSpecialCharsCounter++;
                                    i--;
                                    continue;
                                }                            
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                            break;
                    }


                }


                if (flags[5] || flags[6])  //Enforcing Safety pasword
                {
                    List<int> indexes = new List<int>();

                    for (int i = 0; i < 4; i++)
                    {
                        int tmp = rng.Next(0, length);

                        if (!indexes.Contains(tmp))
                        {
                            indexes.Add(tmp);
                        }
                        else
                        {
                            i--;
                            continue;
                        }

                    }
                    StringBuilder stringBuilder = new StringBuilder(passwordOutputString);
                    stringBuilder[indexes[0]] = lowercaseChars[rng.Next(0, 26)];
                    stringBuilder[indexes[1]] = uppercaseChars[rng.Next(0, 26)];
                    stringBuilder[indexes[2]] = numbers[rng.Next(0, 10)];
                    stringBuilder[indexes[3]] = basicSpecialChars[rng.Next(0, 8)];

                    passwordOutputString = stringBuilder.ToString();
                }
            }

            return passwordOutputString;
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public bool[] Flags
        {
            get { return flags; }
            set { flags = value; }
        }
    }
}
