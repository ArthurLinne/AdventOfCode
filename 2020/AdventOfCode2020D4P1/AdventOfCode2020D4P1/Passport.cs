using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D4P1
{
    public class Passport
    {
        private readonly string birthYear = "";
        private readonly string expirationYear = "";
        private readonly string eyeColor = "";
        private readonly string hairColor = "";
        private readonly string height = "";
        private readonly string issueYear = "";
        private readonly string passportId = "";
        private readonly string countryId = "";

        public Passport(string passportInput)
        {
            List<string> fieldList = passportInput.Split(" ").ToList();

            foreach (string field in fieldList)
            {
                string fieldName = field.Substring(0, field.IndexOf(":"));
                string fieldValue = field.Substring(field.IndexOf(":") + 1, field.Length - field.IndexOf(":") - 1);

                switch (fieldName)
                {
                    case "byr":
                        birthYear = fieldValue;
                        break;
                    case "iyr":
                        issueYear = fieldValue;
                        break;
                    case "eyr":
                        expirationYear = fieldValue;
                        break;
                    case "hgt":
                        height = fieldValue;
                        break;
                    case "hcl":
                        hairColor = fieldValue;
                        break;
                    case "ecl":
                        eyeColor = fieldValue;
                        break;
                    case "pid":
                        passportId = fieldValue;
                        break;
                    case "cid":
                        countryId = fieldValue;
                        break;
                    default:
                        Console.WriteLine("I've never heard of this passport field!");
                        break;
                }
            }
        }
        
        public bool BirthYearValid()
        {
            try
            {
                int birthYearNum = Int32.Parse(birthYear);

                if (1920 <= birthYearNum && birthYearNum <= 2020)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
        
        public bool IssueYearValid()
        {
            try
            {
                int issueYearNum = Int32.Parse(issueYear);

                if (2010 <= issueYearNum && issueYearNum <= 2020)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
        
        public bool ExpirationYearValid()
        {
            try
            {
                int expYear = Int32.Parse(expirationYear);

                if (2020 <= expYear && expYear <= 2030)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
        
        public bool HeightValid()
        {
            if (height == "")
            {
                return false;
            }

            try
            {
                int heightNum = Int32.Parse(height.Substring(0, height.Length - 2));
                string units = height.Substring(height.Length - 2, 2);

                int minHeight = 0;
                int maxHeight = 0;

                switch (units)
                {
                    case "cm":
                        minHeight = 150;
                        maxHeight = 193;
                        break;
                    case "in":
                        minHeight = 59;
                        maxHeight = 76;
                        break;
                    default:
                        break;
                }

                if (minHeight <= heightNum && heightNum <= maxHeight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool HairColorValid()
        {
            if (hairColor == "")
            {
                return false;
            }

            try
            {
                char identifier = hairColor[0];

                if (identifier != '#')
                {
                    return false;
                }

                string hairColorValues = hairColor.Substring(1);

                foreach (char character in hairColorValues)
                {
                    if (!(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' }.Contains(character)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool EyeColorValid()
        {
            if (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(eyeColor))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PassportIdValid()
        {
            try
            {
                if (passportId.Length != 9)
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        public bool CountryIdValid()
        {
            return true;
        }

        public bool NecessaryFieldsPresent()
        {
            if (
                birthYear           == ""
                || expirationYear   == ""
                || eyeColor         == ""
                || hairColor        == ""
                || height           == ""
                || issueYear        == ""
                || passportId       == ""
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool FullPassportValid()
        {
            if (
                BirthYearValid()
                && IssueYearValid()
                && ExpirationYearValid()
                && HeightValid()
                && HairColorValid()
                && EyeColorValid()
                && PassportIdValid()
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PrintPassport()
        {
            Console.WriteLine("{0}: {1}", "birthYear", birthYear);
            Console.WriteLine("{0}: {1}", "expirationYear", expirationYear);
            Console.WriteLine("{0}: {1}", "eyeColor", eyeColor);
            Console.WriteLine("{0}: {1}", "hairColor", hairColor);
            Console.WriteLine("{0}: {1}", "height", height);
            Console.WriteLine("{0}: {1}", "issueYear", issueYear);
            Console.WriteLine("{0}: {1}", "passportId", passportId);
            Console.WriteLine("{0}: {1}", "countryId", countryId);

            if (NecessaryFieldsPresent())
            {
                Console.WriteLine("All fields present");
            }
            else
            {
                Console.WriteLine("Some fields missing");
            }

            if (FullPassportValid())
            {
                Console.WriteLine("Valid Passport");
            }
            else
            {
                Console.WriteLine("Invalid Passport");
            }

            Console.WriteLine();

        }
    }
}
