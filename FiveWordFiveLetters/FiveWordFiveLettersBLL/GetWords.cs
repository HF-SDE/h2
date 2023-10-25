namespace FiveWordFiveLettersBLL
{
    public class GetWords
    {
        /// <summary>
        /// Reads a text file and retrieves words of a specific length with unique characters.
        /// </summary>
        /// <param name="file">The path to the text file to read.</param>
        /// <param name="wordLength">The desired length of the words to retrieve (default is 5).</param>
        /// <returns>
        /// An array of words from the file that meet the specified criteria.
        /// </returns>
        public static string[] GetWord(string file, int wordLength = 5)
        {
            List<string> words = new();
            using (var reader = new StreamReader(file))
            {

                var word = "";
                while ((word = reader.ReadLine()) != null)
                {
                    if (word.Length != wordLength) continue;
                    if (word.Distinct().Count() != wordLength) continue;
                    if (words.Where(x => string.Concat(x, word).Distinct().Count() == wordLength).Count() > 0) continue;
                    words.Add(word);
                }
                Console.WriteLine("Antal ord: {0}", words.Count);
            }
            return words.ToArray();
        }

        /// <summary>
        /// Reads a text file and retrieves words of a specific length with unique characters, representing them as binary values.
        /// </summary>
        /// <param name="file">The path to the text file to read.</param>
        /// <param name="wordLength">The desired length of the words to retrieve (default is 5).</param>
        /// <returns>
        /// A dictionary where keys are binary representations of words and values are the corresponding words.
        /// </returns>
        public static Dictionary<int, string> GetWordBinary(string file, int wordLength = 5)
        {
            Dictionary<int, string> words = new();
            using (var reader = new StreamReader(file))
            {

                var word = "";
                while ((word = reader.ReadLine()) != null)
                {
                    if (word.Length != wordLength) continue;
                    if (word.Distinct().Count() != wordLength) continue;
                    int bits = 0;
                    foreach(var ch in word)
                    {
                        bits |= 1 << ch - 'a';
                    }
                    if (words.ContainsKey(bits)) continue;
                    words.Add(bits, word);
                }
                Console.WriteLine("Antal ord: {0}", words.Count);
            }
            
            return words;
        }
    }
}
