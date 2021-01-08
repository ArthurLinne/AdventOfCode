using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D11P1
{
    class Program
    {
        public static List<int> adjList = new List<int> { -1, 0, 1 };

        public static int CheckNeighbors(List<string> currentSeatList, char characterToCheck, int rowIndex, int columnIndex)
        {
            int neighbors = 0;

            int rowLength = currentSeatList.Count;
            int columnLength = currentSeatList[0].Length;

            foreach (int rowMod in adjList)
            {
                foreach (int columnMod in adjList)
                {
                    if (0 <= rowIndex + rowMod
                        && rowIndex + rowMod <= rowLength - 1
                        && 0 <= columnIndex + columnMod
                        && columnIndex + columnMod <= columnLength - 1
                        && (rowMod != 0 || columnMod != 0))
                    {
                        if (currentSeatList[rowIndex + rowMod][columnIndex + columnMod] == characterToCheck)
                        {
                            neighbors++;
                        }
                    }
                }
            }

            return neighbors;
        }

        static List<string> ApplySeatRules(List<string> currentSeatList)
        {
            List<string> newSeatList = new List<string>();

            int rowLength = currentSeatList.Count;
            int columnLength = currentSeatList[0].Length;

            for (int rowIndex = 0; rowIndex < rowLength; rowIndex++)
            {
                string newRow = "";

                for (int columnIndex = 0; columnIndex < columnLength; columnIndex++)
                {
                    switch (currentSeatList[rowIndex][columnIndex])
                    {
                        case '.':
                            newRow = newRow.Insert(newRow.Length, ".");
                            break;
                        case 'L':
                            int occupiedNeighbors = CheckNeighbors(currentSeatList, '#', rowIndex, columnIndex);

                            if (occupiedNeighbors > 0)
                            {
                                newRow = newRow.Insert(newRow.Length, "L");
                            }
                            else
                            {
                                newRow = newRow.Insert(newRow.Length, "#");
                            }

                            break;

                        case '#':
                            int unoccupiedNeighbors = CheckNeighbors(currentSeatList, '#', rowIndex, columnIndex);

                            if (unoccupiedNeighbors >= 4)
                            {
                                newRow = newRow.Insert(newRow.Length, "L");
                            }
                            else
                            {
                                newRow = newRow.Insert(newRow.Length, "#");
                            }

                            break;
                    }
                }

                newSeatList.Add(newRow);
            }

            return newSeatList;
        }

        static void Main(string[] args)
        {
            List<string> newSeatList = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D11P1\SeatingArrangement.txt").ToList();

            List<string> currentSeatList = newSeatList;

            bool listsDiffer = true;

            while (listsDiffer)
            {
                currentSeatList = newSeatList;
                
                newSeatList = ApplySeatRules(currentSeatList);

                listsDiffer = false;

                for (int rowIndex = 0; rowIndex < currentSeatList.Count; rowIndex++)
                {
                    if (currentSeatList[rowIndex] != newSeatList[rowIndex])
                    {
                        listsDiffer = true;
                        break;
                    }
                }
            }

            int totalOccupiedSeats = 0;

            foreach (string row in currentSeatList)
            {
                totalOccupiedSeats += row.Split('#').Length - 1;
            }

            Console.WriteLine($"The total number of occupied seats is {totalOccupiedSeats}.");
        }
    }
}
