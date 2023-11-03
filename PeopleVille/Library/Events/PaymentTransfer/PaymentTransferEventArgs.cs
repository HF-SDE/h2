namespace Library.Events.PaymentTransfer
{
    internal class PaymentTransferEventArgs : EventArgs
    {

        internal PaymentTransferEventArgs(float amount, string uuid, string toUuid = "")
        {
            EntityUUID = uuid;
            ToUUID = toUuid;
            Amount = amount;
        }

        internal float Amount { get; set; }
        internal string EntityUUID { get; set; }
        internal string ToUUID { get; set; }
    }
}
