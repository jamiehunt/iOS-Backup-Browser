namespace iOS_Backup_Browser.App_Controls
{
    using System.IO;
    using System.Windows.Forms;
    using Models;

    public partial class CallHistory : UserControl
    {
        public CallHistory()
        {
            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup, string fileLocation)
        {
            var callHistoryPath = Path.Combine(fileLocation, backup.BackupUid, "2b2b0084a1bc3a5ac8c27afdf14afb42c61a19ca");
            var callHistory = MCallHistory.Load(callHistoryPath);

            var source = new BindingSource {DataSource = callHistory};

            callHistoryGridView.DataSource = source;
        }
    }
}
