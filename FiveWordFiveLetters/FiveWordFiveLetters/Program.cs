using FiveWordFiveLettersBLL;

var stopWatch = new System.Diagnostics.Stopwatch();
stopWatch.Start();
// run self call function
//List<string> combinations = new();
//Dictionary<int, string> beta = GetWords.GetWordBinary("new_beta.txt");
Dictionary<int, string> alpha = GetWords.GetWordBinary("words_alpha.txt");

int[] bitwords = new int[alpha.Count];
alpha.Keys.CopyTo(bitwords, 0);
MultiThreadBinaryParallel();

stopWatch.Stop();
Console.WriteLine(stopWatch.Elapsed.ToString());

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
        Console.WriteLine($"{alpha[combination[0]]} {alpha[combination[1]]} {alpha[combination[2]]} {alpha[combination[3]]} {alpha[combination[4]]}");
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

void MultiThreadBinaryParallel()
{
    List<string> combinations = new();
    //var tasks = new List<Task>();
    var op = new ParallelOptions
    {
        MaxDegreeOfParallelism = 1
    };
    var t = Parallel.For(0, bitwords.Length, i =>
    {
        FirstMultiTheardBinary(i, bitwords, 0, new int[1], combinations);
    });

    Console.WriteLine($"[Binary] Amount of beta data: {combinations.Count}");
}
