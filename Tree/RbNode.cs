using System.Numerics;

namespace Tree;

public class RbNode
{
    public INumber<int> Data;
    public RbNode Left;
    public RbNode Right;
    public RbNode Parent;
    public NodeColor Color = NodeColor.Black;
    public RbNode(INumber<int> data) : this(data, null, null, null)
    {
    }

    public RbNode(INumber<int> data, RbNode left, RbNode right, RbNode parent)
    {
        this.Data = data;
        this.Left = left;
        this.Right = right;
        this.Parent = parent;
    }
}