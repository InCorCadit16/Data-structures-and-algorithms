using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class Test
    {
        public static void Main(string[] args)
        {
            Graph FirstGraph = new Graph();
            FirstGraph.AddVerticies(5);

            FirstGraph.AddEdge(0,1,2);
            FirstGraph.AddEdge(0,2,5);
            FirstGraph.AddEdge(1,3,4);
            FirstGraph.AddEdge(0,3,8);
            FirstGraph.AddEdge(1,2,7);
            FirstGraph.AddEdge(0,4,16);
            FirstGraph.AddEdge(3,4,24);
            

            FirstGraph.PrintGraph();

            FirstGraph.AddVertex();

            FirstGraph.AddEdge(1, 5, 15);
            FirstGraph.AddEdge(3, 5, 8);

            FirstGraph.PrintGraph();

            FirstGraph.RemoveEdge(0, 2);

            FirstGraph.RemoveVertex(4);

            FirstGraph.PrintGraph();
        }
    }

    class Graph
    {
        public List<SortedList<int,int>> Verticies;

        public Graph() {
            Verticies = new List<SortedList<int, int>>(); 
        }

        public void AddEdge(int V1, int V2)
        {
            AddEdge(V1, V2, 1);
        }

        public void AddEdge(int V1, int V2, int Weigth)
        {
            Verticies[V1].Add(V2, Weigth);
            Verticies[V2].Add(V1, Weigth);
        }

        public SortedList<int, int> GetVertex(int Value)
        {
            return Verticies[Value];
        }

        public void RemoveEdge(int V1, int V2)
        {
            Verticies[V1].Remove(V2);
            Verticies[V2].Remove(V1);
        }

        public void AddVertex()
        {
            Verticies.Add(new SortedList<int, int>());
        }

        public void AddVerticies(int Number)
        {
            for (int i = 0; i < Number; i++)
                AddVertex();
        }

        public void RemoveVertex(int Value)
        {
            Verticies.Remove(GetVertex(Value));
            Verticies.ForEach(Edges => Edges.Remove(Value)); 
        }

        public void PrintGraph()
        {
            foreach (SortedList<int,int> Edges in Verticies)
            {
                Console.WriteLine("Vertex {0} is connected to: ", Verticies.IndexOf(Edges) + 1);
                foreach (KeyValuePair<int,int> Pair in Edges)
                {
                    Console.Write("{0} ({1}); ", Pair.Key + 1, Pair.Value);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
