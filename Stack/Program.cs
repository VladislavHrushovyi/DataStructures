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