using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementWithXml.Model
{
    public class Activity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string User { get; set; }
        public string GuaranteeStatus { get; set; }
    }
}
