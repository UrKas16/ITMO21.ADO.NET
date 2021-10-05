using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITMO21.ADO.NET.DatasetDesigner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetCustomersButton_Click(object sender, EventArgs e)
        {
            NorthwindDataSet northwindDataSet1 = new NorthwindDataSet();
            NorthwindDataSetTableAdapters.CustomerTableAdapter customerTableAdapter1 = new NorthwindDataSetTableAdapters.CustomerTableAdapter();

            customerTableAdapter1.Fill(northwindDataSet1.Customer);

            foreach (NorthwindDataSet.CustomerRow NWCustomer in northwindDataSet1.Customer.Rows)
            {
                CustomersListBox.Items.Add(NWCustomer.CustomerID);
            }
        }
    }
}
