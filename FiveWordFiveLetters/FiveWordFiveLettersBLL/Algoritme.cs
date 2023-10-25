using System;
using System.Linq;

namespace FiveWordFiveLettersBLL
{
    public class Algoritme
    {
        int _progress = 0;

        public List<string> MultiTheardBinary(Dictionary<int, string> wordList, int combinationLength = 5)
        {
            int[] bitwords = new int[wordList.Count];
            wordList.Keys.CopyTo(bitwords, 0);

            List<string> combinations = new();
            List<int> usedWords = new();

            // Multi Thread
            var t = Parallel.For(0, bitwords.Length, i =>
            {
                FirstMultiTheardBinary(i, bitwords, 0, new int[1]);
            });

            return combinations;

            /// <summary>
            /// Recursively generates binary combinations of words using a specified starting index.
            /// </summary>
            /// <param name="startIndex">The starting index for word combinations.</param>
            /// <param name="words">An array of integers representing words as bit flags.</param>
            /// <param name="usedBits">A bit flag representing the combination of used words.</param>
            /// <param name="combination">An array to store the current combination of word indices.</param>
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

            /// <summary>
            /// Recursively generates binary combinations of words based on bit flags.
            /// </summary>
            /// <param name="words">An array of integers representing words as bit flags.</param>
            /// <param name="usedBits">A bit flag representing the combination of used words.</param>
            /// <param name="combination">An array to store the current combination of word indices.</param>
            void MultiTheardBinary(int[] words, int usedBits, int[] combination)
            {
                if (combination.Length == combinationLength)
                {
                    string result = "";
                    OnPropertyChanged(_progress + 1);
                    for (int i = 0; i < combinationLength; i++)
                    {
                        result += $"{wordList[combination[i]]}";
                    }
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
