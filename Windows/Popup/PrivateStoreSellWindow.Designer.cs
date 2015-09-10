namespace L2_login
{
    partial class PrivateStoreSellWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivateStoreSellWindow));
            this.listView_privatestoresell_inv = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.listView_privatestoresell_ItemsOnSale = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.label_privatestoresell_quantity = new System.Windows.Forms.Label();
            this.textBox_privatestoresell_quantity = new System.Windows.Forms.TextBox();
            this.label_privatestoresell_price = new System.Windows.Forms.Label();
            this.textBox_privatestoresell_price = new System.Windows.Forms.TextBox();
            this.label_privatestoresell_inventory = new System.Windows.Forms.Label();
            this.label_privatestoresell_itemsonsale = new System.Windows.Forms.Label();
            this.label_privatestoresell_message = new System.Windows.Forms.Label();
            this.textBox_privatestoresell_message = new System.Windows.Forms.TextBox();
            this.button__privatestoresell_start = new System.Windows.Forms.Button();
            this.button_privatestoresell_abort = new System.Windows.Forms.Button();
            this.button__privatestoresell_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_privatestoresell_inv
            // 
            this.listView_privatestoresell_inv.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView_privatestoresell_inv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.listView_privatestoresell_inv.Location = new System.Drawing.Point(12, 25);
            this.listView_privatestoresell_inv.Name = "listView_privatestoresell_inv";
            this.listView_privatestoresell_inv.Size = new System.Drawing.Size(360, 131);
            this.listView_privatestoresell_inv.TabIndex = 1;
            this.listView_privatestoresell_inv.UseCompatibleStateImageBehavior = false;
            this.listView_privatestoresell_inv.View = System.Windows.Forms.View.Details;
            this.listView_privatestoresell_inv.DoubleClick += new System.EventHandler(this.listView_privatestoresell_inv_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 296;
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
            // listView_privatestoresell_ItemsOnSale
            // 
            this.listView_privatestoresell_ItemsOnSale.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView_privatestoresell_ItemsOnSale.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader6});
            this.listView_privatestoresell_ItemsOnSale.Location = new System.Drawing.Point(12, 201);
            this.listView_privatestoresell_ItemsOnSale.Name = "listView_privatestoresell_ItemsOnSale";
            this.listView_privatestoresell_ItemsOnSale.Size = new System.Drawing.Size(360, 131);
            this.listView_privatestoresell_ItemsOnSale.TabIndex = 2;
            this.listView_privatestoresell_ItemsOnSale.UseCompatibleStateImageBehavior = false;
            this.listView_privatestoresell_ItemsOnSale.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item";
            this.columnHeader3.Width = 235;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            // 
            // columnHeader7
            // 
            this.columnHeader7.DisplayIndex = 3;
            this.columnHeader7.Text = "Price";
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 2;
            this.columnHeader6.Text = "ObjID";
            this.columnHeader6.Width = 0;
            // 
            // label_privatestoresell_quantity
            // 
            this.label_privatestoresell_quantity.AutoSize = true;
            this.label_privatestoresell_quantity.Location = new System.Drawing.Point(9, 165);
            this.label_privatestoresell_quantity.Name = "label_privatestoresell_quantity";
            this.label_privatestoresell_quantity.Size = new System.Drawing.Size(46, 13);
            this.label_privatestoresell_quantity.TabIndex = 3;
            this.label_privatestoresell_quantity.Text = "Quantity";
            // 
            // textBox_privatestoresell_quantity
            // 
            this.textBox_privatestoresell_quantity.Location = new System.Drawing.Point(61, 162);
            this.textBox_privatestoresell_quantity.Name = "textBox_privatestoresell_quantity";
            this.textBox_privatestoresell_quantity.Size = new System.Drawing.Size(100, 20);
            this.textBox_privatestoresell_quantity.TabIndex = 4;
            this.textBox_privatestoresell_quantity.Text = "1";
            this.textBox_privatestoresell_quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_privatestoresell_quantity_KeyPress);
            // 
            // label_privatestoresell_price
            // 
            this.label_privatestoresell_price.AutoSize = true;
            this.label_privatestoresell_price.Location = new System.Drawing.Point(235, 165);
            this.label_privatestoresell_price.Name = "label_privatestoresell_price";
            this.label_privatestoresell_price.Size = new System.Drawing.Size(31, 13);
            this.label_privatestoresell_price.TabIndex = 5;
            this.label_privatestoresell_price.Text = "Price";
            // 
            // textBox_privatestoresell_price
            // 
            this.textBox_privatestoresell_price.Location = new System.Drawing.Point(272, 162);
            this.textBox_privatestoresell_price.Name = "textBox_privatestoresell_price";
            this.textBox_privatestoresell_price.Size = new System.Drawing.Size(100, 20);
            this.textBox_privatestoresell_price.TabIndex = 6;
            this.textBox_privatestoresell_price.Text = "1000";
            this.textBox_privatestoresell_price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_privatestoresell_price_KeyPress);
            // 
            // label_privatestoresell_inventory
            // 
            this.label_privatestoresell_inventory.AutoSize = true;
            this.label_privatestoresell_inventory.Location = new System.Drawing.Point(12, 9);
            this.label_privatestoresell_inventory.Name = "label_privatestoresell_inventory";
            this.label_privatestoresell_inventory.Size = new System.Drawing.Size(57, 13);
            this.label_privatestoresell_inventory.TabIndex = 7;
            this.label_privatestoresell_inventory.Text = "Equipment";
            // 
            // label_privatestoresell_itemsonsale
            // 
            this.label_privatestoresell_itemsonsale.AutoSize = true;
            this.label_privatestoresell_itemsonsale.Location = new System.Drawing.Point(9, 185);
            this.label_privatestoresell_itemsonsale.Name = "label_privatestoresell_itemsonsale";
            this.label_privatestoresell_itemsonsale.Size = new System.Drawing.Size(71, 13);
            this.label_privatestoresell_itemsonsale.TabIndex = 8;
            this.label_privatestoresell_itemsonsale.Text = "Items on Sale";
            // 
            // label_privatestoresell_message
            // 
            this.label_privatestoresell_message.AutoSize = true;
            this.label_privatestoresell_message.Location = new System.Drawing.Point(12, 335);
            this.label_privatestoresell_message.Name = "label_privatestoresell_message";
            this.label_privatestoresell_message.Size = new System.Drawing.Size(50, 13);
            this.label_privatestoresell_message.TabIndex = 9;
            this.label_privatestoresell_message.Text = "Message";
            // 
            // textBox_privatestoresell_message
            // 
            this.textBox_privatestoresell_message.Location = new System.Drawing.Point(68, 335);
            this.textBox_privatestoresell_message.Name = "textBox_privatestoresell_message";
            this.textBox_privatestoresell_message.Size = new System.Drawing.Size(304, 20);
            this.textBox_privatestoresell_message.TabIndex = 10;
            // 
            // button__privatestoresell_start
            // 
            this.button__privatestoresell_start.Location = new System.Drawing.Point(297, 363);
            this.button__privatestoresell_start.Name = "button__privatestoresell_start";
            this.button__privatestoresell_start.Size = new System.Drawing.Size(75, 23);
            this.button__privatestoresell_start.TabIndex = 11;
            this.button__privatestoresell_start.Text = "Start";
            this.button__privatestoresell_start.UseVisualStyleBackColor = true;
            this.button__privatestoresell_start.Click += new System.EventHandler(this.button__privatestoresell_start_Click);
            // 
            // button_privatestoresell_abort
            // 
            this.button_privatestoresell_abort.Location = new System.Drawing.Point(216, 363);
            this.button_privatestoresell_abort.Name = "button_privatestoresell_abort";
            this.button_privatestoresell_abort.Size = new System.Drawing.Size(75, 23);
            this.button_privatestoresell_abort.TabIndex = 12;
            this.button_privatestoresell_abort.Text = "Abort";
            this.button_privatestoresell_abort.UseVisualStyleBackColor = true;
            this.button_privatestoresell_abort.Click += new System.EventHandler(this.button_privatestoresell_abort_Click);
            // 
            // button__privatestoresell_close
            // 
            this.button__privatestoresell_close.Location = new System.Drawing.Point(15, 363);
            this.button__privatestoresell_close.Name = "button__privatestoresell_close";
            this.button__privatestoresell_close.Size = new System.Drawing.Size(75, 23);
            this.button__privatestoresell_close.TabIndex = 13;
            this.button__privatestoresell_close.Text = "Close";
            this.button__privatestoresell_close.UseVisualStyleBackColor = true;
            this.button__privatestoresell_close.Click += new System.EventHandler(this.button__privatestoresell_close_Click);
            // 
            // PrivateStoreSellWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 398);
            this.Controls.Add(this.button__privatestoresell_close);
            this.Controls.Add(this.button_privatestoresell_abort);
            this.Controls.Add(this.button__privatestoresell_start);
            this.Controls.Add(this.textBox_privatestoresell_message);
            this.Controls.Add(this.label_privatestoresell_message);
            this.Controls.Add(this.label_privatestoresell_itemsonsale);
            this.Controls.Add(this.label_privatestoresell_inventory);
            this.Controls.Add(this.textBox_privatestoresell_price);
            this.Controls.Add(this.label_privatestoresell_price);
            this.Controls.Add(this.textBox_privatestoresell_quantity);
            this.Controls.Add(this.label_privatestoresell_quantity);
            this.Controls.Add(this.listView_privatestoresell_ItemsOnSale);
            this.Controls.Add(this.listView_privatestoresell_inv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrivateStoreSellWindow";
            this.Text = "Private Store (Sell)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_privatestoresell_inv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView listView_privatestoresell_ItemsOnSale;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label_privatestoresell_quantity;
        private System.Windows.Forms.TextBox textBox_privatestoresell_quantity;
        private System.Windows.Forms.Label label_privatestoresell_price;
        private System.Windows.Forms.TextBox textBox_privatestoresell_price;
        private System.Windows.Forms.Label label_privatestoresell_inventory;
        private System.Windows.Forms.Label label_privatestoresell_itemsonsale;
        private System.Windows.Forms.Label label_privatestoresell_message;
        private System.Windows.Forms.TextBox textBox_privatestoresell_message;
        private System.Windows.Forms.Button button__privatestoresell_start;
        private System.Windows.Forms.Button button_privatestoresell_abort;
        private System.Windows.Forms.Button button__privatestoresell_close;

    }
}