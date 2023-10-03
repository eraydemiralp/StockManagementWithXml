using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Windows.Forms;
using StockManagementWithXml.Model;

namespace StockManagementWithXml.Forms
{
    public partial class StockManagementForm : Form
    {
        public StockManagementForm()
        {
            InitializeComponent();

        }

        public string DateFormatLongWith24Hour = "dd.MM.yyyy HH:mm:sss";

        private void RequiredCountNumericBox_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                OrderCountTextBox.Text = RequiredCountNumericBox.Value >= CurrentCountNumericBox.Value ? (RequiredCountNumericBox.Value - CurrentCountNumericBox.Value).ToString(CultureInfo.InvariantCulture) : "0";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "RequiredCountNumericBox_ValueChanged", ex));
                throw;
            }
        }

        private void CurrentCountNumericBox_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                OrderCountTextBox.Text = RequiredCountNumericBox.Value >= CurrentCountNumericBox.Value ? (RequiredCountNumericBox.Value - CurrentCountNumericBox.Value).ToString(CultureInfo.InvariantCulture) : "0";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "CurrentCountNumericBox_ValueChanged", ex));
                throw;
            }
        }

        private void StockManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGridView();
                FillPartTypeDropDown();
                FillShelveDropDown();
                FillUserDropDown();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "StockManagementForm_Load", ex));
                throw;
            }
        }

        private void stockDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex < 0) return;
                idTextBox.Text = stockDataGridView.Rows[rowIndex].Cells[0].Value.ToString();
                StockCodeTextBox.Text = stockDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
                StockNameTextbox.Text = stockDataGridView.Rows[rowIndex].Cells[2].Value.ToString();
                var shelves = XmlHelper.ShelveXmlHelper.GetListFromXml();

                if (shelves.Count > 0)
                {
                    var selectedRowShelveName = stockDataGridView.Rows[rowIndex].Cells[3].Value.ToString();

                    foreach (var shelveItem in shelveNameDropdown.Items)
                    {
                        var itemText = shelveNameDropdown.GetItemText(shelveItem);
                        if (itemText.Equals(selectedRowShelveName))
                        {
                            shelveNameDropdown.SelectedItem = (object)shelveItem;
                        }
                    }
                }
                var partTypes = XmlHelper.PartTypeXmlHelper.GetListFromXml();
                if (partTypes.Count > 0)
                {
                    var selectedRowPartTypeName = stockDataGridView.Rows[rowIndex].Cells[4].Value.ToString();
                    foreach (var partTypeItem in partTypeDropDown.Items)
                    {
                        var itemText = partTypeDropDown.GetItemText(partTypeItem);
                        if (itemText.Equals(selectedRowPartTypeName))
                        {
                            partTypeDropDown.SelectedItem = (object)partTypeItem;
                        }
                    }
                }
                explanationTextBox.Text = stockDataGridView.Rows[rowIndex].Cells[5].Value.ToString();
                RequiredCountNumericBox.Value = Convert.ToInt16(stockDataGridView.Rows[rowIndex].Cells[6].Value);
                CurrentCountNumericBox.Value = Convert.ToInt16(stockDataGridView.Rows[rowIndex].Cells[7].Value);
                OrderCountTextBox.Text = stockDataGridView.Rows[rowIndex].Cells[8].Value.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "stockDataGridView_CellClick", ex));
                throw;
            }
        }

        private void PopulateGridView()
        {
            stockDataGridView.DataSource = XmlHelper.StockXmlHelper.GetListFromXml();
        }

        private void FillPartTypeDropDown()
        {
            try
            {

                var partTypes = XmlHelper.PartTypeXmlHelper.GetListFromXml();
                if (partTypes.Count <= 0) return;
                foreach (var item in partTypes.Select(partType => new ComboBoxItem
                {
                    Text = partType.PartTypeName,
                    Value = partType.PartTypeId
                }))
                {
                    partTypeDropDown.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "fillPartTypeDropDown", ex));
                throw;
            }
        }

        private void FillShelveDropDown()
        {
            try
            {

                List<Shelve> shelves = XmlHelper.ShelveXmlHelper.GetListFromXml();
                if (shelves.Count <= 0) return;
                foreach (var item in shelves.Select(shelve => new ComboBoxItem
                {
                    Text = shelve.Name,
                    Value = shelve.Id
                }))
                {
                    shelveNameDropdown.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "fillShelveDropDown", ex));
                throw;
            }
        }

        private void FillUserDropDown()
        {
            try
            {

                var users = XmlHelper.UserXmlHelper.GetListFromXml();
                if (users.Count <= 0) return;
                foreach (var item in users.Select(user => new ComboBoxItem
                {
                    Text = user.Name,
                    Value = user.Id
                }))
                {
                    userCombobox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "fillUserDropDown", ex));
                throw;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                var stocks = XmlHelper.StockXmlHelper.GetListFromXml();
                if (stocks.Any(s => s.Code == StockCodeTextBox.Text))
                {
                    MessageBox.Show("Aynı koda sahip parça tabloda mevcut, lütfen güncelleme veya silme işlemi ile devam ediniz. ");
                    return;
                }
                if (!ValidateAdd())
                {
                    MessageBox.Show("Lütfen girilen bilgileri kontrol ediniz.");
                    return;
                }

                var stockToAdd = getStockFromScreen();
                stocks.Add(stockToAdd);
                stockDataGridView.DataSource = stocks;
                XmlHelper.StockXmlHelper.Insert(stockToAdd);
                var activityText = "Parça Ekleme ||Parça Kodu: " + StockCodeTextBox.Text + " || Parça Adı: "
                                   + StockNameTextbox.Text;
                var activity = new Activity();
                var selectedUserItem = userCombobox.SelectedItem as ComboBoxItem;

                activity.Date = DateTime.Now.ToString(DateFormatLongWith24Hour);
                activity.Name = activityText;
                activity.User = selectedUserItem != null ? selectedUserItem.Text : "";
                XmlHelper.ActivitiesXmlHelper.Insert(activity);
                ClearAll();
                PopulateGridView();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "addButton_Click", ex));
                throw;
            }
        }

        private bool ValidateAdd()
        {
            return !(string.IsNullOrEmpty(StockCodeTextBox.Text)
                     || string.IsNullOrEmpty(StockNameTextbox.Text)
                     || RequiredCountNumericBox.Value <= 0
                     || shelveNameDropdown.SelectedIndex < 0
                     || partTypeDropDown.SelectedIndex < 0
                     || userCombobox.SelectedIndex < 0);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show("Lütfen tablodan parça türü seçiniz");
                    stockDataGridView.Focus();
                    return;
                }
                if (userCombobox.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen işlem yapan kullanıcı seçiniz.");
                    userCombobox.Focus();
                    return;
                }

                var dialogResult = MessageBox.Show("Parçayı silmek istediğinize emin misiniz?", "Parça Silme"
                        , MessageBoxButtons.YesNo);
                if (dialogResult != DialogResult.Yes) return;
                var stocks = XmlHelper.StockXmlHelper.GetListFromXml();
                if (stocks.All(s => s.Id != idTextBox.Text))
                {
                    MessageBox.Show("Parça tabloda bulunamadı!!!");
                    return;
                }
                XmlHelper.StockXmlHelper.Delete(idTextBox.Text);
                stocks.RemoveAll(s => s.Id == idTextBox.Text);
                stockDataGridView.DataSource = stocks;
                var activityText = "Parça Silme ||Parça Kodu: " + StockCodeTextBox.Text + " || Parça Adı: "
                                   + StockNameTextbox.Text;
                var activity = new Activity();
                var selectedUserItem = userCombobox.SelectedItem as ComboBoxItem;
                activity.Date = DateTime.Now.ToString(DateFormatLongWith24Hour);
                activity.Name = activityText;
                activity.User = selectedUserItem != null ? selectedUserItem.Text : "";
                XmlHelper.ActivitiesXmlHelper.Insert(activity);
                ClearAll();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "deleteButton_Click", ex));
                throw;
            }
        }

        private void ClearAll()
        {
            StockCodeTextBox.Clear();
            StockNameTextbox.Clear();
            explanationTextBox.Clear();
            RequiredCountNumericBox.Value = 0;
            CurrentCountNumericBox.Value = 0;
            OrderCountTextBox.Text = "0";
            partTypeDropDown.SelectedIndex = -1;
            shelveNameDropdown.SelectedIndex = -1;
            idTextBox.Clear();
            StockCodeTextBox.Focus();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz parçayı seçiniz!!!");
                    stockDataGridView.Focus();
                    return;
                }

                var selectedShelve = (ComboBoxItem)shelveNameDropdown.SelectedItem;
                var selectedPartType = (ComboBoxItem)partTypeDropDown.SelectedItem;
                if (selectedShelve == null)
                {
                    MessageBox.Show("Lütfen Raf seçimi yapınız.");
                    return;
                }
                if (selectedPartType == null)
                {
                    MessageBox.Show("Lütfen Parça Türü seçimi yapınız.");
                    return;
                }
                if (userCombobox.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen işlem yapan kullanıcı seçiniz.");
                    userCombobox.Focus();
                    return;
                }
                var stocks = XmlHelper.StockXmlHelper.GetListFromXml();
                if (stocks.All(s => s.Id != idTextBox.Text))
                {
                    MessageBox.Show("!!Parça tabloda bulunamadı!!!");
                }
                var stockToUpdate = getStockFromScreen();
                foreach (var stock in stocks.Where(s => s.Id == idTextBox.Text))
                {
                    stock.Code = stockToUpdate.Code;
                    stock.Name = stockToUpdate.Name;
                    stock.Explanation = stockToUpdate.Explanation;
                    stock.CriticalCount = stockToUpdate.CriticalCount;
                    stock.ExistingCount = stockToUpdate.ExistingCount;
                    stock.OrderCount = stockToUpdate.OrderCount;
                    stock.ShelveId = stockToUpdate.ShelveId;
                    stock.PartTypeId = stockToUpdate.PartTypeId;
                    stock.ShelveName = stockToUpdate.ShelveName;
                    stock.PartTypeName = stockToUpdate.PartTypeName;
                }

                stockDataGridView.DataSource = stocks;
                XmlHelper.StockXmlHelper.Update(idTextBox.Text, stockToUpdate);
                var activityText = "Parça Bilgileri Güncelleme ||Parça Kodu: " + StockCodeTextBox.Text + " || Parça Adı: "
                                   + StockNameTextbox.Text;
                var activity = new Activity();
                var selectedUserItem = userCombobox.SelectedItem as ComboBoxItem;
                activity.Date = DateTime.Now.ToString(DateFormatLongWith24Hour);
                activity.Name = activityText;
                activity.User = selectedUserItem != null ? selectedUserItem.Text : "";
                XmlHelper.ActivitiesXmlHelper.Insert(activity);
                ClearAll();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "updateButton_Click", ex));
                throw;
            }
        }

        private void searchStockCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            var xmlStocks = XmlHelper.StockXmlHelper.GetListFromXml();
            if (string.IsNullOrEmpty(searchStockCodeTextBox.Text))
            {
                stockDataGridView.DataSource = xmlStocks;
            }
            var filteredStocks = xmlStocks.Where(s => s.Code.Contains(searchStockCodeTextBox.Text)).ToList();
            stockDataGridView.DataSource = filteredStocks;
        }

        private void searcStockNameTextBox_TextChanged(object sender, EventArgs e)
        {
            var xmlStocks = XmlHelper.StockXmlHelper.GetListFromXml();
            if (string.IsNullOrEmpty(searcStockNameTextBox.Text))
            {
                stockDataGridView.DataSource = xmlStocks;
            }
            var filteredStocks = xmlStocks.Where(s =>
                s.Name.ToLowerInvariant().Contains(searcStockNameTextBox.Text.ToLowerInvariant())).ToList();
            stockDataGridView.DataSource = filteredStocks;
        }

        private void searchShelveTextBox_TextChanged(object sender, EventArgs e)
        {

            var xmlStocks = XmlHelper.StockXmlHelper.GetListFromXml();
            if (string.IsNullOrEmpty(searchShelveTextBox.Text))
            {
                stockDataGridView.DataSource = xmlStocks;
            }
            var filteredStocks = xmlStocks.Where(s =>
                s.ShelveName.ToLowerInvariant().Contains(searchShelveTextBox.Text.ToLowerInvariant())).ToList();
            stockDataGridView.DataSource = filteredStocks;

        }

        private void addStockButton_Click(object sender, EventArgs e)
        {
            try
            {
                var stocks = XmlHelper.StockXmlHelper.GetListFromXml();
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show("Lütfen tablodan bir parça seçiniz.");
                    return;
                }

                int addStockCount = Convert.ToInt16(stockCountNumeric.Value);
                int existingCount = Convert.ToInt16(CurrentCountNumericBox.Value);
                int criticalCount = Convert.ToInt16(RequiredCountNumericBox.Value);
                int orderCount;
                if (addStockCount == 0)
                {
                    MessageBox.Show("Lütfen 0'dan farklı bir değer giriniz!");
                    return;
                }
                if (userCombobox.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen işlem yapan kullanıcı seçiniz.");
                    userCombobox.Focus();
                    return;
                }

                var newExistingCount = existingCount + addStockCount;
                if (newExistingCount > criticalCount)
                {
                    orderCount = 0;
                }
                else
                {
                    orderCount = criticalCount - newExistingCount;
                }

                var stockToUpdate = getStockFromScreen();
                stockToUpdate.CriticalCount = criticalCount;
                stockToUpdate.ExistingCount = newExistingCount;
                stockToUpdate.OrderCount = orderCount;
                foreach (var stock in stocks.Where(s => s.Id == idTextBox.Text))
                {
                    stock.CriticalCount = stockToUpdate.CriticalCount;
                    stock.ExistingCount = stockToUpdate.ExistingCount;
                    stock.OrderCount = stockToUpdate.OrderCount;
                }
                stockDataGridView.DataSource = stocks;
                XmlHelper.StockXmlHelper.Update(idTextBox.Text, stockToUpdate);
                var activityText = "Stok Ekleme ||Parça Kodu: " + StockCodeTextBox.Text + " || Parça Adı: "
                                   + StockNameTextbox.Text;
                var activity = new Activity();
                var selectedUserItem = userCombobox.SelectedItem as ComboBoxItem;
                activity.Date = DateTime.Now.ToString(DateFormatLongWith24Hour);
                activity.Name = activityText;
                activity.User = selectedUserItem != null ? selectedUserItem.Text : "";
                XmlHelper.ActivitiesXmlHelper.Insert(activity);
                ClearAll();
                PopulateGridView();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "addStockButton_Click", ex));
                throw;
            }
        }

        private void removeStock_Click(object sender, EventArgs e)
        {
            try
            {
                var stocks = XmlHelper.StockXmlHelper.GetListFromXml();
                if (string.IsNullOrEmpty(idTextBox.Text))
                {
                    MessageBox.Show("Lütfen tablodan bir parça seçiniz.");
                    return;
                }

                int removeStockCount = Convert.ToInt16(stockCountNumeric.Value);
                int existingCount = Convert.ToInt16(CurrentCountNumericBox.Value);
                int criticalCount = Convert.ToInt16(RequiredCountNumericBox.Value);
                int orderCount;

                if (existingCount == 0)
                {
                    MessageBox.Show("Mevcut stok adedi sıfırdan farklı olmalıdır");
                    return;
                }

                if (removeStockCount > existingCount)
                {
                    MessageBox.Show("Mevcut stok adedinden fazla stok çıkarılamaz.");
                    return;
                }
                if (userCombobox.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen işlem yapan kullanıcı seçiniz.");
                    userCombobox.Focus();
                    return;
                }
                int newExistingCount = existingCount - removeStockCount;
                if (newExistingCount > criticalCount)
                {
                    orderCount = 0;
                }
                else
                {
                    orderCount = criticalCount - newExistingCount;
                }

                var stockToUpdate = getStockFromScreen();
                stockToUpdate.CriticalCount = criticalCount;
                stockToUpdate.ExistingCount = newExistingCount;
                stockToUpdate.OrderCount = orderCount;
                foreach (var stock in stocks.Where(s => s.Id == idTextBox.Text))
                {
                    stock.CriticalCount = stockToUpdate.CriticalCount;
                    stock.ExistingCount = stockToUpdate.ExistingCount;
                    stock.OrderCount = stockToUpdate.OrderCount;
                }
                stockDataGridView.DataSource = stocks;
                XmlHelper.StockXmlHelper.Update(idTextBox.Text, stockToUpdate);
                var activityText = "Stok Çıkarma ||Parça Kodu: " + StockCodeTextBox.Text + " || Parça Adı: "
                                   + StockNameTextbox.Text;
                var activity = new Activity();
                var selectedUserItem = userCombobox.SelectedItem as ComboBoxItem;
                activity.Date = DateTime.Now.ToString(DateFormatLongWith24Hour);
                activity.Name = activityText;
                activity.User = selectedUserItem != null ? selectedUserItem.Text : "";
                XmlHelper.ActivitiesXmlHelper.Insert(activity);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "removeStock_Click", ex));
                throw;
            }
        }



        private void StockCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar));
        }

        private Stock getStockFromScreen()
        {
            var selectedShelve = (ComboBoxItem)shelveNameDropdown.SelectedItem;
            var selectedPartType = (ComboBoxItem)partTypeDropDown.SelectedItem;
            var shelveId = selectedShelve.Value;
            var partTypeId = selectedPartType.Value;
            var shelveName = selectedShelve.Text;
            var partTypeName = selectedPartType.Text;
            var stock = new Stock
            {
                Code = StockCodeTextBox.Text,
                Name = StockNameTextbox.Text,
                Explanation = explanationTextBox.Text,
                ExistingCount = Convert.ToInt16(CurrentCountNumericBox.Value),
                CriticalCount = Convert.ToInt16(RequiredCountNumericBox.Value),
                OrderCount = Convert.ToInt16(OrderCountTextBox.Text),
                ShelveId = shelveId,
                PartTypeId = partTypeId,
                PartTypeName = partTypeName,
                ShelveName = shelveName
            };
            return stock;
        }

        private void stockDataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //for (int i = 0; i < stockDataGridView.RowCount; i++)
            //{
            //    DataGridViewRow row = stockDataGridView.Rows[i];
            //    var existingCount = Convert.ToInt32(row.Cells[7].Value);
            //    var criticalCount = Convert.ToInt32(row.Cells[6].Value);
            //    if (existingCount == 0)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.OrangeRed;
            //    }
            //    else
            //    {
            //        if (existingCount > criticalCount)
            //        {
            //            row.DefaultCellStyle.BackColor = Color.YellowGreen;
            //        }
            //    }
            //}
        }

        private void stockDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.Value != null)
            {
                e.CellStyle.BackColor = Convert.ToInt16(e.Value) > 0 ? Color.DarkOrange : Color.YellowGreen;
            }

            if (e.ColumnIndex == 7 && e.Value != null)
            {
                if (Convert.ToInt16(e.Value) == 0)
                {
                    e.CellStyle.BackColor = Color.OrangeRed;
                }
            }
        }
    }


}
