namespace L2_login
{
    partial class BuyWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuyWindow));
            this.listView_buylist = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.button_trade_confirm = new System.Windows.Forms.Button();
            this.button_trade_cancel = new System.Windows.Forms.Button();
            this.listView_buylist_selected_items = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.textBox_buywindow_quantity = new System.Windows.Forms.TextBox();
            this.lbl_trade_quantity = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView_buylist
            // 
            this.listView_buylist.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView_buylist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader7});
            this.listView_buylist.Location = new System.Drawing.Point(6, 12);
            this.listView_buylist.Name = "listView_buylist";
            this.listView_buylist.Size = new System.Drawing.Size(234, 128);
            this.listView_buylist.TabIndex = 0;
            this.listView_buylist.UseCompatibleStateImageBehavior = false;
            this.listView_buylist.View = System.Windows.Forms.View.Details;
            this.listView_buylist.DoubleClick += new System.EventHandler(this.listView_buylist_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Price";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ItemID";
            this.columnHeader5.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "BuyListID";
            this.columnHeader7.Width = 0;
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
            this.button_trade_cancel.Location = new System.Drawing.Point(12, 294);
            this.button_trade_cancel.Name = "button_trade_cancel";
            this.button_trade_cancel.Size = new System.Drawing.Size(82, 24);
            this.button_trade_cancel.TabIndex = 3;
            this.button_trade_cancel.Text = "Cancel";
            this.button_trade_cancel.UseVisualStyleBackColor = true;
            this.button_trade_cancel.Click += new System.EventHandler(this.button_trade_cancel_Click);
            // 
            // listView_buylist_selected_items
            // 
            this.listView_buylist_selected_items.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader8});
            this.listView_buylist_selected_items.Location = new System.Drawing.Point(6, 177);
            this.listView_buylist_selected_items.Name = "listView_buylist_selected_items";
            this.listView_buylist_selected_items.Size = new System.Drawing.Size(234, 111);
            this.listView_buylist_selected_items.TabIndex = 5;
            this.listView_buylist_selected_items.UseCompatibleStateImageBehavior = false;
            this.listView_buylist_selected_items.View = System.Windows.Forms.View.Details;
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
            // columnHeader6
            // 
            this.columnHeader6.Text = "ItemID";
            this.columnHeader6.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "BuyListID";
            this.columnHeader8.Width = 0;
            // 
            // textBox_buywindow_quantity
            // 
            this.textBox_buywindow_quantity.Location = new System.Drawing.Point(113, 151);
            this.textBox_buywindow_quantity.MaxLength = 100;
            this.textBox_buywindow_quantity.Name = "textBox_buywindow_quantity";
            this.textBox_buywindow_quantity.Size = new System.Drawing.Size(63, 20);
            this.textBox_buywindow_quantity.TabIndex = 7;
            this.textBox_buywindow_quantity.Text = "1";
            this.textBox_buywindow_quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_buywindow_quantity.WordWrap = false;
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
            // BuyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(247, 323);
            this.Controls.Add(this.lbl_trade_quantity);
            this.Controls.Add(this.textBox_buywindow_quantity);
            this.Controls.Add(this.listView_buylist_selected_items);
            this.Controls.Add(this.button_trade_cancel);
            this.Controls.Add(this.button_trade_confirm);
            this.Controls.Add(this.listView_buylist);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BuyWindow";
            this.Text = "Buy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_buylist;
        private System.Windows.Forms.Button button_trade_confirm;
        private System.Windows.Forms.Button button_trade_cancel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listView_buylist_selected_items;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox textBox_buywindow_quantity;
        private System.Windows.Forms.Label lbl_trade_quantity;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}