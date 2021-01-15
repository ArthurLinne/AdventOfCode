using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D17P1
{
    class Program
    {
        public static void PrintPocketDimension(Dictionary<(int, int, int), int> pocketDimension, (int, int, int, int, int, int) boundaries)
        {
            int xMax = boundaries.Item1;
            int xMin = boundaries.Item2;
            int yMax = boundaries.Item3;
            int yMin = boundaries.Item4;
            int zMax = boundaries.Item5;
            int zMin = boundaries.Item6;

            for (int z = zMin; z <= zMax; z++)
            {
                Console.WriteLine($"z = {z}");

                for (int y = yMin; y <= yMax; y++)
                {
                    string outputLine = "";
                    for (int x = xMin; x <= xMax; x++)
                    {
                        if (pocketDimension.TryGetValue((x, y, z), out int returnValue))
                        {
                            outputLine += "#";
                        }
                        else
                        {
                            outputLine += ".";
                        }
                    }
                    Console.WriteLine(outputLine);
                }

                Console.WriteLine();
            }

        }

        public static int CheckForActiveNeighbors(Dictionary<(int, int, int), int> pocketDimension, (int, int, int) cube)
        {
            int totalActiveNeighbors = 0;
            int x = cube.Item1;
            int y = cube.Item2;
            int z = cube.Item3;

            for (int dx = -1; dx <= 1; dx++)
            {
                for(int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (!(dx == 0 && dy == 0 && dz == 0))
                        {
                            if (pocketDimension.TryGetValue((x + dx, y + dy, z + dz), out int returnValue))
                            {
                                totalActiveNeighbors += returnValue;
                            }
                        }
                    }
                }
            }

            return totalActiveNeighbors;
        }

        public static Dictionary<(int, int, int), int> CompleteCycle(
            Dictionary<(int, int, int), int> pocketDimension, 
            (int, int, int ,int, int, int) boundaries)
        {
            int xMax = boundaries.Item1;
            int xMin = boundaries.Item2;
            int yMax = boundaries.Item3;
            int yMin = boundaries.Item4;
            int zMax = boundaries.Item5;
            int zMin = boundaries.Item6;

            Dictionary<(int, int, int), int> newPocketDimension = new Dictionary<(int, int, int), int>();

            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    for (int z = zMin; z <= zMax; z++)
                    {
                        if (pocketDimension.TryGetValue((x, y, z), out int returnValue))
                        {
                            int activeNeighbors = CheckForActiveNeighbors(pocketDimension, (x, y, z));
                            if (activeNeighbors == 2 || activeNeighbors == 3)
                            {
                                newPocketDimension[(x, y, z)] = 1;
                            }
                        }
                        else
                        {
                            int activeNeighbors = CheckForActiveNeighbors(pocketDimension, (x, y, z));
                            if (activeNeighbors == 3)
                            {
                                newPocketDimension[(x, y, z)] = 1;
                            }
                        }
                    }                    
                }
            }
            return newPocketDimension;
        }

        public static (int, int, int, int, int, int) DetermineBoundaries(Dictionary<(int, int, int), int> pocketDimension)
        {
            int xMax = 0;
            int xMin = 0;
            int yMax = 0;
            int yMin = 0;
            int zMax = 0;
            int zMin = 0;

            foreach (KeyValuePair<(int, int, int), int> cube in pocketDimension)
            {
                if (cube.Value == 1)
                {
                    if (cube.Key.Item1 > xMax)
                    {
                        xMax = cube.Key.Item1;
                    }
                    if (cube.Key.Item1 < xMin)
                    {
                        xMin = cube.Key.Item1;
                    }
                    if (cube.Key.Item2 > yMax)
                    {
                        yMax = cube.Key.Item2;
                    }
                    if (cube.Key.Item2 < yMin)
                    {
                        yMin = cube.Key.Item2;
                    }
                    if (cube.Key.Item3 > zMax)
                    {
                        zMax = cube.Key.Item3;
                    }
                    if (cube.Key.Item3 < zMin)
                    {
                        zMin = cube.Key.Item3;
                    }
                }
            }

            return (xMax + 1, xMin - 1, yMax + 1, yMin - 1, zMax + 1, zMin - 1);
        }

        static void Main()
        {
            string[] pocketDimensionSeed = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D17P1\PocketDimensionSeed.txt");

            Dictionary<(int, int, int), int> pocketDimension = new Dictionary<(int, int, int), int>();

            for (int x = 0; x < pocketDimensionSeed.Length; x++)
            {
                for (int y = 0; y < pocketDimensionSeed[0].Length; y++)
                {
                    if (pocketDimensionSeed[y][x] == '#')
                    {
                        pocketDimension.Add((x, y, 0), 1);
                    }
                }
            }

            for (int cycleIndex = 0; cycleIndex < 6; cycleIndex++)
            {
                pocketDimension = CompleteCycle(pocketDimension, DetermineBoundaries(pocketDimension));
            }

            int totalActive = pocketDimension.Values.Sum();

            Console.WriteLine($"The total number of active cubes is {totalActive}");
        }
    }
}
