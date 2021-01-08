using System;

namespace AdventOfCodeD3P1
{
    class Program
    {
        static int CheckForTrees(string[] treeFile, int rise, int run)
        {
            int rowLength = treeFile[0].Length;

            int rowIndex = 0;
            int columnIndex = 0;

            int treeCount = 0;

            rowIndex += rise;
            columnIndex += run;



            while (rowIndex < treeFile.Length)
            {
                if (treeFile[rowIndex].Substring(columnIndex, 1) == "#")
                {
                    treeCount++;
                }

                rowIndex += 1;
                columnIndex = (columnIndex + run) % rowLength;
            }

            return treeCount;
        }

        static void Main()
        {
            string[] treeFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D3P1\TreeLocations.txt");

            int treeCount = CheckForTrees(treeFile, 1, 3);

            Console.WriteLine($"You're gonna hit {treeCount} trees!");

        }
    }
}
