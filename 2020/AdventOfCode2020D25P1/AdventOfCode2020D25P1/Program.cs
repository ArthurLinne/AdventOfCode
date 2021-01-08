using System;

namespace AdventOfCode2020D25P1
{
    class Program
    {
        static void Main(string[] args)
        {
            int cardPublicKey = 10943862;
            int doorPublicKey = 12721030;
            int baseSubject = 7;

            int mod = 20201227;
            int subject = baseSubject;
            long value = 1;

            int loopSize = 1;
            int cardLoopSize = -1;
            int doorLoopSize = -1;

            while (true)
            {
                value = (value * subject) % mod;

                if (value == cardPublicKey)
                {
                    cardLoopSize = loopSize;
                    if (doorLoopSize > 0)
                    {
                        break;
                    }
                }
                
                else if (value == doorPublicKey)
                {
                    doorLoopSize = loopSize;
                    if (cardLoopSize > 0)
                    {
                        break;
                    }
                }

                loopSize++;

            }

            value = 1;
            subject = cardPublicKey;
            loopSize = doorLoopSize;

            for (int i = 0; i < loopSize; i++)
            {
                value = (value * subject) % mod;
            }

            Console.WriteLine($"The encryption key is {value}.");
            

        }
    }
}
