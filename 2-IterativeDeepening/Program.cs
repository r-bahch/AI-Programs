using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_IterativeDeepening
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> g = new Graph<string>();
            StreamReader sr = new StreamReader("input.txt");
            //populate graph
            List<string> nodes = sr.ReadLine().Split(',').ToList();
            foreach (var node in nodes)
            {
                g.AddNode(node);
            }
            string[] edges = sr.ReadLine().Split(';');
            foreach (var edge in edges)
            {
                string[] tmp = edge.Trim('(', ')', ' ').Split(',');
                if (tmp.Length == 2)
                    g.AddEdge(tmp[0], tmp[1]);
            }
            
            //enter start and end nodes
            string start = (sr.ReadLine().Trim());
            string end = sr.ReadLine().Trim();

            //get path
            var path = g.IterativeDeepening(start, end);
            if (path.Count > 0)
                Console.WriteLine("Path is: " + string.Join(",", path));
            else Console.WriteLine("No path exists!");
        }
    }
}
