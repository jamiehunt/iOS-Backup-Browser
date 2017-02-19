namespace iOS_Backup_Browser.App_Controls
{
    using iOS_Backup_Browser.Constants;
    using iOS_Backup_Browser.Services;
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using Properties;

    public partial class Contacts : UserControl
    {
        private readonly IBackupFileService _backupFileService;
        public Contacts(IBackupFileService backupFileService)
        {
            _backupFileService = backupFileService;

            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup, string fileLocation)
        {
            try
            {
                using (var dbConnection = _backupFileService.GetFileAsSQLiteConnection(backup, fileLocation, iOSBackupFile.iOSBackupFileName.AddressBookDb))
                {
                    dbConnection.Open();

                    var contacts = Models.Contacts.Contact.Load(dbConnection);
                    foreach (var contact in contacts.OrderBy(x => x.DisplayName))
                    {
                        contactsList.Items.Add(contact);
                    }

                    dbConnection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.Contacts_Could_not_load_address_book + Environment.NewLine + e.Message);
            }
        }

        private void contactsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var contact = (Models.Contacts.Contact)contactsList.SelectedItem;

            lblDisplayName.Text = contact.ToString();
            lblJobTitle.Text = contact.JobTitle;
            lblOrganization.Text = contact.Department + ", " + contact.Organization;

            label1.Text = "Phone Mobile: " + contact.PhoneMobile;
            label1.Text += "\r\nPhone Home: " + contact.PhoneHome;
            label1.Text += "\r\nPhone Work: " + contact.PhoneWork;
            label1.Text += "\r\n";
            label1.Text += "\r\nEmail: " + contact.Email;
            label1.Text += "\r\nAddress: " + contact.Address;
            label1.Text += "\r\nCity: " + contact.City;
        }
    }
}
