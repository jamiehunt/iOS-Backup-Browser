﻿namespace iOS_Backup_Browser.App_Controls
{
    partial class Messages
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.messagesList = new System.Windows.Forms.ListBox();
            this.messagesArea = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.messagesList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.messagesArea);
            this.splitContainer1.Size = new System.Drawing.Size(558, 340);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 0;
            // 
            // messagesList
            // 
            this.messagesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesList.FormattingEnabled = true;
            this.messagesList.Location = new System.Drawing.Point(0, 0);
            this.messagesList.Name = "messagesList";
            this.messagesList.Size = new System.Drawing.Size(186, 340);
            this.messagesList.TabIndex = 1;
            this.messagesList.SelectedIndexChanged += new System.EventHandler(this.messagesList_SelectedIndexChanged);
            // 
            // messagesArea
            // 
            this.messagesArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesArea.Location = new System.Drawing.Point(0, 0);
            this.messagesArea.Name = "messagesArea";
            this.messagesArea.Size = new System.Drawing.Size(368, 340);
            this.messagesArea.TabIndex = 0;
            this.messagesArea.Text = "";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Messages";
            this.Size = new System.Drawing.Size(558, 340);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox messagesList;
        private System.Windows.Forms.RichTextBox messagesArea;
    }
}
