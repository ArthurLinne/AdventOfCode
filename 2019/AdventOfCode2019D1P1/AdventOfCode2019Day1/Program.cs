using System;
using System.Collections.Generic;

namespace AdventOfCode2019Day1
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

            int totalFuel = 0;

            foreach (string entry in lines)
            {
                massList.Add(Int32.Parse(entry));
            }

            foreach (int mass in massList)
            {
                Console.WriteLine($"Mass: {mass}");
                Console.WriteLine($"Fuel: {FuelConversion(mass)}");
                Console.WriteLine();

                totalFuel += FuelConversion(mass);
            }

            Console.WriteLine($"Total Fuel Required: {totalFuel}");

        }
    }
}
