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
                //passport.PrintPassport();
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
    }
}
