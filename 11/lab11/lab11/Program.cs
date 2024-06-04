using System.Diagnostics;

string input = "Rubashek Alexandr Alexandrovich";

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

string md5Hash = MD5Hash.CalculateMD5Hash(input);

stopwatch.Stop();

Console.WriteLine("Входные данные: " + input);
Console.WriteLine("MD5 Hash: " + md5Hash);
Console.WriteLine("Время хэширования {0} символов составило {1} мс ", input.Length ,stopwatch.ElapsedMilliseconds);
