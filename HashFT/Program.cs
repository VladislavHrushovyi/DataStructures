
using HashFT;

// int value = 555;
// string data = "Hello wowrld";
// Console.WriteLine("____________DIVIDING METHOD____________");
// Console.WriteLine($"Value {value} have hash {HashGenerator.ByDividing(value)}");
// Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByDividing(data)}");
// Console.WriteLine("____________END_____________");
//
// Console.WriteLine("_____________MID-SQUARE METHOD____________");
// Console.WriteLine($"Value {value} have hash {HashGenerator.ByMidSquare(value)}");
// Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByMidSquare(data)}");
// Console.WriteLine("____________END_____________");
//
// Console.WriteLine("_____________DIGIT FOLDING METHOD____________");
// Console.WriteLine($"Value {value} have hash {HashGenerator.ByDigitFolding(value)}");
// Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByDigitFolding(data)}");
// Console.WriteLine("____________END_____________");
//
// Console.WriteLine("_____________DIGIT FOLDING METHOD____________");
// Console.WriteLine($"Value {value} have hash {HashGenerator.ByMultiplication(value)}");
// Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByMultiplication(data)}");
// Console.WriteLine("____________END_____________");

var hashTable = new HashTable<string>();
var values = Enumerable.Range(1, 100)
    .Select(_ => new Random().Next(1, 1000));

foreach (var value in values)
{
    hashTable.Add(value.ToString());    
}

hashTable.Display();

Console.WriteLine("Enter key to find value");
var key = Convert.ToInt32(Console.ReadLine());

var findValues = hashTable.Find(key.ToString());

Console.WriteLine($"Findind value {findValues}");