using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace admin_assignment
{
    public partial class Admin_Form : Form
    {
        public static string name;
        public Admin_Form(string n)
        {
            InitializeComponent();
            name = n;
        }

        //for show the username
        private void Admin_Form_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Hello, " + name;
        }

        //to get into register receptionist interface
        private void btnRecep_Click(object sender, EventArgs e)
        {
            Register_Receptionist frm = new Register_Receptionist();
            frm.ShowDialog();
        }

        //to get into register technician interface
        private void btnTech_Click(object sender, EventArgs e)
        {
            Register_Technician frm = new Register_Technician();
            frm.ShowDialog();
        }

        //to get into service report interface
        private void btnService_Click(object sender, EventArgs e)
        {
            Service_Report frm = new Service_Report();
            frm.ShowDialog();
        }

        //to get into income report interface
        private void btnIncome_Click(object sender, EventArgs e)
        {
            Income_Report frm = new Income_Report();
            frm.ShowDialog();
        }

        //exit the application
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
