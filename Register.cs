using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StdInfoSystem
{
    public partial class Register : Form
    {
        // SQL Server database ki connection string
        string ConnectionString = @"Data Source=DESKTOP-5UUCN0M\SQLEXPRESS;Initial Catalog=student;Integrated Security=True";

        public Register()
        {
            InitializeComponent();
        }

        private void txtColor_MouseClick(
            object sender,
            MouseEventArgs e)
        {
            // Colour dialog box open kiya
            cdColor.ShowDialog();
        }

        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            // Student class ka object create kiya
            Student student = new Student();

            // Form se Name liya
            student.Name = txtName.Text.Trim();

            // Form se Father Name liya
            student.FatherName =
                txtFatherName.Text.Trim();

            // Form se Email liya
            student.Email =
                txtEmail.Text.Trim();

            // Form se Password liya
            student.Password =
                txtPassword.Text.Trim();

            // Date of Birth li
            student.DOB = dtDOB.Value;

            // Gender check kiya
            if (rdMale.Checked)
            {
                student.Gender = "Male";
            }
            else if (rdFemale.Checked)
            {
                student.Gender = "Female";
            }
            else
            {
                student.Gender =
                    "Don't want to Disclose";
            }

            // Favourite colour liya
            student.FavColour =
                cdColor.Color.Name;

            // Courses ke liye empty string
            string courses = "";

            // Selected courses read kiye
            foreach (var item in lstCourse.SelectedItems)
            {
                courses += item.ToString() + ", ";
            }

            // Last comma aur space remove kiya
            student.Courses =
                courses.TrimEnd(',', ' ');

            // Education li
            student.Education =
                cmbEducation.Text;

            try
            {
                // Database connection create ki
                using (SqlConnection con =
                    new SqlConnection(ConnectionString))
                {
                    // Database connection open ki
                    con.Open();

                    // person table mein data insert karne ki query
                    string Query = @"
                        INSERT INTO person
                        (
                            name,
                            fatherName,
                            email,
                            password,
                            DOB,
                            Gender,
                            FavColour,
                            Courses,
                            Education,
                            createdBy,
                            updatedBy
                        )
                        VALUES
                        (
                            @Name,
                            @FatherName,
                            @Email,
                            @Password,
                            @DOB,
                            @Gender,
                            @FavColour,
                            @Courses,
                            @Education,
                            1,
                            1
                        )";

                    // SQL command create ki
                    using (SqlCommand cmd =
                        new SqlCommand(Query, con))
                    {
                        // Name parameter
                        cmd.Parameters.AddWithValue(
                            "@Name",
                            student.Name);

                        // Father Name parameter
                        cmd.Parameters.AddWithValue(
                            "@FatherName",
                            student.FatherName);

                        // Email parameter
                        cmd.Parameters.AddWithValue(
                            "@Email",
                            student.Email);

                        // Password parameter
                        cmd.Parameters.AddWithValue(
                            "@Password",
                            student.Password);

                        // DOB parameter
                        cmd.Parameters.AddWithValue(
                            "@DOB",
                            student.DOB);

                        // Gender parameter
                        cmd.Parameters.AddWithValue(
                            "@Gender",
                            student.Gender);

                        // Favourite colour parameter
                        cmd.Parameters.AddWithValue(
                            "@FavColour",
                            student.FavColour);

                        // Courses parameter
                        cmd.Parameters.AddWithValue(
                            "@Courses",
                            student.Courses);

                        // Education parameter
                        cmd.Parameters.AddWithValue(
                            "@Education",
                            student.Education);

                        // INSERT query execute ki
                        cmd.ExecuteNonQuery();
                    }

                    // Success message
                    MessageBox.Show(
                        "Student Added Successfully");
                }
            }
            catch (Exception ex)
            {
                // Error message
                MessageBox.Show(
                    "Error: " + ex.Message);
            }

            // Save ke baad GridView ko refresh kiya
            LoadStudents();
        }

        private void dataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }

        private void btnload_Click(
            object sender,
            EventArgs e)
        {
            // Load button click hone par
            // database ka data GridView mein show hoga
            LoadStudents();
        }

        private void LoadStudents()
        {
            // Database connection object create kiya
            SqlConnection con =
                new SqlConnection(ConnectionString);

            try
            {
                // Database connection open ki
                con.Open();

                // person table se data fetch karne ki query
                string query =
                    "SELECT * FROM person";

                // SQL command create ki
                SqlCommand cmd =
                    new SqlCommand(query, con);

                // Query execute ki
                // aur data reader create kiya
                SqlDataReader reader =
                    cmd.ExecuteReader();

                // Empty DataTable create ki
                DataTable table =
                    new DataTable();

                // DataReader ka data DataTable mein load kiya
                table.Load(reader);

                // DataTable ko DataGridView mein show kiya
                dataGridView1.DataSource =
                    table;

                // DataReader close kiya
                reader.Close();
            }
            catch (Exception ex)
            {
                // Loading error show kiya
                MessageBox.Show(
                    "Error Loading Data: " +
                    ex.Message);
            }
            finally
            {
                // Database connection close ki
                if (con.State ==
                    ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}