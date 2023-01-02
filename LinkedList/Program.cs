﻿using LinkedList;

var dll = new DoubleLinkedList<int>();

dll.Push(1);
dll.Push(2);
dll.Push(3);
dll.Push(4);
dll.Append(77);
dll.InsertBefore(dll[0], 11);
dll.InsertAfter(dll[0], 22);
dll.RemoveNode(dll[^2]);

for (int i = 0; i < dll.Length; i++)
{
    Console.WriteLine(dll[i].Value);
}

Console.WriteLine("####################################");

foreach (var node in dll)
{
    Console.WriteLine(node.Value);
}

Console.WriteLine($"Length --> {dll.Length}");