using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;

namespace ExpenseTracker.Utility
{
    public static class DataSerializer
    {
        static DataSerializer()
        {
            XmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//ExpenseXml.xml");
            JsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//ExpenseJson.Json");
        }

        public static string JsonPath { get; set; }
        public static string XmlPath { get; set; }

        /// <summary>
        /// Jsons the dserializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonString">The json string.</param>
        /// <returns></returns>
        public static T JsonDserializer<T>(string JsonString)
        {
            return JsonConvert.DeserializeObject<T>(JsonString);
        }

        /// <summary>
        /// Jsons the dserializer from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JosnPath">The josn path.</param>
        /// <returns></returns>
        public static T JsonDserializerFromFile<T>()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            // Object Obj = null;
            using (var sr = new StreamReader(JsonPath))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return (T)serializer.Deserialize<T>(jsonTextReader);
            }
            // return (T)Obj;
        }

        /// <summary>
        /// Jsons the serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string JsonSerializer<T>(Object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Jsons the serializer save as file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JosnPath">The josn path.</param>
        /// <param name="obj">The object.</param>
        public static void JsonSerializerSaveAsFile<T>(Object obj)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(JsonPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Dserialize xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="XmlFilePath">Xml path</param>
        public static T XmlDserializer<T>()
        {
            DataContractSerializer Dserializer = new DataContractSerializer(typeof(T));
            Object Obj = null;
            using (XmlReader reader = XmlReader.Create(XmlPath))
            {
                Obj = (T)Dserializer.ReadObject(reader);
            }
            return (T)Obj;
        }

        /// <summary>
        ///  Serialize xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="XmlFilePath">Xml path</param>
        public static void XmlSerializer<T>(Object obj)
        {
            DataContractSerializer Serializer = new DataContractSerializer(typeof(T));
            using (XmlWriter xw = XmlWriter.Create(XmlPath))
            {
                Serializer.WriteObject(xw, obj);
            }
        }
    }
}