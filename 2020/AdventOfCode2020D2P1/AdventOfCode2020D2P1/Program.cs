using System;

namespace AdventOfCode2020D2P1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] passwordInfoList = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D2P1\PasswordList.txt");

            int validPasswordCount = 0;
            int invalidPasswordCount = 0;

            foreach (string passwordInfo in passwordInfoList)
            {
                string ruleCharacter;
                int minOccurence;
                int maxOccurence;
                string potentialPassword;

                minOccurence = Int32.Parse(passwordInfo.Substring(0, passwordInfo.IndexOf("-")));

                maxOccurence = Int32.Parse(passwordInfo.Substring(passwordInfo.IndexOf("-") + 1, passwordInfo.IndexOf(" ") - passwordInfo.IndexOf("-") - 1));

                ruleCharacter = passwordInfo.Substring(passwordInfo.IndexOf(":") - 1, 1);

                potentialPassword = passwordInfo.Substring(passwordInfo.IndexOf(":") + 2);

                int ruleOccurence = potentialPassword.Length - potentialPassword.Replace(ruleCharacter, "").Length;

                /*
                Console.WriteLine(passwordInfo);
                Console.WriteLine($"Min Occurence: {minOccurence}");
                Console.WriteLine($"Max Occurence: {maxOccurence}");
                Console.WriteLine($"Rule Character: {ruleCharacter}");
                Console.WriteLine($"Potential Password: {potentialPassword}");
                Console.WriteLine($"Rule Occurence: {ruleOccurence}");
                */

                if (minOccurence <= ruleOccurence && ruleOccurence <= maxOccurence)
                {
                    validPasswordCount++;
                }
                else
                {
                    invalidPasswordCount++;
                }

                //Console.WriteLine();

            }

            Console.WriteLine($"The total number of valid passwords is {validPasswordCount}");
            Console.WriteLine($"The total number of invalid passwords is {invalidPasswordCount}");
            Console.WriteLine($"The total number of passwords is {passwordInfoList.Length}");
        }
    }
}
