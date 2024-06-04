using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Primes
{
    public static void WriteFactors(int num)
    {
        StringBuilder sb = new StringBuilder();

        // Находим простые множители числа num
        for (int i = 2; i <= num; i++)
        {
            int count = 0;
            while (num % i == 0)
            {
                count++;
                num /= i;
            }
            if (count > 0)
            {
                if (sb.Length > 0) sb.Append(" ");
                sb.Append(i);
                if (count > 1) sb.Append($"^{count}");
            }
        }
        Console.WriteLine(sb.ToString());
    }

    public static bool IsConcatenationPrime(int m, int n)
    {
        // Конкатенируем цифры m и n в новое число
        int concat = int.Parse(m.ToString() + n.ToString());

        return IsPrime(concat);
    }

    public static int FindGCD(int a, int b)
    {
        // Выполняем алгоритм Евклида для нахождения НОД
        while (b != 0)
        {
            int remainder = a % b;
            a = b;
            b = remainder;
        }
        return a;
    }

    public static bool IsPrime(int n)
    {
        if (n <= 1)
        {
            return false;
        }
        for (int i = 2; i <= Math.Sqrt(n); i++)
        {
            if (n % i == 0)
            {
                return false;
            }
        }
        return true;
    }

    public static void FindPrimes(int start, int end)
    {
        int count = 0;
        for (int i = start; i <= end; i++)
        {
            if (IsPrime(i))
            {
                Console.Write(i + " ");
                count++;
            }
        }
        Console.WriteLine("\nКоличество простых чисел в интервале от " +
            start + " до " + end + " = " + count);
    }
}
