using Library.Interfaces;
using static Library.Interfaces.IEvent;

namespace Library.Events
{
    internal class EventRaiser: IEvent
    {
        internal event MyEventHandler MyEvent;

        internal void DoSomething()
        {
            Console.WriteLine("Doing something...");

            // Step 4: Raise the event when something interesting happens
            OnMyEvent(EventArgs.Empty);
        }

        protected virtual void OnMyEvent(EventArgs e)
        {
            // Step 5: Check if there are any subscribers to the event before raising it
            MyEventHandler handler = MyEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
