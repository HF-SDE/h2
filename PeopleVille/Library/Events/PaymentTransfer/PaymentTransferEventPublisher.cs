namespace Library.Events.PaymentTransfer
{
    internal class PaymentTransferEventPublisher
    {
        internal event PaymentTransferEventHandler PaymentTransferEvent;

        internal void RaiseEvent(string entityUUID, string toUuid = "")
        {
            OnPaymentTransferEvent(new PaymentTransferEventArgs(entityUUID, toUuid));
        }

        protected virtual void OnPaymentTransferEvent(PaymentTransferEventArgs e)
        {
            PaymentTransferEvent.Invoke(this, e);
        }
    }
}
