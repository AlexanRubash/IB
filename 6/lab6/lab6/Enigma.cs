using System.Text;

public class Enigma
{
    public int posL;
    public int posM;
    public int posR;
    public Rotor rotorL;
    public Rotor rotorM;
    public Rotor rotorR;
    private Reflector reflector;
    private string keyboard;

    public Enigma(Reflector reflector, int posL, int posM, int posR, string keyboard)
    {
        this.posL= posL;
        this.posM = posM;
        this.posR = posR;
        this.reflector = reflector;
        this.keyboard = keyboard;
    }
    public string Crypt(string text)
    {
        var rotorL = new RotorIII(0);
        var rotorM = new Gamma(0);
        var rotorR = new RotorV(0);

        var result = new StringBuilder(text.Length);
        char symbol;

        foreach (var ch in text)
        {
            if (keyboard.Contains(ch))
            {
                symbol = rotorR[keyboard.IndexOf(ch)];
            }
            else
            {
                result.Append(ch);
                continue;
            }

            symbol = rotorM[keyboard.IndexOf(symbol)];
            symbol = rotorL[keyboard.IndexOf(symbol)];

            symbol = reflector.Reflect(symbol);

            symbol = keyboard[rotorL.IndexOf(symbol)];
            symbol = keyboard[rotorM.IndexOf(symbol)];
            symbol = keyboard[rotorR.IndexOf(symbol)];
            result.Append(symbol);

            // Поворот роторов
            rotorR.MoveRotor(posR); // Поворот самого медленного ротора
            if (posR == 0 && rotorM.isFullyRotated)
            {
                rotorM.MoveRotor(1); // Если средний ротор на позиции 0 и он полностью повернут, поворачиваем его
                rotorL.MoveRotor(1); // И также поворачиваем самый быстрый ротор
            }
            else if (posM == 0)
            {
                rotorL.MoveRotor(1); // Если средний ротор на позиции 0, поворачиваем быстрый ротор
            }
        }
        return result.ToString();
    }
}