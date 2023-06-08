namespace SortMethods.SorterLargeFile;

public class LineComparer : IComparer<Line>
{
    public int Compare(Line x, Line y)
    {
        var numberComparison = x.Number.CompareTo(y.Number);
        if (numberComparison != 0) return numberComparison;
        return string.Compare(x.Data, y.Data, StringComparison.Ordinal);
    }
}