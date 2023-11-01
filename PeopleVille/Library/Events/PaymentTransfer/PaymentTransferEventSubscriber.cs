using Library.Interfaces;
using Library.Utils;

namespace Library.Events.PaymentTransfer
{
    internal class PaymentTransferEventSubscriber : IPaymentTransferEvent
    {
        public void Subscribe(PaymentTransferEventPublisher publisher)
        {
            publisher.PaymentTransferEvent += OnPaymentTransferEvent;
        }

        public void Unsubscribe(PaymentTransferEventPublisher publisher)
        {
            publisher.PaymentTransferEvent -= OnPaymentTransferEvent;
        }

        public void OnPaymentTransferEvent(object sender, PaymentTransferEventArgs e)
        {
            TravelTime travelTime = new();
            travelTime.Generate();
            string locationUuid = e.ToUUID;
            string personUuid = e.EntityUUID;

            Console.WriteLine($"{travelTime.FromClock}: {personUuid} traveled to {locationUuid}");
            Console.WriteLine($"{travelTime.ToClock}: {personUuid} was leaving {locationUuid}");
            Console.WriteLine($"{personUuid} was away from home in the time period {travelTime}");
        }
    }
}
