using System.Collections.Generic;
using System.Windows.Forms;

namespace L2_login
{
    partial class packet_window
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
            this.button_start_rec = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_Load_list = new System.Windows.Forms.Button();
            this.button_Save_list = new System.Windows.Forms.Button();
            this.button_inject = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox_srv_cli = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.column_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_filter = new System.Windows.Forms.Button();
            this.checkBox_filter = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.error_label = new System.Windows.Forms.Label();
            this.parse_codebox = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start_rec
            // 
            this.button_start_rec.Location = new System.Drawing.Point(12, 17);
            this.button_start_rec.Name = "button_start_rec";
            this.button_start_rec.Size = new System.Drawing.Size(103, 25);
            this.button_start_rec.TabIndex = 0;
            this.button_start_rec.Text = "Start Recording";
            this.button_start_rec.UseVisualStyleBackColor = true;
            this.button_start_rec.Click += new System.EventHandler(this.button_start_rec_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(12, 46);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(103, 25);
            this.button_clear.TabIndex = 2;
            this.button_clear.Text = "Clear Window";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_Load_list
            // 
            this.button_Load_list.Location = new System.Drawing.Point(136, 17);
            this.button_Load_list.Name = "button_Load_list";
            this.button_Load_list.Size = new System.Drawing.Size(101, 25);
            this.button_Load_list.TabIndex = 3;
            this.button_Load_list.Text = "Load Packet Log";
            this.button_Load_list.UseVisualStyleBackColor = true;
            this.button_Load_list.Click += new System.EventHandler(this.button_Load_list_Click);
            // 
            // button_Save_list
            // 
            this.button_Save_list.Location = new System.Drawing.Point(136, 46);
            this.button_Save_list.Name = "button_Save_list";
            this.button_Save_list.Size = new System.Drawing.Size(101, 25);
            this.button_Save_list.TabIndex = 4;
            this.button_Save_list.Text = "Save Packet Log";
            this.button_Save_list.UseVisualStyleBackColor = true;
            this.button_Save_list.Click += new System.EventHandler(this.button_Save_list_Click);
            // 
            // button_inject
            // 
            this.button_inject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_inject.Location = new System.Drawing.Point(751, 536);
            this.button_inject.Name = "button_inject";
            this.button_inject.Size = new System.Drawing.Size(80, 25);
            this.button_inject.TabIndex = 5;
            this.button_inject.Text = "Inject";
            this.button_inject.UseVisualStyleBackColor = true;
            this.button_inject.Click += new System.EventHandler(this.button_inject_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(115, 536);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(630, 20);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(626, 248);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(205, 277);
            this.textBox2.TabIndex = 7;
            // 
            // comboBox_srv_cli
            // 
            this.comboBox_srv_cli.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_srv_cli.FormattingEnabled = true;
            this.comboBox_srv_cli.Items.AddRange(new object[] {
            "Send to Server",
            "Send to Client"});
            this.comboBox_srv_cli.Location = new System.Drawing.Point(12, 535);
            this.comboBox_srv_cli.Name = "comboBox_srv_cli";
            this.comboBox_srv_cli.Size = new System.Drawing.Size(97, 21);
            this.comboBox_srv_cli.TabIndex = 9;
            this.comboBox_srv_cli.SelectedIndexChanged += new System.EventHandler(this.comboBox_srv_cli_SelectedIndexChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_index,
            this.column_type,
            this.column_time,
            this.column_size,
            this.column_data,
            this.column_id});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 107);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(608, 418);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // column_index
            // 
            this.column_index.Text = "No";
            this.column_index.Width = 40;
            // 
            // column_type
            // 
            this.column_type.Text = "Type";
            this.column_type.Width = 40;
            // 
            // column_time
            // 
            this.column_time.Text = "Time";
            this.column_time.Width = 80;
            // 
            // column_size
            // 
            this.column_size.Text = "Size";
            this.column_size.Width = 40;
            // 
            // column_data
            // 
            this.column_data.Text = "Packet name";
            this.column_data.Width = 320;
            // 
            // column_id
            // 
            this.column_id.Text = "ID";
            this.column_id.Width = 70;
            // 
            // button_filter
            // 
            this.button_filter.Location = new System.Drawing.Point(6, 13);
            this.button_filter.Name = "button_filter";
            this.button_filter.Size = new System.Drawing.Size(104, 25);
            this.button_filter.TabIndex = 11;
            this.button_filter.Text = "Packet Filters";
            this.button_filter.UseVisualStyleBackColor = true;
            this.button_filter.Click += new System.EventHandler(this.button_filter_Click);
            // 
            // checkBox_filter
            // 
            this.checkBox_filter.AutoSize = true;
            this.checkBox_filter.Location = new System.Drawing.Point(6, 38);
            this.checkBox_filter.Name = "checkBox_filter";
            this.checkBox_filter.Size = new System.Drawing.Size(80, 17);
            this.checkBox_filter.TabIndex = 12;
            this.checkBox_filter.Text = "Filter on/off";
            this.checkBox_filter.UseVisualStyleBackColor = true;
            this.checkBox_filter.CheckedChanged += new System.EventHandler(this.checkBox_filter_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 13;
            this.button1.Text = "Converter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(62, 28);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(79, 20);
            this.textBox3.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(146, 13);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 19);
            this.button2.TabIndex = 15;
            this.button2.Text = "UP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(146, 47);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 19);
            this.button3.TabIndex = 16;
            this.button3.Text = "DOWN";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Location = new System.Drawing.Point(146, 28);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 21);
            this.button4.TabIndex = 17;
            this.button4.Text = "Search";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 14);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 17);
            this.radioButton1.TabIndex = 18;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Client";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 36);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(55, 17);
            this.radioButton2.TabIndex = 19;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Server";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 17);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "Follow new packets";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(470, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 85);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 59);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(126, 17);
            this.checkBox4.TabIndex = 20;
            this.checkBox4.Text = "Search in pck names";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(16, 55);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(118, 17);
            this.checkBox2.TabIndex = 22;
            this.checkBox2.Text = "Hide Client packets";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.button_filter);
            this.groupBox2.Controls.Add(this.checkBox_filter);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Location = new System.Drawing.Point(685, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 97);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filters";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(16, 74);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(124, 17);
            this.checkBox3.TabIndex = 24;
            this.checkBox3.Text = "Hide Server Packets";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(136, 80);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(146, 17);
            this.checkBox5.TabIndex = 24;
            this.checkBox5.Text = "Save only visible packets";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.parse_codebox);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Location = new System.Drawing.Point(627, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 135);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Packet Parser";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(7, 73);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(73, 17);
            this.checkBox6.TabIndex = 4;
            this.checkBox6.Text = "Parse edit";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.error_label);
            this.groupBox4.Location = new System.Drawing.Point(6, 94);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(185, 35);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status:";
            // 
            // error_label
            // 
            this.error_label.AutoSize = true;
            this.error_label.Location = new System.Drawing.Point(10, 16);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(16, 13);
            this.error_label.TabIndex = 0;
            this.error_label.Text = "...";
            // 
            // parse_codebox
            // 
            this.parse_codebox.Location = new System.Drawing.Point(7, 20);
            this.parse_codebox.Name = "parse_codebox";
            this.parse_codebox.Size = new System.Drawing.Size(185, 20);
            this.parse_codebox.TabIndex = 2;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(113, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(81, 21);
            this.button6.TabIndex = 1;
            this.button6.Text = "Help";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 46);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(77, 21);
            this.button5.TabIndex = 0;
            this.button5.Text = "Process";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // packet_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 568);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.comboBox_srv_cli);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_inject);
            this.Controls.Add(this.button_Save_list);
            this.Controls.Add(this.button_Load_list);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_start_rec);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(851, 596);
            this.Name = "packet_window";
            this.ShowIcon = false;
            this.Text = "Packet Window";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start_rec;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_Load_list;
        private System.Windows.Forms.Button button_Save_list;
        private System.Windows.Forms.Button button_inject;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox_srv_cli;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader column_index;
        private System.Windows.Forms.ColumnHeader column_type;
        private System.Windows.Forms.ColumnHeader column_time;
        private System.Windows.Forms.ColumnHeader column_data;
        private System.Windows.Forms.ColumnHeader column_id;
        public void add_to_list(pck_window_dat dat) // add thing to list
        {
           
            try
            {
                ListViewItem temp_item = new ListViewItem();
                int temp_count = listView1.Items.Count;
                //listView1.Items.Add(temp_count.ToString());
                temp_item.Text = temp_count.ToString();
                if(dat.type == 1) // client
                {
                    //listView1.Items[temp_count].SubItems.Add("Cli");
                     temp_item.SubItems.Add("Cli");
                }
                else // server
                {
                    temp_item.SubItems.Add("Srv");
                    //listView1.Items[temp_count].SubItems.Add("Srv");
                }
                temp_item.SubItems.Add(dat.time);
                //listView1.Items[temp_count].SubItems.Add(dat.time);
                temp_item.SubItems.Add(dat.bytebuffer.Length.ToString());
                //listView1.Items[temp_count].SubItems.Add(dat.bytebuffer.Length.ToString());
                if (dat.type == 1)//cli
                {
                    int temp_id = 0;
                    if (dat.bytebuffer.Length > 3)
                    {
                        if (dat.bytebuffer[0] < 0xd0)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                            //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                           // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                        }
                    }
                    else
                    {
                        if (dat.bytebuffer[0] < 0xd0)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                           // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < dat.bytebuffer.Length; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                              temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                           // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                        }
                    }
                    //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer));
                    string pck_id;
                    if (dat.bytebuffer[0] == 0xd0)
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                        pck_id += ":";
                        pck_id += dat.bytebuffer[1].ToString("X2");
                        pck_id += dat.bytebuffer[2].ToString("X2");
                    }
                    else
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                    }
                    temp_item.SubItems.Add(pck_id);
                    //listView1.Items[temp_count].SubItems.Add(pck_id);
                    temp_item.BackColor = System.Drawing.Color.LightSeaGreen;
                    //listView1.Items[temp_count].BackColor = System.Drawing.Color.LightSeaGreen;
                    //
                }
                else//srv
                {
                    int temp_id = 0;
                    if (dat.bytebuffer.Length > 3)
                    {
                        if (dat.bytebuffer[0] < 0xfe)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                           // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                           // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                        }
                    }
                    else
                    {
                        if (dat.bytebuffer[0] < 0xfe)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                           // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < dat.bytebuffer.Length; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                            //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                        }
                    }
                    //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.get_srv_pck_name(dat.bytebuffer));
                    string pck_id;
                    if (dat.bytebuffer[0] == 0xfe)
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                        pck_id += ":";
                        pck_id += dat.bytebuffer[1].ToString("X2");
                        pck_id += dat.bytebuffer[2].ToString("X2");
                    }
                    else
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                    }
                    temp_item.SubItems.Add(pck_id);
                    //listView1.Items[temp_count].SubItems.Add(pck_id);
                    temp_item.BackColor = System.Drawing.Color.Yellow;
                   //listView1.Items[temp_count].BackColor = System.Drawing.Color.Yellow;
                }
                listView1.Items.Add(temp_item);
                if (Globals.pck_thread.folow_new_pck)//folow pcjk
                {
                    listView1.Items[temp_count].EnsureVisible();
                }
                 //listView1.Items[temp_count].SubItems.Add(dat.bytebuffer[0].ToString("X2"));
            }
                //11:16 < Infant> srv rgb: 224 ; 245 ; 192
                //11:16 < Infant> client rgb: 255 ; 250 ; 168

                //
            // to do
            catch
            {

                // error ...ListViewNF
            }
        }
        public void add_range_from_cache()
        {
            
            cache_lictview_items = new System.Windows.Forms.ListViewItem[Globals.pck_thread.temp_cache_for_pck_window.Count];
           Globals.pck_thread.temp_cache_for_pck_window.CopyTo(cache_lictview_items);
       
            listView1.BeginUpdate();
            listView1.Items.AddRange(cache_lictview_items);
            listView1.EndUpdate();
           Globals.pck_thread.temp_cache_for_pck_window.Clear();
            if (Globals.pck_thread.folow_new_pck)//folow pcjk
            {
                listView1.Items[listView1.Items.Count-1].EnsureVisible();
            }

            //cache_lictview_items
           // listView1.Items.AddRange(Globals.cache_for_pck_window);
           // ListView.ListViewItemCollection test = new ListView.ListViewItemCollection(listView1);
          // ListViewItem temp = new ListViewItem();;
            //listView1.Items.AddRange(
           // test.AddRange(;
           
        }
        public void add_one_to_temp_cache(pck_window_dat dat)
        {

            try
            {
                ListViewItem temp_item = new ListViewItem();
                int temp_count = listView1.Items.Count;
                temp_count +=Globals.pck_thread.temp_cache_for_pck_window.Count;
                //listView1.Items.Add(temp_count.ToString());
                temp_item.Text = temp_count.ToString();
                if (dat.type == 1) // client
                {
                    //listView1.Items[temp_count].SubItems.Add("Cli");
                    temp_item.SubItems.Add("Cli");
                }
                else // server
                {
                    temp_item.SubItems.Add("Srv");
                    //listView1.Items[temp_count].SubItems.Add("Srv");
                }
                temp_item.SubItems.Add(dat.time);
                //listView1.Items[temp_count].SubItems.Add(dat.time);
                temp_item.SubItems.Add(dat.bytebuffer.Length.ToString());
                //listView1.Items[temp_count].SubItems.Add(dat.bytebuffer.Length.ToString());
                if (dat.type == 1)//cli
                {
                    int temp_id = 0;
                    if (dat.bytebuffer.Length > 3)
                    {
                        if (dat.bytebuffer[0] < 0xd0)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                            //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                            // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                        }
                    }
                    else
                    {
                        if (dat.bytebuffer[0] < 0xd0)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                            // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < dat.bytebuffer.Length; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                            // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                        }
                    }
                    //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer));
                    string pck_id;
                    if (dat.bytebuffer[0] == 0xd0)
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                        pck_id += ":";
                        pck_id += dat.bytebuffer[1].ToString("X2");
                        pck_id += dat.bytebuffer[2].ToString("X2");
                    }
                    else
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                    }
                    temp_item.SubItems.Add(pck_id);
                    //listView1.Items[temp_count].SubItems.Add(pck_id);
                    temp_item.BackColor = System.Drawing.Color.LightSeaGreen;
                    //listView1.Items[temp_count].BackColor = System.Drawing.Color.LightSeaGreen;
                    //
                }
                else//srv
                {
                    int temp_id = 0;
                    if (dat.bytebuffer.Length > 3)
                    {
                        if (dat.bytebuffer[0] < 0xfe)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                            // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                            // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                        }
                    }
                    else
                    {
                        if (dat.bytebuffer[0] < 0xfe)
                        {
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                            // listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < dat.bytebuffer.Length; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            temp_item.SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                            //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                        }
                    }
                    //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.get_srv_pck_name(dat.bytebuffer));
                    string pck_id;
                    if (dat.bytebuffer[0] == 0xfe)
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                        pck_id += ":";
                        pck_id += dat.bytebuffer[1].ToString("X2");
                        pck_id += dat.bytebuffer[2].ToString("X2");
                    }
                    else
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                    }
                    temp_item.SubItems.Add(pck_id);
                    //listView1.Items[temp_count].SubItems.Add(pck_id);
                    temp_item.BackColor = System.Drawing.Color.Yellow;
                    //listView1.Items[temp_count].BackColor = System.Drawing.Color.Yellow;
                }

               Globals.pck_thread.temp_cache_for_pck_window.Add(temp_item);
            }

            catch
            {

                // error ...ListViewNF
            }
        }
        private System.Windows.Forms.ColumnHeader column_size;
        private System.Windows.Forms.Button button_filter;
        private System.Windows.Forms.CheckBox checkBox_filter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private CheckBox checkBox2;
        private GroupBox groupBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label error_label;
        private TextBox parse_codebox;
        private Button button6;
        private Button button5;
        private ErrorProvider errorProvider1;
        private CheckBox checkBox6;

    }
}
public static class ControlExtensions
{
    public static void DoubleBuffer(this Control control)
    {
        // http://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form/77233#77233
        // Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx

        if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;
        System.Reflection.PropertyInfo dbProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        dbProp.SetValue(control, true, null);
    }
}
/*
        public void add_to_list(pck_window_dat dat) // add thing to list
        {
           
            try
            {
                int temp_count = listView1.Items.Count;
                listView1.Items.Add(temp_count.ToString());
                if(dat.type == 1) // client
                {
                    listView1.Items[temp_count].SubItems.Add("Cli");
                }
                else // server
                {
                    listView1.Items[temp_count].SubItems.Add("Srv");
                }
                listView1.Items[temp_count].SubItems.Add(dat.time);
                listView1.Items[temp_count].SubItems.Add(dat.bytebuffer.Length.ToString());
                if (dat.type == 1)//cli
                {
                    int temp_id = 0;
                    if (dat.bytebuffer.Length > 3)
                    {
                        if (dat.bytebuffer[0] < 0xd0)
                        {
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                        }
                    }
                    else
                    {
                        if (dat.bytebuffer[0] < 0xd0)
                        {
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < dat.bytebuffer.Length; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                        }
                    }
                    //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(dat.bytebuffer));
                    string pck_id;
                    if (dat.bytebuffer[0] == 0xd0)
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                        pck_id += ":";
                        pck_id += dat.bytebuffer[1].ToString("X2");
                        pck_id += dat.bytebuffer[2].ToString("X2");
                    }
                    else
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                    }
                    listView1.Items[temp_count].SubItems.Add(pck_id);
                    listView1.Items[temp_count].BackColor = System.Drawing.Color.LightSeaGreen;
                    //
                }
                else//srv
                {
                    int temp_id = 0;
                    if (dat.bytebuffer.Length > 3)
                    {
                        if (dat.bytebuffer[0] < 0xfe)
                        {
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                        }
                    }
                    else
                    {
                        if (dat.bytebuffer[0] < 0xfe)
                        {
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(dat.bytebuffer[0]));
                        }
                        else
                        {
                            for (int i = 0; i < dat.bytebuffer.Length; i++)
                            {
                                temp_id += dat.bytebuffer[i];
                            }
                            listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(temp_id));
                        }
                    }
                    //listView1.Items[temp_count].SubItems.Add(Globals.pck_thread.get_srv_pck_name(dat.bytebuffer));
                    string pck_id;
                    if (dat.bytebuffer[0] == 0xfe)
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                        pck_id += ":";
                        pck_id += dat.bytebuffer[1].ToString("X2");
                        pck_id += dat.bytebuffer[2].ToString("X2");
                    }
                    else
                    {
                        pck_id = dat.bytebuffer[0].ToString("X2");
                    }
                    listView1.Items[temp_count].SubItems.Add(pck_id);
                    listView1.Items[temp_count].BackColor = System.Drawing.Color.Yellow;
                }
                if (Globals.pck_thread.folow_new_pck)//folow pcjk
                {
                    listView1.Items[temp_count].EnsureVisible();
                }
                 //listView1.Items[temp_count].SubItems.Add(dat.bytebuffer[0].ToString("X2"));
            }
                //11:16 < Infant> srv rgb: 224 ; 245 ; 192
                //11:16 < Infant> client rgb: 255 ; 250 ; 168

                //
            // to do
            catch
            {

                // error ...ListViewNF
            }
        }



*/