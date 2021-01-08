using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D20P2
{
    class ImageTile
    {
        private static readonly List<int> boundaryList = new List<int>()
        {
            12,
            12,
            -12,
            -12
        };
        private static readonly List<int> trueBoundaryList = new List<int>()
        {
            0,
            0,
            0,
            0
        };
        private static readonly Dictionary<int, (int, int)> IndexDirection = new Dictionary<int, (int, int)>()
        {
            { 0, (0, 1) },
            { 1, (1, 0) },
            { 2, (0, -1) },
            { 3, (-1, 0) }
        };
        private static Dictionary<(int, int), ImageTile> imageArray = new Dictionary<(int, int), ImageTile>();
        private static List<ImageTile> lonelyTiles = new List<ImageTile>();
        private static List<ImageTile> unplacedTiles = new List<ImageTile>();


        private readonly int cameraId;
        private readonly int arrayBinaryLength;

        private int arrayLocationX;
        private int arrayLocationY;
        private string[] rawImageData;
        private List<string> processedImageData;

        public static string[] FillPuzzle()
        {
            unplacedTiles[0].PlaceTile(0, 0);

            while (lonelyTiles.Count > 0)
            {
                ImageTile currentTile = lonelyTiles[0];
                int imageIndex = currentTile.CheckForSlot();

                if (imageIndex != -1)
                {
                    bool foundTile = false;
                    foreach (ImageTile possibleTile in unplacedTiles)
                    {
                        if (possibleTile.MatchSetTile(currentTile, imageIndex))
                        {
                            foundTile = true;
                            break;
                        }
                    }

                    if (!foundTile)
                    {
                        boundaryList[imageIndex] =
                            currentTile.arrayLocationX * Math.Abs(IndexDirection[imageIndex].Item1)
                            + currentTile.arrayLocationY * Math.Abs(IndexDirection[imageIndex].Item2);

                        //Console.WriteLine($"Set limit at {imageIndex} to {boundaryList[imageIndex]}");
                        
                        currentTile.processedImageData[imageIndex] = "*" + currentTile.processedImageData[imageIndex];
                    }

                }
            }

            List<string> fullImage = new List<string>();
            int arrayLength = imageArray[(0, 0)].arrayBinaryLength;

            for (int outerRowIndex = boundaryList[0]; outerRowIndex >= boundaryList[2]; outerRowIndex--)
            {
                for (int innerRowIndex = 1; innerRowIndex < arrayLength - 1; innerRowIndex++)
                {
                    string newLine = "";
                    for (int outerColumnIndex = boundaryList[3]; outerColumnIndex <= boundaryList[1]; outerColumnIndex++)
                    {
                        newLine += imageArray[(outerColumnIndex, outerRowIndex)].rawImageData[innerRowIndex].Substring(1, arrayLength - 2);
                    }
                    fullImage.Add(newLine);
                }
            }

            return fullImage.ToArray();
        }

        public ImageTile(int cameraId, string[] rawImageData)
        {
            this.cameraId = cameraId;
            this.rawImageData = rawImageData;
            this.arrayBinaryLength = rawImageData[0].Length;

            processedImageData = ProcessRawImageData();

            unplacedTiles.Add(this);
        }

        public List<string> ProcessRawImageData()
        {
            List<string> edges = new List<string>();

            string topSideString = rawImageData[0];
            string bottomSideString = rawImageData[arrayBinaryLength - 1];

            string rightSideString = "";
            string leftSideString = "";

            for (int i = 0; i < arrayBinaryLength; i++)
            {
                leftSideString += rawImageData[i][0].ToString();
                rightSideString += rawImageData[i][arrayBinaryLength - 1].ToString();
            }

            edges.Add(topSideString);
            edges.Add(rightSideString);
            edges.Add(bottomSideString);
            edges.Add(leftSideString);

            return edges;
        }

        public void RotateTile(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                processedImageData.Insert(0, processedImageData[3]);
                processedImageData.RemoveAt(4);

                
                char[] charList = processedImageData[0].ToCharArray();
                Array.Reverse(charList);
                processedImageData[0] = new string(charList);

                charList = processedImageData[2].ToCharArray();
                Array.Reverse(charList);
                processedImageData[2] = new string(charList);

                string[] rotatedImageData = new string[arrayBinaryLength];

                for (int columnIndex = 0; columnIndex < rawImageData.Length; columnIndex++)
                {
                    string newLine = "";
                    for (int rowIndex = arrayBinaryLength - 1; rowIndex >= 0; rowIndex--)
                    {
                        newLine += rawImageData[rowIndex][columnIndex].ToString();
                    }
                    rotatedImageData[columnIndex] = newLine;
                }

                rawImageData = rotatedImageData;
            }

            //Console.WriteLine($"Rotated tile {this.cameraId} {iterations} times.");
        }

        public void ReflectTile()
        {
            string holdingVariable = processedImageData[2];
            processedImageData[2] = processedImageData[0];
            processedImageData[0] = holdingVariable;

            char[] charList = processedImageData[1].ToCharArray();
            Array.Reverse(charList);
            processedImageData[1] = new string(charList);

            charList = processedImageData[3].ToCharArray();
            Array.Reverse(charList);
            processedImageData[3] = new string(charList);

            List<string> holdingData = rawImageData.ToList();

            holdingData.Reverse();

            rawImageData = holdingData.ToArray();

            //Console.WriteLine($"Reflected tile {this.cameraId} about the x-axis.");
        }

        public bool MatchSetTile(ImageTile setTile, int imageIndex)
        {
            for (int i = 0; i < 4; i++)
            {
                if (CheckForMatch(setTile, imageIndex))
                {
                    return true;
                }
                this.RotateTile(1);
            }

            this.ReflectTile();

            for (int i = 0; i < 4; i++)
            {
                if (CheckForMatch(setTile, imageIndex))
                {
                    return true;
                }
                this.RotateTile(1);
            }

            this.ReflectTile();

            return false;

        }

        public bool CheckForMatch(ImageTile setTile, int imageIndex)
        {
            if (setTile.processedImageData[imageIndex] == this.processedImageData[(imageIndex + 2) % 4])
            {
                //Console.WriteLine($"Match found in direction {imageIndex}");
                //Console.WriteLine($"{setTile.processedImageData[imageIndex]}");
                //Console.WriteLine("matched with");
                //Console.WriteLine($"{this.processedImageData[(imageIndex + 2) % 4]}");

                int dx = IndexDirection[imageIndex].Item1;
                int dy = IndexDirection[imageIndex].Item2;

                PlaceTile(setTile.arrayLocationX + dx, setTile.arrayLocationY + dy);
                
                //Console.WriteLine();
                //Console.WriteLine($"Placed Tile: {this.cameraId}");
                //this.PrintRawImage();

                //Console.WriteLine($"Set Tile: {setTile.cameraId}");
                //setTile.PrintRawImage();
                //Console.WriteLine();

                return true;
            }

            if (this.cameraId == 3079)
            {
                Console.WriteLine(this.processedImageData[(imageIndex + 2) % 4]);
                Console.WriteLine("Does Not Line up with");
                Console.WriteLine(setTile.processedImageData[imageIndex]);
            }


            return false;
        }

        public void PlaceTile(int gridX, int gridY)
        {
            this.arrayLocationX = gridX;
            this.arrayLocationY = gridY;

            imageArray.Add((this.arrayLocationX, this.arrayLocationY), this);
            lonelyTiles.Add(this);
            unplacedTiles.Remove(this);
            
            if (gridY > trueBoundaryList[0])
            {
                trueBoundaryList[0] = gridY;
            }

            if (gridX > trueBoundaryList[1])
            {
                trueBoundaryList[1] = gridX;
            }

            if (gridY < trueBoundaryList[2])
            {
                trueBoundaryList[2] = gridY;
            }

            if (gridX < trueBoundaryList[3])
            {
                trueBoundaryList[3] = gridX;
            }

            //Console.WriteLine();
            //Console.WriteLine($"{this.cameraId} placed at {this.arrayLocationX}, {this.arrayLocationY}");
            //Console.WriteLine();

            PrintPartialImage();
        }

        public int CheckForSlot()
        {
            for (int direction = 0; direction < 4; direction++)
            {
                //Console.WriteLine($"Looking for an open slot from {this.arrayLocationX}, {this.arrayLocationY}");
                if (imageArray.TryGetValue(
                    (
                    this.arrayLocationX + IndexDirection[direction].Item1,
                    this.arrayLocationY + IndexDirection[direction].Item2
                    ),
                    out ImageTile adjacentTile
                    )
                    )
                {
                    //Console.WriteLine($"No slot, as there is a tile at location {this.arrayLocationX + IndexDirection[direction].Item1}, {this.arrayLocationY + IndexDirection[direction].Item2}");
                    continue;
                }
                else if (this.processedImageData[direction][0] == '*')
                {
                    //Console.WriteLine("No slot, as this is the end of the line.");
                    continue;
                }
                else
                {
                    return direction;
                }
            }

            lonelyTiles.Remove(this);
            return -1;
        }

        public void PrintRawImage()
        {
            Console.WriteLine();
            Console.WriteLine($"Tile {cameraId}:");

            foreach (string imageLine in rawImageData)
            {
                Console.WriteLine(imageLine);
            }

            Console.WriteLine();
        }

        public static void PrintCameraCoordinates()
        {
            for (int outerRowIndex = boundaryList[0]; outerRowIndex >= boundaryList[2]; outerRowIndex--)
            {
                for (int outerColumnIndex = boundaryList[1]; outerColumnIndex >= boundaryList[3]; outerColumnIndex--)
                {
                    Console.WriteLine($"{imageArray[(outerColumnIndex, outerRowIndex)].cameraId}");
                }                
            }
        }

        public static void PrintPartialImage()
        {
            List<string> partialImage = new List<string>();
            int arrayLength = imageArray[(0, 0)].arrayBinaryLength;
            int rowLength = trueBoundaryList[1] - trueBoundaryList[3] + 1;
            int columnLength = trueBoundaryList[0] - trueBoundaryList[2] + 1;

            for (int outerRowIndex = trueBoundaryList[0]; outerRowIndex >= trueBoundaryList[2]; outerRowIndex--)
            {
                for (int innerRowIndex = 0; innerRowIndex < arrayLength; innerRowIndex++)
                {
                    string newLine = "";
                    for (int outerColumnIndex = trueBoundaryList[3]; outerColumnIndex <= trueBoundaryList[1]; outerColumnIndex++)
                    {
                        if (imageArray.TryGetValue((outerColumnIndex, outerRowIndex), out ImageTile tile))
                        {
                            newLine += tile.rawImageData[innerRowIndex].Substring(0, arrayLength);
                        }
                        else
                        {
                            newLine += "          ";
                        }
                    }
                    partialImage.Add(newLine);
                }
            }

            Console.WriteLine("Partial Image:");
            for (int rowIndex = 0; rowIndex < partialImage.Count; rowIndex++)
            {
                for (int column = rowLength - 1; column > 0; column--)
                {
                    partialImage[rowIndex] = partialImage[rowIndex].Insert(arrayLength * column, " ");
                }
                Console.WriteLine(partialImage[rowIndex]);
                if ((rowIndex + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();

        }

        public void PrintEdges()
        {
            Console.WriteLine();
            Console.WriteLine($"List of edges for {this.cameraId}");
            foreach (string edge in processedImageData)
            {
                Console.WriteLine(edge);
            }
        }
    }
}
