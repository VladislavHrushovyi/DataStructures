namespace LinkedList;

public interface ILinkedList<T>
{
    public void Push(T value);
    public void Append(T value);
    public void InsertBefore(Node<T> node, T value);
    public void InsertAfter(Node<T> node, T value);
    public void RemoveNode(Node<T> node);
}