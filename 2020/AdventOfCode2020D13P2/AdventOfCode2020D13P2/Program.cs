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
                long modulusPortion = busIdProduct / bus.Key;

                long otherPortion = 1;

                for (long i = 0; i < bus.Key - 2; i++)
                {
                    otherPortion *= modulusPortion;
                    otherPortion %= bus.Key;
                }

                long addition = modulusPortion * otherPortion * bus.Value;

                goal += addition;
                goal %= busIdProduct;
            }

            Console.WriteLine($"The earliest time that all buses leave at the right offsets is {goal}.");

            System.IO.File.WriteAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D13P1\Output.txt", goal.ToString());
        }
    }
}
