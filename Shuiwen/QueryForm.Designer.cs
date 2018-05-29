namespace Shuiwen
{
    partial class QueryForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioMinute = new System.Windows.Forms.RadioButton();
            this.radioMonth = new System.Windows.Forms.RadioButton();
            this.radioSecond = new System.Windows.Forms.RadioButton();
            this.radioDay = new System.Windows.Forms.RadioButton();
            this.radioHour = new System.Windows.Forms.RadioButton();
            this.checkBoxAbnormal = new System.Windows.Forms.CheckBox();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.radioButtonGrid = new System.Windows.Forms.RadioButton();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.panelDate = new System.Windows.Forms.Panel();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.chartSensors = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridSensor = new System.Windows.Forms.DataGridView();
            this.ColumnSiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSiteNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSensorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSensorNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSensorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.siteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSensors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSensor)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer.Size = new System.Drawing.Size(798, 525);
            this.splitContainer.SplitterDistance = 185;
            this.splitContainer.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.checkBoxAbnormal);
            this.splitContainer2.Panel1.Controls.Add(this.radioButtonLine);
            this.splitContainer2.Panel1.Controls.Add(this.radioButtonGrid);
            this.splitContainer2.Panel1.Controls.Add(this.buttonQuery);
            this.splitContainer2.Panel1.Controls.Add(this.panelDate);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartSensors);
            this.splitContainer2.Panel2.Controls.Add(this.dataGridSensor);
            this.splitContainer2.Size = new System.Drawing.Size(609, 525);
            this.splitContainer2.SplitterDistance = 85;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioMinute);
            this.groupBox1.Controls.Add(this.radioMonth);
            this.groupBox1.Controls.Add(this.radioSecond);
            this.groupBox1.Controls.Add(this.radioDay);
            this.groupBox1.Controls.Add(this.radioHour);
            this.groupBox1.Location = new System.Drawing.Point(6, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 38);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间单位";
            // 
            // radioMinute
            // 
            this.radioMinute.AutoSize = true;
            this.radioMinute.Location = new System.Drawing.Point(53, 16);
            this.radioMinute.Name = "radioMinute";
            this.radioMinute.Size = new System.Drawing.Size(35, 16);
            this.radioMinute.TabIndex = 10;
            this.radioMinute.Text = "分";
            this.radioMinute.UseVisualStyleBackColor = true;
            this.radioMinute.CheckedChanged += new System.EventHandler(this.radioMinute_CheckedChanged);
            // 
            // radioMonth
            // 
            this.radioMonth.AutoSize = true;
            this.radioMonth.Location = new System.Drawing.Point(188, 16);
            this.radioMonth.Name = "radioMonth";
            this.radioMonth.Size = new System.Drawing.Size(35, 16);
            this.radioMonth.TabIndex = 13;
            this.radioMonth.Text = "月";
            this.radioMonth.UseVisualStyleBackColor = true;
            this.radioMonth.CheckedChanged += new System.EventHandler(this.radioMonth_CheckedChanged);
            // 
            // radioSecond
            // 
            this.radioSecond.AutoSize = true;
            this.radioSecond.Location = new System.Drawing.Point(12, 16);
            this.radioSecond.Name = "radioSecond";
            this.radioSecond.Size = new System.Drawing.Size(35, 16);
            this.radioSecond.TabIndex = 9;
            this.radioSecond.Text = "秒";
            this.radioSecond.UseVisualStyleBackColor = true;
            this.radioSecond.CheckedChanged += new System.EventHandler(this.radioSecond_CheckedChanged);
            // 
            // radioDay
            // 
            this.radioDay.AutoSize = true;
            this.radioDay.Checked = true;
            this.radioDay.Location = new System.Drawing.Point(147, 16);
            this.radioDay.Name = "radioDay";
            this.radioDay.Size = new System.Drawing.Size(35, 16);
            this.radioDay.TabIndex = 12;
            this.radioDay.TabStop = true;
            this.radioDay.Text = "天";
            this.radioDay.UseVisualStyleBackColor = true;
            this.radioDay.CheckedChanged += new System.EventHandler(this.radioDay_CheckedChanged);
            // 
            // radioHour
            // 
            this.radioHour.AutoSize = true;
            this.radioHour.Location = new System.Drawing.Point(94, 16);
            this.radioHour.Name = "radioHour";
            this.radioHour.Size = new System.Drawing.Size(47, 16);
            this.radioHour.TabIndex = 11;
            this.radioHour.Text = "小时";
            this.radioHour.UseVisualStyleBackColor = true;
            this.radioHour.CheckedChanged += new System.EventHandler(this.radioHour_CheckedChanged);
            // 
            // checkBoxAbnormal
            // 
            this.checkBoxAbnormal.AutoSize = true;
            this.checkBoxAbnormal.Location = new System.Drawing.Point(367, 49);
            this.checkBoxAbnormal.Name = "checkBoxAbnormal";
            this.checkBoxAbnormal.Size = new System.Drawing.Size(84, 16);
            this.checkBoxAbnormal.TabIndex = 7;
            this.checkBoxAbnormal.Text = "只显示异常";
            this.checkBoxAbnormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonLine
            // 
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Checked = true;
            this.radioButtonLine.Location = new System.Drawing.Point(314, 48);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new System.Drawing.Size(47, 16);
            this.radioButtonLine.TabIndex = 6;
            this.radioButtonLine.TabStop = true;
            this.radioButtonLine.Text = "曲线";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            // 
            // radioButtonGrid
            // 
            this.radioButtonGrid.AutoSize = true;
            this.radioButtonGrid.Location = new System.Drawing.Point(261, 48);
            this.radioButtonGrid.Name = "radioButtonGrid";
            this.radioButtonGrid.Size = new System.Drawing.Size(47, 16);
            this.radioButtonGrid.TabIndex = 5;
            this.radioButtonGrid.Text = "表格";
            this.radioButtonGrid.UseVisualStyleBackColor = true;
            // 
            // buttonQuery
            // 
            this.buttonQuery.Location = new System.Drawing.Point(468, 6);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(81, 58);
            this.buttonQuery.TabIndex = 4;
            this.buttonQuery.Text = "查询";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // panelDate
            // 
            this.panelDate.Controls.Add(this.dateTimeFrom);
            this.panelDate.Controls.Add(this.dateTimeTo);
            this.panelDate.Controls.Add(this.label2);
            this.panelDate.Location = new System.Drawing.Point(3, 3);
            this.panelDate.Name = "panelDate";
            this.panelDate.Size = new System.Drawing.Size(372, 24);
            this.panelDate.TabIndex = 3;
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Location = new System.Drawing.Point(3, 1);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(165, 21);
            this.dateTimeFrom.TabIndex = 0;
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Location = new System.Drawing.Point(199, 1);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(159, 21);
            this.dateTimeTo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "至";
            // 
            // chartSensors
            // 
            chartArea1.AxisX.LabelStyle.Format = "yyyy/MM/dd HH:mm:ss";
            chartArea1.Name = "ChartArea1";
            this.chartSensors.ChartAreas.Add(chartArea1);
            this.chartSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartSensors.Legends.Add(legend1);
            this.chartSensors.Location = new System.Drawing.Point(0, 0);
            this.chartSensors.Name = "chartSensors";
            this.chartSensors.Size = new System.Drawing.Size(609, 436);
            this.chartSensors.TabIndex = 1;
            this.chartSensors.Text = "chart1";
            this.chartSensors.Visible = false;
            // 
            // dataGridSensor
            // 
            this.dataGridSensor.AllowUserToAddRows = false;
            this.dataGridSensor.AllowUserToDeleteRows = false;
            this.dataGridSensor.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridSensor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridSensor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSensor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSiteName,
            this.ColumnSiteNum,
            this.ColumnSensorName,
            this.ColumnSensorNum,
            this.ColumnSensorType,
            this.ColumnData,
            this.ColumnTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridSensor.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridSensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSensor.Location = new System.Drawing.Point(0, 0);
            this.dataGridSensor.Name = "dataGridSensor";
            this.dataGridSensor.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridSensor.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridSensor.RowTemplate.Height = 23;
            this.dataGridSensor.Size = new System.Drawing.Size(609, 436);
            this.dataGridSensor.TabIndex = 0;
            // 
            // ColumnSiteName
            // 
            this.ColumnSiteName.DataPropertyName = "siteName";
            this.ColumnSiteName.HeaderText = "分站名";
            this.ColumnSiteName.Name = "ColumnSiteName";
            this.ColumnSiteName.ReadOnly = true;
            // 
            // ColumnSiteNum
            // 
            this.ColumnSiteNum.DataPropertyName = "siteNum";
            this.ColumnSiteNum.HeaderText = "分站号";
            this.ColumnSiteNum.Name = "ColumnSiteNum";
            this.ColumnSiteNum.ReadOnly = true;
            // 
            // ColumnSensorName
            // 
            this.ColumnSensorName.DataPropertyName = "sensorName";
            this.ColumnSensorName.HeaderText = "传感器名";
            this.ColumnSensorName.Name = "ColumnSensorName";
            this.ColumnSensorName.ReadOnly = true;
            // 
            // ColumnSensorNum
            // 
            this.ColumnSensorNum.DataPropertyName = "sensorNum";
            this.ColumnSensorNum.HeaderText = "传感器号";
            this.ColumnSensorNum.Name = "ColumnSensorNum";
            this.ColumnSensorNum.ReadOnly = true;
            // 
            // ColumnSensorType
            // 
            this.ColumnSensorType.DataPropertyName = "sensorType";
            this.ColumnSensorType.HeaderText = "传感器类型";
            this.ColumnSensorType.Name = "ColumnSensorType";
            this.ColumnSensorType.ReadOnly = true;
            // 
            // ColumnData
            // 
            this.ColumnData.DataPropertyName = "data";
            this.ColumnData.HeaderText = "数据";
            this.ColumnData.Name = "ColumnData";
            this.ColumnData.ReadOnly = true;
            // 
            // ColumnTime
            // 
            this.ColumnTime.DataPropertyName = "time";
            this.ColumnTime.HeaderText = "时间";
            this.ColumnTime.Name = "ColumnTime";
            this.ColumnTime.ReadOnly = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(362, 28);
            this.treeView1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(362, 353);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "至";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(191, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(159, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(3, 3);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(159, 21);
            this.dateTimeStart.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.siteName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(362, 321);
            this.dataGridView1.TabIndex = 0;
            // 
            // siteName
            // 
            this.siteName.HeaderText = "分站名";
            this.siteName.Name = "siteName";
            this.siteName.ReadOnly = true;
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(798, 525);
            this.Controls.Add(this.splitContainer);
            this.Name = "QueryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TabText = "查询";
            this.Text = "查询";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelDate.ResumeLayout(false);
            this.panelDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSensors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSensor)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteName;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.DataGridView dataGridSensor;
        private System.Windows.Forms.Panel panelDate;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.RadioButton radioButtonGrid;
        private System.Windows.Forms.RadioButton radioButtonLine;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSensors;
        private System.Windows.Forms.CheckBox checkBoxAbnormal;
        private System.Windows.Forms.RadioButton radioMinute;
        private System.Windows.Forms.RadioButton radioSecond;
        private System.Windows.Forms.RadioButton radioHour;
        private System.Windows.Forms.RadioButton radioMonth;
        private System.Windows.Forms.RadioButton radioDay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSiteNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSensorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSensorNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSensorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;

    }
}