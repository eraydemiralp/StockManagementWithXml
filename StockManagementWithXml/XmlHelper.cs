using StockManagementWithXml.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockManagementWithXml
{
    public static class XmlHelper
    {
        public static class PartTypeXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "PartType.xml");
            private static readonly string PartTypeIdAttributeName = "Id";
            private static readonly string PartTypeNodeName = "PartType";
            private static readonly string PartTypeNameNode = "Name";

            public static void InsertToXml(PartType partType)
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                var newPartType = new XElement(PartTypeNodeName);
                var idAttribute = new XAttribute(PartTypeIdAttributeName, partType.PartTypeId);
                var partTypeName = new XElement(PartTypeNameNode, partType.PartTypeName);
                newPartType.Add(idAttribute, partTypeName);
                if (rootElement == null) { return; }
                rootElement.Add(newPartType);
                xDoc.Save(XmlFilePath);
            }

            public static List<PartType> GetListFromXml()
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                var partTypes = new List<PartType>();
                if (rootElement == null) { return new List<PartType>(); }
                foreach (var node in rootElement.Elements())
                {
                    var partType = new PartType();
                    if (node.Attribute(PartTypeIdAttributeName) != null)
                    {
                        partType.PartTypeId = node.Attribute(PartTypeIdAttributeName).Value;

                    }
                    partType.PartTypeName = node.Element(PartTypeNameNode).Value;
                    partTypes.Add(partType);
                }
                return partTypes;
            }

            public static void UpdatePartTypeName(string Id, string newPartTypeName)
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                if(rootElement == null) return;
                foreach (XElement node in rootElement.Elements())
                {
                    if (node.Attribute(PartTypeIdAttributeName).Value.Equals(Id))
                    {
                        node.Element(PartTypeNameNode).Value = newPartTypeName;
                        break;
                    }
                }
                xDoc.Save(XmlFilePath);
            }
        }
        public static class StockTypeXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent.FullName, "XmlFiles", "Stock.xml");

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

            public static List<Stock> GetListFromXml()
            {
                XDocument xDoc = XDocument.Load(XmlFilePath);
                XElement rootElement = xDoc.Root;
                List<Stock> stocks = new List<Stock>();
                if (rootElement == null) return new List<Stock>();
                foreach (XElement node in rootElement.Elements())
                {
                    Stock stock = new Stock();
                    stock.Id = node.Element(Id).Value;
                    stock.Code = node.Element(Code).Value;
                    stock.Name = node.Element(Name).Value;
                    stock.Explanation = node.Element(Explanation).Value;
                    stock.CriticalCount = Convert.ToInt32(node.Element(CriticalCount).Value);
                    stock.ExistingCount = Convert.ToInt32(node.Element(ExistingCount).Value);
                    stock.OrderCount = Convert.ToInt32(node.Element(OrderCount).Value);
                    stock.ShelveId = node.Element(ShelveId).Value;
                    stock.ShelveName= node.Element(ShelveName).Value;
                    stock.PartTypeId = node.Element(PartTypeId).Value;
                    stock.PartTypeName = node.Element(PartTypeName).Value;
                    stocks.Add(stock);
                }
                return stocks;
            }
            public static void UpdateStocksPartTypeName(string stockId, string newPartTypeName)
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
        }
    }

}
