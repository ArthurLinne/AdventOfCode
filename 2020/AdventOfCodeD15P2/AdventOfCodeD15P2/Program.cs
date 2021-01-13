using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D15P1
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 2020;

            List<int> gameSeed = "20, 0, 1, 11, 6, 3".Replace(" ", "").Split(",").ToList().ConvertAll(x => Int32.Parse(x));

            MemoryGame memoryGame = MemoryGame(gameSeed);

            int lastNumber = memoryGame(iterations);

            Console.WriteLine($"The {iterations}th number is {lastNumber}");
        }
    }
}
