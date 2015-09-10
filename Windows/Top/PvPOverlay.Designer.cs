namespace L2_login.Windows.Top
{
    partial class PvPList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PvPList));
            this.pvp_listview = new System.Windows.Forms.ListView();
            this.plist_clan_tag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plist_class = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plist_dist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plist_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plist_clan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pvp_filter_enemy = new System.Windows.Forms.CheckBox();
            this.pvp_filter_friend = new System.Windows.Forms.CheckBox();
            this.pvp_filter_neutral = new System.Windows.Forms.CheckBox();
            this.pvp_filter_dead = new System.Windows.Forms.CheckBox();
            this.pvp_filter_petrify = new System.Windows.Forms.CheckBox();
            this.pvp_filter_invincible = new System.Windows.Forms.CheckBox();
            this.pvp_player_count = new System.Windows.Forms.ListView();
            this.pvp_list_enemy_count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pvp_list_neutral_count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pvp_list_friend_count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pvp_listview
            // 
            this.pvp_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.plist_clan_tag,
            this.plist_class,
            this.plist_dist,
            this.plist_name,
            this.plist_clan});
            this.pvp_listview.Location = new System.Drawing.Point(9, 32);
            this.pvp_listview.Name = "pvp_listview";
            this.pvp_listview.Size = new System.Drawing.Size(456, 165);
            this.pvp_listview.TabIndex = 1;
            this.pvp_listview.UseCompatibleStateImageBehavior = false;
            this.pvp_listview.View = System.Windows.Forms.View.Details;
            // 
            // plist_clan_tag
            // 
            this.plist_clan_tag.Text = "Tag";
            // 
            // plist_class
            // 
            this.plist_class.Text = "Class";
            this.plist_class.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.plist_class.Width = 53;
            // 
            // plist_dist
            // 
            this.plist_dist.Text = "Range";
            this.plist_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.plist_dist.Width = 48;
            // 
            // plist_name
            // 
            this.plist_name.Text = "Name";
            this.plist_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.plist_name.Width = 147;
            // 
            // plist_clan
            // 
            this.plist_clan.Text = "Clan";
            this.plist_clan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.plist_clan.Width = 144;
            // 
            // pvp_filter_enemy
            // 
            this.pvp_filter_enemy.AutoSize = true;
            this.pvp_filter_enemy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_filter_enemy.Location = new System.Drawing.Point(32, 489);
            this.pvp_filter_enemy.Name = "pvp_filter_enemy";
            this.pvp_filter_enemy.Padding = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.pvp_filter_enemy.Size = new System.Drawing.Size(68, 20);
            this.pvp_filter_enemy.TabIndex = 2;
            this.pvp_filter_enemy.Text = "Enemy";
            this.pvp_filter_enemy.UseVisualStyleBackColor = true;
            // 
            // pvp_filter_friend
            // 
            this.pvp_filter_friend.AutoSize = true;
            this.pvp_filter_friend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_filter_friend.Location = new System.Drawing.Point(162, 489);
            this.pvp_filter_friend.Name = "pvp_filter_friend";
            this.pvp_filter_friend.Padding = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.pvp_filter_friend.Size = new System.Drawing.Size(65, 20);
            this.pvp_filter_friend.TabIndex = 3;
            this.pvp_filter_friend.Text = "Friend";
            this.pvp_filter_friend.UseVisualStyleBackColor = true;
            // 
            // pvp_filter_neutral
            // 
            this.pvp_filter_neutral.AutoSize = true;
            this.pvp_filter_neutral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_filter_neutral.Location = new System.Drawing.Point(96, 489);
            this.pvp_filter_neutral.Name = "pvp_filter_neutral";
            this.pvp_filter_neutral.Padding = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.pvp_filter_neutral.Size = new System.Drawing.Size(70, 20);
            this.pvp_filter_neutral.TabIndex = 4;
            this.pvp_filter_neutral.Text = "Neutral";
            this.pvp_filter_neutral.UseVisualStyleBackColor = true;
            // 
            // pvp_filter_dead
            // 
            this.pvp_filter_dead.AutoSize = true;
            this.pvp_filter_dead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_filter_dead.Location = new System.Drawing.Point(223, 489);
            this.pvp_filter_dead.Name = "pvp_filter_dead";
            this.pvp_filter_dead.Padding = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.pvp_filter_dead.Size = new System.Drawing.Size(62, 20);
            this.pvp_filter_dead.TabIndex = 5;
            this.pvp_filter_dead.Text = "Dead";
            this.pvp_filter_dead.UseVisualStyleBackColor = true;
            // 
            // pvp_filter_petrify
            // 
            this.pvp_filter_petrify.AutoSize = true;
            this.pvp_filter_petrify.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_filter_petrify.Location = new System.Drawing.Point(282, 489);
            this.pvp_filter_petrify.Name = "pvp_filter_petrify";
            this.pvp_filter_petrify.Padding = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.pvp_filter_petrify.Size = new System.Drawing.Size(65, 20);
            this.pvp_filter_petrify.TabIndex = 6;
            this.pvp_filter_petrify.Text = "Petrify";
            this.pvp_filter_petrify.UseVisualStyleBackColor = true;
            // 
            // pvp_filter_invincible
            // 
            this.pvp_filter_invincible.AutoSize = true;
            this.pvp_filter_invincible.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_filter_invincible.Location = new System.Drawing.Point(344, 489);
            this.pvp_filter_invincible.Name = "pvp_filter_invincible";
            this.pvp_filter_invincible.Padding = new System.Windows.Forms.Padding(5, 2, 5, 1);
            this.pvp_filter_invincible.Size = new System.Drawing.Size(81, 20);
            this.pvp_filter_invincible.TabIndex = 7;
            this.pvp_filter_invincible.Text = "Invincible";
            this.pvp_filter_invincible.UseVisualStyleBackColor = true;
            // 
            // pvp_player_count
            // 
            this.pvp_player_count.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pvp_list_enemy_count,
            this.pvp_list_neutral_count,
            this.pvp_list_friend_count});
            this.pvp_player_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_player_count.Location = new System.Drawing.Point(9, 433);
            this.pvp_player_count.Margin = new System.Windows.Forms.Padding(0);
            this.pvp_player_count.Name = "pvp_player_count";
            this.pvp_player_count.Size = new System.Drawing.Size(185, 53);
            this.pvp_player_count.TabIndex = 8;
            this.pvp_player_count.UseCompatibleStateImageBehavior = false;
            this.pvp_player_count.View = System.Windows.Forms.View.Details;
            // 
            // pvp_list_enemy_count
            // 
            this.pvp_list_enemy_count.Text = "Enemies";
            this.pvp_list_enemy_count.Width = 58;
            // 
            // pvp_list_neutral_count
            // 
            this.pvp_list_neutral_count.Text = "Neutrals";
            this.pvp_list_neutral_count.Width = 62;
            // 
            // pvp_list_friend_count
            // 
            this.pvp_list_friend_count.Text = "Friends";
            this.pvp_list_friend_count.Width = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(83, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Target List";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 200);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(105, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Targeting You";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.Location = new System.Drawing.Point(12, 234);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(456, 165);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Class";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 53;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Range";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 48;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 147;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Clan";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 144;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 402);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(99, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Player Count";
            // 
            // PvPList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 515);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pvp_player_count);
            this.Controls.Add(this.pvp_filter_invincible);
            this.Controls.Add(this.pvp_filter_petrify);
            this.Controls.Add(this.pvp_filter_dead);
            this.Controls.Add(this.pvp_filter_neutral);
            this.Controls.Add(this.pvp_filter_friend);
            this.Controls.Add(this.pvp_filter_enemy);
            this.Controls.Add(this.pvp_listview);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PvPList";
            this.Text = "PvP Overlay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView pvp_listview;
        private System.Windows.Forms.ColumnHeader plist_clan_tag;
        private System.Windows.Forms.ColumnHeader plist_class;
        private System.Windows.Forms.ColumnHeader plist_dist;
        private System.Windows.Forms.ColumnHeader plist_name;
        private System.Windows.Forms.ColumnHeader plist_clan;
        private System.Windows.Forms.CheckBox pvp_filter_enemy;
        private System.Windows.Forms.CheckBox pvp_filter_friend;
        private System.Windows.Forms.CheckBox pvp_filter_neutral;
        private System.Windows.Forms.CheckBox pvp_filter_dead;
        private System.Windows.Forms.CheckBox pvp_filter_petrify;
        private System.Windows.Forms.CheckBox pvp_filter_invincible;
        private System.Windows.Forms.ListView pvp_player_count;
        private System.Windows.Forms.ColumnHeader pvp_list_enemy_count;
        private System.Windows.Forms.ColumnHeader pvp_list_neutral_count;
        private System.Windows.Forms.ColumnHeader pvp_list_friend_count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label3;
    }
}