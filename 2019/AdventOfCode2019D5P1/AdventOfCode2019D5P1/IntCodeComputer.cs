using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019D5P1
{
    static class IntCodeComputer
    {

        private static readonly int MAX_PARAMETERS = 5;

        private static int OpCode1(List<int> inputList, int index, int parameterMode1, int parameterMode2, int parameterMode3)
        {
            int param1 = 0;
            int param2 = 0;
            int output1 = 0;
            int indexIncrease = 4;

            if (parameterMode1 == 0)
            {
                param1 = inputList[index + 1];
            }
            else if (parameterMode1 == 1)
            {
                param1 = index + 1;
            }

            if (parameterMode2 == 0)
            {
                param2 = inputList[index + 2];
            }
            else if (parameterMode1 == 1)
            {
                param2 = index + 2;
            }

            if (parameterMode3 == 0)
            {
                output1 = inputList[index + 3];
            }
            else if (parameterMode1 == 1)
            {
                param1 = index + 3;
            }

            inputList[output1] = inputList[param1] + inputList[param2];

            return indexIncrease;
        }

        private static int OpCode2(List<int> inputList, int index, int parameterMode1, int parameterMode2, int parameterMode3)
        {
            int param1 = 0;
            int param2 = 0;
            int output1 = 0;
            int indexIncrease = 4;

            if (parameterMode1 == 0)
            {
                param1 = inputList[index + 1];
            }
            else if (parameterMode1 == 1)
            {
                param1 = index + 1;
            }

            if (parameterMode2 == 0)
            {
                param2 = inputList[index + 2];
            }
            else if (parameterMode1 == 1)
            {
                param2 = index + 2;
            }

            if (parameterMode3 == 0)
            {
                output1 = inputList[index + 3];
            }
            else if (parameterMode1 == 1)
            {
                param1 = index + 3;
            }

            inputList[output1] = inputList[param1] * inputList[param2];

            return indexIncrease;
        }

        private static int OpCode3(List<int> inputList, int index, int parameterMode1)
        {
            int param1 = 0;
            int indexIncrease = 2;

            if (parameterMode1 == 0)
            {
                param1 = inputList[index + 1];
            }
            else if (parameterMode1 == 1)
            {
                param1 = index + 1;
            }

            Console.WriteLine("Please enter an input:");
            inputList[param1] = Int32.Parse(Console.ReadLine());

            return indexIncrease;
        }
                
        private static int OpCode4(List<int> inputList, int index, int parameterMode1)
        {
            int param1 = 0;
            int indexIncrease = 2;

            if (parameterMode1 == 0)
            {
                param1 = inputList[index + 1];
            }
            else if (parameterMode1 == 1)
            {
                param1 = index + 1;
            }

            Console.WriteLine(inputList[param1]);

            return indexIncrease;
        }

        public static void ComputeIntcode(List<int> input)
        {
            List<int> codeList = new List<int>(input);
            int currentIndex = 0;
            bool continueLoop = true;

            while (continueLoop)
            {
                string currentInstruction = codeList[currentIndex].ToString();

                if (currentInstruction.Length < MAX_PARAMETERS)
                {
                    string zeroList = new String('0', MAX_PARAMETERS);
                    currentInstruction = zeroList.Substring(0, MAX_PARAMETERS - currentInstruction.Length) + currentInstruction;
                }

                int currentOpCode = Int32.Parse(currentInstruction.Substring(currentInstruction.Length - 2, 2));

                int parameterMode1 = Int32.Parse(currentInstruction.Substring(currentInstruction.Length - 3));
                int parameterMode2 = Int32.Parse(currentInstruction.Substring(currentInstruction.Length - 4));
                int parameterMode3 = Int32.Parse(currentInstruction.Substring(currentInstruction.Length - 5));


                switch (currentOpCode)
                {
                    case 99:
                        continueLoop = false;
                        Console.WriteLine("Reached 99, break.");
                        break;
                    case 1:
                        currentIndex += OpCode1(codeList, currentIndex, parameterMode1, parameterMode2, parameterMode3);
                        break;
                    case 2:
                        currentIndex += OpCode2(codeList, currentIndex, parameterMode1, parameterMode2, parameterMode3);
                        break;
                    case 3:
                        currentIndex += OpCode3(codeList, currentIndex, parameterMode1);
                        break;
                    case 4:
                        currentIndex += OpCode4(codeList, currentIndex, parameterMode1);
                        break;
                    default:
                        continueLoop = false;
                        Console.WriteLine("Reached unknown OpCode, break.");
                        Console.WriteLine($"Current OpCode: {currentOpCode}");
                        Console.WriteLine($"Current Index: {currentIndex}");
                        break;
                }
            }
        }
    }
}
