﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D19P2
{
    class Program
    {
        static void Main()
        {
            string[] monsterMessageInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D19P1\MonsterMessage.txt");

            List<string> possibleMessageList = new List<string>();

            foreach (string line in monsterMessageInput)
            {
                if (line == "")
                {
                    continue;
                }
                else if (line.Contains(':'))
                {
                    new MessageRule(line);
                }
                else
                {
                    possibleMessageList.Add(line);
                }
            }

            MessageRule.fullRuleSet[42].SetMinMaxViable();
            MessageRule.fullRuleSet[31].SetMinMaxViable();


            int totalMatching = 0;

            foreach (string possibleMessage in possibleMessageList)
            {
                if (MessageRule.EvaluateRecursiveMessage(possibleMessage))
                {
                    totalMatching++;
                }
            }

            Console.WriteLine($"The total number of matching messages is {totalMatching}");

        }
    }
}
