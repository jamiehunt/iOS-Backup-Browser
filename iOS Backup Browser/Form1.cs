namespace iOS_Backup_Browser
{
    using iOS_Backup_Browser.App_Controls;
    using iOS_Backup_Browser.Services;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Properties;

    public partial class Form1 : Form
    {
        private string _backupDirectory;
        private bool _loadBackups = true;
        private List<iOS_Backup> _loadedBackups;

        public Form1()
        {
            InitializeComponent();

            InitializeBackupDirectory();
        }

        private void InitializeBackupDirectory()
        {
            if (string.IsNullOrEmpty(Settings.Default.BackupLocation) || !Directory.Exists(Settings.Default.BackupLocation))
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                _backupDirectory = Path.Combine(appDataPath, @"Apple Computer\MobileSync\Backup");

                if (!Directory.Exists(_backupDirectory))
                {
                    _loadBackups = false;
                }
            }
            else
            {
                _backupDirectory = Settings.Default.BackupLocation;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!_loadBackups)
            {
                MessageBox.Show(Resources.Main_iOS_Backup_Path_Invalid, Resources.Main_Product_Name);
                return;
            }

            InitializeBackups();
        }

        private void InitializeBackups()
        {
            _loadedBackups = iOS_Backup.LoadDirectory(_backupDirectory);

            foreach (var backup in _loadedBackups)
            {
                var newNode = new TreeNode($"{backup.DeviceName} ({backup.ProductName})")
                {
                    Tag = backup.BackupUid
                };

                var nodeId = treeView1.Nodes[0].Nodes.Add(newNode);

                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Contacts"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Call History"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Messages"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Notes"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Voicemail"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Voice Memos"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Internet"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Photos"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Applications"));
                treeView1.Nodes[0].Nodes[nodeId].Nodes.Add(new TreeNode("Raw Data"));
            }

            treeView1.Nodes[0].Expand();
        }

        private void LoadPanel(UserControl panel)
        {
            panel.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Clear();
            try
            {
                splitContainer1.Panel2.Controls.Add(panel);
            }
            catch (Win32Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null)
            {
                splitContainer1.Panel2.Controls.Clear();
                return;
            }

            if (treeView1.SelectedNode.Parent.Text == "Backups")
            {
                // This is a top-level, return the info panel.
                var infoPanel = new App_Controls.Info();
                infoPanel.SetBackup(_loadedBackups.FirstOrDefault(x => x.BackupUid == treeView1.SelectedNode.Tag.ToString()));
                LoadPanel(infoPanel);

                return;
            }

            var currentBackup = _loadedBackups.FirstOrDefault(x => x.BackupUid == treeView1.SelectedNode.Parent.Tag.ToString());
            statusLabel.Text = currentBackup.DeviceName;

            switch (treeView1.SelectedNode.Text)
            {
                case "Contacts":
                    // Load contacts panel
                    var contactsPanel = new App_Controls.Contacts(new BackupFileService());
                    statusLabel.Text += " - Contacts";
                    contactsPanel.SetBackup(currentBackup, _backupDirectory);
                    LoadPanel(contactsPanel);

                    break;
                case "Call History":
                    // Load Call History panel
                    var callHistoryPanel = new App_Controls.CallHistory();
                    callHistoryPanel.SetBackup(currentBackup, _backupDirectory);
                    LoadPanel(callHistoryPanel);
                    break;
                case "Messages":
                    // Load Messages panel
                    var view = new App_Controls.Messages();
                    var presenter = new MessagesPresenter(view, null);
                    presenter.LoadBackup(currentBackup, _backupDirectory);

                    LoadPanel(view);
                    break;
                case "Notes":
                    // Load Notes panel
                    var notesPanel = new App_Controls.Notes();
                    notesPanel.SetBackup(currentBackup, _backupDirectory);
                    LoadPanel(notesPanel);
                    break;
                case "Voicemail":
                    // TODO: Load Voicemail panel
                    break;
                case "Voice Memos":
                    // TODO: Load Voice Memos panel
                    break;
                case "Internet":
                    // TODO: Load Internet panel
                    break;
                case "Photos":
                    // Load the Photos Panel
                    var photosPanel = new App_Controls.Photos();
                    photosPanel.SetBackup(currentBackup, _backupDirectory);
                    LoadPanel(photosPanel);
                    break;
                case "Applications":
                    // TODO: Load Applications panel
                    break;
                case "Raw Data":
                    // TODO: Load Raw Data panel
                    break;
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var preferences = new Preferences();

            if (preferences.ShowDialog() == DialogResult.OK)
            {
                InitializeBackupDirectory();

                treeView1.Nodes[0].Nodes.Clear();

                InitializeBackups();
            }
        }
    }
}