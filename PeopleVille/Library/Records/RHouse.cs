namespace Library.Records
{
    [Serializable]
    internal record RHouse : RLocation
    {
        public required List<string> Members { get; init; }
        public required int MaxMembers { get; init; }
    }
}
