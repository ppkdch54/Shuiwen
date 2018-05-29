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
    public partial class AbnormalForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public AbnormalForm(Sites sites)
        {
            InitializeComponent();
            this.sites = sites;
        }

        private void AbnormalForm_Load(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            uus.Clear();
            Site st;
            int i = 0;
            while (sites.GetAt(i++,out st))
            {
                Sensor ss;
                int j = 0;
                while (st.GetAt(j++,out ss))
                {
                    DataUpdate du = new DataUpdate(st, ss, listView1, toolStripEditMax);
                    uus.Add(du);
                }
            }
        }

        private class DataUpdate
        {
            public DataUpdate(Site site, Sensor sensor,ListView listView,ToolStripTextBox toolStripEditMax)
            {
                st = site;
                ss = sensor;
                this.listView = listView;
                max = toolStripEditMax;
                uu = new UiUpdater(site.num, sensor.num, DataCome);
            }
            public void DataCome(double dataOrigion,double data,double dataAvg,double dataSum,DateTime time)
            {
                double d = Parser.GetShowData(ss.type,dataOrigion,data, dataAvg,dataSum);
                if (d > (double)ss.alarmHigh || d < (double)ss.alarmLow)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = st.name;
                    lvi.SubItems.Add(st.num.ToString());
                    lvi.SubItems.Add(ss.name);
                    lvi.SubItems.Add(ss.num.ToString());
                    lvi.SubItems.Add(Sensor.items[ss.type]);
                    lvi.SubItems.Add(Math.Round(d, 2).ToString() + Parser.GetSymbol(ss.type));
                    lvi.SubItems.Add(ss.alarmHigh >= decimal.MaxValue ? "" : ss.alarmHigh.ToString());
                    lvi.SubItems.Add(ss.alarmLow <= decimal.MinValue ? "" : ss.alarmLow.ToString());
                    lvi.SubItems.Add(time.ToString("yyyy年MM月dd日 HH:mm:ss"));

                    listView.Items.Add(lvi);
                    int maxCount = Convert.ToInt32(max.Text);
                    while (listView.Items.Count > maxCount)
                    {
                        listView.Items.RemoveAt(0);
                    }
                }
            }
            private Site st;
            private Sensor ss;
            private ListView listView;
            private ToolStripTextBox max;
            public UiUpdater uu;
        }

        private List<DataUpdate> uus = new List<DataUpdate>();
        private Sites sites;

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}
