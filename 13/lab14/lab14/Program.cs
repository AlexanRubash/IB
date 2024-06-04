using System.Drawing.Imaging;
using System.Drawing;
using System.Text;

Bitmap container = new Bitmap("D:\\Univer\\IB\\13\\lab14\\mob.bmp");

Console.WriteLine("Метод псевдослучайной перестановки");
string message = "Рубашек Александр Александрович";
byte[] messageBytes = Encoding.UTF8.GetBytes(message);

Bitmap stegoContainer = Steganography.EmbedPixelPermutationVertical(container, messageBytes);

stegoContainer.Save("D:\\Univer\\IB\\13\\lab14\\stego_containerPP.bmp", ImageFormat.Bmp);

Bitmap colorMatrix = Steganography.GenerateColorMatrix(stegoContainer, 3);
colorMatrix.Save("D:\\Univer\\IB\\13\\lab14\\matrixPP.bmp", ImageFormat.Bmp);

int messageLength = messageBytes.Length;
string extractedMessage = Steganography.ExtractPixelPermutationVertical(stegoContainer, messageLength);

Console.WriteLine($"Исходное сообщение: {message}");
Console.WriteLine($"Извлеченное сообщение: {extractedMessage}");

if (message == extractedMessage)
{
    Console.WriteLine("Сообщение успешно стеганографировано и извлечено методом псевдослучайной перестановки.");
}
else
{
    Console.WriteLine("Ошибка стеганографии методом псевдослучайной перестановки.");
}
Console.WriteLine();
Console.WriteLine("Метод LSB");

string docxFilePath = "D:\\Univer\\IB\\2\\ответы.docx";
string messageFromDocx = Steganography.ExtractTextFromDocx(docxFilePath);

string limitedMessage = messageFromDocx.Length > 100 ? messageFromDocx.Substring(0, 100) : messageFromDocx;

Bitmap stegoContainerLSB = Steganography.EmbedLSB(limitedMessage, container);

stegoContainerLSB.Save("D:\\Univer\\IB\\13\\lab14\\stego_containerLSB.bmp", ImageFormat.Bmp);

Bitmap colorMatrix2 = Steganography.GenerateColorMatrix(stegoContainerLSB, 3);
colorMatrix2.Save("D:\\Univer\\IB\\13\\lab14\\matrixLSB.bmp", ImageFormat.Bmp);

string extractedLimitedMessage = Steganography.ExtractLSB(stegoContainerLSB);

Console.WriteLine($"Исходное сообщение: {limitedMessage}");
Console.WriteLine($"Извлеченное сообщение: {extractedLimitedMessage}");

if (limitedMessage == extractedLimitedMessage)
{
    Console.WriteLine("Сообщение успешно стеганографировано и извлечено методом LSB.");
}
else
{
    Console.WriteLine("Ошибка стеганографии методом LSB.");
}


