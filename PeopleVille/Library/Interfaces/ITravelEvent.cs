using Library.Events.Main;

namespace Library.Interfaces
{
    internal interface ITravelEvent
    {
        void OnTravelEvent(object sender, TravelEventArgs e);
        void Subscribe(TravelEventPublisher publisher);
        void Unsubscribe(TravelEventPublisher publisher);
    }
}
