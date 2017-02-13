namespace iOS_Backup_Browser.App_Controls
{
    using System.Drawing;
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

            deviceImage.Image = GetImageForDevice(backup.ProductName);
        }

        public Bitmap GetImageForDevice(string deviceName)
        {
            switch (deviceName.ToLower().Trim())
            {
                case "iphone 4":
                    return Resources.iPhone_4;
                case "iphone 4s":
                    return Resources.iPhone_4s;
                case "iphone 5":
                    return Resources.iPhone_5;
                case "iphone 5c":
                    return Resources.iPhone_5C;
                case "iphone 5s":
                    return Resources.iPhone_5s;
                case "iphone 6":
                    return Resources.iPhone_6;
                case "iphone 6s":
                    return Resources.iPhone_6S;
                case "iphone 7":
                    return Resources.iPhone_7;
                case "ipad air":
                    return Resources.iPad_air;
                case "ipad air 2":
                    return Resources.iPad_air_2;
                default:
                    return null;
            }
        }
    }
}