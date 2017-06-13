using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis.MyCollections.IntegerCollections
{
    class MyIntegerFileList : IntegerDataList
    {
        int prevNode;
        int currentNode;
        int nextNode;

        public const int TokenEnd = -256;

        private const int MaxArrayValue = 100;
        private const int DefaultValue = 0;

        public MyIntegerFileList(string filename, int n, int seed,
            bool fillWithRandoms)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
                    FileMode.Create)))
                {
                    for (int j = 0; j < /*length*/length; j++)
                    {
                        int randomValue =
                        fillWithRandoms ? rand.Next(MaxArrayValue) : DefaultValue;
                        writer.Write(randomValue);
                        writer.Write((j + 1) * 8);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public FileStream fs { get; set; }
        public override int Head()
        {
            byte[] data = new byte[16];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 16);
            prevNode = -1;
            int result = BitConverter.ToInt32(data, 0);
            currentNode = 0;
            nextNode = BitConverter.ToInt32(data, 12);
            return result;
        }

        public override int Next()
        {
            byte[] data = new byte[8];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            prevNode = currentNode;
            currentNode = nextNode;
            int result = BitConverter.ToInt32(data, 0);
            nextNode = BitConverter.ToInt32(data, 4);
            return nextNode != 0 ? result : TokenEnd;        }

        public override int Previous()
        {
            if (prevNode == 0)
            {
                fs.Seek(prevNode, SeekOrigin.Begin);
                byte[] lastData = new byte[4];
                nextNode = currentNode;
                currentNode = 0;
                prevNode = -1;
                return BitConverter.ToInt32(lastData, 0);
            }
            else if (prevNode == -1)
            {
                return TokenEnd;
            }
            byte[] data = new byte[16];
            fs.Seek(prevNode - 8, SeekOrigin.Begin);
            fs.Read(data, 0, 16);
            nextNode = currentNode;
            currentNode = prevNode;
            prevNode -= 8;
            int result = BitConverter.ToInt32(data, 8);
            return prevNode >= 0 ? result : TokenEnd;
        }

        public override void ChangeCurrentValue(int value)
        {

            byte[] bytesToWrite = BitConverter.GetBytes(value);
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Write(BitConverter.GetBytes(value), 0, 4);
            byte[] updatedValue = new byte[8];
            fs.Read(updatedValue, 0, 4);
            int newValue = BitConverter.ToInt32(updatedValue, 0);
        }

        public override void Swap(int a, int b)
        {
            Byte[] data;
            fs.Seek(prevNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(a);
            fs.Write(data, 0, 4);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(b);
            fs.Write(data, 0, 4);
        }

    }
}
