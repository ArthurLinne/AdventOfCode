﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D14P2
{
    class Program
    {

        public static string ApplyBitmask(string bitmask, int value)
        {
            string binaryValue = Convert.ToString(value, 2);

            int binaryValueLength = binaryValue.Length;

            for (int stringIndex = 0; stringIndex < bitmask.Length - binaryValueLength; stringIndex++)
            {
                binaryValue = "0" + binaryValue;
            }

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

        public static List<ulong> EvaluateFloatingBits(string binaryValue)
        {
            List<string> valuePossibilities = new List<string>();

            valuePossibilities.Add(binaryValue);

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

            List<ulong> convertedValueList = new List<ulong>();

            foreach (string possibleValue in valuePossibilities)
            {
                convertedValueList.Add(Convert.ToUInt64(possibleValue, 2));
            }

            return convertedValueList;
        }


        static void Main(string[] args)
        {
            string[] bitmaskFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D14P1\BitmaskFile.txt");

            string currentBitmask = "";

            Dictionary<ulong, int> memory = new Dictionary<ulong, int>();

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

                    foreach (ulong possibleMemoryIndex in EvaluateFloatingBits(ApplyBitmask(currentBitmask, memoryIndex)))
                    {
                        if (memory.ContainsKey(possibleMemoryIndex))
                        {
                            memory[possibleMemoryIndex] = value;
                        }
                        else
                        {
                            memory.Add(possibleMemoryIndex, value);
                        }
                    }                    
                }
            }

            ulong totalValue = 0;

            foreach (int value in memory.Values)
            {
                totalValue += (ulong)value;
            }

            Console.WriteLine($"The total of all values is {totalValue}");

        }
    }
}