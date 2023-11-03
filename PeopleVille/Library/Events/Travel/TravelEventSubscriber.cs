using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using System.Configuration;

namespace Library.Events.Main
{
    internal class TravelEventSubscriber : ITravelEvent
    {
        public void Subscribe(TravelEventPublisher publisher)
        {
            publisher.TravelEvent += OnTravelEvent;
        }

        public void Unsubscribe(TravelEventPublisher publisher)
        {
            publisher.TravelEvent -= OnTravelEvent;
        }

        public void OnTravelEvent(object sender, TravelEventArgs e)
        {
            TravelTime travelTime = new();
            travelTime.Generate(5, 22);
            string locationUuid = e.ToUUID;
            string personUuid = e.EntityUUID;

            Logger logger = new();
            Day day = new();
            string fileName = ConfigurationManager.AppSettings["peopleDataFileName"]!;

            string text = $"{personUuid} traveled to {locationUuid}";
            Console.WriteLine($"{day} day {travelTime.FromClock}: {text}");
            logger.Log(fileName, personUuid, day.Get(), travelTime.FromClock, text);

            text = $"{personUuid} was leaving {locationUuid}";
            Console.WriteLine($"{day} day {travelTime.ToClock}: {text}");
            logger.Log(fileName, personUuid, day.Get(), travelTime.ToClock, text);

            text = $"{personUuid} was away from home in the time period";
            Console.WriteLine($"{day} day {travelTime} {text}");
            logger.Log(fileName, personUuid, day.Get(), travelTime.ToString(), text);
        }
    }
}
