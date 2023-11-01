namespace Library.Events
{
    internal class EventSubscriber
    {
        internal void HandleEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Event handled by EventSubscriber");
        }
    }
}
