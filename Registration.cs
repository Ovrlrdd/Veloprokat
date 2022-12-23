using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace _123
{
    public partial class Registration : UserControl
    {
        private MySqlConnection con;
        public Registration()
        {
            InitializeComponent();
        }

        private void DB()
        {
            FileStream load1 = null;
            load1 = new FileStream(@"C:\Velosiped\Server.txt", FileMode.Open);
            StreamReader read1 = new StreamReader(load1);
            string server = read1.ReadToEnd();
            load1.Close();

            FileStream load2 = null;
            load2 = new FileStream(@"C:\Velosiped\User_id.txt", FileMode.Open);
            StreamReader read2 = new StreamReader(load2);
            string user_id = read2.ReadToEnd();
            load2.Close();

            FileStream load3 = null;
            load3 = new FileStream(@"C:\Velosiped\Password.txt", FileMode.Open);
            StreamReader read3 = new StreamReader(load3);
            string password = read3.ReadToEnd();
            load3.Close();

            FileStream load4 = null;
            load4 = new FileStream(@"C:\Velosiped\Data_base.txt", FileMode.Open);
            StreamReader read4 = new StreamReader(load4);
            string data_base = read4.ReadToEnd();
            load4.Close();

            con = new MySqlConnection("server = " + server + "; user id = " + user_id + "; password = " + password + "; database = " + data_base + "");
        }
        public void insertData()
        {

            try
            {
                string str = "INSERT INTO клиенты ( Фамилия, Имя, Отчество, Паспорт) VALUES ( @Фамилия, @Имя, @Отчество, @Паспорт)";
                MySqlCommand cmd = new MySqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Фамилия", textBox1.Text);
                cmd.Parameters.AddWithValue("@Имя", textBox2.Text);
                cmd.Parameters.AddWithValue("@Отчество", textBox3.Text);
                cmd.Parameters.AddWithValue("@Паспорт", int.Parse(textBox4.Text));//конвертация в тип int
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException mex)
            {
                MessageBox.Show("Server error: " + mex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void TextBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void TextBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void TextBox4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button_reg_Click(object sender, EventArgs e)
        {
            string path = @"C:\Velosiped";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                MessageBox.Show("База данных не найдена или неправильно заданы параметры подключения! Добавьте информацию о подключении во вкладке <<База данных>>.", "Ошибка");
            }
            else
            {
                DB();
                con.Open();
                insertData();
                VisitingCard newForm = new VisitingCard();
                newForm.Show();
            }
        }
    }
}
