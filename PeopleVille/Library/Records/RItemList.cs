using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Records
{
    internal record RItemList
    {
        public required List<string> Adjectives { get; init; }
        public required List<string> Nouns { get; init; }
    }
}
