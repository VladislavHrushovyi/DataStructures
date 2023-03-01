namespace Graph;

public class DijkstraAlgorithm
{
    protected readonly MyGraph Graph;
    protected readonly List<GraphVertexInfo> _vertexInfos;

    public DijkstraAlgorithm(MyGraph graph)
    {
        Graph = graph;
        _vertexInfos = Graph.Vertices.Select(v => new GraphVertexInfo(v)).ToList();
    }

    protected GraphVertexInfo GetVertexInfo(GraphVertex vertex)
    {
        return _vertexInfos.SingleOrDefault(v => v.Vertex == vertex);
    }

    public string FindShortestPath(string startVertex, string finishVertex)
    {
        return FindShortestPath(Graph.FindVertex(startVertex), Graph.FindVertex(finishVertex));
    }

    private string FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
    {
        var first = GetVertexInfo(startVertex);
        first.EdgeWeightSum = 0;
        while (true)
        {
            var current = FindUnvisitedVertexWithMinSum();
            if (current == null)
            {
                break;
            }

            SetSumToTheNextVertex(current);
        }
        return GetPath(startVertex, finishVertex);
    }

    private void SetSumToTheNextVertex(GraphVertexInfo current)
    {
        current.IsUnvisited = false;
        foreach (var edge in current.Vertex.Edges)
        {
            var nextInfo = GetVertexInfo(edge.ConnectedVertex);
            var sum = current.EdgeWeightSum + edge.EdgeWeight;
            if (sum < nextInfo.EdgeWeightSum)
            {
                nextInfo.EdgeWeightSum = sum;
                nextInfo.PrevVertex = current.Vertex;
            }
        }
    }

    private GraphVertexInfo FindUnvisitedVertexWithMinSum()
    {
        var minValue = int.MaxValue;
        GraphVertexInfo minVertexInfo = null;
        foreach (var i in _vertexInfos)
        {
            if (i.IsUnvisited && i.EdgeWeightSum < minValue)
            {
                minVertexInfo = i;
                minValue = i.EdgeWeightSum;
            }
        }

        return minVertexInfo;
    }

    private string GetPath(GraphVertex startVertex, GraphVertex finishVertex)
    {
        var path = finishVertex.ToString();
        while (startVertex != finishVertex)
        {
            finishVertex = GetVertexInfo(finishVertex).PrevVertex;
            path = finishVertex + path;
        }

        return path;
    }
}

public class GraphVertexInfo
{
    public GraphVertex Vertex { get; set; }
    public bool IsUnvisited { get; set; }
    public int EdgeWeightSum { get; set; }
    public GraphVertex PrevVertex { get; set; }

    public GraphVertexInfo(GraphVertex vertex)
    {
        Vertex = vertex;
        IsUnvisited = true;
        EdgeWeightSum = int.MaxValue;
        PrevVertex = null;
    }
}