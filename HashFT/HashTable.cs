using Tree;

namespace HashFT;

public class HashTable<TValue> where TValue : IComparable
{
    private AvlTree<TValue>[] Table = new AvlTree<TValue>[100];

    public void Add(TValue item)
    {
        var hashValue = HashGenerator.ByDigitFolding(item.ToString());
        if (Table[hashValue] == null)
        {
            Table[hashValue] = new AvlTree<TValue>();
        }
        Table[hashValue].Insert(item);
    }

    public void Remove(TValue val)
    {
        var hasValue = HashGenerator.ByDigitFolding(val.ToString());
        Table[hasValue] = null;
    }

    public IComparable Find(TValue key)
    {
        var hash = HashGenerator.ByDigitFolding(key.ToString());
        if (Table[hash] != null)
        {
            return Table[hash].Find(key).Data;
        }

        return null;
    }

    public void Display()
    {
        for(int i = 0; i < Table.Length; i++)
        {
            if (Table[i] != null)
            {
                Console.WriteLine($"Index {i}");
                Table[i].TraverseInOrder();
                Console.WriteLine();
            }
        }
    }
}