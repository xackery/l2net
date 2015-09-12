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
		private System.Windows.Forms.Label label_netversion;
		private System.Windows.Forms.Label label_version;
		private System.Windows.Forms.LinkLabel linkLabel_http;
        private Button button_close;
        private Label label_mode;
        private ListBox listBox_help;
        private Label label2;
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
            this.label_netversion = new System.Windows.Forms.Label();
            this.linkLabel_http = new System.Windows.Forms.LinkLabel();
            this.button_close = new System.Windows.Forms.Button();
            this.label_mode = new System.Windows.Forms.Label();
            this.listBox_help = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.label_version.Text = "2015-??-??";
            this.label_version.Click += new System.EventHandler(this.label_version_Click);
            // 
            // label_netversion
            // 
            this.label_netversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_netversion.Location = new System.Drawing.Point(49, 210);
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
            this.button_close.Location = new System.Drawing.Point(110, 234);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label_mode
            // 
            this.label_mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_mode.Location = new System.Drawing.Point(49, 185);
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
            "mpj123 for making this open source",
            "slothmo for being lead programmer",
            "Oddi",
            "obce",
            "joe",
            "Agita",
            "Fallen_A",
            "sespark",
            "ImBored",
            "lhdlp",
            "Jeapordy",
            "Spider",
            "deMEV",
            "escabuchen"});
            this.listBox_help.Location = new System.Drawing.Point(12, 87);
            this.listBox_help.Name = "listBox_help";
            this.listBox_help.Size = new System.Drawing.Size(266, 121);
            this.listBox_help.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(97, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "special thanks to";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(290, 262);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox_help);
            this.Controls.Add(this.label_mode);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.linkLabel_http);
            this.Controls.Add(this.label_netversion);
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

        private void label_version_Click(object sender, EventArgs e)
        {

        }
    }
}
