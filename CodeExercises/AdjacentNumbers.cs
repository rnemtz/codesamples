using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    public class AdjacentNumbers
    {
        private static Dictionary<KeyValuePair<int, int>, int> _visitedCdn = new Dictionary<KeyValuePair<int, int>, int>();

        public bool Exist(char[,] board, string word)
        {
            if (board == null || string.IsNullOrEmpty(word)) return false;
            for (var r = 0; r < board.GetLength(0); r++)
            {
                for (var c = 0; c < board.GetLength(1); c++)
                {
                    _visitedCdn = new Dictionary<KeyValuePair<int, int>, int>();
                    if (!FindWord(board, r, c, word, 0)) continue;

                    foreach (var item in _visitedCdn)
                    {
                        Console.Write($"{board[item.Key.Key, item.Key.Value]} [{item.Key.Key},{item.Key.Value}] ->");
                    }
                    return true;
                }
            }
            return false;
        }

        private bool FindWord(char[,] board, int r, int c, string word, int i)
        {
            var list = GetAdjacents(board, r, c);
            if (!list.Any()) return false;
            if (!ExistInAdjacents(board, list, word[i], out var cdn)) return false;
            _visitedCdn.Add(cdn, 0);
            i++;
            return i == word.Length-1 || FindWord(board, cdn.Key, cdn.Value, word, i);
        }

        private bool ExistInAdjacents(char[,] board, IEnumerable<KeyValuePair<int, int>> list, char c, out KeyValuePair<int, int> cdn)
        {
            cdn = new KeyValuePair<int, int>();
            foreach (var item in list)
            {
                if (_visitedCdn.ContainsKey(item) || board[item.Key, item.Value] != c) continue;
                cdn = item;
                return true;
            }
            return false;
        }

        private static List<KeyValuePair<int, int>> GetAdjacents(char[,] board, int r, int c)
        {
            var list = new List<KeyValuePair<int, int>>();
            //return 4 top
            // 0*0
            // 0*-1
            // 0*+1
            // +1*0
            // -1*0



            return list;
        }
    }
}
