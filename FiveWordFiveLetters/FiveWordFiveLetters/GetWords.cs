namespace FiveWordFiveLetters
{
    public class GetWords
    {
        public static List<string> Words(string file)
        {
            try
            {
                String line;
                List<string> words = new();
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new(file);
                //Read the first line of text
                line = sr.ReadLine()!;
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    words.Add(line);
                    //Read the next line
                    line = sr.ReadLine()!;
                }
                //close the file
                sr.Close();
                return words;
            }
            catch
            {
                return new List<string>();
            };
        }
        public static string[] GetWord(string file, int wordLength = 5)
        {
            List<string> words = new();
            using (var reader = new StreamReader(file))
            {

                var word = "";
                while ((word = reader.ReadLine()) != null)
                {
                    if (word.Length != wordLength) continue;
                    if (word.Distinct().Count() != wordLength) continue;
                    if (words.Where(x => string.Concat(x, word).Distinct().Count() == wordLength).Count() > 0) continue;
                    words.Add(word);
                }
                Console.WriteLine("Antal ord: {0}", words.Count);
            }
            return words.ToArray();
        }
    }
}
