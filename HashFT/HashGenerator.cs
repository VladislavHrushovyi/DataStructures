using System.Text;

namespace HashFT;

public static class HashGenerator
{
    private const int MagicCoef = 97;
    private const double A = 0.3252342;

    public static int ByDividing(int value)
    {
        return value % MagicCoef;
    }

    public static int ByDividing(string data)
    {
        var sumAscii = Encoding.ASCII.GetBytes(data).Sum(s => s);

        return sumAscii % MagicCoef;
    }

    public static int ByMidSquare(int value)
    {
        var charsSquare = (value * value).ToString().ToCharArray();
        if (charsSquare.Length <= 3)
        {
            return Int32.Parse(charsSquare[1].ToString());
        }

        var midIndex = new Index(charsSquare.Length / 2);
        Range range = midIndex..(midIndex.Value + 2);
        var midValue = charsSquare[range];
        return Int32.Parse(midValue);
    }

    public static int ByMidSquare(string data)
    {
        var sumAscii = Encoding.ASCII.GetBytes(data).Sum(s => s);
        
        return ByMidSquare(sumAscii);
    }

    public static int ByDigitFolding(int value)
    {
        var sumOfDigitPairs = value.ToString().Chunk(2).Sum(s => Int32.Parse(s));

        return sumOfDigitPairs;
    }

    public static int ByDigitFolding(string data)
    {
        var sumAscii = Encoding.ASCII.GetBytes(data).Sum(s => s);

        return ByDigitFolding(sumAscii);
    }

    public static int ByMultiplication(int value)
    {
        return Convert.ToInt32(Math.Floor(MagicCoef * (value * A)));
    }

    public static int ByMultiplication(string data)
    {
        var sumAscii = Encoding.ASCII.GetBytes(data).Sum(s => s);

        return ByMultiplication(sumAscii);
    }
}