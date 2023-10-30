using Library.Interfaces;
using Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Builders
{
    public class ItemBuilder : IBuilder
    {
        public void Build()
        {
            Randomizer randomizer = new();

            for (int i = 0; i < 10; i++)
            {
                string randomItemName = randomizer.GenerateRandomItemName();
                Console.WriteLine(randomItemName);
            }
        }

        public void Create()
        {
            throw new NotImplementedException();
        }
    }
}