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

namespace PROJECT___IN_PROGRESS
{
    public partial class AddHouses : Form
    {
        homeownerBase h1 = new homeownerBase();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        public AddHouses(homeownerBase obj)
        {
            h1 = obj;
            InitializeComponent();
        }
        private byte[] SavedPhoto(Image picture)
        {
            if (picture != null)
            {
                MemoryStream ms = new MemoryStream();
                picture.Save(ms, picture.RawFormat);
                return ms.GetBuffer();
            }
            return null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Bitmap[] images = new Bitmap[3];
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image == null)
                {
                    images[0] = new Bitmap(ofd.FileName);
                    pictureBox1.Image = images[0];
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (pictureBox2.Image == null)
                {
                    images[1] = new Bitmap(ofd.FileName);
                    pictureBox2.Image = images[1];
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    images[2] = new Bitmap(ofd.FileName);
                    pictureBox3.Image = images[2];
                    pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                errorProvider1.SetError(this.richTextBox1, "Please enter house details.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void richTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                errorProvider2.SetError(this.richTextBox2, "Please enter address.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == true)
            {
                errorProvider3.SetError(this.comboBox1, "Please Choose a city.");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(numericUpDown1.Text) == true)
            {
                errorProvider3.SetError(this.numericUpDown1, "Please enter a rate.");
            }
            else
            {
                errorProvider3.Clear();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) != true && string.IsNullOrWhiteSpace(richTextBox2.Text) != true && String.IsNullOrWhiteSpace(comboBox1.Text) != true && String.IsNullOrWhiteSpace(Convert.ToString(numericUpDown1.Value))!= true && pictureBox1.Image != null){
                String houseid = getID();
                
                string insert = "Insert into houses (houseid,housedetails,address,city,rentrate,homeownerid,Picture1,Picture2,Picture3) values(@houseid,@housedetails,@address,@city,@rentrate,@homeownerid,@Picture1,@Picture2,@Picture3)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@houseid", houseid);
                cmd.Parameters.AddWithValue("@housedetails", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@address", richTextBox2.Text);
                cmd.Parameters.AddWithValue("@city", comboBox1.Text);
                cmd.Parameters.AddWithValue("@rentrate", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@homeownerid", h1.homeownerid);
                cmd.Parameters.AddWithValue("@Picture1", SavedPhoto(pictureBox1.Image));

                if (pictureBox2.Image != null)
                {
                    cmd.Parameters.AddWithValue("@Picture2", SavedPhoto(pictureBox2.Image));
                }
                else
                {
                    cmd.Parameters.Add("@Picture2", SqlDbType.Image).Value = DBNull.Value; 
                }
                if (pictureBox3.Image != null)
                {
                    cmd.Parameters.AddWithValue("@Picture3", SavedPhoto(pictureBox3.Image));
                }
                else
                {
                    cmd.Parameters.Add("@Picture3", SqlDbType.Image).Value = DBNull.Value;
                }
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("House added Successfully.");
                con.Close();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please provide all details");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HouseDash hd = new HouseDash(h1);
            hd.loadHouseList();
            this.Hide();
        }

        private string getID()
        {
            string id;
            String query = "SELECT TOP 1 HOUSEID FROM HOUSES ORDER BY HOUSESERIAL DESC";
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
                    id = "HS00001";
                }

                con.Close();

                return id;
            }
            
        }

        public string incrementID(string id)
        {
            int number = Convert.ToInt32(id.Substring(2, 5));
            number += 1;
            return id.Substring(0, 2) + number.ToString("D5");
        }
    }
}
