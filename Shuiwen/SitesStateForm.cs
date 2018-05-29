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
    public partial class SitesStateForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public SitesStateForm(Sites sts)
        {
            InitializeComponent();
            sites = sts;
           
            RefreshSites();
        }

        public void RefreshSites()
        {
            ssfs.Clear();
            flowLayouSites.Controls.Clear();
            Site st;
            int i = 0;
            while (sites.GetAt(i++,out st))
            {
                SiteStateForm ssf = new SiteStateForm(st);
                ssf.TopLevel = false;
                ssfs.Add(st.num, ssf);
                flowLayouSites.Controls.Add(ssf);
                ssf.Show();
            }
        }

        private Sites sites;
        private Dictionary<uint, SiteStateForm> ssfs = new Dictionary<uint, SiteStateForm>();

        private void SitesStateForm_Load(object sender, EventArgs e)
        {
            ResiteSites(colCount);
        }

        public void ResiteSites(int col)
        {
            if (ssfs.Count <= 0)
            {
                return;
            }
            colCount = col;
            UiChange.Set(GetKey(), colCount.ToString());
            int sWidth = (flowLayouSites.Width / colCount) - 10;
            int sHeight = (flowLayouSites.Height / (ssfs.Count / colCount + ssfs.Count % colCount)) - 10;
            foreach (KeyValuePair<uint, SiteStateForm> smf in ssfs)
            {
                smf.Value.Size = new Size(sWidth, sHeight);
            }
        }

        public void ResiteSites()
        {
             ResiteSites(colCount);
        }

        public string GetKey()
        {
            return "SitesStateForm_" + sites.Count().ToString();
        }

        private int colCount = 1;
    }
}
