using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace PROJECT___IN_PROGRESS
{
    public partial class Login : Form
    {
        
        SqlConnection con = new SqlConnection();
        public Login()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        }

        
        

        private void button1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            if (status)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Please Enter Your Email.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "Please Enter Your Password.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateAccount createAcc = new CreateAccount();
            createAcc.ShowDialog();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == true)
            {
                errorProvider3.SetError(this.comboBox1, "Please Choose an Usertype.");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) != true && string.IsNullOrWhiteSpace(textBox2.Text) != true && string.IsNullOrWhiteSpace(comboBox1.Text) != true)
            {
                if (comboBox1.Text == "Homeowner")
                {
                    homeowner h1 = new homeowner();
                    if (h1.findOwner(textBox1.Text, textBox2.Text))
                    {
                        
                        //MessageBox.Show("Welcome " + textBox1.Text, "Login Successful!", MessageBoxButtons.OK);
                        this.Hide();
                        homeownerBase hb = new homeownerBase();
                        hb = h1.returnOwner();
                        HomeownerDashboard d1 = new HomeownerDashboard(hb);
                        d1.ShowDialog();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Check Email and password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // }
                    /*string query = "SELECT * FROM homeowner WHERE email = @email AND password = @pass";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                    con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows == true)
                        {
                            MessageBox.Show("Welcome " + textBox1.Text, "Login Successful!", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Check Username and password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    con.Close();*/
                }
                else
                {
                    tenant t1 = new tenant();
                    if (t1.findTenant(textBox1.Text, textBox2.Text))
                    {
                        this.Hide();
                        tenantBase tn = new tenantBase();
                        tn = t1.returnTenant();
                        TenantDashboard d1 = new TenantDashboard(tn);
                        d1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Check Email and password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    /*string query = "SELECT * FROM Tenant WHERE email = @email AND password = @pass";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                    con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows == true)
                        {
                            MessageBox.Show("Welcome " + textBox1.Text, "Login Successful!", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Check Username and password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    con.Close();
                    */
                }
            }
        }
    }
}
