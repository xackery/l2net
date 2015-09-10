#if DEBUG
//    #define SILENT
#endif

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace L2_login
{
	/// <summary>
	/// Summary description for Splash.
	/// </summary>
	public class SplashScreen : System.Windows.Forms.Form
    {
		public SplashScreen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

#if SILENT
#else
            this.ClientSize = this.BackgroundImage.Size;

            this.TransparencyKey = ((Bitmap)this.BackgroundImage).GetPixel(1, 1);
#endif
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.SuspendLayout();
            // 
            // SplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(520, 238);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.ResumeLayout(false);

		}
		#endregion
	}//end of class
}
