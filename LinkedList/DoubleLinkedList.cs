using System.Collections;

namespace LinkedList;

public class DoubleLinkedList<T> : ILinkedList<T>, IEnumerable<Node<T>>, ICloneable
{
    public Node<T>? Head;
    public Node<T>? Last => GetLast();
    public int Length { get; private set; }

    public Node<T> this[int index]
    {
        get => GetNodeByIndex(index);
    }

    private Node<T> GetNodeByIndex(int index)
    {
        if (index < 0 || index > Length)
        {
            throw new Exception("Index out of range");
        }

        if (index == 0)
        {
            return Head;
        }

        if (index == Length - 1)
        {
            return Last;
        }
        int i = 0;
        var node = Head;
        while (i < index)
        {
            i++;
            node = node.Next;
        }

        return node;
    }

    private Node<T> GetLast()
    {
        var last = Head;
        while (last?.Next != null)
        {
            last = last.Next;
        }

        return last;
    }

    public void Push(T value)
    {
        var node = new Node<T>(value);

        node.Next = Head;
        node.Prev = null;

        if (Head != null)
        {
            Head.Prev = node;
        }

        Length++;
        Head = node;
    }

    public void Append(T value)
    {
        var newNode = new Node<T>(value);

        Node<T> last = Head;

        newNode.Next = null;

        if (Head == null)
        {
            Push(value);
            return;
        }

        while (last?.Next != null)
        {
            last = last.Next;
        }

        last.Next = newNode;
        newNode.Prev = last;
        Length++;
    }

    public void InsertBefore(Node<T> node, T value)
    {
        if (node == null)
        {
            throw new Exception("Node is null");
        }

        if (node.Prev == null)
        {
            Push(value);
            return;
        }

        var newNode = new Node<T>(value);
        newNode.Next = node;
        newNode.Prev = node.Prev;
        node.Prev = newNode;
        if (newNode.Prev != null)
        {
            newNode.Prev.Next = node.Prev;
        }

        Length++;
    }

    public void InsertAfter(Node<T> node, T value)
    {
        if (node == null)
        {
            throw new Exception("Node is null");
        }

        if (node.Next == null)
        {
            Append(value);
            return;
        }

        var newNode = new Node<T>(value)
        {
            Next = node.Next
        };
        node.Next = newNode;
        newNode.Prev = node;

        if (newNode.Next != null)
        {
            newNode.Next.Prev = node.Next;
        }

        Length++;
    }

    public void RemoveNode(Node<T> node)
    {
        if (node is {Prev: { }, Next: { }})
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;               
        }

        if (node is {Prev : null })
        {
            node.Next.Prev = null;
            Head = node.Next;
            
        }

        if (node is {Next : null})
        {
            node.Prev.Next = null;
        }
        
        Length--;
    }

    public void Display()
    {
        foreach (var item in this)
        {
            Console.WriteLine(item.Value);
        }
    }

    public Node<T> FindNode(Node<T> node)
    {
        return BinarySearch(this, 0, Length - 1, node);
    }

    private Node<T> BinarySearch(DoubleLinkedList<T> dll, int left, int right, Node<T> node)
    {
        if (left <= right)
        {
            var mid = left + (right - left) / 2;
            if (dll[mid].Equals(node)) return this[mid];

            if (dll[mid] > node) return BinarySearch(dll, left, mid - 1, node);

            return BinarySearch(dll, mid + 1, right, node);
        }

        return null;
    }
    
    public IEnumerator<Node<T>> GetEnumerator()
    {
        return new LinkedListEnumerator<T>(Head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
