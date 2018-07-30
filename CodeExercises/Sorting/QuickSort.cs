namespace CodeExercises.Sorting
{
    public class QuickSort
    {

        public static int[] Execute(int[] array)
        {
            Sort(ref array, 0, array.Length - 1);
            return array;
        }

        private static void Sort(ref int[] array, int start, int end)
        {
            if (start >= end) return;
            var pIndex = Partition(ref array, start, end);
            Sort(ref array, start, pIndex - 1);
            Sort(ref array, pIndex + 1, end);
        }

        private static int Partition(ref int[] array,int start, int end)
        {
            var pivot = array[end];
            var pIndex = start; //Set partition index as start
            for (var i = start; i < end; i++)
            {
                if (array[i] > pivot) continue;
                Swap(ref array, i, pIndex); //Swap if element is lesser than pivot
                pIndex++;
            }
            Swap(ref array, end, pIndex); //Swap pivot with element at partition index
            return pIndex;
        }

        private static void Swap(ref int[] array, int source, int target)
        {
            var temp = array[source];
            array[source] = array[target];
            array[target] = temp;
        }
    }

    public class Implement
    {
        public static void Execute(ref int[] array, int start, int end)
        {
            if (start >= end) return;
            var pIndex = Partition(ref array, start, end);
            Execute(ref array, start, pIndex - 1);
            Execute(ref array, pIndex + 1, end);
        }

        public static int Partition(ref int[] array, int start, int end)
        {
            var pIndex = start;
            for (var i = start; i < end; i++)
            {
                if (array[i] >= array[end]) continue;
                Swap(ref array, i, pIndex++);
            }
            Swap(ref array, end, pIndex);
            return pIndex;
        }

        private static void Swap(ref int[] array, int source, int target)
        {
            var temp = array[source];
            array[source] = array[target];
            array[target] = temp;
        }
    }
}