using System.Numerics;

bool yes = true;
while (yes)
{
    Console.WriteLine("Выберите алгоритм для ЭЦП:");
    Console.WriteLine("1. RSA");
    Console.WriteLine("2. Эль-Гамаля");
    Console.WriteLine("3. Шнорра");
    Console.WriteLine("4. Выход");

    int choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.Write("Введите исходный текст: ");
            string sourceText = Console.ReadLine();
            RSA rsa = new RSA();
            BigInteger[] digitalSignRSA = rsa.CreateDigitalSignature(sourceText);
            Console.Write("\nВведите текст для проверки: ");
            string checkingText = Console.ReadLine();
            rsa.VerifyDigitalSignature(checkingText, digitalSignRSA);
            break;
        case 2:
            Console.Write("Введите исходный текст: ");
            sourceText = Console.ReadLine();
            ElGamal elGamal = new ElGamal();
            BigInteger[,] digitalSignElGamal = elGamal.CreateDigitalSignature(sourceText);
            Console.Write("\nВведите текст для проверки: ");
            checkingText = Console.ReadLine();
            elGamal.VerifyDigitalSignature(checkingText, digitalSignElGamal);
            break;
        case 3:
            Console.WriteLine();
            Console.Write("Введите исходный текст: ");
            sourceText = Console.ReadLine();
            Schnorr schnorr = new Schnorr();
            BigInteger[,] digitalSignSchnorr = schnorr.GenerateDigitalSignature(sourceText);
            Console.Write("\nВведите текст для проверки: ");
            checkingText = Console.ReadLine();
            schnorr.VerifyDigitalSignature(checkingText, digitalSignSchnorr);
            break;
        case 4:
            yes = false;
            break;
        default:
            break;
    }
}