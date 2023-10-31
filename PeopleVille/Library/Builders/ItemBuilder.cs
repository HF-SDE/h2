using Library.Interfaces;
using Library.Utils;

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