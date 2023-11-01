using Library.Interfaces;
using Library.Records;
using Library.Utils;

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
            travelTime.Generate();
            string locationUuid = e.ToUUID;
            string personUuid = e.EntityUUID;


            string text = $"{travelTime.FromClock}: {personUuid} traveled to {locationUuid}";
            Console.WriteLine(text);
            Console.WriteLine($"{travelTime.ToClock}: {personUuid} was leaving {locationUuid}");
            Console.WriteLine($"{personUuid} was away from home in the time period {travelTime}");
        }
    }
}
