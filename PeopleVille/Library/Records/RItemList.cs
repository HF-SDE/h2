namespace Library.Records
{
    internal record RItemList
    {
        public required List<string> Adjectives { get; init; }
        public required List<string> Nouns { get; init; }
    }
}
