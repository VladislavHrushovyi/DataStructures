namespace Stack;

public interface IMyStack<T>
{
    public void Pop();
    public void Push(T data);
    public T Peek();
    public bool IsEmpty();
    public void Display();
}