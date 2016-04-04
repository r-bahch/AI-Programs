using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_k_NN
{
    public enum IrisClass { IrisSetosa, IrisVersicolor, IrisVirginica}

    public struct Iris
    {
        public double sepalLength;
        public double sepalWidth;
        public double petalLength;
        public double petalWidth;
        public IrisClass category;

        public Iris (double sepalLength, double sepalWidth, double petalLength, double petalWidth, string category)
        {
            this.sepalLength = sepalLength;
            this.sepalWidth = sepalWidth;
            this.petalLength = petalLength;
            this.petalWidth = petalWidth;
            if (category.Equals("Iris-versicolor"))
                this.category = IrisClass.IrisVersicolor;
            else if (category.Equals("Iris-setosa"))
                this.category = IrisClass.IrisSetosa;
            else this.category = IrisClass.IrisVirginica;
        }

        public Iris(double sepalLength, double sepalWidth, double petalLength, double petalWidth)
        {
            this.sepalLength = sepalLength;
            this.sepalWidth = sepalWidth;
            this.petalLength = petalLength;
            this.petalWidth = petalWidth;
            category = IrisClass.IrisSetosa;
        }
    }
}
