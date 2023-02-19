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
        Node<T> temp = (Node<T>)_root;

        while (temp != NIL)
        {
            y = temp;
            temp = z.Data.CompareTo(temp.Data) < 0 ? (Node<T>)temp.Left : (Node<T>)temp.Right;
        }

        z.Parent = y;

        if (y == NIL)
        {
            _root = z;
        }
        else if (z.Data.CompareTo(y.Data) < 0)
            y.Left = z;
        else
            y.Right = z;

        z.Right = NIL;
        z.Left = NIL;

        InsertFixup(z);
    }

    private void InsertFixup(Node<T> z)
    {
        while (z.Parent.Color == NodeColor.Red)
        {
            if (z.Parent == z.Parent.Parent.Left)
            {
                Node<T> y = (Node<T>)z.Parent.Parent.Right;

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
                Node<T> y = (Node<T>)z.Parent.Parent.Left;

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
        Node<T> item = Find(data);
        Node<T> X = null;
        Node<T> Y = null;

        if (item.Left == null || item.Right == null)
        {
            Y = item;
        }
        else
        {
            Y = TreeSuccessor(item);
        }

        if (Y.Left != null)
        {
            X = Y.Left;
        }
        else
        {
            X = Y.Right;
        }

        if (X != null)
        {
            X.Parent = Y;
        }

        if (Y.Parent == null)
        {
            _root = X;
        }
        else if (Y == Y.Parent.Left)
        {
            Y.Parent.Left = X;
        }
        else
        {
            Y.Parent.Left = X;
        }

        if (Y != item)
        {
            item.Data = Y.Data;
        }

        if (Y.Color == NodeColor.Black)
        {
            DeleteFixUp(X);
        }
    }

    private void DeleteFixUp(Node<T> x)
    {
        Node<T> s;
        while (x != _root && x.Color == NodeColor.Black) {
            if (x == x.Parent.Left) {
                s = x.Parent.Right;
                if (s.Color == NodeColor.Red) {
                    s.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    LeftRotate(x.Parent);
                    s = x.Parent.Right;
                }

                if (s.Left.Color == NodeColor.Black && s.Right.Color == NodeColor.Black) {
                    s.Color = NodeColor.Red;
                    x = x.Parent;
                } else {
                    if (s.Right.Color == NodeColor.Black) {
                        s.Left.Color = NodeColor.Black;
                        s.Color = NodeColor.Red;
                        RightRotate(s);
                        s = x.Parent.Right;
                    }

                    s.Color = x.Parent.Color;
                    x.Parent.Color = NodeColor.Black;
                    s.Right.Color = NodeColor.Black;
                    LeftRotate(x.Parent);
                    x = _root;
                }
            } else {
                s = x.Parent.Left;
                if (s.Color == NodeColor.Red) {
                    s.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    RightRotate(x.Parent);
                    s = x.Parent.Left;
                }

                if (s.Right.Color == NodeColor.Black && s.Right.Color == NodeColor.Black) {
                    s.Color = NodeColor.Red;
                    x = x.Parent;
                } else {
                    if (s.Left.Color == NodeColor.Black) {
                        s.Right.Color = NodeColor.Black;
                        s.Color = NodeColor.Red;
                        LeftRotate(s);
                        s = x.Parent.Left;
                    }

                    s.Color = x.Parent.Color;
                    x.Parent.Color = NodeColor.Black;
                    s.Left.Color = NodeColor.Black;
                    RightRotate(x.Parent);
                    x = _root;
                }
            }
        }
        x.Color = 0;
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
            x.Parent.Right = y;
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

    private Node<T> TreeSuccessor(Node<T> X)
    {
        if (X.Left != null)
        {
            return FindMin(X);
        }

        Node<T> Y = X.Parent;
        while (Y != null && X == Y.Right)
        {
            X = Y;
            Y = Y.Parent;
        }

        return Y;
    }
}
