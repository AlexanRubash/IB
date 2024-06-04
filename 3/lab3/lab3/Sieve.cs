using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Sieve
{
    public static void SieveEratosthenes(int start, int end)
    {
        bool[] isPrime = new bool[end - start + 1];
        for (int i = 0; i < isPrime.Length; i++)
        {
            isPrime[i] = true;
        }

        // Отмечаем числа, которые не являются простыми
        for (int i = 2; i <= Math.Sqrt(end); i++)
        {
            for (int j = Math.Max(i * i, ((start - 1) / i + 1) * i); j <= end; j += i)
            {
                isPrime[j - start] = false;
            }
        }

        // Выводим найденные простые числа
        for (int i = start; i <= end; i++)
        {
            if (isPrime[i - start])
            {
                Console.Write(i + " ");
            }
        }
        Console.WriteLine();
    }
}
