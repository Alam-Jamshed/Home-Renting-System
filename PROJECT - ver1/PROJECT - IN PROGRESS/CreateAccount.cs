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
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;

namespace PROJECT___IN_PROGRESS
{
    public partial class CreateAccount : Form
    {
        SqlConnection con = new SqlConnection();
        SqlDataReader dr;
        Login login = new Login();
        String accType;

        

        public CreateAccount()
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


        private void button4_MouseDown(object sender, MouseEventArgs e)
        {

        }


        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Name";
                this.textBox1.ForeColor = System.Drawing.Color.Silver;
                errorProvider1.SetError(this.textBox1, "Please Enter Your Name.");
            }
            else
            {
                errorProvider1.Clear();
            }
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

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) == true)
            {
                errorProvider2.SetError(this.textBox2, "Please Enter Your Password.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox3.Text, pattern))
            {
                errorProvider3.Clear();
            }
            else
            {
                errorProvider3.SetError(this.textBox3, "Please Enter Correct Email Address.");
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == true)
            {
                errorProvider5.SetError(this.comboBox1, "Please Choose an User Type.");
            }
            else
            {
                errorProvider5.Clear();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            homeowner h1 = new homeowner();
            tenant t1 = new tenant();
            
            if (string.IsNullOrWhiteSpace(textBox1.Text) != true && string.IsNullOrWhiteSpace(textBox2.Text) != true && string.IsNullOrWhiteSpace(textBox3.Text) != true && String.IsNullOrWhiteSpace(dateTimePicker1.Value.ToString()) != true && (comboBox1.Text == "Homeowner" || comboBox1.Text == "Tenant")) {
                
                if (comboBox1.Text == "Homeowner")
                {
                    if (h1.findEmail(textBox3.Text) !=true)
                    {
                        accType = "homeowner";
                        String homeOwnerID = getID(accType);

                        string insert = "INSERT INTO HOMEOWNER (HOMEOWNERID,NAME,PASSWORD,EMAIL,DOB) VALUES(@HOMEOWNERID,@NAME,@PASSWORD,@EMAIL,@DOB)";
                        SqlCommand cmd = new SqlCommand(insert, con);
                        cmd.Parameters.AddWithValue("@HOMEOWNERID", homeOwnerID);
                        cmd.Parameters.AddWithValue("@NAME", textBox1.Text);
                        cmd.Parameters.AddWithValue("@PASSWORD", textBox2.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", textBox3.Text);
                        cmd.Parameters.AddWithValue("@DOB", SqlDbType.Date).Value = dateTimePicker1.Value.Date;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Registration Successful.");

                        this.Hide();
                        login.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Email has already been used!", "Account creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (t1.findEmail(textBox3.Text) != true)
                    {
                        string insert = "INSERT INTO Tenant (name,password,email,dob,TENANTID) values(@name,@password,@email,@dob,@TENANTID)";
                        SqlCommand cmd = new SqlCommand(insert, con);
                        accType = "tenant";
                        String tenantID = getID(accType);
                        con.Open();
                        cmd.Parameters.AddWithValue("@TENANTID", tenantID);
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", textBox2.Text);
                        cmd.Parameters.AddWithValue("@email", textBox3.Text);
                        cmd.Parameters.AddWithValue("@dob", SqlDbType.Date).Value = dateTimePicker1.Value.Date;

                        cmd.ExecuteNonQuery();

                        con.Close();
                        this.Hide();
                        MessageBox.Show("Registration Successful.");
                        login.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Email has already been used!", "Account creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            else
            {
                MessageBox.Show("Registration not Successful");
            }
        }

        private string getID(string accType)
        {
            string id;
            if (accType.Equals("homeowner"))
            {
                String query = "SELECT TOP 1 HOMEOWNERID FROM HOMEOWNER ORDER BY SERIAL DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        dr.Read();
                        id = incrementID(dr.GetString(0));
                    }
                    else
                    {
                        id = "H00001";
                    }

                    con.Close();

                    return id;
                }
            }
            else
            {
                String query = "SELECT TOP 1 TENANTID FROM TENANT ORDER BY SERIAL DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        dr.Read();
                        id = incrementID(dr.GetString(0));
                    }
                    else
                    {
                        id = "T00001";
                    }

                    con.Close();

                    return id;
                }
            }
        }

        public string incrementID(string id)
        {
            int number = Convert.ToInt32(id.Substring(1, 5));
            number += 1;
            return id.Substring(0, 1) + number.ToString("D5");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }
    }
}
