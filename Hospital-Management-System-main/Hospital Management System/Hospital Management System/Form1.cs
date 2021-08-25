using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hospital_Management_System
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-S738BS9M\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Please enter the DocId and DocPassword");
            }
            else {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from DoctorTbl where DocName='"+ textBox1.Text + "'and DocPass='" + textBox2.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Form2 f = new Form2();
                    f.Show();
                    this.Hide();
                }
                else {
                    MessageBox.Show("Wrong DocId password");
                }
                con.Close();
            }
           
        }
    }
}
