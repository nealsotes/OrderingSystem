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
    public partial class ManageProduct : Form
    {
        private OleDbConnection con;
        private UpdateProduct up = new UpdateProduct();
     
        public ManageProduct()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");
            
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewProduct addPr = new AddNewProduct();
                addPr.Show();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
           
        }

        private void ManageProduct_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        public void LoadDataGrid()
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Product order by ProdID asc",con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;
            con.Close();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Product order by ProdID asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;
            con.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            
            try
            {
                up.txtUpProdID.Text = dataGridView1.Rows[e.RowIndex].Cells["ProdID"].Value.ToString();
                up.txtUpDisc.Text = dataGridView1.Rows[e.RowIndex].Cells["Discription"].Value.ToString();
                up.txtUpUnitPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();
              

            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
            
           
        }
        



        private void btnRemove_Click(object sender, EventArgs e)
        {
           

            up.Hide();
           
            con.Open();
            try
            {
                string num = up.txtUpProdID.Text;
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    OleDbCommand com = new OleDbCommand("Delete from Product where ProdID = " + num + "", con);
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
            LoadDataGrid();
            
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
               
                up.Show();
                

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Product where Discription like '%" + txtSearch.Text + "%'", con);
            com.ExecuteNonQuery();
            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();
            adap.Fill(tab);
            dataGridView1.DataSource = tab;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
