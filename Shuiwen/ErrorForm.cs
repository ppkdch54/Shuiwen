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
    public partial class ErrorForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ErrorForm(Sites sites)
        {
            InitializeComponent();
            this.sites = sites;
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            uu = new UiUpdater(IsDataRight);
        }

        bool IsDataRight(uint siteNum, uint sensorNum, double dataOrigion,double data,double dataAvg,double dataSum,DateTime time)
        {
            if (!sites.Exists(siteNum, sensorNum))
            {
                listBoxError.Items.Add(string.Format("时间:{4}分站:{0};传感器:{1};数据:{2},{3};{5}", siteNum, sensorNum, dataOrigion,dataAvg,dataSum, time));
                return false;
            }
            else
            {
                return true;
            }
        }
        private UiUpdater uu;
        private Sites sites;

        private void ErrorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            uu.UnSet();
        }
    }
}
