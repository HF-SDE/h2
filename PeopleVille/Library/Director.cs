using Library.Builders;
using Library.Events.Main;
using Library.Events.PaymentTransfer;
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

        public void NextDay()
        {
            Day day = new();
            day.AddDay();
        }

        public void ResetDay()
        {
            Day day = new();
            day.ResetDay();
        }

        public void FireEvent()
        {
            FileClass file = new();
            List<RPeople> peoples = file.Get<RPeople>(ConfigurationManager.AppSettings["peopleDataFileName"]!);


            #region Travelers
            TravelEventPublisher publisher = new();
            TravelEventSubscriber subscriber = new();
            HashSet<string> usedPersons = new();

            int amountOfTravelers = Randomizer.Range(0, peoples.Count / 3);

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
                publisher.RaiseEvent(personUuid, shopUuid);
            }
            subscriber.Unsubscribe(publisher);
            #endregion

            #region PaymentTransfers
            PaymentTransferEventPublisher paymentTransferPublisher = new();
            PaymentTransferEventSubscriber paymentTransfersubscriber = new();
            HashSet<string> usedPersonsPayment = new();

            int amountOfPayer = Randomizer.Range(0, peoples.Count / 3);
            paymentTransfersubscriber.Subscribe(paymentTransferPublisher);
            for (int i = 0; i < amountOfPayer; i++)
            {
                string personUuid = Randomizer.RandomPerson();
                string receiverUuid = Randomizer.RandomPerson();

                while (usedPersonsPayment.Contains(personUuid))
                {
                    personUuid = Randomizer.RandomPerson();

                }

                usedPersonsPayment.Add(personUuid);

                paymentTransferPublisher.RaiseEvent(Randomizer.Range(50.00F, 5000.00F), personUuid, receiverUuid);


            }
            paymentTransfersubscriber.Unsubscribe(paymentTransferPublisher);
            #endregion

        }
    }
}