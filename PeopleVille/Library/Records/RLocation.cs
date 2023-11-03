namespace Library.Records
{
    [Serializable]
    internal record RLocation
    {
        public required string UUID { get; init; }
        public required string Type { get; init; }
    }
}
