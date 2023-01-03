using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Stack;

public class MyStack<T> : IEnumerable<StackNode<T>>, IMyStack<T>
{
    private StackNode<T> _top;
    public StackNode<T> Top
    {
        get => _top;
    }

    public IEnumerator<StackNode<T>> GetEnumerator()
    {
        return new MyStackEnumerator<T>(_top);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Pop()
    {
        if (IsEmpty())
        {
            throw new Exception("Stack is empty");
        }

        _top = _top.Next;
    }

    public void Push(T data)
    {
        var node = new StackNode<T>(data)
        {
            Next = _top
        };

        _top = node;
    }

    public T Peek()
    {
        if (!IsEmpty())
        {
            return _top.Data;
        }
        else
        {
            throw new Exception("Stack is empty");
        }
    }

    public bool IsEmpty()
    {
        return _top == null;
    }

    public void Display()
    {
        foreach (var item in this)
        {
            Console.WriteLine(item.Data);
        }
    }
}