using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D15P2
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 30000000;

            List<int> gameSeed = "20, 0, 1, 11, 6, 3".Replace(" ", "").Split(",").ToList().ConvertAll(x => Int32.Parse(x));

            MemoryGame memoryGame = new MemoryGame(gameSeed);

            int lastNumber = memoryGame.PlayToTurn(iterations);

            Console.WriteLine($"The {iterations}th number is {lastNumber}");
        }
    }
}
