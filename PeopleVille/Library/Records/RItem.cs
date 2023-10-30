using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Records
{
    [Serializable]
    internal record RItem
    {
        public required string Name { get; init; }
        public required float Cost { get; init; }
    }
}
