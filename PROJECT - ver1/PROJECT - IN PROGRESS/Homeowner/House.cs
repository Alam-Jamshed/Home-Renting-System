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
    class House
    {
        public  Image getImage(Byte[] pic)
        {
            MemoryStream ms = new MemoryStream(pic);
            return Image.FromStream(ms);
        }

        public static List<houseDetails> houseList = new List<houseDetails>();
        public static houseDetails houseDetail = new houseDetails();
        public List<bDate> bookingDates = new List<bDate>();
        bDate bookingDate;
        public House()
        {
            
            houseList.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * FROM houses";
            
            SqlDataReader dateR;

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader(); 
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    houseDetail.houseid = dr.GetValue(0).ToString();
                    houseDetail.details = dr.GetValue(1).ToString();
                    houseDetail.address = dr.GetValue(2).ToString();
                    houseDetail.city = dr.GetValue(3).ToString();
                    houseDetail.rentRate = Convert.ToInt32(dr.GetValue(4).ToString());
                    houseDetail.homeownerid = dr.GetValue(5).ToString();
                    houseDetail.image1 = getImage((byte[])dr.GetValue(6));
                    if (!DBNull.Value.Equals(dr.GetValue(7)))
                    {
                        houseDetail.image2 = getImage((byte[])dr.GetValue(7));
                    }
                    else
                    {
                        houseDetail.image2 = null;
                    }
                    if (!DBNull.Value.Equals(dr.GetValue(8)))
                    {
                        houseDetail.image3 = getImage((byte[])dr.GetValue(8));
                    }
                    else
                    {
                        houseDetail.image3 = null;
                    }

                    houseDetail.bookingDate = bookingDates;
                    houseList.Add(houseDetail);
                }
            }
            con.Close();
        }



        public void loadHouse()
        {
            houseList.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * FROM houses";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader(); ;
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    houseDetail.houseid = dr.GetValue(0).ToString();
                    houseDetail.details = dr.GetValue(1).ToString();
                    houseDetail.address = dr.GetValue(2).ToString();
                    houseDetail.city = dr.GetValue(3).ToString();
                    houseDetail.rentRate = Convert.ToInt32(dr.GetValue(4).ToString());
                    houseDetail.homeownerid = dr.GetValue(5).ToString();
                    houseDetail.image1 = getImage((byte[])dr.GetValue(6));
                    if (!DBNull.Value.Equals(dr.GetValue(7)))
                    {
                        houseDetail.image2 = getImage((byte[])dr.GetValue(7));
                    }
                    else
                    {
                        houseDetail.image2 = null;
                    }
                    if (!DBNull.Value.Equals(dr.GetValue(8)))
                    {
                        houseDetail.image3 = getImage((byte[])dr.GetValue(8));
                    }
                    else
                    {
                        houseDetail.image3 = null;
                    }

                    houseList.Add(houseDetail);
                }
            }
            con.Close();
        }

        public static houseDetails returnHouseObj()
        {
            return null;
        }


        /*public bool findOwner(string email, string pass)
        {
            foreach (homeownerBase homeowner in homeOwners)
            {
                if (homeowner.email == email && homeowner.password == pass)
                {
                    hb = homeowner;
                    return true;
                }
            }

            return false;
        }

        public homeownerBase returnOwner()
        {
            return hb;
        }*/
    }
}
