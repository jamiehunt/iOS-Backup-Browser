namespace iOS_Backup_Browser.App_Controls
{
    using System.IO;
    using iOS_Backup_Browser.Models;

    public class MessagesPresenter
    {
        private readonly IMessagesView _messagesView;
        public MessagesPresenter(IMessagesView messagesView, IMessagesRepository messagesRepository)
        {
            _messagesView = messagesView;
        }

        public void LoadBackup(iOS_Backup backup, string backupDirectory)
        {
            var messagesPath = Path.Combine(backupDirectory, backup.BackupUid, "3d0d7e5fb2ce288813306e4d4636395e047a3d28");
            if (!File.Exists(messagesPath))
            {
                messagesPath = Path.Combine(backupDirectory, backup.BackupUid, "3d\\", "3d0d7e5fb2ce288813306e4d4636395e047a3d28");
            }

            var chats = MChat.Load(messagesPath);

            foreach (var chat in chats)
            {
                _messagesView.MessagesList.Items.Add(chat);
            }
        }
    }
}
