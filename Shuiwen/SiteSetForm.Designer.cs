namespace Shuiwen
{
    partial class SiteSetForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNum = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.listViewSensors = new System.Windows.Forms.ListView();
            this.columnNum = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnType = new System.Windows.Forms.ColumnHeader();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.comConfigControl1 = new ComConfig.ComConfigControl();
            this.groupBoxTcp = new System.Windows.Forms.GroupBox();
            this.radioSerial = new System.Windows.Forms.RadioButton();
            this.radioTcp = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioSMS = new System.Windows.Forms.RadioButton();
            this.radioGPRS = new System.Windows.Forms.RadioButton();
            this.groupBoxTcp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "端口";
            // 
            // textBoxNum
            // 
            this.textBoxNum.Location = new System.Drawing.Point(57, 12);
            this.textBoxNum.Name = "textBoxNum";
            this.textBoxNum.Size = new System.Drawing.Size(203, 21);
            this.textBoxNum.TabIndex = 4;
            this.textBoxNum.Text = "1";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(354, 13);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(209, 21);
            this.textBoxName.TabIndex = 5;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(46, 30);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(209, 21);
            this.textBoxIP.TabIndex = 6;
            // 
            // textBoxPort
            // 
            this.textBoxPort.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxPort.Location = new System.Drawing.Point(46, 57);
            this.textBoxPort.MaxLength = 5;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(209, 21);
            this.textBoxPort.TabIndex = 7;
            this.textBoxPort.Text = "12345";
            // 
            // listViewSensors
            // 
            this.listViewSensors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNum,
            this.columnName,
            this.columnType});
            this.listViewSensors.FullRowSelect = true;
            this.listViewSensors.GridLines = true;
            this.listViewSensors.Location = new System.Drawing.Point(12, 276);
            this.listViewSensors.Name = "listViewSensors";
            this.listViewSensors.Size = new System.Drawing.Size(560, 186);
            this.listViewSensors.TabIndex = 8;
            this.listViewSensors.UseCompatibleStateImageBehavior = false;
            this.listViewSensors.View = System.Windows.Forms.View.Details;
            // 
            // columnNum
            // 
            this.columnNum.Text = "编号";
            // 
            // columnName
            // 
            this.columnName.Text = "名称";
            // 
            // columnType
            // 
            this.columnType.Text = "类型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "传感器列表";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(14, 468);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 10;
            this.buttonAdd.Text = "添加";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(242, 468);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(75, 23);
            this.buttonModify.TabIndex = 11;
            this.buttonModify.Text = "修改";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(490, 468);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(75, 23);
            this.buttonDel.TabIndex = 12;
            this.buttonDel.Text = "删除";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(490, 506);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(242, 506);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // comConfigControl1
            // 
            this.comConfigControl1.Location = new System.Drawing.Point(12, 74);
            this.comConfigControl1.Name = "comConfigControl1";
            this.comConfigControl1.Size = new System.Drawing.Size(286, 175);
            this.comConfigControl1.TabIndex = 15;
            // 
            // groupBoxTcp
            // 
            this.groupBoxTcp.Controls.Add(this.label3);
            this.groupBoxTcp.Controls.Add(this.label4);
            this.groupBoxTcp.Controls.Add(this.textBoxIP);
            this.groupBoxTcp.Controls.Add(this.textBoxPort);
            this.groupBoxTcp.Location = new System.Drawing.Point(304, 74);
            this.groupBoxTcp.Name = "groupBoxTcp";
            this.groupBoxTcp.Size = new System.Drawing.Size(261, 166);
            this.groupBoxTcp.TabIndex = 16;
            this.groupBoxTcp.TabStop = false;
            this.groupBoxTcp.Text = "网口设置";
            // 
            // radioSerial
            // 
            this.radioSerial.AutoSize = true;
            this.radioSerial.Location = new System.Drawing.Point(25, 6);
            this.radioSerial.Name = "radioSerial";
            this.radioSerial.Size = new System.Drawing.Size(47, 16);
            this.radioSerial.TabIndex = 18;
            this.radioSerial.TabStop = true;
            this.radioSerial.Text = "串口";
            this.radioSerial.UseVisualStyleBackColor = true;
            this.radioSerial.CheckedChanged += new System.EventHandler(this.radioSerial_CheckedChanged);
            // 
            // radioTcp
            // 
            this.radioTcp.AutoSize = true;
            this.radioTcp.Location = new System.Drawing.Point(108, 6);
            this.radioTcp.Name = "radioTcp";
            this.radioTcp.Size = new System.Drawing.Size(47, 16);
            this.radioTcp.TabIndex = 19;
            this.radioTcp.Text = "网口";
            this.radioTcp.UseVisualStyleBackColor = true;
            this.radioTcp.CheckedChanged += new System.EventHandler(this.radioTcp_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "通信方式";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioGPRS);
            this.panel1.Controls.Add(this.radioSMS);
            this.panel1.Controls.Add(this.radioSerial);
            this.panel1.Controls.Add(this.radioTcp);
            this.panel1.Location = new System.Drawing.Point(103, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 28);
            this.panel1.TabIndex = 20;
            // 
            // radioSMS
            // 
            this.radioSMS.AutoSize = true;
            this.radioSMS.Location = new System.Drawing.Point(191, 6);
            this.radioSMS.Name = "radioSMS";
            this.radioSMS.Size = new System.Drawing.Size(47, 16);
            this.radioSMS.TabIndex = 20;
            this.radioSMS.Text = "短信";
            this.radioSMS.UseVisualStyleBackColor = true;
            this.radioSMS.CheckedChanged += new System.EventHandler(this.radioSMS_CheckedChanged);
            // 
            // radioGPRS
            // 
            this.radioGPRS.AutoSize = true;
            this.radioGPRS.Location = new System.Drawing.Point(274, 6);
            this.radioGPRS.Name = "radioGPRS";
            this.radioGPRS.Size = new System.Drawing.Size(47, 16);
            this.radioGPRS.TabIndex = 20;
            this.radioGPRS.Text = "GPRS";
            this.radioGPRS.UseVisualStyleBackColor = true;
            this.radioGPRS.CheckedChanged += new System.EventHandler(this.radioSiteComm_CheckedChanged);
            // 
            // SiteSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBoxTcp);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.comConfigControl1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonModify);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listViewSensors);
            this.Name = "SiteSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SiteSetForm";
            this.Load += new System.EventHandler(this.SiteSetForm_Load);
            this.groupBoxTcp.ResumeLayout(false);
            this.groupBoxTcp.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNum;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.ListView listViewSensors;
        private System.Windows.Forms.ColumnHeader columnNum;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOK;
        private ComConfig.ComConfigControl comConfigControl1;
        private System.Windows.Forms.GroupBox groupBoxTcp;
        private System.Windows.Forms.RadioButton radioSerial;
        private System.Windows.Forms.RadioButton radioTcp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioSMS;
        private System.Windows.Forms.RadioButton radioGPRS;
    }
}