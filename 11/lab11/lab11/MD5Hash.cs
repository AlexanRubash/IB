using System;
using System.Security.Cryptography;
using System.Text;

public class MD5Hash
{
    public static string CalculateMD5Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = new byte[md5.HashSize / 8];

            if (md5.TryComputeHash(inputBytes, hashBytes, out int bytesWritten))
            {
                StringBuilder builder = new StringBuilder(hashBytes.Length * 2);

                foreach (byte hashByte in hashBytes)
                {
                    builder.AppendFormat("{0:x2}", hashByte);
                }

                return builder.ToString();
            }
            else
            {
                throw new InvalidOperationException("Failed to compute MD5 hash.");
            }
        }
    }
}
