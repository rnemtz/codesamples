using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    public class Amazon
    {
        #region AMAZON

        public static List<List<int>> OptimalUtilization(int deviceCapacity,
            List<List<int>> foregroundAppList,
            List<List<int>> backgroundAppList)
        {

            var dict = new Dictionary<List<int>, double>();
            foreach (var fa in foregroundAppList)
            { //O(f*b) <--
                foreach (var ba in backgroundAppList)
                {
                    if (fa[1] + ba[1] > deviceCapacity) continue;
                    dict.Add(new List<int> { fa[0], ba[0] }, fa[1] + ba[1]);
                }
            }

            var result = new List<List<int>>();
            if (dict.Count == 0) return result;

            double lastValue = -1;
            foreach (var item in dict.OrderByDescending(x => x.Value))
            {
                if (lastValue > 0 && item.Value < lastValue) break;
                result.Add(item.Key);
                lastValue = item.Value;
            }

            return result;
        }


        public static List<List<int>> NearestXsteakHouses(int totalSteakhouses,
            int[,] allLocations,
            int numSteakhouses)
        {

            if (allLocations == null
                || allLocations.Length == 0
                || numSteakhouses > totalSteakhouses)
                return new List<List<int>>();

            var map = new Dictionary<KeyValuePair<int, int>, double>();
            for (var i = 0; i < totalSteakhouses; i++)
            { //O(N)
                var locationX = allLocations[i, 0];
                var locationY = allLocations[i, 1];
                var distance = Math.Sqrt((locationX * locationX)
                                         + (locationY * locationY));
                var kv = new KeyValuePair<int, int>(locationX, locationY);
                if (map.ContainsKey(kv)) continue;
                map.Add(kv, distance);
            }

            var closestSteakHouses = map.OrderBy(x => x.Value).Take(numSteakhouses); //O(N log N) <- Bottleneck
            return closestSteakHouses.Select(x => new List<int> { x.Key.Key, x.Key.Value }).ToList();
        }


        /*
         * Amazon AWS
         */

        /* Need to do math with really big integers. Say `LargeInt`.
         *  For now, we need to add two really large numbers.
         *  Say 193,707,721 + 76,187,287 = 269,895,008
         *  I want you to build this in a way that other people can use it. e.g. a library.
         */

        public static string Sum(string a, string b)
        {
            var result = string.Empty;
            var aLength = a.Length - 1;
            var bLength = b.Length - 1;
            var carry = 0;
            var maxIndex = Math.Max(aLength, bLength);

            for (var i = 0; i <= maxIndex; i++)
            {
                var left = aLength >= 0 ? int.Parse(a[aLength].ToString()) : 0;
                var right = bLength >= 0 ? int.Parse(b[bLength].ToString()) : 0;
                var sum = left + right + carry;

                carry = sum >= 10 ? 1 : 0;

                result += sum.ToString()[sum.ToString().Length - 1].ToString();

                bLength--;
                aLength--;
            }
            return string.Join(string.Empty, result.Reverse());
        }

        /*
        * Amazon 
        * 
        * Given an array of numbers in sorted order
        * count the pairs of numbers whose sum is less than X
        */

        public static int GetCountOfPairs(int[] numbers, int n)
        {
            var count = 0;
            var firstIndex = 0;

            var lastIndex = numbers.Length - 1;
            while (firstIndex != lastIndex)
                if (numbers[firstIndex] + numbers[lastIndex] < n)
                {
                    count += lastIndex - firstIndex;
                    firstIndex++;
                }
                else
                {
                    lastIndex--;
                }
            return count;
        }

        /*
         * AMAZON Luxembourg Question
         */

        /*
         var literatureText ="Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat avoids a pain that produces no resultant pleasure? quo voluptas nulla pariatur? Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet.., comes from a line in section 1.10.32. There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc. It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
        var wordsToExclude = new[] {"and", "he", "the", "to", "is"};
        var startDate = DateTime.Now;
        var res = GetFrequentWords(literatureText, wordsToExclude);
        var resultTimeStamp = DateTime.Now - startDate;
        Console.WriteLine($"(Total Time in O(N**2): {resultTimeStamp}");
        Console.WriteLine();
        startDate = DateTime.Now;
        res = GetFrequentWordsFaster(literatureText, wordsToExclude);
        resultTimeStamp = DateTime.Now - startDate;
        Console.WriteLine($"(Total Time in O(N): {resultTimeStamp}");
        Console.ReadKey();
        */

        /*
         * Optimized Solution
         */

        public static string[] GetFrequentWordsFaster(string literatureText, string[] wordsToExclude)
        {
            if (string.IsNullOrWhiteSpace(literatureText)) return new string[0];
            literatureText = literatureText.ToLower(); //O(1)
            var result = new List<string>();

            //Create a Dictionary with the allowed alphabetic characters
            var allowedCharacters = new Dictionary<char, int>
            {
                {
                    'a', 0
                },
                {'b', 0},
                {'c', 0},
                {'d', 0},
                {'e', 0},
                {'f', 0},
                {'g', 0},
                {'h', 0},
                {'i', 0},
                {'j', 0},
                {'k', 0},
                {'l', 0},
                {'m', 0},
                {'n', 0},
                {'o', 0},
                {'p', 0},
                {'q', 0},
                {'r', 0},
                {'s', 0},
                {'t', 0},
                {'u', 0},
                {'v', 0},
                {'x', 0},
                {'w', 0},
                {'y', 0},
                {'z', 0},
                {'A', 0},
                {'B', 0},
                {'C', 0},
                {'D', 0},
                {'E', 0},
                {'F', 0},
                {'G', 0},
                {'H', 0},
                {'I', 0},
                {'J', 0},
                {'K', 0},
                {'L', 0},
                {'M', 0},
                {'N', 0},
                {'O', 0},
                {'P', 0},
                {'Q', 0},
                {'R', 0},
                {'S', 0},
                {'T', 0},
                {'U', 0},
                {'V', 0},
                {'X', 0},
                {'W', 0},
                {'Y', 0},
                {'Z', 0}
            };
            var lText = literatureText.ToArray(); //O(N)
            for (var c = 0; c < lText.Length; c++) //O(N)
                if (!allowedCharacters.ContainsKey(lText[c])) lText[c] = ' '; //O(1) 
            var splitArray = string.Join(string.Empty, lText) //O(N**2) it seems <-- Bottleneck
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var fqwords = new Dictionary<string, int>();
            var maxFrequency = 1;
            var wordsToExcludeDict = new Dictionary<string, int>();
            foreach (var word in wordsToExclude) //O(N)   
                wordsToExcludeDict.Add(word, 0);
            foreach (var word in splitArray) //O(N)
            {
                if (wordsToExcludeDict.ContainsKey(word)) continue; //O(1) 
                if (fqwords.ContainsKey(word)) //O(1)
                {
                    fqwords[word] = fqwords[word] + 1;
                    if (fqwords[word] > maxFrequency) maxFrequency = fqwords[word];
                }
                else
                {
                    fqwords.Add(word, 1);
                }
            }
            foreach (var word in fqwords) //O(N)
                if (word.Value == maxFrequency) result.Add(word.Key);
            return result.ToArray(); //O(N)
        }

        /*
         * Complexity O(N**2) due the Array.Contains inside a loop
         */
        public static string[] GetFrequentWords(string literatureText, string[] wordsToExclude)
        {
            var result = new List<string>();

            //check nulls,
            if (string.IsNullOrWhiteSpace(literatureText)) return new string[0];

            //Create a Dictionary with the allowed alphabetic characters
            var allowedCharacters = new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            var lText = literatureText.ToArray(); //O(N)
            for (var c = 0; c < lText.Length; c++) //O(N)
                if (!allowedCharacters.Contains(lText[c])) lText[c] = ' '; //O(1) since is a fixed array
            //Remove words to Exclude
            var splitArray = string.Join(string.Empty, lText) //O(N**2) ??
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var fqwords = new Dictionary<string, int>();
            var maxFrequency = 1;
            foreach (var word in splitArray) //O(N)
            {
                if (wordsToExclude.Contains(word)) continue; //O(N) <-- O(N**2) 
                /*
                 * Solution to this bottle neck, 
                 * 1. is to remove the words using Booyer-Moore algorithm
                 * 2. implement it using a Trie/Prefix Tree
                 * 3. Insert words to a dictionary to get a ContainsKey => O(1)
                 */
                var w = word.ToLower();
                if (fqwords.ContainsKey(w)) //O(1)
                {
                    fqwords[w] = fqwords[w] + 1;
                    if (fqwords[w] > maxFrequency) maxFrequency = fqwords[w];
                }
                else
                {
                    fqwords.Add(w, 1);
                }
            }
            foreach (var word in fqwords) //O(N)
                if (word.Value == maxFrequency) result.Add(word.Key);

            //iterate through dictionary and get maxfrequency
            //get keys and insert into result array.
            return result.ToArray(); //O(N)
        }

        #endregion
    }
}
