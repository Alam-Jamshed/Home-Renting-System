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
    public partial class RentHistory : Form
    {
        tenantBase tn = new tenantBase();
        public RentHistory(tenantBase t1)
        {
            this.tn = t1;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            //int totalRent = (EndDate - StartDate).TotalDays
            double totalRent;

            


            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);



            SqlDataAdapter sqldata = new SqlDataAdapter("SELECT BOOKINGID,STARTDATE,ENDDATE,RENTRATE from BOOKING where TENANTID=" + "'" + tn.tenantid + "'", con);
            DataTable data = new DataTable();
            sqldata.Fill(data);
            using (data)
            {
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.ColumnCount = 4;

                dataGridView1.Columns[0].Name = "BOOKINGID";
                dataGridView1.Columns[0].HeaderText = "Booking ID";
                dataGridView1.Columns[0].DataPropertyName = "BOOKINGID";

                dataGridView1.Columns[1].Name = "BookingStartDate";
                dataGridView1.Columns[1].HeaderText = "Booking Start Date";
                dataGridView1.Columns[1].DataPropertyName = "STARTDATE";

                dataGridView1.Columns[2].Name = "BookingEndDate";
                dataGridView1.Columns[2].HeaderText = "Booking End Date";
                dataGridView1.Columns[2].DataPropertyName = "ENDDATE";

                dataGridView1.Columns[3].Name = "RentRate";
                dataGridView1.Columns[3].HeaderText = "Rent Rate";
                dataGridView1.Columns[3].DataPropertyName = "RentRate";

                dataGridView1.DataSource = data;
            }

            DataGridViewButtonColumn AddReview = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(AddReview);
            AddReview.Text = "Add Review";
            AddReview.Name = "AddReview";
            AddReview.UseColumnTextForButtonValue = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "AddReview")
            {
                String bookingID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                AddReview addReview = new AddReview(bookingID);
                addReview.ShowDialog();
            }
        }
    }
}
