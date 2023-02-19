namespace Tree;

public class RbTree<T> : BinaryTree<T> where T : IComparable
{
    Node<T> NIL;

    public RbTree()
    {
        Node<T> nilNode = new Node<T>(null);
        nilNode.Color = NodeColor.Black;
        NIL = nilNode;
        _root = NIL;
    }

    public new void Insert(T data)
    {
        Node<T> z = new Node<T>(data);
        Node<T> y = NIL;
        Node<T> x = _root;

        while (x != NIL)
        {
            y = x;
            x = z.Data.CompareTo(x.Data) < 0 ? x.Left : x.Right;
        }

        z.Parent = y;

        if (y == NIL)
        {
            _root = z;
        }
        else if (z.Data.CompareTo(y.Data) < 0)
        {
            y.Left = z;
        }
        else
        {
            y.Right = z;
        }

        z.Left = NIL;
        z.Right = NIL;
        z.Color = NodeColor.Red;

        InsertFixup(z);
    }

    private void InsertFixup(Node<T> z)
    {
        while (z.Parent.Color == NodeColor.Red)
        {
            if (z.Parent == z.Parent.Parent.Left)
            {
                Node<T> y = z.Parent.Parent.Right;

                if (y.Color == NodeColor.Red)
                {
                    z.Parent.Color = NodeColor.Black;
                    y.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Right)
                    {
                        z = z.Parent;
                        LeftRotate(z);
                    }

                    z.Parent.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    RightRotate(z.Parent.Parent);
                }
            }
            else
            {
                Node<T> y = z.Parent.Parent.Left;

                if (y.Color == NodeColor.Red)
                {
                    z.Parent.Color = NodeColor.Black;
                    y.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Left)
                    {
                        z = z.Parent;
                        RightRotate(z);
                    }

                    z.Parent.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    LeftRotate(z.Parent.Parent);
                }
            }
        }

        _root.Color = NodeColor.Black;
    }

    public new void RemoveNode(T data)
    {
        Node<T> z = Find(data);

        if (z == NIL)
        {
            return; // the node to remove is not in the tree
        }

        Node<T> x;
        Node<T> y = z;

        NodeColor yOriginalColor = y.Color;

        if (z.Left == NIL)
        {
            x = z.Right;
            Transplant(z, z.Right);
        }
        else if (z.Right == NIL)
        {
            x = z.Left;
            Transplant(z, z.Left);
        }
        else
        {
            y = FindMin(z.Right);
            yOriginalColor = y.Color;
            x = y.Right;

            if (y.Parent == z)
            {
                x.Parent = y;
            }
            else
            {
                Transplant(y, y.Right);
                y.Right = z.Right;
                y.Right.Parent = y;
            }

            Transplant(z, y);
            y.Left = z.Left;
            y.Left.Parent = y;
            y.Color = z.Color;
        }

        if (yOriginalColor == NodeColor.Black)
        {
            DeleteFixUp(x);
        }
    }

    private void Transplant(Node<T> u, Node<T> v)
    {
        if (u.Parent == NIL)
        {
            _root = v;
        }
        else if (u == u.Parent.Left)
        {
            u.Parent.Left = v;
        }
        else
        {
            u.Parent.Right = v;
        }

        v.Parent = u.Parent;
    }

    private void DeleteFixUp(Node<T> x)
    {
        while (x != _root && x.Color == NodeColor.Black) {
            if (x == x.Parent.Left) {
                Node<T> w = x.Parent.Right;
                if (w.Color == NodeColor.Red) {
                    w.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    LeftRotate(x.Parent);
                    w = x.Parent.Right;
                }
                if (w.Left.Color == NodeColor.Black && w.Right.Color == NodeColor.Black) {
                    w.Color = NodeColor.Red;
                    x = x.Parent;
                } else {
                    if (w.Right.Color == NodeColor.Black) {
                        w.Left.Color = NodeColor.Black;
                        w.Color = NodeColor.Red;
                        RightRotate(w);
                        w = x.Parent.Right;
                    }
                    w.Color = x.Parent.Color;
                    x.Parent.Color = NodeColor.Black;
                    w.Right.Color = NodeColor.Black;
                    LeftRotate(x.Parent);
                    x = _root;
                }
            } else {
                Node<T> w = x.Parent.Left;
                if (w.Color == NodeColor.Red) {
                    w.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    RightRotate(x.Parent);
                    w = x.Parent.Left;
                }
                if (w.Right.Color == NodeColor.Black && w.Left.Color == NodeColor.Black) {
                    w.Color = NodeColor.Red;
                    x = x.Parent;
                } else {
                    if (w.Left.Color == NodeColor.Black) {
                        w.Right.Color = NodeColor.Black;
                        w.Color = NodeColor.Red;
                        LeftRotate(w);
                        w = x.Parent.Left;
                    }
                    w.Color = x.Parent.Color;
                    x.Parent.Color = NodeColor.Black;
                    w.Left.Color = NodeColor.Black;
                    RightRotate(x.Parent);
                    x = _root;
                }
            }
        }
        x.Color = NodeColor.Black;
    }

    private void LeftRotate(Node<T> x)
    {
        Node<T> y = x.Right;
        x.Right = y.Left;
        if (y.Left != NIL)
        {
            y.Left.Parent = x;
        }

        y.Parent = x.Parent;
        if (x.Parent == NIL)
        {
            _root = y;
        }
        else if (x == x.Parent.Left)
        {
            x.Parent.Left = y;
        }
        else
        {
            y.Parent.Right = y;
        }

        y.Left = x;
        x.Parent = y;
    }

    private void RightRotate(Node<T> x)
    {
        Node<T> y = x.Left;
        x.Left = y.Right;
        if (y.Right != NIL)
        {
            y.Right.Parent = x;
        }

        y.Parent = x.Parent;
        if (x.Parent == NIL)
        {
            _root = y;
        }
        else if (x == x.Parent.Right)
        {
            x.Parent.Right = y;
        }
        else
        {
            x.Parent.Left = y;
        }

        y.Right = x;
        x.Parent = y;
    }
}