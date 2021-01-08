using System;
using System.Collections.Generic;

namespace AdventOfCode2020D22P2
{
    class Program
    {
        static void Main()
        {
            string[] deckListInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D22P1\DeckLists.txt");

            bool addingPlayer1 = false;
            bool addingPlayer2 = false;
            List<int> player1Deck = new List<int>();
            List<int> player2Deck = new List<int>();


            foreach (string line in deckListInput)
            {
                if (line == "Player 1:")
                {
                    addingPlayer1 = true;
                }

                else if (line == "Player 2:")
                {
                    addingPlayer2 = true;
                }

                else if (line == "")
                {
                    addingPlayer1 = false;
                }

                else if (addingPlayer1)
                {
                    player1Deck.Add(Int32.Parse(line));
                }

                else if (addingPlayer2)
                {
                    player2Deck.Add(Int32.Parse(line));
                }
            }

            Player player1 = new Player("Player 1", player1Deck);
            Player player2 = new Player("Player 2", player2Deck);

            player1.PlayFullGame(player2);

        }
    }
}
