using System.Diagnostics.CodeAnalysis;

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
                var current = lines.First();
                await writerResult.WriteLineAsync(current.Line.Build());

                if (current.Reader!.EndOfStream)
                {
                    lines.Remove(current);
                    continue;
                }

                current.Line = new Line(current.Reader.ReadLine()!);
                lines = Reorder(lines);

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

    private List<LineState> Reorder(List<LineState> lines)
    {
        if (lines.Count == 1)
        {
            return lines.Take(1).ToList();
        }

        return lines.OrderBy(x => x.Line).ToList();
    }

    private async Task SeparateFile()
    {
        int countSepFiles = 1;
        int i = 0;
        using var readerInputFile = new StreamReader(_path);
        var chunkItems = new string[_chunkSize];
        while (!readerInputFile.EndOfStream)
        {
            var stringLine = await readerInputFile.ReadLineAsync();
            chunkItems[i] = stringLine!;

            if (i == _chunkSize - 1)
            {
                
                var partFileName = countSepFiles + ".txt";
                await File.WriteAllLinesAsync(partFileName, chunkItems);
                i = 0;
                countSepFiles++;
                Console.WriteLine(partFileName + " was created");
                _partFiles.Add(partFileName);
            }

            i++;
        }
        await SortPartFiles();
        Array.Clear(chunkItems);
        readerInputFile.Dispose();
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