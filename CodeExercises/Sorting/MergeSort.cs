using System;

namespace CodeExercises.Sorting
{
    public class MergeSort
    {
        public static int Comparissons { get; set; }

        public MergeSort()
        {
            Comparissons = 0;
        }
        public static int[] Ascending(int[] array)
        {
            Comparissons = 0;
            return Sort(Order.Ascending, array, 0, array.Length);
        }

        public static int[] Descending(int[] array)
        {
            Comparissons = 0;
            return Sort(Order.Descending, array, 0, array.Length);
        }

        private static int[] Sort(Order order, int[] array, int low, int high)
        {
            //Fase 1: Base Cases
            if (high - low < 2) return new[] {array[low]};

            //Fase 2: Split
            var middle = low + (high - low) / 2; //So we dont get int overflow exception;
            var left = Sort(order, array, low, middle); // Recursively until there's nothing to split
            var right = Sort(order, array, middle, high);

            //Fase 3 - Sort and Merge
            var result = new int[left.Length + right.Length];
            var indexL = 0;
            var indexR = 0;
            int i;

            for (i = 0; indexL < left.Length && indexR < right.Length; i++)
            {
                Comparissons++;
                switch (order)
                {
                    case Order.Ascending:
                        if (left[indexL] < right[indexR]) result[i] = left[indexL++];
                        else result[i] = right[indexR++];
                        break;
                    case Order.Descending:
                        if (right[indexR] >= left[indexL]) result[i] = right[indexR++];
                        else result[i] = left[indexL++];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(order), order, null);
                }
            }

            //Fase 4 - Copy remaining Items
            while (indexL < left.Length) result[i++] = left[indexL++];
            while (indexR < right.Length) result[i++] = right[indexR++];

            return result;
        }

        private enum Order
        {
            Ascending,
            Descending
        }
    }
}