namespace SortMethods.SorterLargeFile;

public class SorterLargeFile
{
    private readonly int _chunkSize;
    private readonly string _path;
    private readonly List<string> _partFiles;

    public SorterLargeFile(int sorterPart, string path)
    {
        this._chunkSize = sorterPart;
        _path = path;
        this._partFiles = new List<string>();
    }

    private class LineState
    {
        public StreamReader? Reader { get; init; }
        public Line Line { get; set; }
    }

    public async Task Sort()
    {
        await SeparateFile();
        await SortResult();
    }

    private async Task SortResult()
    {
        var readers = _partFiles.Select(x => new StreamReader(x)).ToArray();
        try
        {
            var lines = readers.Select(x => new LineState()
            {
                Reader = x,
                Line = new Line(x.ReadLine()!)
            }).OrderBy(x => x.Line).ToList();

            await using var writerResult = new StreamWriter(_path);
            int logStep = 0;
            int countLogStep = 0;
            while (lines.Count > 0)
            {
                var current = lines[0];
                WriteLine(writerResult, current.Line);

                if (current.Reader!.EndOfStream)
                {
                    lines.Remove(current);
                    continue;
                }

                current.Line = new Line((await current.Reader.ReadLineAsync())!);
                Reorder(lines);

                if (logStep == 1_000_000)
                {
                    countLogStep++;
                    Console.WriteLine(logStep * countLogStep + " lines was merged");
                    logStep = 0;
                }

                logStep++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            foreach (var stream in readers)
            {
                stream.Dispose();
            }
        }
    }

    private void Reorder(List<LineState> lines)
    {
        lines = lines.OrderBy(x => x.Line).ToList();
    }

    private async Task SeparateFile()
    {
        int countSepFiles = 0;
        using var reader = new StreamReader(_path);

        foreach (var part in reader.FileAsEnumerable().Chunk(_chunkSize))
        {
            var sortedPartLine = part.Select(x => new Line(x))
                .OrderBy(x => x)
                .ToArray();
            
            countSepFiles++;
            var partNameFile = countSepFiles + ".txt";
            _partFiles.Add(partNameFile);
            
            WriteAllLines(partNameFile, sortedPartLine);
            Console.WriteLine(partNameFile + " was created");
        }
    }

    private void WriteAllLines(string pathFile, Line[] chunkItems)
    {
        using var writer = new StreamWriter(pathFile);
        foreach (var item in chunkItems)
        {
            WriteLine(writer, item);
        }
    }

    private void WriteLine(StreamWriter writer, Line line)
    {
        writer.Write(line.Number);
        writer.Write(" ");
        writer.WriteLine(line.Word);
    }
}