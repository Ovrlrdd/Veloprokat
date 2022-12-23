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
using System.IO;

namespace _123
{
    public partial class Bikes : Form
    {
        private MySqlConnection con;
        public Bikes()
        {
            InitializeComponent();
            //con.Open();
            comboBox1.Items.Add("Горный");
            comboBox1.Items.Add("Шоссейный");
            comboBox1.Items.Add("Детский");

            comboBox2.Items.Add("В велопрокате");
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
                string str = "INSERT INTO велосипеды ( Модель, Тип, Статус) VALUES ( @Модель, @Тип, @Статус)";
                
                MySqlCommand cmd = new MySqlCommand(str, con);

                // cmd.Parameters.AddWithValue("@ID", textBox_ID);
                cmd.Parameters.AddWithValue("@Модель", textBox1.Text);
                cmd.Parameters.AddWithValue("@Тип", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Статус", comboBox2.Text);
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
                //con.Close();
            }
        }

        private void showData()
        {
            string selectQuery = "SELECT * FROM велосипеды";
            DataTable DATA = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, con);
            adapter.Fill(DATA);
            dataGridView1.DataSource = DATA;
        }

        private void Bikes_Load(object sender, EventArgs e)
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
                showData();
                con.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
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
                showData();
                con.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string path = @"C:\Velosiped";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                MessageBox.Show("База данных не найдена или неправильно заданы параметры подключения! Добавьте информацию о подключении во вкладке <<База данных>>.", "Ошибка");
            }
            else
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
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
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                String selectedCustomersID = "";

                selectedCustomersID = dataGridView1.Rows[selectedIndex].Cells["idВелосипеды"].Value.ToString();
                dataGridView1.Rows.RemoveAt(selectedIndex);

                string deleteQuery = "DELETE FROM велосипеды WHERE idВелосипеды = " + selectedCustomersID;
                MySqlCommand command = new MySqlCommand(deleteQuery, con);
                MySqlDataReader myReader;
                myReader = command.ExecuteReader();
                dataGridView1.Refresh();
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
                string selectQuery = "ALTER TABLE велосипеды AUTO_INCREMENT=0";
                MySqlCommand mycommand = new MySqlCommand(selectQuery, con);
                MySqlDataReader reader;
                reader = mycommand.ExecuteReader();
                con.Close();
            }
        }
    }
}
