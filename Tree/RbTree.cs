using System.Numerics;

namespace Tree;

public class RbTree
{
    private RbNode _root;
    private RbNode _TNULL;

    public RbTree()
    {
        _TNULL = new RbNode(null);
        _TNULL.Color = NodeColor.Black;
        _TNULL.Left = null;
        _TNULL.Right = null;
        _root = _TNULL;
    }

    public void Insert(INumber<int> data)
    {
        RbNode newNode = new RbNode(data);
        newNode.Parent = null;
        newNode.Data = data;
        newNode.Left = _TNULL;
        newNode.Right = _TNULL;
        newNode.Color = NodeColor.Red;

        RbNode y = null;
        RbNode x = this._root;

        while (x != _TNULL)
        {
            y = x;
            if (newNode.Data.CompareTo(x.Data) < 0)
            {
                x = x.Left;
            }
            else
            {
                x = x.Right;
            }
        }

        newNode.Parent = y;
        if (y == null)
        {
            _root = newNode;
        }
        else if (newNode.Data.CompareTo(y.Data) < 0)
        {
            y.Left = newNode;
        }
        else
        {
            y.Right = newNode;
        }

        if (newNode.Parent == null)
        {
            newNode.Color = NodeColor.Black;
            return;
        }

        if (newNode.Parent.Parent == null)
        {
            return;
        }

        FixInsert(newNode);
    }

    private void FixInsert(RbNode newNode)
    {
        RbNode u;
        while (newNode.Parent.Color == NodeColor.Red)
        {
            if (newNode.Parent == newNode.Parent.Parent.Right)
            {
                u = newNode.Parent.Parent.Left;
                if (u.Color == NodeColor.Red)
                {
                    u.Color = NodeColor.Black;
                    newNode.Parent.Color = NodeColor.Black;
                    newNode.Parent.Parent.Color = NodeColor.Red;
                    newNode = newNode.Parent.Parent;
                }
                else
                {
                    if (newNode == newNode.Parent.Left)
                    {
                        newNode = newNode.Parent;
                        RightRotate(newNode);
                    }

                    newNode.Parent.Color = NodeColor.Black;
                    newNode.Parent.Parent.Color = NodeColor.Red;
                    LeftRotate(newNode.Parent.Parent);
                }
            }
            else
            {
                u = newNode.Parent.Parent.Right;

                if (u.Color == NodeColor.Red)
                {
                    u.Color = NodeColor.Black;
                    newNode.Parent.Color = NodeColor.Black;
                    newNode.Parent.Parent.Color = NodeColor.Red;
                    newNode = newNode.Parent.Parent;
                }
                else
                {
                    if (newNode == newNode.Parent.Right)
                    {
                        newNode = newNode.Parent;
                        LeftRotate(newNode);
                    }

                    newNode.Parent.Color = NodeColor.Black;
                    newNode.Parent.Parent.Color = NodeColor.Red;
                    RightRotate(newNode.Parent.Parent);
                }
            }

            if (newNode == _root) break;
        }

        _root.Color = NodeColor.Black;
    }

    public void Delete(INumber<int> data)
    {
        DeleteHelper(_root, data);
    }

    private void DeleteHelper(RbNode root, INumber<int> key)
    {
        RbNode z = _TNULL;
        RbNode x, y;

        while (root != _TNULL)
        {
            if (root.Data.CompareTo(key) == 0)
            {
                z = root;
            }

            root = root.Data.CompareTo(key) > 0 ? root.Left : root.Right;
        }

        if (z == _TNULL)
        {
            throw new ArgumentException($"Tree doesn`t contains the key: {key}");
        }

        y = z;
        int yOriginalColor = (int) y.Color;
        if (z.Left == _TNULL)
        {
            x = z.Right;
            RbTransplant(z, z.Right);
        }
        else if (z.Right == _TNULL)
        {
            x = z.Left;
            RbTransplant(z, z.Left);
        }
        else
        {
            y = Minimum(z.Right);
            yOriginalColor = (int) y.Color;
            x = y.Right;
            if (y.Parent == z)
            {
                x.Parent = z;
            }
            else
            {
                RbTransplant(y, y.Right);
                y.Right = z.Right;
                y.Right.Parent = y;
            }

            RbTransplant(y, z);
            y.Left = z.Left;
            y.Left.Parent = y;
            y.Color = z.Color;
        }

        if (yOriginalColor == (int) NodeColor.Black)
        {
            FixDelete(x);
        }
    }

    private void FixDelete(RbNode x)
    {
        RbNode s;
        while (x != _root && x.Color == NodeColor.Black)
        {
            if (x == x.Parent.Left)
            {
                s = x.Parent.Left;
                if (s.Color == NodeColor.Red)
                {
                    s.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    LeftRotate(x.Parent);
                    s = x.Parent.Right;
                }

                if (s.Left.Color == NodeColor.Black && s.Right.Color == NodeColor.Black)
                {
                    s.Color = NodeColor.Red;
                    x = x.Parent;
                }
                else
                {
                    if (s.Right.Color == NodeColor.Black)
                    {
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
            }
            else
            {
                s = x.Parent.Left;
                if (s.Color == NodeColor.Red)
                {
                    s.Color = NodeColor.Black;
                }
                else
                {
                    if (s.Left.Color == NodeColor.Black)
                    {
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

        x.Color = NodeColor.Black;
    }

    public RbNode Minimum(RbNode node)
    {
        while (node.Left != _TNULL)
        {
            node = node.Left;
        }

        return node;
    }

    private void RbTransplant(RbNode u, RbNode v)
    {
        if (u.Parent == null)
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

    private void RightRotate(RbNode newNode)
    {
        RbNode y = newNode.Left;
        newNode.Left = y.Right;

        if (y.Right == _TNULL)
        {
            y.Right.Parent = newNode;
        }

        y.Parent = newNode.Parent;
        if (newNode.Parent == null)
        {
            _root = y;
        }
        else if (newNode == newNode.Parent.Right)
        {
            newNode.Parent.Right = y;
        }
        else
        {
            newNode.Parent.Left = y;
        }

        y.Right = newNode;
        newNode.Parent = y;
    }

    private void LeftRotate(RbNode newNode)
    {
        RbNode y = newNode.Right;
        newNode.Right = y.Left;

        if (y.Left == _TNULL)
        {
            y.Left.Parent = newNode;
        }

        y.Parent = newNode.Parent;
        if (newNode.Parent == null)
        {
            _root = y;
        }
        else if (newNode == newNode.Parent.Left)
        {
            newNode.Parent.Left = y;
        }
        else
        {
            newNode.Parent.Right = y;
        }

        y.Left = newNode;
        newNode.Parent = y;
    }

    public void Display()
    {
        DisplayHelper(this._root);
    }

    private void DisplayHelper(RbNode root)
    {
        if (root != _TNULL)
        {
            DisplayHelper(root.Left);
            Console.Write(root.Data + " ");
            DisplayHelper(root.Right);
        }
    }
}