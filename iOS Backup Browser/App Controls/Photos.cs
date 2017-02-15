namespace iOS_Backup_Browser.App_Controls
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Photos : UserControl
    {
        public Photos()
        {
            InitializeComponent();
            photoElements = new List<PhotoElement>();
        }

        private List<PhotoElement> photoElements;

        public void SetBackup(iOS_Backup backup, string fileLocation)
        {
            var photosPath = Path.Combine(fileLocation, backup.BackupUid, "12b144c0bd44f2b3dffd9186d3f9c05b917cee25");

            if (File.Exists(photosPath))
            {
                var dbConnection = new SQLiteConnection($"Data Source={photosPath};Version=3;");
                dbConnection.Open();

                var photos = Models.Photo.Load(dbConnection);

                flowLayoutPanel1.SuspendLayout();

                foreach (var photo in photos.Where(x => x.Filename.Contains("DCIM") && !x.Filename.Contains("MOV") && !x.Filename.ToUpperInvariant().Contains("MP4")))
                {
                    var filenameOnDisk = fileLocation + "\\" + backup.BackupUid + "\\" + photo.FileNameDisk;
                    var photoElement = new PhotoElement(filenameOnDisk, photo.Filename);

                    photoElements.Add(photoElement);
                    flowLayoutPanel1.Controls.Add(photoElement);
                }

                flowLayoutPanel1.ResumeLayout(true);

                flowLayoutPanel1.AutoScroll = true;

                var bw = new BackgroundWorker();
                
                bw.DoWork += delegate
                {
                    foreach (var photoElement in photoElements)
                    {
                        photoElement.InitializeImage();
                    }
                };
                
                bw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Could not load photos");
            }
        }
        
        public bool ThumbCallback()
        {
            return false;
        }
    }
}