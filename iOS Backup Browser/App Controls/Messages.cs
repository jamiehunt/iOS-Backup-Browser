namespace iOS_Backup_Browser.App_Controls
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using iOS_Backup_Browser.Models;

    public partial class Messages : UserControl, IMessagesView
    {
        public Messages()
        {
            InitializeComponent();
        }

        public MessagesPresenter Presenter { private get; set; }

        public ListBox MessagesList { get; set; }

        private void messagesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            messagesArea.Text = string.Empty;
            var chat = MessagesList.SelectedItem as MChat;

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