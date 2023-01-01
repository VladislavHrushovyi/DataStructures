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
}