using System;
using System.Collections.Generic;

namespace AdventOfCode2019Day1P2
{
    class Program
    {
        public static int FuelConversion(int mass)
        {
            return (mass / 3) - 2;
        }

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2019\AdventOfCode2019D1P1\ModuleMassList.txt");

            List<int> massList = new List<int>();

            foreach (string entry in lines)
            {
                massList.Add(Int32.Parse(entry));
            }

            int totalFuel = 0;

            foreach (int mass in massList)
            {
                int moduleFuel = 0;
                int currentFuel = FuelConversion(mass);

                while (currentFuel > 0)
                {
                    moduleFuel += currentFuel;

                    currentFuel = FuelConversion(currentFuel);
                }

                totalFuel += moduleFuel;
            }

            Console.WriteLine($"Total Fuel Required: {totalFuel}");
        }
    }
}
