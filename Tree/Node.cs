namespace Tree;

public class Node<T>
{
    public IComparable Data;
    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }
    public Node<T> Parent { get; set; }
    public NodeColor Color { get; set; }

    public Node(IComparable data)
    {
        Data = data;
        Left = null;
        Right = null;
        Parent = null;
        Color = NodeColor.Red;
    }
}