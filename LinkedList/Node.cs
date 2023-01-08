using System.Numerics;

namespace LinkedList;

public class Node<T>
{
    public T? Value;
    public Node<T>? Prev;
    public Node<T>? Next;

    public Node(T value)
    {
        this.Value = value;
    }

    public static bool operator >(Node<T> node1, Node<T> node2)
    {
        if (node1.Value is int value1 && node2.Value is int value2)
        {
            return value1 > value2;
        }

        throw new Exception("Arguments is not a type int");
    }

    public static bool operator <(Node<T> node1, Node<T> node2)
    {
        if (node1.Value is int value1 && node2.Value is int value2)
        {
            return value1 < value2;
        }

        throw new Exception("Arguments is not a type int");
    }
}