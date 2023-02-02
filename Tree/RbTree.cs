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

    // private int Compare(IComparable data, RbNode node)
    // {
    //     if (node != _root) return data.CompareTo(node.Data);
    //     else return 1;
    // }

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
        } else if (newNode.Data.CompareTo(y.Data) < 0)
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
            if(newNode == _root) break;
        }

        _root.Color = NodeColor.Black;
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
        } else if (newNode == newNode.Parent.Right)
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
        } else if (newNode == newNode.Parent.Left)
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
        if (root != _TNULL) {
            DisplayHelper(root.Left);
            Console.Write(root.Data + " ");
            DisplayHelper(root.Right);
        }
    }
}