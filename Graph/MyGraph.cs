using Stack;

namespace Graph;

public class MyGraph
{
    public List<GraphVertex?> Vertices { get; set; }
    
    public MyGraph()
    {
        Vertices = new List<GraphVertex?>();
    }

    public void AddVertex(string name)
    {
        Vertices.Add(new GraphVertex(name));
    }

    public GraphVertex? FindVertex(string name)
    {
        return Vertices.SingleOrDefault(v => v?.Name == name);
    }

    public void AddEdge(string firstNameVertex, string secondNameVertex, int weightVertex)
    {
        var v1 = FindVertex(firstNameVertex);
        var v2 = FindVertex(secondNameVertex);
        if (v1 != null && v2 != null)
        {
            v1.AddEdge(v2, weightVertex);
            v2.AddEdge(v1, weightVertex);
        }
    }

    public void Display()
    {
        foreach (var v in Vertices)
        {
            Console.WriteLine(v + ":");
            foreach (var e in v.Edges)
            {
                Console.Write(e.EdgeWeight + " > " + e.ConnectedVertex + ", ");
            }
            
            Console.WriteLine();
        }
    }
}