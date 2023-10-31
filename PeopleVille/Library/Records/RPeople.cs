using System.Collections;

namespace Library.Records
{
    [Serializable]
    internal record RPeople
    {
        public required string UUID { get; init; }
        public required float Coins { get; init; }
        public required string Home { get; init; }

        public required List<RItem> Inventory { get; init; }
    }
}
