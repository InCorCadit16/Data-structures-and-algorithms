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
            /*FirstGraph.AddVerticies(9);

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
            FirstGraph.AddEdge(7, 8, 8);*/

            FirstGraph.AddVerticies(6);

            FirstGraph.AddEdge(0, 1);
            FirstGraph.AddEdge(0, 2);
            FirstGraph.AddEdge(0, 3);
            FirstGraph.AddEdge(0, 4);
            FirstGraph.AddEdge(2, 3);
            FirstGraph.AddEdge(1, 5);
            FirstGraph.AddEdge(4, 5);

            var path = EulerLoop(FirstGraph);
            if (path == null)
                Console.WriteLine("Can\'t build Euler Loop");
            else
            {
                for (int i = 0; i < path.Length; i++)
                {
                    Console.Write(path[i] + " ");
                }
            }
                
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

        // For Prims
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
        
        static Graph Dijkstra(Graph I)
        {
            var A = new Graph();
            A.AddVerticies(I.Vertices.Count);
            int[] values = new int[I.Vertices.Count];
            int[] minFrom = new int[I.Vertices.Count];
            bool[] inA = new bool[I.Vertices.Count];
            var Edges = new List<Edge>();

            for (int i = 1; i < values.Length; i++)
            {
                values[i] = int.MaxValue;
                inA[i] = false;
            }

            for (int i = 0; i < values.Length - 1; i++)
            {
                int min = GetMinTrue(values, inA);
                inA[min] = true;

                Edge minEdge = new Edge { left = min, weight = int.MaxValue };
                foreach (var pair in I.GetVertex(min))
                {
                    if (values[min] + pair.Value < values[pair.Key] && !inA[pair.Key])
                    {
                        values[pair.Key] = values[min] + pair.Value;
                        minFrom[pair.Key] = min;
                    }                            
                }

                for (int c = 0; c < values.Length; c++)
                {
                    if (values[c] < minEdge.weight && !inA[c])
                    {
                        minEdge.weight = values[c];
                        minEdge.right = c;
                        minEdge.left = minFrom[c];
                    }
                }

                Edges.Add(minEdge);
            }

            foreach (var edge in Edges)
            {
                if (A.GetEdgeWeight(edge.left, edge.right) == 0)
                    A.AddEdge(edge.left, edge.right, edge.weight);
            }

            return A;
        } 


        static int[,] Floyd(Graph I)
        {
            int size = I.Vertices.Count;
            var matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                        matrix[i, j] = I.GetEdgeWeight(i, j) == 0 ? int.MaxValue/2 : I.GetEdgeWeight(i, j);
                    else
                        matrix[i, j] = 0;
                }
            }

            for (int k = 0; k < size; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (matrix[i, k] + matrix[k, j] < matrix[i, j])
                            matrix[i, j] = matrix[i, k] + matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        static void PrintFloyd(int [,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = i+1; j < matrix.GetLength(1); j++)
                {
                    Console.WriteLine($"{i} and {j}: {matrix[i,j]}");
                }
            }
        }

        static Graph HamiltonianLoop(Graph I)
        {
            var path = new int[I.Vertices.Count];
            for (int i = 0; i < path.Length; i++)
                path[i] = -1;

            path[0] = 0;
            if (!HamCycleExists(I, path, 1))
                return null;



            Graph A = new Graph();
            A.AddVerticies(path.Length);
            for (int i = 0; i < path.Length - 1; i++)
            {
                A.AddEdge(path[i], path[i + 1], I.GetEdgeWeight(path[i], path[i + 1]));
            }
            A.AddEdge(path.First(), path.Last(), I.GetEdgeWeight(path.First(), path.Last()));

            return A;
        }

        static bool HamCycleExists(Graph I, int[] path, int pos)
        {
            if (pos == I.Vertices.Count)
            {
                return I.GetEdgeWeight(path[0], path[pos - 1]) != 0;
            }


            for (int v = 1; v < I.Vertices.Count; v++)
            {
                if (isSave(v,I,path,pos))
                {
                    path[pos] = v;

                    if (HamCycleExists(I, path, pos + 1))
                        return true;

                    path[pos] = -1;
                }
            }
                
                
            return true;
        }

        static bool isSave(int v, Graph I, int[] path, int pos)
        {
            if (I.GetEdgeWeight(path[pos - 1], v) == 0)
                return false;

            for (int i = 0; i < pos; i++)
                if (path[i] == v) return false;

            return true;
        }

        static int[] EulerLoop(Graph I)
        {
            for (int i = 0; i < I.Vertices.Count; i++)
            {
                var Vertex = I.GetVertex(i);
                if (Vertex.Count == 0 || Vertex.Count % 2 != 0)
                    return null;
            }

            var path = new int[I.GetEdgeCount()+1];

            var A = (Graph) I.Clone();

            int curVertex = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (i == path.Length - 2)
                {
                    path[i] = curVertex;
                    path[path.Length - 1] = 0;
                    break;
                }

                var edgeList = A.GetVertex(curVertex);
                foreach (var edge in edgeList)
                {
                    if (A.GetVertex(edge.Key).Count > 1)
                    {
                        path[i] = curVertex;
                        A.RemoveEdge(curVertex, edge.Key);
                        curVertex = edge.Key;
                        break;
                    }
                }
            }
           
            return path;
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

        public int GetEdgeWeight(int V1, int V2)
        {
            return Vertices[V1].GetValueOrDefault(V2);
        }

        public int GetEdgeCount()
        {
            var edges = new List<(int l, int r)>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                var Vertex = GetVertex(i);

                foreach (var edge in Vertex)
                {
                    if (!edges.Any(e => e.l == edge.Key && e.r == i))
                        edges.Add((i, edge.Key));
                }
            }
            return edges.Count;
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
                    G.AddEdge(Vertices.IndexOf(Edges), Edge.Key, Edge.Value);
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
