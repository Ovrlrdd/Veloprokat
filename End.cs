﻿using System;
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
    public partial class End : Form
    {
        private MySqlConnection con;

        public End()
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
                string updateQuery = "UPDATE велосипеды SET Статус = 'В велопрокате' WHERE Модель = '" + textBox1.Text + "' AND Статус = 'В пути'";
                string deleteQuery = "DELETE FROM информация WHERE Велосипеды_Модель = '" + textBox1.Text + "'";
                MySqlCommand command_1 = new MySqlCommand(updateQuery, con);
                command_1.ExecuteNonQuery();
                MySqlCommand command_2 = new MySqlCommand(deleteQuery, con);
                MySqlDataReader myReader;
                myReader = command_2.ExecuteReader();
                con.Close();
            }
        }
    }
}
