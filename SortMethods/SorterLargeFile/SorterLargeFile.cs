namespace SortMethods.SorterLargeFile;

public class SorterLargeFile
{
    private readonly int _partOfSeparate;
    private readonly string _path;
    private readonly List<StreamReader> _fileStreams;
    public SorterLargeFile(int sorterPart, string path)
    {
        this._partOfSeparate = sorterPart;
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
        
    }

    private string SortResult()
    {
        try
        {
            var lines = _fileStreams.Select(x => new LineState()
            {
                Reader = x,
                Line = new Line(x.ReadLine())
            }).OrderBy(x => x.Line).ToList();

            using var writerResult = new StreamWriter(_path);

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
                Reorder(lines);
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
        return _path;
    }

    private void Reorder(List<LineState> lines)
    {
        throw new NotImplementedException();
    }

    private void SeparateFile()
    {
        int countSepFiles = 1;
        foreach (var rows in File.ReadAllLines($"./{_path}").Chunk(_partOfSeparate))
        {
            var lines = rows.Select(line => new Line(line.Split(" ")))
                .Order(new LineComparer()).Select(x => x.ToString());
                //.OrderBy(x => x.Data).ThenBy(x => x.Number).Select(x => x.ToString());

            string fileName = countSepFiles + ".txt";
            File.WriteAllLines($"{countSepFiles}.txt", lines);
            _fileStreams.Add(new StreamReader(fileName));
            countSepFiles++;
        }
    }
}