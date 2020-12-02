using System;
using System.Collections.Generic;

namespace AdventOfCode2020D1P1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D1P1\ExpenseReport.txt");

            List<int> expenseList = new List<int>();

            foreach (string entry in lines)
            {
                expenseList.Add(Int32.Parse(entry));
            }

            for (int i = 0; i < expenseList.Count; i++)
            {
                for (int j = 0; j < expenseList.Count; j++)
                {
                    if (i != j && expenseList[i] + expenseList[j] == 2020)
                    {
                        Console.WriteLine($"The two entries are {expenseList[i]} and {expenseList[j]}.");
                        Console.WriteLine($"Their product is {expenseList[i] * expenseList[j]}.");
                    }
                }
            }

        }
    }
}
