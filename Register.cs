using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace admin_assignment
{
    internal class Register
    {
        private string name;
        private string userID;
        private string password;
        private string email;
        private string phoneNumber;
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public Register(string n,string id, string p, string e, string pN) 
        {
            name = n;
            userID = id;
            password = p;
            email = e;
            phoneNumber = pN;
        }

        public string add_Receptionist() 
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into UserData(UserName, Password, Role) values(@ID, @p, 'receptionist')", con);
            SqlCommand cmd2 = new SqlCommand("insert into Receptionist(name, phone_number, Email, userID, password) values(@name, @pn, @em, @ID, @p)", con);
            SqlCommand cmd3 = new SqlCommand("SELECT count(*) FROM UserData where UserName = @ID", con);
            cmd.Parameters.AddWithValue("@ID", userID);
            cmd.Parameters.AddWithValue("@p", password);
            cmd2.Parameters.AddWithValue("@name", name);
            cmd2.Parameters.AddWithValue("@pn", phoneNumber);
            cmd2.Parameters.AddWithValue("@em", email);
            cmd2.Parameters.AddWithValue("@ID", userID);
            cmd2.Parameters.AddWithValue("@p", password);
            cmd3.Parameters.AddWithValue("@ID", userID);
            int test = Convert.ToInt32(cmd3.ExecuteScalar());
            if (test == 0)
            {
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                status = "Register successtul";
            }
            else 
            {
                status = "Your username is existed, please type another one!";
            }
            con.Close();
            return status;
        }

        public string add_Technician()
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into UserData(UserName, Password, Role) values(@name, @p, 'technician')", con);
            SqlCommand cmd2 = new SqlCommand("insert into Technician(name, phone_number, Email, userID, password) values(@name, @pn, @em, @ID, @p)", con);
            SqlCommand cmd3 = new SqlCommand("SELECT count(*) FROM UserData where UserName = @ID", con);
            cmd.Parameters.AddWithValue("@name", userID);
            cmd.Parameters.AddWithValue("@p", password);
            cmd2.Parameters.AddWithValue("@name", name);
            cmd2.Parameters.AddWithValue("@pn", phoneNumber);
            cmd2.Parameters.AddWithValue("@em", email);
            cmd2.Parameters.AddWithValue("@ID", userID);
            cmd2.Parameters.AddWithValue("@p", password);
            cmd3.Parameters.AddWithValue("@ID", userID);
            int test = Convert.ToInt32(cmd3.ExecuteScalar());
            if (test == 0)
            {
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                status = "Register successtul";
            }
            else
            {
                status = "Your username is existed, please type another one!";
            }
            con.Close();
            return status;
        }
    }
}
