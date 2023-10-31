using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Library.Builders
{
    public class ItemBuilder : IBuilder
    {
        public void Build()
        {
            JArray items = Create();

            Save save = new();
            save.Set(items, "itemVariants.json");

            int itemsAmounts = items.Count;
            save.Update("ItemsAmounts", itemsAmounts, "MainData.json");

            Console.WriteLine($"Amount of item variants: {itemsAmounts}");
            Console.WriteLine("Item variants saved");

        }

        private JArray Create()
        {
            Randomizer randomizer = new();
            int maxItems = Randomizer.Range(5, 20);
            JArray items = new();
            RItem item;
            for (int i = 0; i < maxItems; i++)
            {
                string randomItemName = randomizer.GenerateRandomItemName();
                float itemCost = Randomizer.Range(10.99F, 999.99F);
                item = new() { Name = randomItemName, Cost = itemCost };
                while (items.Contains(randomItemName))
                {
                    randomItemName = randomizer.GenerateRandomItemName();
                    item = new() { Name = randomItemName, Cost = itemCost };
                }
                items.Add(JsonConvert.SerializeObject(item));
            }
            return items;
        }
    }
}