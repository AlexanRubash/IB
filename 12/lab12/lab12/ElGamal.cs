using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

class ElGamal
{
    private Random random = new Random();
    private BigInteger primeP, generatorG, privateKeyX, publicKeyY;
    private byte[] hash;

    public ElGamal()
    {
        primeP = Helper.GeneratePrimeNumber();
        generatorG = Helper.GenerateCoprimeNumber(primeP);
        privateKeyX = random.Next(2, (int)primeP);
        publicKeyY = BigInteger.ModPow(generatorG, privateKeyX, primeP);
        Console.WriteLine($"Открытый ключ: (p, g, y) = ({primeP}, {generatorG}, {publicKeyY})");
        Console.WriteLine($"Закрытый ключ: (p, g, x) = ({primeP}, {generatorG}, {privateKeyX})");
        Console.WriteLine();
    }


    public BigInteger[,] CreateDigitalSignature(string message)
    {
        DateTime startTimeElGamal = DateTime.Now;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        }
        BigInteger[,] digitalSignature = new BigInteger[hash.Length, 2];
        for (int i = 0; i < hash.Length; i++)
        {
            do
            {
                BigInteger k = random.Next(2, (int)primeP - 2);
                while (Helper.GetGCD(k, primeP - 1) != 1)
                {
                    k = random.Next(2, (int)primeP - 2);
                }
                digitalSignature[i, 0] = BigInteger.ModPow(generatorG, k, primeP);
                BigInteger temp = BigInteger.Multiply(BigInteger.Subtract(hash[i], BigInteger.Multiply(privateKeyX, digitalSignature[i, 0])), Helper.ModInverse(k, primeP - 1));
                digitalSignature[i, 1] = temp < 0 ? (primeP - 1) - BigInteger.ModPow(BigInteger.Negate(temp), 1, primeP - 1) : BigInteger.ModPow(temp, 1, primeP - 1);
            } while (digitalSignature[i, 1] == 0);
        }
        DateTime endTimeElGamal = DateTime.Now;
        Console.WriteLine($"Цифровая подпись: {string.Join(" ", digitalSignature.Cast<BigInteger>())}");
        Console.WriteLine($"Время создания цифровой подписи: {(endTimeElGamal - startTimeElGamal).TotalMilliseconds} мс");
        return digitalSignature;
    }

    public bool VerifyDigitalSignature(string message, BigInteger[,] digitalSignature)
    {
        DateTime startVerifyTimeElGamal = DateTime.Now;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        }
        bool result = true;
        for (int i = 0; i < digitalSignature.GetUpperBound(0) + 1; i++)
        {
            BigInteger leftPart = BigInteger.ModPow(generatorG, hash[i], primeP);
            BigInteger rightPart = BigInteger.ModPow(BigInteger.Multiply(BigInteger.Pow(publicKeyY, (int)digitalSignature[i, 0]), BigInteger.Pow(digitalSignature[i, 0], (int)digitalSignature[i, 1])), 1, primeP);
            bool compareResult = leftPart == rightPart;
            result = result && compareResult;
        }
        DateTime endVerifyTimeElGamal = DateTime.Now;
        Console.WriteLine($"Результат проверки цифровой подписи: {result}");
        Console.WriteLine($"Время проверки цифровой подписи: {(endVerifyTimeElGamal - startVerifyTimeElGamal).TotalMilliseconds} мс\n");
        return result;
    }
}
