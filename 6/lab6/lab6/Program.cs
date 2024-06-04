int[] pos = { 1, 1, 2 };
string keyboard = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

var enigma = new Enigma(new ReflectorCDunn(), pos[0], pos[1], pos[2], keyboard);
var encoded = enigma.Crypt("AA");
Console.WriteLine($"Шифровка:{encoded}\n");
var decoded = enigma.Crypt(encoded);
Console.WriteLine($"Расшифровка:{decoded}");