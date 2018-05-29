namespace Shuiwen
{
    partial class AbnormalForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripEditMax = new System.Windows.Forms.ToolStripTextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnSiteName = new System.Windows.Forms.ColumnHeader();
            this.columnSiteNum = new System.Windows.Forms.ColumnHeader();
            this.columnSensorName = new System.Windows.Forms.ColumnHeader();
            this.columnSensorNum = new System.Windows.Forms.ColumnHeader();
            this.columnType = new System.Windows.Forms.ColumnHeader();
            this.columnData = new System.Windows.Forms.ColumnHeader();
            this.columnMax = new System.Windows.Forms.ColumnHeader();
            this.columnMin = new System.Windows.Forms.ColumnHeader();
            this.columnTime = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClear,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripEditMax});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(599, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.Image = global::Shuiwen.Properties.Resources.delete_pro32;
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonClear.Text = "清空";
            this.toolStripButtonClear.ToolTipText = "清空";
            this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "最大记录数:";
            // 
            // toolStripEditMax
            // 
            this.toolStripEditMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripEditMax.MaxLength = 4;
            this.toolStripEditMax.Name = "toolStripEditMax";
            this.toolStripEditMax.Size = new System.Drawing.Size(50, 25);
            this.toolStripEditMax.Text = "1000";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSiteName,
            this.columnSiteNum,
            this.columnSensorName,
            this.columnSensorNum,
            this.columnType,
            this.columnData,
            this.columnMax,
            this.columnMin,
            this.columnTime});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(599, 314);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnSiteName
            // 
            this.columnSiteName.Text = "分站名";
            // 
            // columnSiteNum
            // 
            this.columnSiteNum.Text = "分站号";
            // 
            // columnSensorName
            // 
            this.columnSensorName.Text = "传感器名";
            this.columnSensorName.Width = 77;
            // 
            // columnSensorNum
            // 
            this.columnSensorNum.Text = "传感器号";
            this.columnSensorNum.Width = 76;
            // 
            // columnType
            // 
            this.columnType.Text = "类型";
            // 
            // columnData
            // 
            this.columnData.Text = "数据";
            this.columnData.Width = 71;
            // 
            // columnMax
            // 
            this.columnMax.Text = "上限";
            // 
            // columnMin
            // 
            this.columnMin.Text = "下限";
            // 
            // columnTime
            // 
            this.columnTime.Text = "时间";
            // 
            // AbnormalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 339);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.HideOnClose = true;
            this.Name = "AbnormalForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TabText = "异常记录";
            this.Text = "异常记录";
            this.Load += new System.EventHandler(this.AbnormalForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripEditMax;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnSiteName;
        private System.Windows.Forms.ColumnHeader columnSiteNum;
        private System.Windows.Forms.ColumnHeader columnSensorName;
        private System.Windows.Forms.ColumnHeader columnSensorNum;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnData;
        private System.Windows.Forms.ColumnHeader columnMax;
        private System.Windows.Forms.ColumnHeader columnMin;
        private System.Windows.Forms.ColumnHeader columnTime;
    }
}