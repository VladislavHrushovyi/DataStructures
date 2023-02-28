namespace Graph;

public class GraphVertex
{
    public string Name { get; }
    public List<GraphEdge> Edges { get; }
    
    public GraphVertex(string name)
    {
        Name = name;
        Edges = new List<GraphEdge>();
    }

    public void AddEdge(GraphEdge edge)
    {
        Edges.Add(edge);
    }

    public void AddEdge(GraphVertex vertex, int edgeWeight)
    {
        AddEdge(new GraphEdge(vertex, edgeWeight));
    }

    public override string ToString()
    {
        return Name;
    }
}