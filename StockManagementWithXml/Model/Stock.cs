using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementWithXml.Model
{
    public class Stock
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Explanation { get; set; }
        public int CriticalCount { get; set; }
        public int ExistingCount { get; set; }
        public int OrderCount { get; set; }
        public string ShelveId { get; set; }
        public string ShelveName { get; set; }
        public string PartTypeId { get; set; }
        public string PartTypeName { get; set; }
    }
}
