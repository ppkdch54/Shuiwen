using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shuiwen
{
    public partial class SensorLineForm : Form
    {
        public SensorLineForm()
        {
            InitializeComponent();
        }

//         public void AddData(double data)
//         {
//             chartSensor.Series[0].Points.AddXY(DateTime.Now,data);
//             chartSensor.Refresh();
//         }

        public void AddData(double data, DateTime time)
        {
            chartSensor.Series[0].Points.AddXY(time, data);
            if (chartSensor.Series[0].Points.Count>10000)
            {
                chartSensor.Series[0].Points.RemoveAt(0);
            }
        }

        public void Reset()
        {
            chartSensor.Series[0].Points.Clear();
        }

        private void chartSensor_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chartSensor.Series[0].Points.AddXY(DateTime.Now, 20.12345);
        }
        delegate void AddDataDelegate(double data, DateTime time);
    }

}
