using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D19P1
{
    class MessageRule
    {
        public static Dictionary<int, MessageRule> fullRuleSet = new Dictionary<int, MessageRule>();
        private const char DEFAULT_LETTER = ' ';

        public static bool EvaluateMessage0(string message)
        {
            (bool subMatchFound, int subReturnedMessageIndex) = fullRuleSet[0].EvaluateMessage(message);

            if (subMatchFound && subReturnedMessageIndex == message.Length)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private readonly List<List<int>> ruleSet = new List<List<int>>();
        private readonly int ruleId;
        private readonly char letter = DEFAULT_LETTER;

        public MessageRule(string ruleInput)
        {
            this.ruleId = Int32.Parse(ruleInput.Substring(0, ruleInput.IndexOf(":")).ToString());

            List<string> rawRuleSet = ruleInput.Substring(ruleInput.IndexOf(":") + 2).Replace(" | ", "|").Split("|").ToList();

            foreach (string rawRule in rawRuleSet)
            {
                if (rawRule[1] == 'a' || rawRule[1] == 'b')
                {
                    letter = rawRule[1];
                }

                else
                {
                    ruleSet.Add(rawRule.Split(" ").ToList().ConvertAll(x => Int32.Parse(x)));
                }   
            }

            fullRuleSet.Add(ruleId, this);
        }

        public (bool matchFound, int returnedMessageIndex) EvaluateMessage(string message, int messageIndex = 0, string depth = "")
        {
            bool failure = false;

            if (this.letter != DEFAULT_LETTER)
            {
                if (messageIndex >= message.Length)
                {
                    return (false, -1);
                }

                else if (message[messageIndex] == this.letter)
                {
                    messageIndex++;
                    return (true, messageIndex);
                }

                else
                {
                    return (false, -1);
                }
            }

            else
            {
                int holdingIndex = messageIndex;

                foreach (List<int> subRule in ruleSet)
                {
                    failure = false;
                    messageIndex = holdingIndex;
                    foreach (int rule in subRule)
                    {
                        (bool subMatchFound, int subReturnedMessageIndex) = fullRuleSet[rule].EvaluateMessage(message, messageIndex, depth + "*");

                        if (subMatchFound)
                        {
                            messageIndex = subReturnedMessageIndex;
                        }
                        else
                        {
                            failure = true;
                            break;
                        }
                    }

                    if (!failure)
                    {
                        break;
                    }
                }

                if (failure)
                {
                    return (false, -1);
                }

                else
                {
                    return (true, messageIndex);
                }
            }
        }

        public void PrintMessageRule()
        {
            Console.WriteLine($"Rule {ruleId}");
            if (letter != ' ')
            {
                Console.WriteLine($"Letter: {letter}");
            }
            else
            {
                for (int i = 0; i < ruleSet.Count; i++)
                {
                    Console.Write($"Set {i + 1}:");
                    foreach (int rule in ruleSet[i])
                    {
                        Console.Write($" {rule}");
                    }
                    Console.WriteLine();
                }
            }
        }

    }
}
