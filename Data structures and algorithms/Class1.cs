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
        private List<SortedList<int,int>> Edges;

        public Graph() {
            Edges = new List<SortedList<int, int>>(); 
        }

        public void AddEdge(int V1, int V2)
        {
            Edges[V1].Add(V2,1);
            Edges[V2].Add(V1,1);
        }

        public void AddEdge(int V1, int V2, int Weigth)
        {
            Edges[V1].Add(V2, Weigth);
            Edges[V2].Add(V1, Weigth);
        }

        public void RemoveEdge(int V1, int V2)
        {
            Edges[V1].Remove(V2);
            Edges[V2].Remove(V1);
        }

        public void AddVertex(int Value)
        {
            Edges.Add(new SortedList<int, int>());
            Edges[Edges.Count - 1].Add(Value, 0);
        }

        public void PrintGraph()
        {
            foreach (SortedList<int,int> Verticies in Edges)
            {
                int Value = Verticies.Keys[Verticies.IndexOfValue(0)];
                Console.WriteLine("Vertex with value {0} is connected to: ", Value);
                foreach (KeyValuePair<int,int> Pair in Verticies)
                {
                    if (Pair.Key == Value) continue;
                    Console.Write("{0} ({1}); ", Pair.Key, Pair.Value);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
