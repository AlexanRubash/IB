using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

class Schnorr
{
    private Random random = new Random();
    private BigInteger primeP = 48731, primeQ, generatorG, secretKeyX, publicKeyY;
    private byte[] hash;

    public Schnorr()
    {
        do
        {
            primeQ = Helper.GeneratePrimeNumberS();
        } while ((primeP - 1) % primeQ != 0);

        do
        {
            generatorG = random.Next(10000, 15000);
        } while (generatorG == 1 || BigInteger.ModPow(generatorG, (int)primeQ, primeP) != 1);

        do
        {
            secretKeyX = random.Next((int)primeQ);
        } while (secretKeyX >= primeQ);

        publicKeyY = Helper.ModInverse(BigInteger.ModPow(generatorG, secretKeyX, primeP), primeP);
        Console.WriteLine($"Открытый ключ: (p, q, g, y) = ({primeP}, {primeQ}, {generatorG}, {publicKeyY})");
        Console.WriteLine($"Закрытый ключ: (x) = ({secretKeyX})");
        Console.WriteLine();
    }

    public BigInteger[,] GenerateDigitalSignature(string message)
    {
        DateTime startTimeSchnorr = DateTime.Now;
        BigInteger randomK;
        do
        {
            randomK = random.Next();
        } while (!(randomK > 1 && randomK < primeQ));
        BigInteger r = BigInteger.ModPow(generatorG, randomK, primeP);
        message += r;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        }
        BigInteger[,] digitalSignature = new BigInteger[hash.Length, 2];
        for (int i = 0; i < hash.Length; i++)
        {
            digitalSignature[i, 0] = hash[i];
            digitalSignature[i, 1] = BigInteger.Add(randomK, BigInteger.Multiply(secretKeyX, hash[i])) % primeQ;
        }
        DateTime endTimeSchnorr = DateTime.Now;
        Console.WriteLine($"Цифровая подпись: {string.Join(" ", digitalSignature.Cast<BigInteger>())}");
        Console.WriteLine($"Время создания цифровой подписи: {(endTimeSchnorr - startTimeSchnorr).TotalMilliseconds} мс");
        return digitalSignature;
    }

    public bool VerifyDigitalSignature(string message, BigInteger[,] digitalSignature)
    {
        DateTime startVerifyTimeSchnorr = DateTime.Now;
        BigInteger x = BigInteger.Multiply(BigInteger.ModPow(generatorG, (int)digitalSignature[0, 1], primeP), BigInteger.ModPow(publicKeyY, (int)digitalSignature[0, 0], primeP)) % primeP;
        message += x;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
        }
        bool result = hash.SequenceEqual(Enumerable.Range(0, digitalSignature.GetLength(0)).Select(i => digitalSignature[i, 0].ToByteArray()[0]).ToArray());
        DateTime endVerifyTimeSchnorr = DateTime.Now;
        Console.WriteLine($"Результат проверки цифровой подписи: {result}");
        Console.WriteLine($"Время проверки цифровой подписи: {(endVerifyTimeSchnorr - startVerifyTimeSchnorr).TotalMilliseconds} мс\n");
        return result;
    }
}
