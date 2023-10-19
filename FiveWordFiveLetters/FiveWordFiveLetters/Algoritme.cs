namespace FiveWordFiveLetters
{
    internal class Algoritme
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
                                Console.WriteLine($"{combination}");
                                combinaions.Add(combination);
                            }
                        }
                    }
                }
            }
            return combinaions;
        }
    }
}
