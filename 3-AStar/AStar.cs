using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace _3_AStar
{
    class AStar
    {
        public int Explored { get; private set; }   //how many nodes are in the closed set
        private Node[,] nodes;
        private Node start;
        private Node end;
        
        /// <summary>
        /// Creates an instance of the AStar class.
        /// </summary>
        /// <param name="grid">Represents the map in which to search</param>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        public AStar (bool[,] grid, Point start, Point end)
        {
            Explored = 0;
            PopulateNodes(grid, end);
            this.start = nodes[start.X, start.Y];
            this.end = nodes[end.X, end.Y];
            this.start.State = StateOfNode.Open;
        }

        /// <summary>
        /// Creates the matrix of nodes that represents the map
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="end">End point coordinates</param>
        private void PopulateNodes (bool[,] grid, Point end)
        {
            nodes = new Node[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < nodes.GetLength(1); i++)        //columns (x)
            {
                for (int j = 0; j < nodes.GetLength(0); j++)    //rows (y)
                {
                    nodes[j,i] = new Node(j, i, grid[j, i], end);
                }
            }
        }

        /// <summary>
        /// Searches for path to the end node in the grid.
        /// </summary>
        /// <param name="start">Node from where to start the search</param>
        /// <returns></returns>
        private bool SearchPath (Node start)
        {
            if (!start.Available)
                return false;
            if (start.Equals(this.end))
                return true;
            start.State = StateOfNode.Closed;
            Explored++; //for performance measure
            UpdateNeighborNodes(start);

            //get all the open nodes
            List<Node> openNodes = new List<Node>();
            for (int i = 0; i < nodes.GetLength(1); i++)
            {
                for (int j = 0; j < nodes.GetLength(0); j++)
                {
                    if (nodes[j, i].State == StateOfNode.Open)
                        openNodes.Add(nodes[j, i]);
                }
            }

            //nextNode is the open node with minimal totalCost
            Node nextNode = openNodes.Min();

            if (nextNode != null) //if there is a valid next node
            {
                if (SearchPath(nextNode))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the state and cost of the neigbors
        /// </summary>
        /// <param name="n">The node whose neighbors are updated</param>
        /// <returns></returns>
        private void UpdateNeighborNodes (Node n)
        {
            int x = n.Location.X;
            int y = n.Location.Y;
            Point[] neigborPoints = new Point[] { //contains all the adjasent point coordinates
                new Point(x - 1, y - 1),
                new Point(x - 1, y),
                new Point(x - 1, y + 1),
                new Point(x, y + 1),
                new Point(x + 1, y + 1),
                new Point(x + 1, y),
                new Point(x + 1, y - 1),
                new Point(x, y - 1),
            };

            //update state
            foreach (Point neighborPoint in neigborPoints)
            {
                //check whether the coordinates are in the map
                if (neighborPoint.X >= 0 && neighborPoint.X < nodes.GetLength(0) && 
                    neighborPoint.Y >= 0 && neighborPoint.Y < nodes.GetLength(1))
                {
                    Node neighborNode = this.nodes[neighborPoint.X, neighborPoint.Y];
                    //check whether the node is not closed and walkable
                    if (neighborNode.Available && neighborNode.State != StateOfNode.Closed)
                    {
                        double curCostToNeighboor = n.Cost +
                                Node.GetPointDistance(n.Location, neighborNode.Location);
                        if (neighborNode.State == StateOfNode.Open)
                        {
                            //if the state of the neighbor is open, only update it's cost if the new 
                            //cost is less than the current one
                            if (curCostToNeighboor < neighborNode.Cost)
                            {
                                neighborNode.Parrent = n;
                                neighborNode.Cost = curCostToNeighboor;
                            }
                        }
                        else //if unexplored, make it open and update cost
                        {
                            neighborNode.Parrent = n;
                            neighborNode.Cost = curCostToNeighboor;
                            neighborNode.State = StateOfNode.Open; //set state from unexplored to open

                        }
                    }
                }
            }
        } 

        /// <summary>
        /// Returns the path between start and end point
        /// </summary>
        /// <returns></returns>
        public List<Point> GetPath()
        {
            List<Point> result = new List<Point>();
            if (SearchPath(start))
            {
                Node iter = end;
                while (iter.Parrent!= null)
                {
                    result.Add(iter.Location);
                    iter = iter.Parrent;
                }
                result.Add(iter.Location);
                result.Reverse();
            }
            return result;
        }
    }
}
