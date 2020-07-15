using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSys
{
    public partial class Home : Form
    {

        private ManageProduct manage;
        ManageCustomer mc;
        public Home()
        {
            InitializeComponent();
            manage = new ManageProduct();
            mc = new ManageCustomer();
        }

        private void mangeProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           
            manage.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            manage.Hide();
            
        }

        private void manageCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            mc.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            mc.Hide();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            mc.Close();
            manage.Close();
            login log = new login();
            log.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageOrder mo = new ManageOrder();
            mo.Show();
        }
    }
}
