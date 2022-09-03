using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;


namespace PROJECT___IN_PROGRESS
{
    class tenant
    {
        static ArrayList tenants = new ArrayList();
        static tenantBase tn = new tenantBase();
        public tenant()
        {
            tenants.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT tenantid,name, password, email, dob FROM TENANT";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader(); ;
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    tenantBase t1 = new tenantBase();
                    t1.tenantid = dr.GetValue(0).ToString();
                    t1.name = dr.GetValue(1).ToString();
                    t1.password = dr.GetValue(2).ToString();
                    t1.email = dr.GetValue(3).ToString();
                    t1.dob = Convert.ToDateTime(dr.GetValue(4).ToString());

                    tenants.Add(t1);
                }
            }
            con.Close();
        }



        public static void loadTenants()
        {
            tenants.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT tenantid,name, password, email, dob FROM TENANT";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader(); ;
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    tenantBase t1 = new tenantBase();
                    t1.tenantid = dr.GetValue(0).ToString();
                    t1.name = dr.GetValue(1).ToString();
                    t1.password = dr.GetValue(2).ToString();
                    t1.email = dr.GetValue(3).ToString();
                    t1.dob = Convert.ToDateTime(dr.GetValue(4).ToString());

                    tenants.Add(t1);
                }
            }
            con.Close();

        }

        public bool findTenant(string email, string pass)
        {
            loadTenants();
            foreach (tenantBase t1 in tenants)
            {
                if (t1.email == email && t1.password == pass)
                {
                    tn = t1;
                    return true;
                }
            }
            return false;
        }

        public tenantBase returnTenant()
        {
            return tn;
        }

        internal bool findEmail(string email)
        {
            foreach (tenantBase tenant in tenants)
            {
                if (tenant.email.Equals(email))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
