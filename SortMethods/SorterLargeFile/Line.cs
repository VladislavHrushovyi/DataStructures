namespace SortMethods.SorterLargeFile;

public struct Line : IComparable<Line>
{
    public long Number { get; set; }
    public string Data { get; set; }

    public Line(string[] line)
    {
        Number = long.Parse(line[0]);
        Data = line[1];
    }

    public Line(string line) : this(line.Split(" "))
    {
    }

    public int CompareTo(Line other)
    {
        int numberComparison = this.Number.CompareTo(other.Number);
        if (numberComparison != 0) return numberComparison;

        return String.Compare(this.Data, other.Data, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return Number + " " + Data;
    }

    // public int CompareTo(Line? other)
    // {
    //     if (ReferenceEquals(this, other)) return 0;
    //     if (ReferenceEquals(null, other)) return 1;
    //     var numberComparison = Number.CompareTo(other.Number);
    //     if (numberComparison != 0) return numberComparison;
    //     return string.Compare(Data, other.Data, StringComparison.Ordinal);
    // }
}