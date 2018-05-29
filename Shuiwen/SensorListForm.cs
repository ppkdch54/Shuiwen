using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShuiwenLib;

namespace Shuiwen
{
    public partial class SensorListForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public SensorListForm(Sites sites)
        {
            InitializeComponent();

            this.sites = sites;
        }

        private void SensorListForm_Load(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            listViewData.Items.Clear();
            listViewData.Groups.Clear();
            Site st;
            int idx=0;
            while(sites.GetAt(idx++,out st))
            {
                ListViewGroup lvg = new ListViewGroup();
                lvg.Tag = st;
                lvg.Header = st.name + "(" + st.num.ToString() + ")";
                lvg.Name = st.num.ToString();
                listViewData.Groups.Add(lvg);
                Sensor ss;
                int j = 0;
                while (st.GetAt(j++, out ss))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "";
                    lvi.SubItems.Add(ss.name);
                    lvi.SubItems.Add(Sensor.items[ss.type]);
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add(ss.alarmHigh == Decimal.MaxValue ? "" : ss.alarmHigh.ToString());
                    lvi.SubItems.Add(ss.alarmLow == Decimal.MinValue ? "" : ss.alarmLow.ToString());
                    lvi.SubItems.Add("");
                    lvg.Items.Add(lvi);
                    ListViewItem newLvi = listViewData.Items.Add(lvi);
                    SensorDataHandler sdh = new SensorDataHandler(st.num, ss, newLvi);
                    UiUpdater uu = new UiUpdater(st.num, ss.num, sdh.UpdateData);
                    sdh.uu = uu;
                }
            }
        }

        private class SensorDataHandler
        {
            public SensorDataHandler(uint siteNum, Sensor sensor, ListViewItem lvi)
            {
                this.siteNum = siteNum;
                this.lvi = lvi;
                this.sensor = sensor;
            }

            public void UpdateData(double dataOrigion,double data,double dataAvg,double dataSum, DateTime time)
            {
                lvi.SubItems[3].Text = time.ToString("yyyy年MM月dd日 HH:mm:ss");
                double d = dataAvg;
                if (sensor.type == SENSOR_TYPE.WUWEI)
                {
                    double value  = Math.Round(d, 3);
                    lvi.Text = Math.Round((sensor.sensorDeep - value), 3).ToString() + "m + " + value.ToString() + Parser.GetSymbol(sensor.type);
                }
                else if (sensor.type == SENSOR_TYPE.GUANDAO || sensor.type == SENSOR_TYPE.MINGQU)
                {
                    lvi.Text = "实时：" + Math.Round(data, 3).ToString() + "㎥/h，当日累计："
                        + Math.Round(dataSum, 3).ToString() + "㎥";

                    //lvi.Text = "流量：" + Math.Round(data, 3, MidpointRounding.AwayFromZero).ToString() + "㎥/h，流速："
                    //    + Math.Round(dataSum, 3, MidpointRounding.AwayFromZero).ToString() + "㎥";
                }
                else
                {
                    lvi.Text = Math.Round(d, 3).ToString() + Parser.GetSymbol(sensor.type);
                    
                }
                lvi.SubItems[6].Text = dataOrigion.ToString();
                if (d > (double)sensor.alarmHigh || d < (double)sensor.alarmLow)
                {
                    lvi.ForeColor = Color.Red;
                    SoundAlarm.Alarm();
                }
                else
                {
                    lvi.ForeColor = Color.Black;
                }
            }
            private uint siteNum;
            private ListViewItem lvi;
            private Sensor sensor;
            public UiUpdater uu;
        }
        private Sites sites;
    }
}
