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
    public partial class UpdateProduct : Form
    {
        private OleDbConnection con;
        public UpdateProduct()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");
          
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();

           

            try
            {
                string id = txtUpProdID.Text;
                OleDbCommand com = new OleDbCommand("Update Product SET Discription='" + txtUpDisc.Text + "',UnitPrice= '" + txtUpUnitPrice.Text + "' where ProdID=" + id + "", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Succesfully UPDATED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();
            ManageProduct m = new ManageProduct();
            m.LoadDataGrid();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
