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
using System.Diagnostics;

namespace OrderSys
{
   
    public partial class AddNewProduct : Form
    {
        private OleDbConnection con;
        ManageProduct mn;
       
        public AddNewProduct()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");
            mn = new ManageProduct();
          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            con.Open();
            try
            {
                int prodId = int.Parse(txtProdID.Text);
                double unitPrice = double.Parse(txtUPrice.Text);
                OleDbCommand com = new OleDbCommand("Insert into Product values('" + prodId + "', '" + txtDiscription.Text + "', '" + unitPrice + "' )", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Succesfull ADDED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) 
            {

                Debug.WriteLine(ex.Message);
            }
                
            
            mn.LoadDataGrid();
            con.Close();
       
            txtDiscription.Text = " ";
            txtProdID.Text = " ";
            txtUPrice.Text = " ";


        }

     


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
