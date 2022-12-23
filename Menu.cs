using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace _123
{
    public partial class Menu : Form
    {
        //MySqlConnection con = new MySqlConnection("server = localhost; user id = root; password = 0000; database = velos");
        public Menu()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            firstUserControl1.BringToFront();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MakeOrder newForm = new MakeOrder();
            newForm.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Active newForm = new Active();
            newForm.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            End newForm = new End();
            newForm.Show();
        }

        private void button_screen_Click(object sender, EventArgs e)
        {
            frontPanel1.BringToFront();     
        }

        private void button_db_Click(object sender, EventArgs e)
        {
            Admin_auth newForm = new Admin_auth();
            newForm.Show();
        }

        private void instagram_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://instagram.com/volga_apple?igshid=154sfkfwmk461");
        }

        private void vk_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com");
        }
    }
}
