using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D22P1
{
    class Player
    {
        private string playerName;
        private List<int> playerDeck;

        public Player(string playerName, List<int> playerDeck)
        {
            this.playerName = playerName;
            this.playerDeck = playerDeck;
        }

        public void PlayRound(Player opponent)
        {
            int myCard = this.PlayTopCard();
            int oppCard = opponent.PlayTopCard();

            if (myCard > oppCard)
            {
                this.AddToBottom(myCard);
                this.AddToBottom(oppCard);
            }

            else if (oppCard > myCard)
            {
                opponent.AddToBottom(oppCard);
                opponent.AddToBottom(myCard);
            }
            else
            {
                Console.WriteLine("There was a tie. That shouldn't be possible!");
            }

        }

        public int PlayTopCard()
        {
            int topCard = playerDeck[0];

            this.playerDeck.RemoveAt(0);

            return topCard;
        }

        public void AddToBottom(int card)
        {
            this.playerDeck.Add(card);
        }

        public bool CheckForDefeat()
        {
            if (this.playerDeck.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public long WriteScore()
        {
            long score = 0;

            List<int> backwardDeck = this.playerDeck;

            backwardDeck.Reverse();

            for (int deckIndex = 0; deckIndex < backwardDeck.Count; deckIndex++)
            {
                score += backwardDeck[deckIndex] * (deckIndex + 1);
            }

            Console.WriteLine($"{this.playerName} scored {score}.");

            return score;
        }

        public void PrintDeck()
        {
            Console.WriteLine($"{playerName} deck from top to bottom:");

            foreach (int card in this.playerDeck)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine();
        }

    }
}
