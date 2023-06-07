

using SortMethods.SorterLargeFile;

string pathFile = new FileCreator(1_000_000).CreateLargeFile();

var sorterLargeFile = new SorterLargeFile(100_000, pathFile);

sorterLargeFile.Sort();

// var sDll = new SorterDll();
// sDll.Display();
// Console.WriteLine("##########################################");
//sDll.BubbleSort();
//sDll.InsertionSort();
//sDll.SelectionSort();
//sDll.MergeSort();
// sDll.QuickSort();
// sDll.Display();
