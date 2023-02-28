namespace Graph;

public class GraphEdge
{
    public GraphVertex ConnectedVertex { get; }
    public int EdgeWeight { get; }
    
    public GraphEdge(GraphVertex connectedVertex, int edgeWeight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = edgeWeight;
    }
}