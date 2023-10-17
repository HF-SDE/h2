using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordFiveLetters
{
    internal class @string
    {
        public void value() {

            GetWords getWords = new GetWords();

            List<string> alpha = getWords.Words("words_alpha.txt");
            List<string> alphaFiltered = new List<string>(alpha.FindAll(word => word.Length == 5));

            List<string> testData = getWords.Words("words_alpha.txt");
            List<string> testDataFiltered = new List<string>(testData.FindAll(word => word.Length == 5).Distinct());



            Console.WriteLine("Alpha words amount: {0}", alpha.Count);
            Console.WriteLine("Filtered alpha words amount: {0}", alphaFiltered.Count);
            Console.WriteLine("Test data amount: {0}", testData.Count);
            Console.WriteLine("Filtered test data amount: {0}", testDataFiltered.Count);

            List<List<string>> finalWords = new List<List<string>>();
            int finalCombinations = finalWords.Count;

            int resultCounter = 0;
            List<char> letterInChunkMustNotContain = new();
            List<string> wordsChunk = new List<string>();
            List<string> usedWords = new List<string>();
            bool skibWord = false;
            int counter = 0;
            while (true)
            {
                if (testDataFiltered.Count * testDataFiltered.Count == counter)
                {
                    break;
                }
                foreach (string word in testDataFiltered.Skip(usedWords.Count))
                {
                    if (word.First() == 'b')
                    {
                        continue;
                    }
                    if (usedWords.Contains(word))
                    {
                        break;
                    }
                    skibWord = false;
                    if (wordsChunk.Count() == 5)
                    {
                        finalWords.Add(wordsChunk.ToList());
                        usedWords.Add(wordsChunk[0]);
                        wordsChunk.Clear();
                        letterInChunkMustNotContain.Clear();
                        break;
                    }
                    int counter1 = 0;
                    foreach (var letter in word)
                    {
                        if (letterInChunkMustNotContain.Contains(letter))
                        {
                            skibWord = true;
                            break;
                        }
                        if (counter1 == word.Length - 1)
                        {
                            resultCounter++;
                            wordsChunk.Add(word);
                            foreach (var letter1 in word)
                            {
                                letterInChunkMustNotContain.Add(letter1);
                            }
                        }
                        counter1++;
                    }
                    if (skibWord)
                    {
                        break;
                    }
                }
                counter++;
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
        
        }
    }
}
