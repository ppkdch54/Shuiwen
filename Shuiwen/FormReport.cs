using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Shuiwen.Properties;
using ShuiwenLib;
using System.IO;

namespace Shuiwen
{
    public partial class FormReport : Form
    {
        public FormReport(Sites sites)
        {
            InitializeComponent();
            this.sites = sites;
            Dictionary<uint, string> sitesDic = new Dictionary<uint, string>();
            Site st;
            int i = 0;
            while(sites.GetAt(i++,out st))
            {
                sitesDic.Add(st.num, st.name);
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = sitesDic;
            comboBoxSites.DisplayMember = "Value";
            comboBoxSites.ValueMember = "Key";
            comboBoxSites.DataSource = bs;

            Dictionary<string, DbClient.TIME_UNIT> timeDic = new Dictionary<string, DbClient.TIME_UNIT>() { { "分", DbClient.TIME_UNIT.MIN }, {"小时", DbClient.TIME_UNIT.HOUR }, { "日", DbClient.TIME_UNIT.DAY }, { "月", DbClient.TIME_UNIT.MONTH } };
            bs = new BindingSource();
            bs.DataSource = timeDic;
            comboBoxTime.DisplayMember = "Key";
            comboBoxTime.ValueMember = "Value";
            comboBoxTime.DataSource = bs;

            
 
        }

        private void FormReport_Load(object sender, EventArgs e)
        {   
            //this.reportViewer1.RefreshReport();
            textBoxName.Text = Settings.Default.TitleName;
            this.rdoThisWeek.Checked = true;
        }

        private void comboBoxSites_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxSites.SelectedItem == null)
            {
                return;
            }
            ChangeSite((uint)comboBoxSites.SelectedValue);
        }

        private void ChangeSite(uint num)
        {
            Site st;
            sites.Get(num,out st);
            sel_st = st;
            Sensor ss;
            int i=0;
            Dictionary<uint, string> sensorDic = new Dictionary<uint, string>(); 
            while (st.GetAt(i++,out ss))
            {
                sensorDic.Add(ss.num, ss.name);
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = sensorDic;
            
            comboBoxSensors.DisplayMember = "Value";
            comboBoxSensors.ValueMember = "Key";
            comboBoxSensors.DataSource = bs;
        }
        private Sites sites;

        private void buttonMake_Click(object sender, EventArgs e)
        {
            Settings.Default.TitleName = textBoxName.Text;
            Properties.Settings.Default.Save();
            Sensor ss = null;
            sel_st.Get((uint)comboBoxSensors.SelectedValue,out ss);
            DataSet ds = DbClient.Query(sel_st.num,
                ss.num,
                dateTimeFrom.Value,
                dateTimeTo.Value,
                (DbClient.TIME_UNIT)comboBoxTime.SelectedValue,
                ss.type == SENSOR_TYPE.GUANDAO || ss.type == SENSOR_TYPE.MINGQU 
            );
            DataTable dt_fill = null;
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    dt_fill = dt;
                    break;
                }
            }
            if (dt_fill == null)
            {
                MessageBox.Show("没有查到数据，请重新设定查询条件。");
                return;
            }
            DataView dv = dt_fill.DefaultView;
            dv.Sort = "time asc";
            dt_fill = dv.ToTable();
            dt_fill.Columns.Remove("site");
            dt_fill.Columns.Remove("sensor");
            dt_fill.Columns.Add("sum");
            dt_fill.Columns["time"].ColumnName = "_time";
            dt_fill.Columns.Add("time");
            string format = "yyyy年MM月dd日HH时";
            switch (comboBoxTime.SelectedIndex)
            {
                case 0:
                    format = "yyyy年MM月dd日HH时mm分";
                    break;
                case 1:
                    format = "yyyy年MM月dd日HH时";
                    break;
                case 2:
                    format = "yyyy年MM月dd日";
                    break;
                case 3:
                    format = "yyyy年MM月";
                    break;
                default:
                    
                    break;
            }
            if (ss.type == SENSOR_TYPE.GUANDAO || ss.type == SENSOR_TYPE.MINGQU )
            {
                for (int i=0;i<dt_fill.Rows.Count;++i)
                {
                    double sum = 0.0;
                    for (int j = 0; j <= i; ++j)
                    {
                        double value = (double)dt_fill.Rows[j]["data"];
//                         switch (comboBoxTime.SelectedIndex)
//                         {
//                             case 0:
//                                 sum *= 1;
//                                 break;
//                             case 1:
//                                 sum *= 24;
//                                 break;
//                             case 2:
//                                 DateTime time = (DateTime)dt_fill.Rows[j].ItemArray[0];
//                                 sum *= DateTime.DaysInMonth(time.Year, time.Month);
//                                 break;
//                             default:
//                                 sum *= 1; 
//                                 break;
//                         }
                        sum += value;
                    }
                    
                    DateTime obtm = (DateTime)dt_fill.Rows[i]["_time"];
                    dt_fill.Rows[i]["time"] = obtm.ToString(format);
                    dt_fill.Rows[i]["sum"] = sum;
                }
            }
            else
            {
                for (int i = 0; i < dt_fill.Rows.Count; ++i)
                {
                    DateTime obtm = (DateTime)dt_fill.Rows[i]["_time"];
                    dt_fill.Rows[i]["time"] = obtm.ToString(format);
                    dt_fill.Rows[i]["sum"] = "--";
                }
            }
            dt_fill.TableName = "SwDatas";
            //List<ReportParameter> rps = new List<ReportParameter>();
            ReportParameter p1 = new ReportParameter("colName1","时间");
            ReportParameter p2 = new ReportParameter("colName3","总累计");
            string colName2;
            string title = textBoxName.Text;
            
            switch(ss.type)
            {
                case SENSOR_TYPE.MINGQU:
                case SENSOR_TYPE.GUANDAO:
                    colName2 = @"单位时间流量(㎥)";
                    title += "流量";
                    break;
                case SENSOR_TYPE.SHUIWEI:
                case SENSOR_TYPE.WUWEI:
                    colName2 = @"水位(m)";
                    title += "水位";
                    break;
                case SENSOR_TYPE.WENDU:
                    colName2 = @"温度(℃)";
                    title += "温度";
                    break;
                case SENSOR_TYPE.YALI:
                    colName2 = @"压力(MPa)";
                    title += "压力";
                    break;
                case SENSOR_TYPE.DIANYA:
                    colName2 = @"伏(V)";
                    title += "电压";
                    break;
                default:
                    colName2 = "";
                    break;
            }
            ReportParameter p3 = new ReportParameter("colName2", colName2);
            ReportParameter p4 = new ReportParameter("title", title);
            ReportParameter p5 = new ReportParameter("beginTime",dateTimeFrom.Value.ToString(dateTimeFrom.CustomFormat));
            ReportParameter p6 = new ReportParameter("endTime",dateTimeTo.Value.ToString(dateTimeTo.CustomFormat));
            reportViewer1.LocalReport.ReportEmbeddedResource = "Shuiwen.Report1.rdlc";
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]{p1,p2,p3,p4,p5,p6});
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SwDatas",dt_fill));
            reportViewer1.RefreshReport();

            if ((sender as Button).Tag != null)
            {
                if ((sender as Button).Tag.ToString() == "Export")
                {
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                       "Excel", null, out mimeType, out encoding, out extension,
                       out streamids, out warnings);

                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        DateTime.Now.ToString("yyyyMMddHHmmss") + title + "记录.xls");
                    FileStream fs = new FileStream(fileName, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();

                    MessageBox.Show(Path.GetFileName(fileName) + "已导出至桌面!","提示");
                }
            }
        }
        Site sel_st  = null;

        private void comboBoxTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime v;
            switch ((DbClient.TIME_UNIT)comboBoxTime.SelectedValue)
            {
                case DbClient.TIME_UNIT.MIN:
                    dateTimeFrom.CustomFormat = "yyyy年MM月dd日HH时mm分";
                    dateTimeTo.CustomFormat = "yyyy年MM月dd日HH时mm分";
                    v = dateTimeFrom.Value;
                    dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
                    v = dateTimeTo.Value;
                    dateTimeTo.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
                    break;
                case DbClient.TIME_UNIT.HOUR:
                    dateTimeFrom.CustomFormat = "yyyy年MM月dd日HH时";
                    dateTimeTo.CustomFormat = "yyyy年MM月dd日HH时";
                    v = dateTimeFrom.Value;
                    dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, 0, 0);
                    v = dateTimeTo.Value;
                    dateTimeTo.Value = new System.DateTime(v.Year, v.Month, v.Day, v.Hour, 0, 0);
                    break;
                case DbClient.TIME_UNIT.DAY:
                    dateTimeFrom.CustomFormat = "yyyy年MM月dd日";
                    dateTimeTo.CustomFormat = "yyyy年MM月dd日";
                    v = dateTimeFrom.Value;
                    dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, v.Day, 0, 0, 0);
                    v = dateTimeTo.Value;
                    dateTimeTo.Value = new System.DateTime(v.Year, v.Month, v.Day, 0, 0, 0);
                    break;
                case DbClient.TIME_UNIT.MONTH:
                    dateTimeFrom.CustomFormat = "yyyy年MM月";
                    dateTimeTo.CustomFormat = "yyyy年MM月";
                    v = dateTimeFrom.Value;
                    dateTimeFrom.Value = new System.DateTime(v.Year, v.Month, 1, 0, 0, 0);
                    v = dateTimeTo.Value;
                    dateTimeTo.Value = new System.DateTime(v.Year, v.Month, 1, 0, 0, 0);
                    break;
               default:
                    break;
            }
        }

        private void timeChanged_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;
            DateTime now = DateTime.Now;
            if (rdo.Checked)
            {
                switch (rdo.Text)
                {
                    case "本日":
                        dateTimeFrom.Value = new DateTime(now.Year,now.Month,now.Day,0,0,0);
                        dateTimeTo.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                        break;
                    case "本周":
                        DateTime monday = GetWeekFirstDayMon(now);
                        DateTime sunday = GetWeekLastDaySun(now);
                        dateTimeFrom.Value = new DateTime(monday.Year, monday.Month, monday.Day, 0, 0, 0);
                        dateTimeTo.Value = new DateTime(sunday.Year,sunday.Month,sunday.Day,23,59,59);
                        break;
                    case "本月":
                        int[] months = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                        if (now.Year % 4 == 0 || now.Year % 100 == 0)
                        {
                            months[1] = 29;
                        }
                        int daysOfThisMonth = months[now.Month-1];
                        dateTimeFrom.Value = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                        dateTimeTo.Value = new DateTime(now.Year, now.Month, daysOfThisMonth, 23, 59, 59);
                        break;
                }
            }
        }

        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>
        /// 得到本周最后一天(以星期天为最后一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        
    }
}
