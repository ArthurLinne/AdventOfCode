using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D19P2
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

        public static bool EvaluateRecursiveMessage(string message)
        {
            int availableSlots = message.Length / 8;

            for (int count31 = 1; count31 < availableSlots - count31; count31++)
            {
                string fakeRuleInput = "-1:";

                for (int i = 0; i < availableSlots; i++)
                {
                    if (availableSlots - i <= count31)
                    {
                        fakeRuleInput += " 31";
                    }
                    else
                    {
                        fakeRuleInput += " 42";
                    }
                }

                MessageRule fakeMessageRule = new MessageRule(fakeRuleInput);

                if (fakeMessageRule.EvaluateMessage(message).matchFound)
                {
                    return true;
                }
            }

            return false;
        }

        private readonly List<List<int>> ruleSet = new List<List<int>>();
        private readonly int ruleId;
        private readonly char letter = DEFAULT_LETTER;
        private int minViableMessage = -1;
        private int maxViableMessage = -1;


        public MessageRule(string ruleInput)
        {
            this.ruleId = Int32.Parse(ruleInput.Substring(0, ruleInput.IndexOf(":")).ToString());

            List<string> rawRuleSet = ruleInput.Substring(ruleInput.IndexOf(":") + 2).Replace(" | ", "|").Split("|").ToList();

            foreach (string rawRule in rawRuleSet)
            {
                if (rawRule[0] == '"' || rawRule[0] == '"')
                {
                    letter = rawRule[1];
                }

                else
                {
                    ruleSet.Add(rawRule.Split(" ").ToList().ConvertAll(x => Int32.Parse(x)));
                }
            }

            fullRuleSet[ruleId] = this;
        }

        public (bool matchFound, int returnedMessageIndex) EvaluateMessage(string message, int messageIndex = 0, string depth = "")
        {
            //Console.WriteLine($"{depth} Entered Rule {ruleId}");
            bool failure = false;

            if (this.letter != DEFAULT_LETTER)
            {
                if (messageIndex >= message.Length)
                {
                    //Console.WriteLine($"{depth} Failed rule {ruleId}.");
                    return (false, -1);
                }

                else if (message[messageIndex] == this.letter)
                {
                    //Console.WriteLine($"{depth} Succeeded rule {ruleId}.");
                    messageIndex++;
                    return (true, messageIndex);
                }
                else
                {
                    //Console.WriteLine($"{depth} Failed rule {ruleId}.");
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
                    //Console.WriteLine($"{depth} Failed rule {ruleId}.");
                    return (false, -1);
                }
                else
                {
                    //Console.WriteLine($"{depth} Succeeded rule {ruleId}.");
                    return (true, messageIndex);
                }

            }
        }


        public (int minViable, int maxViable) DetermineMinMaxViableMessage()
        {
            if (letter != DEFAULT_LETTER)
            {
                return (1, 1);
            }

            else
            {
                int min = ruleSet.Min(subRule => subRule.Sum(rule => fullRuleSet[rule].DetermineMinMaxViableMessage().minViable));
                int max = ruleSet.Max(subRule => subRule.Sum(rule => fullRuleSet[rule].DetermineMinMaxViableMessage().maxViable));

                return (min, max);
            }

        }

        public void SetMinMaxViable()
        {
            (minViableMessage, maxViableMessage) = DetermineMinMaxViableMessage();
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

            if (minViableMessage != -1)
            {
                Console.WriteLine($"The minimum viable message is {minViableMessage}");
                Console.WriteLine($"The maximum viable message is {maxViableMessage}");
            }

            Console.WriteLine();
        }

    }
}
