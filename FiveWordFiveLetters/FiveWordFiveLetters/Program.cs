using FiveWordFiveLettersBLL;

var stopWatch = new System.Diagnostics.Stopwatch();
stopWatch.Start();
Dictionary<int, string> alpha = GetWords.GetWordBinary("words_alpha.txt");
Algoritme algoritme = new();
Console.WriteLine($"[Binary] Amount of beta data: {algoritme.MultiTheardBinary(alpha).Count}");
stopWatch.Stop();
Console.WriteLine($"Timed runned: {stopWatch.Elapsed}");
