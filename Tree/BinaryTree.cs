using System.Numerics;

namespace Tree;

public class BinaryTree<T> : ITree<T> where T : INumber<T>
{
    private TreeNode<T> _root;

    public TreeNode<T> this[int index]
    {
        get => GetNodeByIndex(index);
    }

    private TreeNode<T> GetNodeByIndex(int index)
    {
        _counter = 0;
        return FindNodeByIndex(index, _root);
    }

    private int _counter;
    private TreeNode<T> FindNodeByIndex(int index, TreeNode<T> root)
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

    private TreeNode<T> ExecuteInsert(T data, TreeNode<T>? root)
    {
        if (root == null)
        {
            root = new TreeNode<T>(data);
        }
        else if (data < root.Data)
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

    private void ExecuteTraversePreOrder(TreeNode<T> root)
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

    private void ExecuteTraverseInOrder(TreeNode<T> root)
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

    private int ExecuteGetTreeDepth(TreeNode<T> root)
    {
        return root == null ? 0 : Math.Max(ExecuteGetTreeDepth(root.Left), ExecuteGetTreeDepth(root.Right)) + 1;
    }

    private bool ExecuteContain(T value, TreeNode<T> root)
    {
        if (root != null)
        {
            if (root.Data == value) return true;
            else if (value < root.Data) return ExecuteContain(value, root.Left);
            else if (value > root.Data) return ExecuteContain(value, root.Right);
        }

        return false;
    }

    private TreeNode<T> ExecuteRemove(T value, TreeNode<T> root)
    {
        if (root != null)
        {
            if (value < root.Data)
            {
                root.Left = ExecuteRemove(value, root.Left);
            }
            else if (value > root.Data)
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

    private TreeNode<T> FindMax(TreeNode<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }

    private void ExecuteTraversePostOrder(TreeNode<T> root)
    {
        if (root != null)
        {
            ExecuteTraversePostOrder(root.Left);
            ExecuteTraversePostOrder(root.Right);
            Console.Write(root.Data + " ");
        }
    }
}