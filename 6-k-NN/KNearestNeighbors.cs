using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace _6_k_NN
{
    public class KNearestNeighbors
    {
        public List<Iris> database { get; set; }
        public int K { get; set; }
        double maxSepalLength;
        double maxSepalWidth;
        double maxPetalLength;
        double maxPetalWidth;

        public KNearestNeighbors(string inputFileName, int k)
        {
            K = k;
            database = new List<Iris>();
            updateDatabase(inputFileName);
            maxSepalLength = database.Max(item => item.sepalLength);
            maxSepalWidth = database.Max(item => item.sepalWidth);
            maxPetalLength = database.Max(item => item.petalLength);
            maxPetalWidth = database.Max(item => item.petalWidth);
        }

        public void updateDatabase (string inputFileName)
        {
            string buffer;
            string[] line;
            CultureInfo en = new CultureInfo("en");
            StreamReader sr = new StreamReader(inputFileName);
            while ((buffer = sr.ReadLine())!=null)
            {
                line = buffer.Split(',');
                database.Add(new Iris(double.Parse(line[0], en), double.Parse(line[1], en),
                    double.Parse(line[2],en), double.Parse(line[3],en), line[4]));
            }
        }

        public IrisClass GetCategory (Iris specimen)
        {
            Iris[] sorted = database.OrderBy(item => getDistance(specimen, item)).ToArray();
            int setosaCount = 0, versicolorCount = 0, virginicaCount = 0;
            for (int i = 0; i < K; i++)
            {
                if (sorted[i].category == IrisClass.IrisSetosa)
                    setosaCount++;
                else if (sorted[i].category == IrisClass.IrisVersicolor)
                    versicolorCount++;
                else virginicaCount++;
            }

            int max = Math.Max(setosaCount, Math.Max(versicolorCount,virginicaCount));

            if (max == setosaCount)
                return IrisClass.IrisSetosa;
            else if (max == versicolorCount)
                return IrisClass.IrisVersicolor;
            else return IrisClass.IrisVirginica;
        }

        private double getDistance(Iris from, Iris to)
        {
            return Math.Abs(from.sepalLength - to.sepalLength) / maxSepalLength +
                Math.Abs(from.sepalWidth - to.sepalWidth) / maxSepalWidth +
                Math.Abs(from.petalLength - to.petalLength) / maxPetalLength +
                Math.Abs(from.petalWidth - to.petalWidth) / maxPetalWidth;
        }
    }
}
