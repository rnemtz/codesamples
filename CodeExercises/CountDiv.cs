using System;

namespace CodeExercises
{
    public class CountDiv
    {
        public static int Test(int a, int b, int k)
        {
            //O (n)
            if (a == b) return a % k == 0 ? 1 : 0;
            var count = 0;
            for (var c = a; c <= b; c++) count += c % k == 0 ? 1 : 0;
            //return count;

            //O (1)
            var numbers = b - a;
            var div = numbers / k;
            var result = Math.Floor((double)div);
            return (int)result;
        }
    }
}
