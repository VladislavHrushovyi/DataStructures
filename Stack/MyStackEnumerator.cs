using System.Collections;

namespace Stack;

public class MyStackEnumerator<T> : IEnumerator<StackNode<T>>
{
    private StackNode<T>? _current;
    private StackNode<T> _head;

    public MyStackEnumerator(StackNode<T> head)
    {
        _head = head;
    }

    public bool MoveNext()
    {
        _current = _head;
        if (_current == null) return false;

        _head = _head.Next;
        return true;
    }

    public void Reset()
    {
    }

    public StackNode<T> Current => _current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}