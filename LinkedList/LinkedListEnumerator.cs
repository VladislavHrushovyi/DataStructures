using System.Collections;

namespace LinkedList;

public class LinkedListEnumerator<T> : IEnumerator<Node<T>>
{
    private Node<T> _current; 
    public LinkedListEnumerator(Node<T> head)
    {
        _current = head;
    }
    public bool MoveNext()
    {
        Current = _current;
        if (Current == null) return false;
        _current = Current.Next;
        return true;

    }

    public void Reset()
    {
    }

    public Node<T> Current { get; set; }
    
    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}