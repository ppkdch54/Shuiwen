namespace Shuiwen
{
    partial class SensorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNum = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAlarmHigh = new System.Windows.Forms.TextBox();
            this.textBoxAlarmLow = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxInterval = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxSensorDeep = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxDaogui = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxFormula = new System.Windows.Forms.TextBox();
            this.lblRopeDepth = new System.Windows.Forms.Label();
            this.txtRopeDepth = new System.Windows.Forms.TextBox();
            this.lblWellDepth = new System.Windows.Forms.Label();
            this.txtWellDepth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "类型";
            // 
            // textBoxNum
            // 
            this.textBoxNum.Location = new System.Drawing.Point(69, 6);
            this.textBoxNum.Name = "textBoxNum";
            this.textBoxNum.Size = new System.Drawing.Size(203, 21);
            this.textBoxNum.TabIndex = 3;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(69, 33);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(203, 21);
            this.textBoxName.TabIndex = 4;
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "流量",
            "温度",
            "压强",
            "水位（5m）",
            "水位（50m）"});
            this.comboBoxType.Location = new System.Drawing.Point(69, 60);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxType.TabIndex = 5;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(14, 311);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(197, 311);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "取消";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "报警上限";
            // 
            // textBoxAlarmHigh
            // 
            this.textBoxAlarmHigh.Location = new System.Drawing.Point(98, 228);
            this.textBoxAlarmHigh.Name = "textBoxAlarmHigh";
            this.textBoxAlarmHigh.Size = new System.Drawing.Size(100, 21);
            this.textBoxAlarmHigh.TabIndex = 9;
            // 
            // textBoxAlarmLow
            // 
            this.textBoxAlarmLow.Location = new System.Drawing.Point(98, 256);
            this.textBoxAlarmLow.Name = "textBoxAlarmLow";
            this.textBoxAlarmLow.Size = new System.Drawing.Size(100, 21);
            this.textBoxAlarmLow.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "报警下限";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "时间间隔";
            // 
            // textBoxInterval
            // 
            this.textBoxInterval.Enabled = false;
            this.textBoxInterval.Location = new System.Drawing.Point(98, 200);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new System.Drawing.Size(100, 21);
            this.textBoxInterval.TabIndex = 16;
            this.textBoxInterval.Text = "3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(205, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "秒";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(205, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "米";
            // 
            // textBoxSensorDeep
            // 
            this.textBoxSensorDeep.Enabled = false;
            this.textBoxSensorDeep.Location = new System.Drawing.Point(98, 172);
            this.textBoxSensorDeep.Name = "textBoxSensorDeep";
            this.textBoxSensorDeep.Size = new System.Drawing.Size(100, 21);
            this.textBoxSensorDeep.TabIndex = 19;
            this.textBoxSensorDeep.Text = "0.0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 175);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "传感器深度";
            // 
            // comboBoxDaogui
            // 
            this.comboBoxDaogui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDaogui.FormattingEnabled = true;
            this.comboBoxDaogui.Items.AddRange(new object[] {
            "",
            "流入",
            "流出"});
            this.comboBoxDaogui.Location = new System.Drawing.Point(98, 283);
            this.comboBoxDaogui.Name = "comboBoxDaogui";
            this.comboBoxDaogui.Size = new System.Drawing.Size(100, 20);
            this.comboBoxDaogui.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "量程";
            // 
            // textBoxFormula
            // 
            this.textBoxFormula.Location = new System.Drawing.Point(69, 86);
            this.textBoxFormula.Name = "textBoxFormula";
            this.textBoxFormula.Size = new System.Drawing.Size(203, 21);
            this.textBoxFormula.TabIndex = 25;
            // 
            // lblRopeDepth
            // 
            this.lblRopeDepth.AutoSize = true;
            this.lblRopeDepth.Location = new System.Drawing.Point(12, 116);
            this.lblRopeDepth.Name = "lblRopeDepth";
            this.lblRopeDepth.Size = new System.Drawing.Size(77, 12);
            this.lblRopeDepth.TabIndex = 24;
            this.lblRopeDepth.Text = "绳索下放深度";
            this.lblRopeDepth.Visible = false;
            // 
            // txtRopeDepth
            // 
            this.txtRopeDepth.Location = new System.Drawing.Point(98, 113);
            this.txtRopeDepth.Name = "txtRopeDepth";
            this.txtRopeDepth.Size = new System.Drawing.Size(174, 21);
            this.txtRopeDepth.TabIndex = 25;
            this.txtRopeDepth.Visible = false;
            // 
            // lblWellDepth
            // 
            this.lblWellDepth.AutoSize = true;
            this.lblWellDepth.Location = new System.Drawing.Point(12, 143);
            this.lblWellDepth.Name = "lblWellDepth";
            this.lblWellDepth.Size = new System.Drawing.Size(53, 12);
            this.lblWellDepth.TabIndex = 24;
            this.lblWellDepth.Text = "井口标高";
            this.lblWellDepth.Visible = false;
            // 
            // txtWellDepth
            // 
            this.txtWellDepth.Location = new System.Drawing.Point(98, 140);
            this.txtWellDepth.Name = "txtWellDepth";
            this.txtWellDepth.Size = new System.Drawing.Size(174, 21);
            this.txtWellDepth.TabIndex = 25;
            this.txtWellDepth.Visible = false;
            // 
            // SensorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 350);
            this.Controls.Add(this.txtWellDepth);
            this.Controls.Add(this.lblWellDepth);
            this.Controls.Add(this.txtRopeDepth);
            this.Controls.Add(this.lblRopeDepth);
            this.Controls.Add(this.textBoxFormula);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxDaogui);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxSensorDeep);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxInterval);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxAlarmLow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxAlarmHigh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SensorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "传感器";
            this.Load += new System.EventHandler(this.SensorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNum;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAlarmHigh;
        private System.Windows.Forms.TextBox textBoxAlarmLow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxInterval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSensorDeep;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxDaogui;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxFormula;
        private System.Windows.Forms.Label lblRopeDepth;
        private System.Windows.Forms.TextBox txtRopeDepth;
        private System.Windows.Forms.Label lblWellDepth;
        private System.Windows.Forms.TextBox txtWellDepth;
    }
}