﻿using System;
using System.Linq;

namespace ChineseRemainderTheorem
{
    public static class ChineseRemainderTheorem
    {
        public static int Solve(int[] n, int[] a)
        {
            int prod = n.Aggregate(1, (i, j) => i * j);
            int p;
            int sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static int ModularMultiplicativeInverse(int a, int mod)
        {
            int b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}