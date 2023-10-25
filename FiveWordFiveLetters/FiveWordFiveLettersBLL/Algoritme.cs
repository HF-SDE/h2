using System;
using System.Linq;

namespace FiveWordFiveLettersBLL
{
    public class Algoritme
    {
        int _progress = 0;

        public List<string> MultiTheardBinary(Dictionary<int, string> wordList)
        {

            Load load = new();
            int[] bitwords = new int[wordList.Count];
            wordList.Keys.CopyTo(bitwords, 0);

            List<string> combinations = new();
            List<int> used = new();

            // Multi Thread
            var t = Parallel.For(0, bitwords.Length, i =>
            {
                FirstMultiTheardBinary(i, bitwords, 0, new int[1]);
            });

            return combinations;


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

            void MultiTheardBinary(int[] words, int usedBits, int[] combination)
            {
                if (combination.Length == 5)
                {
                    OnPropertyChanged(combinations.Count == 537 ? combinations.Count  + 1 : combinations.Count);
                    string result = $"{wordList[combination[0]]} {wordList[combination[1]]} {wordList[combination[2]]} {wordList[combination[3]]} {wordList[combination[4]]}";
                    combinations.Add(result);
                    //Console.WriteLine(result);
                    //Console.WriteLine($"{wordList[combination[0]]} {wordList[combination[1]]} {wordList[combination[2]]} {wordList[combination[3]]} {wordList[combination[4]]}");
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
        public event EventHandler<int> Progress;
        protected virtual void OnPropertyChanged(int index)
        {
            Progress?.Invoke(this, index);
            _progress = index;
        }
    }
}
