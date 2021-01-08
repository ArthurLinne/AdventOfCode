using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D12P1
{
    class Program
    {
        private static int ship_x = 0;
        private static int ship_y = 0;
        private static string ship_direction = "E";

        private static List<string> directionList = new List<string> { "E", "S", "W", "N" };

        public static void RotateShip(int degrees)
        {
            int turns = degrees / 90;

            int directionIndex = directionList.IndexOf(ship_direction);

            int newDirectionIndex = (directionIndex + turns) % 4;

            if (newDirectionIndex < 0)
            {
                newDirectionIndex += 4;
            }

            ship_direction = directionList[newDirectionIndex];
        }

        public static void ReadNavInstruct(string instruct)
        {
            string action = instruct.Substring(0, 1);
            int value = Int32.Parse(instruct.Substring(1));

            switch (action)
            {
                case "N":
                    ship_y += value;
                    break;
                case "S":
                    ship_y -= value;
                    break;
                case "E":
                    ship_x += value;
                    break;
                case "W":
                    ship_x -= value;
                    break;
                case "R":
                    RotateShip(value);
                    break;
                case "L":
                    RotateShip(-1 * value);
                    break;
                case "F":
                    ReadNavInstruct(ship_direction + value.ToString());
                    break;
            }
        }

        static void Main(string[] args)
        {
            List<string> navInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D12P1\NavigationInstructions.txt").ToList();

            foreach (string instruct in navInput)
            {
                ReadNavInstruct(instruct);
            }

            Console.WriteLine($"The ship ends up at ({ship_x}, {ship_y}), and the distance is {Math.Abs(ship_x) + Math.Abs(ship_y)}");
        }
    }
}
