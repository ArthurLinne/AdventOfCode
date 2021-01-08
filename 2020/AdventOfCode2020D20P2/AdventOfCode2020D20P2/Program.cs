using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D20P2
{
    class Program
    {
        private static string[] fullImage;

        public static void PrintImage()
        {
            for (int rowIndex = 0; rowIndex < fullImage.Length; rowIndex++)
            {
                Console.WriteLine(fullImage[rowIndex].Insert(16, " ").Insert(8, " "));
                if ((rowIndex + 1) % 8 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        public static void RotateImage()
        {
            string[] rotatedImageData = new string[fullImage.Length];

            for (int columnIndex = 0; columnIndex < fullImage.Length; columnIndex++)
            {
                string newLine = "";
                for (int rowIndex = fullImage.Length - 1; rowIndex >= 0; rowIndex--)
                {
                    newLine += fullImage[rowIndex][columnIndex].ToString();
                }
                rotatedImageData[columnIndex] = newLine;
            }

            Console.WriteLine("Rotated Clockwise");

            fullImage = rotatedImageData;
        }

        public static void ReflectImage()
        {
            List<string> holdingData = fullImage.ToList();

            holdingData.Reverse();

            Console.WriteLine("Reflected along the x-axis");

            fullImage = holdingData.ToArray();
        }

        public static List<(int, int)> SearchWithOrientation(string[] keyImage)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    List<(int, int)> monsterKeys = SearchForImage(keyImage);

                    PrintImage();

                    if (monsterKeys.Count > 0)
                    {
                        return monsterKeys;
                    }
                    RotateImage();
                }

                ReflectImage();
            }

            return null;
        }

        public static List<(int, int)> SearchForImage(string[] keyImage)
        {
            List<(int, int)> monsterKeys = new List<(int, int)>();

            for (int startRowIndex = 0; startRowIndex < fullImage.Length - keyImage.Length + 1; startRowIndex++)
            {
                for (int startColumnIndex = 0; startColumnIndex < fullImage[0].Length - keyImage[0].Length + 1; startColumnIndex++)
                {
                    bool mismatch = false;

                    for (int innerRowIndex = 0; innerRowIndex < keyImage.Length; innerRowIndex++)
                    {
                        for (int innerColumnIndex = 0; innerColumnIndex < keyImage[0].Length; innerColumnIndex++)
                        {
                            if (
                                keyImage[innerRowIndex][innerColumnIndex] == ' '
                                || keyImage[innerRowIndex][innerColumnIndex] 
                                == fullImage[startRowIndex + innerRowIndex][startColumnIndex + innerColumnIndex]
                                )
                            {
                                continue;
                            }
                            else
                            {
                                mismatch = true;
                            }
                        }
                        if (mismatch)
                        {
                            break;
                        }
                    }
                    if (!mismatch)
                    {
                        monsterKeys.Add((startRowIndex, startColumnIndex));
                    }
                }
            }

            return monsterKeys;
        }

        static void Main()
        {
            string[] puzzlePiecesInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D20P1\PuzzlePieces.txt");
            string[] seaMonster = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D20P1\SeaMonster.txt");

            bool addNewTile = false;
            int tileRowCount = 0;
            string[] newTile = new string[10];
            int currentTileId = 0;


            foreach (string line in puzzlePiecesInput)
            {
                if (line == "")
                {
                    continue;
                }
                else if (addNewTile)
                {
                    newTile[tileRowCount] = line;
                    tileRowCount++;
                    if (tileRowCount == 10)
                    {
                        ImageTile imageTile = new ImageTile(currentTileId, newTile);
                        addNewTile = false;
                    }
                }
                else if (line.Substring(0, 4) == "Tile")
                {
                    currentTileId = Int32.Parse(line.Substring(line.IndexOf(" ") + 1, line.IndexOf(":") - line.IndexOf(" ") - 1));
                    addNewTile = true;
                    tileRowCount = 0;
                    newTile = new string[10];
                    continue;
                }
            }

            fullImage = ImageTile.FillPuzzle();

            List<(int, int)> monsterKeys = SearchWithOrientation(seaMonster);

            int totalHash = 0;
            int seaMonsterSize = 0;

            foreach (string line in fullImage)
            {
                Console.WriteLine(line);

                totalHash += line.Length - line.Replace("#", "").Length;
            }

            foreach (string line in seaMonster)
            {
                seaMonsterSize += line.Length - line.Replace("#", "").Length;
            }


            Console.WriteLine($"There are {monsterKeys.Count} sea monsters.");

            foreach ((int, int) key in monsterKeys)
            {
                Console.WriteLine($"There is a monster at ({key.Item1}, {key.Item2})");
            }

            Console.WriteLine($"The water roughness is {totalHash - seaMonsterSize * monsterKeys.Count}.");
        }
    }
}
