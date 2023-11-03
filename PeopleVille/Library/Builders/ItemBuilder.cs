using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Library.Builders
{
    public class ItemBuilder : IItemBuilder
    {
        public void Build()
        {
            JArray items = Create();
            //items = new JArray(items.OrderBy(jv => jv.ToString(), StringComparer.OrdinalIgnoreCase));

            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
                //}

            FileClass file = new();
            Console.WriteLine(ConfigurationManager.AppSettings["itemVariantsFileName"]!);
            file.Save(items, ConfigurationManager.AppSettings["itemVariantsFileName"]!);

            int itemsAmounts = items.Count;
            file.Update("ItemsAmounts", itemsAmounts, ConfigurationManager.AppSettings["mainDataFileName"]!);

            Console.WriteLine($"Amount of item variants: {itemsAmounts}");
            Console.WriteLine("Item variants saved");

        }

        private static JArray Create()
        {
            Randomizer randomizer = new();
            int maxItems = Randomizer.Range(5, 100);
            JArray items = new();
            RItem item;
            HashSet<string> uniqueItemNames = new();
            for (int i = 0; i < maxItems; i++)
            {
                string randomItemName = randomizer.GenerateRandomItemName();
                float itemCost = Randomizer.Range(10.99F, 999.99F);
                item = new() { Name = randomItemName, Cost = itemCost };
                //Console.WriteLine(new JArray(items.OrderBy(jv => jv.ToString(), StringComparer.OrdinalIgnoreCase)));
                while (uniqueItemNames.Contains(randomItemName))
                {
                    randomItemName = randomizer.GenerateRandomItemName();
                    item = new() { Name = randomItemName, Cost = itemCost };
                }
                uniqueItemNames.Add(randomItemName);
                items.Add(JsonConvert.SerializeObject(item));
            }
            return items;
        }
    }
}