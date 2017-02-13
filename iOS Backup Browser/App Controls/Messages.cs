namespace iOS_Backup_Browser.App_Controls
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using iOS_Backup_Browser.Models;

    public partial class Messages : UserControl
    {
        public Messages()
        {
            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup, string fileLocation)
        {
            var messagesPath = Path.Combine(fileLocation, backup.BackupUid, "3d0d7e5fb2ce288813306e4d4636395e047a3d28");
            if (!File.Exists(messagesPath))
            {
                messagesPath = Path.Combine(fileLocation, backup.BackupUid, "3d\\", "3d0d7e5fb2ce288813306e4d4636395e047a3d28");
            }

            var chats = MChat.Load(messagesPath);

            foreach (var chat in chats)
            {
                messagesList.Items.Add(chat);
            }
        }

        private void messagesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            messagesArea.Text = string.Empty;
            var chat = messagesList.SelectedItem as MChat;

            if (chat == null)
            {
                return;
            }

            foreach (var message in chat.Messages.OrderByDescending(x => x.MessageId).Take(500))
            {
                // TODO: Come up with a better UI element for this.
                messagesArea.Text += message.Text + Environment.NewLine + Environment.NewLine;
            }
        }
    }
}