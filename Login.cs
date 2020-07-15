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

namespace OrderSys
{
    public partial class login : Form
    {
        private OleDbConnection con;
        
        public login()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select Role from Login Where username='" + txtUsername.Text + "' and Password='" + txtPassword.Text + "' ",con);
            com.ExecuteNonQuery();
            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

        adap.Fill(tab);

            if (tab.Rows.Count == 1) 
            {
                this.Hide();
                Home home = new Home();
                home.Show();
                
            }

        }
    }
}
