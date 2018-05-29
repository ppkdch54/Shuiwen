using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Common;
using System.Data;
using ComConfig;
using System.IO.Ports;

namespace ShuiwenLib
{
    [Serializable]
    public  class Sites
    {
        public bool Add(uint num, string name, string ip, uint port)
        {
            if (sites.ContainsKey(num))
            {
                return false;
            }
            sites.Add(num, new Site(num,name,ip,port));
            return false;
        }
        public bool Add(Site site)
        {
            if (sites.ContainsKey(site.num))
            {
                return false;
            }
            sites.Add(site.num, site);
            return false;
        }
        public bool Delete(uint num)
        {
            return sites.Remove(num);
        }
        public bool Modify(uint num, Site st)
        {
            if (num!= st.num)
            {
                if (sites.ContainsKey(st.num))
                {
                    return false;
                }
            }
            sites.Remove(num);
            sites.Add(st.num, st);
            return true;
        }
        public bool Get(uint num,out Site st)
        {
            return sites.TryGetValue(num, out st);
        }
        public bool GetAt(int nIdx,out Site st)
        {
            try
            {
                st = sites.ElementAt(nIdx).Value;
                return true;
            }
            catch (System.Exception ex)
            {
                st = null;
                return false;
            }
        }
        public int Count()
        {
            return sites.Count;
        }

        public bool Exists(uint siteNum,uint sensorNum)
        {
            Site st;
            if (!sites.TryGetValue(siteNum,out st))
            {
                return false;
            }
            Sensor ss;
            return st.Get(sensorNum,out ss);
        }

        public void Save(IDatabase db)
        {
            //string path = Environment.CurrentDirectory + @"\sites.bin";
            Stream fStream = new MemoryStream();
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            binFormat.Serialize(fStream, this);

            byte[] config = new byte[fStream.Length];
            fStream.Seek(0, SeekOrigin.Begin);
            fStream.Read(config, 0, (int)fStream.Length);

            db.SaveConfig(config);
        }

        public SENSOR_TYPE GetType(uint site, uint sensor)
        {
            try
            {
                return sites[site].GetType(sensor);
            }
            catch (System.Exception ex)
            {
                return SENSOR_TYPE.UNKNOWN;
            }
        }

        static public void Load(out Sites sites,IDatabase db)
        {
            try
            {
                byte[] config = db.ReadConfig();
                if (config == null)
                {
                    sites = new Sites();
                    return;
                }
                Stream fStream = new MemoryStream(config);
                BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
                sites = (Sites)binFormat.Deserialize(fStream);//反序列化对象
            }
            catch (System.Exception ex)
            {
           
                sites = new Sites();
                //Log("请检查分站配置：" + ex.Message);
                throw ex;
            }
        }
        private Dictionary<uint, Site> sites = new Dictionary<uint, Site>();
    }

    [Serializable]
    public class Site 
    {
        public enum CommType
        {
            Serial = 0,
            Tcp = 1,
            SMS = 2,
            GPRS = 3
        }
        public uint num;
        public string ip;
        public uint port;
        public string name;
        //public ComSetting comSetting;
        public int baudRate { get; set; }
        public string com { get; set; }
        public int dataBits { get; set; }
        public Parity parity { get; set; }
        public StopBits stopBits { get; set; }
        public CommType commType;
       
        public Site(uint num, string name, string ip, uint port)
        {
            this.num = num;
            this.name = name;
            this.ip = ip;
            this.port = port;
            commType = CommType.Tcp;
        }

        public Site(uint num, string name, ComSetting comSetting)
        {
            this.num = num;
            this.name = name;
            this.ip = "";
            this.port = 12345;
            this.com = comSetting.com;
            this.baudRate = comSetting.baudRate;
            this.dataBits = comSetting.dataBits;
            this.stopBits = comSetting.stopBits;
            this.parity = comSetting.parity;
            commType = CommType.Serial;
        }

        public Site()
        {
            num = 0;
            port = 12345;
        }

        public SENSOR_TYPE GetType(uint num)
        {
            try
            {
                return sensors[num].type;
            }
            catch (System.Exception ex)
            {
                return SENSOR_TYPE.UNKNOWN;
            }
            
        }

        public bool Add(uint num, string name, SENSOR_TYPE type, decimal alarmLow, decimal alarmHigh)
        {
	        if (sensors.ContainsKey(num))
	        {
                return false;
	        }
            sensors.Add(num, new Sensor(num, name, type, alarmLow,alarmHigh));
            return true;
        }

        public bool Add(Sensor sensor)
        {
            if (sensors.ContainsKey(sensor.num))
            {
                return false;
            }
            try
            {
                sensors.Add(sensor.num, sensor);
            }
            catch (System.Exception ex)
            {
            	
            }
            
            return true;
        }

        public bool Delete(uint num)
        {
            return sensors.Remove(num);
        }

        public bool Modify(uint num, Sensor sensor)
        {
            if (num != sensor.num)
            {
                if (sensors.ContainsKey(sensor.num))
                {
                    return false;
                }
            }
            sensors.Remove(num);
            sensors.Add(sensor.num, sensor);
            return true;
        }

        public bool Get(uint num, out Sensor sensor)
        {
            return sensors.TryGetValue(num,out sensor);
        }

        public bool GetAt(int nIdx, out Sensor ss)
        {
            try
            {
                ss = sensors.ElementAt(nIdx).Value;
                return true;
            }
            catch (System.Exception ex)
            {
                ss = null;
                return false;
            }
            
        }
        public int Count()
        {
            return sensors.Count;
        }
        private Dictionary<uint, Sensor> sensors = new Dictionary<uint, Sensor>();
    }

    [Serializable]
    public class Sensor 
    {
        public uint num;
        public string name;
        public SENSOR_TYPE type;
        public decimal alarmLow;
        public decimal alarmHigh;
        public double sensorDeep = 0.0;
        public int interval = 3;

        public double positionX=0.0;
        public double positionY=0.0;
        public double positionZ=0.0;
        public string paper;
        public int daogui = 0;

        //水位专用
        public double ropeDepth = 0.0;
        public double wellDepth = 0.0;

        public string formula = "";

        [NonSerialized]
        public FumulaDelegate formulaDelegate;
        //
        public Sensor(uint num, string name, SENSOR_TYPE type, decimal alarmLow, decimal alarmHigh)
        {
            this.num = num;
            this.name = name;
            this.type = type;
            this.alarmLow = alarmLow;
            this.alarmHigh = alarmHigh;
        }
        public static Dictionary<SENSOR_TYPE, string> items = new Dictionary<SENSOR_TYPE, string> {
            {SENSOR_TYPE.GUANDAO,"管道流量" },
            {SENSOR_TYPE.WENDU,"温度"},
            {SENSOR_TYPE.YALI,"压强"},
            {SENSOR_TYPE.WUWEI,"物位"},
            {SENSOR_TYPE.SHUIWEI,"水位"},
            {SENSOR_TYPE.MINGQU,"明渠流量"},
            {SENSOR_TYPE.DIANYA,"电压"}
        };

        public static Dictionary<SENSOR_TYPE, string> icons = new Dictionary<SENSOR_TYPE, string> {
            {SENSOR_TYPE.GUANDAO,"流" },
            {SENSOR_TYPE.WENDU,"温"},
            {SENSOR_TYPE.YALI,"压"},
            {SENSOR_TYPE.WUWEI,"物"},
            {SENSOR_TYPE.SHUIWEI,"水"},
            {SENSOR_TYPE.MINGQU,"渠"},
            {SENSOR_TYPE.DIANYA,"V"}
        };
    }
    
    public enum SENSOR_TYPE
    {
        WENDU=0,
		YALI,
		WUWEI,
		SHUIWEI,
		GUANDAO,
        MINGQU,
        DIANYA,
        UNKNOWN,
    }

    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public bool IsEnable { get; set; }
        

    }
}
