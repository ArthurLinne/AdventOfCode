using System;
using System.Collections.Generic;

namespace AdventOfCode2020D14P1
{
    class Program
    {

        public static ulong ApplyBitmask(string bitmask, int value)
        {
            string binaryValue = Convert.ToString(value, 2);

            int binaryValueLength = binaryValue.Length;

            for (int stringIndex = 0; stringIndex < bitmask.Length - binaryValueLength; stringIndex++)
            {
                binaryValue = "0" + binaryValue;
            }

            for (int stringIndex = 0; stringIndex < bitmask.Length; stringIndex++)
            {
                if (bitmask[stringIndex] != 'X')
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

            ulong maskedValue = Convert.ToUInt64(binaryValue, 2);
            
            return maskedValue;
        }


        static void Main(string[] args)
        {
            string[] bitmaskFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D14P1\BitmaskFile.txt");

            string currentBitmask = "";

            Dictionary<int, ulong> memory = new Dictionary<int, ulong>();

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

                    if (memory.ContainsKey(memoryIndex))
                    {
                        memory[memoryIndex] = ApplyBitmask(currentBitmask, value);
                    }
                    else
                    {
                        memory.Add(memoryIndex, ApplyBitmask(currentBitmask, value));
                    }
                }
            }

            ulong totalValue = 0;

            foreach (ulong value in memory.Values)
            {
                totalValue += value;
            }

            Console.WriteLine($"The total of all values is {totalValue}");

        }
    }
}
