using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis.MyCollections.IntegerCollections
{
    class MyIntegerFileArray : IntegerDataArray
    {
        private const int MaxArrayValue = 1000;

        public MyIntegerFileArray(string filename, int n, int seed,
            bool fillWithRandoms)
        {
            int[] data = new int[n];
            length = n;
            if (fillWithRandoms)
            {
                Random rand = new Random(seed);
                for (int i = 0; i < length; i++)
                {
                    data[i] = rand.Next(MaxArrayValue);
                }
            }
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public FileStream fs { get; set; }

        public override int this[int index]
        {
            get
            {
                Byte[] data = new Byte[4];
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                int result = BitConverter.ToInt32(data, 0);
                return result;
            }
            set
            {
                byte[] data = new byte[4];
                BitConverter.GetBytes(value).CopyTo(data, 0);
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Write(data, 0, 4);
            }
        }

        public override void Swap(int j, int a, int b)
        {
            Byte[] data = new Byte[16];
            BitConverter.GetBytes(a).CopyTo(data, 0);
            BitConverter.GetBytes(b).CopyTo(data, 4);
            fs.Seek(4 * (j - 1), SeekOrigin.Begin);
            fs.Write(data, 0, 8);
        }

    }
}
