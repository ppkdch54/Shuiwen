using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;

namespace Shuiwen
{
    class UiChange
    {
        public static void Set(string key, string value)
        {
            if (!objs.ContainsKey(key))
            {
                objs.Add(key, value);
            }
            else
            {
                objs[key] = value;
            }
        }

        public static string Get(string key)
        {
            if (!objs.ContainsKey(key))
            {
                return "";
            }
            else
            {
                return objs[key];
            }
        }
        private static object locker = new object();
        public static void Save()
        {
            string path = Environment.CurrentDirectory + @"\UICHANGE.xml";
            lock(locker)
            {
                Stream fStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer format = new XmlSerializer(typeof(SerializableDictionary<string, string>));//创建二进制序列化器
                format.Serialize(fStream, objs);
                fStream.Close();
            }

        }

        public static void Load()
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\UICHANGE.xml";
                lock (locker)
                {
                    Stream fStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    XmlSerializer format = new XmlSerializer(typeof(SerializableDictionary<string, string>));//创建二进制序列化器
                    objs = (SerializableDictionary<string, string>)format.Deserialize(fStream);
                    fStream.Close();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public static SerializableDictionary<string, string> objs = new SerializableDictionary<string, string>();
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerializableDictionary() { }
        public void WriteXml(XmlWriter write)       // Serializer
        {
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                write.WriteStartElement("SerializableDictionary");
                write.WriteStartElement("key");
                KeySerializer.Serialize(write, kv.Key);
                write.WriteEndElement();
                write.WriteStartElement("value");
                ValueSerializer.Serialize(write, kv.Value);
                write.WriteEndElement();
                write.WriteEndElement();
            }
        }
        public void ReadXml(XmlReader reader)       // Deserializer
        {
            reader.Read();
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("SerializableDictionary");
                reader.ReadStartElement("key");
                TKey tk = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue vl = (TValue)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(tk, vl);
                reader.MoveToContent();
            }
            reader.ReadEndElement();

        }
        public XmlSchema GetSchema()
        {
            return null;
        }
    }
}
