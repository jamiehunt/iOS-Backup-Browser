namespace iOS_Backup_Browser.App_Controls
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using iOS_Backup_Browser.Models;

    public partial class Notes : UserControl
    {
        public Notes()
        {
            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup, string FileLocation)
        {
            string CallHistoryPath = Path.Combine(FileLocation, backup.BackupUid, "ca3bc056d4da0bbf88b5fb3be254f3b7147e639c");
            var notes = MNotes.Load(CallHistoryPath);

            foreach (var note in notes)
            {
                notesList.Items.Add(note);
            }
        }

        private void notesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var note = notesList.SelectedItem as MNotes;

            webBrowser1.Navigate("about:blank");

            if (note == null || webBrowser1.Document == null)
            {
                return;
            }

            webBrowser1.Document.OpenNew(false);
            webBrowser1.Document.Write(note.Content);
            webBrowser1.Refresh();
        }
    }
}