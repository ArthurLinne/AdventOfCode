using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D10P1
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

            

            int joltageDiffOne = 0;
            int joltageDiffThree = 0;

            for (int index = 1; index < joltageList.Count; index++)
            {
                switch (joltageList[index] - joltageList[index - 1])
                {
                    case 1:
                        joltageDiffOne++;
                        break;
                    case 3:
                        joltageDiffThree++;
                        break;
                }
            }

            Console.WriteLine($"There are {joltageDiffOne} adapters with a difference of one, and {joltageDiffThree} adapters with a difference of three.");

            Console.WriteLine($"Their product is {joltageDiffOne * joltageDiffThree}");
        }
    }
}
