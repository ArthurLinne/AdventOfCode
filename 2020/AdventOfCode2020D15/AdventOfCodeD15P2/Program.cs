using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D15P2
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstIteration = 2020; 
            int secondIteration = 30000000;

            List<int> gameSeed = "20, 0, 1, 11, 6, 3".Replace(" ", "").Split(",").ToList().ConvertAll(x => Int32.Parse(x));

            MemoryGame memoryGame = new MemoryGame(gameSeed);

            int lastNumber = memoryGame.PlayToTurn(firstIteration);

            Console.WriteLine($"The {firstIteration}th number is {lastNumber}");

            lastNumber = memoryGame.PlayToTurn(secondIteration);

            Console.WriteLine($"The {secondIteration}th number is {lastNumber}");
        }
    }
}
