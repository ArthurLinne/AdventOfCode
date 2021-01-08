using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D22P2
{
    class Player
    {
        private string playerName;
        private List<int> playerDeck;
        private List<List<int>> playerDeckHistory;
        private int gameDepth;

        public Player(string playerName, List<int> playerDeck):
            this(playerName, playerDeck, 0)
        {
        }
        
        public Player(string playerName, List<int> playerDeck, int gameDepth)
        {
            this.playerName = playerName;
            this.playerDeck = playerDeck;
            this.gameDepth = gameDepth;

            this.playerDeckHistory = new List<List<int>>();
        }

        public Player PlayFullGame(Player opponent)
        {
            //Console.WriteLine($"Game Depth: {this.gameDepth}");

            while (true)
            {
                //this.PrintDeck();
                //opponent.PrintDeck();

                if (this.CheckForRepeatedGameState() && opponent.CheckForRepeatedGameState())
                {
                    //Console.WriteLine($"{this.playerName} wins by repeated game state!");
                    if (this.gameDepth == 0)
                    {
                        this.WriteScore();
                    }
                    return this;
                }

                this.AddCurrentDeckToHistory();
                opponent.AddCurrentDeckToHistory();

                this.PlayRound(opponent);

                if (this.CheckForDefeat())
                {
                    //Console.WriteLine($"{opponent.playerName} wins by exhaustion!");
                    if (this.gameDepth == 0)
                    {
                        opponent.WriteScore();
                    }
                    return opponent;
                }

                if (opponent.CheckForDefeat())
                {
                    //Console.WriteLine($"{this.playerName} wins by exhaustion!");
                    if (this.gameDepth == 0)
                    {
                        this.WriteScore();
                    }
                    return this;
                }
            }
        }

        public void PlayRound(Player opponent)
        {
            int myCard = this.PlayTopCard();
            //Console.WriteLine($"{this.playerName} plays {myCard}.");
            
            int oppCard = opponent.PlayTopCard();
            //Console.WriteLine($"{opponent.playerName} plays {oppCard}.");

            if (this.CheckForRecursion(myCard) && opponent.CheckForRecursion(oppCard))
            {
                Player subGamePlayer1 = new Player(this.playerName, this.playerDeck.GetRange(0, myCard), this.gameDepth + 1);
                Player subGamePlayer2 = new Player(opponent.playerName, opponent.playerDeck.GetRange(0, oppCard), opponent.gameDepth + 1);

                Player winner = subGamePlayer1.PlayFullGame(subGamePlayer2);

                if (winner == subGamePlayer1)
                {
                    this.AddToBottom(myCard);
                    this.AddToBottom(oppCard);
                }

                else if (winner == subGamePlayer2)
                {
                    opponent.AddToBottom(oppCard);
                    opponent.AddToBottom(myCard);
                }
            }

            else if (myCard > oppCard)
            {
                //Console.WriteLine($"{this.playerName} wins!");
                this.AddToBottom(myCard);
                this.AddToBottom(oppCard);
            }

            else if (oppCard > myCard)
            {
                //Console.WriteLine($"{opponent.playerName} wins!");
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

        public void AddCurrentDeckToHistory()
        {
            this.playerDeckHistory.Add(new List<int>(this.playerDeck));
        }

        

        public long WriteScore()
        {
            long score = 0;

            List<int> backwardDeck = new List<int>(this.playerDeck);

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
            Console.Write($"{playerName}'s deck: ");

            foreach (int card in this.playerDeck)
            {
                Console.Write($"{card}, ");
            }
            Console.WriteLine();
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

        public bool CheckForRecursion(int playedCard)
        {
            if (playedCard <= this.playerDeck.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckForRepeatedGameState()
        {
            foreach (List<int> oldDeck in this.playerDeckHistory)
            {
                if (Enumerable.SequenceEqual(oldDeck.OrderBy(e => e), this.playerDeck.OrderBy(e => e)))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
