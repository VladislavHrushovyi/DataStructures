namespace Tree;

public class AvlTree<T> : BinaryTree<T> where T : IComparable
{
    private int GetHeight(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private int Bfactor(Node<T> node)
    {
        return GetHeight(node.Right) - GetHeight(node.Left);
    }

    private void FixHeight(Node<T> node)
    {
        var heightLeft = GetHeight(node.Left);
        var heightRight = GetHeight(node.Right);

        node.Height = (heightLeft > heightRight ? heightLeft : heightRight) + 1;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> p = node.Right;
        node.Right = p.Left;
        p.Left = node;
        FixHeight(node);
        FixHeight(p);

        return p;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> p = node.Left;
        node.Left = p.Right;
        p.Right = node;
        FixHeight(node);
        FixHeight(p);

        return p;
    }

    private Node<T> Balance(Node<T> node)
    {
        FixHeight(node);
        if (Bfactor(node) == 2)
        {
            if (Bfactor(node.Right) < 0)
            {
                node.Right = RotateRight(node.Right);
            }

            return RotateLeft(node);
        }

        if (Bfactor(node) == -2)
        {
            if (Bfactor(node.Left) > 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            return RotateRight(node);
        }
        return node;
    }

    public new void Insert(T data)
    { 
        _root = InsertHandle(_root, data);
    }

    private Node<T> InsertHandle(Node<T> root, T data)
    {
        if (root == null)
        {
            return new Node<T>(data);
        }

        if (data.CompareTo(root.Data) < 0)
        {
            root.Left = InsertHandle(root.Left, data);
        }
        else
        {
            root.Right = InsertHandle(root.Right, data);
        }

        return Balance(root);
    }

    private Node<T> RemoveMin(Node<T> node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = RemoveMin(node.Left);
        return Balance(node);
    }

    public new void RemoveNode(T key)
    {
        _root = RemoveHandle(_root, key);
    }

    private Node<T> RemoveHandle(Node<T> node, T key)
    {
        if (node == null)
        {
            return node;
        }

        if (key.CompareTo(node.Data) < 0)
        {
            node.Left = RemoveHandle(node.Left, key);
        }
        else if(key.CompareTo(node.Data) > 0)
        {
            node.Right = RemoveHandle(node.Right, key);
        }
        else
        {
            Node<T> q = node.Left;
            Node<T> r = node.Left;
            if (r == null) return q;
            Node<T> min = FindMin(r);
            min.Right = RemoveMin(r);
            min.Left = q;
            return Balance(min);
        }

        return Balance(node);
    }
}