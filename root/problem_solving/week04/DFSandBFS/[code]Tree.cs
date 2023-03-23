using System;
using System.Collections.Generic;

class Node
{
    public int value;
    public Node left;
    public Node right;

    public Node(int value)
    {
        this.value = value;
        this.left = null;
        this.right = null;
    }
}

class Graph
{
    private int numNodes;
    private List<Node>[] adjacencyList;

    public Graph(int numNodes)
    {
        this.numNodes = numNodes;
        this.adjacencyList = new List<Node>[numNodes + 1];
        for (int i = 1; i <= numNodes; i++)
        {
            this.adjacencyList[i] = new List<Node>();
        }
    }

    public void AddEdge(int u, int v)
    {
        Node nodeU = new Node(u);
        Node nodeV = new Node(v);
        adjacencyList[u].Add(nodeV);
        adjacencyList[v].Add(nodeU);
    }

    public List<Node> GetNodes()
    {
        List<Node> nodes = new List<Node>();
        for (int i = 1; i <= numNodes; i++)
        {
            nodes.Add(new Node(i));
        }
        return nodes;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int numNodes = 0, numEdges = 0, startNode = 0;
        bool validInput = false;

        while (!validInput)
        {
            Console.Write("노드의 수, 간선의 수, 시작 노드 번호를 입력하세요: ");
            string[] input = Console.ReadLine().Split();

            if (input.Length == 3 &&
                int.TryParse(input[0], out numNodes) &&
                int.TryParse(input[1], out numEdges) &&
                int.TryParse(input[2], out startNode))
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
        }

        Graph graph = new Graph(numNodes);
        Console.WriteLine($"{numEdges}개의 간선을 입력하세요:");
        for (int i = 0; i < numEdges; i++)
        {
            string[] edge = Console.ReadLine().Split();
            int u = int.Parse(edge[0]);
            int v = int.Parse(edge[1]);
            graph.AddEdge(u, v);
        }

        List<Node> nodes = graph.GetNodes();
        Console.WriteLine("생성된 그래프의 노드:");
        foreach (Node node in nodes)
        {
            Console.Write(node.value + " ");
        }
        Console.WriteLine();
    }
}