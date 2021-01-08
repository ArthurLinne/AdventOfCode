using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D16P1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] trainTicketInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D16P1\TrainTicket.txt");

            Dictionary<string, int[]> fieldRules = new Dictionary<string, int[]>();
            List<int> yourTicket = new List<int>();
            List<List<int>> nearbyTickets = new List<List<int>>();

            bool insertYourTicket = false;
            bool insertNearbyTickets = false;

            foreach (string line in trainTicketInput)
            {
                if (line == "" || line == "nearby tickets:")
                {
                    continue;
                }

                if (line == "your ticket:")
                {
                    insertYourTicket = true;
                    continue;
                }

                if (insertYourTicket)
                {
                    yourTicket = line.Split(",").ToList().ConvertAll(x => Int32.Parse(x));
                    insertYourTicket = false;
                    insertNearbyTickets = true;
                    continue;
                }

                if (insertNearbyTickets)
                {
                    nearbyTickets.Add(line.Split(",").ToList().ConvertAll(x => Int32.Parse(x)));
                }
                else
                {
                    string field = line.Substring(0, line.IndexOf(":"));

                    int[] ruleList = new int[4];

                    ruleList[0] = Int32.Parse(line.Substring(line.IndexOf(":") + 2, line.IndexOf("-") - line.IndexOf(":") - 2));
                    ruleList[1] = Int32.Parse(line.Substring(line.IndexOf("-") + 1, line.IndexOf(" or") - line.IndexOf("-") - 1));
                    ruleList[2] = Int32.Parse(line.Substring(line.IndexOf("or ") + 3, line.LastIndexOf("-") - line.IndexOf("or ") - 3));
                    ruleList[3] = Int32.Parse(line.Substring(line.LastIndexOf("-") + 1, line.Length - line.LastIndexOf("-") - 1));

                    fieldRules.Add(field, ruleList);
                }
            }

            List<int> ticketsToDelete = new List<int>();

            for (int ticketIndex = 0; ticketIndex < nearbyTickets.Count; ticketIndex++)
            {
                List<int> ticket = nearbyTickets[ticketIndex];

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
                        ticketsToDelete.Add(ticketIndex);
                        break;
                    }
                }
            }

            ticketsToDelete.Reverse();

            foreach (int ticketIndex in ticketsToDelete)
            {
                nearbyTickets.RemoveAt(ticketIndex);
            }

            Dictionary<string, List<int>> possibleFieldLocations = new Dictionary<string, List<int>>();

            foreach (string field in fieldRules.Keys)
            {
                possibleFieldLocations.Add(field, Enumerable.Range(0, fieldRules.Count).ToList());
            }

            foreach (List<int> ticket in nearbyTickets)
            {
                foreach (int ticketValue in ticket)
                {
                    foreach (KeyValuePair<string, int[]> fieldRule in fieldRules)
                    {
                        string field = fieldRule.Key;
                        int[] rule = fieldRule.Value;

                        if (
                            (rule[0] <= ticketValue && ticketValue <= rule[1])
                            ||
                            (rule[2] <= ticketValue && ticketValue <= rule[3])
                            )
                        {
                            continue;
                        }
                        else
                        {
                            possibleFieldLocations[field].Remove(ticket.IndexOf(ticketValue));
                        }
                    }
                }
            }

            Dictionary<string, int> fieldLocations = new Dictionary<string, int>();

            bool allEmpty = false;

            while (!allEmpty)
            {
                int removeLocation = -1;

                foreach (KeyValuePair<string, List<int>> possibleFieldLocation in possibleFieldLocations)
                {
                    if (possibleFieldLocation.Value.Count == 1)
                    {
                        removeLocation = possibleFieldLocation.Value[0];
                        fieldLocations.Add(possibleFieldLocation.Key, possibleFieldLocation.Value[0]);
                        break;
                    }
                }

                foreach (KeyValuePair<string, List<int>> possibleFieldLocation in possibleFieldLocations)
                {
                    possibleFieldLocation.Value.Remove(removeLocation);
                }

                allEmpty = true;
                foreach (List<int> location in possibleFieldLocations.Values)
                {
                    if (location.Count > 0)
                    {
                        allEmpty = false;
                        break;
                    }
                }
            }

            long departureProduct = 1;

            foreach (KeyValuePair<string, int> fieldLocation in fieldLocations)
            {
                if (fieldLocation.Key.Contains("departure"))
                {
                    departureProduct *= yourTicket[fieldLocation.Value];
                }
            }

            Console.WriteLine($"The product of all of your departure fields is {departureProduct}.");

        }
    }
}
