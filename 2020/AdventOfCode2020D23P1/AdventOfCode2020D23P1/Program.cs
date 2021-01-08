using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D23P1
{
    class Program
    {
        static void Main()
        {
            List<int> circleList = "952316487".ToCharArray().ToList().ConvertAll<int>(x => Int32.Parse(x.ToString()));

            CupCircle cupCircle = new CupCircle(circleList);

            cupCircle.PlayMultipleRounds(100);

            cupCircle.PrintCircleFinal();
        }
    }
}
