using Graph;

var g = new MyGraph();

g.AddVertex("1");
g.AddVertex("2");
g.AddVertex("3");
g.AddVertex("4");
g.AddVertex("5");
g.AddVertex("6");

g.AddEdge("5", "6", 9);
g.AddEdge("5", "4", 6);
g.AddEdge("6", "3", 3);
g.AddEdge("6", "1", 14);
g.AddEdge("4", "3", 11);
g.AddEdge("4", "2", 15);
g.AddEdge("3", "1", 9);
g.AddEdge("3", "2", 10);
g.AddEdge("1", "2", 7);

var dijkstra = new DijkstraAlgorithm(g);
var path = dijkstra.FindShortestPath("1", "5");
Console.WriteLine(path);

var bdfs = new BDFS(g);

bdfs.BFS("5");
Console.WriteLine();

bdfs.DFS("5");
Console.WriteLine();