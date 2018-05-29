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
    public partial class SiteStateForm : Form
    {
        public SiteStateForm(Site st)
        {
            InitializeComponent();
            site = st;
            RefreshForms();
            toolStripNum.Text = "编号:" + st.num.ToString();
            toolStripName.Text = "名称:" + st.name;
            string v = UiChange.Get(GetKey());
            if (v.Length>0)
            {
                colCount = Convert.ToInt32(v);
            }
            else
            {
                colCount = 1;
            }
        }

        public void RefreshForms()
        {
            Sensor ss;
            int i = 0;
            while (site.GetAt(i++, out ss))
            {
                SensorMsgForm smf = new SensorMsgForm(site.num,ss);
                smfs.Add(ss.num, smf);
                smf.TopLevel = false;
                flowLayoutPanel2.Controls.Add(smf);
                smf.Show();
            }
            uu = new UiUpdater(site.num, UptateState);
        }

        private delegate void InvokeCallback(STATE state);
        public void UptateState(STATE state)
        {
            if (this.InvokeRequired)
            {
                InvokeCallback msgCallback  =   new  InvokeCallback(UptateState);      
                Invoke(msgCallback);
            }
            else
            {
                string text="";
                switch (state)
                {
                case STATE.CONNECTED:
                        text = "已连接";
            	    break;
                case STATE.CONNECTING:
                        text = "连接中";
                    break;
                case STATE.DISCONN:
                        text = "未连接";
                    break;
                }
                this.toolStripTextBox1.Text = text;
            }
        }


        public void UpdateSensor(uint sNum,double data)
        {
            SensorMsgForm smf;
            if (smfs.TryGetValue(sNum,out smf))
            {
                smf.AddDataData(data, DateTime.Now);
            }
            ResizeSensors(colCount);
        }

        public void ResizeSensors(int colCount)
        {
            if (smfs.Count<=0)
            {
                return;
            }
            int sWidth = (flowLayoutPanel2.Width / colCount) - 10;
            int sHeight = (flowLayoutPanel2.Height / (smfs.Count / colCount + smfs.Count%colCount)) - 10;
            foreach (KeyValuePair<uint,SensorMsgForm> smf in smfs)
            {
                smf.Value.Size =  new Size(sWidth,sHeight);
            }
        }

        private int colCount = 1;
        private Site site;
        private Dictionary<uint, SensorMsgForm> smfs = new Dictionary<uint, SensorMsgForm>();
        private UiUpdater uu;
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            colCount = toolStripComboBox1.SelectedIndex + 1;
            UiChange.Set(GetKey(), colCount.ToString());
            ResizeSensors(colCount);
        }

        private void SiteStateForm_SizeChanged(object sender, EventArgs e)
        {
            ResizeSensors(colCount);
        }

        private string GetKey()
        {
            return "SiteStateForm_"+site.num.ToString() + site.name + site.Count();
        }

        private void SiteStateForm_Load(object sender, EventArgs e)
        {
            ResizeSensors(colCount);
        }
    }
}
