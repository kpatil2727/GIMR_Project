using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grocery
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (PassTb.Text == "")
            {
                MessageBox.Show("Enter a Password");
            }
            else if (PassTb.Text == "Pass")
            {
                Employees Emp = new Employees();
                Emp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password");
            }
               
        }
    }
}
