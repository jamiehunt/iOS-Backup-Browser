namespace iOS_Backup_Browser.App_Controls
{
    partial class PhotoElement
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lbl_fileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::iOS_Backup_Browser.Properties.Resources.Loading_Placeholder;
            this.pictureBox.Location = new System.Drawing.Point(11, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(128, 128);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // lbl_fileName
            // 
            this.lbl_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fileName.Location = new System.Drawing.Point(3, 134);
            this.lbl_fileName.Name = "lbl_fileName";
            this.lbl_fileName.Size = new System.Drawing.Size(147, 35);
            this.lbl_fileName.TabIndex = 1;
            this.lbl_fileName.Text = "FILE NAME";
            this.lbl_fileName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PhotoElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_fileName);
            this.Controls.Add(this.pictureBox);
            this.DoubleBuffered = true;
            this.Name = "PhotoElement";
            this.Size = new System.Drawing.Size(150, 169);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label lbl_fileName;
    }
}
