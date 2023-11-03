using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Library.Builders
{
    public class PeopleBuilder : IPeopleBuilder
    {
        private readonly FileClass _file = new();
        public void Build()
        {
            JArray Folks = new();
            int AmountOfFolks = Randomizer.Range(200, 2000);
            HashSet<Guid> uniqueGuids = new();
            for (int i = 0; i < AmountOfFolks; i++)
            {
                RPeople people = Create();
                while(!uniqueGuids.Add(Guid.Parse(people.UUID)))
                {
                    people = Create();
                }
                Folks.Add(JsonConvert.SerializeObject(people));
                uniqueGuids.Add(Guid.Parse(people.UUID));
                //Console.WriteLine(JsonConvert.SerializeObject(Create()));
            }
            _file.Save(Folks, ConfigurationManager.AppSettings["peopleDataFileName"]!);

            int folksCount = Folks.Count;
            _file.Update("AmountOfFolks", folksCount, ConfigurationManager.AppSettings["mainDataFileName"]!);

            Console.WriteLine($"Amount of Poeple: {folksCount}");
            Console.WriteLine("Saved folks");
        }

        private RPeople Create()
        {
            dynamic fileItem = _file.Get<RItem>(ConfigurationManager.AppSettings["itemVariantsFileName"]!);
            if (fileItem.GetType() != typeof(List<RItem>))
            {
                Console.WriteLine("Error in distribute items or homes to people");
            }

            List<RItem> itemsList = fileItem;
            int random = Randomizer.Range(1, itemsList.Count);
            RItem item = itemsList[random];
            List<RItem> items = new()
            {
                new() { Cost = item.Cost, Amount = 1, Name = item.Name }
            };

            Guid uuid = Guid.NewGuid();
            float money = Randomizer.Range(1.0F, 99999.0F);
            
            RPeople instance = new() { UUID = uuid.ToString(), Coins = money, Home = "", Inventory = items, Logs = new() };
            return instance;
        }
    }
}