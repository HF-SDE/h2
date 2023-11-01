using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Utils
{
    internal struct TravelTime
    {
        internal string FromClock {  get; set; }
        internal string ToClock {  get; set; }

        internal void Generate()
        {
            NumberConverter converter = new();
            int hoursFrom = Randomizer.Range(0, 23);
            int minFrom = Randomizer.Range(1, 50);
            string fromHours = converter.ConvertWithLeadingZero(hoursFrom);
            string fromMin = converter.ConvertWithLeadingZero(minFrom);
            FromClock = $"[{fromHours}:{fromMin}]";

            int hoursTo = 23;
            int minTo = Randomizer.Range(minFrom, 59);
            if (hoursFrom != 23)
            {
                hoursTo = Randomizer.Range(hoursFrom, 23);
            }
            string toHours = converter.ConvertWithLeadingZero(hoursTo);
            string toMin = converter.ConvertWithLeadingZero(minTo);
            ToClock = $"[{toHours}:{toMin}]";
        }

        public override string ToString() => $"{FromClock} - {ToClock}";
    }
}
