using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShuiwenServer
{
    [Serializable]
    public class Config
    {
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public string DatabaseIP { get; set; }
        public int DatabasePort { get; set; }
        public string DatabaseUserName { get; set; }
        public string DatabasePassword { get; set; }

        static string currentPath = Directory.GetCurrentDirectory();
        public static Config LoadConfig()
        {
            string path = Path.Combine(currentPath, "Config.ini");
            if (File.Exists(path))
            {
                byte[] datas = File.ReadAllBytes(path);
                Stream fStream = new MemoryStream(datas);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                Config config = (Config)binFormat.Deserialize(fStream);//反序列化对象
                if (config == null)
                {
                    config = new Config();
                }
                return config;
            }
            return new Config();
        }

        public void SaveConfig()
        {
            Stream fStream = new MemoryStream();
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            binFormat.Serialize(fStream, this);

            string path = Path.Combine(currentPath, "Config.ini");
            byte[] config = new byte[fStream.Length];
            fStream.Seek(0, SeekOrigin.Begin);
            fStream.Read(config, 0, (int)fStream.Length);
            File.WriteAllBytes(path, config);
        }

    }

    
}
