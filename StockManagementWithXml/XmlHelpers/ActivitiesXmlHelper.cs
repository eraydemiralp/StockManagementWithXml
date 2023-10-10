using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockManagementWithXml.Model;

namespace StockManagementWithXml.XmlHelpers
{
    public static class ActivitiesXmlHelper
    {
        public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "Activities.xml");
        public static string BackupRawPath = ConfigurationManager.AppSettings["backupFilePath"];

        private static readonly string Id = "Id";
        private static readonly string Name = "Name";
        private static readonly string Date = "Date";
        private static readonly string User = "User";
        private static readonly string Activity = "Activity";
        private static readonly string GuaranteeStatus = "GuaranteeStatus";
        public static void CreateBackupFile()
        {
            try
            {
                var backupPath = Path.Combine(BackupRawPath, DateTime.Now.ToString("ddMMyyyy"));
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }
                var xDoc = XDocument.Load(XmlFilePath);
                xDoc.Save(backupPath);
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Exception occured while creating Backup. Exception Detail: " + ex);
            }
        }
        public static void Insert(Activity activity)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var newActivity = new XElement(Activity);
            var idElement = new XElement(Id, GenerateId());
            var nameElement = new XElement(Name, activity.Name);
            var dateElement = new XElement(Date, activity.Date);
            var userElement = new XElement(User, activity.User);
            var guaranteeElement = new XElement(GuaranteeStatus, activity.GuaranteeStatus);
            newActivity.Add(idElement, nameElement, dateElement, userElement, guaranteeElement);
            if (rootElement == null) return;
            rootElement.Add(newActivity);
            xDoc.Save(XmlFilePath);
        }

        public static List<Activity> DeleteOldRecords(List<Activity> activities)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return new List<Activity>();
            foreach (var node in rootElement.Elements())
            {
                string rawDate = node.Element(Date).Value;
                string[] splittedDate = rawDate.Split(' ');
                if (splittedDate.Length > 0)
                {
                    DateTime date = DateTime.ParseExact(splittedDate[0], "dd.MM.yyyy", null);
                    if (date <= DateTime.Now.AddMonths(-3))
                    {
                        node.Remove();
                    }
                }
            }
            xDoc.Save(XmlFilePath);

            return activities;
        }

        public static List<Activity> GetListFromXml()
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var activities = new List<Activity>();
            if (rootElement == null) return new List<Activity>();
            foreach (var node in rootElement.Elements())
            {
                var activity = new Activity();
                bool isOldActivity = false;
                string rawDate = node.Element(Date).Value;
                string[] splitDate = rawDate.Split(' ');
                if (splitDate.Length > 0)
                {
                    DateTime date = DateTime.ParseExact(splitDate[0], "dd.MM.yyyy", null);
                    if (date <= DateTime.Now.AddMonths(-3))
                    {
                        isOldActivity = true;
                    }
                }
                if (!isOldActivity)
                {
                    activity.Id = node.Element(Id).Value;
                    activity.Name = node.Element(Name).Value;
                    activity.Date = node.Element(Date).Value;
                    activity.User = node.Element(User).Value;
                    activity.GuaranteeStatus = node.Element(GuaranteeStatus).Value;
                    activities.Add(activity);
                }
            }

            activities = DeleteOldRecords(activities);
            activities = activities.OrderByDescending(a => a.Date).ToList();
            return activities;
        }

        public static string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvxyz";
            return new string(Enumerable.Repeat(chars, 30)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
