using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_DFS
{
    class Graph<T>
    {
        public List<T> Nodes { get; private set; } 
        public List<Tuple<T,T>> Edges { get; private set; }

        public Graph()
        {
            Nodes = new List<T>();
            Edges = new List<Tuple<T, T>>();
        }

        public Graph(List<T> nodes, List<Tuple<T,T>> edges)
        {
            Nodes = nodes;
            Edges = edges;  
        }

        public List<T> getChildren (T node)
        {
            List<T> result = new List<T>();
            foreach (var item in Edges)
            {
                if (item.Item1.Equals(node))
                {
                    result.Add(item.Item2);
                }
            }
            return result;
        }

        public List<T> DFS (T start, T end)
        {
            if (start.Equals(end) && Nodes.Contains(start))
            {
                return new List<T> { start };
            }
            List<List<T>> visited = new List<List<T>>();
            visited.Add(new List<T> { start });
            Stack<T> stack = new Stack<T>();
            stack.Push(start);
            while (stack.Count != 0)
            {
                T current = stack.Pop();
                Console.WriteLine(current);
                List<T> pathToCurrent = new List<T>(visited.Find(x => x.Last().Equals(current)));
                if(current.Equals(end))
                {
                    return pathToCurrent;
                }
                List<T> curChildren = getChildren(current);
                foreach (var child in curChildren)
                {
                    if (!visited.Exists(x => x.Last().Equals(child)))
                    {
                        stack.Push(child);
                        List<T> pathToChild = new List<T>(pathToCurrent);
                        pathToChild.Add(child);
                        visited.Add(pathToChild);
                    }
                }
            }
            return null;
        }
    }
}
