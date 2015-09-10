namespace L2_login
{
    partial class CreateChar
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
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_create = new System.Windows.Forms.Button();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.comboBox_race = new System.Windows.Forms.ComboBox();
            this.comboBox_sex = new System.Windows.Forms.ComboBox();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.comboBox_hairstyle = new System.Windows.Forms.ComboBox();
            this.comboBox_haircolor = new System.Windows.Forms.ComboBox();
            this.comboBox_face = new System.Windows.Forms.ComboBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_race = new System.Windows.Forms.Label();
            this.label_sex = new System.Windows.Forms.Label();
            this.label_type = new System.Windows.Forms.Label();
            this.label_hairstyle = new System.Windows.Forms.Label();
            this.label_haircolor = new System.Windows.Forms.Label();
            this.label_face = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(146, 199);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 0;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(12, 199);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(75, 23);
            this.button_create.TabIndex = 1;
            this.button_create.Text = "Create";
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(72, 6);
            this.textBox_name.MaxLength = 16;
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(149, 20);
            this.textBox_name.TabIndex = 2;
            // 
            // comboBox_race
            // 
            this.comboBox_race.FormattingEnabled = true;
            this.comboBox_race.Items.AddRange(new object[] {
            "Human",
            "Elf",
            "Dark Elf",
            "Orc",
            "Dwarf",
            "Kamael"});
            this.comboBox_race.Location = new System.Drawing.Point(72, 33);
            this.comboBox_race.Name = "comboBox_race";
            this.comboBox_race.Size = new System.Drawing.Size(121, 21);
            this.comboBox_race.TabIndex = 3;
            // 
            // comboBox_sex
            // 
            this.comboBox_sex.FormattingEnabled = true;
            this.comboBox_sex.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.comboBox_sex.Location = new System.Drawing.Point(72, 60);
            this.comboBox_sex.Name = "comboBox_sex";
            this.comboBox_sex.Size = new System.Drawing.Size(121, 21);
            this.comboBox_sex.TabIndex = 4;
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "Fighter",
            "Mage"});
            this.comboBox_type.Location = new System.Drawing.Point(72, 88);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(121, 21);
            this.comboBox_type.TabIndex = 5;
            // 
            // comboBox_hairstyle
            // 
            this.comboBox_hairstyle.FormattingEnabled = true;
            this.comboBox_hairstyle.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F (Female Only)",
            "G (Female Only)"});
            this.comboBox_hairstyle.Location = new System.Drawing.Point(72, 116);
            this.comboBox_hairstyle.Name = "comboBox_hairstyle";
            this.comboBox_hairstyle.Size = new System.Drawing.Size(121, 21);
            this.comboBox_hairstyle.TabIndex = 6;
            // 
            // comboBox_haircolor
            // 
            this.comboBox_haircolor.FormattingEnabled = true;
            this.comboBox_haircolor.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E (Kamael Only)"});
            this.comboBox_haircolor.Location = new System.Drawing.Point(72, 144);
            this.comboBox_haircolor.Name = "comboBox_haircolor";
            this.comboBox_haircolor.Size = new System.Drawing.Size(121, 21);
            this.comboBox_haircolor.TabIndex = 7;
            // 
            // comboBox_face
            // 
            this.comboBox_face.FormattingEnabled = true;
            this.comboBox_face.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.comboBox_face.Location = new System.Drawing.Point(72, 172);
            this.comboBox_face.Name = "comboBox_face";
            this.comboBox_face.Size = new System.Drawing.Size(121, 21);
            this.comboBox_face.TabIndex = 8;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(12, 9);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(35, 13);
            this.label_name.TabIndex = 9;
            this.label_name.Text = "Name";
            // 
            // label_race
            // 
            this.label_race.AutoSize = true;
            this.label_race.Location = new System.Drawing.Point(12, 36);
            this.label_race.Name = "label_race";
            this.label_race.Size = new System.Drawing.Size(33, 13);
            this.label_race.TabIndex = 10;
            this.label_race.Text = "Race";
            // 
            // label_sex
            // 
            this.label_sex.AutoSize = true;
            this.label_sex.Location = new System.Drawing.Point(12, 63);
            this.label_sex.Name = "label_sex";
            this.label_sex.Size = new System.Drawing.Size(25, 13);
            this.label_sex.TabIndex = 11;
            this.label_sex.Text = "Sex";
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(12, 91);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(31, 13);
            this.label_type.TabIndex = 12;
            this.label_type.Text = "Type";
            // 
            // label_hairstyle
            // 
            this.label_hairstyle.AutoSize = true;
            this.label_hairstyle.Location = new System.Drawing.Point(12, 119);
            this.label_hairstyle.Name = "label_hairstyle";
            this.label_hairstyle.Size = new System.Drawing.Size(52, 13);
            this.label_hairstyle.TabIndex = 13;
            this.label_hairstyle.Text = "Hair Style";
            // 
            // label_haircolor
            // 
            this.label_haircolor.AutoSize = true;
            this.label_haircolor.Location = new System.Drawing.Point(12, 147);
            this.label_haircolor.Name = "label_haircolor";
            this.label_haircolor.Size = new System.Drawing.Size(53, 13);
            this.label_haircolor.TabIndex = 14;
            this.label_haircolor.Text = "Hair Color";
            // 
            // label_face
            // 
            this.label_face.AutoSize = true;
            this.label_face.Location = new System.Drawing.Point(12, 175);
            this.label_face.Name = "label_face";
            this.label_face.Size = new System.Drawing.Size(31, 13);
            this.label_face.TabIndex = 15;
            this.label_face.Text = "Face";
            // 
            // CreateChar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(230, 231);
            this.ControlBox = false;
            this.Controls.Add(this.label_face);
            this.Controls.Add(this.label_haircolor);
            this.Controls.Add(this.label_hairstyle);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.label_sex);
            this.Controls.Add(this.label_race);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.comboBox_face);
            this.Controls.Add(this.comboBox_haircolor);
            this.Controls.Add(this.comboBox_hairstyle);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.comboBox_sex);
            this.Controls.Add(this.comboBox_race);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.button_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateChar";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Character";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.ComboBox comboBox_race;
        private System.Windows.Forms.ComboBox comboBox_sex;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.ComboBox comboBox_hairstyle;
        private System.Windows.Forms.ComboBox comboBox_haircolor;
        private System.Windows.Forms.ComboBox comboBox_face;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_race;
        private System.Windows.Forms.Label label_sex;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label label_hairstyle;
        private System.Windows.Forms.Label label_haircolor;
        private System.Windows.Forms.Label label_face;
    }
}