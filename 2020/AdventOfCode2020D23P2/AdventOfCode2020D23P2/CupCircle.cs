﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AdventOfCode2020D23P2
{
    class CupCircle
    {
        private int PICKUP_SIZE = 3;

        private int[] circleIndexToLabel;
        private int[] circleLabelToIndex;

        private int circleSize;    

        public CupCircle(int[] circleInput, int sizeGoal)
        {
            circleSize = sizeGoal;

            circleIndexToLabel = new int[sizeGoal];
            circleLabelToIndex = new int[sizeGoal + 1];


            for (int i = 0; i < sizeGoal; i++)
            {
                circleIndexToLabel[i] = i + 1;
                circleLabelToIndex[i + 1] = i;
            }

            for (int inputIndex = 0; inputIndex < circleInput.Length; inputIndex++)
            {
                circleIndexToLabel[inputIndex] = circleInput[inputIndex];
                circleLabelToIndex[circleInput[inputIndex]] = inputIndex;
            }
        }

        public void PlayRound(int iteration)
        {
            int currentCup = circleIndexToLabel[0];
            int prevCup = currentCup - 1;
            int[] pickedUp = new int[PICKUP_SIZE];

            for (int i = 0; i < pickedUp.Length; i++)
            {
                pickedUp[i] = circleIndexToLabel[i + 1];
            }

            while (pickedUp.Contains(prevCup) || prevCup == 0)
            {
                if (prevCup == 0)
                {
                    prevCup = circleSize;
                }
                else
                {
                    prevCup--;
                }
            }

            int oldInsertIndex = circleLabelToIndex[prevCup] + 1;
            int newInsertIndex = oldInsertIndex - 4;

            //Console.WriteLine($"Iteration {iteration}: {(oldInsertIndex - 1 + iteration) % circleSize}");

            for (int i = 4; i < circleSize; i++)
            {
                if (i < oldInsertIndex)
                {
                    //AddCupIndexLabel(i - 4, circleIndexToLabel[i]);
                    circleIndexToLabel[i - 4] = circleIndexToLabel[i];
                    circleLabelToIndex[circleIndexToLabel[i]] = i - 4;
                }
                else
                {
                    //AddCupIndexLabel(i - 1, circleIndexToLabel[i]);
                    circleIndexToLabel[i - 1] = circleIndexToLabel[i];
                    circleLabelToIndex[circleIndexToLabel[i]] = i - 1;
                }
            }

            for (int i = 0; i < PICKUP_SIZE; i++)
            {
                //AddCupIndexLabel(newInsertIndex + i, pickedUp[i]);
                circleIndexToLabel[newInsertIndex + i] = pickedUp[i];
                circleLabelToIndex[pickedUp[i]] = newInsertIndex + i;
            }

            //AddCupIndexLabel(circleSize - 1, currentCup);
            circleIndexToLabel[circleSize - 1] = currentCup;
            circleLabelToIndex[currentCup] = circleSize - 1;
        }

        public void PlayMultipleRounds(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                this.PlayRound(i);
                //this.PrintCircle();
            }
        }

        private void AddCupIndexLabel(int index, int label)
        {
            circleIndexToLabel[index] = label;
            circleLabelToIndex[label] = index;
        }

        public void PrintCircle()
        {
            for (int index = 0; index < circleSize; index++)
            {
                Console.Write($"{circleIndexToLabel[index]} ");
            }
            Console.WriteLine();
        }

        public void PrintAbbreviatedCircle(int length)
        {
            Console.WriteLine($"Abbreviated Circle for {length}");
            for (int i = 0; i < length; i++)
            {
                Console.Write($"{circleIndexToLabel[i]}, ");
            }
            Console.WriteLine("...");

            for (int i = circleSize - length; i < circleSize; i++)
            {
                Console.Write($"{circleIndexToLabel[i]}, ");
            }
            Console.WriteLine();
        }

        public void PrintCircleFinal()
        {
            int indexOne = circleLabelToIndex[1];

            int nextIndex = indexOne + 1;

            if (nextIndex == circleSize)
            {
                nextIndex = 0;
            }

            int nextNextIndex = nextIndex + 1;

            if (nextNextIndex == circleSize)
            {
                nextNextIndex = 0;
            }


            long finalProduct = (long)circleIndexToLabel[nextIndex] * (long)circleIndexToLabel[nextNextIndex];
            Console.WriteLine($"{circleIndexToLabel[nextIndex]} * {circleIndexToLabel[nextNextIndex]} = {finalProduct}");
            // 907135 * 401051 = 363807398885
        }

    }
}
