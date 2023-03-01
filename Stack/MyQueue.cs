using System.Reflection.Metadata.Ecma335;

namespace Stack;

public class MyQueue<T>
{
    private readonly MyStack<T> _stack1 = new();
    private readonly MyStack<T> _stack2 = new();
    private int _count;

    public int Count => _count;

    public void EnQueue(T data)
    {
        while (!_stack1.IsEmpty())
        {
            _stack2.Push(_stack1.Pop());
        }
        
        _stack1.Push(data);

        while (!_stack2.IsEmpty())
        {
            _stack1.Push(_stack2.Pop());
        }

        _count++;
    }

    public T DeQueue()
    {
        if (_stack1.IsEmpty())
        {
            throw new Exception("Queue is empty");
        }

        var itemObjData = _stack1.Peek();
        _stack1.Pop();

        _count--;
        return itemObjData;
    }
}