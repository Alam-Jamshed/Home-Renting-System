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
    public class homeowner
    {

        public static ArrayList homeOwners = new ArrayList();
        public static homeownerBase hb = new homeownerBase();
        public homeowner()
        {
            homeOwners.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT homeownerid,name, password, email, dob FROM HOMEOWNER";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader(); ;
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    homeownerBase h1 = new homeownerBase();
                    h1.homeownerid = dr.GetValue(0).ToString();
                    h1.name = dr.GetValue(1).ToString();
                    h1.password = dr.GetValue(2).ToString();
                    h1.email = dr.GetValue(3).ToString();
                    h1.dob = Convert.ToDateTime(dr.GetValue(4).ToString());
                    
                    homeOwners.Add(h1);
                }
            }
            con.Close();
        }



        public static void loadOwners(homeownerBase hb)
        {
            homeOwners.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT name, password, email, dob FROM HOMEOWNER";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader(); ;
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    homeownerBase h1 = new homeownerBase();
                    h1.homeownerid = dr.GetValue(0).ToString();
                    h1.name = dr.GetValue(1).ToString();
                    h1.password = dr.GetValue(2).ToString();
                    h1.email = hb.password = dr.GetValue(3).ToString();
                    h1.dob = Convert.ToDateTime(dr.GetValue(4).ToString());
                    homeOwners.Add(h1);

                }
            }
            con.Close();

        }

        public bool findOwner(string email, string pass)
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
        }

        internal bool findEmail(string email)
        {
            foreach (homeownerBase homeowner in homeOwners)
            {
                if (homeowner.email.Equals(email))
                {
                    return true;
                }
            }
            return false;
        }
    }
} 
