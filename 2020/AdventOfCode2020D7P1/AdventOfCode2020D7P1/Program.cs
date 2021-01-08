using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D7P1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] bagRulesFile = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D7P1\BagRulesFile.txt");

            Dictionary<string, List<string>> baseBagRules = new Dictionary<string, List<string>>();

            foreach (string line in bagRulesFile)
            {
                string outerBag = line.Substring(0, line.IndexOf("bags") - 1);

                string innerBags = line.Substring(line.IndexOf("contain") + 8, line.Length - (line.IndexOf("contain") + 8));

                innerBags = innerBags.Replace(" bags", "").Replace(" bag", "").Replace(".", "").Replace(", ", ",");

                foreach (char letter in "123456789")
                {
                    innerBags = innerBags.Replace(letter + " ", "");
                }

                List<string> innerBagsList = innerBags.Split(",").ToList();

                baseBagRules.Add(outerBag, innerBagsList);
            }

            Dictionary<string, List<string>> possibleBagDict = new Dictionary<string, List<string>>();



            foreach(KeyValuePair<string, List<string>> bagRule in baseBagRules)
            {
                string outerBag = bagRule.Key;
                List<string> innerBagList = bagRule.Value;

                List<string> currentInnerBagList = innerBagList;

                List<string> possibleBagList = new List<string>();

                while (currentInnerBagList.Count > 0)
                {
                    possibleBagList.AddRange(currentInnerBagList);

                    if (currentInnerBagList[0] == "no other")
                    {
                        break;
                    }

                    List<string> newInnerBagList = new List<string>();

                    foreach (string innerBag in currentInnerBagList)
                    {
                        foreach (string deepBag in baseBagRules[innerBag])
                        {
                            if (deepBag == "no other")
                            {
                                continue;
                            }
                            if (!possibleBagList.Contains(deepBag))
                            {
                                newInnerBagList.Add(deepBag);
                            }
                        }
                    }

                    currentInnerBagList = newInnerBagList;
                }

                possibleBagDict.Add(outerBag, possibleBagList);

            }

            int totalGold = 0;

            foreach (KeyValuePair<string, List<String>> bagSet in possibleBagDict)
            {
                if (bagSet.Value.Contains("shiny gold"))
                {
                    totalGold++;
                }
            }

            Console.WriteLine($"The total number of bags that can contain shiny gold bags is {totalGold}");

        }
    }
}


