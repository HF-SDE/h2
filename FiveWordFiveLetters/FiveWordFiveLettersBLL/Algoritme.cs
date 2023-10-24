namespace FiveWordFiveLettersBLL
{
    public class Algoritme
    {

        public static int MultiTheardBinary(Dictionary<int, string> wordList)
        {
            int[] bitwords = new int[wordList.Count];
            wordList.Keys.CopyTo(bitwords, 0);

            List<string> combinations = new();

            // Multi Thread
            var t = Parallel.For(0, bitwords.Length, i =>
            {
                FirstMultiTheardBinary(i, bitwords, 0, new int[1]);
            });

            return combinations.Count;


            void FirstMultiTheardBinary(int startIndex, int[] words, int usedBits, int[] combination)
            {
                usedBits = bitwords[startIndex];
                combination[0] = usedBits;

                for (int i = startIndex; i < words.Length; i++)
                {
                    if ((usedBits & words[i]) == 0)
                    {
                        int[] newCombination = new int[combination.Length + 1];
                        combination.CopyTo(newCombination, 0);
                        newCombination[combination.Length] = bitwords[i];
                        int[] newWords = words.Skip(i + 1).ToArray();
                        newWords = words.Where(x => (x & usedBits) == 0).ToArray();
                        MultiTheardBinary(newWords, usedBits | words[i], newCombination);
                    }
                    //if ((usedBits & words[i]) == 0)
                    //{
                    //    List<int> newCombination = new(combination)
                    //    {
                    //        bitwords[i]
                    //    };

                    //    int[] newWords = words.Skip(i + 1).Where(x => (x & usedBits) == 0).ToArray();

                    //    MultiTheardBinary(newWords, usedBits | words[i], newCombination.ToArray());
                    //}
                }
                return;
            }

            void MultiTheardBinary(int[] words, int usedBits, int[] combination)
            {
                if (combination.Length == 5)
                {
                    string result = $"{combination[0]} {combination[1]} {combination[2]} {combination[3]} {combination[4]}";
                    combinations.Add(result);
                    Console.WriteLine(result);
                    Console.WriteLine($"{wordList[combination[0]]} {wordList[combination[1]]} {wordList[combination[2]]} {wordList[combination[3]]} {wordList[combination[4]]}");
                    return;
                }

                for (int i = 0; i < words.Length; i++)
                {
                    if ((usedBits & words[i]) == 0)
                    {
                        int[] newCombination = new int[combination.Length + 1];
                        newCombination[combination.Length] = bitwords[i];
                        combination.CopyTo(newCombination, 0);
                        int[] newWords = words.Skip(i + 1).ToArray();
                        newWords = newWords.Where(x => (x & usedBits) == 0).ToArray();
                        MultiTheardBinary(newWords, usedBits | words[i], newCombination);
                    }
                    //if ((usedBits & words[i]) == 0)
                    //{
                    //    List<int> newCombination = new(combination)
                    //    {
                    //        bitwords[i]
                    //    };

                    //    int[] newWords = words.Skip(i + 1).Where(x => (x & usedBits) == 0).ToArray();

                    //    MultiTheardBinary(newWords, usedBits | words[i], newCombination.ToArray());
                    //}
                }
                return;
            }
        }
    }
}
