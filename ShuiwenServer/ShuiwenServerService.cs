using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ShuiwenServer
{
    public partial class ShuiwenServerService : ServiceBase
    {
        Socket socket, newSocket;
        public ShuiwenServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch(); 
            //读取配置
            Config config = Config.LoadConfig();
            config.ServerPort = 5000;
            config.ServerIP = "192.168.1.55";
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(config.ServerIP), config.ServerPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start(1000);
            listener.BeginAcceptTcpClient(new AsyncCallback(AcceptConnect), listener);
        }

        void AcceptConnect(IAsyncResult ar)
        {


        }

        void AcceptInfo(object o)
        {
            Socket socket = o as Socket;
            while (true)
            {

                //通信用socket

                try
                {

                    //创建通信用的Socket

                    Socket tSocket = socket.Accept();

                    string point = tSocket.RemoteEndPoint.ToString();

                    //IPEndPoint endPoint = (IPEndPoint)client.RemoteEndPoint;

                    //string me = Dns.GetHostName();//得到本机名称

                    //MessageBox.Show(me);


                    //接收消息

                    Thread th = new Thread(ReceiveMsg);

                    th.IsBackground = true;

                    th.Start(tSocket);

                }

                catch (Exception ex)
                {

                    break;

                }

            }

        }

        void OnClientConnected()
        {

        }
        //接收消息

        void ReceiveMsg(object o)
        {

            Socket client = o as Socket;

            while (true)
            {

                //接收客户端发送过来的数据

                try
                {

                    //定义byte数组存放从客户端接收过来的数据

                    byte[] buffer = new byte[1024 * 1024];

                    //将接收过来的数据放到buffer中，并返回实际接受数据的长度

                    int n = client.Receive(buffer);

                    //将字节转换成字符串

                    //string words = Encoding.ASCII.GetString(buffer, 0, n);
                    string words = ByteToHexString(buffer, n);
                    System.IO.File.AppendAllText("d:\\data.txt", words);

                }

                catch (Exception ex)
                {

                    break;

                }

            }

        }

        /// <summary>
        /// btye数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ByteToHexString(byte[] bytes, int length)
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

        protected override void OnStop()
        {
        }
    }
}
