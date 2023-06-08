namespace SortMethods.SorterLargeFile;

public class SorterLargeFile
{
    private readonly int _chunkSize;
    private readonly string _path;
    private readonly List<StreamReader> _fileStreams;

    public SorterLargeFile(int sorterPart, string path)
    {
        this._chunkSize = sorterPart;
        _path = path;
        this._fileStreams = new List<StreamReader>();
    }

    private class LineState
    {
        public StreamReader Reader { get; set; }
        public Line Line { get; set; }
    }

    public void Sort()
    {
        SeparateFile();
        SortResult();
    }

    private void SortResult()
    {
        try
        {
            var lines = _fileStreams.Select(x => new LineState()
            {
                Reader = x,
                Line = new Line(x.ReadLine())
            }).OrderBy(x => x.Line).ToList();

            using var writerResult = new StreamWriter(_path);
            int logstep = 0;
            int countLogStep = 0;
            while (lines.Count > 0)
            {
                var current = lines.First();
                writerResult.WriteLine(current.Line);

                if (current.Reader.EndOfStream)
                {
                    lines.Remove(current);
                    continue;
                }

                current.Line = new Line(current.Reader.ReadLine());
                lines = Reorder(lines);

                if (logstep == 1_000_000)
                {
                    countLogStep++;
                    Console.WriteLine(logstep * countLogStep + " lines was merged");
                    logstep = 0;
                }

                logstep++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            foreach (var stream in _fileStreams)
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

    private void SeparateFile()
    {
        int countSepFiles = 1;
        int i = 0;
        var readerInputFile = new StreamReader(_path);
        var chunkItems = new Line[_chunkSize];
        Console.WriteLine("allo");
        while (!readerInputFile.EndOfStream)
        {
            var stringLine = readerInputFile.ReadLine().Split(" ");
            chunkItems[i] = new Line(stringLine);

            if (i == _chunkSize - 1)
            {
                var result = chunkItems
                    .OrderBy(x => x)
                    .Select(x => x.ToString());
                var partFileName = countSepFiles + ".txt";
                File.WriteAllLines(partFileName, result);
                i = 0;
                countSepFiles++;
                Console.WriteLine(partFileName + " was created");
                _fileStreams.Add(new StreamReader(partFileName));
            }

            i++;
        }

        readerInputFile.Dispose();

        // foreach (var rows in File.ReadAllLines($"./{_path}").Chunk(_partOfSeparate))
        // {
        //     var lines = rows.Select(line => new Line(line.Split(" ")))
        //         .Order(new LineComparer()).Select(x => x.ToString());
        //         //.OrderBy(x => x.Data).ThenBy(x => x.Number).Select(x => x.ToString());
        //
        //     string fileName = countSepFiles + ".txt";
        //     File.WriteAllLines($"{countSepFiles}.txt", lines);
        //     _fileStreams.Add(new StreamReader(fileName));
        //     countSepFiles++;
        // }
    }
}