using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using ComConfig;
using ShuiwenLib;

namespace Shuiwen
{
    public partial class SiteSetForm : Form
    {
        public SiteSetForm(Sites sites,Site site)
        {
            InitializeComponent();
            this.site = site;
            this.sites = sites;
            if (site == null)
            {
                this.Text = "添加分站";
                isAdd = true;
                this.site = new Site((uint)sites.Count(), "", "", 12345);
                this.radioTcp.Checked = true;
            }
            else
            {
                this.Text = "修改分站";
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxNum.Text.Length <=0)
            {
                MessageBox.Show("请输入编号!");
                return;
            }
            uint num;
            Site st;
            try
            {
                num = Convert.ToUInt32(textBoxNum.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入有效编号！");
                return;
            }
            if (textBoxName.Text.Length <=0)
            {
                MessageBox.Show("请输入名称！");
                return;
            }
            if (radioTcp.Checked)
            {
                //IPAddress ip;
                //if (!IPAddress.TryParse(textBoxIP.Text, out ip))
                //{
                //    MessageBox.Show("请输入有效IP!");
                //    return;
                //}
                uint port;
                try
                {
                    port = Convert.ToUInt32(textBoxPort.Text);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("请输入有效的端口!");
                    return;
                }
                site.ip = textBoxIP.Text;
                site.port = port;
            }
            else
            {

            }
          
            
            if (sites.Get(num,out st))
            {
                if (isAdd || num != site.num)
                {
                    MessageBox.Show("编号已存在！");
                    return;
                }
            }
            site.num = num;
            site.name = textBoxName.Text;
            
            if (radioSerial.Checked)
            {
                site.commType = ShuiwenLib.Site.CommType.Serial;
            }
            else if (radioTcp.Checked)
            {
                site.commType = ShuiwenLib.Site.CommType.Tcp;
            }
            else if (radioSMS.Checked)
            {
                site.commType = ShuiwenLib.Site.CommType.SMS;
            }
            else if (radioGPRS.Checked)
            {
                site.commType = ShuiwenLib.Site.CommType.GPRS;
            }
            else
            {
                site.commType = ShuiwenLib.Site.CommType.Tcp;
                    
            }
            if (site.commType == ShuiwenLib.Site.CommType.Serial)
            {
                ComSetting comSetting;
                try
                {
                    comSetting = comConfigControl1.GetCom();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("请设置串口参数");
                    return;
                }
                
                site.com = comSetting.com;
                site.parity = comSetting.parity;
                site.stopBits = comSetting.stopBits;
                site.baudRate = comSetting.baudRate;
                site.dataBits = comSetting.dataBits;
            }
            this.DialogResult = DialogResult.OK;
        }

        public Site site;
        public Sites sites;

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SensorForm sf = new SensorForm(site, null);
            if (DialogResult.OK == sf.ShowDialog())
            {
                site.Add(sf.sensor);
                RefreshList();
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (listViewSensors.SelectedItems.Count <= 0)
            {
                return;
            }
            uint num = Convert.ToUInt32(listViewSensors.SelectedItems[0].Text);
            Sensor ss;
            if (!site.Get(num,out ss))
            {
                return;
            }
            SensorForm sf = new SensorForm(site, ss);
            if (DialogResult.OK == sf.ShowDialog())
            {
                site.Delete(num);
                site.Add(sf.sensor);
                RefreshList();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listViewSensors.SelectedItems.Count <= 0)
            {
                return;
            }
            uint num = Convert.ToUInt32(listViewSensors.SelectedItems[0].Text);
            Sensor ss;
            if (site.Get(num, out ss))
            {
                if (DialogResult.Yes == MessageBox.Show("确实要删除吗？", "删除传感器", MessageBoxButtons.YesNo))
                {
                    site.Delete(num);
                    RefreshList();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool isAdd = false;

        private void SiteSetForm_Load(object sender, EventArgs e)
        {
            Fill();
            RefreshList();
        }

        private void Fill()
        {
            textBoxNum.Text = site.num.ToString();
            textBoxName.Text = site.name;
            textBoxIP.Text = site.ip;
            textBoxPort.Text = site.port.ToString();
            radioTcp.Checked = site.commType == ShuiwenLib.Site.CommType.Tcp;
            radioSerial.Checked = site.commType == ShuiwenLib.Site.CommType.Serial;
            radioSMS.Checked = site.commType == ShuiwenLib.Site.CommType.SMS;
            radioGPRS.Checked = site.commType == ShuiwenLib.Site.CommType.GPRS;
            ComSetting cs = new ComSetting()
            {
                com = site.com,
                dataBits = site.dataBits,
                baudRate = site.baudRate,
                stopBits = site.stopBits,
                parity = site.parity,
            };
            comConfigControl1.SetCom(cs);
        }
        private void RefreshList()
        {
            listViewSensors.Items.Clear();
            Sensor ss;
            int idx = 0;
            while (site.GetAt(idx++,out ss))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ss.num.ToString();
                lvi.SubItems.Add(ss.name);
                lvi.SubItems.Add(Sensor.items[ss.type]);
                lvi.SubItems.Add(site.Count().ToString());

                listViewSensors.Items.Add(lvi);
            }
        }

        private void radioSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSerial.Checked)
            {
                comConfigControl1.Enabled = true;
                groupBoxTcp.Enabled = false;
            }
            else if(radioTcp.Checked)
            {
                comConfigControl1.Enabled = false;
                groupBoxTcp.Enabled = true;
            }
            else
            {
                comConfigControl1.Enabled = false;
                groupBoxTcp.Enabled = false;
            }
        }

        private void radioTcp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSerial.Checked)
            {
                comConfigControl1.Enabled = true;
                groupBoxTcp.Enabled = false;
            }
            else if(radioTcp.Checked)
            {
                comConfigControl1.Enabled = false;
                groupBoxTcp.Enabled = true;
            }
            else
            {
                comConfigControl1.Enabled = false;
                groupBoxTcp.Enabled = false;
            }
        }

        private void radioSMS_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSerial.Checked)
            {
                comConfigControl1.Enabled = true;
                groupBoxTcp.Enabled = false;
            }
            else if (radioTcp.Checked)
            {
                comConfigControl1.Enabled = false;
                groupBoxTcp.Enabled = true;
            }
            else
            {
                comConfigControl1.Enabled = false;
                groupBoxTcp.Enabled = false;
            }
        }

        private void radioSiteComm_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxTcp.Enabled = this.comConfigControl1.Enabled = false;
        }
    }
}
