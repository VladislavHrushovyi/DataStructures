using System.Runtime.CompilerServices;
using Tree;

namespace HashFT;

public class HashTable<TValue> where TValue : IComparable
{
    private readonly int Capacity;
    private AvlTree<TValue>[] Table;

    public HashTable(int capacity)
    {
        Capacity = capacity;
        Table = new AvlTree<TValue>[Capacity];
    }

    public void Add(TValue item)
    {
        var hashValue = HashGenerator.ByDigitFolding(item.ToString()) % Capacity;
        if (Table[hashValue] == null)
        {
            Table[hashValue] = new AvlTree<TValue>();
        }
        Table[hashValue].Insert(item);
    }

    public void Remove(TValue val)
    {
        var hashValue = HashGenerator.ByDigitFolding(val.ToString()) % Capacity;
        Table[hashValue].RemoveNode(val);
    }

    public IComparable Find(TValue key)
    {
        var hash = HashGenerator.ByDigitFolding(key.ToString()) % Capacity;
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