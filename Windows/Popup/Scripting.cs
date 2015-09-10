using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace L2_login
{
	/// <summary>
	/// Summary description for Scripting.
	/// </summary>
	public class Scripting : System.Windows.Forms.Form
	{
		public System.Windows.Forms.RichTextBox richTextBox_script;
		private System.Windows.Forms.Button button_loadscript;
		private System.Windows.Forms.Button button_savescript;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button button_close;
		private System.Windows.Forms.Button button_top;
		private System.Windows.Forms.Label label_linenum;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Scripting()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			UpdateUI();

			richTextBox_script.LinkClicked += new LinkClickedEventHandler(richTextBox_script_LinkClicked);
			richTextBox_script.SelectionChanged += new EventHandler(richTextBox_script_SelectionChanged);
		}

		public void UpdateUI()
		{
            this.Text = Globals.m_ResourceManager.GetString("menuItem_scripting");
            button_close.Text = Globals.m_ResourceManager.GetString("button_npc_close");
            button_loadscript.Text = Globals.m_ResourceManager.GetString("button_loadscript");
            button_savescript.Text = Globals.m_ResourceManager.GetString("button_savescript");
            button_top.Text = Globals.m_ResourceManager.GetString("button_top");
			this.Refresh();
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
            this.richTextBox_script = new System.Windows.Forms.RichTextBox();
            this.button_loadscript = new System.Windows.Forms.Button();
            this.button_savescript = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button_close = new System.Windows.Forms.Button();
            this.button_top = new System.Windows.Forms.Button();
            this.label_linenum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox_script
            // 
            this.richTextBox_script.AcceptsTab = true;
            this.richTextBox_script.AllowDrop = true;
            this.richTextBox_script.Location = new System.Drawing.Point(8, 40);
            this.richTextBox_script.Name = "richTextBox_script";
            this.richTextBox_script.Size = new System.Drawing.Size(544, 456);
            this.richTextBox_script.TabIndex = 4;
            this.richTextBox_script.Text = "";
            // 
            // button_loadscript
            // 
            this.button_loadscript.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_loadscript.Location = new System.Drawing.Point(8, 8);
            this.button_loadscript.Name = "button_loadscript";
            this.button_loadscript.Size = new System.Drawing.Size(144, 23);
            this.button_loadscript.TabIndex = 0;
            this.button_loadscript.Text = "Load Script";
            this.button_loadscript.Click += new System.EventHandler(this.button_loadscript_Click);
            // 
            // button_savescript
            // 
            this.button_savescript.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_savescript.Location = new System.Drawing.Point(160, 8);
            this.button_savescript.Name = "button_savescript";
            this.button_savescript.Size = new System.Drawing.Size(144, 23);
            this.button_savescript.TabIndex = 1;
            this.button_savescript.Text = "Save Script";
            this.button_savescript.Click += new System.EventHandler(this.button_savescript_Click);
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_close.Location = new System.Drawing.Point(462, 8);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(88, 23);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_top
            // 
            this.button_top.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_top.Location = new System.Drawing.Point(352, 8);
            this.button_top.Name = "button_top";
            this.button_top.Size = new System.Drawing.Size(72, 23);
            this.button_top.TabIndex = 2;
            this.button_top.Text = "Top";
            this.button_top.Click += new System.EventHandler(this.button_top_Click);
            // 
            // label_linenum
            // 
            this.label_linenum.Location = new System.Drawing.Point(424, 504);
            this.label_linenum.Name = "label_linenum";
            this.label_linenum.Size = new System.Drawing.Size(128, 16);
            this.label_linenum.TabIndex = 5;
            this.label_linenum.Text = "0";
            this.label_linenum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Scripting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(562, 526);
            this.ControlBox = false;
            this.Controls.Add(this.label_linenum);
            this.Controls.Add(this.button_top);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_savescript);
            this.Controls.Add(this.button_loadscript);
            this.Controls.Add(this.richTextBox_script);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Scripting";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Editor";
            this.ResumeLayout(false);

		}
		#endregion

		private void button_loadscript_Click(object sender, System.EventArgs e)
		{
			//load script
			this.Enabled = false;//diable the screen

			try
			{
				DialogResult okOrNo;//dialog return value, if it is DialogResult.OK then everything is OK

                openFileDialog1.InitialDirectory = Globals.PATH + "\\Scripts";//Initial dir, where it begins looking at.
				openFileDialog1.Filter = "Script Files (*.l2s)|*.l2s";//this particualr format is for one file type.
				openFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
				openFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
				okOrNo = openFileDialog1.ShowDialog();//open the dialog box and save the result.			
			
				if(okOrNo == DialogResult.OK)//if the dialog box works
				{
					richTextBox_script.Text = "";

					StreamReader filein = new StreamReader(openFileDialog1.OpenFile());//create a new streamwritter from the stream it returns
					ReadScript(filein);//load everything
					filein.Close();//close the file
				}
			}
			catch
			{
				Globals.l2net_home.Add_Error("ERROR WHILE LOADING SCRIPT!"+System.Environment.NewLine+"Is the script running?");
			}

			this.Enabled = true;//renable everything
		}

		private void button_savescript_Click(object sender, System.EventArgs e)
		{
			//save script
			this.Enabled = false;

			try
			{
				DialogResult okOrNo;//dialog return value, if it is DialogResult.OK then everything is OK

                saveFileDialog1.InitialDirectory = Globals.PATH + "\\Scripts";//Initial dir, where it begins looking at.
				saveFileDialog1.Filter = "Script Files (*.l2s)|*.l2s";//this particualr format is for one file type.
				saveFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
				saveFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
				okOrNo = saveFileDialog1.ShowDialog();//open the dialog box and save the result.			
			
				if(okOrNo == DialogResult.OK)//if the dialog box displays OK
				{
					StreamWriter fileout = new StreamWriter(saveFileDialog1.OpenFile());//create a streamwritter from the stream the dialog box returns
					StoreScript(fileout);//store everything
					fileout.Close();//close the file
				}
			}
			catch
			{
				Globals.l2net_home.Add_Error("ERROR WHILE SAVING SCRIPT!"+System.Environment.NewLine+"Is the script running?");
			}

			this.Enabled = true;
		}

		private void ReadScript(StreamReader file)
		{
			string line;

			while((line = file.ReadLine()) != null)
			{
				richTextBox_script.SelectedText = line + Environment.NewLine;
			}
		}

		private void StoreScript(StreamWriter file)
		{
			foreach(string line in richTextBox_script.Lines)
			{
				file.WriteLine(line);
			}
		}

		private void button_close_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		private void button_top_Click(object sender, System.EventArgs e)
		{
			this.TopMost = !this.TopMost;
		}

		private void richTextBox_script_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(e.LinkText);
			}
			catch
			{
				//problem opening browser?
			}
		}

		private void Set_LineLoc()
		{
			string old = label_linenum.Text;
			label_linenum.Text = (richTextBox_script.GetLineFromCharIndex(richTextBox_script.SelectionStart)).ToString();

            if (!System.String.Equals(old, label_linenum.Text))
			{
				label_linenum.Refresh();
			}
		}

		private void richTextBox_script_SelectionChanged(object sender, EventArgs e)
		{
			Set_LineLoc();
		}
	}//end of class
}
