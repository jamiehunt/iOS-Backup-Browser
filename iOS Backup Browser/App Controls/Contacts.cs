namespace iOS_Backup_Browser.App_Controls
{
    using System;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Properties;

    public partial class Contacts : UserControl
    {
        public Contacts()
        {
            InitializeComponent();
        }

        public void SetBackup(iOS_Backup backup, string fileLocation)
        {
            var addressBookPath = Path.Combine(fileLocation, backup.BackupUid, "31bb7ba8914766d4ba40d6dfb6113c8b614be442");

            if (!File.Exists(addressBookPath))
            {
                addressBookPath = Path.Combine(fileLocation, backup.BackupUid, "31\\", "31bb7ba8914766d4ba40d6dfb6113c8b614be442");
            }

            if (File.Exists(addressBookPath))
            {
                var dbConnection = new SQLiteConnection($"Data Source={addressBookPath};Version=3;");
                dbConnection.Open();

                var contacts = Models.Contacts.Contact.Load(dbConnection);
                foreach (var contact in contacts.OrderBy(x => x.DisplayName))
                {
                    contactsList.Items.Add(contact);
                }
            }
            else
            {
                MessageBox.Show(Resources.Contacts_Could_not_load_address_book);
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
