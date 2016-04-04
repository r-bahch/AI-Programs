using System;
using System.Diagnostics;

namespace _5_ConstraintSatisfactionQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard(50);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            board.Solve();
            sw.Stop();
            Console.WriteLine("Time elapsed: {0} s", sw.Elapsed);
            Console.WriteLine("Number of swaps performed: {0}", board.Swaps);
            board.Print();
        }
    }
}
