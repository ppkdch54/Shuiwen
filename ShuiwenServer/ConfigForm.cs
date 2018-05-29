using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShuiwenServer
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            Config config = Config.LoadConfig();
            this.txtServerIP.Text = config.ServerIP;
            this.txtServerPort.Text = config.ServerPort.ToString();
            this.txtDBIP.Text = config.DatabaseIP;
            this.txtDBPort.Text = config.DatabasePort.ToString();
            this.txtDBUserName.Text = config.DatabaseUserName;
            this.txtDBPassword.Text = config.DatabasePassword;
        }
    }
}
