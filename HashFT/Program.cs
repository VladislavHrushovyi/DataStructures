
using HashFT;

int value = 365;
string data = "Hello wowrld";
Console.WriteLine("____________DIVIDING METHOD____________");
Console.WriteLine($"Value {value} have hash {HashGenerator.ByDividing(value)}");
Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByDividing(data)}");
Console.WriteLine("____________END_____________");

Console.WriteLine("_____________MID-SQUARE METHOD____________");
Console.WriteLine($"Value {value} have hash {HashGenerator.ByMidSquare(value)}");
Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByMidSquare(data)}");
Console.WriteLine("____________END_____________");

Console.WriteLine("_____________DIGIT FOLDING METHOD____________");
Console.WriteLine($"Value {value} have hash {HashGenerator.ByDigitFolding(value)}");
Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByDigitFolding(data)}");
Console.WriteLine("____________END_____________");

Console.WriteLine("_____________DIGIT FOLDING METHOD____________");
Console.WriteLine($"Value {value} have hash {HashGenerator.ByMultiplication(value)}");
Console.WriteLine($"Value \"{data}\" have hash {HashGenerator.ByMultiplication(data)}");
Console.WriteLine("____________END_____________");
