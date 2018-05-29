using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ShuiwenService
{
    class MyTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "log.Txt", message);
        }

        public override void WriteLine(string message)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\"+DateTime.Now.ToString("yyyyMMdd") + "log.Txt", 
                DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + message + Environment.NewLine);
        }
    }
    class Log
    {
        public static void Write(string l)
        {
            if (_log == null)
            {
                _log = new Log();
            }
            Trace.TraceWarning(l);
        }

        private Log()
        {
            Trace.Listeners.Clear();  //清除系统监听器 (就是输出到Console的那个)
            Trace.Listeners.Add(new MyTraceListener()); //添加MyTraceListener实例
        }

        private static Log _log ;
    }
}
