using Board_Gamer_App.Resources.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Board_Gamer_App
{
    public static class SaveManagement
    {
        public static T XMLByteStreamToObject<T>(Byte[] stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Menu>));
            Stream memoryStream = new MemoryStream(stream);
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
