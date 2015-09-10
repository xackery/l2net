namespace L2_login
{
    partial class ScriptDebugger
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
            this.button_close = new System.Windows.Forms.Button();
            this.button_snapshot = new System.Windows.Forms.Button();
            this.button_pause_resume = new System.Windows.Forms.Button();
            this.treeView_variables = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_close.Location = new System.Drawing.Point(460, 12);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(88, 23);
            this.button_close.TabIndex = 2;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_snapshot
            // 
            this.button_snapshot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_snapshot.Location = new System.Drawing.Point(12, 12);
            this.button_snapshot.Name = "button_snapshot";
            this.button_snapshot.Size = new System.Drawing.Size(88, 23);
            this.button_snapshot.TabIndex = 0;
            this.button_snapshot.Text = "Snapshot";
            this.button_snapshot.Click += new System.EventHandler(this.button_snapshot_Click);
            // 
            // button_pause_resume
            // 
            this.button_pause_resume.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_pause_resume.Location = new System.Drawing.Point(106, 12);
            this.button_pause_resume.Name = "button_pause_resume";
            this.button_pause_resume.Size = new System.Drawing.Size(132, 23);
            this.button_pause_resume.TabIndex = 1;
            this.button_pause_resume.Text = "Pause/Resume Script";
            this.button_pause_resume.Click += new System.EventHandler(this.button_pause_resume_Click);
            // 
            // treeView_variables
            // 
            this.treeView_variables.Location = new System.Drawing.Point(12, 41);
            this.treeView_variables.Name = "treeView_variables";
            this.treeView_variables.Size = new System.Drawing.Size(536, 463);
            this.treeView_variables.TabIndex = 3;
            // 
            // ScriptDebugger
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(560, 516);
            this.ControlBox = false;
            this.Controls.Add(this.treeView_variables);
            this.Controls.Add(this.button_pause_resume);
            this.Controls.Add(this.button_snapshot);
            this.Controls.Add(this.button_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScriptDebugger";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Debugger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_snapshot;
        private System.Windows.Forms.Button button_pause_resume;
        private System.Windows.Forms.TreeView treeView_variables;
    }
}