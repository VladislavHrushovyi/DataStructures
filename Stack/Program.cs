// See https://aka.ms/new-console-template for more information

using Stack;

MyStack<int> stack1 = new();

stack1.Push(11);
stack1.Push(22);
stack1.Push(33);
stack1.Push(44);

stack1.Display();
Console.WriteLine($"Top element is {stack1.Peek()}");

stack1.Pop();
stack1.Pop();

stack1.Display();
Console.WriteLine($"Top element is {stack1.Peek()}");

Console.WriteLine("#################----QUEUE----####################");

var q = new MyQueue<int>();

q.EnQueue(1);
q.EnQueue(2);
q.EnQueue(3);

Console.WriteLine(q.DeQueue());
Console.WriteLine(q.DeQueue());
Console.WriteLine(q.DeQueue());

Console.WriteLine("###################################################");

string brackets = "()09090()()()()()9090900)))((()))((())(())(";

var stackRightBrackets = new MyStack<char>();
var stackLeftBrackets = new MyStack<char>();

foreach (var item in brackets)
{
    if(item == '(')
        stackLeftBrackets.Push(item);
    else if (item == ')')
        stackRightBrackets.Push(item);
}

if (stackRightBrackets.Count == stackLeftBrackets.Count)
    Console.WriteLine("Brackets valid");
else
    Console.WriteLine("Brackets invalid");

