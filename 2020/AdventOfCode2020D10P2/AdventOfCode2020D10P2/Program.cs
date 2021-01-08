using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D10P2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] joltageFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D10P1\Joltage.txt");

            List<int> joltageList = new List<int>();

            foreach (string joltage in joltageFile)
            {
                joltageList.Add(Int32.Parse(joltage));
            }

            joltageList.Add(0);
            joltageList.Add(joltageList.Max() + 3);

            joltageList.Sort();

            List<int> joltageDiffList = new List<int>();

            for (int index = 1; index < joltageList.Count; index++)
            {
                joltageDiffList.Add(joltageList[index] - joltageList[index - 1]);
            }

            List<int> repeatedOnesList = new List<int>();

            int totalRepeatedOnes = 0;

            foreach (int diff in joltageDiffList)
            {
                if (diff == 1)
                {
                    totalRepeatedOnes++;
                }
                else if (diff == 3 && totalRepeatedOnes > 0)
                {
                    repeatedOnesList.Add(totalRepeatedOnes);
                    totalRepeatedOnes = 0;
                }
            }

            Dictionary<int, int> repeatedOneDict = new Dictionary<int, int>();

            repeatedOneDict.Add(1, 1);
            repeatedOneDict.Add(2, 2);
            repeatedOneDict.Add(3, 4);
            repeatedOneDict.Add(4, 7);

            long totalCombinations = 1;

            foreach (int repeatedOnes in repeatedOnesList)
            {
                totalCombinations *= repeatedOneDict[repeatedOnes];
            }

            Console.WriteLine($"The total number of combinations is {totalCombinations}");
        }
    }
}
