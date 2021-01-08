using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D19P1
{
    class Program
    {


        static void Main(string[] args)
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
                    MessageRule newMessageRule = new MessageRule(line);
                }
                else
                {
                    possibleMessageList.Add(line);
                }
            }

            //MessageRule.fullRuleSet[8].PrintMessageRule();

            //MessageRule.EvaluateMessage0(possibleMessageList[0]);

            int totalMatching = 0;

            foreach (string possibleMessage in possibleMessageList)
            {
                //Console.WriteLine($"Checking {possibleMessage}:");

                if (MessageRule.EvaluateMessage0(possibleMessage))
                {
                    //Console.WriteLine($"{possibleMessage} satisfies rule 0.");
                    //Console.WriteLine();
                    totalMatching++;
                }
                else
                {
                    //Console.WriteLine($"{possibleMessage} does not satisfy rule 0.");
                    //Console.WriteLine();
                }
            }

            Console.WriteLine($"The total number of matching messages is {totalMatching}");

        }
    }
}
