using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO.Ports;
using ComConfig;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using ShuiwenLib;

namespace Shuiwen
{
    class SitesComm
    {
        public SitesComm(Sites sites)
        {
            this.sites = sites;
        }
        public void Start()
        {
            Stop();
//             Site ss;
//             int idx = 0;
//             while (sites.GetAt(idx++,out ss))
//             {
//                 SiteComm sc;
//                 if (conns.TryGetValue(ss.num, out sc))
//                 {
//                     if (sc != null )
//                     {
//                         sc.Stop();
//                     }
//                     conns.Remove(ss.num);
//                 }
//            
//                 sc = new DbComm(ss.num);
//                 conns.Add(ss.num, sc);
//                 
//                 sc.Start();
//             }
             SiteComm sc;
             if (conns.TryGetValue(0, out sc))
             {
                 if (sc != null )
                 {
                     sc.Stop();
                 }
                conns.Remove(0);
             }
             sc = new DbComm(sites);
             conns.Add(0, sc);
             
             sc.Start();
        }

        public void Stop()
        {
            foreach (KeyValuePair<uint,SiteComm> kvp in conns)
            {
                kvp.Value.Stop();
            }
        }

        public bool Start(uint num)
        {
            try
            {
                conns[num].Start();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool Stop(uint num)
        {
            try
            {
                conns[num].Stop();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        private Sites sites;
        public Dictionary<uint, SiteComm> conns = new Dictionary<uint, SiteComm>();
    }

    interface SiteComm
    {
        void Start();
        void Stop();
    }

    public enum STATE
    {
        CONNECTED = 0, 
        DISCONN = 1,
        CONNECTING = 2,
    }

    public class DbComm:SiteComm
    {
        public DbComm(Sites sites)
        {
            this.sites = sites;
        }
        public void Start()
        {
            Site st;
            int i=0;
            while (sites.GetAt(i++, out st))
            {
                UiUpdater.StateUpdate(st.num, STATE.CONNECTING);
            }
            
            recvThread = new Thread(new ThreadStart(RecvProc));
            recvThread.Start();
        }

        public void RecvProc()
        {
            while (!bStop)
            {
                try
                {
                    Site st;
                    Sensor ss;
                    int i=0;
                    while (sites.GetAt(i++, out st))
                    {
                        int j = 0;
                        while (st.GetAt(j++,out ss))
                        {
                            double data_origion;
                            double data;
                            DateTime time;
                            if (ss.type == SENSOR_TYPE.GUANDAO || ss.type == SENSOR_TYPE.MINGQU)
                            {
                                double sum;
                                //if (DbClient.QueryRecentSum(st.num,ss.num,out data,out time))
                                if (DbClient.QueryRecentAvg(st.num, ss.num, 10,out data_origion, out data, out time)
                                    && DbClient.QueryRecentSum(st.num,ss.num,out sum,out time))
                                {
                                    UiUpdater.DataInQueue(st.num, ss.num,data_origion, data,data * 3600,sum, time);
                                }
                            }
                            else if (DbClient.QueryRecentAvg(st.num, ss.num, 10,out data_origion,out data, out time))
                            {
                                UiUpdater.DataInQueue(st.num, ss.num, data_origion,data,data,0, time);
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Thread.Sleep(5 * 1000);
                }
                //Thread.Sleep(5*1000);
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            bStop = true;
            if (!recvThread.Join(1000))
            {
                recvThread.Abort();
            }
            bStop = false;
        }
        private bool bStop = false;
        private Thread recvThread; 
        private Sites sites;
    }

 
 
}
