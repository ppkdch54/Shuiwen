namespace Shuiwen
{
    partial class SensorListForm
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
            this.listViewData = new Shuiwen.DoubleBufferListView();
            this.columnData = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnType = new System.Windows.Forms.ColumnHeader();
            this.columnTime = new System.Windows.Forms.ColumnHeader();
            this.columnMax = new System.Windows.Forms.ColumnHeader();
            this.columnMin = new System.Windows.Forms.ColumnHeader();
            this.columnOrigionData = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listViewData
            // 
            this.listViewData.BackColor = System.Drawing.Color.LightGreen;
            this.listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnData,
            this.columnName,
            this.columnType,
            this.columnTime,
            this.columnMax,
            this.columnMin,
            this.columnOrigionData});
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewData.FullRowSelect = true;
            this.listViewData.GridLines = true;
            this.listViewData.Location = new System.Drawing.Point(0, 0);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(768, 482);
            this.listViewData.TabIndex = 1;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.Details;
            // 
            // columnData
            // 
            this.columnData.Text = "数据";
            this.columnData.Width = 230;
            // 
            // columnName
            // 
            this.columnName.Text = "名称";
            this.columnName.Width = 144;
            // 
            // columnType
            // 
            this.columnType.Text = "类型";
            this.columnType.Width = 79;
            // 
            // columnTime
            // 
            this.columnTime.Text = "时间";
            this.columnTime.Width = 200;
            // 
            // columnMax
            // 
            this.columnMax.Text = "上限";
            // 
            // columnMin
            // 
            this.columnMin.Text = "下限";
            this.columnMin.Width = 73;
            // 
            // columnOrigionData
            // 
            this.columnOrigionData.Text = "原始数据";
            this.columnOrigionData.Width = 92;
            // 
            // SensorListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 482);
            this.Controls.Add(this.listViewData);
            this.DoubleBuffered = true;
            this.HideOnClose = true;
            this.Name = "SensorListForm";
            this.TabText = "实时数据表";
            this.Text = "实时数据表";
            this.Load += new System.EventHandler(this.SensorListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBufferListView listViewData;
        private System.Windows.Forms.ColumnHeader columnData;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ColumnHeader columnMax;
        private System.Windows.Forms.ColumnHeader columnMin;
        private System.Windows.Forms.ColumnHeader columnOrigionData;

    }
}