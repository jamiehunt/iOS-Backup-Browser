namespace iOS_Backup_Browser.Services
{
    using Constants;
    using System.Data.SQLite;

    public interface IBackupFileService
    {
        SQLiteConnection GetFileAsSQLiteConnection(iOS_Backup backup, string fileLocation, iOSBackupFile.iOSBackupFileName fileName);
    }
}
