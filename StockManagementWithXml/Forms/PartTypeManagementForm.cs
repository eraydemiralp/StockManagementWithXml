using StockManagementWithXml.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace StockManagementWithXml.Forms
{
    public partial class PartTypeManagementForm : Form
    {
        #region Lifecycle Methods

        List<PartType> _partTypes = XmlHelper.PartTypeXmlHelper.GetListFromXml();
        public PartTypeManagementForm()
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
                List<PartType> partTypeList = XmlHelper.PartTypeXmlHelper.GetListFromXml();
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
                string id = XmlHelper.GenerateId();
                string name = partTypeTextBox.Text.TrimStart(' ').TrimEnd(' ');

                PartType partType = new PartType();
                partType.PartTypeId = id;
                partType.PartTypeName = name;
                XmlHelper.PartTypeXmlHelper.Insert(partType);
                partTypeList.Add(partType);
                partTypeDataGridView.DataSource = partTypeList;
                partTypeTextBox.Clear();
                partTypeTextBox.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagementForm", "addPartTypeButton_Click", ex));
                throw ex;
            }
        }
        private void partTypeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0 && partTypeDataGridView.Rows != null && partTypeDataGridView.Rows[rowIndex] != null
                    && partTypeDataGridView.Rows[rowIndex].Cells != null)
                {
                    partTypeTextBox.Text = partTypeDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
                    idTextBox.Text = partTypeDataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagementForm", "partTypeDataGridView_CellContentClick", ex));
                throw ex;
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var partTypeExistErrorMsg = "Parça türüne bağlı parça bulunmaktadır. Öncelikle Parça Yönetimi ekranından parça türüne ait parçaları siliniz!!";
                var selectPartTypeErrorMsg = "Lütfen tablodan parça türü seçiniz";
                var partTypeDoesntExistErrorMsg = "Parça türü tabloda bulunamadı!!!";
                if (string.IsNullOrEmpty(idTextBox.Text))
                {

                    MessageBox.Show(selectPartTypeErrorMsg);
                    partTypeDataGridView.Focus();
                    return;
                }

                if (!_partTypes.Any(p => p.PartTypeId == idTextBox.Text))
                {
                    MessageBox.Show(partTypeDoesntExistErrorMsg);
                    return;
                }


                var stockList = XmlHelper.StockTypeXmlHelper.GetListFromXml();
                if (stockList.Any(s => s.PartTypeId.Equals(idTextBox.Text)))
                {

                    MessageBox.Show(partTypeExistErrorMsg);
                    return;
                }
                XmlHelper.PartTypeXmlHelper.Delete(idTextBox.Text);
                partTypeTextBox.Clear();
                partTypeTextBox.Focus();
                partTypeTextBox.Clear();
                idTextBox.Clear();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagementForm", "deleteButton_Click", ex));
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

                var stockList = XmlHelper.StockTypeXmlHelper.GetListFromXml();
                var hasStockWithPartType = stockList.Any(s => s.PartTypeId.Equals(idTextBox.Text));
                if (hasStockWithPartType)
                {
                    var existingStockList = stockList.FindAll(s => s.PartTypeId == idTextBox.Text).ToList();
                    if (existingStockList.Count > 0)
                    {
                        foreach (var stock in existingStockList)
                        {
                            XmlHelper.StockTypeXmlHelper.UpdatePartTypeName(stock.Id, enteredPartType.TrimStart(' ').TrimEnd(' '));
                        }

                    }
                }

                XmlHelper.PartTypeXmlHelper.Update(idTextBox.Text, enteredPartType.TrimStart(' ').TrimEnd(' '));
                partTypeTextBox.Clear();
                partTypeTextBox.Focus();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "PartTypeManagementForm", "updateButton_Click", ex));
                throw ex;
            }
        }
        #endregion

        #region Custom Methods
        private void PopulateGridView()
        {
            _partTypes = XmlHelper.PartTypeXmlHelper.GetListFromXml();
            partTypeBindingSource.DataSource = _partTypes;
        }




        #endregion
    }
}
