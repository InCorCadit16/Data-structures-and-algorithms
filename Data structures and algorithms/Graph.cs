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
            FirstGraph.AddVerticies(9);

            FirstGraph.AddEdge(0, 1, 8);
            FirstGraph.AddEdge(0, 8, 4);
            FirstGraph.AddEdge(1, 2, 7);
            FirstGraph.AddEdge(1, 4, 4);
            FirstGraph.AddEdge(1, 6, 2);
            FirstGraph.AddEdge(2, 4, 14);
            FirstGraph.AddEdge(2, 3, 9);
            FirstGraph.AddEdge(3, 4, 10);
            FirstGraph.AddEdge(4, 5, 2);
            FirstGraph.AddEdge(5, 6, 6);
            FirstGraph.AddEdge(5, 7, 1);
            FirstGraph.AddEdge(6, 7, 7);
            FirstGraph.AddEdge(7, 0, 11);
            FirstGraph.AddEdge(7, 8, 8);

            Prim(FirstGraph).PrintGraph();
            Kruskal(FirstGraph).PrintGraph();
            
        }

        static Graph Kruskal(Graph I)
        {
            Graph A = new Graph();
            A.AddVerticies(I.Vertices.Count);
            var Sets = new List<HashSet<int>>();

            var Edges = new List<Edge>();
            for (int i = 0; i < I.Vertices.Count; i++)
            {
                var Set = new HashSet<int>();
                Sets.Add(Set);
                Set.Add(i);

                foreach (var edge in I.GetVertex(i))
                {
                    if (!Edges.Any(Edge => Edge.left == edge.Key && Edge.right == i))
                        Edges.Add(new Edge { left = i, right = edge.Key, weight = edge.Value });
                }
            }

            SortEdges(Edges);
            foreach (var Edge in Edges)
            {
                var SetL = SetContainsValue(Sets, Edge.left);
                var SetR = SetContainsValue(Sets, Edge.right);
                if (SetL != SetR)
                {
                    A.AddEdge(Edge.left, Edge.right, Edge.weight);
                    Union(Sets, SetL, SetR);
                }
            }

            return A;
        }

        // For Kruskal
        struct Edge
        {
            public int left, right;
            public int weight;
        }

        // For Kruskal
        static void SortEdges(List<Edge> Edges)
        {
            for (int i = 0; i < Edges.Count; i++)
            {
                Edge min = Edges[i];
                int min_i = i;
                for (int n = i + 1; n < Edges.Count; n++)
                {
                    if (Edges[n].weight < min.weight)
                    {
                        min = Edges[n]; min_i = n;
                    }
                }

                Edges[min_i] = Edges[i];
                Edges[i] = min;
            }
        }

        // For Kruskal
        static HashSet<int> SetContainsValue(List<HashSet<int>> Sets, int value)
        {
            foreach(var Set in Sets)
            {
                if (Set.Contains(value)) return Set;
            }
            return null;
        }

        // For Kruskal
        static void Union(List<HashSet<int>> Sets, HashSet<int> SetL, HashSet<int> SetR)
        {
            foreach (var Vertex in SetR)
            {
                SetL.Add(Vertex);
            }
            Sets.Remove(SetR);
        }

      

        static Graph Prim(Graph I)
        {
            var parent = new int[I.Vertices.Count];
            var keys = new int[I.Vertices.Count];
            var Mst = new bool[I.Vertices.Count];

            for (int i = 0; i < parent.Length; i++)
            {
                keys[i] = int.MaxValue;
                Mst[i] = false;
            }

            keys[0] = 0;
            parent[0] = -1;

            for (int i = 0; i < parent.Length - 1; i++)
            {
                int U = GetMinTrue(keys, Mst);
                Mst[U] = true;

                foreach (var pair in I.GetVertex(U))
                {
                    if (!Mst[pair.Key] && pair.Value < keys[pair.Key])
                    {
                        parent[pair.Key] = U;
                        keys[pair.Key] = pair.Value;
                    }
                }
            }

            Graph A = new Graph();
            A.AddVerticies(I.Vertices.Count);
            for (int i = 1; i < parent.Length; i++)
            {
                A.AddEdge(i, parent[i], keys[i]);
            }
            return A;
        }

        static int GetMinTrue(int[] keys, bool[] Mst)
        {
            int min_key = keys[0], min = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (!Mst[i])
                {
                    min_key = keys[i];
                    min = i;
                    break;
                }
            }
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] < min_key && !Mst[i])
                {
                    min_key = keys[i];
                    min = i;
                }
            }
            return min;
        }
    }

   

    class Graph : ICloneable
    {
        public List<SortedList<int,int>> Vertices;

        public Graph() {
            Vertices = new List<SortedList<int, int>>(); 
        }

        public void AddEdge(int V1, int V2)
        {
            AddEdge(V1, V2, 1);
        }

        public virtual void AddEdge(int V1, int V2, int Weigth)
        {
            if (Vertices[V1].ContainsKey(V2))
                return;

            Vertices[V1].Add(V2, Weigth);
            Vertices[V2].Add(V1, Weigth);
        }

        public virtual void RemoveEdge(int V1, int V2)
        {
            Vertices[V1].Remove(V2);
            Vertices[V2].Remove(V1);
        }

        public int getEdgeWeight(int V1, int V2)
        {
            return Vertices[V1].GetValueOrDefault(V2);
        }

        public SortedList<int, int> GetVertex(int Value)
        {
            return Vertices[Value];
        }

        public void AddVertex()
        {
            Vertices.Add(new SortedList<int, int>());
        }

        public void AddVerticies(int Number)
        {
            for (int i = 0; i < Number; i++)
                AddVertex();
        }

        public void RemoveVertex(int Value)
        {
            Vertices.Remove(GetVertex(Value));
            Vertices.ForEach(Edges => Edges.Remove(Value)); 
        }

        public void PrintGraph()
        {
            foreach (SortedList<int,int> Edges in Vertices)
            {
                Console.WriteLine("Vertex {0} is connected to: ", Vertices.IndexOf(Edges));
                foreach (KeyValuePair<int,int> Pair in Edges)
                {
                    Console.Write("{0} ({1}); ", Pair.Key, Pair.Value);
                }
                Console.WriteLine("\n");
            }
        }

        public object Clone()
        {
            Graph G = new Graph();
            G.AddVerticies(this.Vertices.Count);
            Vertices.ForEach(Edges =>
            {
                foreach (var Edge in Edges)
                {
                    G.AddEdge(Edges.IndexOfKey(Edge.Key), Edge.Key, Edge.Value);
                }
            });
            return G;
        }
    }

    class DirectedGraph : Graph
    {

        public override void AddEdge(int V1, int V2, int Weigth)
        {
            Vertices[V1].Add(V2, Weigth);
        }

        public override void RemoveEdge(int V1, int V2)
        {
            Vertices[V1].Remove(V2);
        }
    }
}
