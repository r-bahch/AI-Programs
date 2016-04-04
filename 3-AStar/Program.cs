using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            ////  □ □ □ □ □ □ □ □
            ////  □ □ □ □ □ □ □ □
            ////  □ S □ □ □ E □ □
            ////  □ □ □ □ □ □ □ □
            ////  □ □ □ □ □ □ □ □
            ////  □ □ □ □ □ □ □ □
            ////  □ □ □ □ □ □ □ □

            //bool[,] map = new bool[8, 7];
            //for (int i = 0; i < 7; i++)
            //    for (int j = 0; j < 8; j++)
            //        map[j, i] = true;

            //var startLocation = new Point(1, 3);
            //var endLocation = new Point(6, 3);



            //  □ □ □ ■ □ □ □ □
            //  □ □ □ ■ □ □ □ □
            //  □ □ □ ■ □ E □ □
            //  □ □ □ ■ □ □ □ □
            //  S □ □ ■ □ □ □ □
            //  □ □ □ ■ ■ □ □ □
            //  □ □ □ □ □ □ □ □

            bool[,] map = new bool[8, 7];
            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 8; x++)
                    map[x, y] = true;

            map[3, 5] = false;
            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[3, 0] = false;
            map[4, 5] = false;

            var startLocation = new Point(0, 4);
            var endLocation = new Point(5, 2);



            //  □ □ □ ■ □ □ □ □
            //  □ □ □ ■ □ □ □ □
            //  □ □ □ ■ □ □ □ □
            //  □ □ □ ■ □ E □ □
            //  S □ □ ■ □ □ □ □
            //  □ □ □ ■ ■ □ □ □
            //  □ □ □ ■ □ □ □ □

            //bool[,] map = new bool[8, 7];
            //for (int y = 0; y < 7; y++)
            //    for (int x = 0; x < 8; x++)
            //        map[x, y] = true;

            //map[3, 6] = false;
            //map[3, 5] = false;
            //map[3, 4] = false;
            //map[3, 3] = false;
            //map[3, 2] = false;
            //map[3, 1] = false;
            //map[3, 0] = false;
            //map[4, 1] = false;

            //var startLocation = new Point(0, 4);
            //var endLocation = new Point(5, 3);


            AStar a = new AStar(map, startLocation, endLocation);
            List<Point> path = a.GetPath();
            if (path.Count == 0)
            {
                ShowRoute(path, map, startLocation, endLocation);
                Console.WriteLine();
                Console.WriteLine("No Path!");
                return;
            }
            Console.WriteLine("Path:");
            for (int i = 0; i < path.Count-1; i++)
            {
                Console.Write("({0},{1}); ", path[i].X, path[i].Y);
            }
            Console.WriteLine("({0},{1})", path.Last().X, path.Last().Y);
            Console.WriteLine("explored: " + a.Explored);
            Console.WriteLine();
            ShowRoute(path, map, startLocation, endLocation);
            Console.WriteLine();
        }

        //TODO: Change that shit
        private static void ShowRoute(List<Point> path, bool[,] map, Point start, Point end)
        {
            for (int i = 0; i <  map.GetLength(1); i++) 
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    Point p = new Point(j, i);
                    if (p.Equals(start))
                        Console.Write('S');
                    else if (p.Equals(end))
                        Console.Write('E');
                    else if (map[j, i] == false)
                        Console.Write('█');
                    else if (path.Contains(p))
                        Console.Write('♦');
                    else
                        Console.Write('·');
                }

                Console.WriteLine();
            }
        }
       
    }
}
