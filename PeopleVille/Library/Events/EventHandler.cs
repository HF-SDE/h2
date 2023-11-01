namespace Library.Events
{
    internal class EventHandler
    {
        internal static void Main()
        {
            EventRaiser myObject = new();
            EventSubscriber subscriber = new();

            // Step 7: Subscribe to the event by adding the event handler method
            myObject.MyEvent += subscriber.HandleEvent;

            // Step 8: Call a method that raises the event
            myObject.DoSomething();

            // Step 9: Unsubscribe from the event (optional)
            myObject.MyEvent -= subscriber.HandleEvent;

            // Calling the method again won't trigger the event for the unsubscribed handler
            myObject.DoSomething();
        }
    }
}
