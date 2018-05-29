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
    public partial class FormSensorIcon : Form
    {
        public FormSensorIcon(uint siteNum,Sensor sensor)
        {
            InitializeComponent();
            this.TopLevel = false;
            this.siteNum = siteNum;
            this.ss = sensor;
        }

        private void FormSensorIcon_Load(object sender, EventArgs e)
        {
            uu = new UiUpdater(siteNum, ss.num, UpdateColor);
        }

        public void SetText(string text)
        {
            label.Text = text;
            this.Size = label.Size;
        }

        public void SetColor(Color clr)
        {
            label.ForeColor = clr;
        }

        public void UpdateColor(double dataOrigion,double data,double dataAvg,double dataSum,DateTime time)
        {
            double d = Parser.GetShowData(ss.type,dataOrigion,data,dataAvg,dataSum);
            if (d >(double)ss.alarmHigh || d<(double)ss.alarmLow)
            {
                SetColor(Color.Red);
            }
            else
            {
                SetColor(Color.Green);
            }
        }

        private uint siteNum;
        private Sensor ss;
        private UiUpdater uu;

        private void label_SizeChanged(object sender, EventArgs e)
        {
            this.Width = label.Width;
            this.Height = label.Height;
            this.Validate();
        }
    }
}
