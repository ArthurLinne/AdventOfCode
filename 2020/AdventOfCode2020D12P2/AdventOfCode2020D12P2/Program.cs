using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D12P2
{
    class Program
    {
        private static int ship_x = 0;
        private static int ship_y = 0;

        private static int waypoint_x = 10;
        private static int waypoint_y = 1;

        private static List<string> directionList = new List<string> { "E", "S", "W", "N" };

        public static void RotateShip(int degrees)
        {
            int turns = degrees / 90;

            turns %= 4;
            if (turns < 0)
            {
                turns += 4;
            }

            for (int rotations = 0; rotations < turns; rotations++)
            {
                int holding_variable = waypoint_x;
                waypoint_x = waypoint_y;
                waypoint_y = holding_variable;

                waypoint_y *= -1;
            }
        }

        public static void ReadNavInstruct(string instruct)
        {
            string action = instruct.Substring(0, 1);
            int value = Int32.Parse(instruct.Substring(1));

            switch (action)
            {
                case "N":
                    waypoint_y += value;
                    break;
                case "S":
                    waypoint_y -= value;
                    break;
                case "E":
                    waypoint_x += value;
                    break;
                case "W":
                    waypoint_x -= value;
                    break;
                case "R":
                    RotateShip(value);
                    break;
                case "L":
                    RotateShip(-1 * value);
                    break;
                case "F":
                    ship_x += waypoint_x * value;
                    ship_y += waypoint_y * value;
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

            Console.WriteLine($"The ship ends up at ({ship_x}, {ship_y}), and the distance is {Math.Abs(ship_x) + Math.Abs(ship_y)}.");
        }
    }
}
