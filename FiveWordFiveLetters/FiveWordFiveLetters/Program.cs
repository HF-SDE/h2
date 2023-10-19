using FiveWordFiveLetters;

string[] perfectData = GetWords.GetWord("PerfectData.txt");
Console.WriteLine($"Amount of perfect data: {Algoritme.NotOptimised(perfectData).Count}");
string[] notPerfectData = GetWords.GetWord("NotPerfectData.txt");
Console.WriteLine($"Amount of not perfect data: {Algoritme.NotOptimised(notPerfectData).Count}");
string[] beta = GetWords.GetWord("new_beta.txt");
Console.WriteLine($"Amount of beta data: {Algoritme.NotOptimised(beta).Count}");

//string[] alpha = GetWords.GetWord("words_alpha.txt");
//Console.WriteLine($"Amount of not perfect data: {Algoritme.NotOptimised(alpha).Count}");

//List<string> alpha = getWords.Words("words_alpha.txt");
//List<string> alphaFiltered = new List<string>(alpha.FindAll(word => word.Length == 5));
//List<string> alphaFiltered1 = RemoveWordsWithDuplicateLetters(alphaFiltered.ToList());


//List<string> testData = getWords.Words("new_beta.txt");
//List<string> testDataFiltered = new List<string>(testData.FindAll(word => word.Length == 5));
//List<string> testDataFiltered1 = new(RemoveWordsWithDuplicateLetters(testDataFiltered.ToList()));

//List<string> perfectData = getWords.Words("PerfectData.txt");
//List<string> perfectDataFiltered = new List<string>(perfectData.FindAll(word => word.Length == 5));
//List<string> perfectDataFiltered1 = new(RemoveWordsWithDuplicateLetters(perfectDataFiltered.ToList()));

//List<string> uPerfectData = getWords.Words("NotPerfectData.txt");
//List<string> uPerfectDataFiltered = new List<string>(perfectData.FindAll(word => word.Length == 5));
//List<string> uPerfectDataFiltered1 = new(RemoveWordsWithDuplicateLetters(perfectDataFiltered.ToList()));

//Console.WriteLine("Alpha words amount: {0}", alpha.Count);
//Console.WriteLine("Filtered alpha words amount: {0}", alphaFiltered.Count);
//Console.WriteLine("Filtered alpha words amount: {0}", alphaFiltered1.Count);
//Console.WriteLine("Test data amount: {0}", testData.Count);
//Console.WriteLine("Filtered test data amount: {0}", testDataFiltered1.Count);



//List<List<string>> finalWords = new();
//int finalCombinations = finalWords.Count;

//List<char> letterInChunkMustNotContain = new();
//List<string> wordsChunk = new();
//List<string> usedWords = new();
//int max = testDataFiltered1.Count;


//for (int i = 0; i < max; i++)
//{
//    foreach (string word in testDataFiltered1.Skip(i))
//    {
//        if (wordsChunk.Contains(word))
//        {
//            continue;
//        }
//        if (wordsChunk.Count() == 5)
//        {
//            finalWords.Add(wordsChunk.ToList());
//            string result = "";
//            foreach (var item in wordsChunk)
//            {
//                result = item + " ";
//            }
//            wordsChunk.Clear();
//            letterInChunkMustNotContain.Clear();
//            continue;
//        }
//        int counter1 = 0;
//        foreach (char letter in word)
//        {
//            if (letterInChunkMustNotContain.Contains(letter))
//            {
//                break;
//            }
//            if (counter1 == word.Length - 1)
//            {
//                wordsChunk.Add(word);
//                foreach (var letter1 in word)
//                {
//                    letterInChunkMustNotContain.Add(letter1);
//                }
//            }

//            counter1++;
//        }
//    }
//    testDataFiltered1.Add(testDataFiltered1[i]);
//    wordsChunk.Clear();
//    letterInChunkMustNotContain.Clear();
//}

//foreach (List<string> words in finalWords)
//{
//    string result = string.Empty;

//    foreach (var word in words)
//    {
//        result += " " + word;
//    }
//    Console.WriteLine(result);
//}
//Console.WriteLine(finalWords.Count);

//static bool HasDuplicateLetters(string word)
//{
//    // Convert the word to lowercase for case-insensitive comparison
//    word = word.ToLower();
//    for (int i = 0; i < word.Length; i++)
//    {
//        char currentChar = word[i];
//        for (int j = i + 1; j < word.Length; j++)
//        {
//            if (currentChar == word[j])
//            {
//                return true;
//            }
//        }
//    }
//    return false;
//}

//static List<string> RemoveWordsWithDuplicateLetters(List<string> wordList)
//{
//    wordList.RemoveAll(HasDuplicateLetters);
//    return wordList;
//}