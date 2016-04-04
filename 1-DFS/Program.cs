using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_DFS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter nodes, separeted by commas.");
            List<string> nodes = Console.ReadLine().Split(',').ToList();
            Console.WriteLine("Enter edges in the following format: (node,node); (node,node); ...");
            string[] edges = Console.ReadLine().Split(';');
            List<Tuple<string, string>> edgeTuples = new List<Tuple<string, string>>();
            foreach (var edge in edges)
            {
                string[] tmp = edge.Trim('(', ')', ' ').Split(',');
                if (tmp.Length == 2)
                    edgeTuples.Add(new Tuple<string,string>(tmp[0],tmp[1]));
            }
            Graph<string> g = new Graph<string>(nodes, edgeTuples);
            Console.Write("Enter start node: ");
            string start = Console.ReadLine().Trim();
            Console.Write("Enter end node: ");
            string end = Console.ReadLine().Trim();
            List<string> path = g.DFS(start, end);
            if (path != null)
                Console.WriteLine("Path is: " + string.Join(",", path));
            else Console.WriteLine("No path exists!");
        }
    }
}
