using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using StockManagementWithXml.Model;
using StockManagementWithXml.XmlHelpers;
using static Google.Apis.Drive.v3.DriveService;

namespace StockManagementWithXml.Forms
{
    public partial class MainPage : Form
    {
        #region Lifecycle Methods
        public MainPage()
        {
            InitializeComponent();

        }


        private void MainPage_Load(object sender, EventArgs e)
        {
            var backups = BackupXmlHelper.GetListFromXml();
            var lastBackup = backups.LastOrDefault();
            if (lastBackup != null)
            {
                var lastBackupDate = DateTime.ParseExact(lastBackup.Date, "dd.MM.yyyy", null);
                if (lastBackupDate >= DateTime.Now.AddDays(-6)) return;
                GoogleDriveHelper.BackupAllFilesToDrive();
                BackupXmlHelper.Insert(new Backup() { Date = DateTime.Now.ToString("dd.MM.yyyy") });

            }
        }
        #endregion
        #region Events
 

        private void AddStockButton_Click(object sender, EventArgs e)
        {
            var stockManagementForm = new StockManagementForm();
            stockManagementForm.Show();
        }

        private void AddShelveButton_Click(object sender, EventArgs e)
        {
            var addShelve = new ShelveManagement();
            addShelve.Show();
        }

        private void PartTypeManagementBtn_Click(object sender, EventArgs e)
        {
            var partTypeManagement = new PartTypeManagement();
            partTypeManagement.Show();
        }

        private void userManagementButton_Click(object sender, EventArgs e)
        {
            var userManagement = new UserManagement();
            userManagement.Show();
        }

        private void actitivitiesButton_Click(object sender, EventArgs e)
        {
            var activities = new Activities();
            activities.Show();
        }

        #endregion

        private void UpdatePricesButton_Click(object sender, EventArgs e)
        {
            UpdatePrices updateForm = new UpdatePrices();
            updateForm.Show();
        }

        private void ManuelBackupButton_Click(object sender, EventArgs e)
        {
            GoogleDriveHelper.BackupAllFilesToDrive();
            SplashSc
            MessageBox.Show("!!MANUEL YEDEKLEME BAŞARILI!!");
        }
    }
}
