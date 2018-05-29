namespace Shuiwen
{
    partial class SitesStateForm
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
            this.flowLayouSites = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayouSites
            // 
            this.flowLayouSites.AutoScroll = true;
            this.flowLayouSites.BackColor = System.Drawing.Color.Silver;
            this.flowLayouSites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayouSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayouSites.Location = new System.Drawing.Point(0, 0);
            this.flowLayouSites.Name = "flowLayouSites";
            this.flowLayouSites.Size = new System.Drawing.Size(674, 333);
            this.flowLayouSites.TabIndex = 0;
            // 
            // SitesStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 333);
            this.Controls.Add(this.flowLayouSites);
            this.Name = "SitesStateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TabText = "实时数据";
            this.Text = "实时数据";
            this.Load += new System.EventHandler(this.SitesStateForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayouSites;
    }
}