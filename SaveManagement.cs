using Board_Gamer_App.Resources.Values;
using Microsoft.Maui.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Board_Gamer_App
{
    public static class SaveManagement
    {
        public class Entry
        {
            public string Key;
            public Menu Value;
            public Entry()
            {
            }

            public Entry(string key, Menu value)
            {
                Key = key;
                Value = value;
            }
        }

        public static T XMLByteStreamToObject<T>(Byte[] stream)
        {
            XmlSerializer serializer = new(typeof(T));
            Stream memoryStream = new MemoryStream(stream);
            return (T)serializer.Deserialize(memoryStream);
        }
        public static string ConvertObjectToXMLString<T>(T o)
        {
            XmlSerializer serializer = new(typeof(T));
            StringWriter writer = new();

            serializer.Serialize(writer, o);

            return writer.ToString();
        }
        public static T ConvertXMLStringToObject<T>(string objectString)
        {
            XmlSerializer serializer = new(typeof(T));
            Stream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(objectString));

            return (T)serializer.Deserialize(memoryStream);
        }

        public static void SaveObjectAsXML<T>(string fileName, T o)
        {
            XmlSerializer serializer = new(typeof(T));
            MemoryStream stream = new();

            serializer.Serialize(stream, o);

            SaveBytes(fileName, stream.ToArray());
        }

        public static T ReadObjectFromXML<T>(string fileName)
        {
            XmlSerializer serializer = new(typeof(T));
            Byte[] bytes = ReadBytes(fileName);

            string path = FileSystem.Current.AppDataDirectory + fileName;
            Stream stream = new MemoryStream(bytes);
            return (T)serializer.Deserialize(stream);
        }


        public static void SaveBytes(string fileName, Byte[] bytes)
        {
            string path = FileSystem.Current.AppDataDirectory + fileName;

            File.WriteAllBytes(path, bytes);
        }

        public static Byte[] ReadBytes(string fileName)
        {
            string path = FileSystem.Current.AppDataDirectory + fileName;
            return File.ReadAllBytes(path);
        }

        public static void SaveString(string fileName, string text)
        {
            string path = FileSystem.Current.AppDataDirectory + fileName;

            File.WriteAllText(path, text);
        }

        public static string ReadString(string fileName)
        {
            string path = FileSystem.Current.AppDataDirectory + fileName;

            return File.ReadAllText(path);
        }
    }
}
