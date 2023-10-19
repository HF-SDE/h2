using FiveWordFiveLetters;

string[] perfectData = GetWords.GetWord("PerfectData.txt");
Console.WriteLine($"Amount of perfect data: {Algoritme.NotOptimised(perfectData).Count}");
string[] notPerfectData = GetWords.GetWord("NotPerfectData.txt");
Console.WriteLine($"Amount of not perfect data: {Algoritme.NotOptimised(notPerfectData).Count}");
string[] beta = GetWords.GetWord("new_beta.txt");
Console.WriteLine($"Amount of beta data: {Algoritme.NotOptimised(beta).Count}");