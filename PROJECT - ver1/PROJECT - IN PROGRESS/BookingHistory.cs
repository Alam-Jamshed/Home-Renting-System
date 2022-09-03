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
    public partial class BookingHistory : Form
    {

        public BookingHistory(homeownerBase h1)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            double totalRent;

            string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);



            SqlDataAdapter sqldata = new SqlDataAdapter("SELECT BOOKINGID,STARTDATE,ENDDATE,RENTRATE from BOOKING where HOMEOWNERID=" + "'" + h1.homeownerid + "'", con);
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
                dataGridView1.Columns[3].FillWeight = 50;

                dataGridView1.DataSource = data;
            }


            

            DataGridViewButtonColumn ViewReview = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(ViewReview);
            ViewReview.Text = "View Review";
            ViewReview.Name = "ViewReview";
            ViewReview.UseColumnTextForButtonValue = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ViewReview")
            {
                String bookingID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                ViewReview viewReview = new ViewReview(bookingID);
                viewReview.ShowDialog();
            }
        }
    }
}
