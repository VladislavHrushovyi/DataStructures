namespace SortMethods.SorterLargeFile;

public class LineComparer : IComparer<Line>
{
    public int Compare(Line x, Line y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        var stringComparison = string.Compare(x.Data, y.Data, StringComparison.Ordinal);
        if (stringComparison != 0) return stringComparison;
        return x.Number.CompareTo(y.Number);
    }
}