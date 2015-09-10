using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace L2_login
{
	/// <summary>
	/// Summary description for Base.
	/// </summary>
	public class Base : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected System.ComponentModel.Container components = null;

		public Base()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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

		protected void MdiParent_Resize(object sender, EventArgs e)
		{
			this.Top = 0;
			this.Left = 0;
            if (this.MdiParent.Width > 500 && this.MdiParent.Height > 300)
            {
                this.ClientSize = new System.Drawing.Size(this.MdiParent.Size.Width - 403, this.MdiParent.Size.Height - 229);
            }

			//panel_game.Width = this.Width;
			//panel_game.Height = this.Height;
            this.Refresh();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Base
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Base";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Base";

		}
		#endregion
	}
}
