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
    public partial class editDetails : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        public Image getImage(Byte[] pic)
        {
            MemoryStream ms = new MemoryStream(pic);
            return Image.FromStream(ms);
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

        string houseid;
        houseDetails cHouse = new houseDetails();
        tenantBase tn = new tenantBase();

        public editDetails(string hid)
        {
            this.houseid = hid;
            InitializeComponent();

            string query = "SELECT * FROM houses where HOUSEID=" + "'" + houseid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cHouse.houseid = dr.GetValue(0).ToString();
                cHouse.details = dr.GetValue(1).ToString();
                cHouse.address = dr.GetValue(2).ToString();
                cHouse.city = dr.GetValue(3).ToString();
                cHouse.rentRate = Convert.ToInt32(dr.GetValue(4).ToString());
                cHouse.homeownerid = dr.GetValue(5).ToString();
                cHouse.image1 = getImage((byte[])dr.GetValue(6));
                if (!DBNull.Value.Equals(dr.GetValue(7)))
                {
                    cHouse.image2 = getImage((byte[])dr.GetValue(7));
                }
                else
                {
                    cHouse.image2 = null;
                }
                if (!DBNull.Value.Equals(dr.GetValue(8)))
                {
                    cHouse.image3 = getImage((byte[])dr.GetValue(8));
                }
                else
                {
                    cHouse.image3 = null;
                }
            }
            con.Close();

            pictureBox1.Image = cHouse.image1;
            pictureBox2.Image = cHouse.image2;
            pictureBox3.Image = cHouse.image3;

            richTextBox1.Text = cHouse.details;
            richTextBox2.Text = cHouse.address;
            comboBox1.Text = cHouse.city;
            numericUpDown1.Value = cHouse.rentRate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null && pictureBox3.Image != null)
            {
                MessageBox.Show("You can only add three images!", "Limit reached", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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
        }

        public void sortImage()
        {
            if (pictureBox1.Image == null)
            {
                if (pictureBox2.Image !=null)
                {
                    pictureBox1.Image = pictureBox2.Image;
                    if (pictureBox3.Image != null)
                    {
                        pictureBox2.Image = pictureBox3.Image;
                    }
                }
                else
                {
                    pictureBox1.Image = pictureBox3.Image;
                }
            }
            if (pictureBox2.Image == null)
            {
                if (pictureBox3.Image != null)
                {
                    pictureBox2.Image = pictureBox3.Image;
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to save this changes?", "Save Changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(richTextBox1.Text) != true && string.IsNullOrWhiteSpace(richTextBox2.Text) != true && String.IsNullOrWhiteSpace(comboBox1.Text) != true && String.IsNullOrWhiteSpace(Convert.ToString(numericUpDown1.Value)) != true && pictureBox1.Image != null)
                {
                    string update = "UPDATE HOUSES SET housedetails=@housedetails, address=@address, city=@city, rentrate=@rentrate,Picture1=@Picture1,Picture2=@Picture2,Picture3=@Picture3 WHERE HOUSEID=" + "'" + houseid + "'";
                    SqlCommand cmd = new SqlCommand(update, con);
                    cmd.Parameters.AddWithValue("@housedetails", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@address", richTextBox2.Text);
                    cmd.Parameters.AddWithValue("@city", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@rentrate", numericUpDown1.Value);
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
}
