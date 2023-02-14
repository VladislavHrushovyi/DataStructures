using System.Numerics;

namespace Tree;

public class BinaryTree<T> : ITree<T> where T : IComparable
{
    protected Node<T> _root;

    public Node<T> this[int index]
    {
        get => GetNodeByIndex(index);
    }

    protected Node<T> GetNodeByIndex(int index)
    {
        _counter = 0;
        return FindNodeByIndex(index, _root);
    }

    private int _counter;

    protected Node<T> FindNodeByIndex(int index, Node<T> root)
    {
        if (root == null)
        {
            return null;
        }

        var leftNode = FindNodeByIndex(index, root.Left);

        if (leftNode != null)
        {
            return leftNode;
        }

        _counter++;
        if (index == _counter)
        {
            return root;
        }

        return FindNodeByIndex(index, root.Right);
    }

    protected Node<T> ExecuteInsert(T data, Node<T>? root)
    {
        if (root == null)
        {
            root = new Node<T>(data);
        }
        else if (data.CompareTo(root.Data) < 0)
        {
            root.Left = ExecuteInsert(data, root.Left);
        }
        else
        {
            root.Right = ExecuteInsert(data, root.Right);
        }

        return root;
    }

    public void Insert(T data)
    {
        _root = ExecuteInsert(data, _root);
    }

    public void TraversePreOrder()
    {
        ExecuteTraversePreOrder(_root);
    }

    protected void ExecuteTraversePreOrder(Node<T> root)
    {
        if (root != null)
        {
            Console.Write(root.Data + " ");
            ExecuteTraversePreOrder(root.Left);
            ExecuteTraversePreOrder(root.Right);
        }
    }

    public void TraverseInOrder()
    {
        ExecuteTraverseInOrder(_root);
    }

    private void ExecuteTraverseInOrder(Node<T> root)
    {
        if (root != null)
        {
            ExecuteTraverseInOrder(root.Left);
            Console.Write(root.Data + " ");
            ExecuteTraverseInOrder(root.Right);
        }
    }

    public void TraversePostOrder()
    {
        ExecuteTraversePostOrder(_root);
    }

    public void RemoveNode(T value)
    {
        ExecuteRemove(value, _root);
    }

    public bool Contain(T value)
    {
        return ExecuteContain(value, _root);
    }

    public int GetTreeDepth()
    {
        return ExecuteGetTreeDepth(_root);
    }

    public Node<T> Find(T key)
    {
        Node<T> iterNode = _root;
        while (iterNode != null)
        {
            if (iterNode.Data.CompareTo(key) == 0)
                return iterNode;
            if (iterNode.Data.CompareTo(key) > 0)
                iterNode = iterNode.Left;
            else
                iterNode = iterNode.Right;
        }

        if (iterNode is null )
        {
            throw new Exception($"Tree doesnt contain node by key {key}");
        }

        return iterNode;
    }

    protected int ExecuteGetTreeDepth(Node<T> root)
    {
        return root == null ? 0 : Math.Max(ExecuteGetTreeDepth(root.Left), ExecuteGetTreeDepth(root.Right)) + 1;
    }

    protected bool ExecuteContain(T value, Node<T> root)
    {
        if (root != null)
        {
            if (root.Data.CompareTo(value) == 0) return true;
            else if (value.CompareTo(root.Data) < 0) return ExecuteContain(value, root.Left);
            else if (value.CompareTo(root.Data) > 0) return ExecuteContain(value, root.Right);
        }

        return false;
    }

    protected Node<T> ExecuteRemove(IComparable value, Node<T> root)
    {
        if (root != null)
        {
            if (value.CompareTo(root.Data) < 0)
            {
                root.Left = ExecuteRemove(value, root.Left);
            }
            else if (value.CompareTo(root.Data) > 0)
            {
                root.Right = ExecuteRemove(value, root.Right);
            }
            else
            {
                if (root is {Right: null, Left: null})
                {
                    root = null;
                }
                else if (root is {Right: not null, Left: not null})
                {
                    var maxNode = FindMax(root.Right);

                    root.Data = maxNode.Data;
                    root.Right = ExecuteRemove(maxNode.Data, root.Right);
                }
                else
                {
                    var child = root.Left != null ? root.Left : root.Right;
                    root = child;
                }
            }
        }

        return root;
    }

    protected Node<T> FindMax(Node<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }
    
    protected Node<T> FindMin(Node<T> X)
    {
        while (X.Left.Left != null)
        {
            X = X.Left;
        }
        if (X.Left.Right != null)
        {
            X = X.Left.Right;
        }
        return X;
    }

    protected void ExecuteTraversePostOrder(Node<T> root)
    {
        if (root != null)
        {
            ExecuteTraversePostOrder(root.Left);
            ExecuteTraversePostOrder(root.Right);
            Console.Write(root.Data + " ");
        }
    }
}