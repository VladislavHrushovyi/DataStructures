namespace Recursion;

public class RecursionMethods
{
    public void PrintSequence(int n)
    {
        if (n == 0)
        {
            return;
        }
        PrintSequence(n - 1);

        Console.WriteLine(n);
    }

    public int CulcFibNumber(int x)
    {
        if (x == 0)
            return 0;
        if (x == 1 || x == 2)
        {
            return 1;
        }
        
        return CulcFibNumber(x - 1) + CulcFibNumber(x - 2);
    }

    public int Factorial(int n)
    {
        if (n == 0 || n == 1)
        {
            return 1;
        }

        return n * Factorial(n - 1);
    }
}