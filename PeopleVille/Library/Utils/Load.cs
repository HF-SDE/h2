using Library.Records;
using System.Text.Json;

namespace Library.Utils
{
    internal class Load
    {

        public (List<string>?, List<string>?) LoadItemListsFromJson(string filePath)
        {
            List<string> adjectives = new();
            List<string> nouns = new();
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new(filePath))
                {
                    string json = reader.ReadToEnd();
                    var itemLists = JsonSerializer.Deserialize<RItemList>(json);

                    if (itemLists != null)
                    {
                        adjectives = itemLists.Adjectives;
                        nouns = itemLists.Nouns;
                    }
                }
                return (adjectives, nouns);
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
                return (null, null);
            }
        }
    }
}
