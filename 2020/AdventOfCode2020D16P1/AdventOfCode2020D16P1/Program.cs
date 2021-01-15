using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D16P1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int[]> fieldRules = new Dictionary<string, int[]>();
            List<int> yourTicket;
            List<List<int>> nearbyTickets = new List<List<int>>();

            string ticketInput = System.IO.File.ReadAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D16P1\TrainTicket.txt");

            ticketInput = ticketInput.Replace(Environment.NewLine, "|");
            ticketInput = ticketInput.Replace("||", "*");

            string[] ticketInputArray = ticketInput.Split("*");

            foreach (string line in ticketInputArray[0].Split("|"))
            {
                string field = line.Substring(0, line.IndexOf(":"));

                int[] ruleList = new int[4];

                ruleList[0] = Int32.Parse(line.Substring(line.IndexOf(":") + 2, line.IndexOf("-") - line.IndexOf(":") - 2));
                ruleList[1] = Int32.Parse(line.Substring(line.IndexOf("-") + 1, line.IndexOf(" or") - line.IndexOf("-") - 1));
                ruleList[2] = Int32.Parse(line.Substring(line.IndexOf("or ") + 3, line.LastIndexOf("-") - line.IndexOf("or ") - 3));
                ruleList[3] = Int32.Parse(line.Substring(line.LastIndexOf("-") + 1, line.Length - line.LastIndexOf("-") - 1));

                fieldRules[field] = ruleList;
            }

            yourTicket = ticketInputArray[1].Replace("your ticket:|", "").Split(",").ToList().ConvertAll(x => Int32.Parse(x));

            foreach (string line in ticketInputArray[2].Replace("nearby tickets:|", "").Split("|"))
            {
                nearbyTickets.Add(line.Split(",").ToList().ConvertAll(x => Int32.Parse(x)));
            }

            int errorRate = 0;

            foreach (List<int> ticket in nearbyTickets)
            {
                foreach (int ticketValue in ticket)
                {
                    bool invalidTicket = true;
                    foreach (int[] rule in fieldRules.Values)
                    {
                        if (
                            (rule[0] <= ticketValue && ticketValue <= rule[1]) 
                            || 
                            (rule[2] <= ticketValue && ticketValue <= rule[3])
                            )
                        {
                            invalidTicket = false;
                            break;
                        }
                    }
                    if (invalidTicket)
                    {
                        errorRate += ticketValue;
                        break;
                    }
                }
            }

            Console.WriteLine($"The error rate is {errorRate}");

        }
    }
}
