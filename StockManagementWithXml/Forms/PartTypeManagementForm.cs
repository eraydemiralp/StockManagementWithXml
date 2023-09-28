using StockManagementWithXml;
using StockManagementWithXml.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;

namespace StockManagementWithXml.Forms
{
    public partial class PartTypeManagementForm : Form
    {
        #region Lifecycle Methods

        public string partTypeXmlFilePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "XmlFiles", "PartType.xml");

        List<PartType> partTypes = new List<PartType>();
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
                string id = GenerateId();
                string name = partTypeTextBox.Text;

                PartType partType = new PartType();
                partType.PartTypeId = id;
                partType.PartTypeName = name;
                XmlHelper.PartTypeXmlHelper.InsertToXml(partType);
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


        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("Lütfen tablodan parça türü seçiniz");
                partTypeDataGridView.Focus();
                return;
            }
            if (partTypes.Any(p => p.PartTypeName.Trim() == partTypeTextBox.Text))
            {
                MessageBox.Show("Parça Türü tabloda mevcut. Lütfen başka bir parça türü giriniz!!");
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
                        XmlHelper.StockTypeXmlHelper.UpdateStocksPartTypeName(stock.Id, partTypeTextBox.Text);
                    }

                }
            }

            XmlHelper.PartTypeXmlHelper.UpdatePartTypeName(idTextBox.Text,partTypeTextBox.Text);
            var newPartTypeList = XmlHelper.PartTypeXmlHelper.GetListFromXml();
            partTypeDataGridView.DataSource = newPartTypeList;
            partTypeTextBox.Clear();
            partTypeTextBox.Focus();
        }
        #endregion

        #region Custom Methods
        private void PopulateGridView()
        {
            partTypes = XmlHelper.PartTypeXmlHelper.GetListFromXml();
            partTypeBindingSource.DataSource = partTypes;
        }


        public static string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvxyz";
            return new string(Enumerable.Repeat(chars, 30)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}
