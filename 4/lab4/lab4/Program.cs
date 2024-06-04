using System.Diagnostics;
using System.Text;

namespace lab4
{
    class Program
    {
        private static readonly string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private static readonly int rows = 4, cols = 8;

        static void Main(string[] args)
        {
            bool flag=true;
            while (flag)
            {
                Console.WriteLine("\nВыберете задание");
                Console.WriteLine("1-шифр Цезаря, 2-дешифр Цезаря, 3-Частота у цезаря, 4-Трисемус, 6-выход");
                int chooseTask;
                string inputFile = "D:\\Univer\\IB\\4\\lab4\\text.txt";
                string encryptedFile = "D:\\Univer\\IB\\4\\lab4\\caesar-encoded.txt";
                string decryptedFile = "D:\\Univer\\IB\\4\\lab4\\caesar-decoded.txt";
                string key = "безопасность";
                int.TryParse(Console.ReadLine(), out chooseTask);
                Stopwatch stopwatch = new Stopwatch();
                switch (chooseTask)
                {
                case 1:
                stopwatch.Start();
                EncryptCaesar(inputFile, encryptedFile, key);
                stopwatch.Stop();
                Console.WriteLine("\nEncryptCaesar занял {0} мс ", stopwatch.ElapsedMilliseconds);                        
                break;
                case 2:
                stopwatch.Reset();
                stopwatch.Start();
                DecryptCaesar(encryptedFile, decryptedFile, key);
                stopwatch.Stop();
                Console.WriteLine("\nDecryptCaesar занял {0} мс ", stopwatch.ElapsedMilliseconds);
                break;
                case 3:
                Console.WriteLine("Частота появления символов в тексте, зашифрованном шифром Цезаря:");
                CountCharacterFrequency(File.ReadAllText(encryptedFile));
                Console.WriteLine("Частота появления символов в расшифрованном тексте:");
                CountCharacterFrequency(File.ReadAllText(decryptedFile));
                break; 
                case 4:
                // Шифрование с помощью таблицы Трисемуса
                encryptedFile = "D:\\Univer\\IB\\4\\lab4\\trisemus-encoded.txt";
                decryptedFile = "D:\\Univer\\IB\\4\\lab4\\trisemus-decoded.txt";
                Console.WriteLine("Таблица Трисемуса");
                var table = TrisemusTable(key);
                PrintTrisemusTable(table);
                stopwatch.Reset();
                stopwatch.Start();
                EncryptTrisemus(inputFile, encryptedFile, table);
                stopwatch.Stop();
                        long encodTime = stopwatch.ElapsedMilliseconds;
               
                
                stopwatch.Reset();
                stopwatch.Start();
                DecryptTrisemus(encryptedFile, decryptedFile, table);
                stopwatch.Stop();
                        Console.WriteLine("\nEncryptTrisemus занял {0} мс ", encodTime);
                        Console.WriteLine("\nDecryptTrisemus занял {0} мс ", stopwatch.ElapsedMilliseconds);
                
                Console.WriteLine("Частота появления символов в тексте, зашифрованном шифром Трисемуса:");
                CountCharacterFrequency(File.ReadAllText(encryptedFile));
                Console.WriteLine("Частота появления символов в расшифрованном тексте:");
                CountCharacterFrequency(File.ReadAllText(decryptedFile));
                break;
                    case 6:
                    default:
                        flag = false;
                        break;
                }               
            }
        }
        
        public static void PrintTrisemusTable(char[,] table)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            int rows = table.GetLength(0);
            int cols = table.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(table[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static void EncryptCaesar(string inputFile, string encryptedFile, string key)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string alpha = GenerateAlphabet(key);
            string inputText = File.ReadAllText(inputFile);
            string encryptedText = "";

            foreach (char c in inputText)
            {
                // Переводим символ в нижний регистр
                char lowercaseChar = char.ToLower(c);

                int index = Alphabet.IndexOf(lowercaseChar);

                if (char.IsLetter(c))
                {
                    // Если символ найден в алфавите, получаем соответствующий зашифрованный символ
                    char encryptedChar = alpha[index];

                    // Проверяем оригинальный регистр символа и переводим зашифрованный символ в тот же регистр
                    if (char.IsUpper(c))
                    {
                        encryptedChar = char.ToUpper(encryptedChar);
                    }

                    encryptedText += encryptedChar;
                }
                else
                {
                    // Если символ не найден в алфавите, добавляем его как есть
                    encryptedText += c;
                }
            }

            Console.WriteLine("Зашифрованный текст:\n---------------\n" + encryptedText);
            File.WriteAllText(encryptedFile, encryptedText);
            Console.ResetColor();

        }


        static string GenerateAlphabet(string keyword)
        {
            StringBuilder alphabet = new StringBuilder();

            foreach (char c in keyword)
            {
                char lowercaseChar = char.ToLower(c);

                if (!char.IsLetter(lowercaseChar) || alphabet.ToString().Contains(lowercaseChar))
                {
                    continue;
                }

                alphabet.Append(lowercaseChar);
            }

            // Добавляем символ "ё" в алфавит, если его нет
            if (!alphabet.ToString().Contains('ё'))
            {
                alphabet.Append('ё');
            }

            for (char c = 'а'; c <= 'я'; c++)
            {
                if (!alphabet.ToString().Contains(c))
                {
                    alphabet.Append(c);
                }
            }

            return alphabet.ToString();
        }


        static void DecryptCaesar(string encryptedFile, string decryptedFile, string key)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string alpha = GenerateAlphabet(key);
            string encryptedText = File.ReadAllText(encryptedFile);
            string decryptedText = "";

            foreach (char c in encryptedText)
            {
                // Переводим символ в нижний регистр
                char lowercaseChar = char.ToLower(c);

                int index = alpha.IndexOf(lowercaseChar);

                if (char.IsLetter(c))
                {
                    // Если символ найден в алфавите, получаем соответствующий расшифрованный символ
                    char decryptedChar = Alphabet[index];

                    // Проверяем оригинальный регистр символа и переводим расшифрованный символ в тот же регистр
                    if (char.IsUpper(c))
                    {
                        decryptedChar = char.ToUpper(decryptedChar);
                    }

                    decryptedText += decryptedChar;
                }
                else
                {
                    // Если символ не найден в алфавите, добавляем его как есть
                    decryptedText += c;
                }
            }

            Console.WriteLine("Расшифрованный текст:\n---------------\n" + decryptedText);
            File.WriteAllText(decryptedFile, decryptedText);

            Console.ResetColor();
        }

        static void EncryptTrisemus(string inputFile, string encryptedFile, char[,] table)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                string text = File.ReadAllText(inputFile);
                var encryptedText = new StringBuilder(text.Length);

                for (int i = 0; i < text.Length; i++)
                {
                    char currentChar = text[i];
                    bool isFound = false;

                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            if (table[row, col] == currentChar)
                            {
                                int newRow = (row + 1) % rows;
                                encryptedText.Append(table[newRow, col]);
                                isFound = true;
                                break;
                            }
                        }

                        if (isFound)
                        {
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        encryptedText.Append(currentChar);
                    }
                }
                Console.WriteLine("Зашифрованный текст:\n---------------\n" + encryptedText);
                File.WriteAllText(encryptedFile, encryptedText.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.ResetColor();
        }


        static void DecryptTrisemus(string encryptedFile, string decryptedFile, char[,] table)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                var text = File.ReadAllText(encryptedFile);
                var decryptedText = new StringBuilder(text.Length);

                for (var i = 0; i < text.Length; ++i)
                {
                    bool isReplaced = false;

                    for (var row = 0; row < rows && !isReplaced; ++row)
                    {
                        for (var column = 0; column < cols; ++column)
                        {
                            if (text[i] == table[row, column])
                            {
                                var newRow = (row == 0) ? rows - 1 : row - 1;
                                decryptedText.Append(table[newRow, column]);
                                isReplaced = true;
                                break;
                            }
                        }
                    }

                    if (!isReplaced)
                    {
                        decryptedText.Append(text[i]);
                    }
                }
                Console.WriteLine("Расшифрованный текст:\n---------------\n" + decryptedText);
                File.WriteAllText(decryptedFile, decryptedText.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.ResetColor();
        }

        public static char[,] TrisemusTable(string keyword)
        {
            var table = new char[rows, cols];
            var index = 0;

            foreach (var c in keyword.Distinct())
            {
                table[index / cols, index % cols] = c;
                index++;
            }

            foreach (var c in Alphabet)
            {
                if (index >= rows * cols)
                    break;
                if (!keyword.Contains(c))
                {
                    table[index / cols, index % cols] = c;
                    index++;
                }
            }

            return table;
        }

        static void CountCharacterFrequency(string text)
        {
            // Создаем словарь, который будет содержать частоту появления каждого символа английского алфавита в тексте
            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();

            // Проходим по каждому символу в тексте и увеличиваем его частоту на 1 в словаре, если это символ английского алфавита
            foreach (char c in text)
            {
                text = text.ToLower();
                if (char.IsLetter(c) && char.IsLower(c))
                {
                    if (characterFrequency.ContainsKey(c))
                    {
                        characterFrequency[c]++;
                    }
                    else
                    {
                        characterFrequency.Add(c, 1);
                    }
                }
            }

            // Вычисляем общее количество символов английского алфавита в тексте
            int totalCharacters = characterFrequency.Sum(x => x.Value);

            // Сортируем словарь по убыванию частоты появления символов и выводим пары "символ - частота появления в процентах"
            foreach (KeyValuePair<char, int> pair in characterFrequency.OrderByDescending(key => key.Value))
            {
                double frequencyPercentage = (double)pair.Value / totalCharacters * 100;
                Console.WriteLine("{0} - {1:F2}%", pair.Key, frequencyPercentage);
            }
        }
    }
}
