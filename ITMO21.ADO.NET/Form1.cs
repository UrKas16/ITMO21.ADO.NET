using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ITMO21.ADO.NET
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection();
        string testConnect = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;
                             Initial Catalog=AdventureWorks2019;Data Source=DESKTOP-C3O5QE8\SQLEXPRESS";
        public Form1()
        {
            InitializeComponent();
        }

        private void openConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;
                    connection.Open();
                    MessageBox.Show("Соеждинение с базой установлено");
                }
                else
                {
                    MessageBox.Show("Соединение с базой уже установлено");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с базой");
                throw;
            }
        }

        private void closreConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Соединение с базой закрыто");
            }
            else
            {
                MessageBox.Show("Соединение с базой уже закрыто");
            }
        }
    }
}
