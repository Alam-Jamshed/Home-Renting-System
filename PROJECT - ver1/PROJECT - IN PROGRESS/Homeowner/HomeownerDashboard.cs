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

namespace PROJECT___IN_PROGRESS
{

    
    public partial class HomeownerDashboard : Form
    {
        homeownerBase hb = new homeownerBase();
        public HomeownerDashboard(homeownerBase obj)
        {
            hb = obj;
            InitializeComponent();
            this.textBox1.Text = hb.name;
            //this.label3.AutoSize = false;
            //this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            button3.BackColor = Color.SlateGray;
            House h = new House();
            h.loadHouse();
            this.mainFormPanel.Controls.Clear();
            HouseDash hd = new HouseDash(hb) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true};
            this.mainFormPanel.Controls.Add(hd);
            hd.loadHouseList();
            hd.BindGridView();
            hd.Show();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button7.Visible = true;
            button6.Visible = true;
            button3.BackColor = Color.SlateGray;
            this.mainFormPanel.Controls.Clear();
            HouseDash hd = new HouseDash(hb) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.mainFormPanel.Controls.Add(hd);
            hd.loadHouseList();
            hd.BindGridView();
            hd.Show();
        }
        private void button3_Leave(object sender, EventArgs e)
        {
            HouseDash hd = new HouseDash(hb);
            hd.Hide();
            button3.BackColor = Color.FromArgb(13, 15, 54);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(13, 15, 54);
            button4.BackColor = Color.SlateGray;
            this.mainFormPanel.Controls.Clear();
            AccountDetails accDetails = new AccountDetails(hb,null) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            accDetails.FormBorderStyle = FormBorderStyle.None;
            this.mainFormPanel.Controls.Add(accDetails);
            accDetails.Show();
            button7.Visible = false;
            button6.Visible = false;
            //AccountDetails accd = new AccountDetails() { Dock = Dockstyle.Fill, TopLevel = false, };
        }
        private void button4_Leave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(13, 15, 54);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            HouseDash hd = new HouseDash(hb);
            hd.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddHouses addhouse = new AddHouses(hb);
            addhouse.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.mainFormPanel.Controls.Clear();
            HouseDash hd = new HouseDash(hb) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.mainFormPanel.Controls.Add(hd);
            hd.loadHouseList();
            hd.BindGridView();
            hd.Show();
        }
    }
}
