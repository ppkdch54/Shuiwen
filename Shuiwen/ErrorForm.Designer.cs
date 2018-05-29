namespace Shuiwen
{
    partial class ErrorForm
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
            this.listBoxError = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxError
            // 
            this.listBoxError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxError.FormattingEnabled = true;
            this.listBoxError.ItemHeight = 12;
            this.listBoxError.Location = new System.Drawing.Point(0, 0);
            this.listBoxError.Name = "listBoxError";
            this.listBoxError.Size = new System.Drawing.Size(284, 256);
            this.listBoxError.TabIndex = 0;
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listBoxError);
            this.Name = "ErrorForm";
            this.TabText = "ErrorForm";
            this.Text = "ErrorForm";
            this.Load += new System.EventHandler(this.ErrorForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ErrorForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxError;
    }
}