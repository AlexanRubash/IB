using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

class RSA
{
    private BigInteger p, q, n, fi, e, d;
    private byte[] hash;

    public RSA()
    {
        p = Helper.GeneratePrimeNumber();
        q = Helper.GeneratePrimeNumber();
        n = p * q;
        fi = (p - 1) * (q - 1);
        e = Helper.GenerateCoprimeNumber(fi);
        d = Helper.ModInverse(e, fi);
        Console.WriteLine($"Открытый ключ: (e, n) = ({e}, {n})");
        Console.WriteLine($"Закрытый ключ: (d, n) = ({d}, {n})");
    }

    public BigInteger[] CreateDigitalSignature(string text)
    {
        DateTime startTimeRSA = DateTime.Now;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
        }
        BigInteger[] digitalSign = new BigInteger[hash.Length];
        for (int i = 0; i < hash.Length; i++)
        {
            digitalSign[i] = BigInteger.ModPow(hash[i], d, n);
        }
        DateTime endTimeRSA = DateTime.Now;
        Console.WriteLine($"Цифровая подпись: {string.Join(" ", digitalSign)}");
        Console.WriteLine($"Время создания цифровой подписи: {(endTimeRSA - startTimeRSA).TotalMilliseconds} мс");
        return digitalSign;
    }

    public bool VerifyDigitalSignature(string text, BigInteger[] digitalSign)
    {
        DateTime startVerifyTimeRSA = DateTime.Now;
        byte[] signBytes = new byte[digitalSign.Length];
        for (int i = 0; i < digitalSign.Length; i++)
        {
            signBytes[i] = (byte)BigInteger.ModPow(digitalSign[i], e, n);
        }
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] receivedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            bool result = VerifyByteArrays(receivedHash, signBytes);
            DateTime endVerifyTimeRSA = DateTime.Now;
            Console.WriteLine($"Результат проверки цифровой подписи: {result}");
            Console.WriteLine($"Время проверки цифровой подписи: {(endVerifyTimeRSA - startVerifyTimeRSA).TotalMilliseconds} мс\n");
            return result;
        }
    }

    private bool VerifyByteArrays(byte[] arr1, byte[] arr2)
    {
        if (arr1.Length != arr2.Length)
            return false;

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }

        return true;
    }
}
