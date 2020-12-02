using System;
using System.Collections.Generic;

namespace AdventOfCode2020D1P2
{
    class Program
    {
        public static void SearchForSum(List<int> expenseList)
        {
            for (int i = 0; i < expenseList.Count; i++)
            {
                for (int j = 0; j < expenseList.Count; j++)
                {
                    for (int k = 0; k < expenseList.Count; k++)
                    {
                        if (i != j && j != k && expenseList[i] + expenseList[j] + expenseList[k] == 2020)
                        {
                            Console.WriteLine($"The three entries are {expenseList[i]}, {expenseList[j]}, and {expenseList[k]}.");
                            Console.WriteLine($"Their product is {expenseList[i] * expenseList[j] * expenseList[k]}.");
                            return;
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D1P1\ExpenseReport.txt");

            List<int> expenseList = new List<int>();

            foreach (string entry in lines)
            {
                expenseList.Add(Int32.Parse(entry));
            }

            SearchForSum(expenseList);

        }
    }
}
