using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace StdInfoSystem
{
    public partial class Login : Form
    {
        SqlConnection connection;
        SqlCommand command;

        // SQL Server database ki connection string
        string ConnectionString = @"Data Source=DESKTOP-5UUCN0M\SQLEXPRESS;Initial Catalog=student;Integrated Security=True";

        Student std = null;

        public Login()
        {
            InitializeComponent();

            // Student class ka object create kiya
            std = new Student();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Sample student information
            std.Name = "HUNZLA ZULFIQAR";
            std.Email = "hunzlakhawaja@gmail.com";
            std.Password = "1234";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Login user ko database mein verify karta hai
            ValidateUser();
        }

        private void ValidateUser()
        {
            // Database connection create ki
            connection = new SqlConnection(ConnectionString);

            try
            {
                // Database connection open ki
                connection.Open();

                // SQL command create ki
                command = new SqlCommand();

                // Command ko database connection ke saath connect kiya
                command.Connection = connection;

                // person table mein email aur password check karne ki query
                command.CommandText =
                    "SELECT COUNT(*) FROM person " +
                    "WHERE email = @Email AND password = @Password";

                // Email parameter add kiya
                command.Parameters.AddWithValue(
                    "@Email",
                    txtEmail.Text.Trim());

                // Password parameter add kiya
                command.Parameters.AddWithValue(
                    "@Password",
                    txtPassword.Text.Trim());

                // Query execute karke matching records ki count li
                int count =
                    Convert.ToInt32(command.ExecuteScalar());

                // Agar matching user mil gaya
                if (count > 0)
                {
                    MessageBox.Show("Login Successful");

                    // Main form open ki
                    (new MainFrm()).Show();

                    // Login form hide ki
                    this.Hide();
                }
                else
                {
                    // Agar email/password incorrect ho
                    MessageBox.Show(
                        "Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                // Database error show kiya
                MessageBox.Show(
                    "Connection Failed: " + ex.Message);
            }
            finally
            {
                // Database connection close ki
                if (connection != null &&
                    connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Exit confirmation
            if (MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Application close
                Application.Exit();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}