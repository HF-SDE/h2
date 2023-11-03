namespace Library.Records
{
    [Serializable]
    internal record RShop : RLocation
    {
        public required List<RItem> Items { get; init; }
    }
}
