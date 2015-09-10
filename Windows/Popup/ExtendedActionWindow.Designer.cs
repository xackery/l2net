namespace L2_login
{
  partial class ExtendedActionWindow
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendedActionWindow));
      this.button_mailbox = new System.Windows.Forms.Button();
      this.toolTip_mailbox = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // button_mailbox
      // 
      this.button_mailbox.BackColor = System.Drawing.Color.Transparent;
      this.button_mailbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button_mailbox.Location = new System.Drawing.Point(12, 62);
      this.button_mailbox.Name = "button_mailbox";
      this.button_mailbox.Size = new System.Drawing.Size(32, 32);
      this.button_mailbox.TabIndex = 1;
      this.toolTip_mailbox.SetToolTip(this.button_mailbox, "Mailbox");
      this.button_mailbox.UseVisualStyleBackColor = false;
      this.button_mailbox.Click += new System.EventHandler(this.button_mailbox_Click);
      // 
      // ExtendedActionWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(238, 396);
      this.Controls.Add(this.button_mailbox);
      this.Name = "ExtendedActionWindow";
      this.Text = "ExtendedActionWindow";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button_mailbox;
    private System.Windows.Forms.ToolTip toolTip_mailbox;

  }
}