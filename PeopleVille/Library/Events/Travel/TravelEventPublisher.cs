namespace Library.Events.Main
{
    internal class TravelEventPublisher
    {
        internal event TravelEventHandler TravelEvent;

        internal void RaiseEvent(string entityUUID, string toUuid = "")
        {
            OnTravelEvent(new TravelEventArgs(entityUUID, toUuid));
        }

        protected virtual void OnTravelEvent(TravelEventArgs e)
        {
            TravelEvent.Invoke(this, e);
        }
    }
}
