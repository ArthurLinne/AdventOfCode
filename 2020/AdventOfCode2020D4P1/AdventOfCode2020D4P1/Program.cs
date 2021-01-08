using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D4P1
{
    public class Program
    {

        public static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        static void Main()
        {
            string passportFile = System.IO.File.ReadAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D4P1\Passportfile.txt");

            passportFile = passportFile.Replace(Environment.NewLine, "|");
            passportFile = passportFile.Replace("||", "*");
            passportFile = passportFile.Replace("|", " ");

            List<string> passportInputList = passportFile.Split("*").ToList();

            List<Passport> passportList = new List<Passport>();

            foreach (string passportInput in passportInputList)
            {
                Passport newPassport = new Passport(passportInput);
                passportList.Add(newPassport);
            }

            int totalFilledPassports = 0;
            int totalValidPassports = 0;

            foreach (Passport passport in passportList)
            {
                passport.PrintPassport();
                if (passport.NecessaryFieldsPresent())
                {
                    totalFilledPassports++;
                }

                if (passport.FullPassportValid())
                {
                    totalValidPassports++;
                }
            }

            Console.WriteLine($"There are {totalFilledPassports} passports with all necessary fields present.");
            Console.WriteLine($"There are {totalValidPassports} passports with all fields valid.");
        }

        static void P1Main()
        {
            List<string> necessaryFields = new List<string>();

            necessaryFields.Add("byr");
            necessaryFields.Add("iyr");
            necessaryFields.Add("eyr");
            necessaryFields.Add("hgt");
            necessaryFields.Add("hcl");
            necessaryFields.Add("ecl");
            necessaryFields.Add("pid");
            
            string passportFile = System.IO.File.ReadAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D4P1\Passportfile.txt");

            passportFile = passportFile.Replace(Environment.NewLine, "|");
            passportFile = passportFile.Replace("||", "*");
            passportFile = passportFile.Replace("|", " ");

            List<string> passportList = passportFile.Split("*").ToList();

            int validPassports = 0;

            foreach (string passport in passportList)
            {
                int totalNecessaryFields = 0;

                foreach (string field in necessaryFields)
                {
                    if (passport.IndexOf(field + ":") >= 0)
                    {
                        Console.WriteLine($"Has the following field: {field}");
                        totalNecessaryFields++;
                    }
                    else
                    {
                        Console.WriteLine($"Lacks the following field: {field}");
                    }
                }

                if (totalNecessaryFields == 7)
                {
                    validPassports++;
                    Console.WriteLine("Valid Passport:");
                    Console.WriteLine(passport);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid Passport:");
                    Console.WriteLine(passport);
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"There are {validPassports} valid passports.");
        }

        static void P2Main()
        {
            
        }
    }
}
