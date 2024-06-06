using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grocery
{
    public partial class biling : Form
    {
        public biling()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\INDIA\OneDrive\Documents\Grocery.Db.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        int n = 0,GrdTotal=0,Amount;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            
            if (ItQtyTb.Text == "" || Convert.ToInt32(ItQtyTb.Text) > stock || ItNameTb.Text=="")
            {
                MessageBox.Show("Enter Quantity");
            }else
            {
                int total = Convert.ToInt32(ItQtyTb.Text) * Convert.ToInt32(ItPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ItNameTb.Text;
                newRow.Cells[2].Value = ItPriceTb.Text;
                newRow.Cells[3].Value = ItQtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
                Amount = GrdTotal;
                TotalLbl.Text = "Rs" + GrdTotal;
                n++;
                UpdateItem();
                Reset();
            }
        }
       
        private void UpdateItem()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Updated ItemTbl set ItQty=" + newQty + " where ItId=" + Key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");
                Con.Close();
                populate();
                //Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Reset()
        {
            ItPriceTb.Text = "";
            ItQtyTb.Text = "";
            ClientNameTb.Text = "";
            ItNameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
         
        private void button2_Click(object sender, EventArgs e)
        {
            if (ClientNameTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl values(" + EmployeeLbl.Text + "','" + ClientNameTb.Text + "','" + Amount + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Billed Saved Successfully");
                    Con.Close();
                    populate();
                  //  Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void biling_Load(object sender, EventArgs e)
        {
            EmployeeLbl.Text = login.EmployeeName;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        int stock = 0, Key = 0;
        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItNameTb.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();

            ItPriceTb.Text = ItemDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ItNameTb.Text == "")
            {
                stock = 0;
                Key = 0;
            }
            else
            {
                stock = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[2].Value.ToString());
                Key = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[0].Value.ToString());
            }


        }

        private void label9_Click(object sender, EventArgs e)
        {
            login Obj = new login();
            Obj.Show();
            this.Hide();

        }
    }
}
