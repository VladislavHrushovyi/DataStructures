using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Stack;

public class MyStack<T> : IEnumerable<StackNode<T>>, IMyStack<T>
{
    private StackNode<T> _top;
    private int _count;
    public StackNode<T> Top
    {
        get => _top;
    }

    public int Count
    {
        get => _count;
    }

    public IEnumerator<StackNode<T>> GetEnumerator()
    {
        return new MyStackEnumerator<T>(_top);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new Exception("Stack is empty");
        }

        var itemObjData = _top.Data;
        _top = _top.Next;
        _count--;

        return itemObjData;
    }

    public void Push(T data)
    {
        var node = new StackNode<T>(data)
        {
            Next = _top
        };

        _top = node;
        _count++;
    }

    public T Peek()
    {
        if (!IsEmpty())
        {
            return _top.Data;
        }

        throw new Exception("Stack is empty");
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