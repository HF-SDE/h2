using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Records
{
    [Serializable]
    internal record RLocation
    {
        public required string UUID { get; init; }
        public required string Type { get; init; }
    }
}
