using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.Trees
{
    public class PrefixTree
    {
        public PrefixNode Root { get; set; }
        public int WordCount { get; set; }
        public int PrefixNodeCount { get; set; }

        public PrefixTree()
        {
            Root = new PrefixNode(null);
            WordCount = 0;
            PrefixNodeCount = 0;
        }

        public void Add(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return;
            word = word.Trim().ToLower();
            Add(Root, word.ToCharArray(), 0);
            WordCount++;
        }

        private void Add(PrefixNode node, char[] word, int index)
        {
            if (index == word.Length)
            {
                if (!node.Children.ContainsKey('*'))
                    node.Children.Add('*', new PrefixNode(node));
                return;
            }
            if (!node.Children.ContainsKey(word[index]))
            {
                node.Children.Add(word[index], new PrefixNode(node));
                PrefixNodeCount++;
            }

            node = node.Children[word[index]];
            index++;
            Add(node, word, index);
        }

        public bool Delete(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) throw new ArgumentException("The word cannot be empty or null");
            word = word.Trim().ToLower();
            if (!Contains(word)) return false;
            //Delete
            var node = Root;
            var index = 0;
            while (index < word.Length)
            {
                node = node.Children[word[index]];
                index++;
            }

            //remove nodes that are not part of another word.
            // r <-e <- n <- e <- *

            // r <-e <- n <- e <- c <- i <- o <- *
            // r <-e <- n <- e <- g <- i <- d <- o <- *
            while (node.Children.Count > 1 && !node.Children.ContainsKey('*'))
            {
                node = node.Parent;
                index--;
            }
            if (node.Children.Count > 1) node.Children.Remove('*');
            else
            {
                while (node.Children.Count == 1)
                {
                    node = node.Parent;
                    index--;
                }
                node.Children.Remove(index > 0 ? '*' : word[index]);
            }
            return true;
        }

       

        public bool Contains(string word)
        {
            var node = Root;
            var index = 0;
            while (index <= word.Length)
            {
                if (index == word.Length) return node.Children.ContainsKey('*');
                if (!node.Children.ContainsKey(word[index])) continue;
                node = node.Children[word[index]];
                index++;
            }
            return false;
        }

        public IEnumerable<string> GetPredictiveWords(string prefix)
        {
            return new List<string>();
        }


        public class PrefixNode
        {
            public PrefixNode Parent { get; set; }
            public Dictionary<char, PrefixNode> Children { get; set; }

            public PrefixNode(PrefixNode parent)
            {
                Parent = parent;
                Children = new Dictionary<char, PrefixNode>();
            }
        }
    }
}