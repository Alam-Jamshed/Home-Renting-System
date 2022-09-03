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
    public partial class HouseDash : Form
    {
        homeownerBase h1 = new homeownerBase();
        ArrayList houseArray = new ArrayList();
        public static houseDetails houseDetail = new houseDetails();
        public HouseDash(homeownerBase obj)
        {
            h1 = obj;
            InitializeComponent();
            this.loadHouseList();
            this.BindGridView();
        }

        public void loadHouseList()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);



            SqlDataAdapter sqldata = new SqlDataAdapter("SELECT HOUSEID from houses where HOMEOWNERID="+ "'"+ h1.homeownerid+"'",con);
            DataTable data = new DataTable();
            sqldata.Fill(data);
            using (data)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns[0].DataPropertyName = "HOUSEID";
                dataGridView1.DataSource = data;
            }

        }

        public void BindGridView()
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ViewHouseDetail")
            {
                bool fromTenantSearch = false;
                String houseid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                viewDetails vd = new viewDetails(houseid, fromTenantSearch,null, h1);
                vd.ShowDialog();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "RemoveProperty")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this property?", "Remove Property!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    String houseid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
                    SqlConnection con = new SqlConnection(cs);
                    string query = "delete from houses where HOUSEID=" + "'" + houseid + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    con.Close();
                    this.loadHouseList();
                }

                else if (dialogResult == DialogResult.No)
                {
                    this.loadHouseList();
                }
            }
            else if(dataGridView1.Columns[e.ColumnIndex].Name == "EditHouseDetail")
            {
                String houseid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                editDetails ed = new editDetails(houseid);
                ed.ShowDialog();
            }
        }
    }
}
