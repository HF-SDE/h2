using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Utils
{
    internal class TravelTime
    {
        internal string FromClock {  get; set; }
        internal string ToClock {  get; set; }

        internal void Generate(int fromHoursClock, int toHoursClock)
        {
            NumberConverter converter = new();
            int hoursFrom = Randomizer.Range(fromHoursClock, toHoursClock);
            int minFrom = Randomizer.Range(1, 50);
            string fromHours = converter.ConvertWithLeadingZero(hoursFrom);
            string fromMin = converter.ConvertWithLeadingZero(minFrom);
            FromClock = $"[{fromHours}:{fromMin}]";

            int hoursTo = toHoursClock;
            int minTo = Randomizer.Range(minFrom, 59);
            if (hoursFrom != toHoursClock)
            {
                hoursTo = Randomizer.Range(hoursFrom, toHoursClock);
            }
            string toHours = converter.ConvertWithLeadingZero(hoursTo);
            string toMin = converter.ConvertWithLeadingZero(minTo);
            ToClock = $"[{toHours}:{toMin}]";
        }

        public override string ToString() => $"{FromClock} - {ToClock}";
    }
}
