using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.DataStructures
{
    public static class HashingAlgorithms
    {
        public static int AdditiveHash(string input)
        {
            return input.Sum(c => (int) c);
        }

        public static int Djb2(string input)
        {
            var hash = 5381;
            foreach (var c in input.ToCharArray())
            {
                unchecked
                {
                    hash = ((hash << 5) + hash) + c;
                }
            }
            return hash;
        }

        public static int FoldingHash(string input)
        {
            var hashValue = 0;
            var startIndex = 0;
            int currentBytes;
            do
            {
                currentBytes = GetNextBytes(startIndex, input);
                unchecked
                {
                    hashValue += currentBytes;
                }
                startIndex += 4;
            } while (currentBytes != 0);
            return hashValue;
        }

        private static int GetNextBytes(int startIndex, string input)
        {
            var currentBytes = 0;
            currentBytes += GetByte(input, startIndex);
            currentBytes += GetByte(input, startIndex + 1) << 8;
            currentBytes += GetByte(input, startIndex + 2) << 16;
            currentBytes += GetByte(input, startIndex + 3) << 24;

            return currentBytes;
        }

        private static int GetByte(string input, int startIndex)
        {
            return startIndex < input.Length ? input[startIndex] : 0;
        }
    }
}
