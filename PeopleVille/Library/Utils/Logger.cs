using Library.Records;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.Utils
{
    internal class Logger
    {
        internal void Log(string fileName, string uuid, int day, string time, string message) {
            List<RLogs> logs = new();

            FileClass file = new();
            List<RPeople> data = file.Get<RPeople>(fileName);
            logs.AddRange(data.First(person => person.UUID == uuid).Logs);
            logs.Add(new() { Day = day, Time = time, EventText = message });

            int index = data.FindIndex(person => person.UUID == uuid);
            float coins = data.First(person => person.UUID == uuid).Coins;
            string home = data.First(person => person.UUID == uuid).Home;

            List<RItem> inventory = data.First(person => person.UUID == uuid).Inventory;

            data.RemoveAt(index);
            data.Insert(index, new() { Coins = coins, Home = home, Inventory = inventory, Logs = logs, UUID = uuid });
            file.SaveHelper(fileName, data);
        }


    }
}
