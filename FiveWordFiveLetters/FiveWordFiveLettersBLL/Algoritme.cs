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
            ThreadLocal<int[]> bits = new (() => new int[wordCount]);

            Parallel.For(0, wordCount + 1, i =>
            {
                var bit = 0;
                foreach (var ch in words[i])
                {
                    bit |= 1 << (ch - 'a');
                }
                dirWords.TryAdd(bit, words[i]);
                //Console.WriteLine("Key: "+bit+ " Value: " + words[i]);
                int[] localArray = bits.Value;
                localArray[i] = bit;
            });

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
    }
}
