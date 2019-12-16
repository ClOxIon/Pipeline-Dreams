using UnityEngine;
using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Util
{

    public static readonly Quaternion TurnUp = Quaternion.Euler(-90, 0, 0);
    public static readonly Quaternion TurnDown = Quaternion.Euler(90, 0, 0);
    public static readonly Quaternion TurnRight = Quaternion.Euler(0, 90, 0);
    public static readonly Quaternion TurnLeft = Quaternion.Euler(0, -90, 0);
    public static Vector3Int FaceToLHVector(int f)
    {
        return new Vector3Int(f >> 1 == 0 ? 1 : 0, f >> 1 == 1 ? 1 : 0, f >> 1 == 2 ? 1 : 0) * (-((f & 1) << 1) + 1);
    }

    public static int LHUnitVectorToFace(Vector3Int v)
    {

        return (1 - v.x) / 2 + Mathf.Abs(v.y) * (5 - v.y) / 2 + Mathf.Abs(v.z) * (9 - v.z) / 2;
    }
    /// <summary>
    /// North = (0,0,1) Up = (0,1,0)
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static int LHQToFace(Quaternion q)
    {
        var v = Vector3Int.RoundToInt(q * Vector3.forward);
        return (1 - v.x) / 2 + Mathf.Abs(v.y) * (5 - v.y) / 2 + Mathf.Abs(v.z) * (9 - v.z) / 2;
    }

    public static Quaternion FaceToLHQ(int f)
    {
        return Quaternion.LookRotation(new Vector3Int(f >> 1 == 0 ? 1 : 0, f >> 1 == 1 ? 1 : 0, f >> 1 == 2 ? 1 : 0) * (-((f & 1) << 1) + 1), Vector3.up);
    }
    public static Vector3Int LHQToLHUnitVector(Quaternion q)
    {
        return Vector3Int.RoundToInt(q * Vector3.forward);
    }
    public static bool IsBasis(Vector3Int v)
    {
        return v.x * v.y == 0 & v.y * v.z == 0 & v.z * v.x == 0;
    }
    public static Vector3Int Normalize(Vector3Int v)
    {
        v.Clamp(Vector3Int.one * -1, Vector3Int.one);
        return v;
    }
    public static Quaternion RotateToFace(int f, Quaternion q0)
    {
        if (LHQToFace(q0) == f) return Quaternion.identity;
        if (LHQToFace(q0 * TurnUp) == f) return TurnUp;
        if (LHQToFace(q0 * TurnDown) == f) return TurnDown;
        if (LHQToFace(q0 * TurnLeft) == f) return TurnLeft;
        if (LHQToFace(q0 * TurnRight) == f) return TurnRight;
        return TurnUp;
    }
    public static bool CompareTiles(Tile t1, Tile t2)
    {
        return (uint)(t1 ^ t2) >> 8 == 0;
    }
    public static bool CompareBlocks(Block t1, Block t2)
    {
        return (uint)(t1 ^ t2) >> 8 == 0;
    }

    public class Vertex<T>
    {
        public Vertex(T value, params Vertex<T>[] parameters)
            : this(value, (IEnumerable<Vertex<T>>)parameters) { }

        public Vertex(T value, IEnumerable<Vertex<T>> neighbors = null)
        {
            Value = value;
            Neighbors = neighbors?.ToList() ?? new List<Vertex<T>>();
            IsVisited = false;
        }
        public T Value { get; }   // can be made writable
        public List<Vertex<T>> Neighbors { get; }
        public bool IsVisited { get; set; }
        public int NeighborCount => Neighbors.Count;
        public void AddEdge(Vertex<T> vertex)
        {
            Neighbors.Add(vertex);
        }
        public void AddEdges(params Vertex<T>[] newNeighbors)
        {
            Neighbors.AddRange(newNeighbors);
        }
        public void AddEdges(IEnumerable<Vertex<T>> newNeighbors)
        {
            Neighbors.AddRange(newNeighbors);
        }
        public void RemoveEdge(Vertex<T> vertex)
        {
            Neighbors.Remove(vertex);
        }
        public override string ToString()
        {
            return Neighbors.Aggregate(new StringBuilder($"{Value}: "), (sb, n) => sb.Append($"{n.Value} ")).ToString();
            //return $"{Value}: {(string.Join(" ", Neighbors.Select(n => n.Value)))}";
        }
    }

    public class Graph<T>
    {
        public List<Vertex<T>> Vertices { get; }
        public Vertex<T> head;
        public int Size => Vertices.Count;
        private System.Random psrng;
        public Graph(params Vertex<T>[] initialNodes)
            : this((IEnumerable<Vertex<T>>)initialNodes) { }

        public Graph(IEnumerable<Vertex<T>> initialNodes = null)
        {
            Vertices = initialNodes?.ToList() ?? new List<Vertex<T>>();
        }

        public Graph(int seed, int numNewNodes, int maxDegree, Func<int, T> instantiator) //Random growth constructor
        {
            //Setting up genorator and vertexes
            //UnityEngine.Debug.Log($"Graph contructor called. Seed {seed} numNewNode {numNewNodes}");
            psrng = new System.Random(seed);
            Vertices = new List<Vertex<T>>();
            //Outer queue used to grow graph outwards
            Queue<Vertex<T>> outerVertexes = new Queue<Vertex<T>>();

            //Adding head aka the entrance vertex
            Vertex<T> newVertex = MakeNewVertex(instantiator, outerVertexes);
            head = newVertex;
            numNewNodes--;
            //Connecting the head vertexes.
            //This has to be done sperately as the head can only have degree 1-5 unlike the
            //other vertexes which could all be degree 6
            int degree = psrng.Next(1, maxDegree - 1);
            for (int i = 0; i < degree && numNewNodes > 0; i++)
            {
                newVertex = MakeNewVertex(instantiator, outerVertexes);
                AddNeighbors(head, newVertex);
                numNewNodes--;
            }
            //Connect the remaining nodes
            while (numNewNodes > 0)
            {
                Vertex<T> workingVertex = outerVertexes.Dequeue();
                degree = psrng.Next(1, maxDegree);
                int loop_degree = psrng.Next(1, degree);
                for (int i = workingVertex.NeighborCount; i < loop_degree; i++)
                {
                    Vertex<T> CloseVertex = null;
                    while (CloseVertex == null || CloseVertex == workingVertex || CloseVertex.Neighbors.Find(x => x == workingVertex) != null)
                        CloseVertex = RandomWalk(workingVertex);
                    AddNeighbors(workingVertex, CloseVertex);
                }
                for (int i = workingVertex.NeighborCount; i < degree && numNewNodes > 0; i++)
                {
                    newVertex = MakeNewVertex(instantiator, outerVertexes);
                    AddNeighbors(workingVertex, newVertex);
                    numNewNodes--;
                }
                if (outerVertexes.Count == 0)
                {
                    outerVertexes.Enqueue(Vertices[psrng.Next(0, Vertices.Count)]);
                }
            }
        }

        public Vertex<T> MakeNewVertex(Func<int, T> instantiator, Queue<Vertex<T>> op_que = null)
        {
            Vertex<T> newVertex = new Vertex<T>(instantiator(psrng.Next(int.MinValue, int.MaxValue)));
            if (op_que != null)
                op_que.Enqueue(newVertex);
            Vertices.Add(newVertex);
            return newVertex;
        }

        public Vertex<T> RandomWalk(Vertex<T> Curr)
        {
            if (psrng.Next(0, 2) == 1)
                return RandomWalk(Curr.Neighbors[psrng.Next(0, Curr.NeighborCount)]);
            else
                return Curr;
        }
        public void AddPair(Vertex<T> first, Vertex<T> second)
        {
            AddToList(first);
            AddToList(second);
            AddNeighbors(first, second);
        }

        public void DepthFirstSearch(Vertex<T> root, Action<string> writer)
        {
            UnvisitAll();
            DepthFirstSearchImplementation(root, writer);

        }

        private void DepthFirstSearchImplementation(Vertex<T> root, Action<string> writer)
        {
            if (!root.IsVisited)
            {
                writer($"{root.Value} ");
                root.IsVisited = true;

                foreach (Vertex<T> neighbor in root.Neighbors)
                {
                    DepthFirstSearchImplementation(neighbor, writer);
                }
            }
        }

        private void AddToList(Vertex<T> vertex)
        {
            if (!Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);
            }
        }

        private void AddNeighbors(Vertex<T> first, Vertex<T> second)
        {
            AddNeighbor(first, second);
            AddNeighbor(second, first);
        }

        private void AddNeighbor(Vertex<T> first, Vertex<T> second)
        {
            if (!first.Neighbors.Contains(second))
            {
                first.AddEdge(second);
            }
        }

        private void UnvisitAll()
        {
            foreach (var vertex in Vertices)
            {
                vertex.IsVisited = false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"Graph of size {Size}\n");
            foreach (Vertex<T> vert in Vertices)
            {
                sb.Append($"{vert}\n");
            }
            return sb.ToString();
        }
    }
}

