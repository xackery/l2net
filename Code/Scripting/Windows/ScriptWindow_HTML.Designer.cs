namespace L2_login
{
    partial class ScriptWindow_HTML
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
            this.webBrowser_view = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser_view
            // 
            this.webBrowser_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser_view.Location = new System.Drawing.Point(0, 0);
            this.webBrowser_view.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_view.Name = "webBrowser_view";
            this.webBrowser_view.Size = new System.Drawing.Size(292, 266);
            this.webBrowser_view.TabIndex = 0;
            // 
            // ScriptWindow_HTML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.webBrowser_view);
            this.Name = "ScriptWindow_HTML";
            this.Text = "ScriptWindow_HTML";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser_view;


    }
}