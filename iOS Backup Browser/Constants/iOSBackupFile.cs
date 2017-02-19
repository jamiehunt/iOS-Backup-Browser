namespace iOS_Backup_Browser.Constants
{
    using System;

    public class iOSBackupFile
    {
        public enum iOSBackupFileName
        {
            SmsDb,
            AddressBookDb,
            CalendarDb,
            NotesDb,
            CallHistoryDb,
            LocationHistoryDb
        }

        public static string ToFileName(iOSBackupFileName e)
        {
            switch (e)
            {
                case iOSBackupFileName.SmsDb:
                    return "3d0d7e5fb2ce288813306e4d4636395e047a3d28";
                case iOSBackupFileName.AddressBookDb:
                    return "31bb7ba8914766d4ba40d6dfb6113c8b614be442";
                case iOSBackupFileName.CalendarDb:
                    return "2041457d5fe04d39d0ab481178355df6781e6858";
                case iOSBackupFileName.NotesDb:
                    return "ca3bc056d4da0bbf88b5fb3be254f3b7147e639c";
                case iOSBackupFileName.CallHistoryDb:
                    return "2b2b0084a1bc3a5ac8c27afdf14afb42c61a19ca";
                case iOSBackupFileName.LocationHistoryDb:
                    return "4096c9ec676f2847dc283405900e284a7c815836";
                default:
                    throw new ArgumentException("Enum value not recognised");
            }
        }
    }
}
