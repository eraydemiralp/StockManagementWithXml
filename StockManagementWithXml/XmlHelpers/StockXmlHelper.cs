using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StockManagementWithXml.Model;

namespace StockManagementWithXml.XmlHelpers
{
    public static class StockXmlHelper
    {
        public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "Stock.xml");
        public static string BackupRawPath = ConfigurationManager.AppSettings["backupFilePath"];
        private static readonly string Id = "Id";
        private static readonly string Code = "Code";
        private static string Name = "Name";
        private static string Explanation = "Explanation";
        private static string CriticalCount = "CriticalCount";
        private static string ExistingCount = "ExistingCount";
        private static string OrderCount = "OrderCount";
        private static string ShelveId = "ShelveId";
        private static string ShelveName = "ShelveName";
        private static string PartTypeId = "PartTypeId";
        private static string PartTypeName = "PartTypeName";
        private static string Price = "Price";
        private static string Stock = "Stock";

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
        public static List<Stock> GetListFromXml()
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var stocks = new List<Stock>();
            if (rootElement == null) return new List<Stock>();
            foreach (var node in rootElement.Elements())
            {
                var stock = new Stock();
                stock.Id = node.Element(Id).Value;
                stock.Code = node.Element(Code).Value;
                stock.Name = node.Element(Name).Value;
                stock.Explanation = node.Element(Explanation).Value;
                stock.CriticalCount = Convert.ToInt32(node.Element(CriticalCount).Value);
                stock.ExistingCount = Convert.ToInt32(node.Element(ExistingCount).Value);
                stock.OrderCount = Convert.ToInt32(node.Element(OrderCount).Value);
                stock.ShelveId = node.Element(ShelveId).Value;
                stock.ShelveName = node.Element(ShelveName).Value;
                stock.PartTypeId = node.Element(PartTypeId).Value;
                stock.PartTypeName = node.Element(PartTypeName).Value;
                stock.Price = Convert.ToInt32(node.Element(Price).Value);
                stocks.Add(stock);
            }

            return stocks;
        }

        public static void UpdatePartTypeName(string stockId, string newPartTypeName)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Id).Value.Equals(stockId))
                {
                    node.Element(PartTypeName).Value = newPartTypeName;
                    break;
                }
            }

            xDoc.Save(XmlFilePath);
        }
        public static void UpdateShelveName(string stockId, string newShelveName)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Id).Value.Equals(stockId))
                {
                    node.Element(ShelveName).Value = newShelveName;
                    break;
                }
            }
            xDoc.Save(XmlFilePath);
        }

        public static void UpdatePrice(string stockCode, string newPrice)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Code).Value.Equals(stockCode))
                {
                    if (newPrice.Contains(','))
                    {
                        string[] priceArr = newPrice.Split(',');
                        if (priceArr.Length > 0)
                        {
                            string price = priceArr[0];
                            if (price.Contains('.'))
                            {
                                price = price.Replace(".", "");
                            }

                            node.Element(Price).Value = price;
                        }
                    }
                    else
                    {
                        node.Element(Price).Value = newPrice;
                    }

                    
                    break;
                }
            }
            xDoc.Save(XmlFilePath);
        }

        public static void UpdateStockName(string stockCode, string newName)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Code).Value.Equals(stockCode))
                {
                    node.Element(Name).Value = newName;
                    break;
                }
            }
            xDoc.Save(XmlFilePath);
        }
        public static void Insert(Stock stock)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            var newShelve = new XElement(Stock);
            var idElement = new XElement(Id, GenerateId());
            var codeElement = new XElement(Code, stock.Code);
            var nameElement = new XElement(Name, stock.Name);
            var explanationElement = new XElement(Explanation, stock.Explanation);
            var criticalCountElement = new XElement(CriticalCount, stock.CriticalCount);
            var existingCountElement = new XElement(ExistingCount, stock.ExistingCount);
            var orderCountElement = new XElement(OrderCount, stock.OrderCount);
            var shelveIdElement = new XElement(ShelveId, stock.ShelveId);
            var partTypeIdElement = new XElement(PartTypeId, stock.PartTypeId);
            var shelveNameElement = new XElement(ShelveName, stock.ShelveName);
            var partTypeNameElement = new XElement(PartTypeName, stock.PartTypeName);
            newShelve.Add(idElement, codeElement, nameElement, explanationElement, criticalCountElement
                , existingCountElement, orderCountElement, shelveIdElement, partTypeIdElement
                , shelveNameElement, partTypeNameElement);
            if (rootElement == null) return;
            rootElement.Add(newShelve);
            xDoc.Save(XmlFilePath);
        }

        public static void Update(string id, Stock stock)
        {
            var xDoc = XDocument.Load(XmlFilePath);
            var rootElement = xDoc.Root;
            if (rootElement == null) return;
            foreach (var node in rootElement.Elements())
            {
                if (node.Element(Id).Value.Equals(id))
                {
                    node.Element(Code).Value = stock.Code;
                    node.Element(Name).Value = stock.Name;
                    node.Element(Explanation).Value = stock.Explanation;
                    node.Element(CriticalCount).Value = stock.CriticalCount.ToString();
                    node.Element(ExistingCount).Value = stock.ExistingCount.ToString();
                    node.Element(OrderCount).Value = stock.OrderCount.ToString();
                    node.Element(ShelveId).Value = stock.ShelveId;
                    node.Element(PartTypeId).Value = stock.PartTypeId;
                    node.Element(ShelveName).Value = stock.ShelveName;
                    node.Element(PartTypeName).Value = stock.PartTypeName;
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
