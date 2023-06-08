

using System.Diagnostics;
using SortMethods.SorterLargeFile;

var stopwatch = new Stopwatch();
string pathFile = await new FileCreator(20_000_000).CreateLargeFile();

var sorterLargeFile = new SorterLargeFile(2_000_000, pathFile);

stopwatch.Start();
await sorterLargeFile.Sort();
stopwatch.Stop();

Console.WriteLine(stopwatch.Elapsed);
Console.ReadKey();

// var sDll = new SorterDll();
// sDll.Display();
// Console.WriteLine("##########################################");
//sDll.BubbleSort();
//sDll.InsertionSort();
//sDll.SelectionSort();
//sDll.MergeSort();
// sDll.QuickSort();
// sDll.Display();
