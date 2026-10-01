using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StdInfoSystem
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SBTimer.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        private void mnuFileRegister_Click(object sender, EventArgs e)
        {
            (new Register()).ShowDialog();
        }
    }
}
