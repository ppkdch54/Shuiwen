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
    public partial class ServerConnForm : Form
    {
        public ServerConnForm(string connectionString)
        {
            InitializeComponent();
            string[] conn = connectionString.Split(new char[]{';','='});
            this.txtDBIp.Text = conn[1];
            this.txtPort.Text = conn[9];
            this.txtUserName.Text = conn[3];
            this.txtPwd.Text = conn[5];
        }
    }
}
