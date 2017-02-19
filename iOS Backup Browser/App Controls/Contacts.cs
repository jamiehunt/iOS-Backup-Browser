namespace iOS_Backup_Browser.App_Controls
{
    using iOS_Backup_Browser.Constants;
    using iOS_Backup_Browser.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Models.Contacts;
    using Properties;

    public partial class Contacts : UserControl
    {
        private readonly IBackupFileService _backupFileService;
        private List<Contact> _contacts;
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

                    _contacts = Models.Contacts.Contact.Load(dbConnection);
                    foreach (var contact in _contacts.OrderBy(x => x.DisplayName))
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
            label1.Text += Environment.NewLine + "Phone Home: " + contact.PhoneHome;
            label1.Text += Environment.NewLine + "Phone Work: " + contact.PhoneWork;
            label1.Text += Environment.NewLine + "";
            label1.Text += Environment.NewLine + "Email: " + contact.Email;
            label1.Text += Environment.NewLine + "Address: " + contact.Address;
            label1.Text += Environment.NewLine + "City: " + contact.City;

            btnExportCsv.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var file = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    file.WriteLine("First Name, Last Name, Mobile Phone, Home Phone, Work Phone, Email Address, Address, City");
                    foreach (var contact in _contacts)
                    {
                        file.WriteLine(contact.First + "," + contact.Last + "," + contact.PhoneMobile + "," +
                                       contact.PhoneHome + "," + contact.PhoneWork + "," + contact.Email + "," +
                                       contact.Address.Replace(",", "") + "," + contact.City);
                    }
                }
            }
        }
    }
}
