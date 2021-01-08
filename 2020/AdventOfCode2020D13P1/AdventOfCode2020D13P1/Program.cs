using System;

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

            for (int index = 0; index < busIdStringList.Length; index++)
            {
                busIdList[index] = Int32.Parse(busIdStringList[index]);
            }

            int waitTime = -1;
            int correctBusId = -1;
            bool found = false;

            for (int departureStartTime = earliestDepartTime; true; departureStartTime++)
            {
                foreach (int busId in busIdList)
                {
                    if (departureStartTime % busId == 0)
                    {
                        waitTime = departureStartTime - earliestDepartTime;
                        correctBusId = busId;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }

            }

            Console.WriteLine($"You'll need to wait {waitTime} minutes to catch the {correctBusId} bus. Their product is {waitTime * correctBusId}.");
        }
    }
}
