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

namespace PROJECT___IN_PROGRESS
{
    public partial class TenantDashboard : Form
    {
        tenantBase tn = new tenantBase();
        RentHistory rh;


        public TenantDashboard(tenantBase t1)
        {
            InitializeComponent();
            tn = t1;
            this.textBox1.Text = tn.name;
            button3.BackColor = Color.LightSlateGray;
            this.mainFormPanel.Controls.Clear();
            TenantSearch tSearch = new TenantSearch(tn) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            tSearch.FormBorderStyle = FormBorderStyle.None;
            this.mainFormPanel.Controls.Add(tSearch);
            tSearch.Show();

        }

        public void loadDataGrid()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            //rh.Hide();
            button3.BackColor = Color.SlateGray;
            this.mainFormPanel.Controls.Clear();
            TenantSearch tSearch = new TenantSearch(tn) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            tSearch.FormBorderStyle = FormBorderStyle.None;
            this.mainFormPanel.Controls.Add(tSearch);
            tSearch.Show();


        }

        private void button6_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(13, 15, 54);
            button6.BackColor = Color.SlateGray;
            this.mainFormPanel.Controls.Clear();
            RentHistory rh = new RentHistory(tn) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            rh.FormBorderStyle = FormBorderStyle.None;
            this.mainFormPanel.Controls.Add(rh);
            rh.Show();

        }

        private void button3_Leave(object sender, EventArgs e)
        {
            
            button3.BackColor = Color.FromArgb(13, 15, 54);
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(13, 15, 54);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            button3.BackColor = Color.FromArgb(13, 15, 54);
            button6.BackColor = Color.FromArgb(13, 15, 54);
            button4.BackColor = Color.SlateGray;
            this.mainFormPanel.Controls.Clear();
            AccountDetails accDetails = new AccountDetails(null, tn) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            accDetails.FormBorderStyle = FormBorderStyle.None;
            this.mainFormPanel.Controls.Add(accDetails);
            accDetails.Show();
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
