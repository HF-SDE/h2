using FiveWordFiveLettersBLL;

var stopWatch = new System.Diagnostics.Stopwatch();
stopWatch.Start();
Dictionary<int, string> alpha = GetWords.GetWordBinary("words_alpha.txt");

Console.WriteLine($"[Binary] Amount of beta data: {Algoritme.MultiTheardBinary(alpha)}");
stopWatch.Stop();
Console.WriteLine($"Timed runned: {stopWatch.Elapsed}");
