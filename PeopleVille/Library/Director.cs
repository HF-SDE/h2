using Library.Builders;
using Library.Events.Main;
using Library.Records;
using Library.Utils;
using System.Configuration;

namespace Library
{
    public class Director
    {
        public void ConstructPeople()
        {
            PeopleBuilder builder = new();
            builder.Build();
        }

        public void ConstructItems()
        {
            ItemBuilder builder = new();
            builder.Build();
        }

        public void ConstructLocations()
        {
            LocationBuilder builder = new();
            builder.Build();
        }

        public void FireEvent()
        {
            PaymentTransferEventPublisher publisher = new();
            PaymentTransferEventSubscriber subscriber = new();


            FileClass file = new();
            List<RPeople> peoples = file.Get<RPeople>(ConfigurationManager.AppSettings["peopleDataFileName"]!);
            int amountOfTravelers = Randomizer.Range(0, peoples.Count / 3);
            HashSet<string> usedPersons = new();

            subscriber.Subscribe(publisher);
            for (int i = 0; i < amountOfTravelers; i++)
            {
                string personUuid = Randomizer.RandomPerson();
                string shopUuid = Randomizer.RandomShop();
                while (usedPersons.Contains(personUuid))
                {
                    personUuid = Randomizer.RandomPerson();

                }

                usedPersons.Add(personUuid);
                publisher.RaiseEvent(PaymentTransferEventArgs.Actions.ShopTravel, personUuid, shopUuid);


            }

            subscriber.Unsubscribe(publisher);

        }
    }
}