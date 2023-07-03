using Assignment;
using GroupAssignment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technician_IOOP;

namespace admin_assignment
{
    internal class ClassLogIn
    {
        //define
        private string username;
        private string password;

        public ClassLogIn (string u, string p) 
        {
            username = u;
            password = p;
        }

        public string logIn(string n) 
        {
            string status = null;
            //set database
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            //call database
            con.Open();
            //search and count the user in database
            SqlCommand cmd = new SqlCommand("select count (*) from UserData where UserName='" + username + "' and Password = '" + password + "'", con);
            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            //if the user is existing then check his role and log in to the specific interface
            if (count > 0)
            {
                //search for the user's role
                SqlCommand cmd2 = new SqlCommand("select Role from UserData where UserName ='" + username + "'and Password = '" + password + "'", con);
                string Role = cmd2.ExecuteScalar().ToString();
                status = Role;
                //get into the specific role's interface
                if (Role == "admin")
                {
                    Admin_Form a = new Admin_Form(n);
                    a.ShowDialog();
                }
                else if (Role == "technician")
                {
                    ChoiceSelection1 CS = new ChoiceSelection1(n);
                    CS.ShowDialog();
                }
                else if (Role == "receptionist")
                {
                    ReceptionistMenu obj = new ReceptionistMenu(n);
                    obj.ShowDialog();
                }
                else if (Role == "Customer")
                {
                    CustomerHome c = new CustomerHome(n);
                    c.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("Invalid user role");
                }
            }
            else 
            {
                //show messeage box to user
                status = "Invalid userID or password";
                MessageBox.Show(status);
            }
            //close database
            con.Close();

            return status;
        }
    }
}
