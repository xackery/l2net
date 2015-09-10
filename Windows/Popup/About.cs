using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace L2_login
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label_netversion;
		private System.Windows.Forms.Label label_version;
		private System.Windows.Forms.LinkLabel linkLabel_http;
        private Button button_close;
        private Label label_mode;
        private ListBox listBox_help;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


            label_mode.Text = "Running in FULL mode";

            string cpu = "";
            switch(IntPtr.Size)
            {
                case 4:
                    cpu = "x86";
                    break;
                case 8:
                    cpu = "x64";
                    break;
            }

            label_version.Text = "rev " + Globals.Version + Globals.VersionLetter;

            label_netversion.Text = ".Net version " + Environment.Version.Major.ToString() + "." + Environment.Version.Minor.ToString() + "." + Environment.Version.Revision.ToString() + " " + cpu;

			UpdateUI();
		}

		public void UpdateUI()
		{
            this.Text = Globals.m_ResourceManager.GetString("menuItem_about");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_netversion = new System.Windows.Forms.Label();
            this.linkLabel_http = new System.Windows.Forms.LinkLabel();
            this.button_close = new System.Windows.Forms.Button();
            this.label_mode = new System.Windows.Forms.Label();
            this.listBox_help = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "L2.Net";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_version
            // 
            this.label_version.Location = new System.Drawing.Point(199, 30);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(88, 16);
            this.label_version.TabIndex = 1;
            this.label_version.Text = "v0.xxa";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "slothmo";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(171, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "lead programmer";
            // 
            // label_netversion
            // 
            this.label_netversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_netversion.Location = new System.Drawing.Point(49, 421);
            this.label_netversion.Name = "label_netversion";
            this.label_netversion.Size = new System.Drawing.Size(192, 23);
            this.label_netversion.TabIndex = 13;
            this.label_netversion.Text = "net version";
            this.label_netversion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel_http
            // 
            this.linkLabel_http.Location = new System.Drawing.Point(0, 42);
            this.linkLabel_http.Name = "linkLabel_http";
            this.linkLabel_http.Size = new System.Drawing.Size(290, 23);
            this.linkLabel_http.TabIndex = 14;
            this.linkLabel_http.TabStop = true;
            this.linkLabel_http.Text = "http://l2net.insane-gamers.com";
            this.linkLabel_http.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_http.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_http_LinkClicked);
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_close.Location = new System.Drawing.Point(110, 445);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label_mode
            // 
            this.label_mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_mode.Location = new System.Drawing.Point(49, 396);
            this.label_mode.Name = "label_mode";
            this.label_mode.Size = new System.Drawing.Size(192, 23);
            this.label_mode.TabIndex = 17;
            this.label_mode.Text = "mode";
            this.label_mode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox_help
            // 
            this.listBox_help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_help.FormattingEnabled = true;
            this.listBox_help.Items.AddRange(new object[] {
            "Fallen_A",
            "sespark",
            "ImBored",
            "lhdlp",
            "Jeapordy",
            "Spider",
            "deMEV",
            "escabuchen"});
            this.listBox_help.Location = new System.Drawing.Point(63, 298);
            this.listBox_help.Name = "listBox_help";
            this.listBox_help.Size = new System.Drawing.Size(178, 121);
            this.listBox_help.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(97, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "special thanks to";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(171, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 24);
            this.label4.TabIndex = 21;
            this.label4.Text = "programmer";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(61, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 32);
            this.label5.TabIndex = 20;
            this.label5.Text = "Oddi";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(171, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 24);
            this.label7.TabIndex = 23;
            this.label7.Text = "programmer";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(61, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 32);
            this.label8.TabIndex = 22;
            this.label8.Text = "mpj123";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(171, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 24);
            this.label9.TabIndex = 25;
            this.label9.Text = "programmer";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(61, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 32);
            this.label10.TabIndex = 24;
            this.label10.Text = "obce";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(171, 220);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 24);
            this.label11.TabIndex = 27;
            this.label11.Text = "programmer";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(61, 207);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 32);
            this.label12.TabIndex = 26;
            this.label12.Text = "joe";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(171, 252);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 24);
            this.label13.TabIndex = 29;
            this.label13.Text = "programmer";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(61, 239);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 32);
            this.label14.TabIndex = 28;
            this.label14.Text = "Agita";
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(290, 473);
            this.ControlBox = false;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox_help);
            this.Controls.Add(this.label_mode);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.linkLabel_http);
            this.Controls.Add(this.label_netversion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.TopMost = true;
            this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel_http_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(linkLabel_http.Text);
			}
			catch
			{
				//problem opening browser?
			}
		}

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
