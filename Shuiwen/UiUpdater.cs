using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Shuiwen
{
    class UiUpdater
    {
        public UiUpdater(uint siteNum, uint sensorNum,DataUpdateFunc func)
        {
            Reset(siteNum, sensorNum, func);
        }

        public UiUpdater(uint siteNum, StateUpdateFunc func)
        {
            Reset(siteNum, func);
        }

        public UiUpdater(DataComeFunc dcf)
        {
            Reset(dcf);
        }

        public void Reset(uint siteNum, uint sensorNum, DataUpdateFunc func)
        {
            UnSet();
            this.siteNum = siteNum;
            this.sensorNum = sensorNum;
            duf = func;
            if (!sensorUi.ContainsKey(siteNum))
            {
                sensorUi.Add(siteNum, new Dictionary<uint, List<DataUpdateFunc>>());
            }
            if (!sensorUi[siteNum].ContainsKey(sensorNum))
            {
                sensorUi[siteNum].Add(sensorNum, new List<DataUpdateFunc>());
            }
            sensorUi[siteNum][sensorNum].Add(func);
            duf = func;
        }

      

        public void Reset(uint siteNum, StateUpdateFunc func)
        {
            UnSet();
            this.siteNum = siteNum;
            suf = func;
            if (!siteUi.ContainsKey(siteNum))
            {
                siteUi.Add(siteNum, new List<StateUpdateFunc>());
            }
            siteUi[siteNum].Add(func);
            suf = func;
        }

        public void Reset(DataComeFunc dcf)
        {
            UnSet();
            this.dcf = dcf;
            dataComeFunc.Add(dcf);
        }

        ~UiUpdater()
        {
            UnSet();
        }

        public void UnSet()
        {
            if (duf != null)
            {
                sensorUi[siteNum][sensorNum].Remove(duf);
            }
            if (suf != null)
            {
                siteUi[siteNum].Remove(suf);
            }
            if (dcf != null)
            {
                dataComeFunc.Remove(dcf);
            }
        }

        public static void Clear()
        {
            sensorUi.Clear();
            siteUi.Clear();
        }

        public static void DataUpdate()
        {
            try
            {
                while (qDatas.Count > 0)
                {
                    SensorData sd = (SensorData)qDatas.Dequeue();
                    bool errorData = false;
                    foreach(DataComeFunc dcf in dataComeFunc)
                    {
                        if (!dcf.Invoke(sd.siteNum, sd.sensorNum,sd.dataOrigion,sd.data, sd.dataAvg,sd.dataSum,sd.time))
                        {
                            errorData = true;
                            break;
                        }
                    }
                    if (errorData)
                    {
                        continue;
                    }
                    foreach (DataUpdateFunc func in sensorUi[sd.siteNum][sd.sensorNum])
                    {
                        try
                        {
                            func.Invoke(sd.dataOrigion,sd.data,sd.dataAvg,sd.dataSum,sd.time);
                        }
                        catch (Exception ex)
                        {
                           // Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
           
        }

        public static void StateUpdate(uint siteNum,STATE state)
        {
            try
            {
                foreach (StateUpdateFunc func in siteUi[siteNum])
                {
                    func.Invoke(state);
                }
            }
            catch (System.Exception ex)
            {
            
            }
        }

        public static void DataInQueue(  uint siteNum,
            uint sensorNum,
            double dataOrigion,
            double data,
            double dataAvg,
            double dataSum,
            DateTime time)
        {
            qDatas.Enqueue(new SensorData() { siteNum = siteNum, sensorNum = sensorNum,dataOrigion = dataOrigion,data = data,dataAvg = dataAvg ,dataSum = dataSum,time = time});
//             try
//             {
//                 DbInterface.Save(siteNum, sensorNum, data);
//             }
//             catch (System.Exception ex)
//             {
//                 MessageBox.Show(ex.Message);
//             }
        }

        struct SensorData
        {
            public uint siteNum;
            public uint sensorNum;
            public double dataOrigion;
            public double data;
            public double dataAvg;
            public double dataSum;
            public DateTime time;
        }

        private DataUpdateFunc duf = null;
        private StateUpdateFunc suf = null;
        private DataComeFunc dcf = null;
        private uint siteNum = uint.MaxValue;
        private uint sensorNum = uint.MaxValue;
        static Dictionary<uint, Dictionary<uint,List<DataUpdateFunc>>> sensorUi = new Dictionary<uint, Dictionary<uint,List<DataUpdateFunc>>>();
        static Dictionary<uint, List<StateUpdateFunc>> siteUi = new Dictionary<uint, List<StateUpdateFunc>>();
        static List<DataComeFunc> dataComeFunc = new List<DataComeFunc>();
        static Queue qDatas = Queue.Synchronized(new Queue());
    }
    delegate bool DataComeFunc(uint siteNum, uint sensorNum, double dataOrigion,double data,double dataAvg,double dataSum,DateTime time);
    delegate void DataUpdateFunc(double dataOrigion,double data,double dataAvg,double dataSum,DateTime time);
    delegate void StateUpdateFunc(STATE state);
}
