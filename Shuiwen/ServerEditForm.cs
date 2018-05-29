using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShuiwenLib;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shuiwen
{
    public partial class ServerEditForm : Form
    {
        Server config;
        public ServerEditForm()
        {
            InitializeComponent();
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.cboIps.Items.Add(ip.ToString());
                }

            }

            config = DbClient.ReadServerConfig();
            if (config != null)
            {
                this.txtName.Text = config.Name;
                this.cboIps.Text = config.IP;
                this.txtPort.Text = config.Port.ToString();
                this.chkEnable.Checked = config.IsEnable;
            }
            else
            {
                this.txtName.Text = "";
                this.cboIps.SelectedIndex = ips.Length > 0 ? 0 : -1;
                this.txtPort.Text = "5000";
                this.chkEnable.Checked = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (config == null)
            {
                config = new Server();

            }
            string ip = this.cboIps.Text.Trim();
            //新增，需要判断IP是否已经存在
            if (config.Id <= 0)
            {
                bool exists = DbClient.ExistsServer(ip);
                if (exists)
                {
                    MessageBox.Show("已存在IP地址为" + ip + "的服务器!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //修改，如果ip更改，也需要判断IP是否已经存在
            else
            {
                if (ip != config.IP)
                {
                    bool exists = DbClient.ExistsServer(ip);
                    if (exists)
                    {
                        MessageBox.Show("已存在IP地址为" + ip + "的服务器!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            config.Name = this.txtName.Text;
            config.IP = ip;
            config.Port = Convert.ToInt32(this.txtPort.Text);
            config.IsEnable = this.chkEnable.Checked;
            int updateOrAdd = 0;
            if (config.Id <= 0)
            {
                updateOrAdd = 1;
            }
            bool result = DbClient.NoQueryServerConfig(config, updateOrAdd);
            if (result)
            {
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
