using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis.MyCollections.IntegerCollections
{
    abstract class IntegerDataArray : MyBaseCollection
    {
        public abstract int this[int index] { get; set; }
        public abstract void Swap(int j, int a, int b);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine(" {0} ", this[i]);
            Console.WriteLine();
        }
    }
}
