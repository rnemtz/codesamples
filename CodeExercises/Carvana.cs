namespace CodeExercises
{
    public class Carvana
    {
        #region CARVANA

        /*
         * Carvana Online Exam (HackerRank)
         */
        private static string Rev(string a)
        {
            //var str = string.Empty;
            //for (var i = a.Length - 1; i >= 0; i--)
            //{
            //    str += a[i].ToString();
            //}
            //return str;
            //1. Array
            //var str = new char[a.Length];
            //for (var i = 0; i < a.Length; i++)
            //{
            //    str[i] = a[a.Length - 1- i];
            //}
            //return string.Join(string.Empty, str);

            //var stk = new Stack<string>();
            //foreach (char t in a)
            //{
            //    stk.Push(t.ToString());
            //}
            //var result = string.Empty;
            //while (stk.Count > 0)
            //{
            //    result += stk.Pop();
            //}
            //return result;

            var index = a.Length - 1;
            var array = a.ToCharArray();
            for (var i = 0; i < a.Length; i++)
            {
                //swap in same string
                //implementing a temporary variable
                //we can use a different method say, 
                // private method just for swap, but this
                // is simply enough to demonstrate it.
                if (index <= i)
                {
                    break;
                }

                var temp = array[i];
                array[i] = array[index];
                array[index] = temp;
                index--;
            }

            return string.Join(string.Empty, array);
        }

        #endregion
    }


}
