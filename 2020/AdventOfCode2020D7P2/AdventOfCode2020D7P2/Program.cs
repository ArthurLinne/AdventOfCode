using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D7P2
{
    class Program
    {

        static int RecursiveBagsContained(
            string color, 
            int multiplier, 
            Dictionary<string, List<(string, int)>> bagRules)
        {
            int totalBags = 0;

            if (bagRules[color][0].Item2 == 0)
            {
                return multiplier;
            }

            else
            {
                foreach ((string, int) innerBag in bagRules[color])
                {
                    totalBags += RecursiveBagsContained(innerBag.Item1, multiplier * innerBag.Item2, bagRules);
                }

                totalBags += multiplier;
            }

            return totalBags;
        }


        static void Main(string[] args)
        {
            string[] bagRulesFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D7P1\BagRulesFile.txt");

            Dictionary<string, List<(string, int)>> baseBagRules = new Dictionary<string, List<(string, int)>>();

            foreach (string line in bagRulesFile)
            {
                string outerBag = line.Substring(0, line.IndexOf("bags") - 1);
                string innerBags = line.Substring(line.IndexOf("contain") + 8, line.Length - (line.IndexOf("contain") + 8));

                innerBags = innerBags.Replace(" bags", "").Replace(" bag", "").Replace(".", "").Replace(", ", ",");

                string[] innerBagsList = innerBags.Split(",");

                List<(string, int)> innerBagAmountList = new List<(string, int)>();

                foreach (string innerBag in innerBagsList)
                {
                    if (innerBag == "no other")
                    {
                        (string, int) emptyBag = ("None", 0);

                        innerBagAmountList.Add(emptyBag);

                        break;
                    }

                    string insertInnerBag = innerBag.Substring(innerBag.IndexOf(" ") + 1, innerBag.Length - innerBag.IndexOf(" ") - 1);

                    int innerBagQuantity = Int32.Parse(innerBag[0].ToString());

                    (string, int) innerBagAmount = (insertInnerBag, innerBagQuantity);

                    innerBagAmountList.Add(innerBagAmount);
                }

                baseBagRules.Add(outerBag, innerBagAmountList);
            }

            int shinyGoldCapacity = RecursiveBagsContained("shiny gold", 1, baseBagRules) - 1;

            Console.WriteLine($"A shiny gold bag must contain {shinyGoldCapacity} bags.");
            
        }
    }
}


