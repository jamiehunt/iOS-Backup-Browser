namespace iOS_Backup_Browser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class iOS_Backup
    {
        public string BackupUid { get; set; }

        public string DeviceName { get; set; }

        public string DisplayName { get; set; }

        public string IMEI { get; set; }

        public DateTime LastBackupDate { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public string ProductVersion { get; set; }

        public string SerialNumber { get; set; }

        public string PhoneNumber { get; set; }

        // ReSharper disable once InconsistentNaming
        public string iTunesVersion { get; set; }

        public static List<iOS_Backup> LoadDirectory(string directory)
        {
            var backups = new List<iOS_Backup>();

            var dInfo = new DirectoryInfo(directory);

            foreach (var folder in dInfo.GetDirectories())
            {
                var backup = new iOS_Backup() { BackupUid = folder.Name.Trim() };

                if (File.Exists(Path.Combine(directory, folder.Name, "Info.plist")))
                {
                    var info = new PList(Path.Combine(directory, folder.Name, "Info.plist"));

                    backup.DeviceName       = info.FirstOrDefault(x => x.Key == "Device Name").Value;
                    backup.DisplayName      = info.FirstOrDefault(x => x.Key == "Display Name").Value;
                    backup.IMEI             = info.FirstOrDefault(x => x.Key == "IMEI").Value;
                    backup.LastBackupDate   = info.FirstOrDefault(x => x.Key == "Last Backup Date").Value;
                    backup.ProductName      = info.FirstOrDefault(x => x.Key == "Product Name").Value;
                    backup.ProductType      = info.FirstOrDefault(x => x.Key == "Product Type").Value;
                    backup.ProductVersion   = info.FirstOrDefault(x => x.Key == "Product Version").Value;
                    backup.SerialNumber     = info.FirstOrDefault(x => x.Key == "Serial Number").Value;
                    backup.PhoneNumber      = info.FirstOrDefault(x => x.Key == "Phone Number").Value;
                    backup.iTunesVersion    = info.FirstOrDefault(x => x.Key == "iTunes Version").Value;

                    backups.Add(backup);
                }
            }

            return backups;
        }
    }
}