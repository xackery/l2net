namespace L2_login
{
    partial class ManualGameKey
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
            this.textBox_StaticKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_NewKey = new System.Windows.Forms.TextBox();
            this.button_GameKey_Set = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_StaticKey
            // 
            this.textBox_StaticKey.Location = new System.Drawing.Point(73, 38);
            this.textBox_StaticKey.Name = "textBox_StaticKey";
            this.textBox_StaticKey.Size = new System.Drawing.Size(213, 20);
            this.textBox_StaticKey.TabIndex = 0;
            this.textBox_StaticKey.Text = "C8 27 93 01 A1 6C 31 97";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key 8-15";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Key 0-7";
            // 
            // textBox_NewKey
            // 
            this.textBox_NewKey.Location = new System.Drawing.Point(73, 12);
            this.textBox_NewKey.Name = "textBox_NewKey";
            this.textBox_NewKey.Size = new System.Drawing.Size(213, 20);
            this.textBox_NewKey.TabIndex = 2;
            // 
            // button_GameKey_Set
            // 
            this.button_GameKey_Set.Location = new System.Drawing.Point(12, 64);
            this.button_GameKey_Set.Name = "button_GameKey_Set";
            this.button_GameKey_Set.Size = new System.Drawing.Size(75, 23);
            this.button_GameKey_Set.TabIndex = 16;
            this.button_GameKey_Set.Text = "Set";
            this.button_GameKey_Set.UseVisualStyleBackColor = true;
            this.button_GameKey_Set.Click += new System.EventHandler(this.button_GameKey_Set_Click);
            // 
            // ManualGameKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 94);
            this.Controls.Add(this.button_GameKey_Set);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_NewKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_StaticKey);
            this.Name = "ManualGameKey";
            this.Text = "Gamekey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_StaticKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_NewKey;
        private System.Windows.Forms.Button button_GameKey_Set;
    }
}