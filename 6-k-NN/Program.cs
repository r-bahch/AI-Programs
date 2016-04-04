using System.IO;
using System;
using System.Collections.Generic;

namespace _6_k_NN
{
    class Program
    {
        static void Main(string[] args)
        {
            KNearestNeighbors knn = new KNearestNeighbors("input.txt", 9);
            Console.WriteLine("enter the new iris parameters");
            double sl, sw, pl, pw;
            System.Globalization.CultureInfo en = new System.Globalization.CultureInfo("en");
            Console.Write("sepal length: ");
            sl = double.Parse(Console.ReadLine(), en);
            Console.Write("sepal width: ");
            sw = double.Parse(Console.ReadLine(), en);
            Console.Write("petal length: ");
            pl = double.Parse(Console.ReadLine(), en);
            Console.Write("petal width: ");
            pw = double.Parse(Console.ReadLine(), en);

            Console.WriteLine("entered iris is of class: {0}", knn.GetCategory(new Iris(sl, sw, pl, pw)));

            //Console.WriteLine( knn.GetCategory(new Iris(1,1,1,1)));
        }
    }
}
