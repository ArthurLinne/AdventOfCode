using System;
using System.Collections.Generic;

namespace AdventOfCode2019D5P1
{
    class Program
    {
        static void Main()
        {
            string listFile = System.IO.File.ReadAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2019\AdventOfCode2019D5P1\IntcodeInput.txt");

            List<int> inputList = new List<int>();

            while (true)
            {
                if (listFile.IndexOf(",") == -1)
                {
                    inputList.Add(Int32.Parse(listFile));
                    break;
                }
                else
                {
                    string currentElement = listFile.Substring(0, listFile.IndexOf(","));

                    inputList.Add(Int32.Parse(currentElement));

                    listFile = listFile.Substring(listFile.IndexOf(",") + 1);
                }
            }

            IntCodeComputer.ComputeIntcode(inputList);
        }
    }
}
