using Stack;

namespace Graph;

public class BDFS : DijkstraAlgorithm
{
    public BDFS(MyGraph graph) : base(graph)
    {
    }

    public void BFS(string startVertexName)
    {
        MyQueue<GraphVertexInfo> queue = new MyQueue<GraphVertexInfo>();
        var startVertex = this.Graph.FindVertex(startVertexName);
        var startVertexInfo = GetVertexInfo(startVertex);
        startVertexInfo.IsUnvisited = false;
        
        queue.EnQueue(startVertexInfo);

        while (queue.Count != 0)
        {
            GraphVertexInfo currentVertexInfo = queue.DeQueue();
            Console.Write(currentVertexInfo.Vertex + " ");

            foreach (var edge in currentVertexInfo.Vertex.Edges)
            {
                var adjacentVertexInfo = GetVertexInfo(edge.ConnectedVertex);
                if (adjacentVertexInfo.IsUnvisited)
                {
                    adjacentVertexInfo.IsUnvisited = false;
                    queue.EnQueue(adjacentVertexInfo);
                }
            }
        }
    }

    public void DFS(string startVertexName)
    {
        var startVertex = Graph.FindVertex(startVertexName);
        var startVertexInfo = GetVertexInfo(startVertex);
        startVertexInfo.IsUnvisited = true;
        Console.Write(startVertexInfo.Vertex + " ");

        foreach (var edge in startVertexInfo.Vertex.Edges)
        {
            var adjacentInfo = GetVertexInfo(edge.ConnectedVertex);
            if (!adjacentInfo.IsUnvisited)
            {
                DFS(edge.ConnectedVertex.Name);
            }
        }
    }
}