bool flag = true;
int m = 354;
int n = 397;
while (flag)
{
    Console.WriteLine("\nВыберите задание: \n" +
                    "1 - Найти все простые числа в интервале [2,397] и подсчитать их количество.\n" +
                    "2 - Найти все простые числа в указанном интервале [354,397]. \n Сравнить полученные результаты с «ручными» вычислениями,используя «решето Эратосфена»\n" +
                    "3 - Записать числа m и n в виде произведения простых множителей, форма записи - каноническая\n" +
                    "4 - Проверить, является ли число, состоящее из конкатенации цифр m||n простым.\n" +
                    "5 - Найти НОД (m, n).\n" +
                    "6 - Выход\n"
                    );
    int choose_task = Convert.ToInt32(Console.ReadLine());

    switch (choose_task)
    {
        case 1:
            Console.WriteLine("Найдём простые числа от 2 до " + n + ":");
            Primes.FindPrimes(2, n);
            Console.WriteLine("n/ln(n):" + n / Math.Log(n, Math.E));
            Console.WriteLine("Округление:" + Math.Round(n / Math.Log(n, Math.E)) + "\n");
            break;
        case 2:
            Console.WriteLine("Найдём простые числа от " + m + " до " + n + ":");
            Primes.FindPrimes(m, n);
            Console.WriteLine("Найдём простые числа, используя решето Эратосфена:");
            Sieve.SieveEratosthenes(354, 397);
            break;
        case 3:
            Console.Write($"{m} = ");
            Primes.WriteFactors(m);
            Console.Write($"{n} = ");
            Primes.WriteFactors(n);
            Console.Write($"{18} = ");
            Primes.WriteFactors(18);
            break;
        case 4:
            if (Primes.IsConcatenationPrime(m, n)) Console.WriteLine("Да, число, состоящее из конкатенации m и n является простым.\n");
            else Console.WriteLine("Нет, число, состоящее из конкатенации m и n не является простым.\n");
            break;
        case 5:
            Console.WriteLine("НОД для чисел " + m + " и " + n + " равен " + Primes.FindGCD(m, n));
            Console.WriteLine("НОД для трёх чисел m, n и 245 равен " + Primes.FindGCD(Primes.FindGCD(m, n), 247) + "\n");
            break;
        case 6:
        default: flag = false; break;
    }
}