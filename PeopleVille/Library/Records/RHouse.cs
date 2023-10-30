using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Records
{
    [Serializable]
    internal record RHouse : RLocation
    {
        public required List<string> Members { get; init; }
        public required int MaxMembers { get; init; }
    }
}
