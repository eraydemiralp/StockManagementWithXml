using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockManagementWithXml.Model;

namespace StockManagementWithXml.XmlHelpers
{
    public static class BackupXmlHelper
    {
        public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "Backup.xml");

        private static readonly string Date = "Date";
        private static readonly string Backup = "Backup";

        public static void Insert(Backup backup)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var newBackup = new XElement(Backup);
            var dateElement = new XElement(Date, backup.Date);
            newBackup.Add(dateElement);
            if (rootElement == null) return;
            rootElement.Add(newBackup);
            xDoc.Save(XmlFilePath);
        }


        public static List<Backup> GetListFromXml()
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var backups = new List<Backup>();
            if (rootElement == null) return new List<Backup>();
            foreach (var node in rootElement.Elements())
            {
                var backup = new Backup
                {
                    Date = node.Element(Date).Value
                };
                backups.Add(backup);
            }
            return backups;
        }
    }
}
