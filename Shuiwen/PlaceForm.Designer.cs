namespace Shuiwen
{
    partial class PlaceForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitLeft = new System.Windows.Forms.SplitContainer();
            this.listBoxPaper = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.treeViewSensors = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitLeft.Panel1.SuspendLayout();
            this.splitLeft.Panel2.SuspendLayout();
            this.splitLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "DWG";
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "DWG files|*.dwg|DXF files|*.dxf";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitLeft);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.splitContainer1.Panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel2_DragDrop);
            this.splitContainer1.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseDown);
            this.splitContainer1.Panel2.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel2_DragEnter);
            this.splitContainer1.Panel2.SizeChanged += new System.EventHandler(this.panel1_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(714, 486);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitLeft
            // 
            this.splitLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeft.Location = new System.Drawing.Point(0, 0);
            this.splitLeft.Name = "splitLeft";
            this.splitLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitLeft.Panel1
            // 
            this.splitLeft.Panel1.Controls.Add(this.listBoxPaper);
            this.splitLeft.Panel1.Controls.Add(this.label2);
            this.splitLeft.Panel1.Controls.Add(this.buttonImport);
            this.splitLeft.Panel1.Controls.Add(this.buttonDel);
            // 
            // splitLeft.Panel2
            // 
            this.splitLeft.Panel2.Controls.Add(this.treeViewSensors);
            this.splitLeft.Size = new System.Drawing.Size(209, 486);
            this.splitLeft.SplitterDistance = 149;
            this.splitLeft.TabIndex = 6;
            // 
            // listBoxPaper
            // 
            this.listBoxPaper.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.listBoxPaper.FormattingEnabled = true;
            this.listBoxPaper.ItemHeight = 12;
            this.listBoxPaper.Location = new System.Drawing.Point(3, 24);
            this.listBoxPaper.Name = "listBoxPaper";
            this.listBoxPaper.Size = new System.Drawing.Size(198, 88);
            this.listBoxPaper.TabIndex = 0;
            this.listBoxPaper.SelectedIndexChanged += new System.EventHandler(this.listBoxPaper_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "图纸列表";
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(3, 118);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 1;
            this.buttonImport.Text = "导入";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(126, 118);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(75, 23);
            this.buttonDel.TabIndex = 2;
            this.buttonDel.Text = "删除";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // treeViewSensors
            // 
            this.treeViewSensors.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.treeViewSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSensors.Location = new System.Drawing.Point(0, 0);
            this.treeViewSensors.Name = "treeViewSensors";
            treeNode1.Name = "ROOT";
            treeNode1.Text = "水文系统";
            this.treeViewSensors.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewSensors.Size = new System.Drawing.Size(209, 333);
            this.treeViewSensors.TabIndex = 3;
            this.treeViewSensors.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSensors_AfterSelect);
            this.treeViewSensors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewSensors_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "传感器列表";
            // 
            // PlaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(722, 497);
            this.Controls.Add(this.splitContainer1);
            this.HideOnClose = true;
            this.Name = "PlaceForm";
            this.TabText = "传感器位置";
            this.Text = "传感器位置";
            this.Load += new System.EventHandler(this.PlaceForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitLeft.Panel1.ResumeLayout(false);
            this.splitLeft.Panel1.PerformLayout();
            this.splitLeft.Panel2.ResumeLayout(false);
            this.splitLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ListBox listBoxPaper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeViewSensors;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitLeft;
    }
}