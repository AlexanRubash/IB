char[] czechAlphabet = { 'a', 'á', 'b', 'c', 'č', 'd', 'ď', 'e', 'é', 'ě', 'f',
    'g', 'h', 'i', 'í', 'j', 'k', 'l', 'm', 'n', 'Ň', 'o', 'Ó', 'p', 'Q', 'r',
    'ř', 's', 'š', 't', 'Ť', 'u', 'Ú', 'Ů', 'v', 'w', 'x', 'y', 'ý', 'z', 'ž' };
char[] icelandicAlphabet = { 'a', 'á', 'b', 'd', 'ð', 'e', 'é', 'f', 'g', 'h', 
    'i', 'í', 'j', 'k', 'l', 'm', 'n', 'o', 'ó', 'p', 'r', 's', 't', 'u', 'ú',
    'v', 'x', 'y', 'ý', 'þ', 'æ', 'ö' };
char[] binaryAlphabet = { '1', '0' };

char[] base64Alphabet = {
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
    'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
    'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
    'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
    'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
    '8', '9', '+', '/'
};
string cheshText = "Lorem Ipsum je prostě fiktivní text tiskařského a sazebního průmyslu. Lorem Ipsum je standardním fiktivním textem v tomto odvětví již od roku 1500, kdy neznámá tiskárna vzala kuchyňku písma a zakódovala ji, aby vytvořila vzorník písma. Přežila nejen pět století, ale i skok do elektronické sazby, přičemž zůstala v podstatě nezměněna. Byl popularizován v 60. letech 20. století vydáním listů Letraset obsahujících pasáže Lorem Ipsum a v poslední době se softwarem pro stolní publikování, jako je Aldus PageMaker včetně verzí Lorem Ipsum.";
cheshText= cheshText.ToLower();
cheshText = cheshText.Replace(" ", "");
string icelandText= "Lorem Ipsum er einfaldlega blekkingartexti prent- og setningariðnaðarins. Lorem Ipsum hefur verið hefðbundinn líknartexti iðnaðarins síðan á 1500, þegar óþekktur prentari tók prentarann ​​og spænaði hana til að búa til sýnishornsbók. Það hefur ekki aðeins lifað af fimm aldir, heldur einnig stökkið yfir í rafræna leturgerð, helst óbreytt. Það var vinsælt á sjöunda áratugnum með útgáfu Letraset blaða sem innihéldu Lorem Ipsum köflum, og nýlega með skrifborðsútgáfuhugbúnaði eins og Aldus PageMaker, þar á meðal útgáfur af Lorem Ipsum.";
icelandText= icelandText.ToLower();
icelandText = icelandText.Replace(" ", "");

Console.WriteLine("Лабораторная работа 2");
bool flag = true;
double entropyCzech = 0;
double entropyIce = 0;
double entropyBin = 0;

while (flag)
{
    Console.WriteLine("\nВыберете задание");
    Console.WriteLine("1-расчёт энтропии языков, 2-binary, 3-кол-во инфы, 4-расчёт количества информации, 5-base64, 6-выход");
    int chooseTask;
    int.TryParse(Console.ReadLine(), out chooseTask);

    switch (chooseTask)
    {
        case 1:
            Console.WriteLine("Энтропия чешского языка по Шеннону:");
            entropyCzech = Entropy.CalculateShannonEntropy(cheshText, czechAlphabet);
            Console.WriteLine("Энтропия исландского языка по Шеннону:");
            entropyIce = Entropy.CalculateShannonEntropy(icelandText, icelandicAlphabet);
            break;
        case 2:
            Console.WriteLine("Энтропия бинарного алфавита по Шеннону:");
            //Console.WriteLine(Entropy.Binary("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."));
            entropyBin = Entropy.CalculateShannonEntropy(Entropy.Binary("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."), binaryAlphabet);
            break;
        case 3:
            entropyCzech = Entropy.CalculateShannonEntropy1(cheshText, czechAlphabet);
            entropyIce = Entropy.CalculateShannonEntropy1(icelandText, icelandicAlphabet);
            entropyBin = Entropy.CalculateShannonEntropy1(Entropy.Binary("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."), binaryAlphabet);
            Console.WriteLine("Сообщение: Rubashek Alexander Alexandrovich");
            Console.WriteLine("Czech\nКоличество информации:    " + Entropy.CountInformation("\r\nRubašek Alexandr Alexandrovič", entropyCzech));
            Console.WriteLine("Icelandic\nКоличество информации:    " + Entropy.CountInformation("Rubashek Alexander Alexandrovich", entropyIce));
            Console.WriteLine("Binary\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Rubashek Alexander Alexandrovich"), entropyBin));
            break;
        case 4:
            double b1 = Entropy.WithError(0.1);
            Console.WriteLine("0.1: " + b1 + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Rubashek Alexander Alexandrovich"), b1));
            double b2 = Entropy.WithError(0.5);
            Console.WriteLine("0.5: " + b2 + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Rubashek Alexander Alexandrovich"), b2));
            double b3 = Entropy.WithError(0.999705);
            Console.WriteLine("1.0: " + b3 + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Rubashek Alexander Alexandrovich"), b3) + "\n");

            double s1 = Entropy.WithError(0.1);
            Console.WriteLine("Для чешского\n0.1: " + s1 + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Rubašek Alexandr Alexandrovič"), s1));
            double s2 = Entropy.WithError(0.5);
            Console.WriteLine("0.5: " + s2 + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Rubašek Alexandr Alexandrovič"), s2));
            double s3 = Entropy.WithError(0.999705);
            Console.WriteLine("1.0: " + s3 + "\nКоличество информации:    " + "0");
            break;
        case 5:
            bool flag2 = true;
            while (flag2) { 
            string text_input = Entropy.ReadTextFromFile("D:\\Univer\\IB\\3\\input.txt");
            text_input = text_input.ToLower();
            text_input = text_input.Replace(" ", "");
            //Console.WriteLine(String.Equals(text_input,icelandText));
            string text_output = Entropy.ReadTextFromFile("D:\\Univer\\IB\\3\\output_base64.txt");
            double[] probabilitiesIce = Entropy.CalculateSymbolProbabilities(text_input, icelandicAlphabet);
            double[] probabilitiesBase64 = Entropy.CalculateSymbolProbabilities(text_output, base64Alphabet);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n1-шанс каждого символла, 2-по шенону, 3-по хартли, 4-количество информации, 5-избыточность информации, 6-выход");
            int chooseTask2;
            double entropyBase64 = 0;
            int.TryParse(Console.ReadLine(), out chooseTask2);
                switch (chooseTask2)
                {
                    case 1:
                        Console.WriteLine("Вероятности символов в Icelandic:");
                        Entropy.PrintSymbolProbabilities(probabilitiesIce, icelandicAlphabet);
                        Console.WriteLine("Вероятности символов в Base64:");
                        Entropy.PrintSymbolProbabilities(probabilitiesBase64, base64Alphabet);

                        break;
                    case 2:
                        Console.WriteLine("Энтропия исландского языка по Шеннону:");
                        Console.WriteLine(Entropy.CalculateShannonEntropy1(text_input, icelandicAlphabet));
                        Console.WriteLine("Энтропия символов base64 по Шеннону:");
                        Console.WriteLine(Entropy.CalculateShannonEntropy1(text_output, base64Alphabet));
                        break;
                    case 3:
                        Console.WriteLine("\nЭнтропия исландского по Хартли: " + Entropy.HartleyEntropy(icelandicAlphabet));

                        Console.WriteLine("\nЭнтропия base64 по Хартли: " + Entropy.HartleyEntropy(base64Alphabet));

                        break;
                    case 4:
                        entropyIce = Entropy.CalculateShannonEntropy1(text_input, icelandicAlphabet);
                        Console.WriteLine("Icelandic\nКоличество информации:    " + Entropy.CountInformation(icelandText, entropyIce));
                        entropyBase64 = Entropy.CalculateShannonEntropy1(text_output, base64Alphabet);
                        Console.WriteLine("Base64\nКоличество информации:    " + Entropy.CountInformation(text_output, entropyBase64));
                        break;
                    case 5:
                        entropyBase64 = Entropy.CalculateShannonEntropy1(text_output, base64Alphabet);
                        entropyIce = Entropy.CalculateShannonEntropy1(text_input, icelandicAlphabet);
                        double entropyBase64H = Entropy.HartleyEntropy(base64Alphabet);
                        double entropyIceH = Entropy.HartleyEntropy(icelandicAlphabet);
                        Console.WriteLine("Избыточность алфавита исландского языка: " + ((float)((entropyIceH - entropyIce )/ entropyIceH) * 100) + "%");
                        Console.WriteLine("Избыточность алфавита Base64: " + ((float)((entropyBase64H -entropyBase64 )/ entropyBase64H) * 100) + "%");

                        break;
                    case 6:
                        flag2 = false;
                        break;
                }
            }
            Console.ResetColor();
            break;
            
        case 6:
        default:
            flag = false;
            break;
    }
}


