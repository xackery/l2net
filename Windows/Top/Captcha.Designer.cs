namespace L2_login
{
    partial class Captcha
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Captcha));
            this.pictureBox_captcha = new System.Windows.Forms.PictureBox();
            this.textBox_captcha = new System.Windows.Forms.TextBox();
            this.button_send_captcha = new System.Windows.Forms.Button();
            this.timer_captcha = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_captcha)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_captcha
            // 
            this.pictureBox_captcha.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_captcha.Name = "pictureBox_captcha";
            this.pictureBox_captcha.Size = new System.Drawing.Size(256, 64);
            this.pictureBox_captcha.TabIndex = 0;
            this.pictureBox_captcha.TabStop = false;
            // 
            // textBox_captcha
            // 
            this.textBox_captcha.Location = new System.Drawing.Point(12, 82);
            this.textBox_captcha.Name = "textBox_captcha";
            this.textBox_captcha.Size = new System.Drawing.Size(256, 20);
            this.textBox_captcha.TabIndex = 1;
            this.textBox_captcha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_captcha_KeyPress);
            // 
            // button_send_captcha
            // 
            this.button_send_captcha.Location = new System.Drawing.Point(12, 108);
            this.button_send_captcha.Name = "button_send_captcha";
            this.button_send_captcha.Size = new System.Drawing.Size(256, 23);
            this.button_send_captcha.TabIndex = 2;
            this.button_send_captcha.Text = "Send";
            this.button_send_captcha.UseVisualStyleBackColor = true;
            this.button_send_captcha.Click += new System.EventHandler(this.button_send_captcha_Click);
            this.button_send_captcha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button_send_captcha_KeyPress);
            // 
            // timer_captcha
            // 
            this.timer_captcha.Interval = 1000;
            this.timer_captcha.Tick += new System.EventHandler(this.timer_captcha_Tick);
            // 
            // Captcha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(282, 146);
            this.Controls.Add(this.button_send_captcha);
            this.Controls.Add(this.textBox_captcha);
            this.Controls.Add(this.pictureBox_captcha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Captcha";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "~Captcha~";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_captcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_captcha;
        private System.Windows.Forms.TextBox textBox_captcha;
        private System.Windows.Forms.Button button_send_captcha;
        private System.Windows.Forms.Timer timer_captcha;
    }
}