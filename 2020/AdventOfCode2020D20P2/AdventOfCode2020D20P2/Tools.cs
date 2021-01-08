using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D20P2
{
    public static class Tools
    {
        public static int BinaryFlip(int input, int binaryLength)
        {
            string binaryValue = Convert.ToString(input, 2);

            while (binaryValue.Length < binaryLength)
            {
                binaryValue = "0" + binaryValue;
            }

            List<char> binaryList = binaryValue.ToCharArray().ToList();

            binaryList.Reverse();

            string binaryValueReversed = new string(binaryList.ToArray());

            return Convert.ToInt32(binaryValueReversed, 2);
        }
    }
}
