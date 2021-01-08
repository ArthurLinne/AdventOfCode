using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D20P1
{
    class ImageTile
    {
        private static List<int> boundaryList = new List<int>()
        {
            12, 
            12, 
            -12, 
            -12
        };
        private static Dictionary<int, (int, int)> IndexDirection = new Dictionary<int, (int, int)>()
        {
            { 0, (0, 1) },
            { 1, (1, 0) },
            { 2, (0, -1) },
            { 3, (-1, 0) }
        };
        private static Dictionary<(int, int), ImageTile> imageArray = new Dictionary<(int, int), ImageTile>();
        private static List<ImageTile> lonelyTiles = new List<ImageTile>();
        public static List<ImageTile> unplacedTiles = new List<ImageTile>();
        


        private readonly int cameraId;
        private readonly string[] rawImageData;
        private readonly int arrayBinaryLength;

        private int arrayLocationX;
        private int arrayLocationY;
        private List<int> processedImageData;

        public static void FillPuzzle()
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

                        currentTile.processedImageData[imageIndex] *= -1;
                    }

                }
            }

            int top = boundaryList[0];
            int right = boundaryList[1];
            int bottom = boundaryList[2];
            int left = boundaryList[3];

            int corner1 = imageArray[(right, top)].cameraId;
            int corner2 = imageArray[(left, top)].cameraId;
            int corner3 = imageArray[(right, bottom)].cameraId;
            int corner4 = imageArray[(left, bottom)].cameraId;

            long cornerProduct = (long)corner1 * (long)corner2 * (long)corner3 * (long)corner4;

            Console.WriteLine($"The product of the IDs in the corners is {cornerProduct}.");
        }

        public ImageTile(int cameraId, string[] rawImageData)
        {
            this.cameraId = cameraId;
            this.rawImageData = rawImageData;
            this.arrayBinaryLength = rawImageData[0].Length;

            processedImageData = ProcessRawImageData();

            unplacedTiles.Add(this);
        }

        public List<int> ProcessRawImageData()
        {
            List<int> edges = new List<int>();

            int topSide = Convert.ToInt32(rawImageData[0].Replace('#', '1').Replace('.', '0'), 2);
            int bottomSide = Convert.ToInt32(rawImageData[9].Replace('#', '1').Replace('.', '0'), 2);

            string rightSideString = "";
            string leftSideString = "";

            for (int i = 0; i < 10; i++)
            {
                rightSideString += rawImageData[i][0].ToString();
                leftSideString += rawImageData[i][9].ToString();
            }

            int rightSide = Convert.ToInt32(rightSideString.Replace('#', '1').Replace('.', '0'), 2);
            int leftSide = Convert.ToInt32(leftSideString.Replace('#', '1').Replace('.', '0'), 2);

            edges.Add(topSide);
            edges.Add(rightSide);
            edges.Add(bottomSide);
            edges.Add(leftSide);

            return edges;
        }

        public void RotateTile(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                processedImageData.Insert(0, processedImageData[3]);
                processedImageData.RemoveAt(4);

                processedImageData[1] = Tools.BinaryFlip(processedImageData[1], arrayBinaryLength);
                processedImageData[3] = Tools.BinaryFlip(processedImageData[3], arrayBinaryLength);
            }
        }

        public void ReflectTile()
        {
            int holdingVariable = processedImageData[2];
            processedImageData[2] = processedImageData[0];
            processedImageData[0] = holdingVariable;

            processedImageData[1] = Tools.BinaryFlip(processedImageData[1], arrayBinaryLength);
            processedImageData[3] = Tools.BinaryFlip(processedImageData[3], arrayBinaryLength);
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
                int dx = IndexDirection[imageIndex].Item1;
                int dy = IndexDirection[imageIndex].Item2;

                PlaceTile(setTile.arrayLocationX + dx, setTile.arrayLocationY + dy);

                return true;
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

            Console.WriteLine($"Placed Tile {cameraId} at ({this.arrayLocationX}, {this.arrayLocationY})");
        }

        public int CheckForSlot()
        {
            for (int direction = 0; direction < 4; direction++)
            {
                if (this.processedImageData[direction] == boundaryList[direction])
                {
                    continue;
                }
                else if (imageArray.TryGetValue(
                    (
                    this.arrayLocationX + IndexDirection[direction].Item1,
                    this.arrayLocationY + IndexDirection[direction].Item2
                    ),
                    out ImageTile adjacentTile
                    )
                    )
                {
                    continue;
                }
                else if (this.processedImageData[direction] < 0)
                {
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

        public void PrintBorders()
        {
            Console.WriteLine($"Tile {cameraId}");
            foreach (int border in processedImageData)
            {
                Console.WriteLine(border);
            }
            Console.WriteLine();
        }

    }
}
