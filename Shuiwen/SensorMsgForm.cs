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
    public partial class SensorMsgForm : Form
    {
        public SensorMsgForm(uint siteNum,Sensor ss)
        {
            InitializeComponent();
            sensor = ss;
//             slf = new SensorLineForm();
//             slf.TopLevel = false;
//             splitContainer1.Panel2.Controls.Add(slf);
//             slf.Dock = DockStyle.Fill;                                                   
//             slf.Show();
            uu = new UiUpdater(siteNum, ss.num, UpdateUI);
            textBoxSensorNum.Text = ss.num.ToString();
            textBoxSensorName.Text = ss.name;
            textBoxType.Text = Sensor.items[ss.type];
        }

        private void SensorMsgForm_Load(object sender, EventArgs e)
        {
  
        }


        public void AddDataData(double data,DateTime date)
        {
            //slf.AddData(data,date);
        }

        public void UpdateUI(double dataOrigion,double data,double dataAvg,double dataSum, DateTime time)
        {
            double d = Parser.GetShowData(sensor.type,dataOrigion,data,dataAvg,dataSum);
            AddDataData(d,time);
            this.textBoxData.Text = Math.Round(d, 2).ToString() + Parser.GetSymbol(sensor.type);
            this.textBoxLastTime.Text = DateTime.Now.ToString(); 
            if (d> (double)sensor.alarmHigh || d< (double)sensor.alarmLow)
            {
                textBoxData.ForeColor = Color.Red;
            }
            else
            {
                textBoxData.ForeColor = Color.Lime;
            }
        }

        private UiUpdater uu;
        private Sensor sensor;
       // private SensorLineForm slf;
    }
}
