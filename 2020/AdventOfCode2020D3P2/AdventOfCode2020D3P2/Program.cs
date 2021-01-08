using System;
using System.Collections.Generic;

namespace AdventOfCodeD3P2
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

                rowIndex += rise;
                columnIndex = (columnIndex + run) % rowLength;
            }

            return treeCount;
        }

        static void Main()
        {
            string[] treeFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D3P1\TreeLocations.txt");
            string[] treeSample = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D3P1\SampleTrees.txt");

            List<List<int>> slopeList = new List<List<int>>();

            slopeList.Add(new List<int>() { 1, 1 });
            slopeList.Add(new List<int>() { 1, 3 });
            slopeList.Add(new List<int>() { 1, 5 });
            slopeList.Add(new List<int>() { 1, 7 });
            slopeList.Add(new List<int>() { 2, 1 });

            long treeProduct = 1;

            foreach (List<int> slope in slopeList)
            {
                int treesHit = CheckForTrees(treeFile, slope[0], slope[1]);
                
                Console.WriteLine($"With rise {slope[0]} and run {slope[1]}, you'll hit {treesHit} trees.");

                treeProduct *= treesHit;
            }

            Console.WriteLine();
            Console.WriteLine($"The product of all tree smashings is {treeProduct}.");

        }
    }
}
