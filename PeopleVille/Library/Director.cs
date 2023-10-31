using Library.Builders;

namespace Library
{
    public class Director
    {
        public void ConstructPeople()
        {
            PeopleBuilder builder = new();
            builder.Build();
            builder.Distribute();
        }

        public void ConstructItems()
        {
            ItemBuilder builder = new();
            builder.Build();
        }
    }
}