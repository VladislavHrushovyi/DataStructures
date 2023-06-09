namespace SortMethods.SorterLargeFile;

public readonly struct Line : IComparable<Line>
{
    private readonly string _line;
    private readonly int _pos;
    public long Number { get; }
    public ReadOnlySpan<char> Word => _line.AsSpan(_pos + 1);
    
    public Line(string line)
    {
        _line = line;
        _pos = _line.IndexOf(" ");
        Number = int.Parse(line.AsSpan(0, _pos));
    }

    public string Build() => _line;

    public int CompareTo(Line other)
    {
        int numberComparison = this.Word.CompareTo(other.Word, StringComparison.Ordinal);
        if (numberComparison != 0) return numberComparison;

        return Number.CompareTo(other.Number);
    }
}