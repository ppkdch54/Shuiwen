namespace Shuiwen
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.服务器设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分站设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.传感器位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库初始化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuPlace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAbnormal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripReport = new System.Windows.Forms.ToolStripMenuItem();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "水文监测系统";
            this.notifyIcon1.Visible = true;
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 25);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(508, 330);
            this.dockPanel.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.toolStripMenuView,
            this.ToolStripReport,
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(508, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务器设置ToolStripMenuItem,
            this.分站设置ToolStripMenuItem,
            this.传感器位置ToolStripMenuItem,
            this.数据库连接ToolStripMenuItem,
            this.数据库初始化ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 服务器设置ToolStripMenuItem
            // 
            this.服务器设置ToolStripMenuItem.Name = "服务器设置ToolStripMenuItem";
            this.服务器设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.服务器设置ToolStripMenuItem.Text = "服务器设置";
            this.服务器设置ToolStripMenuItem.Click += new System.EventHandler(this.服务器设置ToolStripMenuItem_Click);
            // 
            // 分站设置ToolStripMenuItem
            // 
            this.分站设置ToolStripMenuItem.Name = "分站设置ToolStripMenuItem";
            this.分站设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.分站设置ToolStripMenuItem.Text = "分站设置";
            this.分站设置ToolStripMenuItem.Click += new System.EventHandler(this.分站设置ToolStripMenuItem_Click);
            // 
            // 传感器位置ToolStripMenuItem
            // 
            this.传感器位置ToolStripMenuItem.Name = "传感器位置ToolStripMenuItem";
            this.传感器位置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.传感器位置ToolStripMenuItem.Text = "传感器位置";
            this.传感器位置ToolStripMenuItem.Click += new System.EventHandler(this.传感器位置ToolStripMenuItem_Click);
            // 
            // 数据库连接ToolStripMenuItem
            // 
            this.数据库连接ToolStripMenuItem.Name = "数据库连接ToolStripMenuItem";
            this.数据库连接ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.数据库连接ToolStripMenuItem.Text = "数据库连接";
            this.数据库连接ToolStripMenuItem.Click += new System.EventHandler(this.数据库连接ToolStripMenuItem_Click);
            // 
            // 数据库初始化ToolStripMenuItem
            // 
            this.数据库初始化ToolStripMenuItem.Enabled = false;
            this.数据库初始化ToolStripMenuItem.Name = "数据库初始化ToolStripMenuItem";
            this.数据库初始化ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.数据库初始化ToolStripMenuItem.Text = "数据库初始化";
            this.数据库初始化ToolStripMenuItem.Click += new System.EventHandler(this.数据库初始化ToolStripMenuItem_Click);
            // 
            // toolStripMenuView
            // 
            this.toolStripMenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuData,
            this.toolStripMenuPlace,
            this.toolStripMenuAbnormal,
            this.toolStripMenuQuery,
            this.toolStripMenuItem1});
            this.toolStripMenuView.Enabled = false;
            this.toolStripMenuView.Name = "toolStripMenuView";
            this.toolStripMenuView.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuView.Text = "查看";
            this.toolStripMenuView.DropDownOpened += new System.EventHandler(this.ToolStripMenuView_DropDownOpened);
            // 
            // toolStripMenuData
            // 
            this.toolStripMenuData.Name = "toolStripMenuData";
            this.toolStripMenuData.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuData.Text = "实时数据";
            this.toolStripMenuData.Click += new System.EventHandler(this.toolStripMenuData_Click);
            // 
            // toolStripMenuPlace
            // 
            this.toolStripMenuPlace.Name = "toolStripMenuPlace";
            this.toolStripMenuPlace.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuPlace.Text = "传感器位置";
            this.toolStripMenuPlace.Click += new System.EventHandler(this.toolStripMenuPlace_Click);
            // 
            // toolStripMenuAbnormal
            // 
            this.toolStripMenuAbnormal.Name = "toolStripMenuAbnormal";
            this.toolStripMenuAbnormal.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuAbnormal.Text = "异常记录";
            this.toolStripMenuAbnormal.Click += new System.EventHandler(this.toolStripMenuAbnormal_Click);
            // 
            // toolStripMenuQuery
            // 
            this.toolStripMenuQuery.Name = "toolStripMenuQuery";
            this.toolStripMenuQuery.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuQuery.Text = "历史数据";
            this.toolStripMenuQuery.Visible = false;
            this.toolStripMenuQuery.Click += new System.EventHandler(this.toolStripMenuQuery_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem1.Text = "错误数据";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ToolStripReport
            // 
            this.ToolStripReport.Enabled = false;
            this.ToolStripReport.Name = "ToolStripReport";
            this.ToolStripReport.Size = new System.Drawing.Size(44, 21);
            this.ToolStripReport.Text = "报表";
            this.ToolStripReport.Click += new System.EventHandler(this.ToolStripReport_Click);
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem,
            this.toolStripMenuQuit});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // toolStripMenuQuit
            // 
            this.toolStripMenuQuit.Name = "toolStripMenuQuit";
            this.toolStripMenuQuit.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuQuit.Text = "退出";
            this.toolStripMenuQuit.Click += new System.EventHandler(this.toolStripMenuQuit_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 355);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "水文监测系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分站设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuQuery;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuQuit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 传感器位置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPlace;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAbnormal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuData;
        private System.Windows.Forms.ToolStripMenuItem 数据库初始化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripReport;
        private System.Windows.Forms.ToolStripMenuItem 数据库连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 服务器设置ToolStripMenuItem;
    }
}