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

            string[] busIdStringList = busScheduleInput[1].Replace("x,", "").Split(",");

            long[] busIdList = new long[busIdStringList.Length];

            long busIdProduct = 1;

            for (int index = 0; index < busIdStringList.Length; index++)
            {
                busIdList[index] = Int64.Parse(busIdStringList[index]);

                busIdProduct *= busIdList[index];
                Console.WriteLine(busIdProduct);
            }

            Dictionary<long, long> busTimeOffset = new Dictionary<long, long>();
            
            foreach (long busId in busIdList)
            {
                long timeOffset;
                string busString = busScheduleInput[1];

                string busSubString = busString.Substring(0, busString.IndexOf("," + busId.ToString()) + 1);

                timeOffset = (long)(busSubString.Length - busSubString.Replace(",", "").Length) % busId;

                timeOffset = (busId - timeOffset) % busId;

                busTimeOffset.Add(busId, timeOffset);
            }

            long goal = 0;

            foreach (KeyValuePair<long, long> bus in busTimeOffset)
            {
                Console.WriteLine($"Key: {bus.Key} Value: {bus.Value}");

                long modulusPortion = busIdProduct / bus.Key;

                long otherPortion = 1;

                for (long i = 0; i < bus.Key - 2; i++)
                {
                    otherPortion *= modulusPortion;
                    otherPortion %= bus.Key;
                }

                

                Console.WriteLine($"Checking for consistency: {(otherPortion * modulusPortion) % bus.Key}");

                Console.WriteLine(modulusPortion);
                Console.WriteLine(otherPortion);

                long addition = modulusPortion * otherPortion * bus.Value;

                Console.WriteLine($"addition % key = {addition % bus.Key}");

                goal += addition;
                goal %= busIdProduct;
                Console.WriteLine($"goal: {goal}");
            }

            Console.WriteLine(busIdProduct);

            Console.WriteLine(goal);

            foreach (long busId in busTimeOffset.Keys)
            {
                Console.WriteLine($"{busId} leaves after {goal % (long)busId} minutes.");
            }

            System.IO.File.WriteAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D13P1\Output.txt", goal.ToString());
        }
    }
}
