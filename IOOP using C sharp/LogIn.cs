namespace admin_assignment
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        //log in
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string stat;
            ClassLogIn obj1 = new ClassLogIn(txtUN.Text.ToString(), txtPassword.Text.ToString());
            stat = obj1.logIn(txtUN.Text);
            txtUN.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }

        //to exit the application
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
