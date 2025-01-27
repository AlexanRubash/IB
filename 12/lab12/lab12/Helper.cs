﻿using System;
using System.Numerics;

class Helper
{
    private static Random random = new Random();

    public static bool IsPrime(BigInteger n)
    {
        if (n <= 1)
            return false;

        if (n == 2 || n == 3)
            return true;

        if (n % 2 == 0 || n % 3 == 0)
            return false;

        for (BigInteger i = 5; i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        }

        return true;
    }

    public static BigInteger GetGCD(BigInteger a, BigInteger b)
    {
        while (b != 0)
        {
            BigInteger temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        if (m == 1)
            return 0;

        BigInteger m0 = m;
        BigInteger y = 0;
        BigInteger x = 1;

        while (a > 1)
        {
            BigInteger q = a / m;
            BigInteger t = m;

            m = a % m;
            a = t;
            t = y;

            y = x - q * y;
            x = t;
        }

        if (x < 0)
            x += m0;

        return x;
    }
    public static BigInteger GeneratePrimeNumber()
    {
        BigInteger number = random.Next(1000, 5000);
        while (!IsPrime(number))
        {
            number = random.Next(1000, 5000);
        }
        return number;
    }
    public static BigInteger GeneratePrimeNumberS()
    {
        BigInteger number;
        do
        {
            number = random.Next(100, 500);
        } while (!IsPrime(number));
        return number;
    }

    public static BigInteger GenerateCoprimeNumber(BigInteger coprime)
    {
        BigInteger number = random.Next(2, (int)coprime - 1), nod = Helper.GetGCD(number, coprime);
        while (nod != 1)
        {
            number = random.Next(2, (int)coprime - 1);
            nod = Helper.GetGCD(number, coprime);
        }
        return number;
    }
}
