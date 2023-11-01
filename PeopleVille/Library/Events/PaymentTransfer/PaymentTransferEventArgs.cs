namespace Library.Events.PaymentTransfer
{
    internal class PaymentTransferEventArgs : EventArgs
    {

        internal PaymentTransferEventArgs(string uuid, string toUuid = "")
        {
            EntityUUID = uuid;
            ToUUID = toUuid;
        }

        internal string EntityUUID { get; set; }
        internal string ToUUID { get; set; }
    }
}
