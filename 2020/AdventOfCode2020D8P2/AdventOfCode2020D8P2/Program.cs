using System;
using System.Collections.Generic;

namespace AdventOfCode2020D8P2
{
    class Program
    {
        static int? EvaluateBootCode(List<(string, int)> bootCode)
        {
            int accumulator = 0;
            int currentIndex = 0;

            List<int> evaluatedIndices = new List<int>();

            while (!evaluatedIndices.Contains(currentIndex) && currentIndex < bootCode.Count)
            {
                evaluatedIndices.Add(currentIndex);

                switch (bootCode[currentIndex].Item1)
                {
                    case "acc":
                        accumulator += bootCode[currentIndex].Item2;
                        currentIndex++;
                        break;
                    case "jmp":
                        currentIndex += bootCode[currentIndex].Item2;
                        break;
                    case "nop":
                        currentIndex++;
                        break;
                }

                if (evaluatedIndices.Contains(currentIndex))
                {
                    return null;
                }

                if (currentIndex >= bootCode.Count)
                {
                    return accumulator;
                }
            }

            return accumulator; // Just to quell the concerns of the loop.
        }

        static void Main(string[] args)
        {
            string[] bootCodeInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D8P1\HandheldBootCode.txt");

            List<(string, int)> bootCode = new List<(string, int)>();

            foreach (string instruction in bootCodeInput)
            {
                string instructMod = instruction.Replace("+", "");

                string operation = instructMod.Substring(0, instructMod.IndexOf(" "));

                int argument = Int32.Parse(instructMod.Substring(instructMod.IndexOf(" ") + 1, instructMod.Length - (instructMod.IndexOf(" ") + 1)));

                (string, int) fullInstruction = (operation, argument);

                bootCode.Add(fullInstruction);
            }

            for (int possibleErrorIndex = 0; possibleErrorIndex < bootCode.Count; possibleErrorIndex++)
            {
                switch (bootCode[possibleErrorIndex].Item1)
                {
                    case "acc":
                        continue;
                    case "jmp":
                        bootCode[possibleErrorIndex] = ("nop", bootCode[possibleErrorIndex].Item2);
                        break;
                    case "nop":
                        bootCode[possibleErrorIndex] = ("jmp", bootCode[possibleErrorIndex].Item2);
                        break;
                    
                }

                int? bootCodeReturn = EvaluateBootCode(bootCode);

                if (!(bootCodeReturn is null))
                {
                    Console.WriteLine($"The error occurred at index {possibleErrorIndex}.");
                    Console.WriteLine($"The boot code returns {bootCodeReturn}");
                    break;
                }
                else
                {
                    switch (bootCode[possibleErrorIndex].Item1)
                    {
                        case "acc":
                            continue;
                        case "jmp":
                            bootCode[possibleErrorIndex] = ("nop", bootCode[possibleErrorIndex].Item2);
                            break;
                        case "nop":
                            bootCode[possibleErrorIndex] = ("jmp", bootCode[possibleErrorIndex].Item2);
                            break;
                    }
                }
            }


        }
    }
}
