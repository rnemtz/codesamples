using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises
{
    public static class BinarySearch
    {
        /* 
         * Simplest Approach
         * Scan Full Array
         * Big O => O(N)
         */
        public static int Search(int[] a, int n, int x)
        {
            for (var i = 0; i < n; i++) if (a[i] == x) return i;
            return -1;
        }

        /*
         * Binary Search Within an Array
         * Divides each time the Array in two
         * Big O = > O(log N)
         *
         */

        public static int SearchBinary(int[]a, int n, int x)
        {
            var low = 0;
            var high = n - 1;
            
            while (low <= high)
            {
                //Calculate Mid
                var mid = (high + low) / 2;
                //check it mid is the number we need;
                if (a[mid] == x) return mid;
                //Check if the value is in the left of the array
                if (x < a[mid]) high = mid - 1;
                //Right part of the array
                else low = mid + 1;
            }

            //return not found.
            return -1;
        }

        public static int SearchBinaryRecursive(int[] a, int low, int high, int x)
        {
            if (low > high) return -1;
            var mid = low + (high - low) / 2;
            if (a[mid] == x) return mid;
            return x < a[mid] ? 
                SearchBinaryRecursive(a, low, mid - 1, x) 
                : SearchBinaryRecursive(a, mid + 1, high, x);
        }
    }
}
