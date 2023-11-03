namespace Library.Events.Main
{
    internal class TravelEventArgs : EventArgs
    {

        internal TravelEventArgs(string uuid, string toUuid = "")
        {
            EntityUUID = uuid;
            ToUUID = toUuid;
        }

        internal string EntityUUID { get; set; }
        internal string ToUUID { get; set; }
    }
}
