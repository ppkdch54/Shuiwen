using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ShuiwenLib;
using System.Net;

namespace ShuiwenService
{
    class WorkProc
    {
        public static void Work()
        {
            bWork = true;
            workThread = new Thread(Proc);
            workThread.Start();
        }

        public static void Stop()
        {
            bWork = false;
            workThread.Join();
        }

        public static void Proc()
        {
            Sites sites;
            Sites.Load(out sites, new DbServer());
            DbServer.MarkNotNew();
            //编译公式
            Site st;
            Sensor ss;
            int i = 0;
            
            while(sites.GetAt(i++,out st))
            {
                int j = 0;
                while (st.GetAt(j++, out ss))
                {
                    string err = "";
                    ss.formulaDelegate = FormulaBuilder.TryCompile(ss.formula, out err);
                    if (err != "")
                    {
                        Log.Write("编译公式出错：" + err);
                        continue;
                    }
                }
            }
            //获取本机所有IP
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            //获取服务器里所有的服务器IP
            Server server = DbServer.ReadServerConfig();
            bool exists = ips.Any(s => s.ToString() == server.IP);
            if (exists)
            {
                //说明是本机
                if (server.IsEnable)
                {
                    serverThread = new Thread(delegate() 
                        {
                            TcpServerComm tsc = new TcpServerComm(server, sites);
                            tsc.Start();
                        });
                    serverThread.Start();

                }
            }
            

            SitesComm sc = new SitesComm(sites);
            sc.Start();

            while (bWork)
            {
                //Thread.Sleep(10000);
                Thread.Sleep(1000);
                
                if (DbServer.IsNewConfig())
                {
                    sc.Stop();
                    Sites.Load(out sites, new DbServer());
                    sc.sites = sites;
                    DbServer.MarkNotNew();
                    sc.Start();
                }
            }
            sc.Stop();
            
        }

        private static bool bWork = true;
        private static Thread workThread;
        private static Thread serverThread;
    }
}
