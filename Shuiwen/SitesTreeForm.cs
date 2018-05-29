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
    public partial class SitesTreeForm : Form
    {
        public SitesTreeForm(Sites sites)
        {
            InitializeComponent();
            this.sites = sites;
        }

        private void SitesTreeForm_Load(object sender, EventArgs e)
        {
            RefreshTree();
        }

        private void RefreshTree()
        {
            Site st;
            int i = 0;
            while (sites.GetAt(i++, out st))
            {
                TreeNode tn = treeViewSites.Nodes[0].Nodes.Add(st.num.ToString(), "(" + st.num.ToString() + ")" + st.name);
                tn.Tag = st.num;
                Sensor ss;
                int j = 0;
                while (st.GetAt(j++, out ss))
                {
                    TreeNode tns = tn.Nodes.Add(ss.num.ToString(), "(" + ss.num.ToString() + ")" + ss.name);
                    tns.Tag = ss.num;
                }
            }
            treeViewSites.ExpandAll();
        }
        public uint[] GetCheckedSites()
        {
            List<uint> cs = new List<uint>();
            foreach (TreeNode tn in treeViewSites.Nodes[0].Nodes)
            {
                if (tn.Checked)
                {
                    cs.Add((uint)tn.Tag);
                }
            }
            return cs.ToArray();
        }

        public object[] GetCheckedSensors()
        {
            List<object[]> cs = new List<object[]>();
            foreach (TreeNode tn in treeViewSites.Nodes[0].Nodes)
            {
                
                foreach(TreeNode tns in tn.Nodes)
                if (tns.Checked)
                {
                    cs.Add(new object[]{tn.Tag,tns.Tag});
                }
            }
            return cs.ToArray();
        }
        private Sites sites;

        private void treeViewSites_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
            }
        }
    }
}
