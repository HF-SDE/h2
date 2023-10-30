using Library.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Utils
{
    internal class Randomizer
    {
        private static readonly Random _rnd = new();
        internal static int Range(int lower, int higher)
        {
            return _rnd.Next(lower, higher);
        }

        internal static float Range(float lower, float higher)
        {
            float floatValue = (float)(_rnd.NextDouble() * (higher - lower)) + lower;
            int intValue = (int)(floatValue * 100);
            return intValue / 100.0f;
        }

        internal string GenerateRandomItemName()
        {
            Load load = new();
            (List<string>? adjectives, List<string>? nouns) = load.LoadItemListsFromJson("Items.json");

            if (adjectives == null || nouns == null)
            {
                return ("ERROR");
            }
            string adjective = adjectives[_rnd.Next(adjectives.Count)];
            string noun = nouns[_rnd.Next(nouns.Count)];

            return $"{adjective} {noun}";
        }
    }
}
