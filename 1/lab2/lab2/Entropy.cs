using System.Text;
using System.IO;

public class Entropy
{
    public static double[] CalculateSymbolProbabilities(string text, char[] alphabet)
    {
        int textLength = text.Length;

        Dictionary<char, int> charCount = new Dictionary<char, int>();

        // Считаем количество вхождений каждого символа
        foreach (char c in text)
        {
            if (charCount.ContainsKey(c))
            {
                charCount[c]++;
            }
            else
            {
                charCount[c] = 1;
            }
        }

        double[] probabilities = new double[alphabet.Length];
        // Рассчитываем вероятности
        for (int i = 0; i < alphabet.Length; i++)
        {
            char c = alphabet[i];
            if (charCount.ContainsKey(c))
            {
                int count = charCount[c];
                probabilities[i] = (double)count / textLength;
            }
            else
            {
                probabilities[i] = 0;
            }
        }

        return probabilities;
    }


    public static void PrintSymbolProbabilities(double[] probabilities, char[] alphabet)
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            Console.WriteLine($"Символ '{alphabet[i]}': Вероятность появления = {probabilities[i]}");
        }
    }

    public static string ReadTextFromFile(string filePath)//для 3ей лабы
    {
        try
        {
            // Чтение текста из файла
            string text = File.ReadAllText(filePath);
            return text;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            return null;
        }
    }
    public static double CalculateShannonEntropy(string text, char[] alphabet)
    {
        int[] charCount = new int[alphabet.Length];
        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            char c = text[i];
            int index = Array.IndexOf(alphabet, c);
            if (index != -1)
            {
                charCount[index]++;
            }
        }

        double result = 0;
        for (int i = 0; i < alphabet.Length; i++)
        {
            int count = charCount[i];
            double probability = (double)count / textLength;
            if (probability != 0)
            {
                result += probability * Math.Log(probability, 2);
//                Console.WriteLine(-probability * Math.Log(probability, 2));
            }
        }

        double entropy = -result;
        Console.WriteLine("Энтропия: {0}", entropy);
        return entropy;
    }

    public static double CalculateShannonEntropy1(string text, char[] alphabet)
    {
        int[] charCount = new int[alphabet.Length];
        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            char c = text[i];
            int index = Array.IndexOf(alphabet, c);
            if (index != -1)
            {
                charCount[index]++;
            }
        }

        double result = 0;
        for (int i = 0; i < alphabet.Length; i++)
        {
            int count = charCount[i];
            double probability = (double)count / textLength;
            if (probability != 0)
            {
                result += probability * Math.Log(probability, 2);
            }
        }

        double entropy = -result;
        return entropy;
    }

    public static string Binary(string text)
    {
        byte[] buf = Encoding.ASCII.GetBytes(text);
        char[] binaryChars = new char[buf.Length * 8];
        int index = 0;

        foreach (byte b in buf)
        {
            int bitIndex = 7;
            while (bitIndex >= 0)
            {
                binaryChars[index] = ((b >> bitIndex) & 1) == 1 ? '1' : '0';
                index++;
                bitIndex--;
            }
        }

        string binaryStr = new string(binaryChars);
        return binaryStr;
    }

    public static double CountInformation(string text, double entropy)
    {
        text = text.Replace(" ", "");
        return text.Length * entropy;
    }

    public static double WithError(double error)
{
    double puk = (double)(1 - (-error * Math.Log(error, 2) - (1 - error) * Math.Log((1 - error), 2)));
    return puk;
}

    public static double HartleyEntropy(char[] alphabet)
    {
        double entropy = Math.Log(alphabet.Length,2);
        return entropy;
    }


}
