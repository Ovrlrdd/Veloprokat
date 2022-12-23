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
    public partial class Make_Order : UserControl
    {
        private MySqlConnection con;

        public Make_Order()
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

        private string output()
        {
            StringBuilder sb = new StringBuilder();
            string selectQuery = "SELECT Фамилия FROM Клиенты";
            try
            {
                using (MySqlCommand com = new MySqlCommand(selectQuery, con))
                {
                    using (MySqlDataReader rd = com.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            comboBox1.Items.Add(rd[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sb.ToString();
        }

        private string output1()
        {
            StringBuilder sn = new StringBuilder();
            string selectQuery = "SELECT Модель FROM Велосипеды WHERE Статус = 'В велопрокате'";
            try
            {
                using (MySqlCommand com = new MySqlCommand(selectQuery, con))
                {
                    using (MySqlDataReader rd = com.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            comboBox2.Items.Add(rd[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sn.ToString();
        }

        private void Make_Order_Load(object sender, EventArgs e)
        {
            
        }


        public void insertData()
        {
            try
            {
                string str = "INSERT INTO информация ( Клиенты_Фамилия, Велосипеды_Модель, Время_начала, Срок, Сумма) VALUES ( @Клиенты_Фамилия, @Велосипеды_Модель, @Время_начала, @Срок, @Сумма)";
                MySqlCommand cmd = new MySqlCommand(str, con);

                cmd.Parameters.AddWithValue("@Клиенты_Фамилия", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Велосипеды_Модель", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Время_начала", Start.Text);
                cmd.Parameters.AddWithValue("@Срок", int.Parse(Count.Text));
                cmd.Parameters.AddWithValue("@Сумма", double.Parse(Cost.Text));
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException mex)
            {
                MessageBox.Show("Server error" + mex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            Start.Clear();
        }

        private void Cost_Click(object sender, EventArgs e)
        {
            Cost.Clear();
        }

        private void Count_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Count_Click(object sender, EventArgs e)
        {
            int a = (int)Count.Value;
            int c;
            c = 300 * a;
            Cost.Text = c.ToString();
        }

        private void button_make_Click(object sender, EventArgs e)
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
                string updateQuery = "UPDATE велосипеды SET Статус = 'В пути' WHERE Модель = '" + comboBox2.Text + "' AND Статус = 'В велопрокате'";
                MySqlCommand command = new MySqlCommand(updateQuery, con);
                command.ExecuteNonQuery();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                comboBox1.Text = output();
                comboBox2.Text = output1();
                con.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
