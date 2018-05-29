namespace Shuiwen
{
    partial class SensorMsgForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSensorName = new System.Windows.Forms.Panel();
            this.textBoxSensorName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSensorNum = new System.Windows.Forms.Panel();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxSensorNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelData = new System.Windows.Forms.Panel();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.panelUpdateTime = new System.Windows.Forms.Panel();
            this.textBoxLastTime = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelSensorName.SuspendLayout();
            this.panelSensorNum.SuspendLayout();
            this.panelData.SuspendLayout();
            this.panelUpdateTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelSensorName);
            this.flowLayoutPanel1.Controls.Add(this.panelSensorNum);
            this.flowLayoutPanel1.Controls.Add(this.panelData);
            this.flowLayoutPanel1.Controls.Add(this.panelUpdateTime);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(641, 137);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panelSensorName
            // 
            this.panelSensorName.Controls.Add(this.textBoxSensorName);
            this.panelSensorName.Controls.Add(this.label1);
            this.panelSensorName.Location = new System.Drawing.Point(1, 1);
            this.panelSensorName.Margin = new System.Windows.Forms.Padding(1);
            this.panelSensorName.Name = "panelSensorName";
            this.panelSensorName.Size = new System.Drawing.Size(148, 25);
            this.panelSensorName.TabIndex = 2;
            // 
            // textBoxSensorName
            // 
            this.textBoxSensorName.Location = new System.Drawing.Point(37, 2);
            this.textBoxSensorName.Name = "textBoxSensorName";
            this.textBoxSensorName.ReadOnly = true;
            this.textBoxSensorName.Size = new System.Drawing.Size(101, 21);
            this.textBoxSensorName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // panelSensorNum
            // 
            this.panelSensorNum.Controls.Add(this.textBoxType);
            this.panelSensorNum.Controls.Add(this.textBoxSensorNum);
            this.panelSensorNum.Controls.Add(this.label2);
            this.panelSensorNum.Location = new System.Drawing.Point(151, 1);
            this.panelSensorNum.Margin = new System.Windows.Forms.Padding(1);
            this.panelSensorNum.Name = "panelSensorNum";
            this.panelSensorNum.Size = new System.Drawing.Size(148, 25);
            this.panelSensorNum.TabIndex = 3;
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(86, 1);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.Size = new System.Drawing.Size(52, 21);
            this.textBoxType.TabIndex = 1;
            // 
            // textBoxSensorNum
            // 
            this.textBoxSensorNum.Location = new System.Drawing.Point(37, 1);
            this.textBoxSensorNum.Name = "textBoxSensorNum";
            this.textBoxSensorNum.ReadOnly = true;
            this.textBoxSensorNum.Size = new System.Drawing.Size(39, 21);
            this.textBoxSensorNum.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "编号";
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.textBoxData);
            this.panelData.Location = new System.Drawing.Point(301, 1);
            this.panelData.Margin = new System.Windows.Forms.Padding(1);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(135, 33);
            this.panelData.TabIndex = 4;
            // 
            // textBoxData
            // 
            this.textBoxData.BackColor = System.Drawing.Color.Black;
            this.textBoxData.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxData.ForeColor = System.Drawing.Color.Lime;
            this.textBoxData.Location = new System.Drawing.Point(2, 1);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.ReadOnly = true;
            this.textBoxData.Size = new System.Drawing.Size(128, 31);
            this.textBoxData.TabIndex = 1;
            // 
            // panelUpdateTime
            // 
            this.panelUpdateTime.Controls.Add(this.textBoxLastTime);
            this.panelUpdateTime.Location = new System.Drawing.Point(438, 1);
            this.panelUpdateTime.Margin = new System.Windows.Forms.Padding(1);
            this.panelUpdateTime.Name = "panelUpdateTime";
            this.panelUpdateTime.Size = new System.Drawing.Size(148, 25);
            this.panelUpdateTime.TabIndex = 3;
            // 
            // textBoxLastTime
            // 
            this.textBoxLastTime.Location = new System.Drawing.Point(3, 1);
            this.textBoxLastTime.Name = "textBoxLastTime";
            this.textBoxLastTime.ReadOnly = true;
            this.textBoxLastTime.Size = new System.Drawing.Size(128, 21);
            this.textBoxLastTime.TabIndex = 1;
            // 
            // SensorMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 137);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SensorMsgForm";
            this.Text = "SensorMsgForm";
            this.Load += new System.EventHandler(this.SensorMsgForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelSensorName.ResumeLayout(false);
            this.panelSensorName.PerformLayout();
            this.panelSensorNum.ResumeLayout(false);
            this.panelSensorNum.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.panelUpdateTime.ResumeLayout(false);
            this.panelUpdateTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelSensorName;
        private System.Windows.Forms.TextBox textBoxSensorName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSensorNum;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.TextBox textBoxSensorNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.Panel panelUpdateTime;
        private System.Windows.Forms.TextBox textBoxLastTime;

    }
}