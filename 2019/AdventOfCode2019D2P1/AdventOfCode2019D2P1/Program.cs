using System;
using System.Collections.Generic;

namespace AdventOfCode2019D2P1
{

    class Program
    {
        public static int ComputeIntcode(List<int> input)
        {
            List<int> codeList = input;
            int currentIndex = 0;
            bool continueLoop = true;

            while (continueLoop)
            {
                switch (codeList[currentIndex])
                {
                    case 99:
                        continueLoop = false;
                        break;
                    case 1:
                        codeList[codeList[currentIndex + 3]] = codeList[codeList[currentIndex + 1]] + codeList[codeList[currentIndex + 2]];
                        break;
                    case 2:
                        codeList[codeList[currentIndex + 3]] = codeList[codeList[currentIndex + 1]] * codeList[codeList[currentIndex + 2]];
                        break;
                    default:
                        Console.WriteLine("Error!");
                        continueLoop = false;
                        break;
                }

                currentIndex += 4;

                
            }


            return codeList[0];
        }

        static void Main(string[] args)
        {
            string listFile = System.IO.File.ReadAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2019\AdventOfCode2019D2P1\Intcode File.txt");

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

            inputList[1] = 12;
            inputList[2] = 2;

            Console.WriteLine(ComputeIntcode(inputList));

        }
    }
}
