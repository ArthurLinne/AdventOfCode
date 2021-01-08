// A C# program to demonstrate 
// working of Chinese remainder 
// Theorem 
using System;

class GFG
{
    // Returns modulo inverse of 
    // 'a' with respect to 'm' 
    // using extended Euclid Algorithm. 
    // Refer below post for details: 
    // https://www.geeksforgeeks.org/multiplicative-inverse-under-modulo-m/ 
    static ulong inv(ulong a, ulong m)
    {
        ulong m0 = m, t, q;
        ulong x0 = 0, x1 = 1;

        if (m == 1)
            return 0;

        // Apply extended 
        // Euclid Algorithm 
        while (a > 1)
        {
            // q is quotient 
            q = a / m;

            t = m;

            // m is remainder now, 
            // process same as 
            // euclid's algo 
            m = a % m; a = t;

            t = x0;

            x0 = x1 - q * x0;

            x1 = t;
        }

        // Make x1 positive 
        if (x1 < 0)
            x1 += m0;

        return x1;
    }

    // k is size of num[] and rem[]. 
    // Returns the smallest number 
    // x such that: 
    // x % num[0] = rem[0], 
    // x % num[1] = rem[1], 
    // .................. 
    // x % num[k-2] = rem[k-1] 
    // Assumption: Numbers in num[] 
    // are pairwise coprime (gcd 
    // for every pair is 1) 
    static ulong findMinX(ulong[] num,
                        ulong[] rem,
                        ulong k)
    {
        // Compute product 
        // of all numbers 
        ulong prod = 1;
        for (ulong i = 0; i < k; i++)
            prod *= num[i];

        // Initialize result 
        ulong result = 0;

        // Apply above formula 
        for (ulong i = 0; i < k; i++)
        {
            ulong pp = prod / num[i];
            result += rem[i] *
                    inv(pp, num[i]) * pp;
        }

        return result % prod;
    }

    // Driver Code 
    static public void Main()
    {
        ulong[] num = { 17, 41, 37, 367, 19, 23, 29, 613, 13 };
        ulong[] rem = { 0, 7, 11, 17, 17, 17, 17, 48, 10 };
        ulong k = (ulong)num.Length;
        Console.WriteLine("x is " +
                        findMinX(num, rem, k));
    }
}

// This code is contributed 
// by ajit 