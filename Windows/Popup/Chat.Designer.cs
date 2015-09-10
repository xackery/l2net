namespace L2_login
{
    partial class Chat
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
            this.panel_chat = new System.Windows.Forms.Panel();
            this.comboBox_msg_type = new System.Windows.Forms.ComboBox();
            this.button_sendtext = new System.Windows.Forms.Button();
            this.textBox_say = new System.Windows.Forms.TextBox();
            this.tabControl_ChatSelect = new System.Windows.Forms.TabControl();
            this.tab_all = new System.Windows.Forms.TabPage();
            this.colorListBox_all = new L2_login.ColorListBox();
            this.tab_system = new System.Windows.Forms.TabPage();
            this.colorListBox_system = new L2_login.ColorListBox();
            this.tab_bot = new System.Windows.Forms.TabPage();
            this.colorListBox_bot = new L2_login.ColorListBox();
            this.tab_local = new System.Windows.Forms.TabPage();
            this.colorListBox_local = new L2_login.ColorListBox();
            this.tab_trade = new System.Windows.Forms.TabPage();
            this.colorListBox_trade = new L2_login.ColorListBox();
            this.tab_party = new System.Windows.Forms.TabPage();
            this.colorListBox_party = new L2_login.ColorListBox();
            this.tab_clan = new System.Windows.Forms.TabPage();
            this.colorListBox_clan = new L2_login.ColorListBox();
            this.tab_alliance = new System.Windows.Forms.TabPage();
            this.colorListBox_ally = new L2_login.ColorListBox();
            this.tab_hero = new System.Windows.Forms.TabPage();
            this.colorListBox_hero = new L2_login.ColorListBox();
            this.panel_chat.SuspendLayout();
            this.tabControl_ChatSelect.SuspendLayout();
            this.tab_all.SuspendLayout();
            this.tab_system.SuspendLayout();
            this.tab_bot.SuspendLayout();
            this.tab_local.SuspendLayout();
            this.tab_trade.SuspendLayout();
            this.tab_party.SuspendLayout();
            this.tab_clan.SuspendLayout();
            this.tab_alliance.SuspendLayout();
            this.tab_hero.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_chat
            // 
            this.panel_chat.Controls.Add(this.comboBox_msg_type);
            this.panel_chat.Controls.Add(this.button_sendtext);
            this.panel_chat.Controls.Add(this.textBox_say);
            this.panel_chat.Controls.Add(this.tabControl_ChatSelect);
            this.panel_chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_chat.Location = new System.Drawing.Point(0, 0);
            this.panel_chat.Name = "panel_chat";
            this.panel_chat.Size = new System.Drawing.Size(635, 182);
            this.panel_chat.TabIndex = 23;
            // 
            // comboBox_msg_type
            // 
            this.comboBox_msg_type.DropDownWidth = 150;
            this.comboBox_msg_type.Items.AddRange(new object[] {
            "Local",
            "Shout",
            "Tell",
            "Party",
            "Clan",
            "GM",
            "Petition Reply to GM",
            "Petition From GM",
            "Trade",
            "Alliance",
            "Announcement",
            "Boat Message",
            "Fake1",
            "Fake2",
            "Fake3",
            "PartyRoomAll",
            "PartyRoomCommander",
            "HeroVoice"});
            this.comboBox_msg_type.Location = new System.Drawing.Point(1, 3);
            this.comboBox_msg_type.Name = "comboBox_msg_type";
            this.comboBox_msg_type.Size = new System.Drawing.Size(88, 21);
            this.comboBox_msg_type.TabIndex = 0;
            this.comboBox_msg_type.Text = "comboBox1";
            // 
            // button_sendtext
            // 
            this.button_sendtext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_sendtext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_sendtext.Location = new System.Drawing.Point(563, 0);
            this.button_sendtext.Name = "button_sendtext";
            this.button_sendtext.Size = new System.Drawing.Size(72, 24);
            this.button_sendtext.TabIndex = 2;
            this.button_sendtext.Text = "Say";
            this.button_sendtext.Click += new System.EventHandler(this.button_sendtext_Click);
            // 
            // textBox_say
            // 
            this.textBox_say.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_say.Location = new System.Drawing.Point(88, 3);
            this.textBox_say.Name = "textBox_say";
            this.textBox_say.Size = new System.Drawing.Size(474, 20);
            this.textBox_say.TabIndex = 1;
            // 
            // tabControl_ChatSelect
            // 
            this.tabControl_ChatSelect.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl_ChatSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_ChatSelect.Controls.Add(this.tab_all);
            this.tabControl_ChatSelect.Controls.Add(this.tab_system);
            this.tabControl_ChatSelect.Controls.Add(this.tab_bot);
            this.tabControl_ChatSelect.Controls.Add(this.tab_local);
            this.tabControl_ChatSelect.Controls.Add(this.tab_trade);
            this.tabControl_ChatSelect.Controls.Add(this.tab_party);
            this.tabControl_ChatSelect.Controls.Add(this.tab_clan);
            this.tabControl_ChatSelect.Controls.Add(this.tab_alliance);
            this.tabControl_ChatSelect.Controls.Add(this.tab_hero);
            this.tabControl_ChatSelect.Location = new System.Drawing.Point(-3, 21);
            this.tabControl_ChatSelect.Multiline = true;
            this.tabControl_ChatSelect.Name = "tabControl_ChatSelect";
            this.tabControl_ChatSelect.Padding = new System.Drawing.Point(10, 2);
            this.tabControl_ChatSelect.SelectedIndex = 0;
            this.tabControl_ChatSelect.Size = new System.Drawing.Size(642, 161);
            this.tabControl_ChatSelect.TabIndex = 32;
            // 
            // tab_all
            // 
            this.tab_all.Controls.Add(this.colorListBox_all);
            this.tab_all.Location = new System.Drawing.Point(4, 4);
            this.tab_all.Name = "tab_all";
            this.tab_all.Padding = new System.Windows.Forms.Padding(3);
            this.tab_all.Size = new System.Drawing.Size(634, 137);
            this.tab_all.TabIndex = 0;
            this.tab_all.Text = "All";
            this.tab_all.UseVisualStyleBackColor = true;
            // 
            // colorListBox_all
            // 
            this.colorListBox_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_all.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_all.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_all.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_all.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_all.FormattingEnabled = true;
            this.colorListBox_all.HorizontalScrollbar = true;
            this.colorListBox_all.Location = new System.Drawing.Point(0, 3);
            this.colorListBox_all.Name = "colorListBox_all";
            this.colorListBox_all.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_all.Size = new System.Drawing.Size(634, 131);
            this.colorListBox_all.TabIndex = 0;
            // 
            // tab_system
            // 
            this.tab_system.Controls.Add(this.colorListBox_system);
            this.tab_system.Location = new System.Drawing.Point(4, 4);
            this.tab_system.Name = "tab_system";
            this.tab_system.Size = new System.Drawing.Size(634, 131);
            this.tab_system.TabIndex = 6;
            this.tab_system.Text = "System";
            this.tab_system.UseVisualStyleBackColor = true;
            // 
            // colorListBox_system
            // 
            this.colorListBox_system.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_system.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_system.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_system.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_system.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_system.FormattingEnabled = true;
            this.colorListBox_system.HorizontalScrollbar = true;
            this.colorListBox_system.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_system.Name = "colorListBox_system";
            this.colorListBox_system.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_system.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_system.TabIndex = 1;
            // 
            // tab_bot
            // 
            this.tab_bot.Controls.Add(this.colorListBox_bot);
            this.tab_bot.Location = new System.Drawing.Point(4, 4);
            this.tab_bot.Name = "tab_bot";
            this.tab_bot.Size = new System.Drawing.Size(634, 131);
            this.tab_bot.TabIndex = 5;
            this.tab_bot.Text = "Bot";
            this.tab_bot.UseVisualStyleBackColor = true;
            // 
            // colorListBox_bot
            // 
            this.colorListBox_bot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_bot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_bot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_bot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_bot.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_bot.FormattingEnabled = true;
            this.colorListBox_bot.HorizontalScrollbar = true;
            this.colorListBox_bot.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_bot.Name = "colorListBox_bot";
            this.colorListBox_bot.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_bot.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_bot.TabIndex = 1;
            // 
            // tab_local
            // 
            this.tab_local.Controls.Add(this.colorListBox_local);
            this.tab_local.Location = new System.Drawing.Point(4, 4);
            this.tab_local.Name = "tab_local";
            this.tab_local.Padding = new System.Windows.Forms.Padding(3);
            this.tab_local.Size = new System.Drawing.Size(634, 131);
            this.tab_local.TabIndex = 1;
            this.tab_local.Text = "Local";
            this.tab_local.UseVisualStyleBackColor = true;
            // 
            // colorListBox_local
            // 
            this.colorListBox_local.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_local.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_local.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_local.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_local.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_local.FormattingEnabled = true;
            this.colorListBox_local.HorizontalScrollbar = true;
            this.colorListBox_local.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_local.Name = "colorListBox_local";
            this.colorListBox_local.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_local.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_local.TabIndex = 1;
            // 
            // tab_trade
            // 
            this.tab_trade.Controls.Add(this.colorListBox_trade);
            this.tab_trade.Location = new System.Drawing.Point(4, 4);
            this.tab_trade.Name = "tab_trade";
            this.tab_trade.Size = new System.Drawing.Size(634, 131);
            this.tab_trade.TabIndex = 2;
            this.tab_trade.Text = "Trade";
            this.tab_trade.UseVisualStyleBackColor = true;
            // 
            // colorListBox_trade
            // 
            this.colorListBox_trade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_trade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_trade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_trade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_trade.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_trade.FormattingEnabled = true;
            this.colorListBox_trade.HorizontalScrollbar = true;
            this.colorListBox_trade.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_trade.Name = "colorListBox_trade";
            this.colorListBox_trade.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_trade.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_trade.TabIndex = 1;
            // 
            // tab_party
            // 
            this.tab_party.Controls.Add(this.colorListBox_party);
            this.tab_party.Location = new System.Drawing.Point(4, 4);
            this.tab_party.Name = "tab_party";
            this.tab_party.Size = new System.Drawing.Size(634, 131);
            this.tab_party.TabIndex = 7;
            this.tab_party.Text = "Party";
            this.tab_party.UseVisualStyleBackColor = true;
            // 
            // colorListBox_party
            // 
            this.colorListBox_party.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_party.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_party.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_party.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_party.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_party.FormattingEnabled = true;
            this.colorListBox_party.HorizontalScrollbar = true;
            this.colorListBox_party.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_party.Name = "colorListBox_party";
            this.colorListBox_party.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_party.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_party.TabIndex = 1;
            // 
            // tab_clan
            // 
            this.tab_clan.Controls.Add(this.colorListBox_clan);
            this.tab_clan.Location = new System.Drawing.Point(4, 4);
            this.tab_clan.Name = "tab_clan";
            this.tab_clan.Size = new System.Drawing.Size(634, 131);
            this.tab_clan.TabIndex = 3;
            this.tab_clan.Text = "Clan";
            this.tab_clan.UseVisualStyleBackColor = true;
            // 
            // colorListBox_clan
            // 
            this.colorListBox_clan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_clan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_clan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_clan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_clan.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_clan.FormattingEnabled = true;
            this.colorListBox_clan.HorizontalScrollbar = true;
            this.colorListBox_clan.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_clan.Name = "colorListBox_clan";
            this.colorListBox_clan.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_clan.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_clan.TabIndex = 1;
            // 
            // tab_alliance
            // 
            this.tab_alliance.Controls.Add(this.colorListBox_ally);
            this.tab_alliance.Location = new System.Drawing.Point(4, 4);
            this.tab_alliance.Name = "tab_alliance";
            this.tab_alliance.Size = new System.Drawing.Size(634, 131);
            this.tab_alliance.TabIndex = 4;
            this.tab_alliance.Text = "Alliance";
            this.tab_alliance.UseVisualStyleBackColor = true;
            // 
            // colorListBox_ally
            // 
            this.colorListBox_ally.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_ally.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_ally.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_ally.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_ally.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_ally.FormattingEnabled = true;
            this.colorListBox_ally.HorizontalScrollbar = true;
            this.colorListBox_ally.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_ally.Name = "colorListBox_ally";
            this.colorListBox_ally.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_ally.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_ally.TabIndex = 1;
            // 
            // tab_hero
            // 
            this.tab_hero.Controls.Add(this.colorListBox_hero);
            this.tab_hero.Location = new System.Drawing.Point(4, 4);
            this.tab_hero.Name = "tab_hero";
            this.tab_hero.Size = new System.Drawing.Size(634, 131);
            this.tab_hero.TabIndex = 8;
            this.tab_hero.Text = "Hero";
            this.tab_hero.UseVisualStyleBackColor = true;
            // 
            // colorListBox_hero
            // 
            this.colorListBox_hero.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_hero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_hero.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_hero.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_hero.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_hero.FormattingEnabled = true;
            this.colorListBox_hero.HorizontalScrollbar = true;
            this.colorListBox_hero.Location = new System.Drawing.Point(2, 0);
            this.colorListBox_hero.Name = "colorListBox_hero";
            this.colorListBox_hero.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_hero.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_hero.TabIndex = 2;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 182);
            this.Controls.Add(this.panel_chat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Chat";
            this.Text = "Chat";
            this.panel_chat.ResumeLayout(false);
            this.panel_chat.PerformLayout();
            this.tabControl_ChatSelect.ResumeLayout(false);
            this.tab_all.ResumeLayout(false);
            this.tab_system.ResumeLayout(false);
            this.tab_bot.ResumeLayout(false);
            this.tab_local.ResumeLayout(false);
            this.tab_trade.ResumeLayout(false);
            this.tab_party.ResumeLayout(false);
            this.tab_clan.ResumeLayout(false);
            this.tab_alliance.ResumeLayout(false);
            this.tab_hero.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_chat;
        private System.Windows.Forms.ComboBox comboBox_msg_type;
        private System.Windows.Forms.Button button_sendtext;
        private System.Windows.Forms.TextBox textBox_say;
        private System.Windows.Forms.TabControl tabControl_ChatSelect;
        private System.Windows.Forms.TabPage tab_all;
        private ColorListBox colorListBox_all;
        private System.Windows.Forms.TabPage tab_system;
        private ColorListBox colorListBox_system;
        private System.Windows.Forms.TabPage tab_bot;
        private ColorListBox colorListBox_bot;
        private System.Windows.Forms.TabPage tab_local;
        private ColorListBox colorListBox_local;
        private System.Windows.Forms.TabPage tab_trade;
        private ColorListBox colorListBox_trade;
        private System.Windows.Forms.TabPage tab_party;
        private ColorListBox colorListBox_party;
        private System.Windows.Forms.TabPage tab_clan;
        private ColorListBox colorListBox_clan;
        private System.Windows.Forms.TabPage tab_alliance;
        private ColorListBox colorListBox_ally;
        private System.Windows.Forms.TabPage tab_hero;
        private ColorListBox colorListBox_hero;
    }
}