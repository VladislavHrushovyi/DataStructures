namespace SortMethods.SorterLargeFile;

public static class FileExtension
{
    public static IEnumerable<string> FileAsEnumerable(this StreamReader reader)
    {
        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine();
        }
    }
}