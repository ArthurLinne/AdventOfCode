using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D23P2
{
    class Program
    {
        static void Main()
        {
            int[] circleList = Array.ConvertAll<char, int>("952316487".ToCharArray(), x => Int32.Parse(x.ToString()));

            CupCircle cupCircle = new CupCircle(circleList, 1000000);

            cupCircle.PlayMultipleRounds(10000000);

            cupCircle.PrintCircleFinal();
        }
    }
}
