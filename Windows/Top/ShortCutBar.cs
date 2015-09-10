using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace L2_login
{
	/// <summary>
	/// Summary description for ShortCutBar.
	/// </summary>
	public class ShortCutBar : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_s1;
		private System.Windows.Forms.Button button_s2;
		private System.Windows.Forms.Button button_s3;
		private System.Windows.Forms.Button button_s4;
		private System.Windows.Forms.Button button_s5;
		private System.Windows.Forms.Button button_s6;
		private System.Windows.Forms.Button button_s7;
		private System.Windows.Forms.Button button_s8;
		private System.Windows.Forms.Button button_s9;
		private System.Windows.Forms.Button button_s10;
		private System.Windows.Forms.Button button_s11;
		private System.Windows.Forms.Button button_s12;
		private System.Windows.Forms.Button button_left;
		private System.Windows.Forms.Button button_right;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label_page;

		private int page = 0;

		public ShortCutBar()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Load_Images();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortCutBar));
			this.button_s1 = new System.Windows.Forms.Button();
			this.button_s2 = new System.Windows.Forms.Button();
			this.button_s3 = new System.Windows.Forms.Button();
			this.button_s4 = new System.Windows.Forms.Button();
			this.button_s5 = new System.Windows.Forms.Button();
			this.button_s6 = new System.Windows.Forms.Button();
			this.button_s7 = new System.Windows.Forms.Button();
			this.button_s8 = new System.Windows.Forms.Button();
			this.button_s9 = new System.Windows.Forms.Button();
			this.button_s10 = new System.Windows.Forms.Button();
			this.button_s11 = new System.Windows.Forms.Button();
			this.button_s12 = new System.Windows.Forms.Button();
			this.button_left = new System.Windows.Forms.Button();
			this.button_right = new System.Windows.Forms.Button();
			this.label_page = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button_s1
			// 
			this.button_s1.BackColor = System.Drawing.Color.Transparent;
			this.button_s1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s1.Location = new System.Drawing.Point(7, 33);
			this.button_s1.Name = "button_s1";
			this.button_s1.Size = new System.Drawing.Size(32, 32);
			this.button_s1.TabIndex = 0;
			this.button_s1.Click += new System.EventHandler(this.button_s1_Click);
			// 
			// button_s2
			// 
			this.button_s2.BackColor = System.Drawing.Color.Transparent;
			this.button_s2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s2.Location = new System.Drawing.Point(7, 70);
			this.button_s2.Name = "button_s2";
			this.button_s2.Size = new System.Drawing.Size(32, 32);
			this.button_s2.TabIndex = 1;
			this.button_s2.Click += new System.EventHandler(this.button_s2_Click);
			// 
			// button_s3
			// 
			this.button_s3.BackColor = System.Drawing.Color.Transparent;
			this.button_s3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s3.Location = new System.Drawing.Point(7, 107);
			this.button_s3.Name = "button_s3";
			this.button_s3.Size = new System.Drawing.Size(32, 32);
			this.button_s3.TabIndex = 2;
			this.button_s3.Click += new System.EventHandler(this.button_s3_Click);
			// 
			// button_s4
			// 
			this.button_s4.BackColor = System.Drawing.Color.Transparent;
			this.button_s4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s4.Location = new System.Drawing.Point(7, 144);
			this.button_s4.Name = "button_s4";
			this.button_s4.Size = new System.Drawing.Size(32, 32);
			this.button_s4.TabIndex = 3;
			this.button_s4.Click += new System.EventHandler(this.button_s4_Click);
			// 
			// button_s5
			// 
			this.button_s5.BackColor = System.Drawing.Color.Transparent;
			this.button_s5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s5.Location = new System.Drawing.Point(7, 186);
			this.button_s5.Name = "button_s5";
			this.button_s5.Size = new System.Drawing.Size(32, 32);
			this.button_s5.TabIndex = 4;
			this.button_s5.Click += new System.EventHandler(this.button_s5_Click);
			// 
			// button_s6
			// 
			this.button_s6.BackColor = System.Drawing.Color.Transparent;
			this.button_s6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s6.Location = new System.Drawing.Point(7, 223);
			this.button_s6.Name = "button_s6";
			this.button_s6.Size = new System.Drawing.Size(32, 32);
			this.button_s6.TabIndex = 5;
			this.button_s6.Click += new System.EventHandler(this.button_s6_Click);
			// 
			// button_s7
			// 
			this.button_s7.BackColor = System.Drawing.Color.Transparent;
			this.button_s7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s7.Location = new System.Drawing.Point(7, 260);
			this.button_s7.Name = "button_s7";
			this.button_s7.Size = new System.Drawing.Size(32, 32);
			this.button_s7.TabIndex = 6;
			this.button_s7.Click += new System.EventHandler(this.button_s7_Click);
			// 
			// button_s8
			// 
			this.button_s8.BackColor = System.Drawing.Color.Transparent;
			this.button_s8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s8.Location = new System.Drawing.Point(7, 297);
			this.button_s8.Name = "button_s8";
			this.button_s8.Size = new System.Drawing.Size(32, 32);
			this.button_s8.TabIndex = 7;
			this.button_s8.Click += new System.EventHandler(this.button_s8_Click);
			// 
			// button_s9
			// 
			this.button_s9.BackColor = System.Drawing.Color.Transparent;
			this.button_s9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s9.Location = new System.Drawing.Point(7, 339);
			this.button_s9.Name = "button_s9";
			this.button_s9.Size = new System.Drawing.Size(32, 32);
			this.button_s9.TabIndex = 8;
			this.button_s9.Click += new System.EventHandler(this.button_s9_Click);
			// 
			// button_s10
			// 
			this.button_s10.BackColor = System.Drawing.Color.Transparent;
			this.button_s10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s10.Location = new System.Drawing.Point(7, 376);
			this.button_s10.Name = "button_s10";
			this.button_s10.Size = new System.Drawing.Size(32, 32);
			this.button_s10.TabIndex = 9;
			this.button_s10.Click += new System.EventHandler(this.button_s10_Click);
			// 
			// button_s11
			// 
			this.button_s11.BackColor = System.Drawing.Color.Transparent;
			this.button_s11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s11.Location = new System.Drawing.Point(7, 413);
			this.button_s11.Name = "button_s11";
			this.button_s11.Size = new System.Drawing.Size(32, 32);
			this.button_s11.TabIndex = 10;
			this.button_s11.Click += new System.EventHandler(this.button_s11_Click);
			// 
			// button_s12
			// 
			this.button_s12.BackColor = System.Drawing.Color.Transparent;
			this.button_s12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_s12.Location = new System.Drawing.Point(7, 450);
			this.button_s12.Name = "button_s12";
			this.button_s12.Size = new System.Drawing.Size(32, 32);
			this.button_s12.TabIndex = 11;
			this.button_s12.Click += new System.EventHandler(this.button_s12_Click);
			// 
			// button_left
			// 
			this.button_left.BackColor = System.Drawing.Color.Transparent;
			this.button_left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_left.Location = new System.Drawing.Point(2, 14);
			this.button_left.Name = "button_left";
			this.button_left.Size = new System.Drawing.Size(12, 12);
			this.button_left.TabIndex = 12;
			this.button_left.Click += new System.EventHandler(this.button_left_Click);
			// 
			// button_right
			// 
			this.button_right.BackColor = System.Drawing.Color.Transparent;
			this.button_right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_right.Location = new System.Drawing.Point(32, 14);
			this.button_right.Name = "button_right";
			this.button_right.Size = new System.Drawing.Size(12, 12);
			this.button_right.TabIndex = 13;
			this.button_right.Click += new System.EventHandler(this.button_right_Click);
			// 
			// label_page
			// 
			this.label_page.BackColor = System.Drawing.Color.Transparent;
			this.label_page.ForeColor = System.Drawing.Color.White;
			this.label_page.Location = new System.Drawing.Point(12, 10);
			this.label_page.Name = "label_page";
			this.label_page.Size = new System.Drawing.Size(20, 16);
			this.label_page.TabIndex = 14;
			this.label_page.Text = "1";
			this.label_page.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ShortCutBar
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(46, 504);
			this.ControlBox = false;
			this.Controls.Add(this.label_page);
			this.Controls.Add(this.button_right);
			this.Controls.Add(this.button_left);
			this.Controls.Add(this.button_s12);
			this.Controls.Add(this.button_s11);
			this.Controls.Add(this.button_s10);
			this.Controls.Add(this.button_s9);
			this.Controls.Add(this.button_s8);
			this.Controls.Add(this.button_s7);
			this.Controls.Add(this.button_s6);
			this.Controls.Add(this.button_s5);
			this.Controls.Add(this.button_s4);
			this.Controls.Add(this.button_s3);
			this.Controls.Add(this.button_s2);
			this.Controls.Add(this.button_s1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortCutBar";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "   ~+~";
			this.ResumeLayout(false);

		}
		#endregion

		private void button_s1_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(0 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s2_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(1 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s3_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(2 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s4_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(3 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s5_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(4 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s6_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(5 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s7_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(6 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s8_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(7 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s9_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(8 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s10_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(9 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s11_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(10 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_s12_Click(object sender, System.EventArgs e)
		{
            ServerPackets.Use_ShortCut(11 + page * Globals.Skills_PerPage, Globals.gamedata.Control, Globals.gamedata.Shift);
		}

		private void button_left_Click(object sender, System.EventArgs e)
		{
			page--;
			if(page < 0)
			{
                page = Globals.Skills_Pages - 1;
			}

			Set_Label();
			Load_Images();
		}

		private void button_right_Click(object sender, System.EventArgs e)
		{
			page++;
            if (page >= Globals.Skills_Pages)
			{
				page = 0;
			}

			Set_Label();
			Load_Images();
		}

		public void Set_Label()
		{
			label_page.Text = (page+1).ToString();
		}

		public void Press_F(int f)
		{
			//make the bot "click" the correct button
			switch(f)
			{
				case 1:
					button_s1_Click(null,null);
					break;
				case 2:
					button_s2_Click(null,null);
					break;
				case 3:
					button_s3_Click(null,null);
					break;
				case 4:
					button_s4_Click(null,null);
					break;
				case 5:
					button_s5_Click(null,null);
					break;
				case 6:
					button_s6_Click(null,null);
					break;
				case 7:
					button_s7_Click(null,null);
					break;
				case 8:
					button_s8_Click(null,null);
					break;
				case 9:
					button_s9_Click(null,null);
					break;
				case 10:
					button_s10_Click(null,null);
					break;
				case 11:
					button_s11_Click(null,null);
					break;
				case 12:
					button_s12_Click(null,null);
					break;
			}
		}

		public void Load_Images()
		{
			string sk;
			ShortCut sc;

			//need to grab all the crap on this page and set the background pics
            for (int i = 0; i < Globals.Skills_PerPage; i++)
			{
                sc = ((ShortCut)Globals.gamedata.ShortCuts[i + page * Globals.Skills_PerPage]);

				try
				{
					switch( sc.Type)
					{
						case ShortCut_Types.ITEM:
							//need to go thru the inventory and find the item, then put its id or w/e
                            Set_Image(i, Util.GetItemImagePath(Util.GetInventoryItemID(sc.ID)));
							break;
						case ShortCut_Types.SKILL:
							sk = sc.ID.ToString();
							while(sk.Length < 4)
							{
								sk = "0" + sk;
							}

                            Set_Image(i, Globals.PATH + "\\Icons\\skill" + sk + "_0.BMP");
							break;
						case ShortCut_Types.ACTION:
                            Set_Image(i, Globals.PATH + "\\Icons\\" + ((Actions)Globals.actionlist[sc.ID]).Icon + "_0.BMP");
							break;
						case ShortCut_Types.MACRO:
                            Set_Image(i, Util.GetItemImagePath(sc.ID));
							break;
						case ShortCut_Types.RECIPE:
                            Set_Image(i, Util.GetItemImagePath(sc.ID));
							break;
						default:
							//lol?
							//either no button, or something fcked up
							Clear_Image(i);
							break;
					}//switch
				}
				catch
				{
					//something broke... meh
				}
			}
		}

		public void Clear_Image(int i)
		{
			try
			{
				switch(i)
				{
					case 0:
						if(button_s1.BackgroundImage != null)
						{
							button_s1.BackgroundImage.Dispose();
						}
						button_s1.BackgroundImage = null;
						break;
					case 1:
						if(button_s2.BackgroundImage != null)
						{
							button_s2.BackgroundImage.Dispose();
						}
						button_s2.BackgroundImage = null;
						break;
					case 2:
						if(button_s3.BackgroundImage != null)
						{
							button_s3.BackgroundImage.Dispose();
						}
						button_s3.BackgroundImage = null;
						break;
					case 3:
						if(button_s4.BackgroundImage != null)
						{
							button_s4.BackgroundImage.Dispose();
						}
						button_s4.BackgroundImage = null;
						break;
					case 4:
						if(button_s5.BackgroundImage != null)
						{
							button_s5.BackgroundImage.Dispose();
						}
						button_s5.BackgroundImage = null;
						break;
					case 5:
						if(button_s6.BackgroundImage != null)
						{
							button_s6.BackgroundImage.Dispose();
						}
						button_s6.BackgroundImage = null;
						break;
					case 6:
						if(button_s7.BackgroundImage != null)
						{
							button_s7.BackgroundImage.Dispose();
						}
						button_s7.BackgroundImage = null;
						break;
					case 7:
						if(button_s8.BackgroundImage != null)
						{
							button_s8.BackgroundImage.Dispose();
						}
						button_s8.BackgroundImage = null;
						break;
					case 8:
						if(button_s9.BackgroundImage != null)
						{
							button_s9.BackgroundImage.Dispose();
						}
						button_s9.BackgroundImage = null;
						break;
					case 9:
						if(button_s10.BackgroundImage != null)
						{
							button_s10.BackgroundImage.Dispose();
						}
						button_s10.BackgroundImage = null;
						break;
					case 10:
						if(button_s11.BackgroundImage != null)
						{
							button_s11.BackgroundImage.Dispose();
						}
						button_s11.BackgroundImage = null;
						break;
					case 11:
						if(button_s12.BackgroundImage != null)
						{
							button_s12.BackgroundImage.Dispose();
						}
						button_s12.BackgroundImage = null;
						break;
				}
			}
			catch
			{
				//bad image i guess, oh well
			}
		}

		public void Set_Image(int i, string file)
		{
			Clear_Image(i);

			try
			{
				switch(i)
				{
					case 0:
						button_s1.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 1:
						button_s2.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 2:
						button_s3.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 3:
						button_s4.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 4:
						button_s5.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 5:
						button_s6.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 6:
						button_s7.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 7:
						button_s8.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 8:
						button_s9.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 9:
						button_s10.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 10:
						button_s11.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
					case 11:
						button_s12.BackgroundImage = new System.Drawing.Bitmap(file);
						break;
				}
			}
			catch
			{
				//bad image i guess, oh well
			}
		}
	}//end of class
}
