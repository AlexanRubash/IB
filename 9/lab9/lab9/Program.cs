using System.Text;

int baseMultiplier = 8;
int privateKeyMultiplier = 25; // Множитель, используемый для вычисления открытого ключа из закрытого ключа. mod(privateKeyMultiplier, n) = 1
string originalMessage = "UnViYXNoZWsgQWxleGFuZHIgQWxleGFuZHJvdmljaA=";

var ranceCipher = new RanceCipher();
int[] privateKey = ranceCipher.GenerateSequence(baseMultiplier);
Console.WriteLine($"Закрытый ключ: {ranceCipher.ConvertToString(privateKey)}");

int modulus = 0;
for (int i = 0; i < privateKey.Length; i++)
{
    modulus += privateKey[i]; // Должен быть больше суммы всех элементов в закрытом ключе privateKey
}
modulus += 43;
Console.WriteLine("Сумма элементов: " + modulus);

int[] publicKey = ranceCipher.ComputeSequence(privateKey, privateKeyMultiplier, modulus, baseMultiplier);
Console.WriteLine($"Открытый ключ: {ranceCipher.ConvertToString(publicKey)}");

DateTime startTime = DateTime.Now;
int[] encryptedMessage = ranceCipher.Encrypt(publicKey, originalMessage, baseMultiplier);
DateTime endTime = DateTime.Now;
Console.WriteLine($"\nЗашифрованное сообщение: {ranceCipher.ConvertToString(encryptedMessage)}");
Console.WriteLine("Шифрование {0} символов заняло {1} мс", originalMessage.Length, (endTime - startTime).TotalMilliseconds);

int inversePrivateKeyMultiplier = ranceCipher.GetInverse(privateKeyMultiplier, modulus);

int[] decryptedMessage = new int[encryptedMessage.Length];
string decryptedText = "";

for (int i = 0; i < encryptedMessage.Length; i++)
{
    decryptedMessage[i] = (encryptedMessage[i] * inversePrivateKeyMultiplier) % modulus;
}
Console.WriteLine($"Расшифрованное сообщение: {ranceCipher.ConvertToString(decryptedMessage)}");

Console.Write("Расшифрованное сообщение:");
startTime = DateTime.Now;
foreach (int decryptedSymbol in decryptedMessage)
{
    string decryptedSymbolText = ranceCipher.Decrypt(privateKey, decryptedSymbol, baseMultiplier);
    decryptedText += decryptedSymbolText + " ";
}
endTime = DateTime.Now;
Console.WriteLine("\nРасшифрование {0} символов заняло {1} мс", originalMessage.Length, (endTime - startTime).TotalMilliseconds);

decryptedText = decryptedText.Replace(" ", "");
var stringArray = Enumerable.Range(0, decryptedText.Length / 8).Select(i => Convert.ToByte(decryptedText.Substring(i * 8, 8), 2)).ToArray();
var finalText = Encoding.UTF8.GetString(stringArray);
finalText = finalText.Replace("@", " ");
Console.WriteLine("Расшифрованное сообщение: " + finalText);
