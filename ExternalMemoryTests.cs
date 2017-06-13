using AlgorithmAnalisis.MyCollections.IntegerCollections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis
{
    class ExternalMemoryTests
    {

        private const int TestDataAmount = 100;
        private int Seed = 0;
        private const string DataFileName = "C:/programming/Learning/AlgorithmAnalisis/AlgorithmAnalisis/Data.txt";
        private const string ResultsFileName = "C:/programming/Learning/AlgorithmAnalisis/AlgorithmAnalisis/Helper.txt";
        private const string HelperFileName = "C:/programming/Learning/AlgorithmAnalisis/AlgorithmAnalisis/Results.txt";
        public ExternalMemoryTests()
        {
            Seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            //PerformTestsWithArrays();
            //PerformTestsWithLists();
            PerformTestsWithLinearHashTable();
        }

        private void PerformTestsWithLists()
        {
            PerformCountingSortTestWithList();
            //PerfomInsertionSortWithLists();
        }

        private string GenerateRandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void PerformTestsWithLinearHashTable()
        {
            File.Create(DataFileName).Close();
            LinearHashTableOfString hashTable =
                new LinearHashTableOfString(DataFileName);
            List<string> randomKeys = new List<string>();
            Random random = new Random();
            for (int i = 0; i < 500; i++)
            {
                string randomKey = GenerateRandomString(random, 20);
                randomKeys.Add(randomKey);
                hashTable.AddKeyValuePair(randomKey, (i + 1));
            }
            /*for (int i = 0; i < randomKeys.Count; i++)
            {
                if (!hashTable.KeyAlreadyExists(randomKeys[i]))
                {
                    Console.WriteLine("FAIL!!");
                }
            }*/
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < randomKeys.Count; i++)
            {
                hashTable.GetValueByKey(randomKeys[i]);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        private void PerformCountingSortTestWithList()
        {
            MyIntegerFileList data
               = new MyIntegerFileList(DataFileName, 50000,
               Seed, true);
            using (data.fs = new FileStream(
                DataFileName, FileMode.Open, FileAccess.ReadWrite))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                int[] dataArray = new int[data.Length];
                int dataArrayCounter = 0;
                for (int dataElement = data.Head();
                    dataElement != MyIntegerFileList.TokenEnd;
                    dataElement = data.Next())
                {
                    dataArray[dataArrayCounter] = dataElement;
                    dataArrayCounter++;
                }

                int min = dataArray[0];
                int max = dataArray[0];
                for (int i = 1; i < dataArray.Length; i++)
                {
                    if (dataArray[i] < min) min = dataArray[i];
                    else if (dataArray[i] > max) max = dataArray[i];
                }

                int[] counts = new int[max - min + 1];

                for (int i = 0; i < dataArray.Length; i++)
                {
                    counts[dataArray[i] - min]++;
                }

                counts[0]--;
                for (int i = 1; i < counts.Length; i++)
                {
                    counts[i] = counts[i] + counts[i - 1];
                }

                int[] aux = new int[dataArray.Length];
                for (int i = dataArray.Length - 1; i >= 0; i--)
                {
                    aux[counts[dataArray[i] - min]--] = dataArray[i];
                }

                dataArrayCounter = 0;
                for (int dataElement = data.Head();
                    dataElement != MyIntegerFileList.TokenEnd;
                    dataElement = data.Next())
                {
                    if (dataArrayCounter < dataArray.Length)
                    {
                        data.ChangeCurrentValue(aux[dataArrayCounter]);
                        dataArrayCounter ++;
                    }
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                /*dataArrayCounter = 0;
                for (int dataElement = data.Head();
                    dataElement != MyIntegerFileList.TokenEnd;
                    dataElement = data.Next())
                {
                    if (dataElement != aux[dataArrayCounter])
                    {
                        Console.WriteLine("Mismatch!!");
                    }
                    dataArrayCounter++;
                }*/

            }
        }

        private void PerfomInsertionSortWithLists()
        {
            MyDoubleFileList doubleFileList = new MyDoubleFileList(
                DataFileName, 5000, Seed);
            using (doubleFileList.fs = new FileStream(
                DataFileName, FileMode.Open, FileAccess.ReadWrite))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                double[] array = new double[doubleFileList.Length];
                int arrayCounter = 0;
                for (double fileItem = doubleFileList.Head();
                    fileItem != MyDoubleFileList.TokenEnd;
                    fileItem = doubleFileList.Next())
                {
                    array[arrayCounter] = fileItem;
                    arrayCounter++;
                }
                for (var counter = 0; counter < array.Length - 1; counter++)
                {
                    var index = counter + 1;
                    while (index > 0)
                    {
                        if ((array[index - 1] > array[index]))
                        {
                            var temp = array[index - 1];
                            array[index - 1] = array[index];
                            array[index] = temp;
                        }
                        index--;
                    }
                }
                arrayCounter = 0;
                for (double fileItem = doubleFileList.Head();
                    fileItem != MyDoubleFileList.TokenEnd;
                    fileItem = doubleFileList.Next())
                {
                    doubleFileList.ChangeCurrentValue(array[arrayCounter]);
                    arrayCounter++;
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
        }


        private void PerformInsertionSortTestWithArray()
        {

            MyDoubleFileArray doubleFileArray = new MyDoubleFileArray(
                DataFileName, 500, Seed);

            using (doubleFileArray.fs = new System.IO.FileStream(
                DataFileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < doubleFileArray.Length - 1; i++)
                {
                    int index = i + 1;
                    while (index > 0)
                    {
                        if (doubleFileArray[index - 1] > doubleFileArray[index])
                        {
                            double temp = doubleFileArray[index - 1];
                            doubleFileArray[index - 1] = doubleFileArray[index];
                            doubleFileArray[index] = temp;
                        }
                        index--;
                    }
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
        }

        private void PerformCountingSortTestWithArray()
        {
            MyIntegerFileArray integerFileArray = new MyIntegerFileArray(DataFileName,
                5000, Seed, true);
            using (integerFileArray.fs = new System.IO.FileStream(DataFileName,
                System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                int min = integerFileArray[0];
                int max = integerFileArray[0];
                for (int i = 1; i < integerFileArray.Length; i++)
                {
                    if (integerFileArray[i] < min) min = integerFileArray[i];
                    else if (integerFileArray[i] > max) max = integerFileArray[i];
                }

                MyIntegerFileArray counts = new MyIntegerFileArray(HelperFileName,
                    max - min + 1, 0, false);
                using (counts.fs = new System.IO.FileStream(
                    HelperFileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
                {
                    for (int i = 0; i < integerFileArray.Length; i++)
                    {
                        counts[integerFileArray[i] - min]++;
                    }
                    counts[0]--;
                    for (int i = 1; i < counts.Length; i++)
                    {
                        counts[i] = counts[i] + counts[i - 1];
                    }
                    MyIntegerFileArray aux = new MyIntegerFileArray(ResultsFileName,
                        integerFileArray.Length, 0, false);
                    using (aux.fs = new System.IO.FileStream(ResultsFileName, System.IO.FileMode.Open,
                        System.IO.FileAccess.ReadWrite))
                    {
                        for (int i = integerFileArray.Length - 1; i >= 0; i--)
                        {
                            aux[counts[integerFileArray[i] - min]--] = integerFileArray[i];
                        }
                    }
                    
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }

        }

        private void PerformTestsWithArrays()
        {
            PerformCountingSortTestWithArray();
            //PerformInsertionSortTestWithArray();
        }

    }
}
