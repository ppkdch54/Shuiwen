using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ShuiwenLib;

namespace Shuiwen
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void 分站设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                return;
            }
            sitesComm.Stop();
            SitesSetForm fm = new SitesSetForm(sites);
            if (DialogResult.OK == fm.ShowDialog())
            {
                sites.Save(new DbClient());
                DbClient.SaveDaogui(sites);
                sensorListForm.Reset();
                abnormalForm.Reset();
                placeForm.Reset();
            }
            else
            {
                Sites.Load(out sites, new DbClient());
            }
           sitesComm.Start();
        }

        private Sites sites = new Sites();

        private IDockContent GetContentFormPersistString(string persistString)
        {
            if (persistString == typeof(PlaceForm).ToString())
                return placeForm;
            //else if (persistString == typeof(QueryForm).ToString())
            //    return queryForm;
            else if (persistString == typeof(PlaceForm).ToString())
                return placeForm;
            else if (persistString == typeof(SitesStateForm).ToString())
                return ssForm;
            else if (persistString == typeof(SensorListForm).ToString())
            {
                return sensorListForm;
            }
            else if (persistString == typeof(AbnormalForm).ToString())
            {
                return abnormalForm;
            }
            else
                return null;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DbClient.TestConnect())
            {
                MessageBox.Show("数据库连接失败，如果是首次运行，请先配置数据库连接。");
                new SetConnForm().ShowDialog();
                return;
            }

            try
            {
                Sites.Load(out sites, new DbClient());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("加载分站配置失败，请检查据库连接。错误信息：" + ex.Message);
                sites = new Sites();
            }
            //UiChange.Load();
            //queryForm = new QueryForm(sites);
            ssForm = new SitesStateForm(sites);
            placeForm = new PlaceForm(sites, false);
            sensorListForm = new SensorListForm(sites);
            abnormalForm = new AbnormalForm(sites);
            //try
            //{
            //    dockPanel.LoadFromXml("DockSettings", GetContentFormPersistString);
            //}
            //catch (Exception ex)
            //{
            //    //queryForm.Show(this.dockPanel, DockState.Document);
            //    placeForm.Show(this.dockPanel, DockState.Document);
            //    ssForm.Show(this.dockPanel, DockState.Document);
            sensorListForm.Show(this.dockPanel, DockState.Document);
            //    abnormalForm.Show(this.dockPanel, DockState.Document);
            //}

//             string v = UiChange.Get(ssForm.GetKey());
//             colCount = v.Length > 0 ? Convert.ToInt32(v) : 1;
//             toolStripComboBoxCol.SelectedIndex = colCount-1;
            
            sitesComm = new SitesComm(sites);
            sitesComm.Start();

            toolStripMenuView.Enabled = true;
            ToolStripReport.Enabled = true;
        }



        private void toolStripComboBoxCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //colCount = toolStripComboBoxCol.SelectedIndex + 1;
            //ssForm.ResiteSites(colCount);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //ssForm.ResiteSites(colCount);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UiUpdater.DataUpdate();
        }
        static SitesComm sitesComm;

        private void 传感器位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                return;
            }
            placeForm.Reset();
            sitesComm.Stop();
            PlaceForm pf = new PlaceForm(sites, true);
            pf.ShowDialog();
            sites.Save(new DbClient());
            placeForm.Reset();
            //ssForm.RefreshSites();
            sitesComm.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sitesComm != null) sitesComm.Stop();
            else return;
            //dockPanel.SaveAsXml("DockSettings");
            //UiChange.Save();
            if (PlaceForm.dd!= null) PlaceForm.dd.Dispose();
        }

        private void 数据库初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                return;
            }
            if(sitesComm!=null)sitesComm.Stop();
            DbResetForm drf = new DbResetForm();
            if (DialogResult.OK == drf.ShowDialog())
            {
                Application.Exit();
            }

            //sitesComm.Start();
        }

        private void ToolStripMenuView_DropDownOpened(object sender, EventArgs e)
        {
            toolStripMenuData.Checked = sensorListForm.Visible;
            toolStripMenuPlace.Checked = placeForm.Visible;
            toolStripMenuAbnormal.Checked = abnormalForm.Visible;
            //toolStripMenuQuery.Checked = queryForm.Visible;
        }

        //private QueryForm queryForm;
        private PlaceForm placeForm;
        private SitesStateForm ssForm;
        private SensorListForm sensorListForm;
        private AbnormalForm abnormalForm;

        private void toolStripMenuQuery_Click(object sender, EventArgs e)
        {
            //if (toolStripMenuQuery.Checked)
            //{
            //    queryForm.Hide();
            //}
            //else
            //{
            //    queryForm.Show(this.dockPanel);
            //}
        }

        private void toolStripMenuPlace_Click(object sender, EventArgs e)
        {
            if (toolStripMenuPlace.Checked)
            {
                placeForm.Hide();
            }
            else
            {
                placeForm.Show(this.dockPanel);
            }
        }

        private void toolStripMenuAbnormal_Click(object sender, EventArgs e)
        {
            if (toolStripMenuAbnormal.Checked)
            {
                abnormalForm.Hide();
            }
            else
            {
                abnormalForm.Show(this.dockPanel);
            }
        }

        private void toolStripMenuData_Click(object sender, EventArgs e)
        {
            if (toolStripMenuData.Checked)
            {
                sensorListForm.Hide();
            }
            else
            {
                sensorListForm.Show(this.dockPanel);
            }
        }

        private void toolStripMenuQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确实要退出吗?","退出",MessageBoxButtons.OKCancel))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private bool ValidatePassword()
        {
            PasswordForm pf = new PasswordForm();
            pf.ShowDialog();
            return pf.password == "123456";
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox f = new AboutBox();
            f.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ErrorForm ef = new ErrorForm(sites);
            ef.Show();
        }

        private void ToolStripReport_Click(object sender, EventArgs e)
        {
            FormReport fr = new FormReport(sites);
            fr.ShowDialog();
        }

        private void 数据库连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                return;
            }
            if(sitesComm!= null)sitesComm.Stop();
            SetConnForm drf = new SetConnForm();
            drf.ShowDialog();
            if (sitesComm != null) sitesComm.Start();
        }

        private void 服务器设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ServerSetForm serverForm = new ServerSetForm();
            //serverForm.ShowDialog();
            //bool isServer = DbClient.IsServer();
            //if (isServer)
            //{
            //    MessageBox.Show("服务器配置已修改，请重启水文数据服务以生效!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

            //}
            new ServerEditForm().ShowDialog();
        }
     
    }
}
