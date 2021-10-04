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
using System.Data.SqlClient;
using System.Configuration;

namespace ITMO21.ADO.NET
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection();
        //string testConnect = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;
        //                     Initial Catalog=AdventureWorks2019;Data Source=DESKTOP-C3O5QE8\SQLEXPRESS";1

        string testConnect = GetConnectionStringByName("DBConnect.NorthwindConnectionString");

        public Form1()
        {
            InitializeComponent();
            this.connection.StateChange += new StateChangeEventHandler(this.connection_StateChange);
        }

        static string GetConnectionStringByName(string name)
        {
            string returnvalue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
            {
                returnvalue = settings.ConnectionString;
            }
            return returnvalue;
        }


        private void connection_StateChange(object sender, StateChangeEventArgs e)
        {
            openConnectionToolStripMenuItem.Enabled = (e.CurrentState == ConnectionState.Closed);
            closeConnectionToolStripMenuItem.Enabled = (e.CurrentState == ConnectionState.Open);
        }

        private void openConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;
                    connection.Open();
                    MessageBox.Show("Соединение с базой установлено");
                }
                else
                {
                    MessageBox.Show("Соединение с базой уже установлено");
                }
            }
            catch (OleDbException sql)
            {
                foreach (OleDbError se in sql.Errors)
                {
                    MessageBox.Show(se.Message, "SQL error code " + se.NativeError, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //MessageBox.Show("Ошибка соединения с базой");
                //throw;
            }
            catch (Exception Xcp)
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void connectionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    MessageBox.Show("name = " + cs.Name);
                    MessageBox.Show("providerName = " + cs.ProviderName);
                    MessageBox.Show("connactionString = " + cs.ConnectionString);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT COUNT(*) FROM Production.Product";
            int num = (int)command.ExecuteScalar();
            label1.Text = num.ToString();
        }
    }
}
