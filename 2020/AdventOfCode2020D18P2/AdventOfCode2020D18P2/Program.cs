﻿using System;
using System.Collections.Generic;

namespace AdventOfCode2020D18P2
{
    class Program
    {
        public static long EvaluateSimpleLine(string inputLine)
        {
            List<long> numbers = new List<long>();
            List<char> operations = new List<char>();

            string numberString = "";
            foreach (char character in inputLine)
            {
                if (character == '+' || character == '*')
                {
                    numbers.Add(Int64.Parse(numberString));
                    numberString = "";

                    operations.Add(character);
                }
                else
                {
                    numberString += character;
                }
            }
            numbers.Add(Int32.Parse(numberString));

            int currentOperationIndex;

            while (numbers.Count > 1)
            {
                if (operations.Contains('+'))
                {
                    currentOperationIndex = operations.IndexOf('+');
                }
                else
                {
                    currentOperationIndex = 0;
                }
                
                long newNumber = -1;

                switch (operations[currentOperationIndex])
                {
                    case '+':
                        newNumber = numbers[currentOperationIndex] + numbers[currentOperationIndex + 1];
                        break;
                    case '*':
                        newNumber = numbers[currentOperationIndex] * numbers[currentOperationIndex + 1];
                        break;
                }

                operations.RemoveAt(currentOperationIndex);

                numbers.RemoveAt(currentOperationIndex);
                numbers[currentOperationIndex] = newNumber;
            }

            return numbers[0];
        }

        public static long EvaluateComplexLine(string inputLine)
        {
            string complexLine = inputLine;

            while (true)
            {
                if (!complexLine.Contains(")"))
                {
                    return EvaluateSimpleLine(complexLine);
                }

                int firstCloseIndex = complexLine.IndexOf(")");

                int lastPriorOpenIndex = complexLine.Substring(0, firstCloseIndex).LastIndexOf("(");

                long evaluatedExp = EvaluateSimpleLine(complexLine.Substring(lastPriorOpenIndex + 1, firstCloseIndex - lastPriorOpenIndex - 1));

                string priorString;
                if (lastPriorOpenIndex == 0)
                {
                    priorString = "";
                }
                else
                {
                    priorString = complexLine.Substring(0, lastPriorOpenIndex);
                }

                string postString;
                if (firstCloseIndex == complexLine.Length - 1)
                {
                    postString = "";
                }
                else
                {
                    postString = complexLine.Substring(firstCloseIndex + 1);
                }

                complexLine =
                    priorString
                    + evaluatedExp.ToString()
                    + postString;
            }
        }

        static void Main(string[] args)
        {
            string[] homeworkInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D18P1\HomeworkInput.txt");

            long evaluationSum = 0;

            foreach (string homeworkLine in homeworkInput)
            {
                long evaluatedLine = EvaluateComplexLine(homeworkLine.Replace(" ", ""));
                evaluationSum += evaluatedLine;
            }

            Console.WriteLine($"The sum of all expressions is {evaluationSum}.");

        }
    }
}
