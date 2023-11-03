using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Library.Builders
{
    public class LocationBuilder : ILocationBuilder
    {
        public void Build()
        {
            JArray locations = Create();
            FileClass file = new();
            file.Save(locations, ConfigurationManager.AppSettings["locationsFileName"]!);
        }

        private static JArray Create()
        {
            FileClass file = new();
            List<RPeople> folks = file.Get<RPeople>(ConfigurationManager.AppSettings["peopleDataFileName"]!);

            int minHouse = folks.Count() / 4 / 3 * 2;
            int maxHouse = folks.Count() / 4;
            int amountOfHouse = Randomizer.Range(minHouse, maxHouse);
            int amountOfShops = Randomizer.Range(1, 5);

            JArray house = CreateHouse(amountOfHouse);
            JArray shops = CreateShops(amountOfShops);
            JArray locations = new();

            locations.Merge(house);
            locations.Merge(shops);

            Console.WriteLine($"Amount of Shops: {shops.Count()}");
            Console.WriteLine($"Amount of Houses: {house.Count()}");

            folks = file.Get<RPeople>(ConfigurationManager.AppSettings["peopleDataFileName"]!);
            List<RPeople> peoples = folks;
            List<RPeople> peopleWithoutHome = peoples.Where(person => person.Home == "").ToList();
            Console.WriteLine($"People with house: {house.Count() * 4}");
            Console.WriteLine($"People with out a house: {peopleWithoutHome.Count()}");
            return locations;
        }

        private static JArray CreateHouse(int amountOfHouse)
        {
            RHouse house;
            JArray houses = new();
            FileClass file = new();
            for (int i = 0; i < amountOfHouse; i++)
            {
                dynamic folks = file.Get<RPeople>(ConfigurationManager.AppSettings["peopleDataFileName"]!);
                List<RPeople> peoples = folks;
                List<RPeople> peopleWithoutHome = peoples.Where(person => person.Home == "").ToList();
                List<string> members = new();
                Guid uuid = Guid.NewGuid();
                string houseUUID = uuid.ToString();
                for (int j = 0; j < 4; j++)
                {
                    int memberSelector = Randomizer.Range(0, peopleWithoutHome.Count());
                    if (peopleWithoutHome.Count() == 0) break;
                    string member = peopleWithoutHome[memberSelector].UUID;
                    if (members.Contains(member))
                    {
                        j--;
                        if (peopleWithoutHome.Count() < 4) break;
                        continue;
                    }
                    members.Add(member);
                    HouseGiver(houseUUID, peoples, member);
                }

                house = new() { UUID = houseUUID, Type = "House", Members = members };
                if (house.Members.Count == 4)
                {
                    Console.WriteLine($"House: {houseUUID} is now full");
                }
                houses.Add(JsonConvert.SerializeObject(house));
            }
            return houses;
        }

        private static JArray CreateShops(int amountOfShops)
        {
            RShop shop;
            JArray shops = new();
            FileClass file = new();
            List<RItem> items = file.Get<RItem>(ConfigurationManager.AppSettings["itemVariantsFileName"]!);
            for (int i = 0; i < amountOfShops; i++)
            {
                Guid uuid = Guid.NewGuid();
                int startRange = Randomizer.Range(0, items.Count());
                int endRange = Randomizer.Range(startRange, items.Count());
                List<RItem> resultItems = items.GetRange(0, items.Count()).ToList();

                if (resultItems.Count() > 6)
                {
                    while (startRange == endRange)
                    {
                        startRange = Randomizer.Range(0, items.Count());
                        endRange = Randomizer.Range(startRange, items.Count());
                    }
                    resultItems = items.GetRange(startRange, endRange - startRange).ToList();
                }
                
                List<RItem> itemsWithAmount = new();
                foreach (RItem item in resultItems)
                {
                    int amount = Randomizer.Range(1, 100);
                    float cost = Randomizer.Range(item.Cost / 3 * 2, item.Cost / 3 * 4);
                    itemsWithAmount.Add(new() { Name = item.Name, Cost = cost, Amount = amount });
                }
                Console.WriteLine($"Shop: {uuid} have {resultItems.Count()} amount of unique items");
                shop = new() { UUID = uuid.ToString(), Type = "Shop", Items = itemsWithAmount };
                shops.Add(JsonConvert.SerializeObject(shop));
            }
            return shops;
        }

        private static void HouseGiver(string houseUUID, List<RPeople> peoples, string memberUUID)
        {
            RPeople person = peoples.Find(person => person.UUID == memberUUID)!;

            if (person == null)
            {
                Console.WriteLine("Person not found");
                return;
            }

            int personIndex = peoples.FindIndex(person => person.UUID == memberUUID);

            person = new() {Coins = person.Coins, Home = houseUUID, Inventory = person.Inventory, UUID = person.UUID, Logs = new()};
            peoples.RemoveAt(personIndex);
            peoples.Insert(personIndex, person);

            JArray result = new();
            foreach (RPeople persons in peoples)
            {
                string json = JsonConvert.SerializeObject(persons);
                result.Add(json);
            }

            FileClass file = new();
            file.Save(result, ConfigurationManager.AppSettings["peopleDataFileName"]!);
        }
    }

}