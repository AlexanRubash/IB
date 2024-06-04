public class Reflector
{
    private readonly string[] _pairs;

    public Reflector(string[] pairs)
    {
        _pairs = pairs;
    }

    public char Reflect(char c)
    {
        var pair = _pairs.First(x => x.Contains(c));
        return pair.First(x => !x.Equals(c));
    }
}

public class ReflectorB : Reflector
{
    public ReflectorB() : base(new[] { "AY", "BR", "CU", "DH", "EQ", "FS", "GL", "IP", "JX", "KN", "MO", "TZ", "VW" })
    {
    }
}
public class ReflectorC : Reflector
{
    public ReflectorC() : base(new[] { "AF", "BV", "CP", "DJ", "EI", "GO", "HY", "KR", "LZ", "MX", "NW", "TQ", "SU" })
    {
    }
}
public class ReflectorBDunn : Reflector
{
    public ReflectorBDunn() : base(new[] { "AE", "BN", "CK", "DQ", "FU", "GY", "HW", "IJ", "LO", "MP", "RX", "SZ", "TV" })
    {
    }
}
public class ReflectorCDunn : Reflector
{
    public ReflectorCDunn() : base(new[] { "AR", "BD", "CO", "EJ", "FN", "GT", "HK", "IV", "LM", "PW", "QZ", "SX", "UY" })
    {
    }
}
