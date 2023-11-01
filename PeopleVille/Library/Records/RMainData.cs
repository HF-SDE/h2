namespace Library.Records
{
    [Serializable]
    internal record RMainData
    {
        public int AmountOfFolks { get; init; }
        public int AmountOfHouses { get; init; }
        public int ItemsAmounts { get; init; }
        public int Day { get; set; }
    }
}
