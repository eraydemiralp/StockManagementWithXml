using StockManagementWithXml.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using StockManagementWithXml.XmlHelpers;

namespace StockManagementWithXml.Forms
{
    public partial class PartTypeManagement : Form
    {
        #region Lifecycle Methods

        List<PartType> _partTypes = PartTypeXmlHelper.GetListFromXml();
        public PartTypeManagement()
        {
            InitializeComponent();
        }
        private void PartTypeManagement_Load(object sender, EventArgs e)
        {
            PopulateGridView();
        }
        #endregion

        #region Events
        private void addPartTypeButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<PartType> partTypeList = PartTypeXmlHelper.GetListFromXml();
                if (string.IsNullOrEmpty(partTypeTextBox.Text))
                {
                    MessageBox.Show("Parça Türü alanı boş olamaz!!");
                    partTypeTextBox.Focus();
                    return;
                }
                bool partTypeExists = partTypeList.Any(p => p.PartTypeName.Trim().Equals(partTypeTextBox.Text.Trim()));
                if (partTypeExists)
                {
                    MessageBox.Show("Parça Türü tabloda mevcut. Lütfen başka bir parça türü giriniz!!");
                    return;
                }
                string id = PartTypeXmlHelper.GenerateId();
                string name = partTypeTextBox.Text.TrimStart(' ').TrimEnd(' ');

                PartType partType = new PartType();
                partType.PartTypeId = id;
                partType.PartTypeName = name;
                PartTypeXmlHelper.Insert(partType);
                partTypeList.Add(partType);
                partTypeDataGridView.DataSource = partTypeList;
                partTypeTextBox.Clear();
                partTypeTextBox.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagement", "addPartTypeButton_Click", ex));
                throw ex;
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectPartTypeErrorMsg = "Lütfen tablodan parça türü seçiniz";
                var selectOtherPartTypeErorMsg = "Parça Türü tabloda mevcut. Lütfen başka bir parça türü giriniz!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show(selectPartTypeErrorMsg);
                    partTypeDataGridView.Focus();
                    return;
                }

                var enteredPartType = partTypeTextBox.Text;
                if (_partTypes.Any(p => p.PartTypeName.Trim() == enteredPartType.TrimStart(' ').TrimEnd(' ')))
                {
                    MessageBox.Show(selectOtherPartTypeErorMsg);
                    return;
                }

                var stockList = StockXmlHelper.GetListFromXml();
                var hasStockWithPartType = stockList.Any(s => s.PartTypeId.Equals(idTextBox.Text));
                if (hasStockWithPartType)
                {
                    var existingStockList = stockList.FindAll(s => s.PartTypeId == idTextBox.Text).ToList();
                    if (existingStockList.Count > 0)
                    {
                        foreach (var stock in existingStockList)
                        {
                            StockXmlHelper.UpdatePartTypeName(stock.Id, enteredPartType.TrimStart(' ').TrimEnd(' '));
                        }

                    }
                }

                PartTypeXmlHelper.Update(idTextBox.Text, enteredPartType.TrimStart(' ').TrimEnd(' '));
                partTypeTextBox.Clear();
                partTypeTextBox.Focus();
                PopulateGridView();
                PopulateGridView();
                const string updateSuccessMsg = "Güncelleme işlemi başarılı!";
                MessageBox.Show(updateSuccessMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagement", "updateButton_Click", ex));
                throw;
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                const string partTypeExistErrorMsg = "Parça türüne bağlı parça bulunmaktadır. Öncelikle Parça Yönetimi ekranından parça türüne ait parçaları siliniz!!";
                const string selectPartTypeErrorMsg = "Lütfen tablodan parça türü seçiniz";
                const string partTypeNotExistErrorMsg = "Parça türü tabloda bulunamadı!!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {

                    MessageBox.Show(selectPartTypeErrorMsg);
                    partTypeDataGridView.Focus();
                    return;
                }

                if (_partTypes.All(p => p.PartTypeId != idTextBox.Text))
                {
                    MessageBox.Show(partTypeNotExistErrorMsg);
                    return;
                }


                var stockList = StockXmlHelper.GetListFromXml();
                if (stockList.Any(s => s.PartTypeId.Equals(idTextBox.Text)))
                {

                    MessageBox.Show(partTypeExistErrorMsg);
                    return;
                }
                PartTypeXmlHelper.Delete(idTextBox.Text);
                partTypeTextBox.Clear();
                partTypeTextBox.Focus();
                partTypeTextBox.Clear();
                idTextBox.Clear();
                PopulateGridView();
                const string deleteSuccessMsg = "Silme işlemi başarılı!";
                MessageBox.Show(deleteSuccessMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagement", "deleteButton_Click", ex));
                throw;
            }
        }
        private void partTypeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    partTypeTextBox.Text = partTypeDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
                    idTextBox.Text = partTypeDataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagement", "partTypeDataGridView_CellContentClick", ex));
                throw;
            }
        }
  

        #endregion

        #region Custom Methods
        private void PopulateGridView()
        {
            _partTypes = PartTypeXmlHelper.GetListFromXml();
            partTypeBindingSource.DataSource = _partTypes;
        }




        #endregion
    }
}
