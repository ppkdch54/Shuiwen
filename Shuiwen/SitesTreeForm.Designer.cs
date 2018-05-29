namespace Shuiwen
{
    partial class SitesTreeForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("水文系统");
            this.treeViewSites = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewSites
            // 
            this.treeViewSites.BackColor = System.Drawing.Color.LightGreen;
            this.treeViewSites.CheckBoxes = true;
            this.treeViewSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSites.Location = new System.Drawing.Point(0, 0);
            this.treeViewSites.Name = "treeViewSites";
            treeNode1.Name = "节点0";
            treeNode1.Text = "水文系统";
            this.treeViewSites.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewSites.Size = new System.Drawing.Size(284, 323);
            this.treeViewSites.TabIndex = 0;
            this.treeViewSites.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSites_AfterCheck);
            // 
            // SitesTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 323);
            this.ControlBox = false;
            this.Controls.Add(this.treeViewSites);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SitesTreeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SitesTreeForm";
            this.Load += new System.EventHandler(this.SitesTreeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewSites;

    }
}