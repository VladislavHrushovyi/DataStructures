namespace Tree;

public interface ITree<T>
{
    public void Insert(T data);
    public void TraversePreOrder();
    public void TraverseInOrder();
    public void TraversePostOrder();
    public void RemoveNode(T value);
    public bool Contain(T value);
}