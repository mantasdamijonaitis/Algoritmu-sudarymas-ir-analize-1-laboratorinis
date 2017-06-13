using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis.MyCollections.IntegerCollections
{
    abstract class IntegerDataList : MyBaseCollection
    {
        public abstract int Head();
        public abstract int Next();
        public abstract int Previous();
        public abstract void ChangeCurrentValue(int value);
        public abstract void Swap(int a, int b);
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
