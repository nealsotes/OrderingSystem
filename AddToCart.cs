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
    public partial class AddToCart : Form
    {
        private OleDbConnection con;
        public AddToCart()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
   
            try
            {
                int prodId = int.Parse(txtOrder.Text);

                double unitPrice = double.Parse(showUnitPrice.Text) * double.Parse(txtQuantity.Text);
                OleDbCommand com = new OleDbCommand("Insert into Ordering values('" + txtOrder.Text + "', '" + showDisc.Text + "', '" + unitPrice + "','" + txtQuantity.Text + "' )", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Succesfull ADDED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Select Customer first!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Debug.WriteLine(ex.Message);
            }
            
           
          
            con.Close();
            this.Close();

        }
    }

       
    
}
