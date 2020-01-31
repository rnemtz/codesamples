using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.Internal
{
    public class Exercises
    {

        public Exercises()
        {

        }

        public IList<string> LetterCombinations(string digits)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(digits))
            {
                return result;
            }

            BuildLetterCombinations(digits, 0, string.Empty, result);
            return result;
        }

        private readonly Dictionary<char, string> numbers = new Dictionary<char, string>() { { '0', " " }, { '1', "*" }, { '2', "abc" }, { '3', "def" }, { '4', "ghi" }, { '5', "jkl" }, { '6', "mno" }, { '7', "pqrs" }, { '8', "tuv" }, { '9', "wxyz" } };


        private void BuildLetterCombinations(string digits, int index, string current, List<string> result)
        {

            if (index == digits.Length)
            {
                result.Add(current);
            }
            else
            {
                var cStr = numbers[digits[index]];
                foreach (var c in cStr)
                {
                    current += c;
                    BuildLetterCombinations(digits, index + 1, current, result);
                    current = current.Substring(0, current.Length - 1);
                }
            }
        }

    }
}
