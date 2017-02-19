namespace iOS_Backup_Browser.Services
{
    using iOS_Backup_Browser.Constants;
    using System;
    using System.Data.SQLite;
    using System.IO;

    public sealed class BackupFileService : IBackupFileService
    {
        public SQLiteConnection GetFileAsSQLiteConnection(iOS_Backup backup, string fileLocation, iOSBackupFile.iOSBackupFileName fileName)
        {
            var filename = iOSBackupFile.ToFileName(fileName);
            var filePath = Path.Combine(fileLocation, backup.BackupUid, filename);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(fileLocation, backup.BackupUid, "31\\", "31bb7ba8914766d4ba40d6dfb6113c8b614be442");

                if (!File.Exists(filePath))
                {
                    throw new Exception("Backup file could not be found.");
                }
            }

            var dbConnection = new SQLiteConnection($"Data Source={filePath};Version=3;");

            return dbConnection;
        }
    }
}
