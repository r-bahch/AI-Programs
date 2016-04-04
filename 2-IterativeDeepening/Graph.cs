using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_IterativeDeepening
{
    class Graph<T>
    {
        public List<Node<T>> Nodes { get; private set; }  
        public List<Edge<T>> Edges { get; private set; } 
        public Graph()
        {
            Nodes = new List<Node<T>>();
            Edges = new List<Edge<T>>();
        }

        public void AddNode(T data)
        {
            Nodes.Add(new Node<T>(data));
        }

        public void AddEdge(T from, T to)
        {
            Node<T> fromNode = Nodes.Find(x => x.Data.Equals(from));    //find the node with data == from
            Node<T> toNode = Nodes.Find(x => x.Data.Equals(to));        //find the node with data == to
            Edges.Add(new Edge<T>(fromNode, toNode)); 
        }

        private Node<T> getUnvisitedChild(Node<T> node)     //returns reference to the first unvisited child O(n)
        {
            foreach (Edge<T> edge in Edges)                 //iterate through all the edges
            {
                if (edge.From.Equals(node) && !edge.To.Visited)     //if the egde is from node to unvisited
                {
                    return edge.To;
                }
            }
            return null;    //if no unvisited child exist, return null
        }


        //Depth Limited Search (recursive) - returns a list of nodes, representing the path from start to end
        public List<Node<T>> DLS(Node<T> start, Node<T> end, int depth)
        {
            if (start.Equals(end) && Nodes.Exists(x => x.Data.Equals(start.Data)))  //if start==end and start is a valid node
            {
                return start.Path;
            }
            if (depth == 0)
            {
                Console.WriteLine(start.Data);
                return new List<Node<T>>();
            }
            Node<T> child = getUnvisitedChild(start);
            while (child != null)
            {
                child.Visited = true;
                child.Path = new List<Node<T>>(start.Path);
                child.Path.Add(child);
                List<Node<T>> tmp = DLS(child, end, depth - 1);
                if (tmp!= null && tmp.Count > 0)
                    return tmp;
                child = getUnvisitedChild(start);
            }
            return new List<Node<T>>();
        }

        public List<T> IterativeDeepening(T start, T end)
        {
            for (int i = 0; ; i++)
            {
                Node<T> startNode = new Node<T>(start);
                startNode.Visited = true;
                startNode.Path = new List<Node<T>> { startNode };
                Node<T> endNode = new Node<T>(end);
                List<Node<T>> path;
                path = DLS(startNode, endNode, i);
                if (path != null && path.Count > 0)
                {
                    return path.ConvertAll(x => x.Data);
                }
                if (Nodes.FindAll(x => !x.Visited).Count ==1)
                    break;
                ClearNodes();
            }
            return new List<T>();
        }

        private void ClearNodes()
        {
            foreach (var node in Nodes)
            {
                node.Path = null;
                node.Visited = false;
            }
        }
    }


    class Node<T>
    {
        public T Data;
        public bool Visited;
        public List<Node<T>> Path;
        public bool Equals(Node<T> node)
        {
            return Data.Equals(node.Data);
        }
        public Node(T data)
        {
            Data = data;
            Visited = false;
            Path = null;
        }
    }

    class Edge<T>
    {
        public Node<T> From;
        public Node<T> To;
        public Edge(Node<T> from, Node<T> to)
        {
            From = from;
            To = to;
        }
    }
}
