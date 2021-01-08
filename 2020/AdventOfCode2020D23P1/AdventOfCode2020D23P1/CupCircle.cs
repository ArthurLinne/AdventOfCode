using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D23P1
{
    class CupCircle
    {
        private List<int> circleList;
        private int maxLabel;
        public int currentLabel;

        public CupCircle(List<int> circleList)
        {
            this.circleList = circleList;
            this.maxLabel = circleList.Max();
            this.currentLabel = circleList[0];
        }

        public void OrientByLabel(int label)
        {
            int currentIndex = circleList.IndexOf(label);

            if (currentIndex == 0)
            {
                return;
            }

            List<int> beforeLabel = circleList.GetRange(0, currentIndex);
            circleList.RemoveRange(0, currentIndex);
            circleList.AddRange(beforeLabel);
        }

        public void PlayRound()
        {
            List<int> pickedUp = this.circleList.GetRange(1, 3);
            this.circleList.RemoveRange(1, 3);

            int nextLabel = currentLabel - 1;

            while (!this.circleList.Contains(nextLabel))
            {
                if (nextLabel == 0)
                {
                    nextLabel = maxLabel;
                }

                else
                {
                    nextLabel--;
                }
            }

            this.circleList.InsertRange(this.circleList.IndexOf(nextLabel) + 1, pickedUp);

            if (this.circleList.IndexOf(currentLabel) + 1 == this.circleList.Count)
            {
                currentLabel = this.circleList[0];
            }

            else
            {
                currentLabel = circleList[circleList.IndexOf(currentLabel) + 1];
            }

            this.OrientByLabel(currentLabel);
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

        public void PrintCircleFinal()
        {
            this.OrientByLabel(1);

            foreach (int label in circleList.GetRange(1, circleList.Count - 1))
            {
                Console.Write(label);
            }
        }

    }
}
