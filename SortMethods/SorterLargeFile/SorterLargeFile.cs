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

                current.Line = new Line(current.Reader.ReadLine()!);
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
        if (lines.Count == 1)
        {
            return;
        }

        int i = 0;
        while (lines[i].Line.CompareTo(lines[i+1].Line) > 0 )
        {
            (lines[i], lines[i + 1]) = (lines[i + 1], lines[i]);
            i++;
            if (i + 1 == lines.Count)
                return;
        }
    }

    private async Task SeparateFile()
    {
        int countSepFiles = 0;
        int i = 0;
        var chunkItems = new Line[_chunkSize];
        using var reader = new StreamReader(_path);
        
        for (string line = reader.ReadLine()!; ; line = reader.ReadLine()!)
        {
            chunkItems[i] = new Line(line);
            i++;

            if (i == _chunkSize)
            {
                countSepFiles++;
                var partNameFile = countSepFiles + ".txt";
                _partFiles.Add(partNameFile);
                    
                Array.Sort(chunkItems);
                Console.WriteLine(partNameFile + " was sorted");
                    
                WriteAllLines(partNameFile, chunkItems);
                Console.WriteLine(partNameFile + " was created");
                i = 0;
            }

            if (reader.EndOfStream)
            {
                break;
            }
        }

        if (i != 0)
        {
            Array.Resize(ref chunkItems, i + 1);
            countSepFiles++;
            var partFileName = countSepFiles + ".txt";
            _partFiles.Add(partFileName);
            WriteAllLines(partFileName, chunkItems);
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

    private async Task SortPartFiles()
    {
        foreach (var partFile in _partFiles)
        {
            var lines = (await File.ReadAllLinesAsync(partFile)).Select(x => new Line(x)).ToArray();
            Array.Sort(lines);
            await File.WriteAllLinesAsync(partFile, lines.Select(x => x.Build()));
            Console.WriteLine(partFile + " sorted");
            Array.Clear(lines);
        }
    }
}