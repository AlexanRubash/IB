using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class Steganography
{
    public static Bitmap EmbedPixelPermutationVertical(Bitmap container, byte[] message)
    {
        int width = container.Width;
        int height = container.Height;

        Bitmap stegoContainer = new Bitmap(container);

        int messageIndex = 0;
        int bitIndex = 0;

        for (int x = 0; x < width; x++) // Outer loop for columns
        {
            for (int y = 0; y < height; y++) // Inner loop for rows
            {
                if (messageIndex < message.Length)
                {
                    System.Drawing.Color pixel = container.GetPixel(x, y);
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    byte currentByte = message[messageIndex];
                    int currentBit = (currentByte >> (7 - bitIndex)) & 0x01;

                    red = (red & 0xFE) | currentBit;

                    System.Drawing.Color stegoPixel = System.Drawing.Color.FromArgb(red, green, blue);
                    stegoContainer.SetPixel(x, y, stegoPixel);

                    bitIndex++;

                    if (bitIndex >= 8)
                    {
                        bitIndex = 0;
                        messageIndex++;
                    }
                }
            }
        }

        return stegoContainer;
    }


    public static string ExtractPixelPermutationVertical(Bitmap stegoContainer, int messageLength)
    {
        int width = stegoContainer.Width;
        int height = stegoContainer.Height;

        List<byte> messageBytes = new List<byte>();
        int bitIndex = 0;
        byte currentByte = 0;

        for (int x = 0; x < width; x++) // Outer loop for columns
        {
            for (int y = 0; y < height; y++) // Inner loop for rows
            {
                if (messageBytes.Count >= messageLength)
                {
                    break;
                }

                System.Drawing.Color pixel = stegoContainer.GetPixel(x, y);

                int redBit = pixel.R & 0x01;

                currentByte = (byte)((currentByte << 1) | redBit);
                bitIndex++;

                if (bitIndex >= 8)
                {
                    messageBytes.Add(currentByte);
                    currentByte = 0;
                    bitIndex = 0;
                }
            }
        }

        string message = Encoding.UTF8.GetString(messageBytes.ToArray());
        return message;
    }


    public static Bitmap EmbedLSB(string message, Bitmap bmp)
    {
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        int messageLength = messageBytes.Length;

        // Check if the image can hold the message
        if ((messageLength + 2) * 8 > bmp.Width * bmp.Height)
        {
            throw new ArgumentException("Message is too large to be hidden in the given image.");
        }

        // Embed the length of the message as the first two bytes
        byte[] lengthBytes = BitConverter.GetBytes((short)messageLength);
        byte[] fullMessageBytes = new byte[messageBytes.Length + lengthBytes.Length];
        Array.Copy(lengthBytes, fullMessageBytes, lengthBytes.Length);
        Array.Copy(messageBytes, 0, fullMessageBytes, lengthBytes.Length, messageBytes.Length);

        Bitmap stegoImage = new Bitmap(bmp);
        int byteIndex = 0, bitIndex = 0;

        for (int y = 0; y < stegoImage.Height; y++)
        {
            for (int x = 0; x < stegoImage.Width; x++)
            {
                if (byteIndex >= fullMessageBytes.Length)
                {
                    return stegoImage;
                }

                System.Drawing.Color pixel = stegoImage.GetPixel(x, y);
                byte r = pixel.R, g = pixel.G, b = pixel.B;

                // Вставка бита в младший бит синего канала
                b = (byte)((b & 0xFE) | ((fullMessageBytes[byteIndex] >> bitIndex) & 1));
                bitIndex++;

                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }

                System.Drawing.Color newPixel = System.Drawing.Color.FromArgb(r, g, b);
                stegoImage.SetPixel(x, y, newPixel);
            }
        }

        return stegoImage;
    }

    public static string ExtractLSB(Bitmap bmp)
    {
        int byteIndex = 0, bitIndex = 0;
        byte[] lengthBytes = new byte[2];

        // Extract the length of the message first
        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                if (byteIndex < lengthBytes.Length * 8)
                {
                    System.Drawing.Color pixel = bmp.GetPixel(x, y);
                    byte b = pixel.B;

                    // Extract the least significant bit of the blue channel
                    lengthBytes[byteIndex / 8] |= (byte)((b & 1) << (byteIndex % 8));
                    byteIndex++;

                    if (byteIndex >= lengthBytes.Length * 8)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (byteIndex >= lengthBytes.Length * 8)
            {
                break; // Exit the outer loop once the length is extracted
            }
        }

        int messageLength = BitConverter.ToInt16(lengthBytes, 0);
        byte[] messageBytes = new byte[messageLength];
        byteIndex = 0;
        bitIndex = 0;
        int extractedBytes = 0; // Variable to track the number of extracted bytes

        // Extract the message using the obtained length
        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                if (extractedBytes < messageLength) // Check if all message bytes are extracted
                {
                    // Skip processing until the length of the message is reached
                    if (byteIndex < lengthBytes.Length)
                    {
                        bitIndex++;
                        byteIndex += bitIndex / 8;
                        bitIndex %= 8;
                        continue;
                    }

                    System.Drawing.Color pixel = bmp.GetPixel(x, y);
                    byte b = pixel.B;

                    // Extract the least significant bit of the blue channel
                    messageBytes[extractedBytes] |= (byte)((b & 1) << bitIndex);
                   
                    bitIndex++;

                    if (bitIndex == 8)
                    {
                        bitIndex = 0;
                        extractedBytes++;
                    }
                }
                else
                {
                    return Encoding.UTF8.GetString(messageBytes); // Return the extracted message
                }
            }
        }

        return Encoding.UTF8.GetString(messageBytes); // Return the extracted message
    }

    public static Bitmap GenerateColorMatrix(Bitmap container, int bitLevel)
    {
        int width = container.Width;
        int height = container.Height;

        Bitmap colorMatrix = new Bitmap(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                 System.Drawing.Color pixel = container.GetPixel(x, y);

                // Извлекаем компоненты цвета
                int red = pixel.R;
                int green = pixel.G;
                int blue = pixel.B;

                // Получаем значение бита на соответствующем уровне
                int bitValue = GetBitValue(red, green, blue, bitLevel);

                // Создаем пиксель с цветом, соответствующим значению бита
                 System.Drawing.Color matrixPixel = GetColorForBit(bitValue);

                // Заменяем пиксель в цветовой матрице
                colorMatrix.SetPixel(x, y, matrixPixel);
            }
        }

        return colorMatrix;
    }

    public static int GetBitValue(int red, int green, int blue, int bitLevel)
    {
        int bitMask = 1 << bitLevel;

        int redBit = (red & bitMask) >> bitLevel;
        int greenBit = (green & bitMask) >> bitLevel;
        int blueBit = (blue & bitMask) >> bitLevel;

        return (redBit << 2) | (greenBit << 1) | blueBit;
    }

    public static  System.Drawing.Color GetColorForBit(int bitValue)
    {
        // Задаем значения цветовых компонент в соответствии с битом
        int red = (bitValue & 0x04) != 0 ? 255 : 0;   // Красный канал
        int green = (bitValue & 0x02) != 0 ? 255 : 0; // Зеленый канал
        int blue = (bitValue & 0x01) != 0 ? 255 : 0;  // Синий канал

         System.Drawing.Color color = System.Drawing.Color.FromArgb(red, green, blue);
        return color;
    }

    private static int SwapByteBits(int n)
    {
        n = (n & 0xF0) >> 4 | (n & 0x0F) << 4;
        n = (n & 0xCC) >> 2 | (n & 0x33) << 2;
        n = (n & 0xAA) >> 1 | (n & 0x55) << 1;
        return n;
    }
    public static string ExtractTextFromDocx(string filePath)
    {
        using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false))
        {
            Body body = wordDocument.MainDocumentPart.Document.Body;
            return body.InnerText;
        }
    }
}
