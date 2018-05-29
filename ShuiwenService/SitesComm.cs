using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO.Ports;
using ComConfig;
using ShuiwenLib;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace ShuiwenService
{
    public class SitesComm
    {
        public SitesComm(Sites sites)
        {
            this.sites = sites;
        }
        public void Start()
        {
            Stop();
            Site ss;
            int idx = 0;
            while (sites.GetAt(idx++,out ss))
            {
                SiteComm sc;
                if (conns.TryGetValue(ss.num, out sc))
                {
                    if (sc != null )
                    {
                        sc.Stop();
                    }
                    conns.Remove(ss.num);
                }
                if (ss.commType == Site.CommType.Tcp)
                {
                    sc = new TcpComm(ss.num, ss.ip, ss.port, sites);
                    conns.Add(ss.num, sc);
                    sc.Start();
                }  
            }
            smsComm = new SmsComm(sites);
            smsComm.Start();
        }

        public void Stop()
        {
            foreach (KeyValuePair<uint,SiteComm> kvp in conns)
            {
                kvp.Value.Stop();
            }
            if (smsComm != null)
            {
                smsComm.Stop();
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
        public Sites sites;
        public SmsComm smsComm;
        public Dictionary<uint, SiteComm> conns = new Dictionary<uint, SiteComm>();
    }

    public interface SiteComm
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
    /// <summary>
    /// 串口方式通信类
    /// </summary>
    public class SerialComm:SiteComm
    {
        public SerialComm(uint num,ComSetting comSetting,Sites sites)
        {
            this.siteNum = num;
            this.comSetting = comSetting;
            this.sites = sites;
        }
        public void Start()
        {
            recvThread = new Thread(new ThreadStart(RecvProc));
            recvThread.Start();
        }

        public void RecvProc()
        {
            SerialPort sp; 
RETRY:      
            try
            {
                sp = new SerialPort(comSetting.com, comSetting.baudRate, comSetting.parity, comSetting.dataBits, comSetting.stopBits);
                sp.Open();
            }
            catch (System.Exception ex)
            {
                if (!bStop)
                {
                    goto RETRY;
                }
                else
                {
                    throw ex;
                }
            }

            while (!bStop)
            {
                try
                {
                    int count = sp.Read(recvBuff, 0, recvBuff.Length);
                    if (count > 0)
                    {
                        recvData.AddRange(recvBuff.Take(count));
                        Parser.Parse(recvData, siteNum,sites);
                    }
                }
                catch (System.Exception ex)
                {
                    goto RETRY;
                }
              
            }
            sp.Close();
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
        private ComSetting comSetting;
        private uint siteNum;
        public List<byte> recvData = new List<byte>();
        private byte[] recvBuff = new byte[128];
        private bool bStop = false;
        Sites sites;
        Thread recvThread;
    }
    /// <summary>
    /// 网口方式通信类
    /// </summary>
    public class TcpComm:SiteComm
    {
        public TcpComm(uint num, string ip, uint port, Sites sites)
        {
            this.siteNum = num;
            this.ip = ip;
            this.port = port;
            this.sites = sites;
        }

        private Sites sites;

        public void Start()
        {
//             if (tcp != null)
//             {
//                 tcp.Close();
//             }
//             tcp = new TcpClient();
//             tcp.BeginConnect(ip, (int)port, new AsyncCallback(ConnectCallback), tcp);
            recvThread = new Thread(new ThreadStart(RecvProc));
            recvThread.Start();
        }

        public void Stop()
        {
//             if (tcp.Connected)
//             {
//                 //tcp.Close();
//                 //tcp.Client.BeginDisconnect(true, new AsyncCallback(ConnectCallback), tcp);
//                 tcp.Client.Disconnect(true);
//             }
//             if (tcp != null)
//             {
//                 tcp.Close();
//             }
            bStop = true;
            if (!recvThread.Join(1000))
            {
                recvThread.Abort();
            }
            bStop = false;
        }

        public void ConnectCallback(IAsyncResult ar)
        {
            TcpClient t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    t.EndConnect(ar);
                    //t.GetStream().BeginRead(recvBuff, 0, 128, RecvCallback, tcp);
                    t.GetStream().Read(recvBuff, 0, 128);
                }
                else
                {
                    t.EndConnect(ar);
                }

            }
            catch (Exception se)
            {
                t.BeginConnect(ip, (int)port, new AsyncCallback(ConnectCallback), t);
            }
        }

        public void RecvCallback(IAsyncResult ar)
        {
            //主动断开时
            if (!tcp.Connected)
                return;
            int numberOfBytesRead;
            
            try
            {
                NetworkStream mas = tcp.GetStream();
                numberOfBytesRead = mas.EndRead(ar);
                if (numberOfBytesRead > 0)
                {
                    recvData.AddRange(recvBuff.Take(numberOfBytesRead));
                    Parser.Parse(recvData, siteNum,sites);
                    mas.BeginRead(recvBuff, 0, recvBuff.Length,
                            RecvCallback, tcp);
                }
//                else
//                      {
//        //                   tcp.Client.Disconnect(true);
//                          tcp.Close();
//        //                   UiUpdater.StateUpdate(siteNum, STATE.DISCONN);
//        //                   Start();
//             //          }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //tcp.Client.Disconnect(true);
  //              tcp.Close();
                Start();
            }
        }

        private void RecvProc()
        {
RETRY:
            tcp = new TcpClient();
            try
            {
                try
                {
                    tcp.Connect(ip, (int)port);
                }
                catch (System.Exception ex)
                {
                    if (!bStop)
                    {
                        tcp.Client.Close();
                        tcp.Close();
                        goto RETRY;                  
                    }
                    else
                    {
                        throw ex;
                    }
                }
                
                NetworkStream ns = tcp.GetStream();
                //ns.ReadTimeout = 60*1000;
                while (!bStop)
                {
                    int count = ns.Read(recvBuff, 0, 128);
                    if (count > 0)
                    {
                        recvData.AddRange(recvBuff.Take(count));
                        Parser.Parse(recvData, siteNum, sites);
                    }
                    else
                    {
                        tcp.Client.Close();
                        ns.Close();
                        tcp.Close();
                        goto RETRY;
                    }
                }
                ns.Close();
                tcp.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (!bStop)
                {
                    goto RETRY;
                }
            }

        }

        private uint siteNum;
        private string ip;
        private uint port;
        private TcpClient tcp = new TcpClient();
        private byte[] recvBuff = new byte[128];
        private bool bStop = false;
        Thread recvThread;

        public List<byte> recvData = new List<byte>();
    }
    /// <summary>
    /// 短信方式通信类
    /// </summary>
    public class SmsComm : SiteComm
    {
        public SmsComm(Sites sites)
        {
            this.sites = sites;
          
        }
        public void Start()
        {
            bWork = true;
            recvThread = new Thread(RecvProc);
            recvThread.Start();
        }
        public void Stop()
        {
            bWork = false;
            if (!recvThread.Join(1000))
            {
                recvThread.Abort();
            }
        }
        private void RecvProc()
        {
            while (bWork)
            {
                Thread.Sleep(10 * 1000);
                //Thread.Sleep(1000);
                try
                {
                    smsRecvProc = new Process();
                    smsRecvProc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\shuiwenSMS.exe";
                    smsRecvProc.StartInfo.UseShellExecute = false;   // 是否使用外壳程序 
                    smsRecvProc.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
                    smsRecvProc.StartInfo.RedirectStandardInput = true;  // 重定向输入流 
                    smsRecvProc.StartInfo.RedirectStandardOutput = true;  //重定向输出流 
                    smsRecvProc.StartInfo.RedirectStandardError = true;  //重定向错误流 
                    smsRecvProc.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);
                    smsRecvProc.EnableRaisingEvents = true;
                    smsRecvProc.Start();
                    smsRecvProc.BeginOutputReadLine();
                    smsRecvProc.WaitForExit();
                    if (smsData.Length >0)
                    {
                        string[] msgs = smsData.Split('|');
                        foreach (string msg in msgs)
                        {
                            msg.Trim();
                            msg.Trim('\r', '\n');
                            string[] contex = msg.Split(new String[]{"----"},StringSplitOptions.None);
                            if (contex.Length == 3 && contex[0] == "MSG")
                            {
                                string[] datas = contex[1].Split(';');
                                foreach (var data in datas)
                                {
                                    Sensor ss;
                                    Site site;
                                    string[] d = data.Split(',');
                                    if (d.Length == 3)
                                    {
                                        uint siteNum = Convert.ToUInt32(d[0]);
                                        uint sensorNum = Convert.ToUInt32(d[1]);
                                        SENSOR_TYPE type = sites.GetType(siteNum, sensorNum);
                                        if (type == SENSOR_TYPE.UNKNOWN)
                                        {
                                            Log.Write("短信收到没有设置的分站和传感器号" + data);
                                        }
                                        sites.Get(siteNum, out site);
                                        site.Get(sensorNum, out ss);
                                        double rawData = Convert.ToDouble(d[2]);
                                     
                                        IFormatProvider ifp = new CultureInfo("zh-CN", true);
                                        DateTime time = DateTime.ParseExact(contex[2], "yyyyMMddHHmmss", ifp);
                                        int weight = (type == SENSOR_TYPE.GUANDAO || type == SENSOR_TYPE.MINGQU ) ? ss.interval : 1;
                                        if (ss.formulaDelegate == null)
                                        {
                                            Log.Write("传感器没有编译好的公式");
                                            continue;
                                        }
                                        double saveData = ss.formulaDelegate.Invoke(rawData);
                                        if (type == SENSOR_TYPE.GUANDAO||type == SENSOR_TYPE.MINGQU)
                                        {
                                            if (saveData <= 0.01)
                                            {
                                                saveData = 0.0;
                                            }
                                        }
                                        DbServer.SaveAsync(siteNum, sensorNum,saveData, saveData,saveData, weight, time);
                                    }
                                }
                            }
                            else
                            {
                                if(msg.Length>0)Log.Write("无法解析的短信" + msg);
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Write(ex.Message);
                }
                finally
                {
                    smsData = "";
                }
            }
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            smsData += e.Data;
        }
        private Sites sites;
        private string smsData = "";
        private Thread recvThread;
        private bool bWork = true;
        private Process smsRecvProc;
    }
    /// <summary>
    /// Tcp服务器模式
    /// </summary>
    public class TcpServerComm : SiteComm
    {
        
        TcpListener listener;
        Sites sites;
        public TcpServerComm(Server server,Sites sites)
        {
            if (server != null)
            {
                this.sites = sites;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(server.IP), server.Port); 
                listener = new TcpListener(ep);
                listener.Start();
            }
        }

        #region SiteComm 成员

        public void Start()
        {
            Thread listenThread = new Thread(delegate()
            {
                listener.BeginAcceptTcpClient(new AsyncCallback(AcceptClientConnect), listener);
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        public void Stop()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }

        #endregion

        private void AcceptClientConnect(IAsyncResult ar)
        {
            TcpListener listener = ar.AsyncState as TcpListener;
            List<byte> recvData = new List<byte>();
            //开启一个新的线程接收传入的新链接
            Thread listenThread = new Thread(delegate()
            {
                listener.BeginAcceptTcpClient(new AsyncCallback(AcceptClientConnect), listener);
            });
            listenThread.IsBackground = true;
            listenThread.Start();
            TcpClient client = listener.EndAcceptTcpClient(ar);
            if (client != null)
            {
                Socket clientSocket = client.Client;
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[128];
                        int count = clientSocket.Receive(buffer);
                        if (count > 0)
                        {
                            recvData.AddRange(buffer.Take(count));
                            Parser.Parse(recvData, 0, sites);
                        }
                        else
                        {
                            recvData.Clear();
                            client.Close();
                        }
                        //string message = ByteToHexString(buffer, count);
                        
                    }
                }
                catch (Exception)
                {
                    if (client != null)
                    {
                        client.Close();
                    }
                }
            }
        }

        /// <summary>
        /// btye数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string ByteToHexString(byte[] bytes, int length)
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
    }
    
}
