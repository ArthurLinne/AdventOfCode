using System;
using System.Collections.Generic;

namespace AdventOfCode2019D4P1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> potentialPasswordList = new List<int>();

            for (int password = 158126; password < 624574 + 1; password++)
            {
                bool hasAdjacentEquals = false;
                bool increasesLeftToRight = true;
                string passwordString = password.ToString();

                List<int> passwordList = new List<int>();

                for (int digit = 0; digit < passwordString.Length; digit++)
                {
                    passwordList.Add(Int32.Parse(passwordString.Substring(digit, 1)));
                }

                for (int digit = 0; digit < passwordList.Count - 1; digit++)
                {
                    if (passwordList[digit] == passwordList[digit + 1])
                    {
                        if (digit > 0)
                        {
                            if (passwordList[digit - 1] != passwordList[digit])
                            {
                                if (digit < passwordList.Count - 2)
                                {
                                    if (passwordList[digit + 1] != passwordList[digit + 2])
                                    {
                                        hasAdjacentEquals = true;
                                    }
                                }
                                else
                                {
                                    hasAdjacentEquals = true;
                                }
                            }
                        }
                        else
                        {
                            if (digit < passwordList.Count - 2)
                            {
                                if (passwordList[digit + 1] != passwordList[digit + 2])
                                {
                                    hasAdjacentEquals = true;
                                }
                            }
                        }
                    }

                    if (passwordList[digit] > passwordList[digit + 1])
                    {
                        increasesLeftToRight = false;
                    }
                }

                if (hasAdjacentEquals && increasesLeftToRight)
                {
                    potentialPasswordList.Add(password);
                }
            }

            foreach (int potentialPassword in potentialPasswordList)
            {
                Console.WriteLine(potentialPassword);
            }

            Console.WriteLine();
            Console.WriteLine($"There are {potentialPasswordList.Count} potential passwords.");

        }
    }
}
