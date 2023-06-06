using System.Collections;

namespace SortMethods.SorterLargeFile;

public class FileCreator
{
    private readonly long _rowCount;

    public FileCreator(long rowCount)
    {
        _rowCount = rowCount;
    }

    public void CreateLargeFile()
    {
        if (File.Exists("./large_text_file.txt"))
        {
            File.Delete("./large_text_file.txt");
        }
        using var writer = File.AppendText("./large_text_file.txt");
        for (int i = 1; i <= _rowCount; i++)
        {
            var rndNumber = Random.Shared.Next(0, int.MaxValue);
            var rndChars = Enumerable.Range(10, Random.Shared.Next(10, 50))
                .Select(_ => (char)Random.Shared.Next('A', 'Z' + 1));
            var row = rndNumber + " " + string.Join("", rndChars) + "\n";

            writer.Write(row);

            if (i % (_rowCount / 10) == 0 && i != 0)
            {
                Console.WriteLine(i + " lines was created");
            }
        }
    }
}