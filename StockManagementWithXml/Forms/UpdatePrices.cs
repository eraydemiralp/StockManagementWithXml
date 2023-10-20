using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;
using StockManagementWithXml.XmlHelpers;

namespace StockManagementWithXml.Forms
{
    public partial class UpdatePrices : Form
    {
        public UpdatePrices()
        {
            InitializeComponent();
        }

        private void UpdatePrices_Load(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Fiyatlar güncelleniyor. Lütfen bekleyiniz. ");
                        FileStream fileStream = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read);
                        IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                        DataSet ds = reader.AsDataSet();
                        dataGridView1.DataSource = ds.Tables[0];
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            var stockCode = ds.Tables[0].Rows[i][0].ToString();
                            var newName = ds.Tables[0].Rows[i][1].ToString();
                            var newPrice = ds.Tables[0].Rows[i][2].ToString();
                            StockXmlHelper.UpdateStockName(stockCode,newName);
                            StockXmlHelper.UpdatePrice(stockCode,newPrice);
                        }
                        MessageBox.Show("Fiyatlar başarıyla güncellendi.");
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.WriteLog("Exception occured while parsing excel. Detail : " + exception);
                MessageBox.Show("Excel formatınız uygun değil, formatı düzelttikten sonra tekrar deneyiniz!!");
            }
        }
    }
}
