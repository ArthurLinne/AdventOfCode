using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D15P1
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 30000000;

            List<int> memoryGameList = new List<int>();

            List<int> memoryGameListPrev = new List<int>();

            memoryGameList.AddRange("20, 0, 1, 11, 6, 3".Replace(" ", "").Split(",").ToList().ConvertAll(x => Int32.Parse(x)));

            memoryGameListPrev = memoryGameList.GetRange(0, memoryGameList.Count - 1);

            while (memoryGameList.Count < iterations)
            {
                int currentInt = memoryGameList.Last();

                if (memoryGameListPrev.Contains(currentInt))
                {
                    memoryGameList.Add(memoryGameList.LastIndexOf(currentInt) - memoryGameListPrev.LastIndexOf(currentInt));
                    memoryGameListPrev.Add(currentInt);
                }
                else
                {
                    memoryGameList.Add(0);
                    memoryGameListPrev.Add(currentInt);
                }
            }

            Console.WriteLine($"The {iterations}th number is {memoryGameList.Last()}");
        }
    }
}
