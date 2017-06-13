using AlgorithmAnalisis.MyCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis
{
    abstract class DoubleDataList : MyBaseCollection
    {
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(double a, double b);
        public abstract void ChangeCurrentValue(double newValue);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for
           (int i = 1; i < n; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();
        }
    }
}
