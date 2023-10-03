using StockManagementWithXml.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StockManagementWithXml
{
    public static class XmlHelper
    {
        public static class PartTypeXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "XmlFiles", "PartType.xml");

            public static string XmlFilePathProd = Path.Combine(Directory.GetCurrentDirectory(),"XmlFiles", "PartType.xml");

            private static readonly string PartTypeIdAttributeName = "Id";
            private static readonly string PartTypeNodeName = "PartType";
            private static readonly string PartTypeNameNode = "Name";

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
        }

        public static class StockXmlHelper
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
            private static string Stock = "Stock";

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
            public static void Insert(Stock stock)
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                var newShelve = new XElement(Stock);
                var idElement = new XElement(Id, GenerateId());
                var codeElement = new XElement(Code, stock.Code);
                var nameElement= new XElement(Name, stock.Name);
                var explanationElement= new XElement(Explanation, stock.Explanation);
                var criticalCountElement= new XElement(CriticalCount, stock.CriticalCount);
                var existingCountElement= new XElement(ExistingCount, stock.ExistingCount);
                var orderCountElement= new XElement(OrderCount, stock.OrderCount);
                var shelveIdElement = new XElement(ShelveId, stock.ShelveId);
                var partTypeIdElement = new XElement(PartTypeId, stock.PartTypeId);
                var shelveNameElement = new XElement(ShelveName, stock.ShelveName);
                var partTypeNameElement = new XElement(PartTypeName, stock.PartTypeName);
                newShelve.Add(idElement, codeElement,nameElement,explanationElement,criticalCountElement
                    ,existingCountElement,orderCountElement,shelveIdElement,partTypeIdElement
                    ,shelveNameElement,partTypeNameElement);
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
        }

        public static class ShelveXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "XmlFiles", "Shelve.xml");

            private static readonly string Id = "Id";
            private static readonly string Name = "Name";
            private static readonly string Shelve = "Shelve";

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
        }

        public static class UserXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "XmlFiles", "User.xml");

            private static readonly string Id = "Id";
            private static readonly string Name = "Name";
            private static readonly string User = "User";

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
        }

        public static class ActivitiesXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "XmlFiles", "Activities.xml");

            private static readonly string Id = "Id";
            private static readonly string Name = "Name";
            private static readonly string Date = "Date";
            private static readonly string User = "User";
            private static readonly string Activity = "Activity";

            public static void Insert(Activity activity)
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                var newActivity = new XElement(Activity);
                var idElement = new XElement(Id, GenerateId());
                var nameElement = new XElement(Name, activity.Name);
                var dateElement = new XElement(Date, activity.Date);
                var userElement = new XElement(User, activity.User);
                newActivity.Add(idElement, nameElement,dateElement,userElement);
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
                        activities.Add(activity); 
                    }
                }

                activities = DeleteOldRecords(activities);
                return activities;
            }
        }
        public static class BackupXmlHelper
        {
            public static string XmlFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "XmlFiles", "Backup.xml");

            private static readonly string LastBackupDate = "LastBackupDate";

            public static DateTime GetLastBackupDate()
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                if (!rootElement.Elements().Any())
                {
                    var backUpDateElement =
                        new XElement(LastBackupDate, DateTime.Now.AddDays(-10).ToString("dd.MM.yyyy"));
                    rootElement.Add(backUpDateElement);
                    xDoc.Save(XmlFilePath);
                    return DateTime.Now.AddDays(-10);
                }

                var backupDateElement = rootElement.Elements().FirstOrDefault();
                if (backupDateElement != null && !string.IsNullOrEmpty(backupDateElement.Value))
                {
                    return DateTime.ParseExact(backupDateElement.Value, "dd.MM.yyyy", null);
                }
                return DateTime.Now;
            }


            public static void CreateBackup()
            {
                var xDoc = XDocument.Load(XmlFilePath);
                var rootElement = xDoc.Root;
                if (rootElement == null) return;
                foreach (var node in rootElement.Elements())
                {
                    node.Element(LastBackupDate).Value = DateTime.Now.ToString("dd.MM.yyyy");
                }
                xDoc.Save(XmlFilePath);
            }


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