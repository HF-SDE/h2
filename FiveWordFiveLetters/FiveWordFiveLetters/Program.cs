using FiveWordFiveLettersBLL;

// Define
string[] perfectData = GetWords.GetWord("PerfectData.txt");

// Test Collections
//RunTest.Run(perfectData);

//Console.WriteLine($"[Binary] Amount of perfect data: {Algoritme.Binary(perfectData).Count}");
//Console.WriteLine($"[Word] Amount of perfect data: {Algoritme.NotOptimised(perfectData).Count}");
//string[] notPerfectData = GetWords.GetWord("NotPerfectData.txt");
//Console.WriteLine($"Amount of not perfect data: {Algoritme.NotOptimised(notPerfectData).Count}");
string[] beta = GetWords.GetWord("new_beta.txt");
Console.WriteLine($"[Binary] Amount of beta data: {Algoritme.Binary(beta).Count}");
Console.WriteLine($"[Word] Amount of beta data: {Algoritme.NotOptimised(beta).Count}");
//string[] alpha = GetWords.GetWord("words_alpha.txt");
//Console.WriteLine($"[Binary] Amount of beta data: {Algoritme.Binary(alpha).Count}");
//Console.WriteLine($"[Word] Amount of beta data: {Algoritme.NotOptimised(alpha).Count}");
