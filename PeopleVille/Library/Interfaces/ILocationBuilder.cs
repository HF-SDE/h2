using Library.Records;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Library.Interfaces
{
    internal interface ILocationBuilder : IBuilder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private static JArray Create()
        {
            JArray items = new();
            return items;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private static JArray CreateHouse(int amountOfHouse) { return new JArray(amountOfHouse); }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")
        private static JArray CreateShops(int amountOfShops) { return new JArray(); }

        void HouseGiver(string houseUUID, List<RPeople> peoples, string memberUUID) { }
    }
}
