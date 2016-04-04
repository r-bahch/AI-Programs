using System;
using System.Drawing;

namespace _3_AStar
{
    public enum StateOfNode {Open, Closed, Unexplored };
    public class Node : IComparable
    {
        /// <summary>
        /// node's parrent 
        /// </summary>
        public Node Parrent { get; set; }

        /// <summary>
        /// node's location on the grid
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// true if the node is not blocked
        /// </summary>
        public bool Available { get; private set; }

        /// <summary>
        /// cost of path from start node to this node
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// estimated cost from this node to final node
        /// </summary>
        public double Heuristic { get; private set; }

        /// <summary>
        /// state of this node - open, closed or unexplored
        /// </summary>
        public StateOfNode State { get; set; }

        /// <summary>
        /// estimated total cost from start to end trough this node
        /// </summary>
        public double TotalCost
        {
            get { return Heuristic + Cost; }
        }

        /// <summary>
        /// creates a new instance of Node
        /// </summary>
        public Node (int xCoord, int yCoord, bool available, Point endPoint)
        {
            this.Location = new Point(xCoord, yCoord);
            this.Available = available;
            this.Cost = 0;
            this.Heuristic = EvaluateHeuristic(Location, endPoint);
            this.State = StateOfNode.Unexplored;
        }

        /// <summary>
        /// heuristic function
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public double EvaluateHeuristic (Point from, Point to)
        {
            return GetPointDistance(from, to)*5;
            //return (Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y));
        }

        /// <summary>
        /// gets the estimated distance between two poins
        /// </summary>
        /// <param name="from">start point</param>
        /// <param name="to">end point</param>
        /// <returns></returns>
        public static double GetPointDistance(Point from, Point to)
        {
            int a = from.X - to.X;
            int b = from.Y - to.Y;
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// compares nodes by TotalCost
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Node otherNode = obj as Node;
            if (otherNode != null)
                return this.TotalCost.CompareTo(otherNode.TotalCost);
            else
                throw new ArgumentException("Object is not a Node");
        }
    }
}
