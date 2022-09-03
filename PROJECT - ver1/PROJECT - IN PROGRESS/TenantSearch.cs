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
    public partial class TenantSearch : Form
    {
        string city;
        tenantBase tn = new tenantBase();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        public TenantSearch(tenantBase tn)
        {
            this.tn = tn;
            
            //this.city = city;
            InitializeComponent();
            
            /*dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "House Serial";
            dataGridView1.Columns[1].Name = "Rent Rate";
            dataGridView1.AutoGenerateColumns = false;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "";
            btn.Text = "View Details";
            btn.Name = "ViewDetails";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
            dataGridView1.Columns[2].FillWeight = 50;
            */

            /*this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sqldata = new SqlDataAdapter("SELECT slno,rentrate from houses where city=" + "'" + city + "'", con);
            using (DataTable dt = new DataTable())
            {
                sqldata.Fill(dt);

                //Set AutoGenerateColumns False
                dataGridView1.AutoGenerateColumns = false;

                //Set Columns Count
                dataGridView1.ColumnCount = 2;

                //Add Columns
                dataGridView1.Columns[0].Name = "House Serial";
                dataGridView1.Columns[0].HeaderText = "HouseSerial";
                dataGridView1.Columns[0].DataPropertyName = "slno";

                dataGridView1.Columns[1].HeaderText = "Rent Rate";
                dataGridView1.Columns[1].Name = "RentRate";
                dataGridView1.Columns[1].DataPropertyName = "rentrate";

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "";
                btn.Text = "View Details";
                btn.Name = "ViewDetails";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                dataGridView1.Columns[2].FillWeight = 50;

                dataGridView1.DataSource = dt;

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, no houses available in your selected city.", "Search Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }*/
        }

        private void button4_Click42(object sender, EventArgs e)
        {
            
        }


        public void BindGridView()
        {
            city = city.Replace("'", @"''");
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sqldata = new SqlDataAdapter("SELECT HOUSEID,rentrate from houses where city=" + "'" + city + "'", con);
            using (DataTable dt = new DataTable())
            {
                sqldata.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;

               
                dataGridView1.ColumnCount = 2;

                dataGridView1.Columns[0].Name = "House ID";
                dataGridView1.Columns[0].HeaderText = "HouseID";
                dataGridView1.Columns[0].DataPropertyName = "HOUSEID";

                dataGridView1.Columns[1].HeaderText = "Rent Rate";
                dataGridView1.Columns[1].Name = "RentRate";
                dataGridView1.Columns[1].DataPropertyName = "rentrate";

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "";
                btn.Text = "View Details";
                btn.Name = "ViewDetails";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                dataGridView1.Columns[2].FillWeight = 50;

                dataGridView1.DataSource = dt;

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, no houses available in your selected city.", "Search Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool fromTenantSearch = true;
            String serial = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (String.IsNullOrWhiteSpace(serial)!=true)
            {
                viewDetails vd = new viewDetails(serial, fromTenantSearch,tn,null);
                vd.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comboBox1.Text) != true)
            {
                city = comboBox1.Text;
                dataGridView1.Visible = true;
                BindGridView();

            }
            else { errorProvider1.SetError(this.comboBox1, "Please Select a City."); }
        }
    }
}
