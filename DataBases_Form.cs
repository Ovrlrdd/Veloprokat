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
    public partial class DataBases_Form : Form
    {
        private MySqlConnection con = null;
        
        public DataBases_Form()
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

        private void showData()
        {
            string selectQuery = "SELECT * FROM "+comboBox1.Text+"";
            DataTable DATA = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, con);
            adapter.Fill(DATA);
            dataGridView1.DataSource = DATA;

        }

        private void alter()
        {
            con.Open();
            string selectQuery = "ALTER TABLE "+comboBox1.Text+" AUTO_INCREMENT=0";
            MySqlCommand mycommand = new MySqlCommand(selectQuery, con);
            MySqlDataReader reader;
            reader = mycommand.ExecuteReader();
            con.Close();
        }

        private void delete()
        {
            con.Open();
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            String selectedCustomersID = "";
            if(comboBox1.Text == "клиенты")
            {
                selectedCustomersID = dataGridView1.Rows[selectedIndex].Cells["idКлиенты"].Value.ToString();
                dataGridView1.Rows.RemoveAt(selectedIndex);
                string deleteQuery = "DELETE FROM клиенты WHERE idКлиенты = " + selectedCustomersID;
                MySqlCommand command = new MySqlCommand(deleteQuery, con);
                MySqlDataReader myReader;
                myReader = command.ExecuteReader();
                dataGridView1.Refresh();
                con.Close();
            }
            if(comboBox1.Text == "велосипеды")
            {
                selectedCustomersID = dataGridView1.Rows[selectedIndex].Cells["idВелосипеды"].Value.ToString();
                dataGridView1.Rows.RemoveAt(selectedIndex);
                string deleteQuery = "DELETE FROM велосипеды WHERE idВелосипеды = " + selectedCustomersID;
                MySqlCommand command = new MySqlCommand(deleteQuery, con);
                MySqlDataReader myReader;
                myReader = command.ExecuteReader();
                dataGridView1.Refresh();
                con.Close();
            }
            if(comboBox1.Text == "информация")
            {
                selectedCustomersID = dataGridView1.Rows[selectedIndex].Cells["idИнформация"].Value.ToString();
                dataGridView1.Rows.RemoveAt(selectedIndex);
                string deleteQuery = "DELETE FROM информация WHERE idИнформация = " + selectedCustomersID;
                MySqlCommand command = new MySqlCommand(deleteQuery, con);
                MySqlDataReader myReader;
                myReader = command.ExecuteReader();
                dataGridView1.Refresh();
                con.Close();
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataBases_Form_Load(object sender, EventArgs e)
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
                con.Close();
            }
        }

        private string output()
        {
            StringBuilder sb = new StringBuilder();
            string selectQuery = "show tables";
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

        private void button2_Click(object sender, EventArgs e)
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
                showData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                delete();
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                alter();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bikes newForm = new Bikes();
            newForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            JoinDB newForm = new JoinDB();
            newForm.Show();
        }
    }
}
