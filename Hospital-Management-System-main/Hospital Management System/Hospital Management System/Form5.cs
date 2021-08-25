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
    public partial class Form5 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-S738BS9M\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        public Form5()
        {
            InitializeComponent();
        }
        void populatecmb()
        {
           
            string query = "select * from PatitentTbl";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("PatitentId", typeof(int));
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                comboBox1.ValueMember = "PatitentId";
                comboBox1.DataSource = dt;
                con.Close();
            }
            catch { 
            }
        }
        void populate()
        {
            con.Open();
            string query = "select * from DiagnosisTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        string PatitentNm;
        void fetchPatitentData()
        {
            con.Open();
            string query = "select * from PatitentTbl where PatitentId=" + comboBox1.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                PatitentNm = dr["PatitentName"].ToString();
                textBox2.Text = PatitentNm;
            }
            con.Close();
        }
        private void Form5_Load(object sender, EventArgs e)
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
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("No Empty filed Accepted");
            }
            else
            {
                con.Open();
                string query = "insert into DiagnosisTbl values("+ textBox1.Text + ",'"+comboBox1.SelectedValue.ToString()+"','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Data Inserted Successfully");
                con.Close();
                populate();
            }
        }

        private void Form5_Load_1(object sender, EventArgs e)
        {
            populatecmb();
            populate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchPatitentData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "update DiagnosisTbl set Sysmptoms='" + textBox4.Text + "',Diagnosis='" + textBox5.Text + "' where DiagnosisId=" + textBox1.Text + "";
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
                MessageBox.Show("Enter The Diagnosis Id");
            }
            else
            {
                con.Open();
                string query = "delete from DiagnosisTbl where DiagnosisId=" + textBox1.Text + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Data Deleted Successfully");
                con.Close();
                populate();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            label11.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            label10.Text= dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            label9.Text= dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(label12.Text +"\n\n\n\n\n\n\n\n\n\n\n" , new Font("Stencil", 25, FontStyle.Bold), Brushes.Red, new Point(230));

            e.Graphics.DrawString(label12.Text +"\n\n"+ "Patitent Name:  " + label11.Text + "\n\n"+ "Diagnosis: \t" + label9.Text +"\n\n"+ "Symptoms: \t" + label10.Text + "\n\n", new Font("Stencil", 14, FontStyle.Regular), Brushes.Black, new Point(130,130));
            e.Graphics.DrawString(label13.Text + "\n\n\n\n\n\n\n\n", new Font("Stencil", 25, FontStyle.Bold), Brushes.Red, new Point(130,500));

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
