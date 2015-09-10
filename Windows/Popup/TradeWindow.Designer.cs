namespace L2_login
{
    partial class TradeWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeWindow));
            this.listView_trade_mychar = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.button_trade_confirm = new System.Windows.Forms.Button();
            this.button_trade_cancel = new System.Windows.Forms.Button();
            this.button_trade_close = new System.Windows.Forms.Button();
            this.listView_trade_send = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.textBox_trade_quantity = new System.Windows.Forms.TextBox();
            this.lbl_trade_quantity = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView_trade_mychar
            // 
            this.listView_trade_mychar.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView_trade_mychar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.listView_trade_mychar.Location = new System.Drawing.Point(6, 12);
            this.listView_trade_mychar.Name = "listView_trade_mychar";
            this.listView_trade_mychar.Size = new System.Drawing.Size(234, 128);
            this.listView_trade_mychar.TabIndex = 0;
            this.listView_trade_mychar.UseCompatibleStateImageBehavior = false;
            this.listView_trade_mychar.View = System.Windows.Forms.View.Details;
            this.listView_trade_mychar.DoubleClick += new System.EventHandler(this.listView_trade_mychar_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Quantity";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ObjID";
            this.columnHeader5.Width = 0;
            // 
            // button_trade_confirm
            // 
            this.button_trade_confirm.Location = new System.Drawing.Point(158, 294);
            this.button_trade_confirm.Name = "button_trade_confirm";
            this.button_trade_confirm.Size = new System.Drawing.Size(82, 24);
            this.button_trade_confirm.TabIndex = 2;
            this.button_trade_confirm.Text = "Confirm";
            this.button_trade_confirm.UseVisualStyleBackColor = true;
            this.button_trade_confirm.Click += new System.EventHandler(this.button_trade_confirm_Click);
            // 
            // button_trade_cancel
            // 
            this.button_trade_cancel.Location = new System.Drawing.Point(70, 294);
            this.button_trade_cancel.Name = "button_trade_cancel";
            this.button_trade_cancel.Size = new System.Drawing.Size(82, 24);
            this.button_trade_cancel.TabIndex = 3;
            this.button_trade_cancel.Text = "Cancel";
            this.button_trade_cancel.UseVisualStyleBackColor = true;
            this.button_trade_cancel.Click += new System.EventHandler(this.button_trade_cancel_Click);
            // 
            // button_trade_close
            // 
            this.button_trade_close.Location = new System.Drawing.Point(6, 294);
            this.button_trade_close.Name = "button_trade_close";
            this.button_trade_close.Size = new System.Drawing.Size(54, 24);
            this.button_trade_close.TabIndex = 4;
            this.button_trade_close.Text = "Close";
            this.button_trade_close.UseVisualStyleBackColor = true;
            this.button_trade_close.Click += new System.EventHandler(this.button_trade_close_Click);
            // 
            // listView_trade_send
            // 
            this.listView_trade_send.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listView_trade_send.Location = new System.Drawing.Point(6, 177);
            this.listView_trade_send.Name = "listView_trade_send";
            this.listView_trade_send.Size = new System.Drawing.Size(234, 111);
            this.listView_trade_send.TabIndex = 5;
            this.listView_trade_send.UseCompatibleStateImageBehavior = false;
            this.listView_trade_send.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item";
            this.columnHeader3.Width = 170;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            // 
            // textBox_trade_quantity
            // 
            this.textBox_trade_quantity.Location = new System.Drawing.Point(113, 151);
            this.textBox_trade_quantity.MaxLength = 100;
            this.textBox_trade_quantity.Name = "textBox_trade_quantity";
            this.textBox_trade_quantity.Size = new System.Drawing.Size(63, 20);
            this.textBox_trade_quantity.TabIndex = 7;
            this.textBox_trade_quantity.Text = "1";
            this.textBox_trade_quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_trade_quantity.WordWrap = false;
            this.textBox_trade_quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_trade_quantity_KeyPress);
            // 
            // lbl_trade_quantity
            // 
            this.lbl_trade_quantity.AutoSize = true;
            this.lbl_trade_quantity.Location = new System.Drawing.Point(61, 154);
            this.lbl_trade_quantity.Name = "lbl_trade_quantity";
            this.lbl_trade_quantity.Size = new System.Drawing.Size(46, 13);
            this.lbl_trade_quantity.TabIndex = 8;
            this.lbl_trade_quantity.Text = "Quantity";
            // 
            // TradeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 323);
            this.Controls.Add(this.lbl_trade_quantity);
            this.Controls.Add(this.textBox_trade_quantity);
            this.Controls.Add(this.listView_trade_send);
            this.Controls.Add(this.button_trade_close);
            this.Controls.Add(this.button_trade_cancel);
            this.Controls.Add(this.button_trade_confirm);
            this.Controls.Add(this.listView_trade_mychar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradeWindow";
            this.Text = "Trade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_trade_mychar;
        private System.Windows.Forms.Button button_trade_confirm;
        private System.Windows.Forms.Button button_trade_cancel;
        private System.Windows.Forms.Button button_trade_close;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listView_trade_send;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox textBox_trade_quantity;
        private System.Windows.Forms.Label lbl_trade_quantity;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}