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
    public static class ShelveXmlHelper
    {
        public static string BackupRawPath = ConfigurationManager.AppSettings["backupFilePath"];

        public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "Shelve.xml");

        private static readonly string Id = "Id";
        private static readonly string Name = "Name";
        private static readonly string Shelve = "Shelve";
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
        public static void Insert(Shelve shelve)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var newShelve = new XElement(Shelve);
            var idElement = new XElement(Id, shelve.Id);
            var partTypeName = new XElement(Name, shelve.Name);
            newShelve.Add(idElement, partTypeName);
            if (rootElement == null) return;
            rootElement.Add(newShelve);
            xDoc.Save(XmlFilePath);
        }

        public static List<Shelve> GetListFromXml()
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var shelves = new List<Shelve>();
            if (rootElement == null) return new List<Shelve>();
            foreach (var node in rootElement.Elements())
            {
                var shelve = new Shelve();
                if (node.Element(Id) != null) shelve.Id = node.Element(Id).Value;
                shelve.Name = node.Element(Name).Value;
                shelves.Add(shelve);
            }

            return shelves;
        }

        public static void Update(string id, string shelveName)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Id).Value.Equals(id))
                {
                    node.Element(Name).Value = shelveName;
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
