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
    public partial class Active : Form
    {

        public Active()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DB()
        {
            string path = @"C:\Velosiped";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                MessageBox.Show("База данных не найдена или неправильно заданы параметры подключения! Добавьте информацию о подключении во вкладке <<База данных>>.", "Ошибка");
            }
            else
            {
                FileStream load1 = null;
                load1 = new FileStream(@"C:\Velosiped\Server.txt", FileMode.Open);
                StreamReader read1 = new StreamReader(load1);
                string server = read1.ReadToEnd();
                load1.Close();

                FileStream load2 = null;
                load2= new FileStream(@"C:\Velosiped\User_id.txt", FileMode.Open);
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

                MySqlConnection con = new MySqlConnection("server = " + server + "; user id = " + user_id + "; password = " + password + "; database = " + data_base + "");

                con.Open();
                //string selectQuery = "SELECT Фамилия, Модель, Время_начала 'Время начала аренды', Срок 'Количество часов', Сумма 'Стоимость' FROM velos.информация INNER JOIN velos.клиенты ON idКлиенты = Клиенты_idКлиенты INNER JOIN velos.велосипеды ON idВелосипеды = Велосипеды_idВелосипеды";
                string selectQuery = "SELECT Клиенты_Фамилия 'Фамилия', Велосипеды_Модель 'Модель', Время_начала 'Время начала аренды', Срок 'Количество часов', Сумма 'Стоимость' FROM информация";
                DataTable DATA = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, con);
                adapter.Fill(DATA);
                dataGridView1.DataSource = DATA;
                con.Close();

                dataGridView1.Columns["Фамилия"].Width = 120;
                dataGridView1.Columns["Модель"].Width = 130;
                dataGridView1.Columns["Время начала аренды"].Width = 160;
                dataGridView1.Columns["Количество часов"].Width = 150;
                dataGridView1.Columns["Стоимость"].Width = 102;
            }
        }

        private void Active_Load(object sender, EventArgs e)
        {
            DB();
        }

        /*private void Button2_Click(object sender, EventArgs e)
        {
            con.Open();
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            String selectedCustomersID = "";
            selectedCustomersID = dataGridView1.Rows[selectedIndex].Cells["idИнформация"].Value.ToString();
            dataGridView1.Rows.RemoveAt(selectedIndex);
            string deleteQuery = "DELETE FROM информация WHERE idИнформация = " + selectedCustomersID;
            MySqlCommand command = new MySqlCommand(deleteQuery, con);
            MySqlDataReader myReader;
            myReader = command.ExecuteReader();
            dataGridView1.Refresh();

            con.Close();
        }*/
    }
}
