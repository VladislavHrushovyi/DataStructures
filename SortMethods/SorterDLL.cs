using System.Collections.Concurrent;
using LinkedList;

namespace SortMethods;

public class SorterDll : DoubleLinkedList<int>, ISortable
{
    public SorterDll()
    {
        FillRandomNumbers();
    }

    private void FillRandomNumbers()
    {
        for (int i = 0; i < 10; i++)
        {
            Push(new Random().Next(1,100));
            //Append(i);
        }
    }

    public void BubbleSort()
    {
        for (int i = 0; i < Length - 1; i++)
        {
            for (int j = 0; j < Length - i - 1; j++)
            {
                if (this[j].Value > this[j + 1].Value)
                {
                    SwapNode(this[j], this[j + 1]);
                }
            }
        }
    }

    public void InsertionSort()
    {
        for (int i = 1; i < Length; i++)
        {
            var currValue = this[i].Value;
            int j = i - 1;

            while (j >= 0 && this[j].Value > currValue)
            {
                this[j + 1].Value = this[j].Value;
                j -= 1;
            }

            this[j + 1].Value = currValue;
        }
    }

    public void SelectionSort()
    {
        for (int i = 0; i < Length; i++)
        {
            var minIdx = i;
            for (int j = i + 1; j < Length; j++)
            {
                if (this[minIdx].Value > this[j].Value)
                {
                    minIdx = j;
                }
            }
            
            SwapNode(this[i], this[minIdx]);
        }
    }

    public void MergeSort()
    {
        var dll = Clone();
        ExecuteSortArray((DoubleLinkedList<int>) dll,0, Length - 1);
    }

    private void ExecuteSortArray(DoubleLinkedList<int> dll, int left, int right)
    {
        if (left < right)
        {
            var mid = left + (right - left) / 2;
            ExecuteSortArray(dll, left, mid);
            ExecuteSortArray( dll,mid + 1, right);

            MergeArray(dll,left, mid, right);
        }

        Head = dll.Head;
    }

    private void MergeArray(DoubleLinkedList<int> dll, int left, int mid, int right)
    {
        var leftArrayLength = mid - left + 1;
        var rightArrayLength = right - mid;
        var leftTempDll = new DoubleLinkedList<int>();
        var rightTempDll = new DoubleLinkedList<int>();

        int i, j;

        for (i = 0; i < leftArrayLength; ++i)
        {
            leftTempDll.Append(this[left + i].Value);
        }
        
        for (j = 0; j < rightArrayLength; ++j)
        {
            rightTempDll.Append(this[mid + 1 + j].Value);
        }

        i = 0;
        j = 0;
        int k = left;
        
        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (leftTempDll[i].Value <= rightTempDll[j].Value)
            {
                dll[k++].Value = leftTempDll[i++].Value;
            }
            else
            {
                dll[k++].Value = rightTempDll[j++].Value;
            }
        }

        while (i < leftArrayLength)
        {
            dll[k++].Value = leftTempDll[i++].Value;
        }

        while (j < rightArrayLength)
        {
            dll[k++].Value = rightTempDll[j++].Value;
        }
    }

    public void QuickSort()
    {
        ExecuteQuickSort(this, 0, Length - 1);
    }

    private void ExecuteQuickSort(DoubleLinkedList<int> dll, int low, int high)
    {
        if (low < high)
        {
            var pi = Partition(dll, low, high);
            
            ExecuteQuickSort(dll, low, pi - 1);
            ExecuteQuickSort(dll, pi + 1, high);
        }
    }

    private int Partition(DoubleLinkedList<int> dll, int low, int high)
    {
        var pivot = dll[high];
        var i = low - 1;

        for (int j = low; j <= high - 1; j++)
        {
            if (dll[j].Value < pivot.Value)
            {
                i++;
                SwapNode(dll[i], dll[j]);
            }
        }
        
        SwapNode(dll[i + 1], dll[high]);

        return ++i;
    }

    private void SwapNode(Node<int> node1, Node<int> node2)
    {
        (node1.Value, node2.Value) = (node2.Value, node1.Value);
    }
}