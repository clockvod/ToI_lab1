using System.Runtime.Remoting.Messaging;
using System;
using System.Diagnostics;

namespace sharp_lab1
{
    public static class Vigener
    {
        private const int AlphabetSize = 26;
        private static char[,] table = new char[26, 26];
        private const int FirstChar = 97;
        private const int LastChar = 122;

        static Vigener()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < AlphabetSize; i++)
            {
                int k = i;
                for (int j = 0; j < AlphabetSize; j++)
                {
                    if (j + i == AlphabetSize) k = 0;
                    table[i, j] = alphabet[k];
                    k++;
                }
            }
        }

        public static void VigenerCode()
        {
            Console.WriteLine("enter string");
            string source = Console.ReadLine().ToLower();
            Console.WriteLine("enter key");
            string key = Console.ReadLine().ToLower();
            Console.WriteLine("1.encrypt");
            Console.WriteLine("2.decrypt");
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1 :
                    Console.WriteLine(Encrypt(source, key));
                    break;
                case 2 :
                    Console.WriteLine(Decrypt(source, key));
                    break;
            }
        }

        private static string CreateKey(string key, string source)
        {
            int finalLength = source.Length - key.Length;
            for (int i = 0; i < finalLength; i++)
            {
                key += source[i];
            }

            return key;
        }

        private static string Encrypt(string source, string key)
        {
            key = CreateKey(key, source);
            string result = "";
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] >= FirstChar && source[i] <= LastChar)
                {
                    result += table[source[i] - FirstChar, key[i] - FirstChar];
                }
            }

            return result;
        }

        private static string Decrypt(string source, string key)
        {
            int currPos = 0;
            int findChar;
            string result = "";
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] >= FirstChar && source[i] <= LastChar)
                {
                    if (i == key.Length - 1)
                    {
                        while (currPos < result.Length)
                        {
                            key += result[currPos];
                            currPos++;
                        }
                    }
                    int num = key[i] - FirstChar;
                    findChar = 0;
                    while (source[i] != table[num, findChar] && findChar < AlphabetSize)
                    {
                        findChar++;
                    }

                    result += table[0, findChar];

                }
            }

            return result;
        }
    }

    public static class Fense
    {
        public static void FenceCode()
        { 
            Console. WriteLine("enter string");
            string source = Console.ReadLine();
            Console.WriteLine("enter key >= 1");
            int key = int.Parse(Console.ReadLine());
            Console.WriteLine("1.encrypt");
            Console.WriteLine("2.decrypt");

            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1 :
                    Console.WriteLine(Encrypt(source, key));
                    break;
                case 2 :
                    Console.WriteLine(Decrypt(source, key));
                    break;
            }
        }

        private static char[,] CreateMatrix(string source, int key, bool encryption)
        {
            char[,] mass = new char[key, source.Length];
            int direction = -1;
            int row = 0, col = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (row == 0 || row == key - 1)
                    direction *= -1;
                if (encryption)
                    mass[row, col++] = source[i];
                else
                    mass[row, col++] = '+';

                row += direction;
            }

            return mass;
        }

        private static string Encrypt(string source, int key)
        {
            string result = "";
            char[,] matrix = CreateMatrix(source, key, true);
            for (int i = 0; i < key; i++)
                for (int j = 0; j < source.Length; j++)
                    if (matrix[i, j] != '\0') result += matrix[i, j];

            return result;
        }

        private static string Decrypt(string source, int key)
        {
            string result = "";
            char[,] matrix = CreateMatrix(source, key, false);
            int k = 0;
            for (int i = 0; i < key; i++) 
                for (int j = 0; j < source.Length; j++)
                    if (matrix[i, j] == '+') matrix[i, j] = source[k++];

            int direction = -1;
            int row = 0, col = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (row == 0 || row == key - 1)
                    direction *= -1;

                result += matrix[row, col++];
                row += direction;
            }

            return result;
        }
    }

    public static class Columns
    {
        public static void ColumnsCode()
        { 
            Console. WriteLine("enter string");
            string source = Console.ReadLine();
            Console.WriteLine("enter key");
            string key = Console.ReadLine();
            Console.WriteLine("1.encrypt");
            Console.WriteLine("2.decrypt");

            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1 :
                    Console.WriteLine(Encrypt(source, key));
                    break;
                case 2 :
                    Console.WriteLine(Decrypt(source, key));
                    break;
            }
        }
        
        private static string CorrectSpaces(string source, int length)
        {
            int remainder = source.Length % length;
            if (remainder != 0)
            {
                for (int i = 0; i < length - remainder; i++)
                {
                    source += " ";
                }
            }
            return source;
        }
        private static string[] SplitStringToRows(string source, int keyLength)
        {
            string[] result = new string[keyLength];
            int k = 0;
            for (int i = 0; i < source.Length; i++)
            {
                result[k] += source[i];
                k++;
                if (k == keyLength) k = 0;
            }
            return result;
        }
        private static string[] SplitStringToColumns(string source, int keyLength)
        {
            int size = source.Length / keyLength;
            string[] result = new string[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                result[i] = source.Substring(i * size, size);
            }
            return result;
        }
        private static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        public static string Encrypt(string source, string key)
        {
            int keyLength = key.Length;
            source = CorrectSpaces(source, keyLength);
            string[] columns = SplitStringToRows(source, keyLength);
            char[] sortableKey = new char[keyLength];
            for (int i = 0; i < keyLength; i++)
                sortableKey[i] = key[i];
            for (int i = 0; i < keyLength - 1; i++)
                for (int j = i + 1; j < keyLength; j++)
                    if (sortableKey[i] > sortableKey[j])
                    {
                        Swap(ref sortableKey[i], ref sortableKey[j]);
                        Swap(ref columns[i], ref columns[j]);
                    }
            string result = "";
            for (int i = 0; i < keyLength; i++)
                result += columns[i];
            return result;
        }
        public static string Decrypt(string source, string key)
        {
            int keyLength = key.Length;
            source = CorrectSpaces(source, keyLength);
            string[] columns = SplitStringToColumns(source, keyLength);
            char[,] sortableKey = new char[keyLength, 2];
            for (int i = 0; i < keyLength; i++)
            {
                sortableKey[i, 0] = key[i];
                sortableKey[i, 1] = Convert.ToChar(i);
            }
            for (int i = 0; i < keyLength - 1; i++)
                for (int j = i + 1; j < keyLength; j++)
                    if (sortableKey[i, 0] > sortableKey[j, 0])
                    {
                        char[,] tmp = new char[1, 2];
                        tmp[0, 0] = sortableKey[i, 0];
                        tmp[0, 1] = sortableKey[i, 1];
                        sortableKey[i, 0] = sortableKey[j, 0];
                        sortableKey[i, 1] = sortableKey[j, 1];
                        sortableKey[j, 0] = tmp[0, 0];
                        sortableKey[j, 1] = tmp[0, 1];
                    }
            string[] sortedColumns = new string[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                sortedColumns[sortableKey[i, 1]] = columns[i];
            }
            string result = "";
            for (int i = 0; i < source.Length / keyLength; i++)
                for (int j = 0; j < keyLength; j++)
                {
                    result += sortedColumns[j][i];
                }
            return result;
        }
    }
}