namespace iOS_Backup_Browser.App_Controls
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class PhotoElement : UserControl
    {
        private readonly string _filename;
        private bool _imageInitialized = false;

        public PhotoElement(string filename, string friendlyFilename)
        {
            InitializeComponent();

            _filename = filename;
            lbl_fileName.Text = Path.GetFileName(friendlyFilename);
        }

        public void InitializeImage()
        {
            try
            {
                using (var tempImage = Image.FromFile(_filename))
                {
                    var callback = new Image.GetThumbnailImageAbort(ThumbCallback);
                    var thumb = tempImage.GetThumbnailImage(128, 128, callback, IntPtr.Zero);
                    
                    int rotate = 0;
                    int orientationId = 0x112;

                    if (tempImage.PropertyItems.Any(x => x.Id == orientationId))
                    {
                        var orientation = tempImage.GetPropertyItem(orientationId);

                        if (orientation.Value[0] == 6)
                            thumb.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (orientation.Value[0] == 8)
                            thumb.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (orientation.Value[0] == 3)
                            thumb.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    }

                    pictureBox.Image = thumb;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                _imageInitialized = true;
            }
            catch (OutOfMemoryException)
            {
                _imageInitialized = false;
            }
        }

        public bool ThumbCallback()
        {
            return false;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (_imageInitialized)
            {
                Process.Start(_filename);
            }
            else
            {
                InitializeImage();
            }
        }
    }
}