using Library.Events.PaymentTransfer;

namespace Library.Interfaces
{
    internal interface IPaymentTransferEvent
    {
        void OnPaymentTransferEvent(object sender, PaymentTransferEventArgs e);
        void Subscribe(PaymentTransferEventPublisher publisher);
        void Unsubscribe(PaymentTransferEventPublisher publisher);
    }
}
