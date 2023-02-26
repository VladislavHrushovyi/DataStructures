
using HashFT;

// int coef = 97;
// int value = 555;
// string data = "Hello wowrld";
// Console.WriteLine("____________DIVIDING METHOD____________");
// Console.WriteLine($"Value {value} have hash {HashGenerator.ByDividing(value, coef)}");
// Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByDividing(data, coef)}");
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
// Console.WriteLine($"Value {value} have hash {HashGenerator.ByMultiplication(value, coef)}");
// Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByMultiplication(data, coef)}");
// Console.WriteLine("____________END_____________");

var hashTable = new HashTable<string>(1000);
var values = Enumerable.Range(1, 1000)
    .Select(_ => new Random().Next(1, 10000));

foreach (var item in values)
{
    hashTable.Add(item.ToString());    
}

hashTable.Display();

Console.WriteLine("Enter key to find value");
var key = Convert.ToInt32(Console.ReadLine());

var findValues = hashTable.Find(key.ToString());

Console.WriteLine($"Findind value {findValues}");

Console.WriteLine("Enter key to remove");
key = Convert.ToInt32(Console.ReadLine());
hashTable.Remove(key.ToString());
hashTable.Display();