using System;

namespace AdventOfCode2020D2P1
{
    class Program
    {
        static void Main()
        {
            string[] passwordInfoList = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D2P1\PasswordList.txt");

            int validPasswordCount = 0;
            int invalidPasswordCount = 0;

            foreach (string passwordInfo in passwordInfoList)
            {
                string ruleCharacter;
                int firstIndex;
                int secondIndex;
                string potentialPassword;

                firstIndex = Int32.Parse(passwordInfo.Substring(0, passwordInfo.IndexOf("-")));
                firstIndex--;

                secondIndex = Int32.Parse(passwordInfo.Substring(passwordInfo.IndexOf("-") + 1, passwordInfo.IndexOf(" ") - passwordInfo.IndexOf("-") - 1));
                secondIndex--;

                ruleCharacter = passwordInfo.Substring(passwordInfo.IndexOf(":") - 1, 1);

                potentialPassword = passwordInfo.Substring(passwordInfo.IndexOf(":") + 2);

                Console.WriteLine(passwordInfo);
                Console.WriteLine($"Potential Password: {potentialPassword}");
                Console.WriteLine($"Rule Character: {ruleCharacter}");


                int ruleOccurence = 0;

                if (firstIndex < potentialPassword.Length)
                {
                    Console.WriteLine($"First Check: {potentialPassword.Substring(firstIndex, 1)}");
                    if (potentialPassword.Substring(firstIndex, 1) == ruleCharacter)
                    {
                        ruleOccurence++;
                    }
                }

                if (secondIndex < potentialPassword.Length)
                {
                    Console.WriteLine($"Second Check: {potentialPassword.Substring(secondIndex, 1)}");
                    if (potentialPassword.Substring(secondIndex, 1) == ruleCharacter)
                    {
                        ruleOccurence++;
                    }
                }

                Console.WriteLine($"Rule Occurence: {ruleOccurence}");

                if (ruleOccurence == 1)
                {
                    validPasswordCount++;
                }
                else
                {
                    invalidPasswordCount++;
                }

                Console.WriteLine();
                if (firstIndex >= potentialPassword.Length)
                {
                    Console.WriteLine(passwordInfo);
                }
            }

            Console.WriteLine($"The total number of valid passwords is {validPasswordCount}");
            Console.WriteLine($"The total number of invalid passwords is {invalidPasswordCount}");
            Console.WriteLine($"The total number of passwords is {passwordInfoList.Length}");
        }
    }
}


