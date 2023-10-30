using Library.Builders;
using Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}