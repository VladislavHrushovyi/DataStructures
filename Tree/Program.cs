using Tree;

ITree<int> myTree = new BinaryTree<int>();

for (int i = 0; i < 10; i++)
{
    myTree.Insert(new Random().Next(1,100));
}

Console.WriteLine("My tree pre order:");
myTree.TraversePreOrder();
Console.WriteLine();

Console.WriteLine("My tree in order:");
myTree.TraverseInOrder();
Console.WriteLine();

Console.WriteLine("My tree post order:");
myTree.TraversePostOrder();
Console.WriteLine();

Console.WriteLine("Enter the deleting value");
var deleteValue = Convert.ToInt32(Console.ReadLine());
if(myTree.Contain(deleteValue))
    myTree.RemoveNode(deleteValue);
else 
    Console.WriteLine($"Tree doesnt contain value {deleteValue}");

Console.WriteLine();
myTree.TraverseInOrder();