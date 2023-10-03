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
    }
}
