namespace L2_login
{
    partial class TargetInfoScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TargetInfoScreen));
            this.label_targetinfo = new System.Windows.Forms.Label();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_targetinfo
            // 
            this.label_targetinfo.AutoSize = true;
            this.label_targetinfo.Location = new System.Drawing.Point(12, 9);
            this.label_targetinfo.Name = "label_targetinfo";
            this.label_targetinfo.Size = new System.Drawing.Size(62, 13);
            this.label_targetinfo.TabIndex = 0;
            this.label_targetinfo.Text = "Target Info:";
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(124, 4);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.button_Refresh.TabIndex = 1;
            this.button_Refresh.Text = "&Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // TargetInfoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(207, 168);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.label_targetinfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TargetInfoScreen";
            this.Text = "Target";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_targetinfo;
        private System.Windows.Forms.Button button_Refresh;
    }
}