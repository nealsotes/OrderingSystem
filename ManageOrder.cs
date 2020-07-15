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

/* Ordering section
 * you can add order
 * view customer 
 * list of product
 * total price
 * 
 * 
 */
namespace OrderSys
{
    public partial class ManageOrder : Form
    {
        private OleDbConnection con;
        private AddNewProduct anp;//Instance so that i can use the existing class 

        public ManageOrder()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");
            anp = new AddNewProduct();

        }

        private void button2_Click(object sender, EventArgs e)//akong gi hide para choy
        {
            this.Hide();
        }

        private void MangeOrder_Load_1(object sender, EventArgs e)
        {
            // invoke to display the form when loading
            LoadDataGridCust();
            LoadDataGridProd();
            LoadDataGridOrder();

        }
        public void LoadDataGridCust()//sa customer section nga datagridview
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Customer order by CustID asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridViewCust.DataSource = tab;
            con.Close();

        }
        public void LoadDataGridProd()//sa product nga datagridview
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Product order by ProdID asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridViewProd.DataSource = tab;
            con.Close();

        }


        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            anp.Show();//It shows the add new product section 
        }

        private void dataGridViewCust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Random random = new Random();// I am using random to order id because its unique
            txtCustID.Text = dataGridViewCust.Rows[e.RowIndex].Cells["CustID"].Value.ToString();
            int ran = random.Next();
            txtOrderID.Text = ran.ToString();
            txtName.Text = dataGridViewCust.Rows[e.RowIndex].Cells["Name"].Value.ToString();
        }
        public void LoadDataGridOrder()// load the order table in the database
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Ordering order by Order_Id asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridViewCart.DataSource = tab;


            int sum = 0;
            for (int i = 0; i < dataGridViewCart.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridViewCart.Rows[i].Cells[2].Value);
                totalPrice.Text = sum.ToString();
            }
            con.Close();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)// search section 
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Product where Discription like '%" + txtSearch.Text + "%'", con);
            com.ExecuteNonQuery();
            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();
            adap.Fill(tab);
            dataGridViewProd.DataSource = tab;
            con.Close();
        }

        private void dataGridViewProd_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddToCart adc = new AddToCart();//Intance of add new cart section 
            adc.Show();//i am using show to display the add to cart when double click the product in datagridvew of product// 
            adc.showProd.Text = dataGridViewProd.Rows[e.RowIndex].Cells["ProdID"].Value.ToString();
            adc.showDisc.Text = dataGridViewProd.Rows[e.RowIndex].Cells["Discription"].Value.ToString();
            adc.showUnitPrice.Text = dataGridViewProd.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();
            adc.txtOrder.Text = txtOrderID.Text;
        }


        //Reload database/datagridview order section
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            con.Open();
           

            OleDbCommand com = new OleDbCommand("Select * FROM Ordering order by Order_Id asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridViewCart.DataSource = tab;
            con.Close();
        }

      

        

        private void dataGridViewCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOr.Text = dataGridViewCart.Rows[e.RowIndex].Cells["Order_Id"].Value.ToString();
            txtD.Text = dataGridViewCart.Rows[e.RowIndex].Cells["Prod_name"].Value.ToString();
            txtP.Text = dataGridViewCart.Rows[e.RowIndex].Cells["Price"].Value.ToString();
            txtQ.Text = dataGridViewCart.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
         

           
        }

        private void btnRemove_Click(object sender, EventArgs e)//remove data in datagridview/database
        {
        

            con.Open();
            try
            {
                string num = txtOr.Text;
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    OleDbCommand com = new OleDbCommand("Delete from Ordering where Order_Id = " + num + "", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Succesfully DELETED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cancelled!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }


            con.Close();
            LoadDataGridOrder();
        }

        private void btmAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("For administrator only!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);//display when attempting to click without administrator privilege
        }
    }
}

       
    
