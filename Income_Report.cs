using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace admin_assignment
{
    public partial class Income_Report : Form
    {
        public Income_Report()
        {
            InitializeComponent();
        }

        private static ArrayList viewIncome(int m, int y)
        {
            //define the month and year
            int month = m;
            int year = y;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            //define an array list
            ArrayList income = new ArrayList();
            con.Open();
            //select the specific data from database and set the month and year to let database to find the specific month and year
            SqlCommand cmd = new SqlCommand("select Price, PickupDate from report where month(PickupDate) = '" + month + "' and year(PickupDate) = '" + year + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //add the data in the array list
                income.Add("RM" + reader.GetString(0) + "\t" + reader.GetDateTime(1) + "\n");
            }
            con.Close();
            return income;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            //close the form
            this.Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //declare as integer for let the database can search the data
            int Syear = cmbYear.SelectedIndex;
            int Smonth = lstBoxMonth.SelectedIndex;
            int trans = 0;
            //remove the previous report
            Report_List.Items.Clear();
            //set the validation if the user doesn't select the year and month
            if (Syear == -1 || Smonth == -1)
            {
                MessageBox.Show("Please select the year and month!");
            }
            else
            {
                //set the value to let database can search the data
                if (Syear == 0)
                {
                    trans = 2022;
                }
                else if (Syear == 1)
                {
                    trans = 2023;
                }
                else if (Syear == 2)
                {
                    trans = 2024;
                }
                else if (Syear == 3)
                {
                    trans = 2025;
                }
                else if (Syear == 4)
                {
                    trans = 2026;
                }
                else if (Syear == 5)
                {
                    trans = 2027;
                }
                else if (Syear == 6)
                {
                    trans = 2028;
                }
                else if (Syear == 7)
                {
                    trans = 2029;
                }
                else if (Syear == 8)
                {
                    trans = 2030;
                }
                ArrayList check = new ArrayList();
                check = viewIncome(Smonth, trans);
                //set and add the value in the list box
                foreach (var x in check)
                {
                    Report_List.Items.Add(x);
                }
            }
        }
    }
}
