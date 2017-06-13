using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis
{
    public class InternalTests
    {

        private const int TestDataAmount = 50;
        private const int BiggestTestInteger = 100;

        public InternalTests()
        {
            Random random = new Random();
            PerformTestsWithArrays(random);
            //PerformTestsWithLists(random);
            //PerfromTestsWithLinearList(random);
        }

        private string GenerateRandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void PerfromTestsWithLinearList(Random random)
        {
            LinearHashTable<string, string> studentTable = new LinearHashTable<string, string>();
            List<string> randomKeys = new List<string>();
            for(int i = 0; i < 500000; i++)
            {
                string randomKey = GenerateRandomString(random, 20);
                randomKeys.Add(randomKey);
                studentTable.Add(randomKey, GenerateRandomString(random, 20));
            }
            long currentTime = DateTime.Now.ToFileTime();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach(string key in randomKeys)
            {
                int iterationAmount;
                string value;
                studentTable.TryGetValue(key, out value, out iterationAmount);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        

        private void PerformTestsWithLists(Random random)
        {
            /*double[] randomDoublesForInsertionSort = Enumerable.Range(0, 5000)
                .Select(r => random.NextDouble())
                .ToArray();
            LinkedList<double> insertionLinkedList = new LinkedList<double>(randomDoublesForInsertionSort);*/
            //PrintDoubleLinkedList(insertionLinkedList, "LinkedList pries InsertionSort");
            /*Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            insertionLinkedList = PerformInsertionSortWithLists(insertionLinkedList);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);*/
            //PrintDoubleLinkedList(insertionLinkedList, "LinkedList po InsertionSort");
            int[] randomIntegers = Enumerable.Range(0, 50000)
                 .Select(r => random.Next(BiggestTestInteger))
                 .ToArray();
            LinkedList<int> randomIntsForCountingSort = new LinkedList<int>(randomIntegers);
            //PrintIntegerLinkedList(randomIntsForCountingSort, "Pries CountingSort");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            randomIntsForCountingSort = PerformCountingSortWithLists(randomIntsForCountingSort);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //PrintIntegerLinkedList(randomIntsForCountingSort, "Po CountingSort");
        }

        private void PerformTestsWithArrays(Random random)
        {
            double[] randomData = Enumerable.Range(0, 5000)
                .Select(r => random.NextDouble())
                .ToArray();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            randomData = PerformInsertionSortWithArrays(randomData);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //PrintDoubleArray(randomData, "Po InsertionSort rikiavimo");
            /*
            int[] randomIntegers = Enumerable.Range(0, 5000000)
                 .Select(r => random.Next(BiggestTestInteger))
                 .ToArray();*/
            //PrinIntegerArray(randomIntegers, "Pries CountingSortRikiavima");
            /*Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            randomIntegers = PerformCountingSortWithArrays(randomIntegers);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);*/
            //PrinIntegerArray(randomIntegers, "Po CountingSortRikiavimo");*/
        }

        private void PrintIntegerLinkedList(LinkedList<int> listToPrint, string message)
        {
            Console.WriteLine(message);
            foreach (double number in listToPrint)
            {
                Console.WriteLine(number);
            }
        }

        private void PrintDoubleLinkedList(LinkedList<double> listToPrint, string message)
        {
            Console.WriteLine(message);
            foreach (double number in listToPrint)
            {
                Console.WriteLine(number);
            }
        }

        private void PrintDoubleArray(double[] randomData, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < randomData.Length; i++)
            {
                Console.WriteLine(randomData[i]);
            }
        }

        private void PrinIntegerArray(int[] randomData, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < randomData.Length; i++)
            {
                Console.WriteLine(randomData[i]);
            }
        }

        private LinkedList<double> PerformInsertionSortWithLists(LinkedList<double> inputList)
        {
            LinkedList<double> workingList = new LinkedList<double>(inputList);
            for (LinkedListNode<double> counterNode = workingList.First; counterNode.Next != null; counterNode = counterNode.Next)
            {
                LinkedListNode<double> indexNode = counterNode.Next;
                while (indexNode.Previous != null)
                {
                    if (indexNode.Previous.Value > indexNode.Value)
                    {
                        double temporaryValue = indexNode.Previous.Value;
                        indexNode.Previous.Value = indexNode.Value;
                        indexNode.Value = temporaryValue;
                    }
                    indexNode = indexNode.Previous;
                }
            }
            return workingList;
        }

        private T[] PerformInsertionSortWithArrays<T>(T[] inputarray, Comparer<T> comparer = null)
        {
            var equalityComparer = comparer ?? Comparer<T>.Default;
            for (var counter = 0; counter < inputarray.Length - 1; counter++)
            {
                var index = counter + 1;
                while (index > 0)
                {
                    if (equalityComparer.Compare(inputarray[index - 1], inputarray[index]) > 0)
                    {
                        var temp = inputarray[index - 1];
                        inputarray[index - 1] = inputarray[index];
                        inputarray[index] = temp;
                    }
                    index--;
                }
            }
            return inputarray;
        }

        private LinkedList<int> PerformCountingSortWithLists(LinkedList<int> inputList)
        {
            LinkedList<int> workingList = new LinkedList<int>(inputList);
            LinkedList<int> listToReturn = new LinkedList<int>(new int[inputList.Count]);

            int min = workingList.First.Value;
            int max = workingList.First.Value;

            for (LinkedListNode<int> currentNode = workingList.First.Next; currentNode != null; currentNode = currentNode.Next)
            {
                if (currentNode.Value < min)
                {
                    min = currentNode.Value;
                }
                else if (currentNode.Value > max)
                {
                    max = currentNode.Value;
                }
            }

            Console.WriteLine("MAX: " + max);
            Console.WriteLine("MIN: " + min);

            LinkedList<int> counts = new LinkedList<int>(new int[max - min + 1]);

            for (LinkedListNode<int> currentNode = workingList.First; currentNode != null; currentNode = currentNode.Next)
            {
                int index = currentNode.Value - min;
                int counter = 0;
                for (LinkedListNode<int> countsNode = counts.First; countsNode != null; countsNode = countsNode.Next)
                {
                    if (counter == index)
                    {
                        countsNode.Value++;
                    }
                    counter++;
                }
            }

            counts.First.Value--;
            for (LinkedListNode<int> currentNode = counts.First.Next; currentNode != null; currentNode = currentNode.Next)
            {
                currentNode.Value += currentNode.Previous.Value;
            }

            for (LinkedListNode<int> workingNode = workingList.Last; workingNode != null; workingNode = workingNode.Previous)
            {
                int firstIndex = (workingNode.Value - min);
                int countsCounter = 0;
                for (LinkedListNode<int> countsNode = counts.First; countsNode != null; countsNode = countsNode.Next)
                {
                    if (countsCounter == firstIndex)
                    {
                        int returnCounter = 0;
                        for (LinkedListNode<int> returnNode = listToReturn.First; returnNode != null; returnNode = returnNode.Next)
                        {
                            if (returnCounter == countsNode.Value)
                            {
                                returnNode.Value = workingNode.Value;
                            }
                            returnCounter++;
                        }
                    }
                    countsCounter++;
                }
            }

            for (LinkedListNode<int> resultsNode = listToReturn.First.Next; resultsNode != null; resultsNode = resultsNode.Next)
            {
                if (resultsNode.Previous.Value == 0)
                {
                    for (LinkedListNode<int> previousNode = resultsNode.Previous; previousNode != null; previousNode = previousNode.Previous)
                    {
                        if (previousNode.Value != 0)
                        {
                            break;
                        }
                        previousNode.Value = resultsNode.Value;
                    }
                }
            }

            return listToReturn;

        }

        private int[] PerformCountingSortWithArrays(int[] array)
        {
            int[] aux = new int[array.Length];

            int min = array[0];
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min) min = array[i];
                else if (array[i] > max) max = array[i];
            }

            int[] counts = new int[max - min + 1];

            for (int i = 0; i < array.Length; i++)
            {
                counts[array[i] - min]++;
            }

            counts[0]--;
            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] = counts[i] + counts[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                aux[counts[array[i] - min]--] = array[i];
            }

            return aux;
        }


    }

}
