namespace iOS_Backup_Browser
{
    using System.Windows.Forms;

    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_save_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_backupLocation.Text) && System.IO.Directory.Exists(tb_backupLocation.Text))
            {
                Properties.Settings.Default.BackupLocation = tb_backupLocation.Text;
            }

            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Preferences_Load(object sender, System.EventArgs e)
        {
            PopulateInitialSettingValues();
        }

        private void PopulateInitialSettingValues()
        {
            tb_backupLocation.Text = Properties.Settings.Default.BackupLocation;
        }

        private void btn_browseBackupLocation_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tb_backupLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}