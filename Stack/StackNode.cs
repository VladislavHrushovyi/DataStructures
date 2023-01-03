namespace Stack;

public class StackNode<T>
{
    public T Data { get; set; }
    public StackNode<T> Next { get; set; }

    public StackNode(T item)
    {
        Data = item;
    }
}