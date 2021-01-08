using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D13P1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] busScheduleInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D13P1\BusSchedule.txt");

            int earliestDepartTime = Int32.Parse(busScheduleInput[0]);

            string[] busIdStringList = busScheduleInput[1].Replace("x,", "").Split(",");

            int[] busIdList = new int[busIdStringList.Length];

            ulong busIdProduct = 1;

            for (int index = 0; index < busIdStringList.Length; index++)
            {
                busIdList[index] = Int32.Parse(busIdStringList[index]);

                busIdProduct *= (ulong)busIdList[index];
            }

            Dictionary<int, int> busTimeOffset = new Dictionary<int, int>();
            
            foreach (int busId in busIdList)
            {
                int timeOffset;
                string busString = busScheduleInput[1];

                string busSubString = busString.Substring(0, busString.IndexOf("," + busId.ToString()) + 1);

                timeOffset = (busSubString.Length - busSubString.Replace(",", "").Length) % busId;

                busTimeOffset.Add(busId, timeOffset);
            }

            ulong goal = 0;

            foreach (KeyValuePair<int, int> bus in busTimeOffset)
            {
                Console.WriteLine($"Key: {bus.Key} Value: {bus.Value}");

                ulong modulusPortion = busIdProduct / (ulong)bus.Key;

                ulong otherPortion = 1;

                for (int i = 0; i < bus.Key - 2; i++)
                {
                    otherPortion *= (ulong)bus.Value;
                }

                otherPortion %= (ulong)bus.Key;

                Console.WriteLine(modulusPortion);
                Console.WriteLine(otherPortion);

                goal += modulusPortion * otherPortion;
            }

            goal %= busIdProduct;

            Console.WriteLine(busIdProduct);

            Console.WriteLine(goal);

        }
    }
}
