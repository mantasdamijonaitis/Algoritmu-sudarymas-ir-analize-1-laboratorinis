using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis
{
    class LinearHashTableOfString
    {

        private string fileName = string.Empty;
        private const int PrimeToMultiply = 3;
        int CurrentCollectionSize = 0;

        private static char[] Separator = "!~!".ToCharArray();

        public LinearHashTableOfString(string fileName)
        {
            this.fileName = fileName;
        }

        public void AddKeyValuePair(string key, int value)
        {
            if (string.IsNullOrEmpty(fileName) || KeyAlreadyExists(key))
            {
                return;
            }
            InsertKeyValuePair(key, value);
        }

        public bool KeyAlreadyExists(string key)
        {
            using (FileStream fileStream = new FileStream(
                fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                try
                {
                    int index = GetKeyHash(key);
                    if (index < 0)
                    {
                        index *= -1;
                    }
                    fileStream.Seek(index, SeekOrigin.Begin);
                    byte[] dataAsInt = new byte[4];
                    fileStream.Read(dataAsInt, 0, 4);
                    int valueFromFile = BitConverter.ToInt32(dataAsInt, 0);
                    return valueFromFile != 0;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public int GetValueByKey(string key)
        {
            if (KeyAlreadyExists(key))
            {
                using (FileStream fileStream = new FileStream(
                    fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    int index = GetKeyHash(key);
                    if (index < 0)
                    {
                        index *= -1;
                    }
                    fileStream.Seek(index, SeekOrigin.Begin);
                    byte[] valueAsBytes = new byte[4];
                    fileStream.Read(valueAsBytes, 0, 4);
                    return BitConverter.ToInt16(valueAsBytes, 0);
                }
            }
            return 0;
        }

        private int GetKeyHash(string key)
        {
            return key.GetHashCode() * PrimeToMultiply;
        }

        private bool InsertKeyValuePair(string key, int value) { 
            using (FileStream fileStream = new FileStream(
                fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                try
                {
                    int index = GetKeyHash(key);
                    if (index < 0)
                    {
                        index *= -1;
                    }
                    fileStream.Seek(index, SeekOrigin.Begin);
                    fileStream.Write(BitConverter.GetBytes(value), 0, 4);
                    CurrentCollectionSize++;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }   
        
    }

}
