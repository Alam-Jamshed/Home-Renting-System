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
    public partial class viewDetails : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Image getImage(Byte[] pic)
        {
                MemoryStream ms = new MemoryStream(pic);
                return Image.FromStream(ms);
        }

        string houseid;
        houseDetails cHouse = new houseDetails();
        tenantBase tn = new tenantBase();
        homeownerBase hb = new homeownerBase();
        public List<bDate> bookingDates = new List<bDate>();
        
        public viewDetails(string hid, bool fromTenantSearch,tenantBase t1, homeownerBase h1)
        {
            
            houseid = hid;
            InitializeComponent();
            if (fromTenantSearch == false)
            {
                button2.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                button4.Visible = true;
            }
            if (t1 != null)
            {
                this.tn = t1;
            }
            hb = h1;


            SqlConnection con = new SqlConnection(cs);

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

            string dateQuery = "Select STARTDATE,ENDDATE FROM BOOKING where HOUSEID=" + "'" + cHouse.houseid + "'";
            SqlCommand cmdDate = new SqlCommand(dateQuery,con);
            SqlDataReader dateR;
            con.Open();
            dateR = cmdDate.ExecuteReader();
            if (dateR.HasRows == true)
            {
                while (dateR.Read())
                {
                    bDate b1 = new bDate();
                    b1.rentStart = dateR.GetDateTime(0);
                    b1.rentEnd = dateR.GetDateTime(1);
                    bookingDates.Add(b1); 
                    
                }
            }
            cHouse.bookingDate.Clear();
            cHouse.bookingDate.AddRange(bookingDates);
            con.Close();


            pictureBox1.Image = cHouse.image1;
            pictureBox2.Image = cHouse.image2;
            pictureBox3.Image = cHouse.image3;

            label4.Text = cHouse.details;
            label6.Text = cHouse.address;
            label7.Text = cHouse.city;
            label8.Text = Convert.ToString(cHouse.rentRate);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value > dateTimePicker1.Value)
            {
                
                houseDetails hd = new houseDetails();
                bDate selectedDates = new bDate();

                DateTime startDate = dateTimePicker1.Value;
                DateTime endDate = dateTimePicker2.Value;

                if (cHouse.bookingCheck(cHouse.bookingDate, startDate, endDate) == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm this Booking?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string bookingID = getID();
                        SqlConnection con = new SqlConnection(cs);
                        String query = "insert into BOOKING (BOOKINGID,STARTDATE,ENDDATE,TENANTID,HOMEOWNERID,HOUSEID,BOOKINGSTATUS,RENTRATE) values (@BOOKINGID,@STARTDATE,@ENDDATE,@TENANTID,@HOMEOWNERID,@HOUSEID,@BOOKINGSTATUS,@RENTRATE)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@BOOKINGID", bookingID);
                        cmd.Parameters.AddWithValue("@STARTDATE", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                        cmd.Parameters.AddWithValue("@ENDDATE", SqlDbType.Date).Value = dateTimePicker2.Value.Date;
                        cmd.Parameters.AddWithValue("@TENANTID", tn.tenantid);
                        cmd.Parameters.AddWithValue("@HOMEOWNERID", cHouse.homeownerid);
                        cmd.Parameters.AddWithValue("@HOUSEID", houseid);
                        cmd.Parameters.AddWithValue("@BOOKINGSTATUS", 0);
                        cmd.Parameters.AddWithValue("@RENTRATE", cHouse.rentRate);
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Booking confirmed.\nBooking ID: " + bookingID + "\nStarting Date: " + dateTimePicker1.Value.Date.ToString() + "\nEnding Date: " + dateTimePicker2.Value.Date.ToString() + "\nTotal Rent: "  + (dateTimePicker2.Value-dateTimePicker1.Value).TotalDays*cHouse.rentRate, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                       
                    }
                }
                else
                {
                    MessageBox.Show("Sorry! House is already booked on the selected days!", "House already Booked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid booking dates");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BookingHistory bh = new BookingHistory(hb);
            bh.ShowDialog();
        }

        private string getID()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    
            string id;
            String query = "SELECT TOP 1 BOOKINGID FROM BOOKING ORDER BY BOOKINGSERIAL DESC";
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
                    id = "B00001";
                }

                con.Close();

                return id;
            }
        }

        public string incrementID(string id)
        {
            int number = Convert.ToInt32(id.Substring(1, 5));
            number += 1;
            return id.Substring(0, 1) + number.ToString("D5");
        }
    }
}
