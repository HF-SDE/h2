namespace Library.Records
{
    [Serializable]
    internal record RLogs
    {
        public int Day { get; init; }
        public string Time { get; init; }
        public string EventText { get; init; }
    }
}
