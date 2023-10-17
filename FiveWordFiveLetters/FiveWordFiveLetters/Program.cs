using FiveWordFiveLetters;
using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;

GetWords getWords = new GetWords();

List<string> alpha = getWords.Words("words_alpha.txt");
List<string> alphaFiltered = new List<string>(alpha.FindAll(word => word.Length == 5));

List<string> testData = getWords.Words("new_beta.txt");
List<string> testDataFiltered = new List<string>(testData.FindAll(word => word.Length == 5).Distinct());



Console.WriteLine("Alpha words amount: {0}", alpha.Count);
Console.WriteLine("Filtered alpha words amount: {0}", alphaFiltered.Count);
Console.WriteLine("Test data amount: {0}", testData.Count);
Console.WriteLine("Filtered test data amount: {0}", testDataFiltered.Count);

List<List<string>> finalWords = new();
int finalCombinations = finalWords.Count;

List<char> letterInChunkMustNotContain = new();
List<string> wordsChunk = new();
List<string> usedWords = new();
foreach (string item in alphaFiltered)
{
    foreach (string word in alphaFiltered.Skip(usedWords.Count))
    {
        if (!usedWords.Contains(word))
        {
            //if (!word.StartsWith("a"))
            //{
            //    Console.WriteLine(word);

            //}
            if (wordsChunk.Count() == 5)
            {
                finalWords.Add(wordsChunk.ToList());
                usedWords.Add(wordsChunk[0].ToString());
                wordsChunk.Clear();
                letterInChunkMustNotContain.Clear();
                break;
            }
            int counter1 = 0;
            foreach (char letter in word)
            {
                //Console.WriteLine(!letterInChunkMustNotContain.Contains(letter));
                if (!letterInChunkMustNotContain.Contains(letter))
                {
                    if (counter1 == word.Length - 1)
                    {
                        wordsChunk.Add(word);
                        foreach (var letter1 in word)
                        {
                            letterInChunkMustNotContain.Add(letter1);
                        }
                    }
                }

                counter1++;
            }
        }

    }
}

foreach (List<string> words in finalWords)
{
    string result = string.Empty;

    foreach (var word in words)
    {
        result += " " + word;
    }
    Console.WriteLine(result);
}
Console.WriteLine(finalWords.Count);