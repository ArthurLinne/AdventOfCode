using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D23P2
{
    class CupCircle
    {
        private int PICKUP_SIZE = 3;

        public int[] circleList;
        private int maxLabel;        

        public CupCircle(int[] circleInput, int sizeGoal)
        {
            maxLabel = sizeGoal;
            this.circleList = new int[sizeGoal];

            for (int i = 0; i < sizeGoal; i++)
            {
                this.circleList[i] = i + 1;
            }

            for (int inputIndex = 0; inputIndex < circleInput.Length; inputIndex++)
            {
                this.circleList[inputIndex] = circleInput[inputIndex];
            }

        }

        public void PlayRound()
        {
            int currentCup = this.circleList[0];
            int prevCup = currentCup - 1;
            int[] pickedUp = new int[PICKUP_SIZE];

            for (int i = 0; i < pickedUp.Length; i++)
            {
                pickedUp[i] = this.circleList[i + 1];
            }

            while (pickedUp.Contains(prevCup) || prevCup == 0)
            {
                if (prevCup == 0)
                {
                    prevCup = maxLabel;
                }
                else
                {
                    prevCup--;
                }
            }

            int oldInsertIndex = Array.IndexOf(this.circleList, prevCup) + 1;
            int newInsertIndex = oldInsertIndex - 4;

            int[] newCircleList = new int[circleList.Length];

            for (int i = 4; i < maxLabel; i++)
            {
                if (i < oldInsertIndex)
                {
                    newCircleList[i - 4] = circleList[i];
                }
                else
                {
                    newCircleList[i - 1] = circleList[i];
                }
            }

            for (int i = 0; i < PICKUP_SIZE; i++)
            {
                newCircleList[newInsertIndex + i] = pickedUp[i];
            }

            newCircleList[maxLabel - 1] = circleList[0];

            circleList = newCircleList;
        }

        public void PlayMultipleRounds(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                this.PlayRound();
            }
        }

        public void PrintCircle()
        {
            foreach (int label in circleList)
            {
                Console.Write($"{label} ");
            }
        }

        public void PrintAbbreviatedCircle(int length)
        {
            Console.WriteLine($"Abbreviated Circle for {length}");
            for (int i = 0; i < length; i++)
            {
                Console.Write($"{circleList[i]}, ");
            }
            Console.WriteLine("...");

            for (int i = maxLabel - length; i < maxLabel; i++)
            {
                Console.Write($"{circleList[i]}, ");
            }
            Console.WriteLine();
        }

        public void PrintCircleFinal()
        {
            int indexOne = Array.IndexOf(this.circleList, 1);

            int nextIndex = indexOne + 1;

            if (nextIndex == maxLabel)
            {
                nextIndex = 0;
            }

            int nextNextIndex = nextIndex + 1;

            if (nextNextIndex == maxLabel)
            {
                nextNextIndex = 0;
            }


            long finalProduct = circleList[nextIndex] * circleList[nextNextIndex];
            Console.WriteLine(finalProduct);
        }

    }
}
