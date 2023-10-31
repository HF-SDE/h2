﻿using Library.Records;
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
    internal class Save
    {
        internal void Set(JArray obj, string fileName)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new();
                serializer.Serialize(file, obj);
            }
        }

        internal void Update(string objToUpdate, dynamic updateTo, string fileName)
        {
            string jsonContent = File.ReadAllText(fileName);
            JObject jsonData = JObject.Parse(jsonContent);
            jsonData[objToUpdate] = updateTo;
            string updatedJson = jsonData.ToString(Formatting.Indented);
            File.WriteAllText(fileName, updatedJson);
        }
    }
}
