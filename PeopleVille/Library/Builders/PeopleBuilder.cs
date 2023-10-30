using Library.Interfaces;
using Library.Records;
using Library.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Library.Builders
{
    public class PeopleBuilder : IBuilder
    {
        public void Build()
        {
            JArray Folks = new();
            int AmountOfFolks = Randomizer.Range(200, 2000);
            for (int i = 0; i < AmountOfFolks; i++)
            {
                Folks.Add(JsonConvert.SerializeObject(Create()));
                //Console.WriteLine(JsonConvert.SerializeObject(Create()));
            }
            Save save = new();
            save.Set(Folks, "folks.json");
            Console.WriteLine("Saved folks");
        }

        private RPeople Create()
        {
            Guid uuid = Guid.NewGuid();
            float money = Randomizer.Range(1.0F, 99999.0F);
            RPeople instance = new() { UUID = uuid.ToString(), Coins = money, Home = "" };
            return instance;
        }
    }
}