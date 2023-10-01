using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockManagementWithXml.Model;
using System.Linq;

namespace StockManagementWithXml.Forms
{
    public partial class ShelveManagementForm : Form
    {
        #region Lifecycle Events
        public ShelveManagementForm()
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
                List<Shelve> shelves = XmlHelper.ShelveXmlHelper.GetListFromXml();
                bool shelveExist = shelves.Any(r => r.Name.Trim().Equals(enteredShelveName.Trim()));
                if (shelveExist)
                {
                    MessageBox.Show("Aynı isimde raf tabloda mevcut lütfen başka bir raf ismi giriniz!");
                    return;
                }
                Shelve shelve = new Shelve();
                shelve.Id= XmlHelper.GenerateId();
                shelve.Name = enteredShelveName.TrimStart(' ').TrimEnd(' ');
                XmlHelper.ShelveXmlHelper.Insert(shelve);
                shelves.Add(shelve);
                shelveDataGridView.DataSource = shelves;
                shelveNameTextBox.Clear();
                shelveNameTextBox.Focus();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagementForm", "addButton_Click", ex));
                throw ex;
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
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagementForm", "shelveDataGridView_CellClick", ex));
                throw ex;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show("Lütfen tablodan bir raf seçiniz");
                    shelveDataGridView.Focus();
                    return;
                }
                var shelveExistErrorMsg = "Parça türüne bağlı parça bulunmaktadır. Öncelikle Parça Yönetimi ekranından parça türüne ait parçaları siliniz!!";
                var selectShelveErrorMsg = "Lütfen tablodan parça türü seçiniz";
                var shelveDoesntExistErrorMsg = "Parça türü tabloda bulunamadı!!!";
                List<Shelve> shelves = XmlHelper.ShelveXmlHelper.GetListFromXml();

                if (!shelves.Any(s => s.Id == idTextBox.Text))
                {
                    MessageBox.Show(shelveDoesntExistErrorMsg);
                    return;
                }


                var stockList = XmlHelper.StockTypeXmlHelper.GetListFromXml();
                if (stockList.Any(s => s.ShelveId.Equals(idTextBox.Text)))
                {

                    MessageBox.Show(shelveExistErrorMsg);
                    return;
                }
                XmlHelper.ShelveXmlHelper.Delete(idTextBox.Text);
                var shelveToDelete = shelves.FirstOrDefault(s => s.Id == idTextBox.Text);
                shelves.Remove(shelveToDelete);
                shelveNameTextBox.Clear();
                shelveNameTextBox.Focus();
                idTextBox.Clear();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagementForm", "deleteButton_Click", ex));
                throw ex;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectShelveErrorMsg = "Lütfen tablodan raf seçiniz";
                var selectOtherShelveErorMsg = "Raf tabloda mevcut. Lütfen başka bir raf ismi giriniz!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show(selectShelveErrorMsg);
                    shelveDataGridView.Focus();
                    return;
                }

                List<Shelve> shelves = XmlHelper.ShelveXmlHelper.GetListFromXml();
                var enteredShelveName = shelveNameTextBox.Text;
                if (shelves.Any(p => p.Name.Trim() == enteredShelveName.TrimStart(' ').TrimEnd(' ')))
                {
                    MessageBox.Show(selectOtherShelveErorMsg);
                    return;
                }

                var stockList = XmlHelper.StockTypeXmlHelper.GetListFromXml();
                var hasStockWithShelve = stockList.Any(s => s.ShelveId.Equals(idTextBox.Text));
                if (hasStockWithShelve)
                {
                    var existingStockList = stockList.FindAll(s => s.ShelveId == idTextBox.Text).ToList();
                    if (existingStockList.Count > 0)
                    {
                        foreach (var stock in existingStockList)
                        {
                            XmlHelper.StockTypeXmlHelper.UpdateShelveName(stock.Id, enteredShelveName.TrimStart(' ').TrimEnd(' '));
                        }

                    }
                }

                XmlHelper.ShelveXmlHelper.Update(idTextBox.Text, enteredShelveName.TrimStart(' ').TrimEnd(' '));
                foreach (var shelf in shelves)
                {
                    if (shelf.Id == idTextBox.Text)
                    {
                        shelf.Name = enteredShelveName.TrimStart(' ').TrimEnd(' ');
                    }
                }
                shelveNameTextBox.Clear();
                shelveNameTextBox.Focus();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "ShelveManagementForm", "updateButton_Click", ex));
                throw ex;
            }
        }
        #endregion

        #region Custom Methods
        private void PopulateGridView()
        {
            shelveBindingSource.DataSource = XmlHelper.ShelveXmlHelper.GetListFromXml(); ;
        }
        #endregion


    }

}
