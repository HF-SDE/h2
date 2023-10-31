using Library.Records;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Utils
{
    internal class FileClass
    {
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
                List<string> jsonStrings = JsonConvert.DeserializeObject<List<string>>(jsonText);

                List<T> result = new();

                foreach (string jsonString in jsonStrings)
                {
                    T item = JsonConvert.DeserializeObject<T>(jsonString);
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

        internal void Update<T, TData>(string fileName, string propertyName, List<T> objects, TData newValue)
        {
            try
            {
                List<string> jsonLines = File.ReadAllLines(fileName).ToList();

                // Deserialize the JSON string into an object of type T
                T obj = JsonConvert.DeserializeObject<T>(jsonLines[0]);

                // Find the property to update (e.g., "Inventory")
                var propertyInfo = typeof(T).GetProperty(propertyName);

                // Check if the property is a List or array
                if (propertyInfo != null && typeof(IEnumerable<T>).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    // Cast the property value to a list
                    var propertyValue = (IEnumerable<TData>)propertyInfo.GetValue(obj, null);

                    // Update the property if needed
                    // You can modify the logic here to update the property as required
                    if (propertyValue != null)
                    {
                        var updatedList = propertyValue.ToList();
                        updatedList.Add(newValue);

                        // Set the updated property value back to the object
                        propertyInfo.SetValue(obj, updatedList, null);

                        // Serialize the updated object back to JSON
                        jsonLines[0] = JsonConvert.SerializeObject(obj);
                    }
                }

                // Write the updated JSON back to the file
                File.WriteAllLines(fileName, jsonLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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
