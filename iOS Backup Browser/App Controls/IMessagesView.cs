namespace iOS_Backup_Browser.App_Controls
{
    using System.Windows.Forms;

    public interface IMessagesView
    {
        MessagesPresenter Presenter { set; }

        ListBox MessagesList { get; set; }
    }
}
