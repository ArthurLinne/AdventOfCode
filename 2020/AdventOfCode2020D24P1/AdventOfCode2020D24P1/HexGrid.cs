using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D24P1
{
    class HexGrid
    {
        private Dictionary<(int, int, int), int> grid;

        private int xMin = 0;
        private int yMin = 0;
        private int zMin = 0;
        private int xMax = 0;
        private int yMax = 0;
        private int zMax = 0;

        private int cyclesCompleted;


        public HexGrid(string[] simpleInstructionList)
        {
            grid = new Dictionary<(int, int, int), int>();

            foreach (List<string> instruction in ConvertInstructionList(simpleInstructionList))
            {
                ReadInstruction(instruction);
            }
        }

        private List<List<string>> ConvertInstructionList(string[] instructionList)
        {
            List<List<string>> finalInstructionList = new List<List<string>>();

            foreach (string instructionSet in instructionList)
            {
                List<string> finalInstruction = 
                    instructionSet.Replace("e", "e,").Replace("w", "w,").Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                finalInstructionList.Add(finalInstruction);
            }

            return finalInstructionList;
        }

        private void ReadInstruction(List<string> instruction)
        {
            int x = 0;
            int y = 0;
            int z = 0;

            foreach (string move in instruction)
            {
                switch (move)
                {
                    case "e":
                        x++;
                        y--;
                        break;

                    case "ne":
                        x++;
                        z--;
                        break;

                    case "se":
                        z++;
                        y--;
                        break;

                    case "w":
                        y++;
                        x--;
                        break;

                    case "nw":
                        y++;
                        z--;
                        break;

                    case "sw":
                        z++;
                        x--;
                        break;
                    default:
                        Console.WriteLine("Unknown instruction move:");
                        Console.WriteLine($"Unknown Move: {move}.");
                        break;
                }
            }

            FlipTile(x, y, z);
        }

        private void FlipTile(int x, int y, int z)
        {
            if (grid.TryGetValue((x, y, z), out int status))
            {
                grid.Remove((x, y, z));
            }
            else
            {
                grid[(x, y, z)] = 1;
                ModifyBoundaries(x, y, z);
            }
        }

        private void ModifyBoundaries(int xNew, int yNew, int zNew)
        {
            if (xNew < xMin)
            {
                xMin = xNew;
            }
            if (yNew < yMin)
            {
                yMin = yNew;
            }
            if (zNew < zMin)
            {
                zMin = zNew;
            }
            if (xNew > xMax)
            {
                xMax = xNew;
            }
            if (yNew > yMax)
            {
                yMax = yNew;
            }
            if (zNew > zMax)
            {
                zMax = zNew;
            }
        }

        public int CountNeighbors(int x, int y, int z)
        {
            int neighbors = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (dx + dy + dz == 0 && !(dx == 0 && dy == 0 && dz == 0))
                        {
                            if (grid.TryGetValue((x + dx, y + dy, z + dz), out int flippedStatus))
                            {
                                neighbors++;
                            }
                        }
                    }
                }
            }

            return neighbors;
        }

        public void CompleteCycle()
        {
            Dictionary<(int, int, int), int> newGrid = new Dictionary<(int, int, int), int>(grid);

            for (int x = xMin - 1; x <= xMax + 1; x++)
            {
                for (int y = yMin - 1; y <= yMax + 1; y++)
                {
                    for (int z = zMin - 1; z <= zMax + 1; z++)
                    {
                        if (x + y + z == 0)
                        {
                            int neighbors = CountNeighbors(x, y, z);

                            if (grid.TryGetValue((x, y, z), out int flippedStatus))
                            {
                                if (neighbors == 0 || neighbors > 2)
                                {
                                    newGrid.Remove((x, y, z));
                                }
                            }
                            else
                            {
                                if (neighbors == 2)
                                {
                                    newGrid[(x, y, z)] = 1;
                                    ModifyBoundaries(x, y, z);
                                }
                            }
                        }
                        
                    }
                }
            }

            grid = newGrid;
            cyclesCompleted++;
        }

        public void PrintTotalFlipped()
        {
            int totalFlipped = grid.Values.Sum();

            Console.WriteLine($"There are {totalFlipped} black tiles.");
        }

        public void PrintFlippedByCycle()
        {
            Console.WriteLine($"Day {cyclesCompleted}: {grid.Values.Sum()}");
        }

        public void PrintFullGrid()
        {
            Console.WriteLine($"Boundaries: x: ({xMin}, {xMax}) y: ({yMin}, {yMax}) z: ({zMin}, {zMax}) ");

            for (int z = zMin; z <= zMax; z++)
            {
                for (int i = 0; i < 2 * (z - zMin); i++)
                {
                    Console.Write(" ");
                }
                for (int x = xMin; x <= xMax; x++)
                {
                    int y = 0 - z - x;

                    if (grid.TryGetValue((x, y, z), out int flippedStatus))
                    {
                        Console.Write("(**)");
                    }
                    else
                    {
                        Console.Write("(oo)");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
