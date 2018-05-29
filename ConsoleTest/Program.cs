using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            TcpListener tl = new TcpListener(12345);
            tl.Start();
            tl.BeginAcceptTcpClient(AcceptCallback, tl);
            while (true)
            {
                Thread.Sleep(1);
            }
        }

        static int siteCount = 1;
        byte[] data = new byte[] { };

        static void AcceptCallback(IAsyncResult ar)
        {
            TcpListener tl = (TcpListener)ar.AsyncState;
            TcpClient tc = tl.EndAcceptTcpClient(ar);
            Console.WriteLine("收到连接");
            for (int i = 1; i <= 5;++i )
            {
                byte[] rByte = new byte[2];
               
                r.NextBytes(rByte);
                rByte[1] = 0x01;
                rByte[0] &= 0xFF;
                byte[] data = new byte[] { 0xEB, 0x90, (byte)siteCount, (byte)i, 0x1, rByte[0], rByte[1], 0xAA };
                tc.Client.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, new object[]{tc,i});
            }
          
            tl.BeginAcceptTcpClient(AcceptCallback, tl);
        }

        static void SendCallback(IAsyncResult ar)
        {
            TcpClient tc = (TcpClient)((object[])ar.AsyncState)[0];
            int i = (int)((object[])ar.AsyncState)[1];
            Console.WriteLine("发送" + i.ToString());
            SocketError serr;
            tc.Client.EndSend(ar, out serr);
            if (SocketError.Success == serr)
            {
                Thread.Sleep(1000);
                byte[] rByte = new byte[2];
                r.NextBytes(rByte);
                rByte[1] = 0x01;
                rByte[0] &= 0xFF;
                byte[] data = new byte[] { 0xEB, 0x90, (byte)siteCount, (byte)i, 0x1, rByte[0], rByte[1], 0xAA };
                tc.Client.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, ar.AsyncState);
            } 
            else
            {
            }

        }
    }
}
