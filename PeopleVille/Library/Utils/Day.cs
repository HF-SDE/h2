using Library.Records;
using Newtonsoft.Json;
using System.Configuration;

namespace Library.Utils
{
    internal struct Day
    {

        internal int Get()
        {
            dynamic mainData = File.ReadAllText(ConfigurationManager.AppSettings["mainDataFileName"]!);
            RMainData rMainData = JsonConvert.DeserializeObject<RMainData>(mainData);
            return rMainData.Day;
        }

        internal void AddDay()
        {
            string fileName = ConfigurationManager.AppSettings["mainDataFileName"]!;
            dynamic mainData = File.ReadAllText(fileName);
            RMainData rMainData = JsonConvert.DeserializeObject<RMainData>(mainData);
            rMainData.Day += 1;

            FileClass file = new();
            file.Update("Day", rMainData.Day, fileName);
        }

        public override string ToString()
        {
            dynamic mainData = File.ReadAllText(ConfigurationManager.AppSettings["mainDataFileName"]!);
            RMainData rMainData = JsonConvert.DeserializeObject<RMainData>(mainData);
            return rMainData.Day.ToString();
        }
    }
}
