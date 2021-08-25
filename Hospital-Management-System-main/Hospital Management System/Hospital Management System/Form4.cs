using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hospital_Management_System
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-S738BS9M\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        public Form4()
        {
            InitializeComponent();
        }
        void populate()
        {
            con.Open();
            string query = "select * from PatitentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||textBox5.Text=="")
            {
                MessageBox.Show("No Empty filed Accepted");
            }
            else
            {
                con.Open();
                string query = "insert into PatitentTbl values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','"+textBox5.Text+"','"+comboBox1.SelectedItem.ToString()+"','"+comboBox2.SelectedItem.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Patitent Data Inserted Successfully");
                con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "update PatitentTbl set PatitentName='" + textBox2.Text + "',PatitentAds='" + textBox3.Text + "',PatitentPhone='" + textBox4.Text + "',PatitentAge='"+textBox5.Text+"',PatitentGen='"+comboBox1.SelectedItem.ToString()+"',PatitentBlood='"+comboBox2.SelectedItem.ToString()+"' where PatitentId=" + textBox1.Text + "";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Patitent Data updated Successfully");
            con.Close();
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter The Patitent Id");
            }
            else
            {
                con.Open();
                string query = "delete from PatitentTbl where PatitentId=" + textBox1.Text + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Patitent Data Deleted Successfully");
                con.Close();
                populate();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
    }
}
