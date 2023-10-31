namespace Library.Records
{
    [Serializable]
    internal record RMainData
    {
        public int AmountOfFolks { get; init; }
        public int AmoutOfHouses { get; set; }
    }
}
