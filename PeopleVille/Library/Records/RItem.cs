namespace Library.Records
{
    [Serializable]
    internal record RItem
    {
        public required string Name { get; init; }
        public required float Cost { get; init; }
        public int Amount { get; init; } = -1;
    }
}
