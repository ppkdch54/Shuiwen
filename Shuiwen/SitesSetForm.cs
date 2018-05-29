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
    public partial class SitesSetForm : Form
    {
        public SitesSetForm(Sites sites)
        {
            InitializeComponent();
            this.sites = sites;
        }

        private void SitesSetForm_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshList()
        {
            listViewSites.Items.Clear();
            int idx = 0;
            Site st;
            while (sites.GetAt(idx++,out st))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = st.num.ToString();
                lvi.SubItems.Add(st.name);
                string commTypeStr = string.Empty;
                switch (st.commType)
                {
                    case ShuiwenLib.Site.CommType.Serial:
                        commTypeStr = "串口";
                        break;
                    case ShuiwenLib.Site.CommType.Tcp:
                        commTypeStr = "网口";
                        break;
                    case ShuiwenLib.Site.CommType.SMS:
                        commTypeStr = "短信";
                        break;
                    case ShuiwenLib.Site.CommType.GPRS:
                        commTypeStr = "GPRS";
                        break;
                }
                lvi.SubItems.Add(commTypeStr);
                lvi.SubItems.Add(st.Count().ToString());
                listViewSites.Items.Add(lvi);
            }
        }

        private Sites sites = null;
        public Site site = null;

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SiteSetForm fm = new SiteSetForm(sites,null);
            if (DialogResult.OK == fm.ShowDialog())
            {
                sites.Add(fm.site);
                RefreshList();
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (listViewSites.SelectedItems.Count == 0)
            {
                return;
            }
            uint num = Convert.ToUInt32(listViewSites.SelectedItems[0].Text);
            Site st;
            if (sites.Get(num,out st))
            {
                SiteSetForm fm = new SiteSetForm(sites, st);
                if (DialogResult.OK == fm.ShowDialog())
                {
                    sites.Delete(num);
                    sites.Add(fm.site);
                    RefreshList();
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listViewSites.SelectedItems.Count == 0)
            {
                return;
            }
            uint num = Convert.ToUInt32(listViewSites.SelectedItems[0].Text);
            Site st;
            if (sites.Get(num, out st))
            {
                if (DialogResult.Yes == MessageBox.Show("确定要删除吗？","删除分站",MessageBoxButtons.YesNo))
                {
                    sites.Delete(num);
                    RefreshList();
                }
            }
        }
    }
}
