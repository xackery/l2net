namespace L2_login
{
    partial class GameGuardClient
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
            this.button_gg_connect = new System.Windows.Forms.Button();
            this.textBox_gg_srv_ip = new System.Windows.Forms.TextBox();
            this.textBox_gg_srv_port = new System.Windows.Forms.TextBox();
            this.label_srvip = new System.Windows.Forms.Label();
            this.label_srvport = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_gg_connect
            // 
            this.button_gg_connect.Location = new System.Drawing.Point(76, 69);
            this.button_gg_connect.Name = "button_gg_connect";
            this.button_gg_connect.Size = new System.Drawing.Size(184, 23);
            this.button_gg_connect.TabIndex = 54;
            this.button_gg_connect.Text = "Connect";
            this.button_gg_connect.UseVisualStyleBackColor = true;
            this.button_gg_connect.Click += new System.EventHandler(this.button_gg_connect_Click);
            // 
            // textBox_gg_srv_ip
            // 
            this.textBox_gg_srv_ip.Location = new System.Drawing.Point(76, 12);
            this.textBox_gg_srv_ip.MaxLength = 15;
            this.textBox_gg_srv_ip.Name = "textBox_gg_srv_ip";
            this.textBox_gg_srv_ip.Size = new System.Drawing.Size(184, 20);
            this.textBox_gg_srv_ip.TabIndex = 50;
            this.textBox_gg_srv_ip.Text = "127.0.0.1";
            this.textBox_gg_srv_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_gg_srv_port
            // 
            this.textBox_gg_srv_port.Location = new System.Drawing.Point(76, 38);
            this.textBox_gg_srv_port.MaxLength = 5;
            this.textBox_gg_srv_port.Name = "textBox_gg_srv_port";
            this.textBox_gg_srv_port.Size = new System.Drawing.Size(184, 20);
            this.textBox_gg_srv_port.TabIndex = 52;
            this.textBox_gg_srv_port.Text = "1337";
            this.textBox_gg_srv_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_srvip
            // 
            this.label_srvip.Location = new System.Drawing.Point(2, 9);
            this.label_srvip.Name = "label_srvip";
            this.label_srvip.Size = new System.Drawing.Size(68, 24);
            this.label_srvip.TabIndex = 53;
            this.label_srvip.Text = "Server IP";
            this.label_srvip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_srvport
            // 
            this.label_srvport.Location = new System.Drawing.Point(2, 34);
            this.label_srvport.Name = "label_srvport";
            this.label_srvport.Size = new System.Drawing.Size(68, 24);
            this.label_srvport.TabIndex = 51;
            this.label_srvport.Text = "Server Port";
            this.label_srvport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 55;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GameGuardClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 104);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_gg_connect);
            this.Controls.Add(this.textBox_gg_srv_ip);
            this.Controls.Add(this.textBox_gg_srv_port);
            this.Controls.Add(this.label_srvip);
            this.Controls.Add(this.label_srvport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameGuardClient";
            this.Text = "GameGuard Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_gg_connect;
        private System.Windows.Forms.TextBox textBox_gg_srv_ip;
        private System.Windows.Forms.TextBox textBox_gg_srv_port;
        private System.Windows.Forms.Label label_srvip;
        private System.Windows.Forms.Label label_srvport;
        private System.Windows.Forms.Button button1;
    }
}