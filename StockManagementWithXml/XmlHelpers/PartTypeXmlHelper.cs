using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StockManagementWithXml.Model;

namespace StockManagementWithXml.XmlHelpers
{
    public static class PartTypeXmlHelper
    {
        public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "PartType.xml");
        public static string BackupRawPath = ConfigurationManager.AppSettings["backupFilePath"];
        public static string XmlFilePathProd = Path.Combine(Directory.GetCurrentDirectory(), "XmlFiles", "PartType.xml");

        private static readonly string PartTypeIdAttributeName = "Id";
        private static readonly string PartTypeNodeName = "PartType";
        private static readonly string PartTypeNameNode = "Name";

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

        public static void Insert(PartType partType)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var newPartType = new XElement(PartTypeNodeName);
            var idAttribute = new XAttribute(PartTypeIdAttributeName, partType.PartTypeId);
            var partTypeName = new XElement(PartTypeNameNode, partType.PartTypeName);
            newPartType.Add(idAttribute, partTypeName);
            if (rootElement == null) return;
            rootElement.Add(newPartType);
            xDoc.Save(XmlFilePath);
        }

        public static List<PartType> GetListFromXml()
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var partTypes = new List<PartType>();
            if (rootElement == null) return new List<PartType>();
            foreach (var node in rootElement.Elements())
            {
                var partType = new PartType();
                if (node.Attribute(PartTypeIdAttributeName) != null)
                    partType.PartTypeId = node.Attribute(PartTypeIdAttributeName).Value;
                partType.PartTypeName = node.Element(PartTypeNameNode).Value;
                partTypes.Add(partType);
            }

            return partTypes;
        }

        public static void Update(string id, string newPartTypeName)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Attribute(PartTypeIdAttributeName).Value.Equals(id))
                {
                    node.Element(PartTypeNameNode).Value = newPartTypeName;
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
                if (node.Attribute(PartTypeIdAttributeName).Value.Equals(id))
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
