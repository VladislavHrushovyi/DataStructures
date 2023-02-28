using Graph;

var graph = new MyGraph();

graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddVertex("D");
graph.AddVertex("E");

graph.AddEdge("A", "B", 5);
graph.AddEdge("A", "D", 7);
graph.AddEdge("C", "A", 8);
graph.AddEdge("E", "D", 10);
graph.AddEdge("C", "E", 18);
graph.AddEdge("B", "E", 15);
graph.AddEdge("D", "B", 3);
graph.AddEdge("C", "A", 5);

graph.Display();