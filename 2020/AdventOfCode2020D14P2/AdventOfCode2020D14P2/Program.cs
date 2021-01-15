using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D14P2
{
    class Program
    {

        public static string ApplyBitmask(string bitmask, int value)
        {
            StringBuilder binaryValueBuilder = new StringBuilder(Convert.ToString(value, 2), bitmask.Length);

            for (int stringIndex = 0; binaryValueBuilder.Length < bitmask.Length; stringIndex++)
            {
                binaryValueBuilder.Insert(0, "0");
            }

            string binaryValue = binaryValueBuilder.ToString();

            for (int stringIndex = 0; stringIndex < bitmask.Length; stringIndex++)
            {                
                if (bitmask[stringIndex] != '0')
                {
                    if (stringIndex == 0)
                    {
                        binaryValue =
                            bitmask[stringIndex]
                            + binaryValue.Substring(stringIndex + 1, binaryValue.Length - stringIndex - 1);
                    }
                    else if (stringIndex == bitmask.Length - 1)
                    {
                        binaryValue =
                            binaryValue.Substring(0, stringIndex)
                            + bitmask[stringIndex];
                    }
                    else
                    {
                        binaryValue =
                            binaryValue.Substring(0, stringIndex)
                            + bitmask[stringIndex]
                            + binaryValue.Substring(stringIndex + 1, binaryValue.Length - stringIndex - 1);
                    }
                }
            }

            return binaryValue;
        }

        public static List<long> EvaluateFloatingBits(string binaryValue)
        {
            List<string> valuePossibilities = new List<string>
            {
                binaryValue
            };

            while (valuePossibilities[0].IndexOf("X") >= 0)
            {
                List<string> valuePossibilityAddition = new List<string>();

                foreach (string possibleValue in valuePossibilities) 
                {
                    for (int i = 0; i < 2; i++)
                    {
                        char[] charArr = possibleValue.ToCharArray();
                        charArr[possibleValue.IndexOf("X")] = i.ToString()[0];
                        valuePossibilityAddition.Add(new string(charArr));
                    }
                }
                valuePossibilities = valuePossibilityAddition;
            }

            List<long> convertedValueList = valuePossibilities.ConvertAll(x => Convert.ToInt64(x, 2));

            return convertedValueList;
        }


        static void Main(string[] args)
        {
            string[] bitmaskFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D14P1\BitmaskFile.txt");

            string currentBitmask = "";

            Dictionary<long, int> memory = new Dictionary<long, int>();

            foreach (string line in bitmaskFile)
            {
                if (line.Substring(0, 4) == "mask")
                {
                    currentBitmask = line.Substring(line.IndexOf("=") + 2, line.Length - line.IndexOf("=") - 2);
                }
                else if (line.Substring(0, 3) == "mem")
                {
                    int memoryIndex = Int32.Parse(line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - line.IndexOf("[") - 1));

                    int value = Int32.Parse(line.Substring(line.IndexOf("=") + 2, line.Length - line.IndexOf("=") - 2));

                    foreach (long possibleMemoryIndex in EvaluateFloatingBits(ApplyBitmask(currentBitmask, memoryIndex)))
                    {
                        memory[possibleMemoryIndex] = value;
                    }                    
                }
            }

            long totalValue = memory.Values.Sum(x => (long)x);

            Console.WriteLine($"The total of all values is {totalValue}");
        }
    }
}
