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
    public partial class SensorForm : Form
    {
        public SensorForm(Site site,Sensor sensor)
        {
            InitializeComponent();
            this.site = site;
            this.sensor = sensor;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxNum.Text.Length==0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("请输入名称！");
                return;
            }
            if (comboBoxType.SelectedItem == null)
            {
                MessageBox.Show("请选择类型！");
                return;
            }

            uint num = Convert.ToUInt32(textBoxNum.Text);
            Sensor ss;
            if (site.Get(num, out ss))
            {
                if (sensor==null || num != sensor.num)
                {
                    MessageBox.Show("编号已存在！");
                    return;
                }
            }
            if (sensor == null)
            {
                sensor = new Sensor(0, "", SENSOR_TYPE.WENDU,decimal.MinValue,decimal.MaxValue);
            }
            try
            {
                sensor.num = num;
                sensor.name = textBoxName.Text;
                sensor.type = ((KeyValuePair<SENSOR_TYPE, string>)comboBoxType.SelectedItem).Key;
                sensor.alarmLow = textBoxAlarmLow.Text.Length > 0 ? Convert.ToDecimal(textBoxAlarmLow.Text) : decimal.MinValue;
                sensor.alarmHigh = textBoxAlarmHigh.Text.Length > 0 ? Convert.ToDecimal(textBoxAlarmHigh.Text) : decimal.MaxValue;
                sensor.interval = Convert.ToInt32(textBoxInterval.Text);
                sensor.sensorDeep = Convert.ToDouble(textBoxSensorDeep.Text);
                sensor.daogui = comboBoxDaogui.SelectedIndex;

                string ropeDepth = this.txtRopeDepth.Text.Trim();
                string wellDepth = this.txtWellDepth.Text.Trim();
                if (!string.IsNullOrEmpty(ropeDepth))
                {
                    sensor.ropeDepth = Convert.ToDouble(this.txtRopeDepth.Text.Trim());
                }

                if (!string.IsNullOrEmpty(wellDepth))
                {
                    sensor.wellDepth = Convert.ToDouble(this.txtWellDepth.Text.Trim());
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
          
            string err;
            FormulaBuilder.TryCompile(textBoxFormula.Text, out err);
            if (err!="")
            {
                MessageBox.Show("公式编译失败：" + err);
                return;
            }
            sensor.formula = textBoxFormula.Text;

            this.DialogResult = DialogResult.OK;
        }

        public Site site;
        public Sensor sensor;

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SensorForm_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = Sensor.items;
            
            comboBoxType.DisplayMember = "Value";
            comboBoxType.ValueMember = "Key";
            comboBoxType.DataSource = bs;
            if (sensor == null)
            {
                this.Text = "添加传感器";
                //sensor = new Sensor((uint)site.Count(), "", SENSOR_TYPE.LIULIANG);
            }
            else
            {
                this.Text = "修改传感器";
                Sel(sensor);
            }
            //Sel(sensor);
        }

        private void Sel(Sensor sensor)
        {
            textBoxName.Text = sensor.name;
            textBoxNum.Text = sensor.num.ToString();
            comboBoxType.SelectedValue = sensor.type;
            textBoxAlarmHigh.Text = sensor.alarmHigh == decimal.MaxValue ? "" : sensor.alarmHigh.ToString();
            textBoxAlarmLow.Text = sensor.alarmLow == decimal.MinValue ? "" : sensor.alarmLow.ToString();
            textBoxSensorDeep.Text = sensor.sensorDeep.ToString();
            comboBoxDaogui.SelectedIndex = sensor.daogui;
            textBoxFormula.Text = sensor.formula;
            TypeChange(sensor.type);
        }

        private void TypeChange(SENSOR_TYPE type)
        {
            if (type == SENSOR_TYPE.GUANDAO)
            {
                textBoxInterval.Enabled = true;
                textBoxSensorDeep.Enabled = false;
                comboBoxDaogui.Enabled = true;

            }
            else if(type == SENSOR_TYPE.WUWEI)
            {
                textBoxSensorDeep.Enabled = true;
                textBoxInterval.Enabled = false;
                comboBoxDaogui.Enabled = false;
                
            }
            else if (type == SENSOR_TYPE.MINGQU )
            {
                textBoxInterval.Enabled = true;
                textBoxSensorDeep.Enabled = false;
                comboBoxDaogui.Enabled = true;
            }
            else if (type == SENSOR_TYPE.SHUIWEI)
            {
                
            }
            else
            {
                textBoxSensorDeep.Enabled = false;
                textBoxInterval.Enabled = false;
                comboBoxDaogui.Enabled = false;
            }

            this.lblRopeDepth.Visible = type == SENSOR_TYPE.SHUIWEI;
            this.lblWellDepth.Visible = type == SENSOR_TYPE.SHUIWEI;
            this.txtRopeDepth.Visible = type == SENSOR_TYPE.SHUIWEI;
            this.txtWellDepth.Visible = type == SENSOR_TYPE.SHUIWEI;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeChange((SENSOR_TYPE)comboBoxType.SelectedValue);
        }

        private void buttonDefualtF_Click(object sender, EventArgs e)
        {
            SENSOR_TYPE t = (SENSOR_TYPE)comboBoxType.SelectedValue;
            switch (t)
            {
                case SENSOR_TYPE.GUANDAO:
                    textBoxFormula.Text = "((data -770.0) / (3977.0-770.0)) * 5.0";
            	break;
                case SENSOR_TYPE.WENDU:
                    textBoxFormula.Text = "((data - 820) / (4096 - 820)) * 100";
                    break;
                case SENSOR_TYPE.SHUIWEI:
                    textBoxFormula.Text = "((data * 2.5) / 4096 - 0.76) * 2.5";
                    break;
                case SENSOR_TYPE.YALI:
                    textBoxFormula.Text = "((data * 2.5) / 4096 - 0.76) * 5.0";
                    break;
                case SENSOR_TYPE.MINGQU:
                    textBoxFormula.Text = "((data - 1170) / (3500 - 1170)) * 1";
                    break;
                case SENSOR_TYPE.WUWEI:
                    textBoxFormula.Text = "((data - 820) / (4096 - 820)) * 100";
                    break;
                    default:
                    textBoxFormula.Text = "";
                    break;
            }
        }
    }
}
