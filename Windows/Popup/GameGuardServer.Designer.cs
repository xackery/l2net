namespace L2_login
{
    partial class GameGuardServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameGuardServer));
            this.button_gg_listen = new System.Windows.Forms.Button();
            this.textBox_gg_local_ip = new System.Windows.Forms.TextBox();
            this.textBox_gg_local_port = new System.Windows.Forms.TextBox();
            this.label_ip = new System.Windows.Forms.Label();
            this.label_localport = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_gg_listen
            // 
            this.button_gg_listen.Location = new System.Drawing.Point(90, 69);
            this.button_gg_listen.Name = "button_gg_listen";
            this.button_gg_listen.Size = new System.Drawing.Size(184, 23);
            this.button_gg_listen.TabIndex = 49;
            this.button_gg_listen.Text = "Listen";
            this.button_gg_listen.UseVisualStyleBackColor = true;
            this.button_gg_listen.Click += new System.EventHandler(this.button_gg_listen_Click);
            // 
            // textBox_gg_local_ip
            // 
            this.textBox_gg_local_ip.Location = new System.Drawing.Point(90, 12);
            this.textBox_gg_local_ip.MaxLength = 15;
            this.textBox_gg_local_ip.Name = "textBox_gg_local_ip";
            this.textBox_gg_local_ip.Size = new System.Drawing.Size(184, 20);
            this.textBox_gg_local_ip.TabIndex = 45;
            this.textBox_gg_local_ip.Text = "127.0.0.1";
            this.textBox_gg_local_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_gg_local_port
            // 
            this.textBox_gg_local_port.Location = new System.Drawing.Point(90, 38);
            this.textBox_gg_local_port.MaxLength = 5;
            this.textBox_gg_local_port.Name = "textBox_gg_local_port";
            this.textBox_gg_local_port.Size = new System.Drawing.Size(184, 20);
            this.textBox_gg_local_port.TabIndex = 47;
            this.textBox_gg_local_port.Text = "1337";
            this.textBox_gg_local_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ip
            // 
            this.label_ip.Location = new System.Drawing.Point(37, 9);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(47, 24);
            this.label_ip.TabIndex = 48;
            this.label_ip.Text = "Local IP";
            this.label_ip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_localport
            // 
            this.label_localport.Location = new System.Drawing.Point(25, 33);
            this.label_localport.Name = "label_localport";
            this.label_localport.Size = new System.Drawing.Size(59, 24);
            this.label_localport.TabIndex = 46;
            this.label_localport.Text = "Local Port";
            this.label_localport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameGuardServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 98);
            this.Controls.Add(this.button_gg_listen);
            this.Controls.Add(this.textBox_gg_local_ip);
            this.Controls.Add(this.textBox_gg_local_port);
            this.Controls.Add(this.label_ip);
            this.Controls.Add(this.label_localport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameGuardServer";
            this.Text = "Gameguard Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_gg_listen;
        private System.Windows.Forms.TextBox textBox_gg_local_ip;
        private System.Windows.Forms.TextBox textBox_gg_local_port;
        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.Label label_localport;


    }
}