namespace L2_login
{
    partial class PetWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PetWindow));
            this.button_pet_close = new System.Windows.Forms.Button();
            this.label_pet_Name = new System.Windows.Forms.Label();
            this.button_pet_Unsummon = new System.Windows.Forms.Button();
            this.label_pet_Level = new System.Windows.Forms.Label();
            this.label_pet_PAtk = new System.Windows.Forms.Label();
            this.label_pet_PDef = new System.Windows.Forms.Label();
            this.label_pet_MAtk = new System.Windows.Forms.Label();
            this.label_pet_MDef = new System.Windows.Forms.Label();
            this.label_pet_SP = new System.Windows.Forms.Label();
            this.label_info_atk_attrib_val_desc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label_pet_Accuracy = new System.Windows.Forms.Label();
            this.label_pet_CritRate = new System.Windows.Forms.Label();
            this.label_pet_AtkSpd = new System.Windows.Forms.Label();
            this.label_pet_Soulshot = new System.Windows.Forms.Label();
            this.label_pet_Evasion = new System.Windows.Forms.Label();
            this.label_pet_Speed = new System.Windows.Forms.Label();
            this.label_pet_Casting = new System.Windows.Forms.Label();
            this.label_pet_Spiritshot = new System.Windows.Forms.Label();
            this.listView_pet_PetInv = new System.Windows.Forms.ListView();
            this.columnHeader_Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Equipped = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ObjID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_pet_GiveItem = new System.Windows.Forms.Button();
            this.button_pet_TakeItem = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.button_pet_change_movement_mode = new System.Windows.Forms.Button();
            this.button_pet_attack = new System.Windows.Forms.Button();
            this.button_pet_stop = new System.Windows.Forms.Button();
            this.button_pet_pickup = new System.Windows.Forms.Button();
            this.button_pet_move = new System.Windows.Forms.Button();
            this.button_pet_mount = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar_pet_Load = new VistaStyleProgressBar.ProgressBar();
            this.progressBar_pet_XP = new VistaStyleProgressBar.ProgressBar();
            this.progressBar_pet_Food = new VistaStyleProgressBar.ProgressBar();
            this.progressBar_pet_MP = new VistaStyleProgressBar.ProgressBar();
            this.progressBar_pet_HP = new VistaStyleProgressBar.ProgressBar();
            this.SuspendLayout();
            // 
            // button_pet_close
            // 
            this.button_pet_close.Location = new System.Drawing.Point(262, 297);
            this.button_pet_close.Name = "button_pet_close";
            this.button_pet_close.Size = new System.Drawing.Size(68, 23);
            this.button_pet_close.TabIndex = 0;
            this.button_pet_close.Text = "&Close";
            this.button_pet_close.UseVisualStyleBackColor = true;
            this.button_pet_close.Click += new System.EventHandler(this.button_pet_close_Click);
            // 
            // label_pet_Name
            // 
            this.label_pet_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Name.Location = new System.Drawing.Point(46, 18);
            this.label_pet_Name.Name = "label_pet_Name";
            this.label_pet_Name.Size = new System.Drawing.Size(96, 13);
            this.label_pet_Name.TabIndex = 4;
            this.label_pet_Name.Text = "Name";
            this.label_pet_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_pet_Unsummon
            // 
            this.button_pet_Unsummon.Location = new System.Drawing.Point(160, 297);
            this.button_pet_Unsummon.Name = "button_pet_Unsummon";
            this.button_pet_Unsummon.Size = new System.Drawing.Size(68, 23);
            this.button_pet_Unsummon.TabIndex = 7;
            this.button_pet_Unsummon.Text = "&Unsummon";
            this.button_pet_Unsummon.UseVisualStyleBackColor = true;
            this.button_pet_Unsummon.Click += new System.EventHandler(this.button_pet_Unsummon_Click);
            // 
            // label_pet_Level
            // 
            this.label_pet_Level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Level.Location = new System.Drawing.Point(174, 18);
            this.label_pet_Level.Name = "label_pet_Level";
            this.label_pet_Level.Size = new System.Drawing.Size(140, 13);
            this.label_pet_Level.TabIndex = 9;
            this.label_pet_Level.Text = "Level";
            // 
            // label_pet_PAtk
            // 
            this.label_pet_PAtk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_PAtk.Location = new System.Drawing.Point(71, 106);
            this.label_pet_PAtk.Name = "label_pet_PAtk";
            this.label_pet_PAtk.Size = new System.Drawing.Size(71, 13);
            this.label_pet_PAtk.TabIndex = 10;
            this.label_pet_PAtk.Text = "0";
            // 
            // label_pet_PDef
            // 
            this.label_pet_PDef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_PDef.Location = new System.Drawing.Point(71, 122);
            this.label_pet_PDef.Name = "label_pet_PDef";
            this.label_pet_PDef.Size = new System.Drawing.Size(71, 13);
            this.label_pet_PDef.TabIndex = 11;
            this.label_pet_PDef.Text = "0";
            // 
            // label_pet_MAtk
            // 
            this.label_pet_MAtk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_MAtk.Location = new System.Drawing.Point(210, 105);
            this.label_pet_MAtk.Name = "label_pet_MAtk";
            this.label_pet_MAtk.Size = new System.Drawing.Size(39, 13);
            this.label_pet_MAtk.TabIndex = 12;
            this.label_pet_MAtk.Text = "0";
            // 
            // label_pet_MDef
            // 
            this.label_pet_MDef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_MDef.Location = new System.Drawing.Point(210, 122);
            this.label_pet_MDef.Name = "label_pet_MDef";
            this.label_pet_MDef.Size = new System.Drawing.Size(39, 13);
            this.label_pet_MDef.TabIndex = 13;
            this.label_pet_MDef.Text = "0";
            // 
            // label_pet_SP
            // 
            this.label_pet_SP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_SP.Location = new System.Drawing.Point(185, 52);
            this.label_pet_SP.Name = "label_pet_SP";
            this.label_pet_SP.Size = new System.Drawing.Size(87, 13);
            this.label_pet_SP.TabIndex = 15;
            this.label_pet_SP.Text = "SP";
            // 
            // label_info_atk_attrib_val_desc
            // 
            this.label_info_atk_attrib_val_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_atk_attrib_val_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_atk_attrib_val_desc.Location = new System.Drawing.Point(9, 103);
            this.label_info_atk_attrib_val_desc.Name = "label_info_atk_attrib_val_desc";
            this.label_info_atk_attrib_val_desc.Size = new System.Drawing.Size(56, 16);
            this.label_info_atk_attrib_val_desc.TabIndex = 36;
            this.label_info_atk_attrib_val_desc.Text = "P. Atk.";
            this.label_info_atk_attrib_val_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label1.Location = new System.Drawing.Point(9, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "P. Def.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label2.Location = new System.Drawing.Point(9, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Accuracy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label3.Location = new System.Drawing.Point(9, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 39;
            this.label3.Text = "Crit. Rate";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label4.Location = new System.Drawing.Point(9, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 40;
            this.label4.Text = "Atk. Spd.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label5.Location = new System.Drawing.Point(9, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 41;
            this.label5.Text = "Soulshot";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label6.Location = new System.Drawing.Point(148, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 16);
            this.label6.TabIndex = 42;
            this.label6.Text = "SP";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label7.Location = new System.Drawing.Point(9, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 16);
            this.label7.TabIndex = 43;
            this.label7.Text = "HP";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label8.Location = new System.Drawing.Point(9, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 16);
            this.label8.TabIndex = 44;
            this.label8.Text = "MP";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label9.Location = new System.Drawing.Point(9, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 16);
            this.label9.TabIndex = 45;
            this.label9.Text = "XP";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label10.Location = new System.Drawing.Point(9, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 16);
            this.label10.TabIndex = 46;
            this.label10.Text = "Food";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label11.Location = new System.Drawing.Point(148, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 16);
            this.label11.TabIndex = 47;
            this.label11.Text = "Load";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label12.Location = new System.Drawing.Point(148, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 48;
            this.label12.Text = "M. Atk.";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label13.Location = new System.Drawing.Point(148, 119);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 16);
            this.label13.TabIndex = 49;
            this.label13.Text = "M. Def.";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label14.Location = new System.Drawing.Point(148, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 16);
            this.label14.TabIndex = 50;
            this.label14.Text = "Evasion";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label15.Location = new System.Drawing.Point(148, 151);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 51;
            this.label15.Text = "Speed";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label16.Location = new System.Drawing.Point(148, 167);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 16);
            this.label16.TabIndex = 52;
            this.label16.Text = "Casting";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label17.Location = new System.Drawing.Point(148, 183);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 16);
            this.label17.TabIndex = 53;
            this.label17.Text = "Spiritshot";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label18.Location = new System.Drawing.Point(148, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(28, 16);
            this.label18.TabIndex = 55;
            this.label18.Text = "Lv.";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_pet_Accuracy
            // 
            this.label_pet_Accuracy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Accuracy.Location = new System.Drawing.Point(71, 138);
            this.label_pet_Accuracy.Name = "label_pet_Accuracy";
            this.label_pet_Accuracy.Size = new System.Drawing.Size(71, 13);
            this.label_pet_Accuracy.TabIndex = 56;
            this.label_pet_Accuracy.Text = "0";
            // 
            // label_pet_CritRate
            // 
            this.label_pet_CritRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_CritRate.Location = new System.Drawing.Point(71, 154);
            this.label_pet_CritRate.Name = "label_pet_CritRate";
            this.label_pet_CritRate.Size = new System.Drawing.Size(71, 13);
            this.label_pet_CritRate.TabIndex = 57;
            this.label_pet_CritRate.Text = "0";
            // 
            // label_pet_AtkSpd
            // 
            this.label_pet_AtkSpd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_AtkSpd.Location = new System.Drawing.Point(71, 170);
            this.label_pet_AtkSpd.Name = "label_pet_AtkSpd";
            this.label_pet_AtkSpd.Size = new System.Drawing.Size(71, 13);
            this.label_pet_AtkSpd.TabIndex = 58;
            this.label_pet_AtkSpd.Text = "0";
            // 
            // label_pet_Soulshot
            // 
            this.label_pet_Soulshot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Soulshot.Location = new System.Drawing.Point(71, 186);
            this.label_pet_Soulshot.Name = "label_pet_Soulshot";
            this.label_pet_Soulshot.Size = new System.Drawing.Size(71, 13);
            this.label_pet_Soulshot.TabIndex = 59;
            this.label_pet_Soulshot.Text = "0";
            // 
            // label_pet_Evasion
            // 
            this.label_pet_Evasion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Evasion.Location = new System.Drawing.Point(210, 138);
            this.label_pet_Evasion.Name = "label_pet_Evasion";
            this.label_pet_Evasion.Size = new System.Drawing.Size(39, 13);
            this.label_pet_Evasion.TabIndex = 60;
            this.label_pet_Evasion.Text = "0";
            // 
            // label_pet_Speed
            // 
            this.label_pet_Speed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Speed.Location = new System.Drawing.Point(210, 154);
            this.label_pet_Speed.Name = "label_pet_Speed";
            this.label_pet_Speed.Size = new System.Drawing.Size(39, 13);
            this.label_pet_Speed.TabIndex = 61;
            this.label_pet_Speed.Text = "0";
            // 
            // label_pet_Casting
            // 
            this.label_pet_Casting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Casting.Location = new System.Drawing.Point(210, 170);
            this.label_pet_Casting.Name = "label_pet_Casting";
            this.label_pet_Casting.Size = new System.Drawing.Size(39, 13);
            this.label_pet_Casting.TabIndex = 62;
            this.label_pet_Casting.Text = "0";
            // 
            // label_pet_Spiritshot
            // 
            this.label_pet_Spiritshot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_pet_Spiritshot.Location = new System.Drawing.Point(210, 185);
            this.label_pet_Spiritshot.Name = "label_pet_Spiritshot";
            this.label_pet_Spiritshot.Size = new System.Drawing.Size(39, 13);
            this.label_pet_Spiritshot.TabIndex = 63;
            this.label_pet_Spiritshot.Text = "0";
            // 
            // listView_pet_PetInv
            // 
            this.listView_pet_PetInv.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listView_pet_PetInv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.listView_pet_PetInv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_pet_PetInv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Item,
            this.columnHeader_Quantity,
            this.columnHeader_Equipped,
            this.columnHeader_ObjID});
            this.listView_pet_PetInv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.listView_pet_PetInv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_pet_PetInv.Location = new System.Drawing.Point(12, 216);
            this.listView_pet_PetInv.Name = "listView_pet_PetInv";
            this.listView_pet_PetInv.Size = new System.Drawing.Size(315, 75);
            this.listView_pet_PetInv.TabIndex = 64;
            this.listView_pet_PetInv.UseCompatibleStateImageBehavior = false;
            this.listView_pet_PetInv.View = System.Windows.Forms.View.Details;
            this.listView_pet_PetInv.DoubleClick += new System.EventHandler(this.listView_pet_PetInv_DoubleClick);
            // 
            // columnHeader_Item
            // 
            this.columnHeader_Item.Text = "Item";
            this.columnHeader_Item.Width = 228;
            // 
            // columnHeader_Quantity
            // 
            this.columnHeader_Quantity.Text = "Quantity";
            // 
            // columnHeader_Equipped
            // 
            this.columnHeader_Equipped.DisplayIndex = 3;
            this.columnHeader_Equipped.Text = "Equipped";
            this.columnHeader_Equipped.Width = 24;
            // 
            // columnHeader_ObjID
            // 
            this.columnHeader_ObjID.DisplayIndex = 2;
            this.columnHeader_ObjID.Text = "ObjID";
            this.columnHeader_ObjID.Width = 0;
            // 
            // button_pet_GiveItem
            // 
            this.button_pet_GiveItem.Location = new System.Drawing.Point(12, 297);
            this.button_pet_GiveItem.Name = "button_pet_GiveItem";
            this.button_pet_GiveItem.Size = new System.Drawing.Size(68, 23);
            this.button_pet_GiveItem.TabIndex = 65;
            this.button_pet_GiveItem.Text = "&Give Item";
            this.button_pet_GiveItem.UseVisualStyleBackColor = true;
            this.button_pet_GiveItem.Click += new System.EventHandler(this.button_pet_GiveItem_Click);
            // 
            // button_pet_TakeItem
            // 
            this.button_pet_TakeItem.Location = new System.Drawing.Point(86, 297);
            this.button_pet_TakeItem.Name = "button_pet_TakeItem";
            this.button_pet_TakeItem.Size = new System.Drawing.Size(68, 23);
            this.button_pet_TakeItem.TabIndex = 66;
            this.button_pet_TakeItem.Text = "&Take Item";
            this.button_pet_TakeItem.UseVisualStyleBackColor = true;
            this.button_pet_TakeItem.Click += new System.EventHandler(this.button_pet_TakeItem_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label19.Location = new System.Drawing.Point(9, 199);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 16);
            this.label19.TabIndex = 67;
            this.label19.Text = "Inventory:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label20.Location = new System.Drawing.Point(9, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 16);
            this.label20.TabIndex = 68;
            this.label20.Text = "Name";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_pet_change_movement_mode
            // 
            this.button_pet_change_movement_mode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pet_change_movement_mode.Image = ((System.Drawing.Image)(resources.GetObject("button_pet_change_movement_mode.Image")));
            this.button_pet_change_movement_mode.Location = new System.Drawing.Point(255, 103);
            this.button_pet_change_movement_mode.Name = "button_pet_change_movement_mode";
            this.button_pet_change_movement_mode.Size = new System.Drawing.Size(32, 32);
            this.button_pet_change_movement_mode.TabIndex = 69;
            this.toolTip1.SetToolTip(this.button_pet_change_movement_mode, "Change movement mode");
            this.button_pet_change_movement_mode.UseVisualStyleBackColor = false;
            this.button_pet_change_movement_mode.Click += new System.EventHandler(this.button_pet_change_movement_mode_Click);
            // 
            // button_pet_attack
            // 
            this.button_pet_attack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pet_attack.Image = ((System.Drawing.Image)(resources.GetObject("button_pet_attack.Image")));
            this.button_pet_attack.Location = new System.Drawing.Point(293, 103);
            this.button_pet_attack.Name = "button_pet_attack";
            this.button_pet_attack.Size = new System.Drawing.Size(32, 32);
            this.button_pet_attack.TabIndex = 70;
            this.toolTip2.SetToolTip(this.button_pet_attack, "Attack");
            this.button_pet_attack.UseVisualStyleBackColor = false;
            this.button_pet_attack.Click += new System.EventHandler(this.button_pet_attack_Click);
            // 
            // button_pet_stop
            // 
            this.button_pet_stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pet_stop.Image = ((System.Drawing.Image)(resources.GetObject("button_pet_stop.Image")));
            this.button_pet_stop.Location = new System.Drawing.Point(255, 135);
            this.button_pet_stop.Name = "button_pet_stop";
            this.button_pet_stop.Size = new System.Drawing.Size(32, 32);
            this.button_pet_stop.TabIndex = 71;
            this.toolTip3.SetToolTip(this.button_pet_stop, "Stop current action");
            this.button_pet_stop.UseVisualStyleBackColor = false;
            this.button_pet_stop.Click += new System.EventHandler(this.button_pet_stop_Click);
            // 
            // button_pet_pickup
            // 
            this.button_pet_pickup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pet_pickup.Image = ((System.Drawing.Image)(resources.GetObject("button_pet_pickup.Image")));
            this.button_pet_pickup.Location = new System.Drawing.Point(293, 135);
            this.button_pet_pickup.Name = "button_pet_pickup";
            this.button_pet_pickup.Size = new System.Drawing.Size(32, 32);
            this.button_pet_pickup.TabIndex = 72;
            this.toolTip4.SetToolTip(this.button_pet_pickup, "Pick up nearby items");
            this.button_pet_pickup.UseVisualStyleBackColor = false;
            this.button_pet_pickup.Click += new System.EventHandler(this.button_pet_pickup_Click);
            // 
            // button_pet_move
            // 
            this.button_pet_move.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pet_move.Image = ((System.Drawing.Image)(resources.GetObject("button_pet_move.Image")));
            this.button_pet_move.Location = new System.Drawing.Point(255, 170);
            this.button_pet_move.Name = "button_pet_move";
            this.button_pet_move.Size = new System.Drawing.Size(32, 32);
            this.button_pet_move.TabIndex = 73;
            this.toolTip5.SetToolTip(this.button_pet_move, "Move to the target");
            this.button_pet_move.UseVisualStyleBackColor = false;
            this.button_pet_move.Click += new System.EventHandler(this.button_pet_move_Click);
            // 
            // button_pet_mount
            // 
            this.button_pet_mount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pet_mount.Image = ((System.Drawing.Image)(resources.GetObject("button_pet_mount.Image")));
            this.button_pet_mount.Location = new System.Drawing.Point(293, 170);
            this.button_pet_mount.Name = "button_pet_mount";
            this.button_pet_mount.Size = new System.Drawing.Size(32, 32);
            this.button_pet_mount.TabIndex = 74;
            this.toolTip6.SetToolTip(this.button_pet_mount, "Mount/Dismount pet");
            this.button_pet_mount.UseVisualStyleBackColor = false;
            this.button_pet_mount.Click += new System.EventHandler(this.button_pet_mount_Click);
            // 
            // progressBar_pet_Load
            // 
            this.progressBar_pet_Load.Animate = false;
            this.progressBar_pet_Load.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_Load.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_Load.BarText = "Load";
            this.progressBar_pet_Load.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_pet_Load.EndColor = System.Drawing.Color.Orange;
            this.progressBar_pet_Load.Location = new System.Drawing.Point(188, 34);
            this.progressBar_pet_Load.Name = "progressBar_pet_Load";
            this.progressBar_pet_Load.Size = new System.Drawing.Size(84, 18);
            this.progressBar_pet_Load.StartColor = System.Drawing.Color.Yellow;
            this.progressBar_pet_Load.TabIndex = 14;
            this.progressBar_pet_Load.Value = 100;
            // 
            // progressBar_pet_XP
            // 
            this.progressBar_pet_XP.Animate = false;
            this.progressBar_pet_XP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_XP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_XP.BarText = "XP";
            this.progressBar_pet_XP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_pet_XP.EndColor = System.Drawing.Color.DarkGray;
            this.progressBar_pet_XP.Location = new System.Drawing.Point(46, 68);
            this.progressBar_pet_XP.Name = "progressBar_pet_XP";
            this.progressBar_pet_XP.Size = new System.Drawing.Size(96, 18);
            this.progressBar_pet_XP.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.progressBar_pet_XP.TabIndex = 8;
            this.progressBar_pet_XP.Value = 100;
            // 
            // progressBar_pet_Food
            // 
            this.progressBar_pet_Food.Animate = false;
            this.progressBar_pet_Food.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_Food.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_Food.BarText = "Food";
            this.progressBar_pet_Food.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_pet_Food.Location = new System.Drawing.Point(46, 82);
            this.progressBar_pet_Food.Name = "progressBar_pet_Food";
            this.progressBar_pet_Food.Size = new System.Drawing.Size(96, 18);
            this.progressBar_pet_Food.StartColor = System.Drawing.Color.Lime;
            this.progressBar_pet_Food.TabIndex = 3;
            this.progressBar_pet_Food.Value = 100;
            // 
            // progressBar_pet_MP
            // 
            this.progressBar_pet_MP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_MP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_MP.BarText = "MP";
            this.progressBar_pet_MP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_pet_MP.EndColor = System.Drawing.Color.Blue;
            this.progressBar_pet_MP.Location = new System.Drawing.Point(46, 50);
            this.progressBar_pet_MP.Name = "progressBar_pet_MP";
            this.progressBar_pet_MP.Size = new System.Drawing.Size(96, 18);
            this.progressBar_pet_MP.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.progressBar_pet_MP.TabIndex = 2;
            this.progressBar_pet_MP.Value = 100;
            // 
            // progressBar_pet_HP
            // 
            this.progressBar_pet_HP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_HP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_pet_HP.BarText = "HP";
            this.progressBar_pet_HP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_pet_HP.EndColor = System.Drawing.Color.Red;
            this.progressBar_pet_HP.Location = new System.Drawing.Point(46, 34);
            this.progressBar_pet_HP.Name = "progressBar_pet_HP";
            this.progressBar_pet_HP.Size = new System.Drawing.Size(96, 16);
            this.progressBar_pet_HP.TabIndex = 1;
            this.progressBar_pet_HP.Value = 100;
            // 
            // PetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(339, 324);
            this.Controls.Add(this.button_pet_mount);
            this.Controls.Add(this.button_pet_move);
            this.Controls.Add(this.button_pet_pickup);
            this.Controls.Add(this.button_pet_stop);
            this.Controls.Add(this.button_pet_attack);
            this.Controls.Add(this.button_pet_change_movement_mode);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.button_pet_TakeItem);
            this.Controls.Add(this.button_pet_GiveItem);
            this.Controls.Add(this.listView_pet_PetInv);
            this.Controls.Add(this.label_pet_Spiritshot);
            this.Controls.Add(this.label_pet_Casting);
            this.Controls.Add(this.label_pet_Speed);
            this.Controls.Add(this.label_pet_Evasion);
            this.Controls.Add(this.label_pet_Soulshot);
            this.Controls.Add(this.label_pet_AtkSpd);
            this.Controls.Add(this.label_pet_CritRate);
            this.Controls.Add(this.label_pet_Accuracy);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_info_atk_attrib_val_desc);
            this.Controls.Add(this.label_pet_SP);
            this.Controls.Add(this.progressBar_pet_Load);
            this.Controls.Add(this.label_pet_MDef);
            this.Controls.Add(this.label_pet_MAtk);
            this.Controls.Add(this.label_pet_PDef);
            this.Controls.Add(this.label_pet_PAtk);
            this.Controls.Add(this.label_pet_Level);
            this.Controls.Add(this.progressBar_pet_XP);
            this.Controls.Add(this.button_pet_Unsummon);
            this.Controls.Add(this.label_pet_Name);
            this.Controls.Add(this.progressBar_pet_Food);
            this.Controls.Add(this.progressBar_pet_MP);
            this.Controls.Add(this.progressBar_pet_HP);
            this.Controls.Add(this.button_pet_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PetWindow";
            this.Text = "Pet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_pet_close;
        private VistaStyleProgressBar.ProgressBar progressBar_pet_HP;
        private VistaStyleProgressBar.ProgressBar progressBar_pet_MP;
        private VistaStyleProgressBar.ProgressBar progressBar_pet_Food;
        private System.Windows.Forms.Label label_pet_Name;
        private System.Windows.Forms.Button button_pet_Unsummon;
        private VistaStyleProgressBar.ProgressBar progressBar_pet_XP;
        private System.Windows.Forms.Label label_pet_Level;
        private System.Windows.Forms.Label label_pet_PAtk;
        private System.Windows.Forms.Label label_pet_PDef;
        private System.Windows.Forms.Label label_pet_MAtk;
        private System.Windows.Forms.Label label_pet_MDef;
        private VistaStyleProgressBar.ProgressBar progressBar_pet_Load;
        private System.Windows.Forms.Label label_pet_SP;
        private System.Windows.Forms.Label label_info_atk_attrib_val_desc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label_pet_Accuracy;
        private System.Windows.Forms.Label label_pet_CritRate;
        private System.Windows.Forms.Label label_pet_AtkSpd;
        private System.Windows.Forms.Label label_pet_Soulshot;
        private System.Windows.Forms.Label label_pet_Evasion;
        private System.Windows.Forms.Label label_pet_Speed;
        private System.Windows.Forms.Label label_pet_Casting;
        private System.Windows.Forms.Label label_pet_Spiritshot;
        private System.Windows.Forms.ListView listView_pet_PetInv;
        private System.Windows.Forms.ColumnHeader columnHeader_Item;
        private System.Windows.Forms.ColumnHeader columnHeader_Quantity;
        private System.Windows.Forms.ColumnHeader columnHeader_ObjID;
        private System.Windows.Forms.Button button_pet_GiveItem;
        private System.Windows.Forms.Button button_pet_TakeItem;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ColumnHeader columnHeader_Equipped;
        private System.Windows.Forms.Button button_pet_change_movement_mode;
        private System.Windows.Forms.Button button_pet_attack;
        private System.Windows.Forms.Button button_pet_stop;
        private System.Windows.Forms.Button button_pet_pickup;
        private System.Windows.Forms.Button button_pet_move;
        private System.Windows.Forms.Button button_pet_mount;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
        private System.Windows.Forms.ToolTip toolTip6;
    }
}