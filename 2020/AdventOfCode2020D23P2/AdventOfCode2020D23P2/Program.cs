using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace AdventOfCode2020D23P2
{
    class Program
    {
        static void Main()
        {
            Stopwatch stopWatch = new Stopwatch();

            //int[] circleList = Array.ConvertAll<char, int>("952316487".ToCharArray(), x => Int32.Parse(x.ToString()));
            int[] circleList = Array.ConvertAll<char, int>("389125467".ToCharArray(), x => Int32.Parse(x.ToString()));
            //int[] circleList = Array.ConvertAll<char, int>("123456789".ToCharArray(), x => Int32.Parse(x.ToString()));


            //CupCircle cupCircle = new CupCircle(circleList, 1000000);
            CupCircle cupCircle = new CupCircle(circleList, 100);

            stopWatch.Start();

            cupCircle.PlayMultipleRounds(1000);

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            cupCircle.PrintCircleFinal();
        }
    }
}
