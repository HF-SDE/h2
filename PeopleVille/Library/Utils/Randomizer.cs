using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Utils
{
    internal class Randomizer
    {
        private static readonly Random _rnd = new();
        public static int Range(int lower, int higher) 
        { 
            return _rnd.Next(lower, higher); 
        }

    }
}
