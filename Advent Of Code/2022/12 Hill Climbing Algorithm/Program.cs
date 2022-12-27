// See https://aka.ms/new-console-template for more information
using Core;

var lines = File.ReadAllLines("input.txt");
var width = lines.First().Length;
var height = lines.Length;

var heightMap = new SquareGraphNode<char>[width, height];
var allNodes = new HashSet<SquareGraphNode<char>>();

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        var treeHeight = lines[y][x];
        var node = new SquareGraphNode<char>(heightMap, x, y, treeHeight);
        heightMap[x, y] = node;
        allNodes.Add(node);
    }
}

var startNode = heightMap[146, 20];
var endNode = heightMap[128, 20];

// Thanks mr Dijkstra
var visitedNodes = new HashSet<SquareGraphNode<char>>();

var probedNodes = new HashSet<SquareGraphNode<char>>();

var currentNode = startNode!;

currentNode.SetDistance(0);

while (currentNode != null)
{
    visitedNodes.Add(currentNode);
    checked
    {
        var distanceToNextNode = currentNode.GetDistance() + 1;

        foreach(var neighbour in currentNode.AccessibleNodes())
        {
            probedNodes.Add(neighbour);
            neighbour.SetDistance(Math.Min(distanceToNextNode, neighbour.GetDistance()));
        }

        currentNode = probedNodes.Except(visitedNodes).MinBy(node => node.GetDistance());
    }
}

Console.WriteLine(endNode.GetDistance());
var closestA =
    allNodes
    .Where(node => node.Value == 'a')
    .MinBy(node => node.GetDistance());

Console.WriteLine($"Closest A is {closestA} at {closestA.GetDistance()} away");


public static class SquareGraphNodeExtensions
{  
    public static IEnumerable<SquareGraphNode<char>> AccessibleNodes(this SquareGraphNode<char> squareGraphNode)
    {
        return squareGraphNode.Neighbours.Where(neighbour => neighbour.Value.InclusivelyBetween('z', (char)(squareGraphNode.Value - 1)));
    }

    private static readonly Dictionary<SquareGraphNode<char>, int> _distances = new();

    public static void SetDistance(this SquareGraphNode<char> squareGraphNode, int value)
    {
        _distances[squareGraphNode] = value;
    }

    public static int GetDistance(this SquareGraphNode<char> squareGraphNode)
    {
        return _distances.TryGetValue(squareGraphNode, out int value) ? value : int.MaxValue;
    }
}