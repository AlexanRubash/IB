public abstract class Rotor
{
    protected readonly char[] _rotorChar;
    protected int _currentIndex;
    public bool isFullyRotated;

    public Rotor(string rotorString, int startIndex)
    {
        _rotorChar = rotorString.ToCharArray();
        _currentIndex = startIndex >= rotorString.Length ? 0 : startIndex;
    }

    public char this[int index]
    {
        get
        {
            return _rotorChar[(index + _currentIndex) % _rotorChar.Length];
        }
    }

    public string GetRotor()
    {
        string res = null;
        for (int i = 0; i < _rotorChar.Length; i++)
        {
            res += _rotorChar[(_currentIndex + i) % _rotorChar.Length];
        }
        return res;
    }

    public int IndexOf(char symbol)
    {
        int index = _rotorChar.ToList().IndexOf(symbol);

        // shows how much the shift to the right has occurred
        int rightOffset = _rotorChar.Length - _currentIndex;
        int offsetRotorIndex = (index + rightOffset) % _rotorChar.Length;

        return offsetRotorIndex;
    }

    public void MoveRotor(int offset)
    {
        _currentIndex += offset;
        if (_currentIndex >= _rotorChar.Length)
        {
            _currentIndex %= _rotorChar.Length;
            isFullyRotated = true;
        }
        isFullyRotated = false;
    }

    public char CurrentRotor()
    {
        return _rotorChar[_currentIndex];
    }

    public void Reset()
    {
        _currentIndex = 0;
    }
}


public class RotorI : Rotor
{
    public RotorI(int startIndex = 0) : base("EKMFLGDQVZNTOWYHXUSPAIBRCJ", startIndex)
    {
    }
}
public class RotorII : Rotor
{
    public RotorII(int startIndex = 0) : base("AJDKSIRUXBLHWTMCQGZNPYFVOE", startIndex)
    {
    }
}
public class RotorIII : Rotor
{
    public RotorIII(int startIndex = 0) : base("BDFHJLCPRTXVZNYEIWGAKMUSQO", startIndex)
    {
    }
}
public class RotorIV : Rotor
{
    public RotorIV(int startIndex = 0) : base("ESOVPZJAYQUIRHXLNFTGKDCMWB", startIndex)
    {
    }
}
public class RotorV : Rotor
{
    public RotorV(int startIndex = 0) : base("VZBRGITYUPSDNHLXAWMJQOFECK", startIndex)
    {
    }
}
public class RotorVI : Rotor
{
    public RotorVI(int startIndex = 0) : base("JPGVOUMFYQBENHZRDKASXLICTW", startIndex)
    {
    }
}
public class RotorVII : Rotor
{
    public RotorVII(int startIndex = 0) : base("NZJHGRCXMYSWBOUFAIVLPEKQDT", startIndex)
    {
    }
}
public class RotorVIII : Rotor
{
    public RotorVIII(int startIndex = 0) : base("FKQHTLXOCBJSPDZRAMEWNIUYGV", startIndex)
    {
    }
}
public class Beta : Rotor
{
    public Beta(int startIndex = 0) : base("LEYJVCNIXWPBQMDRTAKZGFUHOS", startIndex)
    {
    }
}
public class Gamma : Rotor
{
    public Gamma(int startIndex = 0) : base("FSOKANUERHMBTIYCWLQPZXVGJD", startIndex)
    {
    }
}