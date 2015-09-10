namespace L2_login
{
    partial class PetWindowGive
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
            this.listView_petGive_MyInv = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox_petGive_Quantity = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.button_petGive_Close = new System.Windows.Forms.Button();
            this.toolTip_petgive_GiveItem = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // listView_petGive_MyInv
            // 
            this.listView_petGive_MyInv.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView_petGive_MyInv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.listView_petGive_MyInv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
            this.listView_petGive_MyInv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.listView_petGive_MyInv.Location = new System.Drawing.Point(12, 12);
            this.listView_petGive_MyInv.Name = "listView_petGive_MyInv";
            this.listView_petGive_MyInv.Size = new System.Drawing.Size(260, 108);
            this.listView_petGive_MyInv.TabIndex = 66;
            this.toolTip_petgive_GiveItem.SetToolTip(this.listView_petGive_MyInv, "Double Click to Transfer Item");
            this.listView_petGive_MyInv.UseCompatibleStateImageBehavior = false;
            this.listView_petGive_MyInv.View = System.Windows.Forms.View.Details;
            this.listView_petGive_MyInv.DoubleClick += new System.EventHandler(this.listView_petGive_MyInv_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item";
            this.columnHeader3.Width = 190;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            this.columnHeader4.Width = 54;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ObjID";
            this.columnHeader6.Width = 0;
            // 
            // textBox_petGive_Quantity
            // 
            this.textBox_petGive_Quantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.textBox_petGive_Quantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_petGive_Quantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.textBox_petGive_Quantity.Location = new System.Drawing.Point(85, 129);
            this.textBox_petGive_Quantity.Name = "textBox_petGive_Quantity";
            this.textBox_petGive_Quantity.Size = new System.Drawing.Size(106, 13);
            this.textBox_petGive_Quantity.TabIndex = 68;
            this.textBox_petGive_Quantity.Text = "100";
            this.textBox_petGive_Quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_petGive_Quantity_KeyPress);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label19.Location = new System.Drawing.Point(12, 127);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 16);
            this.label19.TabIndex = 71;
            this.label19.Text = "Quantity:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_petGive_Close
            // 
            this.button_petGive_Close.Location = new System.Drawing.Point(197, 124);
            this.button_petGive_Close.Name = "button_petGive_Close";
            this.button_petGive_Close.Size = new System.Drawing.Size(75, 23);
            this.button_petGive_Close.TabIndex = 72;
            this.button_petGive_Close.Text = "&Close";
            this.button_petGive_Close.UseVisualStyleBackColor = true;
            this.button_petGive_Close.Click += new System.EventHandler(this.button_petGive_Close_Click);
            // 
            // PetWindowGive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(284, 152);
            this.Controls.Add(this.button_petGive_Close);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textBox_petGive_Quantity);
            this.Controls.Add(this.listView_petGive_MyInv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PetWindowGive";
            this.Text = "Give Item to Pet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_petGive_MyInv;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox textBox_petGive_Quantity;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button_petGive_Close;
        private System.Windows.Forms.ToolTip toolTip_petgive_GiveItem;
    }
}