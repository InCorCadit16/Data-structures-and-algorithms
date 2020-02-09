using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    class Test
    {
        public static void Main(string[] args)
        {
            Graph FirstGraph = new Graph(5);
            FirstGraph.AddEdge(0, 1);
            FirstGraph.AddEdge(0, 4);
            FirstGraph.AddEdge(0, 4);
            FirstGraph.AddEdge(0, 4);
            FirstGraph.AddEdge(1, 2);
            FirstGraph.AddEdge(1, 3);
            FirstGraph.AddEdge(1, 4);
            FirstGraph.AddEdge(2, 3);
            FirstGraph.AddEdge(3, 4);
        }
    }

    class Graph
    {
        private List<SortedList<int,int>> Edges;

        public Graph(int Capacity) {
            Edges = new List<SortedList<int, int>>(Capacity); 
            for (int i = 0; i < Capacity; i++)
            {
                Edges[i] = new SortedList<int, int>();
                Edges[i].Add(0, 0);
            }
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
           
        }
    }
}
