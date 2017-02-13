namespace iOS_Backup_Browser.App_Controls
{
    using System.Globalization;
    using System.Windows.Forms;
    using iOS_Backup_Browser.Properties;

    public partial class Info : UserControl
    {
        public Info()
        {
            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup)
        {
            lblDeviceName.Text      = backup.DeviceName;
            lblPhoneNumber.Text     = backup.PhoneNumber;
            lblDeviceModel.Text     = backup.ProductName;
            lblIosVersion.Text      = backup.ProductVersion;
            lblImei.Text            = backup.IMEI;
            lblSerialNumber.Text    = backup.SerialNumber;
            lblBackupDate.Text      = backup.LastBackupDate.ToString(CultureInfo.InvariantCulture);
            lblItunesVersion.Text   = backup.iTunesVersion;
            lblBackupSize.Text      = "Unknown";

            if (backup.ProductName == null)
                return;

            switch (backup.ProductName.ToLower().Trim())
            {
                case "iphone 4":
                    deviceImage.Image = Resources.iPhone_4;
                    break;
                case "iphone 4s":
                    deviceImage.Image = Resources.iPhone_4s;
                    break;
                case "iphone 5":
                    deviceImage.Image = Resources.iPhone_5;
                    break;
                case "iphone 5c":
                    deviceImage.Image = Resources.iPhone_5C;
                    break;
                case "iphone 5s":
                    deviceImage.Image = Resources.iPhone_5s;
                    break;
                case "iphone 6":
                    deviceImage.Image = Resources.iPhone_6;
                    break;
                case "iphone 6s":
                    deviceImage.Image = Resources.iPhone_6S;
                    break;
                case "ipad air":
                    deviceImage.Image = Resources.iPad_air;
                    break;
                case "ipad air 2":
                    deviceImage.Image = Resources.iPad_air_2;
                    break;
            }
        }
    }
}