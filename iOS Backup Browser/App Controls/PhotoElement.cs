namespace iOS_Backup_Browser.App_Controls
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public partial class PhotoElement : UserControl
    {
        private readonly string _filename;

        public PhotoElement(string filename, string friendlyFilename)
        {
            InitializeComponent();

            _filename = filename;
            lbl_fileName.Text = Path.GetFileName(friendlyFilename);
        }

        public void InitializeImage()
        {
            using (var tempImage = Image.FromFile(_filename))
            {
                var callback = new Image.GetThumbnailImageAbort(ThumbCallback);
                var thumb = tempImage.GetThumbnailImage(128, 128, callback, IntPtr.Zero);

                pictureBox.Image = thumb;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public bool ThumbCallback()
        {
            return false;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            InitializeImage();
        }
    }
}