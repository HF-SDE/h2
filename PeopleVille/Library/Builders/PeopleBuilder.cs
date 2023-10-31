using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.Builders
{
    public class PeopleBuilder : IPeopleBuilder, IDistribute
    {
        FileClass file = new();
        public void Build()
        {
            JArray Folks = new();
            int AmountOfFolks = Randomizer.Range(200, 2000);
            for (int i = 0; i < AmountOfFolks; i++)
            {
                Folks.Add(JsonConvert.SerializeObject(Create()));
                //Console.WriteLine(JsonConvert.SerializeObject(Create()));
            }
            file.Save(Folks, "folks.json");

            int folksCount = Folks.Count;
            file.Update("AmountOfFolks", folksCount, "MainData.json");

            Console.WriteLine($"Amount of Poeple: {folksCount}");
            Console.WriteLine("Saved folks");
        }

        public void Distribute()
        {
            dynamic fileItem = file.Get<RItem>("itemVariants.json");
            dynamic fileFolks = file.Get<RPeople>("folks.json");

            if (fileItem.GetType() != typeof(List<RItem>) || fileFolks.GetType() != typeof(List<RPeople>))
            {
                Console.WriteLine("Error in distribute items or homes to people");
                return;
            }

            List<RItem> items = fileItem;
            List<RPeople> folks = fileFolks;
            JArray folksArray = new();

            foreach (RPeople person in folks)
            {
                int random = Randomizer.Range(1, items.Count);
                RItem item = items[random];
                Console.WriteLine(folks[random]);
                person.Inventory.Add(new() { Amount = item.Amount + 1, Cost = item.Cost, Name = item.Name });
                folksArray.Add(person);
            }
            file.Save(folksArray, "folks.json");
        }

        private static RPeople Create()
        {
            Guid uuid = Guid.NewGuid();
            float money = Randomizer.Range(1.0F, 99999.0F);
            List<RItem> items = new();
            RPeople instance = new() { UUID = uuid.ToString(), Coins = money, Home = "", Inventory = items };
            return instance;
        }
    }
}