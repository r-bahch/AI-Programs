using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_ConstraintSatisfactionQueens
{
    class ChessBoard
    {
        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random random;

        /// <summary>
        /// Index is row, value is position in row.
        /// </summary>
        private int[] queens;

        /// <summary>
        /// Size of the board.
        /// </summary>
        private int size;

        /// <summary>
        /// Total number of swaps performed, for performance measure
        /// </summary>
        public int Swaps { get; set; }
        
        /// <summary>
        /// Creates a new chess board of the specified size with the queens randomly distributed.
        /// </summary>
        /// <param name="n">Size of board</param>
        public ChessBoard(int n)
        {
            random = new Random();
            size = n;
            queens = DistributeQueens();
            Swaps = 0;
        }

        /// <summary>
        /// Randomly distributes the queens on the board.
        /// </summary>
        private int[] DistributeQueens()
        {
            return Enumerable.Range(0, size).OrderBy(x => random.Next()).ToArray();
        }

        /// <summary>
        /// Returns the number of conflicts with other queens by rows and diagonals
        /// of the queen in (row,column)
        /// </summary>
        /// <param name="row">Row position</param>
        /// <param name="column">Column position</param>
        /// <returns></returns>
        private int Conflicts(int row, int column)
        {
            int result = 0, r;
            for (int c = 0; c < size; c++)
                if (c != column)
                {
                    r = queens[c]; //(r,c) is the current queen
                    if (r == row || Math.Abs(r - row) == Math.Abs(c - column)) 
                    { //if the current queen is in conflict with the one in (row, column)
                        result++;
                    }
                }
            return result;
        }

        /// <summary>
        /// Rearanges the queens so that there are no conflicts. 
        /// </summary>
        public void Solve()
        {
            //number of queen swaps
            int steps = 0;

            //candidates for swapping
            List<int> possibleQueens = new List<int>();

            for (;;)
            {
                // Find queen/queens that have the most conflicts
                int maxConflictCount = 0, conflicts;
                possibleQueens.Clear();
                for (int c = 0; c < size; c++)
                {
                    conflicts = Conflicts(queens[c], c);
                    if (conflicts == maxConflictCount)
                    {
                        possibleQueens.Add(c);
                    }
                    else if (conflicts > maxConflictCount)
                    {
                        possibleQueens.Clear();
                        possibleQueens.Add(c);
                        maxConflictCount = conflicts;
                    }
                }

                if (maxConflictCount == 0)
                {
                    //if there are no conflicts
                    return;
                }

                // Pick a random queen with maximum conflicsts
                int mostConflictColumn = possibleQueens[random.Next(possibleQueens.Count)];

                // Find the square in the selected queen's column that has min conflicts
                int minConflictCount = queens.Length;
                possibleQueens.Clear();
                for (int r = 0; r < size; r++)
                {
                    conflicts = Conflicts(r, mostConflictColumn);
                    if (conflicts == minConflictCount)
                    {
                        possibleQueens.Add(r);
                    }
                    else if (conflicts < minConflictCount)
                    {
                        minConflictCount = conflicts;
                        possibleQueens.Clear();
                        possibleQueens.Add(r);
                    }
                }

                // put the queen in that square
                if (possibleQueens.Count > 0)
                {
                    queens[mostConflictColumn] =
                        possibleQueens[random.Next(possibleQueens.Count)];
                }

                steps++;
                Swaps++;

                if (steps > queens.Length * 2)
                {
                    //if the steps exeed the acceptable number, start over
                    queens = DistributeQueens();
                    steps = 0;
                }
            }
        }


        public override string ToString()
        {
            bool color;
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < size; r++)
            {
                if (r % 2 == 0) color = true;
                else color = false;
                for (int c = 0; c < size; c++)
                {
                    if (queens[c] == r)
                        sb.Append('Q');
                    else sb.Append(color? '█':' ');
                    color = !color;
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }



        public void Print()
        {
            bool color;
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < size; r++)
            {
                if (r % 2 == 0) color = true;
                else color = false;
                for (int c = 0; c < size; c++)
                {
                    if (color)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    if (queens[c] == r)
                        Console.Write('Q');
                    else
                        Console.Write(' ');
                    color = !color;
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
