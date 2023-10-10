using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockManagementWithXml.Model;
using System.Linq;
using StockManagementWithXml.XmlHelpers;

namespace StockManagementWithXml.Forms
{
    public partial class UserManagement : Form
    {
        public UserManagement()
        {
            InitializeComponent();
        }
        private void UserManagement_Load(object sender, EventArgs e)
        {
            PopulateGridView();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                var enteredUserName = userNameTextBox.Text;
                const string enterUserNameErrMsg = "Lütfen kullanıcı adı giriniz!!";
                var sameUserExistErrMsg = "Aynı isimde kullanıcı tabloda mevcut lütfen başka bir kullanıcı ismi giriniz!";
                if (string.IsNullOrEmpty(enteredUserName))
                {
                    MessageBox.Show(enterUserNameErrMsg);
                    return;

                }
                var users = UserXmlHelper.GetListFromXml();
                var userExists = users.Any(r => r.Name.Trim().Equals(enteredUserName.Trim()));
                if (userExists)
                {
                    MessageBox.Show(sameUserExistErrMsg);
                    return;
                }
                var user = new User()
                {
                    Id = UserXmlHelper.GenerateId(),
                    Name = enteredUserName.TrimStart(' ').TrimEnd(' ')
                };
                UserXmlHelper.Insert(user);
                users.Add(user);
                userDataGridView.DataSource = users;
                userNameTextBox.Clear();
                userNameTextBox.Focus();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "UserManagement", "addButton_Click", ex));
                throw;
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                const string selectUserErrorMsg = "Lütfen tablodan kullanıcı seçiniz";
                const string selectOtherUserErorMsg = "Kullanıcı tabloda mevcut. Lütfen başka bir kullanıcı ismi giriniz!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show(selectUserErrorMsg);
                    userDataGridView.Focus();
                    return;
                }

                var users = UserXmlHelper.GetListFromXml();
                var enteredUserName = userNameTextBox.Text;
                if (users.Any(u => u.Name.Trim() == enteredUserName.TrimStart(' ').TrimEnd(' ')))
                {
                    MessageBox.Show(selectOtherUserErorMsg);
                    return;
                }
                UserXmlHelper.Update(idTextBox.Text, enteredUserName.TrimStart(' ').TrimEnd(' '));
                foreach (var user in users.Where(user => user.Id == idTextBox.Text))
                {
                    user.Name = enteredUserName.TrimStart(' ').TrimEnd(' ');
                }
                userNameTextBox.Clear();
                userNameTextBox.Focus();
                PopulateGridView();
                const string updateSuccessMsg = "Güncelleme işlemi başarılı!";
                MessageBox.Show(updateSuccessMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "UserManagement", "updateButton_Click", ex));
                throw;
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectUserErrMsg = "Lütfen tablodan kullanıcı seçiniz";
                var userNotExistErrMsg = "Kullanıcı tabloda bulunamadı!!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show(selectUserErrMsg);
                }

                List<User> users = UserXmlHelper.GetListFromXml();

                if (users.All(s => s.Id != idTextBox.Text))
                {
                    MessageBox.Show(userNotExistErrMsg);
                    return;
                }
                UserXmlHelper.Delete(idTextBox.Text);
                var userToDelete = users.FirstOrDefault(s => s.Id == idTextBox.Text);
                users.Remove(userToDelete);
                userNameTextBox.Clear();
                userNameTextBox.Focus();
                idTextBox.Clear();
                PopulateGridView();
                const string deleteSuccessMsg = "Silme işlemi başarılı!";
                MessageBox.Show(deleteSuccessMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "UserManagement", "deleteButton_Click", ex));
                throw;
            }
        }
        private void userDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rowIndex = e.RowIndex;
                idTextBox.Text = userDataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                userNameTextBox.Text = userDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "UserManagement", "userDataGridView_CellClick", ex));
                throw;
            }
        }
        private void PopulateGridView()
        {
            try
            {
                userBindingSource.DataSource = UserXmlHelper.GetListFromXml();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "UserManagement", "PopulateGridView", ex));
                throw;
            }
        }
    }
}
