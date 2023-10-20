using FiveWordFiveLettersBLL;

// Define
//string[] perfectData = GetWords.GetWord("PerfectData.txt");

// Test Collections
//RunTest.Run(perfectData);

//Console.WriteLine($"[Binary] Amount of perfect data: {Algoritme.Binary(perfectData).Count}");
//Console.WriteLine($"[Word] Amount of perfect data: {Algoritme.NotOptimised(perfectData).Count}");
//string[] notPerfectData = GetWords.GetWord("NotPerfectData.txt");
//Console.WriteLine($"Amount of not perfect data: {Algoritme.NotOptimised(notPerfectData).Count}");
//string[] beta = GetWords.GetWord("new_beta.txt");
//Console.WriteLine($"[Binary] Amount of beta data: {Algoritme.Binary(beta).Count}");
//Console.WriteLine($"[Word] Amount of beta data: {Algoritme.NotOptimised(beta).Count}");
//string[] alpha = GetWords.GetWord("words_alpha.txt");
//Console.WriteLine($"[Binary] Amount of beta data: {Algoritme.Binary(alpha).Count}");
//Console.WriteLine($"[Word] Amount of beta data: {Algoritme.NotOptimised(alpha).Count}");

var stopWatch = new System.Diagnostics.Stopwatch();
stopWatch.Start();
// run self call function
List<string> combinations = new();
//Dictionary<int, string> beta = GetWords.GetWordBinary("new_beta.txt");
Dictionary<int, string> alpha = GetWords.GetWordBinary("words_alpha.txt");

int[] bitwords = new int[alpha.Count];
alpha.Keys.CopyTo(bitwords, 0);
await MultiTheardBinaryv2(0, 0, Array.Empty<int>());
Console.WriteLine($"[Binary] Amount of beta data: {combinations.Count}");
stopWatch.Stop();
Console.WriteLine(stopWatch.Elapsed.ToString());
//Console.WriteLine($"[Binary] Amount of beta data: {combinations.Count}");

//Console.WriteLine($"[Word] Amount of beta data: {Algoritme.Binaryv2(0, 0, beta)}");

static void Binaryv2(int startIndex, int usedBits, int[] combination, List<string> combinations, int[] bitwords)
{
    if (combination.Length == 5)
    {
        combinations.Add($"{combination[0]} {combination[1]} {combination[2]} {combination[3]} {combination[4]}");
        Console.WriteLine($"{combination[0]} {combination[1]} {combination[2]} {combination[3]} {combination[4]}");
        return;
    }

    for (int i = startIndex; i < bitwords.Length; i++)
    {
        if ((usedBits & bitwords[i]) == 0)
        {
            int[] newCombination = new int[combination.Length + 1];
            combination.CopyTo(newCombination, 0);
            newCombination[combination.Length] = bitwords[i];
            Binaryv2(i + 1, usedBits | bitwords[i], newCombination, combinations, bitwords);
        }
    }
}

async Task MultiTheardBinaryv2(int startIndex, int usedBits, int[] combination)
{
    if (combination.Length == 5)
    {
        string result = $"{combination[0]} {combination[1]} {combination[2]} {combination[3]} {combination[4]}";
        await Task.Run(() =>
        {
            lock (combinations)
            {
                combinations.Add(result);
                Console.WriteLine(result);
            }
        });
        return;
    }

    var tasks = new List<Task>();
    //var options = new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism };
    //Parallel.For(startIndex, bitwords.Length, options, i =>
    //{
    //    if ((usedBits & bitwords[i]) == 0)
    //    {
    //        int[] newCombination = new int[combination.Length + 1];
    //        combination.CopyTo(newCombination, 0);
    //        newCombination[combination.Length] = bitwords[i];
    //        tasks.Add(MultiTheardBinaryv2(i + 1, usedBits | bitwords[i], newCombination, degreeOfParallelism));
    //    }
    //});
    for (int i = startIndex; i < bitwords.Length; i++)
    {
        if ((usedBits & bitwords[i]) == 0)
        {
            int[] newCombination = new int[combination.Length + 1];
            combination.CopyTo(newCombination, 0);
            newCombination[combination.Length] = bitwords[i];
            tasks.Add(MultiTheardBinaryv2(i + 1, usedBits | bitwords[i], newCombination));
        }
    }
    await Task.WhenAll(tasks);
}
