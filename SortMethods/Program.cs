

using System.Diagnostics;
using SortMethods.SorterLargeFile;

var stopwatch = new Stopwatch();
string pathFile = new FileCreator(200_000_000).CreateLargeFile();

var sorterLargeFile = new SorterLargeFile(20_000_000, pathFile);

stopwatch.Start();
sorterLargeFile.Sort();
stopwatch.Stop();

Console.WriteLine(stopwatch.Elapsed);

// var sDll = new SorterDll();
// sDll.Display();
// Console.WriteLine("##########################################");
//sDll.BubbleSort();
//sDll.InsertionSort();
//sDll.SelectionSort();
//sDll.MergeSort();
// sDll.QuickSort();
// sDll.Display();
