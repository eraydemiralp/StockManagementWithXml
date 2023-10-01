using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

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
            doBackup();
        }
        #endregion

        #region Custom Methods
        private void doBackup()
        {
            //try
            //{
            //    BackupTableAdapter backupTableAdapter = new BackupTableAdapter();
            //    BackupDataTable backups = backupTableAdapter.SelectLastBackupDate();
            //    int lastBackupDays = 0;
            //    if (backups == null && backups.Rows == null || backups.Rows.Count == 0)
            //    {
            //        lastBackupDays = 7;
            //    }
            //    else
            //    {
            //        DateTime lastBackupDate = DateTime.ParseExact(backups.Rows[0]["LastBackupDate"].ToString(), "dd.MM.yyyy", null);
            //        DateTime now = DateTime.ParseExact(DateTime.Now.ToString("dd.MM.yyyy"), "dd.MM.yyyy", null);
            //        lastBackupDays = (now - lastBackupDate).Days;
            //    }
            //    if (lastBackupDays >= 7)
            //    {

            //        string backupPath = "D:\\Parça Yönetimi Uygulama Yedek\\" + DateTime.Now.ToString("ddMMyyyy");
            //        if (!Directory.Exists(backupPath))
            //        {
            //            Directory.CreateDirectory(backupPath);
            //        }

            //        using (SqlConnection conn = SqlHelper.GetSqlConnection())
            //        {
            //            string sqlStmt = string.Format("backup database [" + Application.StartupPath + "\\Database\\StockManagement.mdf] to disk='{0}'", backupPath);
            //            using (SqlCommand bu2 = new SqlCommand(sqlStmt, conn))
            //            {
            //                conn.Open();
            //                bu2.ExecuteNonQuery();
            //                conn.Close();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "MainPage", "doBackup", ex));
            //}
        }
        #endregion

        #region Events

        private void AddStockButton_Click(object sender, EventArgs e)
        {
            //StockManagementForm stockManagementForm  = new StockManagementForm();
            //stockManagementForm.Show();
        }

        private void AddShelveButton_Click(object sender, EventArgs e)
        {
            ShelveManagementForm addShelveForm = new ShelveManagementForm();
            addShelveForm.Show();
        }

        private void PartTypeManagementBtn_Click(object sender, EventArgs e)
        {
            PartTypeManagementForm partTypeManagementForm = new PartTypeManagementForm();
            partTypeManagementForm.Show();
        }

        private void userManagementButton_Click(object sender, EventArgs e)
        {
            //UserManagement userManagement = new UserManagement();
            //userManagement.Show();
        }

        private void actitivitiesButton_Click(object sender, EventArgs e)
        {
            //Activities activities = new Activities();
            //activities.Show();
        }

        #endregion
    }
}
