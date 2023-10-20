namespace FiveWordFiveLettersBLL
{
    public class GetWords
    {
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
