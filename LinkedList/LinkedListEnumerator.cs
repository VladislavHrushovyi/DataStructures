using System.Collections;

namespace LinkedList;

public class LinkedListEnumerator<T> : IEnumerator<Node<T>>
{
    private Node<T> _dll;
    private Node<T> _current;
    public LinkedListEnumerator(Node<T> head)
    {
        _dll = head;
    }
    public bool MoveNext()
    {
        _current = _dll;
        if (_current == null) return false;
        _dll = _dll.Next;
        return true;

    }

    public void Reset()
    {
    }

    public Node<T> Current => _current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}