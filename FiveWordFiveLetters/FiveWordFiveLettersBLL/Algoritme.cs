namespace FiveWordFiveLettersBLL
{
    public class Algoritme
    {
        public static List<string> NotOptimised(string[] words, int wordLength = 5)
        {
            List<string> combinaions = new();
            for (int i = 0; i < words.Length; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (string.Concat(words[i], words[j]).Distinct().Count() != wordLength * 2) continue;
                    for (int k = j + 1; k < words.Length; k++)
                    {
                        if (string.Concat(words[i], words[j], words[k]).Distinct().Count() != wordLength * 3) continue;
                        for (int l = k + 1; l < words.Length; l++)
                        {
                            if (string.Concat(words[i], words[j], words[k], words[l]).Distinct().Count() != wordLength * 4) continue;
                            for (int m = l + 1; m < words.Length; m++)
                            {
                                if (string.Concat(words[i], words[j], words[k], words[l], words[m]).Distinct().Count() != wordLength * 5) continue;
                                string combination = string.Format("{0} {1} {2} {3} {4}", words[i], words[j], words[k], words[l], words[m]);
                                //Console.WriteLine($"{combination}");
                                combinaions.Add(combination);
                            }
                        }
                    }
                }
            }
            return combinaions;
        }

        public static List<string> Binary(string[] words, int wordLength = 5)
        {
            List<string> combinations = new();
            int wordCount = words.Length;

            Dictionary<int, string> dirWords = new();
            int[] bits = new int[wordCount];

            for (int i = 0; wordCount < i; i++)
            {
                var bit = 0;
                foreach (var ch in words[i])
                {
                    bit |= 1 << (ch - 'a');
                }
                dirWords.TryAdd(bit, words[i]);
            };

            int combinationBit2;
            int combinationBit3;
            int combinationBit4;


            for (int i = 0; i < wordCount - 4; i++)
            {
                for (int j = i + 1; j < wordCount - 3; j++)
                {
                    if ((bits[i] & bits[j]) != 0) continue;
                    combinationBit2 = bits[i];
                    combinationBit2 |= bits[j];
                    for (int k = j + 1; k < wordCount - 2; k++)
                    {
                        if ((combinationBit2 & bits[k]) != 0) continue;
                        combinationBit3 = combinationBit2;
                        combinationBit3 |= bits[k];
                        for (int l = k + 1; l < wordCount - 1; l++)
                        {
                            if ((combinationBit3 & bits[l]) != 0) continue;
                            combinationBit4 = combinationBit3;
                            combinationBit4 |= bits[l];
                            for (int m = l + 1; m < wordCount; m++)
                            {
                                //int combinationBit = bits[i] & bits[j] & bits[k] & bits[l] & bits[m];
                                if ((combinationBit4 & bits[m]) == 0)
                                {
                                    combinationBit4 |= bits[m];
                                    string combination = string.Format("{0} {1} {2} {3} {4}", dirWords[bits[i]], dirWords[bits[j]], dirWords[bits[k]], dirWords[bits[l]], dirWords[bits[m]]);
                                    combinations.Add(combination);
                                }
                            }
                        }
                    }
                }
            }
            return combinations;
        }
        public void Binaryv2(ref int[] bitwords, int startIndex, int usedBits, int[] combination, ref List<string> combinations)
        {
            if (combination.Length == 5)
            {
                combinations.Add(string.Format("{0} {1} {2} {3} {4}", combination[0], combination[1], combination[2], combination[3], combination[4]));
                return;
            }

            for (int i = startIndex; i < bitwords.Length; i++)
            {
                if ((usedBits & bitwords[i]) == 0)
                {
                    int[] newCombination = new int[combination.Length + 1];
                    combination.CopyTo(newCombination, 0);
                    newCombination[combination.Length] = bitwords[i];
                    Binaryv2(ref bitwords, i + 1, usedBits | bitwords[i], newCombination, ref combinations);
                }
            }

        }

        public static int MultiTheardBinary(Dictionary<int, string> wordList)
        {
            int[] bitwords = new int[wordList.Count];
            wordList.Keys.CopyTo(bitwords, 0);

            List<string> combinations = new();

            // Multi Thread
            var op = new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            };
            var t = Parallel.For(0, bitwords.Length, i =>
            {
                FirstMultiTheardBinary(i, bitwords, 0, new int[1], combinations);
            });

            return combinations.Count;


            void FirstMultiTheardBinary(int startIndex, int[] words, int usedBits, int[] combination, List<string> combinations)
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
                        MultiTheardBinary(newWords, usedBits | words[i], newCombination, combinations);
                    }
                }
                return;
            }

            void MultiTheardBinary(int[] words, int usedBits, int[] combination, List<string> combinations)
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
                        combination.CopyTo(newCombination, 0);
                        newCombination[combination.Length] = bitwords[i];
                        int[] newWords = words.Skip(i + 1).ToArray();
                        newWords = newWords.Where(x => (x & usedBits) == 0).ToArray();
                        MultiTheardBinary(newWords, usedBits | words[i], newCombination, combinations);
                    }
                }
                return;
            }
        }
    }
}
