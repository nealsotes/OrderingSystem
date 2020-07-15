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
/// <summary>
/// Manage customer section
/// ganahan unta ko butangan og validation kaso na hutdan kos oras
/// 
/// </summary>
namespace OrderSys
{
    public partial class ManageCustomer : Form
    {
        private OleDbConnection con;
        public ManageCustomer()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Neal\\Desktop\\AppsDev\\OrderingSystem\\OrderingSystem.mdb");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCustID.Text = dataGridView1.Rows[e.RowIndex].Cells["CustID"].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells["Contact"].Value.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells["Age"].Value.ToString();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
        public void LoadDataGrid()
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Customer order by CustID asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;
            con.Close();

        }

        private void btmAdd_Click(object sender, EventArgs e)//Add button
        {
            con.Open();
            

            try
            {
                int custId = int.Parse(txtCustID.Text);
                OleDbCommand com = new OleDbCommand("Insert into Customer values('" + custId + "', '" + txtName.Text + "', '" + txtContact.Text + "','" + txtAddress.Text + "','" + txtAge.Text + "' )", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Succesfull ADDED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            
            }

           

            con.Close();
            LoadDataGrid();
            // after clicking add e erased ang txtbox para choy kunohay
            txtName.Text = " ";
            txtAge.Text = " ";
            txtAddress.Text = " ";
            txtCustID.Text = " ";
            txtContact.Text = " ";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string num = txtCustID.Text;

            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    OleDbCommand com = new OleDbCommand("Delete from Customer where CustID = " + num + "", con);
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
            txtName.Text = " ";
            txtAge.Text = " ";
            txtAddress.Text = " ";
            txtCustID.Text = " ";
            txtContact.Text = " ";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();

            string id = txtCustID.Text;

            try
            {
                OleDbCommand com = new OleDbCommand("Update Customer SET Name='" + txtName.Text + "',Contact= '" + txtContact.Text + "',Address='" + txtAddress.Text + "',Age='" + txtAge.Text + "'  where CustID=" + id + "", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Succesfully UPDATED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
            con.Close();
            LoadDataGrid();
            txtName.Text = " ";
            txtAge.Text = " ";
            txtAddress.Text = " ";
            txtCustID.Text = " ";
            txtContact.Text = " ";
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = " ";
            txtAge.Text = " ";
            txtAddress.Text = " ";
            txtCustID.Text = " ";
            txtContact.Text = " ";
        }

        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from Customer where Name like '%" + txtSearch1.Text + "%'", con);
            com.ExecuteNonQuery();
            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();
            adap.Fill(tab);
            dataGridView1.DataSource = tab;
            con.Close();
        }
    }
}
