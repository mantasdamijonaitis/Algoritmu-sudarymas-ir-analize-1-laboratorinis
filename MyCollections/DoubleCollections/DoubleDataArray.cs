using AlgorithmAnalisis.MyCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis
{
    abstract class DoubleDataArray : MyBaseCollection
    {
        public abstract double this[int index] { get; set; }
        public abstract void Swap(int j, double a, double b);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine(" {0:F5} ", this[i]);
            Console.WriteLine();
        }
    }
}
