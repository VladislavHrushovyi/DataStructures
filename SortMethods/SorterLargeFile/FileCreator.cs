using System.Collections.Immutable;

namespace SortMethods.SorterLargeFile;

public class FileCreator
{
    private readonly Random _rnd = new Random();
    private readonly string[] words;

    public FileCreator()
    {
        words = Enumerable.Range(0, 10_000)
            .Select(x =>
            {
                var range = Enumerable.Range(20, 100);
                var chars = range.Select(_ => (char)_rnd.Next('A', 'Z')).ToArray();
                var str = new string(chars);
                return str;
            }).ToArray();
    }

    public async Task<string> CreateLargeFile(long rowCount)
    {
        if (File.Exists("./large_text_file.txt"))
        {
            File.Delete("./large_text_file.txt");
            //return "large_text_file.txt";
        }
        await using var writer = File.AppendText("./large_text_file.txt");
        for (int i = 1; i <= rowCount; i++)
        {
            await writer.WriteAsync(GenerateNumber() + " " + GenerateWord());

            if (i % (rowCount / 10) == 0 && i != 0)
            {
                Console.WriteLine(i + " lines was created");
            }
        }

        return "large_text_file.txt";
    }

    private string GenerateWord() => words[_rnd.Next(0, words.Length - 1)];
    private string GenerateNumber() => _rnd.Next(0, 10_000).ToString();
}