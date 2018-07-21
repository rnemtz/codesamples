using System;

namespace CodeExercises
{
    public class BinaryGap
    {
        public static int Test(int n)
        {
            var maxGap = 0;
            var currentGap = 0;
            var isBinaryGap = false;
            var binary = Convert.ToString(n, 2);
            foreach (var c in binary)
            {
                if (!isBinaryGap && c == '0') continue;
                if (c == '1')
                {
                    if (isBinaryGap && currentGap > 0)
                    {
                        if (currentGap > maxGap) maxGap = currentGap;
                    }
                    isBinaryGap = true;
                    currentGap = 0;
                }
                else if (isBinaryGap) currentGap++;
            }
            return maxGap;
        }
    }
}
