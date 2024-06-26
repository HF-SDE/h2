﻿using Library.Records;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.Utils
{
    internal class FileClass
    {

        internal void SaveHelper<T>(string fileName, List<T> data)
        {
            JArray jArray = new();

            foreach (var item in data)
            {
                jArray.Add(JsonConvert.SerializeObject(item));
            }
            Save(jArray, fileName);
        }

        internal void Save(JArray obj, string fileName)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new();
                serializer.Serialize(file, obj);
            }
        }

        internal dynamic Get<T>(string fileName)
        {
            try
            {
                string jsonText = File.ReadAllText(fileName);
                List<string> jsonStrings = JsonConvert.DeserializeObject<List<string>>(jsonText)!;                

                List<T> result = new();

                foreach (string jsonString in jsonStrings)
                {
                    T item = JsonConvert.DeserializeObject<T>(jsonString)!;
                    result.Add(item);
                }
                return result!;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                return "Error";
            }
            catch (JsonException)
            {
                Console.WriteLine("Error deserializing JSON.");
                return "Error";
            }
        }

        internal void Update(string objToUpdate, dynamic updateTo, string fileName)
        {
            try
            {
                string jsonContent = File.ReadAllText(fileName);
                JObject jsonData = JObject.Parse(jsonContent);
                jsonData[objToUpdate] = updateTo;
                string updatedJson = jsonData.ToString(Formatting.Indented);
                File.WriteAllText(fileName, updatedJson);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
