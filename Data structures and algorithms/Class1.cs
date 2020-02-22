using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class Test
    {
        public static void Main(string[] args)
        {
            Graph FirstGraph = new Graph();
            FirstGraph.AddVertex(0);
            FirstGraph.AddVertex(1);
            FirstGraph.AddVertex(2);
            FirstGraph.AddVertex(3);
            FirstGraph.AddVertex(4);

            FirstGraph.AddEdge(0, 1);
            FirstGraph.AddEdge(0, 4);
            FirstGraph.AddEdge(1, 2);
            FirstGraph.AddEdge(1, 3);
            FirstGraph.AddEdge(1, 4);
            FirstGraph.AddEdge(2, 3);
            FirstGraph.AddEdge(3, 4);

            FirstGraph.PrintGraph();
        }
    }

    class Graph
    {
        public List<SortedList<int,int>> Vertices;

        public Graph() {
            Vertices = new List<SortedList<int, int>>(); 
        }

        public void AddEdge(int V1, int V2)
        {
            Vertices[V1].Add(V2,1);
            Vertices[V2].Add(V1,1);
        }

        public void AddEdge(int V1, int V2, int Weigth)
        {
            Vertices[V1].Add(V2, Weigth);
            Vertices[V2].Add(V1, Weigth);
        }

        public void RemoveEdge(int V1, int V2)
        {
            Vertices[V1].Remove(V2);
            Vertices[V2].Remove(V1);
        }

        public SortedList<int, int> GetVertex(int Value)
        {
            foreach (SortedList<int, int> Edges in Vertices)
            {
                if (Edges.Keys[Edges.IndexOfValue(0)] == Value)
                    return Edges;
            }
            return null;
        }

        public void AddVertex(int Value)
        {
            Vertices.Add(new SortedList<int, int>());
            Vertices[Vertices.Count - 1].Add(Value, 0);
        }

        public void RemoveVertex(int Value)
        {
            Vertices.Remove(Vertices[Value]);
            Vertices.ForEach(Edges => Edges.Remove(Value));
        }

        public void PrintGraph()
        {
            foreach (SortedList<int,int> Edges in Vertices)
            {
                int Value = Edges.Keys[Edges.IndexOfValue(0)];
                Console.WriteLine("Vertex with value {0} is connected to: ", Value);
                foreach (KeyValuePair<int,int> Pair in Edges)
                {
                    if (Pair.Key == Value) continue;
                    Console.Write("{0} ({1}); ", Pair.Key, Pair.Value);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
