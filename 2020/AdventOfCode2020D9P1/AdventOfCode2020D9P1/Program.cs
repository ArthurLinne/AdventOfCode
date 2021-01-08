using System;
using System.Collections.Generic;

namespace AdventOfCode2020D9P1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PREAMBLE_SIZE = 25;

            string[] xmasCypherFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D9P1\XmasCypher.txt");

            List<long> xmasCypherList = new List<long>();

            foreach (string line in xmasCypherFile)
            {
                xmasCypherList.Add(Int64.Parse(line));
            }

            for (int currentIndex = PREAMBLE_SIZE; currentIndex < xmasCypherList.Count; currentIndex++ )
            {
                long currentValue = xmasCypherList[currentIndex];

                List<long> prev25 = xmasCypherList.GetRange(currentIndex - PREAMBLE_SIZE, PREAMBLE_SIZE);

                bool satisfiesCondition = false;

                foreach (long firstNumber in prev25)
                {
                    foreach (long secondNumber in prev25)
                    {
                        if (firstNumber != secondNumber && firstNumber + secondNumber == currentValue)
                        {
                            satisfiesCondition = true;
                            break;
                        }
                    }
                    if (satisfiesCondition)
                    {
                        break;
                    }
                }

                if (!satisfiesCondition)
                {
                    Console.WriteLine($"The first number to fail this condition is {currentValue}");
                    break;
                }
            }
        }
    }
}
