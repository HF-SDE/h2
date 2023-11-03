namespace Library.Events.PaymentTransfer
{
    internal class PaymentTransferEventPublisher
    {
        internal event PaymentTransferEventHandler PaymentTransferEvent;

        internal void RaiseEvent(float amount, string entityUUID, string toUuid = "")
        {
            OnPaymentTransferEvent(new PaymentTransferEventArgs(amount, entityUUID, toUuid));
        }

        protected virtual void OnPaymentTransferEvent(PaymentTransferEventArgs e)
        {
            PaymentTransferEvent.Invoke(this, e);
        }
    }
}
