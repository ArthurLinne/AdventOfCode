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

            List<int> busIdList = busScheduleInput[1].Replace("x,", "").Split(",").ToList().ConvertAll(x => Int32.Parse(x));

            int waitTime = -1;
            int correctBusId = -1;

            for (int departureStartTime = earliestDepartTime; waitTime == -1; departureStartTime++)
            {
                foreach (int busId in busIdList)
                {
                    if (departureStartTime % busId == 0)
                    {
                        waitTime = departureStartTime - earliestDepartTime;
                        correctBusId = busId;
                        break;
                    }
                }
            }

            Console.WriteLine($"You'll need to wait {waitTime} minutes to catch the {correctBusId} bus. Their product is {waitTime * correctBusId}.");
        }
    }
}
