using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ITMO21.ADO.NET.DBCommand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder result = new StringBuilder();

            sqlCommand1.CommandType = CommandType.Text;

            sqlConnection1.Open();

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            bool MoreResults = false;

            do
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result.Append(reader[i].ToString() + "\t");
                    }
                    result.Append(Environment.NewLine);

                }

                MoreResults = reader.NextResult();
            }
            while (MoreResults);

            reader.Close();
            sqlCommand1.Connection.Close();

            textBox1.Text = result.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder results = new System.Text.StringBuilder();
            sqlCommand2.CommandType = CommandType.StoredProcedure;
            sqlCommand2.CommandText = "uspGetBillOfMaterials";
            sqlCommand2.Connection.Open();
            SqlDataReader reader = sqlCommand2.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    results.Append(reader[i].ToString() + "\t");
                }
                results.Append(Environment.NewLine);
            }
            reader.Close();
            sqlCommand2.Connection.Close();
            ResultTextBox.Text = results.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlCommand3.CommandType = CommandType.Text;
            sqlCommand3.CommandText = "CREATE TABLE SalesPersons (" +
                   "[SalesPersonID] [int] IDENTITY(1,1) NOT NULL, " +
                   "[FirstName] [nvarchar](50)  NULL, " +
                   "[LastName] [nvarchar](50)  NULL)";
            sqlCommand3.Connection.Open();
            sqlCommand3.ExecuteNonQuery();
            sqlCommand3.Connection.Close();
            MessageBox.Show("Таблица SalesPersons создана");

        }
    }
}
