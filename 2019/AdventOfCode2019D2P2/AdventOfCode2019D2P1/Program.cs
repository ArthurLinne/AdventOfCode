using System;
using System.Collections.Generic;

namespace AdventOfCode2019D2P1
{

    class Program
    {
        public static int ComputeIntcode(List<int> input)
        {
            List<int> codeList = new List<int>(input);
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

            bool fullBreak = false;
            int noun = -1;
            int verb = -1;



            for (int i = 0;  i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    //List<int> inputListInstance = new List<int>(inputList);

                    inputList[1] = i;
                    inputList[2] = j;

                    if (ComputeIntcode(inputList) == 19690720)
                    {
                        noun = i;
                        verb = j;
                        fullBreak = true;
                        break;
                    }
                }
                if (fullBreak)
                {
                    break;
                }
            }

            Console.WriteLine($"The noun is {noun}, the verb is {verb}, so together they give {100 * noun + verb}.");

        }
    }
}
