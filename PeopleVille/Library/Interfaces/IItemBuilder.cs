using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Library.Interfaces
{
    internal interface IItemBuilder: IBuilder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private static JArray Create()
        {
            JArray items = new();
            return items;
        }
    }
}
