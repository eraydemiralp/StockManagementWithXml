using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StockManagementWithXml.Model;

namespace StockManagementWithXml.Forms
{
    public partial class Activities : Form
    {
        #region Properties
        private string rawDateFormat = "dd.MM.yyyy";
        #endregion

        #region Lifecycle Methods
        public Activities()
        {
            InitializeComponent();
        }

        private void Activities_Load(object sender, EventArgs e)
        {
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("tr-TR");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                startDatePicker.CustomFormat = rawDateFormat;
                endDatePicker.CustomFormat = rawDateFormat;
                DateTime currentDate = DateTime.Now;
                DateTime threeMonthsBefore = currentDate.AddMonths(-3);
                startDatePicker.MinDate = threeMonthsBefore;
                endDatePicker.MaxDate = currentDate;
                startDatePicker.Value = DateTime.Now.AddDays(-7);
                filterActivitiesDataSource();
                fillUserDropDown();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "Activities", "Activities_Load", ex));
                throw;
            }
        }
        #endregion

        #region Events

        private void startDatePicker_ValueChanged(object sender, EventArgs e)
        {
            validateDates();
            filterActivitiesDataSource();
        }

        private void endDatePicker_ValueChanged(object sender, EventArgs e)
        {
            validateDates();
            filterActivitiesDataSource();
        }

        private void userCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateDates();
            filterActivitiesDataSource();
        }
        #endregion

        #region Custom Methods
        private void filterActivitiesDataSource()
        {

            try
            {
                var activities = XmlHelper.ActivitiesXmlHelper.GetListFromXml();
                activities = activities.Where(a =>
                {
                    var splitDate = a.Date.ToString().Split(' ');
                    if (splitDate.Length <= 0) return false;
                    var activityDate = DateTime.ParseExact(splitDate[0], "dd.MM.yyyy", null);
                    var startDate = startDatePicker.Value;
                    var endDate = endDatePicker.Value;
                    if (startDate > activityDate || endDate < activityDate) return false;
                    if (userCombobox.SelectedIndex < 0) return true;
                    var selectedUser = (ComboBoxItem)userCombobox.SelectedItem;
                    return selectedUser.Value == "" || a.User.Equals(selectedUser.Text);
                }).ToList();
               
                activityBindingSource.DataSource = activities;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "Activities", "filterActivitiesDataSource", ex));
                throw;
            }
        }

        private void fillUserDropDown()
        {
            try
            {
                List<User> users = XmlHelper.UserXmlHelper.GetListFromXml();
                ComboBoxItem allItem = new ComboBoxItem();
                allItem.Text = "Tümü";
                allItem.Value = "";
                userCombobox.Items.Add(allItem);
                if (users.Count <= 0) return;
                foreach (var item in users.Select(user => new ComboBoxItem() { Text = user.Name, Value = user.Id }))
                {
                    userCombobox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "Activities", "fillUserDropDown", ex));
                throw;
            }
        }
        private void validateDates()
        {
            if (startDatePicker.Value == endDatePicker.Value)
            {
                MessageBox.Show("Başlangıç ve Bitiş Tarihi aynı olamaz.");
            }
            if (startDatePicker.Value > endDatePicker.Value)
            {
                MessageBox.Show("Başlangıç Tarihi ve Bitiş Tarihinden büyük olamaz.");
            }
        }
        #endregion
    }
}
