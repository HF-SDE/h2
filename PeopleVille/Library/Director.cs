using Library.Builders;

namespace Library
{
    public class Director
    {
        public void ConstructPeople()
        {
            PeopleBuilder builder = new();
            builder.Build();
        }

        public void ConstructItems()
        {
            ItemBuilder builder = new();
            builder.Build();
        }

        public void ConstructLocations()
        {
            LocationBuilder builder = new();
            builder.Build();
        }
    }
}