using System.Diagnostics;
using System.Text;

namespace ag
{
    class MainClass
    {
        static string SpiralRouteEncrypt(string input, int rows, int columns)
        {
            char[,] matrix = new char[rows, columns];
            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (index < input.Length)
                    {
                        matrix[i, j] = input[index];
                        index++;
                    }
                    else
                    {
                        matrix[i, j] = ' ';
                    }
                }
            }

            StringBuilder encryptedText = new StringBuilder();
            int topRow = 0, bottomRow = rows - 1, leftColumn = 0, rightColumn = columns - 1;
            while (topRow <= bottomRow && leftColumn <= rightColumn)
            {
                for (int i = topRow; i <= bottomRow; i++) // движение вниз
                {
                    encryptedText.Append(matrix[i, leftColumn]);
                }
                leftColumn++;

                for (int i = leftColumn; i <= rightColumn; i++) // движение вправо
                {
                    encryptedText.Append(matrix[bottomRow, i]);
                }
                bottomRow--;

                if (leftColumn <= rightColumn) // условие для предотвращения двойного прохода
                {
                    for (int i = bottomRow; i >= topRow; i--) // движение вверх
                    {
                        encryptedText.Append(matrix[i, rightColumn]);
                    }
                    rightColumn--;
                }

                if (topRow <= bottomRow) // условие для предотвращения двойного прохода
                {
                    for (int i = rightColumn; i >= leftColumn; i--) // движение влево
                    {
                        encryptedText.Append(matrix[topRow, i]);
                    }
                    topRow++;
                }
            }

            return encryptedText.ToString();
        }

        static string SpiralRouteDecrypt(string input, int rows, int columns)
        {
            char[,] matrix = new char[rows, columns];
            int index = 0;
            int length = input.Length;
            int totalCells = rows * columns;

            int topRow = 0, bottomRow = rows - 1, leftColumn = 0, rightColumn = columns - 1;
            while (index < length)
            {
                for (int i = topRow; i <= bottomRow && index < length; i++) // движение вниз
                {
                    matrix[i, leftColumn] = input[index];
                    index++;
                }
                leftColumn++;

                for (int i = leftColumn; i <= rightColumn && index < length; i++) // движение вправо
                {
                    matrix[bottomRow, i] = input[index];
                    index++;
                }
                bottomRow--;

                if (leftColumn <= rightColumn && index < length) // условие для предотвращения двойного прохода
                {
                    for (int i = bottomRow; i >= topRow && index < length; i--) // движение вверх
                    {
                        matrix[i, rightColumn] = input[index];
                        index++;
                    }
                    rightColumn--;
                }

                if (topRow <= bottomRow && index < length) // условие для предотвращения двойного прохода
                {
                    for (int i = rightColumn; i >= leftColumn && index < length; i--) // движение влево
                    {
                        matrix[topRow, i] = input[index];
                        index++;
                    }
                    topRow++;
                }
            }

            StringBuilder decryptedText = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    decryptedText.Append(matrix[i, j]);
                }
            }

            return decryptedText.ToString();
        }

        static string MultiplePermutationEncrypt(string plaintext, string keyword)
        {
            char[] keywordChars = keyword.ToLower().Where(char.IsLetter).Distinct().OrderBy(c => c).ToArray();
            Dictionary<char, int> keywordIndex = new Dictionary<char, int>();
            for (int i = 0; i < keywordChars.Length; i++)
            {
                keywordIndex[keywordChars[i]] = i;
            }

            int[] sortedIndexes = keywordChars.Select((c, index) => index).ToArray();

            Array.Sort(keywordChars, sortedIndexes);

            List<string> columns = new List<string>();

            for (int i = 0; i < keywordChars.Length; i++)
            {
                string column = "";
                for (int j = i; j < plaintext.Length; j += keywordChars.Length)
                {
                    column += plaintext[j];
                }
                columns.Add(column);
            }

            string ciphertext = "";
            foreach (int index in sortedIndexes)
            {
                ciphertext += columns[index];
            }

            return ciphertext;
        }

        static string MultiplePermutationDecrypt(string ciphertext, string keyword)
        {
            char[] keywordChars = keyword.ToLower().Where(char.IsLetter).Distinct().OrderBy(c => c).ToArray();

            Dictionary<char, int> keywordIndex = new Dictionary<char, int>();
            for (int i = 0; i < keywordChars.Length; i++)
            {
                keywordIndex[keywordChars[i]] = i;
            }

            int[] sortedIndexes = keywordChars.Select((c, index) => index).ToArray();

            Array.Sort(keywordChars, sortedIndexes);

            int columnLength = ciphertext.Length / keywordChars.Length;
            int remainder = ciphertext.Length % keywordChars.Length;

            Dictionary<int, int> columnLengths = new Dictionary<int, int>();
            for (int i = 0; i < keywordChars.Length; i++)
            {
                columnLengths[sortedIndexes[i]] = i < remainder ? columnLength + 1 : columnLength;
            }

            List<string> columns = new List<string>();

            int currentIndex = 0;
            foreach (var pair in columnLengths.OrderBy(x => x.Key))
            {
                columns.Add(ciphertext.Substring(currentIndex, pair.Value));
                currentIndex += pair.Value;
            }

            string[] sortedColumns = new string[keywordChars.Length];

            for (int i = 0; i < keywordChars.Length; i++)
            {
                sortedColumns[i] = columns[keywordIndex[keywordChars[i]]];
            }

            string plaintext = "";
            for (int i = 0; i < columnLength + (remainder > 0 ? 1 : 0); i++)
            {
                for (int j = 0; j < keywordChars.Length; j++)
                {
                    if (i < sortedColumns[j].Length)
                    {
                        plaintext += sortedColumns[j][i];
                    }
                }
            }

            return plaintext;
        }
      

        static void SpiralRouteEncryptDecrypt(string text, out string encrypt)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Маршрутная перестановка:");
            Console.ResetColor();

            int a = 24;
            int b = 39;

            // Замер времени шифрования
            Stopwatch stopwatch = Stopwatch.StartNew();
            string encryptedText = SpiralRouteEncrypt(text, a, b);
            stopwatch.Stop();
            Console.WriteLine($"Зашифрованный текст:\n {encryptedText}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Время шифрования: {stopwatch.ElapsedMilliseconds} мс");
            Console.ResetColor();
            encrypt = encryptedText;

            // Замер времени расшифрования
            stopwatch.Restart();
            string decryptedText = SpiralRouteDecrypt(encryptedText, a, b);
            stopwatch.Stop();
            Console.WriteLine($"Расшифрованный текст:\n {decryptedText}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Время расшифрования: {stopwatch.ElapsedMilliseconds} мс");
            Console.ResetColor();

        }
        static void MultiplePermutationEncryptDecrypt(string text, out string encrypt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Множественная перестановка:");
            Console.ResetColor();
            string keyword = "РУБАШКА";

            // Замер времени шифрования
            Stopwatch stopwatch = Stopwatch.StartNew();
            string encryptedText = MultiplePermutationEncrypt(text, keyword);
            stopwatch.Stop();
            Console.WriteLine($"Зашифрованный текст:\n {encryptedText}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Время шифрования:{stopwatch.ElapsedMilliseconds} мс");
            Console.ResetColor();
            encrypt = encryptedText;

            // Замер времени расшифрования
            stopwatch.Restart();
            string decryptedText = MultiplePermutationDecrypt(encryptedText, keyword);
            stopwatch.Stop();
            Console.WriteLine($"Расшифрованный текст:\n {decryptedText}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Время расшифрования: {stopwatch.ElapsedMilliseconds} мс");
            Console.ResetColor();

        }
        static void Main()
        {
            string text = File.ReadAllText("filerus.txt", Encoding.UTF8);

            string SpiralRouteEncryptedText;
            SpiralRouteEncryptDecrypt(text, out SpiralRouteEncryptedText);
            File.WriteAllText("decfile1.txt", SpiralRouteEncryptedText, Encoding.UTF8);
            string MultiplePermutationEncryptedText;
            MultiplePermutationEncryptDecrypt(text, out MultiplePermutationEncryptedText);
            File.WriteAllText("decfile2.txt", MultiplePermutationEncryptedText, Encoding.UTF8);

        }
    }
}
