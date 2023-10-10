using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StockManagementWithXml.Model;
using StockManagementWithXml.XmlHelpers;

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
                FillGuaranteeDropdown();
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
            if(!validateDates())
                return;
            filterActivitiesDataSource();
        }

        private void endDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!validateDates())
                return;
            filterActivitiesDataSource();
        }

        private void userCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!validateDates())
                return;
            filterActivitiesDataSource();
        }
        #endregion

        #region Custom Methods
        private void filterActivitiesDataSource()
        {

            try
            {
                var exist = true;
                if (!validateDates())
                    return;
                var activities = ActivitiesXmlHelper.GetListFromXml();
                var filteredActivities = activities;
                if (!filteredActivities.Any(a =>
                    {
                        var splitDate = a.Date.Split(' ');
                        var startDate = DateTime.ParseExact(splitDate[0], "dd.MM.yyyy", null);
                        return startDate >= startDatePicker.Value.AddDays(-1);
                    }))
                {
                    exist = false;
                }
                filteredActivities = !exist ? new List<Activity>() : filteredActivities.Where(a =>
                 {
                     var splitDate = a.Date.Split(' ');
                     var startDate = DateTime.ParseExact(splitDate[0], "dd.MM.yyyy", null);
                     return startDate >= startDatePicker.Value.AddDays(-1);
                 }).ToList();
                if (!filteredActivities.Any(a =>
                    {
                        var splitDate = a.Date.Split(' ');
                        var endDate = DateTime.ParseExact(splitDate[0], "dd.MM.yyyy", null);
                        return endDate <= endDatePicker.Value;
                    }))
                {
                    exist = false;
                }

                filteredActivities = !exist ? new List<Activity>() : filteredActivities.Where(a =>
                   {
                       var splitDate = a.Date.Split(' ');
                       var endDate = DateTime.ParseExact(splitDate[0], "dd.MM.yyyy", null);
                       return endDate <= endDatePicker.Value;
                   }).ToList();
                var selectedUser = (ComboBoxItem)userCombobox.SelectedItem;
                var selectedGuarantee = (ComboBoxItem)guaranteeDropdown.SelectedItem;
                if (selectedUser != null && !string.IsNullOrEmpty(selectedUser.Value))
                {
                    exist = filteredActivities.All(a => a.User != selectedUser.Text);
                    filteredActivities = exist
                        ? new List<Activity>() : filteredActivities.Where(a => a.User == selectedUser.Text).ToList();
                }

                if (selectedGuarantee != null && !string.IsNullOrEmpty(selectedGuarantee.Value))
                {
                    if (!filteredActivities
                        .Any(a => a.GuaranteeStatus.Equals(selectedGuarantee.Text)))
                    {
                        exist = false;
                    }

                    filteredActivities = !exist ? new List<Activity>() : filteredActivities
                        .Where(a => a.GuaranteeStatus.Equals(selectedGuarantee.Text)).ToList();

                }
                activityBindingSource.DataSource = filteredActivities;
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
                List<User> users = UserXmlHelper.GetListFromXml();
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

        private void FillGuaranteeDropdown()
        {
            try
            {
                ComboBoxItem allComboBoxItem = new ComboBoxItem() { Text = "Tümü", Value = "" };
                ComboBoxItem yesComboBoxItem = new ComboBoxItem() { Text = "Evet", Value = "1" };
                ComboBoxItem noComboBoxItem = new ComboBoxItem() { Text = "Hayır", Value = "2" };

                guaranteeDropdown.Items.Add(allComboBoxItem);
                guaranteeDropdown.Items.Add(yesComboBoxItem);
                guaranteeDropdown.Items.Add(noComboBoxItem);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format(Logger.DefaultLogMessage, "StockManagementForm", "fillUserDropDown", ex));
                throw;
            }
        }
        private bool validateDates()
        {
            if (startDatePicker.Value == endDatePicker.Value)
            {
                MessageBox.Show("Başlangıç ve Bitiş Tarihi aynı olamaz.");
                return false;
            }
            if (startDatePicker.Value > endDatePicker.Value)
            {
                MessageBox.Show("Başlangıç Tarihi ve Bitiş Tarihinden büyük olamaz.");
                return false;
            }

            return true;
        }
        #endregion

        private void guaranteeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterActivitiesDataSource();
        }
    }
}
