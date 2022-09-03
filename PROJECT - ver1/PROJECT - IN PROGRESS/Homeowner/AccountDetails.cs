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
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PROJECT___IN_PROGRESS
{

    public partial class AccountDetails : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        homeownerBase hb = new homeownerBase();
        tenantBase tn = new tenantBase();
        public AccountDetails(homeownerBase h1, tenantBase t1)
        {
            hb = h1;
            tn = t1;
            InitializeComponent();
            string usertype;

            if (hb != null)
            {
                textBox1.Text = hb.name;
                textBox2.Text = hb.email;
                dateTimePicker1.Value = hb.dob;
                textBox3.Text = "Homeowner";
            }
            else
            {
                textBox1.Text = tn.name;
                textBox2.Text = tn.email;
                dateTimePicker1.Value = tn.dob;
                textBox3.Text = "Tenant";
            }

        }

        private void AccountDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Enabled = true;
            this.textBox2.Enabled = true;
            this.dateTimePicker1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (hb != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to save this changes?", "Save Changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    if (string.IsNullOrWhiteSpace(textBox1.Text) != true && string.IsNullOrWhiteSpace(textBox3.Text) != true && String.IsNullOrWhiteSpace(dateTimePicker1.Value.ToString()) != true)
                    {
                        string update = "UPDATE HOMEOWNER SET NAME=@NAME, EMAIL=@EMAIL, DOB=@DOB WHERE HOMEOWNERID=" + "'" + hb.homeownerid + "'";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.Parameters.AddWithValue("@NAME", textBox1.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", textBox3.Text);
                        cmd.Parameters.AddWithValue("@DOB", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Details Updated Successfully.");
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please provide all details");
                    }
                }

                else if (dialogResult == DialogResult.No)
                {
                    this.Hide();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to save this changes?", "Save Changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    if (string.IsNullOrWhiteSpace(textBox1.Text) != true && string.IsNullOrWhiteSpace(textBox2.Text) != true && String.IsNullOrWhiteSpace(dateTimePicker1.Value.ToString()) != true)
                    {
                        string update = "UPDATE TENANT SET NAME=@NAME, EMAIL=@EMAIL, DOB=@DOB WHERE TENANTID=" + "'" + tn.tenantid + "'";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.Parameters.AddWithValue("@NAME", textBox1.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", textBox2.Text);
                        cmd.Parameters.AddWithValue("@DOB", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Details Updated Successfully.");
                        con.Close();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please provide all details");
                    }
                }

                else if (dialogResult == DialogResult.No)
                {
                    this.Hide();
                }
            }
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

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox2.Text, pattern))
            {
                errorProvider2.Clear();
            }
            else
            {
                errorProvider2.SetError(this.textBox2, "Please Enter Correct Email Address.");
            }
        }
    }
}
