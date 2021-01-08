using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D20P1
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string[] puzzlePiecesInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D20P1\PuzzlePieces.txt");

            List<string[]> tileList = new List<string[]>();
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

            ImageTile.FillPuzzle();
        }
    }
}
