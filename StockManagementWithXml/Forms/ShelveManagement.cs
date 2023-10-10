using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockManagementWithXml.Model;
using System.Linq;
using StockManagementWithXml.XmlHelpers;

namespace StockManagementWithXml.Forms
{
    public partial class ShelveManagement : Form
    {
        #region Lifecycle Events
        public ShelveManagement()
        {
            InitializeComponent();
        }

        private void ShelveManagementForm_Load(object sender, EventArgs e)
        {
            PopulateGridView();
        }
        #endregion

        #region Events
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                var enteredShelveName = shelveNameTextBox.Text;
                if (string.IsNullOrEmpty(enteredShelveName))
                {
                    MessageBox.Show("Raf İsmi alanı boş olamaz!!");
                    shelveNameTextBox.Focus();
                    return;
                }
                List<Shelve> shelves = ShelveXmlHelper.GetListFromXml();
                bool shelveExist = shelves.Any(r => r.Name.Trim().Equals(enteredShelveName.Trim()));
                if (shelveExist)
                {
                    MessageBox.Show("Aynı isimde raf tabloda mevcut lütfen başka bir raf ismi giriniz!");
                    return;
                }
                Shelve shelve = new Shelve();
                shelve.Id= ShelveXmlHelper.GenerateId();
                shelve.Name = enteredShelveName.TrimStart(' ').TrimEnd(' ');
                ShelveXmlHelper.Insert(shelve);
                shelves.Add(shelve);
                shelveDataGridView.DataSource = shelves;
                shelveNameTextBox.Clear();
                shelveNameTextBox.Focus();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagement", "addButton_Click", ex));
                throw ex;
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                const string selectShelveErrorMsg = "Lütfen tablodan raf seçiniz";
                const string selectOtherShelveErrMsg = "Raf tabloda mevcut. Lütfen başka bir raf ismi giriniz!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show(selectShelveErrorMsg);
                    shelveDataGridView.Focus();
                    return;
                }

                var shelves = ShelveXmlHelper.GetListFromXml();
                var enteredShelveName = shelveNameTextBox.Text;
                if (shelves.Any(p => p.Name.Trim() == enteredShelveName.TrimStart(' ').TrimEnd(' ')))
                {
                    MessageBox.Show(selectOtherShelveErrMsg);
                    return;
                }

                var stockList = StockXmlHelper.GetListFromXml();
                var hasStockWithShelve = stockList.Any(s => s.ShelveId.Equals(idTextBox.Text));
                if (hasStockWithShelve)
                {
                    var existingStockList = stockList.FindAll(s => s.ShelveId == idTextBox.Text).ToList();
                    if (existingStockList.Count > 0)
                    {
                        foreach (var stock in existingStockList)
                        {
                            StockXmlHelper.UpdateShelveName(stock.Id, enteredShelveName.TrimStart(' ').TrimEnd(' '));
                        }

                    }
                }

                ShelveXmlHelper.Update(idTextBox.Text, enteredShelveName.TrimStart(' ').TrimEnd(' '));
                foreach (var shelf in shelves.Where(shelf => shelf.Id == idTextBox.Text))
                {
                    shelf.Name = enteredShelveName.TrimStart(' ').TrimEnd(' ');
                }
                shelveNameTextBox.Clear();
                shelveNameTextBox.Focus();
                PopulateGridView();
                const string updateSuccessMsg = "Güncelleme işlemi başarılı!";
                MessageBox.Show(updateSuccessMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagement", "updateButton_Click", ex));
                throw;
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectShelfErrMsg = "Lütfen tablodan bir raf seçiniz";
                var stockExistErrorMsg = "Rafa bağlı parça bulunmaktadır. Öncelikle Stok Yönetimi ekranından rafa ait parçaları siliniz!!";
                var shelveNotExistErrorMsg = "Raf tabloda bulunamadı!!!";

                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show(selectShelfErrMsg);
                    shelveDataGridView.Focus();
                    return;
                }
                List<Shelve> shelves = ShelveXmlHelper.GetListFromXml();

                if (shelves.All(s => s.Id != idTextBox.Text))
                {
                    MessageBox.Show(shelveNotExistErrorMsg);
                    return;
                }


                var stockList = StockXmlHelper.GetListFromXml();
                if (stockList.Any(s => s.ShelveId.Equals(idTextBox.Text)))
                {

                    MessageBox.Show(stockExistErrorMsg);
                    return;
                }
                ShelveXmlHelper.Delete(idTextBox.Text);
                var shelveToDelete = shelves.FirstOrDefault(s => s.Id == idTextBox.Text);
                shelves.Remove(shelveToDelete);
                shelveNameTextBox.Clear();
                shelveNameTextBox.Focus();
                idTextBox.Clear();
                PopulateGridView();
                const string deleteSuccessMsg = "Silme işlemi başarılı!";
                MessageBox.Show(deleteSuccessMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagement", "deleteButton_Click", ex));
                throw;
            }
        }
        private void shelveDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0 && shelveDataGridView.Rows != null && shelveDataGridView.Rows[rowIndex] != null
                    && shelveDataGridView.Rows[rowIndex].Cells != null)
                {

                    shelveNameTextBox.Text = shelveDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
                    idTextBox.Text = shelveDataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagement", "shelveDataGridView_CellClick", ex));
                throw ex;
            }
        }
        #endregion

        #region Custom Methods
        private void PopulateGridView()
        {
            shelveBindingSource.DataSource = ShelveXmlHelper.GetListFromXml(); ;
        }
        #endregion


    }

}
