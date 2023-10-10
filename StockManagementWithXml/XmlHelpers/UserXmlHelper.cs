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
    public static class UserXmlHelper
    {
        public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "User.xml");
        public static string BackupRawPath = ConfigurationManager.AppSettings["backupFilePath"];

        private static readonly string Id = "Id";
        private static readonly string Name = "Name";
        private static readonly string User = "User";
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
        public static void Insert(User user)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var newUser = new XElement(User);
            var idElement = new XElement(Id, user.Id);
            var userNamElement = new XElement(Name, user.Name);
            newUser.Add(idElement, userNamElement);
            if (rootElement == null) return;
            rootElement.Add(newUser);
            xDoc.Save(XmlFilePath);
        }

        public static List<User> GetListFromXml()
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var users = new List<User>();
            if (rootElement == null) return new List<User>();
            foreach (var node in rootElement.Elements())
            {
                var user = new User();
                if (node.Element(Id) != null) user.Id = node.Element(Id).Value;
                user.Name = node.Element(Name).Value;
                users.Add(user);
            }

            return users;
        }

        public static void Update(string id, string userName)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Id).Value.Equals(id))
                {
                    node.Element(Name).Value = userName;
                    break;
                }
            }
            xDoc.Save(XmlFilePath);
        }

        public static void Delete(string id)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Id).Value.Equals(id))
                {
                    node.Remove();
                    break;
                }
            }

            xDoc.Save(XmlFilePath);
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
