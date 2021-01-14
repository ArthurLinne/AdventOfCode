using System;
using System.Collections.Generic;

namespace AdventOfCode2020D5P2
{
    class Program
    {
        public static (int, int) EvaluateSeatInstructions(
            string seatInstructions,
            int rowInstructionCount,
            int columnInstructionCount)
        {
            (int, int) rowColumn;

            string rowInstructions = seatInstructions.Substring(0, rowInstructionCount);
            string columnInstructions = seatInstructions.Substring(rowInstructionCount, columnInstructionCount);

            int minRow = 0;
            int maxRow = (int)(Math.Pow(2, rowInstructionCount) - 1);

            int minColumn = 0;
            int maxColumn = (int)(Math.Pow(2, columnInstructionCount) - 1);

            foreach (char instruct in rowInstructions)
            {
                switch (instruct)
                {
                    case 'F':
                        maxRow -= (maxRow - minRow + 1) / 2;
                        break;
                    case 'B':
                        minRow += (maxRow - minRow + 1) / 2;
                        break;
                }
            }

            foreach (char instruct in columnInstructions)
            {
                switch (instruct)
                {
                    case 'L':
                        maxColumn -= (maxColumn - minColumn + 1) / 2;
                        break;
                    case 'R':
                        minColumn += (maxColumn - minColumn + 1) / 2;
                        break;
                }
            }

            rowColumn = (minRow, minColumn);

            return rowColumn;
        }

        static void Main(string[] args)
        {
            string[] seatList = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D5P1\SeatFile.txt");

            List<int> seatIdList = new List<int>();

            foreach (string seat in seatList)
            {
                (int, int) rowColumn = EvaluateSeatInstructions(seat, 7, 3);
                int row = rowColumn.Item1;
                int column = rowColumn.Item2;

                int seatId = row * 8 + column;

                seatIdList.Add(seatId);
            }

            for (int seatId = 0; seatId < 996; seatId++)
            {
                if (!seatIdList.Contains(seatId))
                {
                    if (seatIdList.Contains(seatId - 1) && seatIdList.Contains(seatId + 1))
                    {
                        Console.WriteLine($"Your seat ID is {seatId}.");
                    }
                }
            }
        }
    }
}
