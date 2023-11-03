namespace Library.Records
{
    [Serializable]
    internal record RHouse : RLocation
    {
        public required List<string> Members { get; init; }
        public int MaxMembers { get; init; } = 4;
    }
}
