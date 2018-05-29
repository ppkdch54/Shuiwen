using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;

namespace ShuiwenService
{
    class Balance
    {
        public static double GetParam(int site, int sensor)
        {
            return _onlyYou._GetParam(site, sensor);
        }
        private Balance()
        {
            Load();
            tmBalance.Interval = 60 * 1000;
            tmBalance.Elapsed += new ElapsedEventHandler(tmBalance_Elapsed);
            tmBalance.Start();
        }

        private void tmBalance_Elapsed(object sender, ElapsedEventArgs e)
        {
            CalcBalanceParams();
        }

        private void Load()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Balance.txt";
            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string strFile = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            Log.Write(strFile);
            string[] fomulas= strFile.Split('\n');
            foreach (var f in fomulas)
            {
                f.Trim();
                f.Trim('\r');
                f.Trim(' ');
                string[] pair = f.Split('=');
                if (pair.Length == 2)
                {
                    string left = pair[0];
                    string right = pair[1];
                    balanceFormulas.Add(left, right);
                }
            }
        }
        //以左边为准，校正右边
        private void CalcBalanceParams()
        {
            foreach (var kv in balanceFormulas)
            {
                double sumLeft = 0;
                double sumRight = 0;
                string[] strSiteSensors = kv.Key.Split(',');
                foreach (var sss in strSiteSensors)
                {
                    string[] ss = sss.Split(':');
                    if (ss.Length == 2)
                    {
                        string strSite = ss[0];
                        string strSensor = ss[1];

                        int site = Convert.ToInt32(strSite);
                        int sensor = Convert.ToInt32(strSensor);

                        sumLeft += DbServer.Sum(site, sensor, DateTime.Now.AddDays(-10), DateTime.Now,3);
                    }
                }
                strSiteSensors = kv.Value.Split(',');
                foreach (var sss in strSiteSensors)
                {
                    string[] ss = sss.Split(':');
                    if (ss.Length == 2)
                    {
                        string strSite = ss[0];
                        string strSensor = ss[1];

                        int site = Convert.ToInt32(strSite);
                        int sensor = Convert.ToInt32(strSensor);

                        sumRight += DbServer.Sum(site, sensor, DateTime.Now.AddDays(-10), DateTime.Now,3);
                    }
                }

                if (sumLeft > 0 && sumRight > 0)
                {
                    double muti = (sumLeft / sumRight)*(sumLeft/sumRight);
                    if (muti >10 && muti <0.1)
                    {
                        muti = 1;
                    }
                    foreach (var sss in strSiteSensors)
                    {
                        string[] ss = sss.Split(':');
                        if (ss.Length == 2)
                        {
                            string strSite = ss[0];
                            string strSensor = ss[1];

                            int site = Convert.ToInt32(strSite);
                            int sensor = Convert.ToInt32(strSensor);
                            lock (balanceParams)
                            {
                                if (!balanceParams.ContainsKey(site))
                                {
                                    balanceParams.Add(site, new Dictionary<int, double>());
                                }
                                if (!balanceParams[site].ContainsKey(sensor))
                                {
                                    balanceParams[site].Add(sensor, 1.0);
                                }
                                balanceParams[site][sensor] = muti;
                                Log.Write(string.Format("MUTI:{0}:{1}=={2}",site,sensor,muti));
                            }
                        }
                    }
                }
            }
        }
        private double _GetParam(int site, int sensor)
        {
            lock (balanceParams)
            {
                try
                {
                    return balanceParams[site][sensor];
                }
                catch 
                {
                    return 1;
                }
            }
        }
        private static Balance _onlyYou = new Balance();
        private Timer tmBalance = new Timer();
        private Dictionary<int, Dictionary<int, double>> balanceParams = new Dictionary<int, Dictionary<int, double>>();
        private Dictionary<string, string> balanceFormulas = new Dictionary<string, string>();
    }
}
