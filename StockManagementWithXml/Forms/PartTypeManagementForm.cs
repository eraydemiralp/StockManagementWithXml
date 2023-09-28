using StockManagementWithXml;
using StockManagementWithXml.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StockManagementWithXml.Forms
{
    public partial class PartTypeManagementForm : Form
    {
        #region Lifecycle Methods

        public string partTypeXmlFilePath = @"..\XmlFiles\PartType.xml";

        List<PartType> partTypes = new List<PartType>();
        public PartTypeManagementForm()
        {
            InitializeComponent();
        }
        private void PartTypeManagement_Load(object sender, EventArgs e)
        {
        } 
        #endregion

        #region Events
        private void addPartTypeButton_Click(object sender, EventArgs e)
        {
           
        }
        private void partTypeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
           

        }
        private void updateButton_Click(object sender, EventArgs e)
        {
           
        }
        #endregion

        #region Custom Methods
        private void PopulateGridView()
        {
            XDocument xDoc = XDocument.Load(partTypeXmlFilePath);
            XElement rootElement = xDoc.Root;
            List<PartType> partTypes = new List<PartType>();

            foreach (XElement node in rootElement.Elements())
            {
                PartType partType = new PartType();
                partType.PartTypeId = node.Attribute("Id").Value;
                partType.PartTypeName = ""
            }
        } 
        #endregion
    }
}
