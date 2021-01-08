using System;

namespace AdventOfCode2020D24P1
{
    class Program
    {
        static void Main()
        {
            string[] instructionFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D24P1\InstructionFile.txt");
            HexGrid hexGrid = new HexGrid(instructionFile);

            hexGrid.PrintFlippedByCycle();
            hexGrid.PrintFullGrid();

            for (int i = 1; i <= 100; i++)
            {
                hexGrid.CompleteCycle();
                hexGrid.PrintFlippedByCycle();
                //hexGrid.PrintFullGrid();
            }
        }
    }
}
