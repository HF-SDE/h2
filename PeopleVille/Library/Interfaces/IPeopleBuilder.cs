using Library.Records;

namespace Library.Interfaces
{
    internal interface IPeopleBuilder: IBuilder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private static RPeople Create() 
        {
            List<RItem> items = new();
            RPeople rPeople = new() { UUID = "", Coins = 0.00F, Home = "", Inventory = items };
            return rPeople;
        }
    }
}
