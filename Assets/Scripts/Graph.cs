using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineDreams
{
    public class Graph<T>
    {
        public List<Vertex<T>> Vertices { get; }
        public Vertex<T> head;
        public int Size => Vertices.Count;
        private System.Random psrng;
        public Graph(params Vertex<T>[] initialNodes)
            : this((IEnumerable<Vertex<T>>)initialNodes) { }

        public Graph(IEnumerable<Vertex<T>> initialNodes = null) {
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
            for (int i = 0; i < degree && numNewNodes > 0; i++) {
                newVertex = MakeNewVertex(instantiator, outerVertexes);
                AddNeighbors(head, newVertex);
                numNewNodes--;
            }
            //Connect the remaining nodes
            while (numNewNodes > 0) {
                Vertex<T> workingVertex = outerVertexes.Dequeue();
                degree = psrng.Next(1, maxDegree);
                int loop_degree = psrng.Next(1, degree);
                for (int i = workingVertex.NeighborCount; i < loop_degree; i++) {
                    Vertex<T> CloseVertex = null;
                    while (CloseVertex == null || CloseVertex == workingVertex || CloseVertex.Neighbors.Find(x => x == workingVertex) != null)
                        CloseVertex = RandomWalk(CloseVertex);//fixed seemingly typo; was RandomWalk(workingVertex);
                    AddNeighbors(workingVertex, CloseVertex);
                }
                for (int i = workingVertex.NeighborCount; i < degree && numNewNodes > 0; i++) {
                    newVertex = MakeNewVertex(instantiator, outerVertexes);
                    AddNeighbors(workingVertex, newVertex);
                    numNewNodes--;
                }
                if (outerVertexes.Count == 0) {
                    outerVertexes.Enqueue(Vertices[psrng.Next(0, Vertices.Count)]);
                }
            }
        }

        public Vertex<T> MakeNewVertex(Func<int, T> instantiator, Queue<Vertex<T>> op_que = null) {
            Vertex<T> newVertex = new Vertex<T>(instantiator(psrng.Next(int.MinValue, int.MaxValue)));
            if (op_que != null)
                op_que.Enqueue(newVertex);
            Vertices.Add(newVertex);
            return newVertex;
        }

        public Vertex<T> RandomWalk(Vertex<T> Curr) {
            if (psrng.Next(0, 2) == 1)
                return RandomWalk(Curr.Neighbors[psrng.Next(0, Curr.NeighborCount)]);
            else
                return Curr;
        }
        public void AddPair(Vertex<T> first, Vertex<T> second) {
            AddToList(first);
            AddToList(second);
            AddNeighbors(first, second);
        }

        public void DepthFirstSearch(Vertex<T> root, Action<string> writer) {
            UnvisitAll();
            DepthFirstSearchImplementation(root, writer);

        }

        private void DepthFirstSearchImplementation(Vertex<T> root, Action<string> writer) {
            if (!root.IsVisited) {
                writer($"{root.Value} ");
                root.IsVisited = true;

                foreach (Vertex<T> neighbor in root.Neighbors) {
                    DepthFirstSearchImplementation(neighbor, writer);
                }
            }
        }

        private void AddToList(Vertex<T> vertex) {
            if (!Vertices.Contains(vertex)) {
                Vertices.Add(vertex);
            }
        }

        private void AddNeighbors(Vertex<T> first, Vertex<T> second) {
            AddNeighbor(first, second);
            AddNeighbor(second, first);
        }

        private void AddNeighbor(Vertex<T> first, Vertex<T> second) {
            if (!first.Neighbors.Contains(second)) {
                first.AddEdge(second);
            }
        }

        private void UnvisitAll() {
            foreach (var vertex in Vertices) {
                vertex.IsVisited = false;
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder($"Graph of size {Size}\n");
            foreach (Vertex<T> vert in Vertices) {
                sb.Append($"{vert}\n");
            }
            return sb.ToString();
        }
    }

}