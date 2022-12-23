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
    public partial class VisitingCard : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; user id = root; password = 0000; database = velos");
        public VisitingCard()
        {
            InitializeComponent();

        }

        private void id_f()
        {
            MySqlCommand cow = new MySqlCommand("SELECT MAX(idКлиенты) FROM клиенты", con);
            cow.Parameters.AddWithValue("@idКлиенты", ID.Text);

            string id = cow.ExecuteScalar().ToString();

            if (id != null)
            {
                ID.Text = id.ToString();
            }
            else
            {
                MessageBox.Show("pc");
            }

        }
        private void func()
        {
            MySqlCommand com = new MySqlCommand("SELECT Имя FROM клиенты WHERE idКлиенты = "+ ID.Text + "", con);
            MySqlCommand coq = new MySqlCommand("SELECT Фамилия FROM клиенты WHERE idКлиенты = " + ID.Text + "", con);
            
            com.Parameters.AddWithValue("@Имя", First_name.Text);
            coq.Parameters.AddWithValue("@Фамилия", Last_name.Text);
            
            //con.Open();
            string fname = com.ExecuteScalar().ToString();
            string lname = coq.ExecuteScalar().ToString();
            
            if (fname != null)
            {
                First_name.Text = fname.ToString();
                Last_name.Text = lname.ToString();
            }
            else
            {
                MessageBox.Show("pc");
            }
        }

        private void First_name_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VisitingCard_Load(object sender, EventArgs e)
        {
            con.Open();
            id_f();
            func();
        }
    }
}
