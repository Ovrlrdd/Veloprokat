using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _123
{
    public partial class JoinDB : Form
    {
        public JoinDB()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void inp()
        {
            try
            {
                string path = @"C:\Velosiped";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                //Console.WriteLine("Введите строку для записи в файл:");
                string text1 = textBox1.Text;
                string text2 = textBox2.Text;
                string text3 = textBox3.Text;
                string text4 = textBox4.Text;

                // запись в файл
                using (FileStream fstream1 = new FileStream(@"C:\Velosiped\Server.txt", FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array1 = System.Text.Encoding.Default.GetBytes(text1);
                    // запись массива байтов в файл
                    fstream1.Write(array1, 0, array1.Length);    
                }
                using (FileStream fstream2 = new FileStream(@"C:\Velosiped\User_id.txt", FileMode.OpenOrCreate))
                {
                    byte[] array2 = System.Text.Encoding.Default.GetBytes(text2);
                    fstream2.Write(array2, 0, array2.Length);  
                }
                using (FileStream fstream3 = new FileStream(@"C:\Velosiped\Password.txt", FileMode.OpenOrCreate))
                {
                    byte[] array3 = System.Text.Encoding.Default.GetBytes(text3);
                    fstream3.Write(array3, 0, array3.Length);
                }
                using(FileStream fstream4 = new FileStream(@"C:\Velosiped\Data_base.txt", FileMode.OpenOrCreate))
                {
                    byte[] array4 = System.Text.Encoding.Default.GetBytes(text4);
                    fstream4.Write(array4, 0, array4.Length);
                }
                }
                    catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            MessageBox.Show("Данные о базе данных добавлены");
        }

        private void button_make_Click(object sender, EventArgs e)
        {
            inp();
        }
    }
    }

