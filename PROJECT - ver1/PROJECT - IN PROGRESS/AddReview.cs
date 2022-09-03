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

namespace PROJECT___IN_PROGRESS
{
    public partial class AddReview : Form
    {
        string bookingID;
        public AddReview(string bID)
        {
            bookingID = bID;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(richTextBox1.Text) != true)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
                string update = "UPDATE BOOKING SET REVIEW=@REVIEW WHERE BOOKINGID=" + "'" + bookingID + "'";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.Parameters.AddWithValue("@REVIEW", richTextBox1.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("Review Added!");
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
