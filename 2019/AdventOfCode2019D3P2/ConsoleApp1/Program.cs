using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019D3P2
{
    class Program
    {
        public static List<string> CreateInstructionList(string instructionFile)
        {
            List<string> instructionList = new List<string>();

            while (true)
            {
                if (instructionFile.IndexOf(",") == -1)
                {
                    instructionList.Add(instructionFile);
                    break;
                }
                else
                {
                    string currentElement = instructionFile.Substring(0, instructionFile.IndexOf(","));

                    instructionList.Add(currentElement);

                    instructionFile = instructionFile.Substring(instructionFile.IndexOf(",") + 1);
                }
            }

            return instructionList;
        }

        public static List<int[]> CreateWire(List<string> instructionList)
        {
            List<int[]> newWire = new List<int[]>();

            int[] currentLocation = { 0, 0 };


            // This adds the central port to the wire.
            // Since we don't want to count intersections at the central port, we can just exclude it from the wire list.
            //newWire.Add((int[]) currentLocation.Clone());

            foreach (string instruct in instructionList)
            {
                int deltaX = 0;
                int deltaY = 0;

                switch (instruct.Substring(0, 1))
                {
                    case "R":
                        deltaX = 1;
                        break;
                    case "L":
                        deltaX = -1;
                        break;
                    case "U":
                        deltaY = 1;
                        break;
                    case "D":
                        deltaY = -1;
                        break;
                    default:
                        break;
                }

                int wireLength = Int32.Parse(instruct.Substring(1, instruct.Length - 1));

                for (int i = 0; i < wireLength; i++)
                {
                    currentLocation[0] += deltaX;
                    currentLocation[1] += deltaY;

                    newWire.Add((int[])currentLocation.Clone());
                }


            }

            return newWire;
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2019\AdventOfCode2019D3P1\WireInstructions.txt");

            List<int[]> firstWire = CreateWire(CreateInstructionList(lines[0]));
            List<int[]> secondWire = CreateWire(CreateInstructionList(lines[1]));

            List<int[]> intersectionList = new List<int[]>();

            foreach (int[] firstPoint in firstWire)
            {
                foreach (int[] secondPoint in secondWire)
                {
                    if (firstPoint[0] == secondPoint[0] && firstPoint[1] == secondPoint[1])
                    {
                        intersectionList.Add(firstPoint);
                    }
                }
            }

            int minimumDistance = -1;

            foreach (int[] intersectPoint in intersectionList)
            {
                Console.WriteLine($"Current intersection point to check: [{intersectPoint[0]}, {intersectPoint[1]}]");

                int currentDistance = 0;

                for (int index = 0; index < firstWire.Count; index++)
                {
                    if (firstWire[index][0] == intersectPoint[0] && firstWire[index][1] == intersectPoint[1])
                    {
                        currentDistance += index + 1;
                        Console.WriteLine($"Distance for first wire: {index + 1}");
                        break;
                    }
                }

                for (int index = 0; index < secondWire.Count; index++)
                {
                    if (secondWire[index][0] == intersectPoint[0] && secondWire[index][1] == intersectPoint[1])
                    {
                        currentDistance += index + 1;
                        Console.WriteLine($"Distance for second wire: {index + 1}");
                        break;
                    }
                }

                if (minimumDistance == -1 | currentDistance < minimumDistance)
                {
                    minimumDistance = currentDistance;
                }
                Console.WriteLine();
            }

            Console.WriteLine($"The minimum distance is {minimumDistance}.");

        }
    }
}
