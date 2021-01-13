using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeD15P2
{
    public class MemoryGame
    {
        private Dictionary<int, int> gameHistory = new Dictionary<int, int>();

        private int currentNumber;
        private int currentTurn;

        public MemoryGame(List<int> gameSeed)
        {
            int lastIndex = gameSeed.Count - 1;

            for (int index = 0; index < lastIndex; index++)
            {
                gameHistory[gameSeed[index]] = index + 1;
            }

            currentNumber = gameSeed[lastIndex];
            currentTurn = lastIndex + 1;
        }

        private void PlayRound()
        {
            int nextNumber;

            if (gameHistory.TryGetValue(currentNumber, out int previousTurn))
            {
                nextNumber = currentTurn - previousTurn;
            }
            else
            {
                nextNumber = 0;
            }

            gameHistory[currentNumber] = currentTurn;

            currentNumber = nextNumber;
            currentTurn++;
        }

        public int PlayToTurn(int endingTurn)
        {
            while (currentTurn < endingTurn)
            {
                PlayRound();
            }

            return currentNumber;
        }

    }
}
