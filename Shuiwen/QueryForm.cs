using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ShuiwenLib;

namespace Shuiwen
{
    public partial class QueryForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public QueryForm(Sites sites)
        {
            InitializeComponent();
            this.sites = sites;
            
            stf = new SitesTreeForm(sites);
            dateTimeFrom.Format = DateTimePickerFormat.Custom;
            dateTimeFrom.CustomFormat = "yyyy-MM-dd";

            dateTimeTo.Format = DateTimePickerFormat.Custom;
            dateTimeTo.CustomFormat = "yyyy-MM-dd";
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            stf.TopLevel = false;
            splitContainer.Panel1.Controls.Add(stf);
            stf.Dock = DockStyle.Fill;
            stf.Show();

            DataSet ds = new DataSet();
             DataTable dt = ds.Tables.Add("datas");
             dt.Columns.Add("siteName");
             dt.Columns.Add("siteNum");
             dt.Columns.Add("sensorName");
             dt.Columns.Add("sensorNum");
             dt.Columns.Add("sensorType");
             dt.Columns.Add("data_origion");
             dt.Columns.Add("data");
             DataColumn dcTime = dt.Columns.Add("time");
         
 
            dataGridSensor.DataSource = ds;
            dataGridSensor.DataMember = dt.TableName;
        }

        private SitesTreeForm stf;
        private Sites sites;
        private DbClient.TIME_UNIT timeUnit = DbClient.TIME_UNIT.DAY;

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            object[] objs = stf.GetCheckedSensors();
            //List<datas> result = new List<datas>();
            DataSet newSet = new DataSet();
            foreach (object[] obj in objs)
            {
                SENSOR_TYPE type = sites.GetType((uint)obj[0],(uint)obj[1]) ;
                newSet.Merge(DbClient.Query((uint)obj[0],
                    (uint)obj[1],
                    dateTimeFrom.Value,
                    dateTimeTo.Value,
                    timeUnit,
                    (type == SENSOR_TYPE.GUANDAO||type==SENSOR_TYPE.MINGQU))
                );
                //result.AddRange();
            }
        
            if (radioButtonGrid.Checked)
            {
                dataGridSensor.Show();
                chartSensors.Hide();
                FillGrid(newSet);
            }
            else
            {
                dataGridSensor.Hide();
                chartSensors.Show();
                FillChart(newSet);
            }
        }

        private void FillChart(DataSet datas)
        {
            chartSensors.Series.Clear();
            if (datas.Tables.Count<=0)
            {
                return;
            }
            foreach (DataRow dr in datas.Tables[0].Rows)
            {
                Site st;
                if (sites.Get((uint)(int)dr["site"], out st))
                {
                    Sensor ss;
                    if (st.Get((uint)(int)dr["sensor"], out ss))
                    {
                        //dt.Rows.Add(new object[] { st.name, st.num, ss.name, ss.num, Sensor.items[(SENSOR_TYPE)ss.type], Parser.GetShowData(ss.type, (double)dr[2]), ((DateTime)dr[3]).ToString("yyyy年MM月dd日 HH:mm:ss") });
                        if (chartSensors.Series.IndexOf(st.name+'.'+ss.name)<0)
                        {
                            Series newS = new Series(st.name+'.'+ss.name);
                            newS.ChartType = SeriesChartType.FastLine;
                            newS.ShadowColor = Color.Black;
                            newS.ShadowOffset = 1;
                            chartSensors.Series.Add(newS);
                        }
                        double data =  Parser.GetShowData(ss.type,(double)dr["data_origion"],(double)dr["data"], (double)dr["data"],(double)dr["data"]);
                        if (checkBoxAbnormal.Checked && data <= (double)ss.alarmHigh && data >= (double)ss.alarmLow )
                        {
                            continue;
                        }
                        chartSensors.Series.FindByName(st.name + '.' + ss.name).Points.AddXY(dr["time"],data);
                    }
                }
            }
        }

        private void FillGrid(DataSet datas)
        {
            DataTable dt = ((DataSet)dataGridSensor.DataSource).Tables[0];
            dt.Rows.Clear();
            if (datas.Tables.Count <= 0)
            {
                return;
            }
           
            foreach (DataRow dr in datas.Tables[0].Rows)
            {
                Site st;
                if (sites.Get((uint)(int)dr["site"], out st))
                {
                    Sensor ss;
                    if (st.Get((uint)(int)dr["sensor"], out ss))
                    {
                        double data = Parser.GetShowData(ss.type,(double)dr["data_origion"], (double)dr["data"],(double)dr["data"], (double)dr["data"]);
                        if (checkBoxAbnormal.Checked && data <= (double)ss.alarmHigh && data >= (double)ss.alarmLow)
                        {
                            continue;
                        }
                        dt.Rows.Add(new object[] { st.name, st.num, ss.name, ss.num, Sensor.items[(SENSOR_TYPE)ss.type], data, ((DateTime)dr["time"]).ToString("yyyy年MM月dd日 HH:mm:ss") });
                    }
                }
            }
        }

        private void radioSecond_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSecond.Checked)
            {
                dateTimeFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                dateTimeTo.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                timeUnit = DbClient.TIME_UNIT.SECOND;
            }
        }

        private void radioMinute_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMinute.Checked)
            {
                dateTimeFrom.CustomFormat = "yyyy-MM-dd HH:mm";
                dateTimeTo.CustomFormat = "yyyy-MM-dd HH:mm";
                DateTime v = dateTimeFrom.Value;
                dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
                v = dateTimeTo.Value;
                dateTimeTo.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
                timeUnit = DbClient.TIME_UNIT.MIN;
            }
        }

        private void radioHour_CheckedChanged(object sender, EventArgs e)
        {
            if (radioHour.Checked)
            {
                dateTimeFrom.CustomFormat = "yyyy-MM-dd HH";
                dateTimeTo.CustomFormat = "yyyy-MM-dd HH";
                DateTime v = dateTimeFrom.Value;
                dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, 0, 0);
                v = dateTimeTo.Value;
                dateTimeTo.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, 0, 0);
                timeUnit = DbClient.TIME_UNIT.HOUR;
            }
        }

        private void radioDay_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDay.Checked)
            {
                dateTimeFrom.CustomFormat = "yyyy-MM-dd";
                dateTimeTo.CustomFormat = "yyyy-MM-dd";
                DateTime v = dateTimeFrom.Value;
                dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, v.Day, 0, 0, 0);
                v = dateTimeTo.Value;
                dateTimeTo.Value = new System.DateTime(v.Year, v.Month, v.Day, 0, 0, 0);
                timeUnit = DbClient.TIME_UNIT.DAY;
            }
        }

        private void radioMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMonth.Checked)
            {
                dateTimeFrom.CustomFormat = "yyyy-MM";
                dateTimeTo.CustomFormat = "yyyy-MM";
                DateTime v = dateTimeFrom.Value;
                dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, 1, 0, 0, 0);
                v = dateTimeTo.Value;
                dateTimeTo.Value = new System.DateTime(v.Year, v.Month, 1, 0, 0, 0);
                timeUnit = DbClient.TIME_UNIT.MONTH;
            }
        }

    }
}
