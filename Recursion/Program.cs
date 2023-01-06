
using Recursion;

var recOdj = new RecursionMethods();

recOdj.PrintSequence(5);

for (int i = 0; i < 5; i++)
{
    Console.Write(recOdj.CulcFibNumber(i) + " --> ");
}
Console.WriteLine();
Console.WriteLine($"Factorial of 5 is {recOdj.Factorial(5)}");