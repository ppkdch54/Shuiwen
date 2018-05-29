using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuiwenLib;
using System.Collections;
using System.IO;

namespace ShuiwenService
{
    class Parser
    {
        public static void Parse(List<byte> recvDatas,uint siteNum,Sites sites)
        {
            byte b = 0;
            STATUS s = STATUS.R_UNKOWN;
            int nEB = 0;
            int nParsed = 0;

            for (int i = 0; i < recvDatas.Count; ++i)
            {
                string sss = ByteToHexString(recvDatas.ToArray(), recvDatas.ToArray().Length);
                //System.IO.File.AppendAllText("c:/data.txt", sss + "\r\n");
                //Console.WriteLine(sss);
                b = recvDatas[i];
                switch (s)
                {
                    case STATUS.R_UNKOWN:
                        {
                            if (b == 0xEB)
                            {
                                s = STATUS.R_EB;
                                nEB = i;
                            }
                        }
                        break;
                    case STATUS.R_EB:
                        {
                            if (b == 0x90)
                            {
                                s = STATUS.R_90;
                            }
                        }
                        break;
                    
                    case STATUS.R_90:
                        {
                            if (i - nEB == 9)
                            {
                                if (b == 0xAA)
                                {
                                    siteNum = recvDatas[nEB + 2];
                                    uint sensorNum = recvDatas[nEB+3];
                                    //recvDatas[nEB+8],recvDatas[nEB+7],recvDatas[nEB+6],recvDatas[nEB+5]
                                    //double d = (double)BitConverter.ToUInt16(new byte[]{recvDatas[nEB+6],recvDatas[nEB+5]},0);
                                    double?[] data = new double?[5];  //用来存放数据，长度可能还会更改
                                    //double d = (double)BitConverter.ToUInt16(new byte[] { recvDatas[nEB + 8], recvDatas[nEB + 7], recvDatas[nEB + 6], recvDatas[nEB + 5] }, 0);
                                    SENSOR_TYPE type = sites.GetType(siteNum,sensorNum);
                                    switch (type)
                                    {
                                        case SENSOR_TYPE.SHUIWEI:    
                                        case SENSOR_TYPE.YALI:
                                        case SENSOR_TYPE.WENDU:
                                        case SENSOR_TYPE.DIANYA:
                                            data[0] = (double)BitConverter.ToUInt16(new byte[] { recvDatas[nEB + 6], recvDatas[nEB + 5] }, 0);
                                            break;
                                        case SENSOR_TYPE.MINGQU:
                                            
                                            break;
                                        case SENSOR_TYPE.GUANDAO:
                                            data[0] = (double)BitConverter.ToUInt16(new byte[] { recvDatas[nEB + 6], recvDatas[nEB + 5] }, 0);
                                            data[1] = (double)BitConverter.ToUInt16(new byte[] { recvDatas[nEB + 8], recvDatas[nEB + 7] }, 0);
                                            break;
                                        default:
                                            break;

                                    }
                                    Sensor ss;
                                    Site site;
                                    do 
                                    {
                                        sites.Get(siteNum, out site);
                                        site.Get(sensorNum, out ss);
                                        if (site == null || ss == null)
                                        {
                                            break;
                                        }
                                        //if (ss.formulaDelegate == null)
                                        //{
                                        //    Log.Write("传感器没有编译好的公式");
                                        //    break;
                                        //}
                                        //double saveData = ss.formulaDelegate.Invoke(d);
                                        double dataOrigion = data[0].Value;
                                        double saveData = 0;
                                        double sumData = 0;
                                        double liangcheng = 0;
                                        bool flag = double.TryParse(ss.formula,out liangcheng);  //此处的"公式"废弃不用，使用"量程"替代
                                        liangcheng = flag ? liangcheng : 1;
                                        switch (type)
                                        {
                                            case SENSOR_TYPE.YALI:
                                            case SENSOR_TYPE.WENDU:
                                                saveData = (data[0].Value - 819) / (4096 - 819) * liangcheng;
                                                break;
                                            case SENSOR_TYPE.SHUIWEI:
                                                double tempData = (data[0].Value - 819) / (4096 - 819) * liangcheng;
                                                saveData = ss.wellDepth - ss.ropeDepth + tempData;
                                                break;
                                            case SENSOR_TYPE.MINGQU:
                                                break; 
                                            case SENSOR_TYPE.GUANDAO:
                                                saveData = data[0].Value / 100 * liangcheng;
                                                //DateTime? lastTime = DbServer.GetLastCreateTime(siteNum, sensorNum);
                                                //if (lastTime == null)
                                                //{
                                                //    sumData = 0;
                                                //    File.AppendAllText("d:/log.txt","if lasttime=null sumData=" + sumData) ;
                                                    
                                                //}
                                                //else
                                                //{
                                                //    double substractHours = DateTime.Now.Subtract(lastTime.Value).Seconds / 3600.0;
                                                //    sumData = saveData * substractHours;
                                                //    File.AppendAllText("d:/log.txt", "if lasttime=null else sumData=" + sumData);
                                                //}
                                                
                                                
                                                break;
                                            case SENSOR_TYPE.DIANYA:
                                                saveData = data[0].Value / 100;
                                                break;
                                            default:
                                                break;
                                                
                                        }

                                        DateTime? lastTime = DbServer.GetLastCreateTime(siteNum, sensorNum);
                                        if (lastTime == null)
                                        {
                                            sumData = 0;
                                            File.AppendAllText("d:/log.txt", "if lasttime=null sumData=" + sumData);

                                        }
                                        else
                                        {
                                            double substractHours = DateTime.Now.Subtract(lastTime.Value).Seconds / 3600.0;
                                            sumData = saveData * substractHours;
                                            File.AppendAllText("d:/log.txt", "if lasttime=null else sumData=" + sumData);
                                        }  

                                        //if (type == SENSOR_TYPE.GUANDAO || type == SENSOR_TYPE.MINGQU)
                                        //{
                                        //    if (saveData <= 0.00005)
                                        //    {
                                        //        saveData = 0.0;
                                        //    }
                                        //}

                                        saveData *= Balance.GetParam((int)siteNum, (int)sensorNum);
                                        File.AppendAllText("d:/log.txt","sumData="+sumData.ToString());
                                        //double weight = GetWeight(siteNum, sensorNum);
                                        double weight = 1;
                                        if (weight <= 0.0)
                                            break;
                                        //weight = (type == SENSOR_TYPE.GUANDAO || type == SENSOR_TYPE.MINGQU) ? weight:1;
                                        
                                        saveData = Math.Round(saveData, 3);
                                        saveData = saveData <= 0 ? 0 : saveData;
                                        sumData = Math.Round(sumData, 3);
                                        
                                        DbServer.SaveAsync(siteNum, sensorNum, dataOrigion, saveData, sumData, weight, DateTime.Now);
                                        //                                     if (siteNum == 1 && sensorNum == 2)
                                        //                                     {
                                        //                                         DbServer.SaveAsync(1, 3, saveData / 100, 1, DateTime.Now);
                                        //                                     }
                                    } while (false);
                                   
                                }
                                else
                                {
                                    //数据格式错误
                                }
                                nParsed = i;
                                s = STATUS.R_UNKOWN;
                                nEB = 0;
                            }
                        }
                        break;
                }
            }
            recvDatas.RemoveRange(0, nParsed);
        }

        /// <summary>
        /// btye数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexString(byte[] bytes, int length)
        {
            string str = string.Empty;
            if (bytes != null)
            {
                for (int i = 0; i < length; i++)
                {
                    str += bytes[i].ToString("X2");
                }
            }
            return str;
        }

        private static double GetWeight(uint site, uint sensor)
        {
            DateTime now = DateTime.Now;
            DateTime last = now;
            double weight = 1.0;
            uint key = (site << 16) | sensor;
            if (lastTime.ContainsKey(key))
            {
                weight = (now - (DateTime)lastTime[key]).TotalMilliseconds / 1000;
                if (weight >30.0)
                {
                    weight = 30.0;
                    lastTime[key] = now;
                }
                else if (weight < 5.0)//每个传感器最多12秒保存一次数据
                {
                    weight = 0.0;
                }
                else
                {
                    lastTime[key] = now;
                }
            } 
            else
            {
                weight = 1.0;
                lastTime.Add(key, now);
            }
            return weight;
        }

//         public static double GetShowData(SENSOR_TYPE type, double data)
//         {
//             double d = 0.0;
//             switch (type)
//             {
//                 case SENSOR_TYPE.LIULIANG:
//                     {
//                         d = ((data -770.0) / (3977.0-770.0)) * 5.0;
//                         if (d<0.2)
//                         {
//                             d = 0;
//                         }
//                     }
//                     break;
//                 case SENSOR_TYPE.SHUIWEI:
//                     {
//                         d = ((data * 2.5) / 4096 - 0.76) * 2.5;
// 
//                     }
//                     break;
//                 case SENSOR_TYPE.WUWEI:
//                     {
//                         d = ((data - 820) / (4096 - 820)) * 100;
//                     }
//                     break;
//                 case SENSOR_TYPE.WENDU:
//                     {
//                         d = ((data - 820) / (4096 - 820)) * 100;
// 
//                     }
//                     break;
//                 case SENSOR_TYPE.YALI:
//                     {
//                         d = ((data * 2.5) / 4096 - 0.76) * 5.0;
// 
//                     }
//                     break;
//                 case SENSOR_TYPE.MINGQUXIAO:
//                     {
//                         d = ((float)(data - 1170) / (3500 - 1170)) * 1;
// 
//                         if (d < 0)
//                         {
//                             d = 0;
//                         }
//                         d = Math.Pow(d, 1.55);
//                         d = 0.36 * d * 3600;
//                     }
//                     break;
//                 case SENSOR_TYPE.MINGQUDA:
//                     {
//                         d = ((float)(data - 1170) / (3500 - 1170)) * 1;
// 
//                         if (d < 0)
//                         {
//                             d = 0;
//                         }
//                         d = Math.Pow(d, 1.54);
//                         d = 0.3812 * d * 3600;
// 
//                     }
//                     break;
//             }
//             return d <0.0?0.0:d;
//         }


        public enum STATUS
        {
            R_EB = 0,
            R_90=1,
            R_SITE=2,
            R_SENSORNUM=3,
            R_SENSORTYPE=4,
            R_DATA1=5,
            R_DATA2=6,
            R_DATA3=7,
            R_DATA4=8,
            R_AA=9,
            R_UNKOWN=10,
        }

        private static Hashtable lastTime = Hashtable.Synchronized(new Hashtable());
    }
}
