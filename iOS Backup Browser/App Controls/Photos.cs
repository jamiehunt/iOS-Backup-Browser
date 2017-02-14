namespace iOS_Backup_Browser.App_Controls
{
    using System.Data.SQLite;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Photos : UserControl
    {
        public Photos()
        {
            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup, string fileLocation)
        {
            var photosPath = Path.Combine(fileLocation, backup.BackupUid, "12b144c0bd44f2b3dffd9186d3f9c05b917cee25");

            if (File.Exists(photosPath))
            {
                var dbConnection = new SQLiteConnection($"Data Source={photosPath};Version=3;");
                dbConnection.Open();

                var photos = Models.Photo.Load(dbConnection);
                foreach (var photo in photos.Where(x => x.Filename.Contains("DCIM") && !x.Filename.Contains("MOV")).Take(50))
                {
                    imageList1.Images.Add(Image.FromFile(fileLocation + "\\" + backup.BackupUid + "\\" + photo.FileNameDisk));
                }

                for (var i = 0; i < imageList1.Images.Count; i++)
                {
                    var item = new ListViewItem();
                    item.ImageIndex = i;

                    listView1.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Could not load photos");
            }
        }
    }
}