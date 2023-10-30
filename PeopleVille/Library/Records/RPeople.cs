using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Records
{
    [Serializable]
    internal record RPeople
    {
        public required string UUID { get; init; }
        public required float Coins { get; init; }
        public required string Home { get; init; }
    }
}
